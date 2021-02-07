using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using fancyzebra.net.sdk.core.Dtos;
using fancyzebra.net.sdk.forms.Features;

namespace fancyzebra.net.sdk.forms
{
    public interface IFancyForms
    {
        /// <summary>
        /// Check documents for userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task EnsureDocumentsForUser(string userId);
    }
    
    public class FancyForms : IFancyForms
    {
        private readonly FancyBuilder _gracePrivacy;

        internal FancyForms(FancyBuilder gracePrivacy)
        {
            this._gracePrivacy = gracePrivacy;
        }
        
        
        public async Task EnsureDocumentsForUser(string userId)
        {
            this._gracePrivacy.Culture ??= CultureInfo.CurrentUICulture;

            this._gracePrivacy.PrivacyService.Init(this._gracePrivacy.AppId, userId, this._gracePrivacy.Culture);
            var documentsToAccept = await this._gracePrivacy.PrivacyService.GetDocumentAsync();
            if(documentsToAccept.Any())
                await this.InjectDocumentView(documentsToAccept);
        }

        
        private async Task InjectDocumentView(DocumentToAcceptDto[] documentToAcceptDtos)
        {
            var page = new FancyAcceptPage(this._gracePrivacy.App.NavigationProxy, this._gracePrivacy.PrivacyService, this._gracePrivacy.StringLocalizer, documentToAcceptDtos);
            await this._gracePrivacy.App.NavigationProxy.PushModalAsync(page);
        }
    }
}