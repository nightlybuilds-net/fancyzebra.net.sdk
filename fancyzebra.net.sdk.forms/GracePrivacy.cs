using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using fancyzebra.net.sdk.core.Dtos;
using fancyzebra.net.sdk.core.Exceptions;
using fancyzebra.net.sdk.core.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace fancyzebra.net.sdk.forms
{
    public class GracePrivacy: IGracePrivacy, IGracePrivacyBuilder
    {
        private static GracePrivacy _instance;
        private readonly IPrivacyService _privacyService;
        private string _appId;
        private string _userId;
        private CultureInfo _culture;
        private ViewDetails _details;
        private Application _app;
        private IStringLocalizer _stringLocalizer;
        private Page _page;

        private GracePrivacy()
        {
            this._privacyService = new PrivacyService();
            this._stringLocalizer = new StringLocalizer();
        }

        public static GracePrivacy Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new GracePrivacy();
                return _instance;
            }
        }

        #region Builder

        public IGracePrivacyBuilder WithAppId(string id)
        {
            this._appId = id;
            return this;
        }

        public IGracePrivacyBuilder WithUserId(string id)
        {
            this._userId = id;
            return this;
        }

        public IGracePrivacyBuilder WithCulture(CultureInfo cultureInfo)
        {
            this._culture = cultureInfo;
            return this;
        }

        public IGracePrivacyBuilder WithViewDetails(ViewDetails details)
        {
            this._details = details;
            return this;
        }

        public IGracePrivacyBuilder WithApp(Application app)
        {
            this._app = app;
            return this;
        }

        public IGracePrivacyBuilder WithIStringLocalizer(IStringLocalizer stringLocalizer)
        {
            this._stringLocalizer = stringLocalizer;
            return this;
        }

        #endregion

        public async Task Init()
        {
            if (this._appId == default || this._userId == default)
            {
                throw new Exception("AppId and UserId must have a value");
            }
            
            if(this._culture == null)
                this._culture = CultureInfo.CurrentUICulture;

            this._privacyService.Init(this._appId, this._userId, this._culture);
            this.Response = await this._privacyService.GetDocumentAsync();
            await this.ManageResponse(this.Response);
        }

        public PrivacyResponseDto Response { get; private set; }

        private async Task ManageResponse(PrivacyResponseDto responseDto)
        {
            if (!responseDto.Documents.Any())
                return;
            await this.InjectDocumentView();
        }

        private async Task InjectDocumentView()
        {
            this._page = this.GetModalPage();
            await this._app.NavigationProxy.PushModalAsync(this._page);
        }

        private Page GetModalPage()
        {
            var modal = new ContentPage();

            var documentContainer = new StackLayout() {Orientation = StackOrientation.Vertical};


            foreach (var document in this.Response.Documents)
            {
                documentContainer.Children.Add(new Label(){Text = document.Text});
                foreach (var clause in document.Clauses)
                {
                    documentContainer.Children.Add(new Label(){Text = clause.Text});
                    var checkBoxContainer = new StackLayout() {Orientation = StackOrientation.Horizontal};
                    checkBoxContainer.Children.Add(new Label() {Text = this._stringLocalizer.Accept});
                    checkBoxContainer.Children.Add(new CheckBox());
                }
            }

            var acceptButton = new Button
            {
                Text = this._stringLocalizer.Request,
                Command = new Command(async () => await this.InnerAccept())
            };
            
            documentContainer.Children.Add(acceptButton);

            modal.Content = documentContainer; 

            
            if (this._details != null)
            {
                //todo view personalization
            }

            return modal;
        }

        private async Task InnerAccept()
        {
            try
            {
                this.ThrowForNoConnection();
                this.IsBusy = true;
            }
            catch (Exception e)
            {
                var errorMessage = this.GetMessageFromException(e);
                await this._page.DisplayAlert(this._stringLocalizer.Error, errorMessage, this._stringLocalizer.Ok);
            }
            finally
            {
                this.IsBusy = false;
            }
        }

        private string GetMessageFromException(Exception exception)
        {
            switch (exception)
            {
                case ConnectivityException ce:
                    return this._stringLocalizer.NoConnectionMessage;
                default:
                    return this._stringLocalizer.GenericError;
            }
        }

        public bool IsBusy { get; set; }

        public void ThrowForNoConnection()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                throw new ConnectivityException();
        }
    }
}