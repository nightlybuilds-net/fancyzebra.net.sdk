using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using fancyzebra.net.sdk.core.Dtos;
using Newtonsoft.Json;

namespace fancyzebra.net.sdk.core.Services
{
    public class PrivacyService: IPrivacyService
    {
        private readonly HttpClient _httpClient;
        private const string ApiUrl = "todo";
        private const string FunctionKey = "todo";
        public PrivacyService()
        {
            this._httpClient = new HttpClient()
            {
                Timeout = TimeSpan.FromSeconds(10),
                BaseAddress = new Uri(ApiUrl)
            };
        }
        public void Init(string appId, string userId, CultureInfo culture)
        {
            this._httpClient.DefaultRequestHeaders.Add("AppId", appId);
            this._httpClient.DefaultRequestHeaders.Add("UserId", userId);
            this._httpClient.DefaultRequestHeaders.Add("Culture", culture.Name); //four letters
        }

        public async Task<PrivacyResponseDto> GetDocumentAsync()
        {
            var response = await this._httpClient.GetAsync("getdocument");
            var payload = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<PrivacyResponseDto>(payload);
        }
    }
}
