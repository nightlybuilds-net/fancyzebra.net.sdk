using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using fancyzebra.net.sdk.core.Dtos;
using fancyzebra.net.sdk.core.Exceptions;
using fancyzebra.net.sdk.core.Services;
using fancyzebra.net.sdk.forms.Xaml;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;

namespace fancyzebra.net.sdk.forms
{
    public class GracePrivacy: IGracePrivacyBuilder
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
            var response = await this._privacyService.CheckDocumentsAsync();
            if(!response)
                await this.InjectDocumentView();
        }

        private async Task InjectDocumentView()
        {
            this._page = new GracePrivacyPage(this._app.NavigationProxy, this._privacyService, this._stringLocalizer);
            await this._app.NavigationProxy.PushModalAsync(this._page);
        }

        
    }
}