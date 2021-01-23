using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using fancyzebra.net.sdk.core.Dtos;
using fancyzebra.net.sdk.core.Exceptions;
using fancyzebra.net.sdk.core.Services;
using fancyzebra.net.sdk.forms.Annotations;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace fancyzebra.net.sdk.forms.Xaml
{
    public class GracePrivacyViewModel: INotifyPropertyChanged
    {
        private readonly NavigationProxy _navigationProxy;
        private readonly Page _page;
        private readonly IPrivacyService _privacyService;
        public IStringLocalizer StringLocalizer { get; private set; }
        private ObservableCollection<DocumentDto> _documents;
        
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand CloseCommand { get; private set; }
        public ICommand AcceptCommand { get; private set; }

        public ObservableCollection<DocumentDto> Documents
        {
            get => this._documents;
            set
            {
                this._documents = value;
                this.OnPropertyChanged();
            }
        }

        public GracePrivacyViewModel(NavigationProxy navigationProxy,
            Page page,
            IPrivacyService privacyService,
            IStringLocalizer stringLocalizer)
        {
            this._navigationProxy = navigationProxy;
            this._page = page;
            this._privacyService = privacyService;
            this.StringLocalizer = stringLocalizer;
            this.Documents = new ObservableCollection<DocumentDto>();
            this.CloseCommand = new Command(async () => await this._navigationProxy.PopModalAsync());
            this.AcceptCommand = new Command(async () => await this.InnerAccept());

            this._page.Appearing += async (sender, args) => await this.GetDocuments();
        }

        private async Task GetDocuments()
        {
            try
            {
                this.ThrowForNoConnection();
                this.IsBusy = true;
                var responseDto = await this._privacyService.GetDocumentAsync();
                this.Documents.Clear();
                responseDto.Documents.ForEach(document => this.Documents.Add(document));
            }
            catch (Exception e)
            {
                var errorMessage = this.GetMessageFromException(e);
                await this._page.DisplayAlert(this.StringLocalizer.Error, errorMessage, this.StringLocalizer.Ok);
            }
            finally
            {
                this.IsBusy = false;
            }
        }

        private async Task InnerAccept()
        {
            try
            {
                this.ThrowForNoConnection();
                this.IsBusy = true;
                var isAccepted = this.CheckAcceptance();
                if (!isAccepted)
                    throw new AcceptanceException();
                await this._privacyService.AcceptDocumentAsync();
                await this._navigationProxy.PopModalAsync();
            }
            catch (Exception e)
            {
                var errorMessage = this.GetMessageFromException(e);
                await this._page.DisplayAlert(this.StringLocalizer.Error, errorMessage, this.StringLocalizer.Ok);
            }
            finally
            {
                this.IsBusy = false;
            }
        }

        private bool CheckAcceptance()
        {
            return this.Documents
                .SelectMany(s => s.Clauses)
                .Where(cl => cl.IsMandatory)
                .All(cl => cl.IsAccepted);
        }

        public bool IsBusy { get; set; }

        public void ThrowForNoConnection()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                throw new ConnectivityException();
        }
        
        private string GetMessageFromException(Exception exception)
        {
            switch (exception)
            {
                case ConnectivityException ce:
                    return this.StringLocalizer.NoConnectionMessage;
                case AcceptanceException ae:
                    return this.StringLocalizer.MandatoryClausesMissingMessage;
                default:
                    return this.StringLocalizer.GenericError;
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}