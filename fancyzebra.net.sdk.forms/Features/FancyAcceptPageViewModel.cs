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
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace fancyzebra.net.sdk.forms.Features
{
    public class FancyAcceptPageViewModel : INotifyPropertyChanged
    {
        private readonly NavigationProxy _navigationProxy;
        private readonly Page _page;
        private readonly IPrivacyService _privacyService;
        private readonly DocumentToAcceptDto[] _documentToAcceptDtos;
        public IStringLocalizer StringLocalizer { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand CloseCommand { get; private set; }
        public ICommand AcceptCommand { get; private set; }

        public ObservableCollection<DocumentToAcceptDto> Documents { get; set; }

        public FancyAcceptPageViewModel(NavigationProxy navigationProxy,
            Page page,
            IPrivacyService privacyService,
            IStringLocalizer stringLocalizer, DocumentToAcceptDto[] documentToAcceptDtos)
        {
            this._navigationProxy = navigationProxy;
            this._page = page;
            this._privacyService = privacyService;
            this._documentToAcceptDtos = documentToAcceptDtos;
            this.StringLocalizer = stringLocalizer;
            this.Documents = new ObservableCollection<DocumentToAcceptDto>();
            this.CloseCommand = new Command(async () => await this._navigationProxy.PopModalAsync());
            this.AcceptCommand = new Command(async () => await this.InnerAccept(), () => !this.IsBusy);
            this.ShowDocuments();
        }

        private void ShowDocuments()
        {
            try
            {
                this.ThrowForNoConnection();
                this.IsBusy = true;
                this.Documents.Clear();
                this._documentToAcceptDtos.ForEach(document => this.Documents.Add(document));
            }
            catch (Exception e)
            {
                var errorMessage = this.GetMessageFromException(e);
                this._page.DisplayAlert(this.StringLocalizer.Error, errorMessage, this.StringLocalizer.Ok);
                this._navigationProxy.PopModalAsync();
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

                await this._privacyService.AcceptDocumentAsync(this.Documents.Select(documentToAcceptDto =>
                    new AcceptDocumentTextRequest
                    {
                        DocumentTextId = documentToAcceptDto.DocumentText.Id,
                        Clauses = documentToAcceptDto.Clauses.Select(dto => new AcceptClauseRequest
                        {
                            Accepted = dto.IsAccepted,
                            ClauseId = dto.Id
                        })
                    }));
                await this._navigationProxy.PopModalAsync();
            }
            catch (Exception e)
            {
                var errorMessage = this.GetMessageFromException(e);
                await this._page.DisplayAlert(this.StringLocalizer.Error, errorMessage, this.StringLocalizer.Ok);
                
                // no connection? server error... unblock user app
                if(e.GetType() != typeof(AcceptanceException))
                    await this._navigationProxy.PopModalAsync();
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

        private bool _isBusy;
        public bool IsBusy
        {
            get => this._isBusy;
            set
            {
                this._isBusy = value;
                ((Command)this.AcceptCommand).ChangeCanExecute();
            }
        }

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