using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using fancyzebra.net.sdk.core.Dtos;
using fancyzebra.net.sdk.core.Services;

namespace fancyzebra.net.sdk.forms
{
    public class GracePrivacy: IGracePrivacyBuilder
    {
        private GracePrivacy _instance;
        private readonly IPrivacyService _privacyService;
        private string _appId;
        private string _userId;
        private CultureInfo _culture;
        private ViewDetails _details;

        private GracePrivacy()
        {
            this._privacyService = new PrivacyService();
        }

        public GracePrivacy Instance
        {
            get
            {
                if (this._instance == null)
                    this._instance = new GracePrivacy();
                return this._instance;
            }
        }

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
            this.ManageResponse(this.Response);
        }

        public PrivacyResponseDto Response { get; private set; }

        private void ManageResponse(PrivacyResponseDto responseDto)
        {
            if (!responseDto.Documents.Any())
                return;
            this.InjectDocumentView();
        }

        private void InjectDocumentView()
        {
            if (this._details != null)
            {
                //todo view personalization
            }
            //todo get INavigationInstance and inject view
        }
    }
}