using System.Globalization;
using System.Threading.Tasks;
using fancyzebra.net.sdk.core.Dtos;

namespace fancyzebra.net.sdk.core.Services
{
    public interface IPrivacyService
    {
        void Init(string appId, string userId, CultureInfo culture);
        Task<PrivacyResponseDto> GetDocumentAsync();
        Task AcceptDocumentAsync();
        Task<bool> CheckDocumentsAsync();
    }
}