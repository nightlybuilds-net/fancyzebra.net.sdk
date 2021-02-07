using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using fancyzebra.net.sdk.core.Dtos;
using System.Text.Json;



namespace fancyzebra.net.sdk.core.Services
{
    public class PrivacyService: IPrivacyService
    {
        private readonly HttpClient _httpClient;
        private string _appId;
        private string _userId;
        private CultureInfo _culture;
        
        private const string ApiUrl = "http://localhost:7071/api/acceptance/mydocuments";
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
            this._appId = appId;
            this._userId = userId;
            this._culture = culture;
            
            // this._httpClient.DefaultRequestHeaders.Add("appId", appId);
            // this._httpClient.DefaultRequestHeaders.Add("userId", userId);
            // this._httpClient.DefaultRequestHeaders.Add("culture", culture.Name); //four letters
        }

        public async Task<DocumentToAcceptDto[]> GetDocumentAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                new Uri("http://localhost:7071/api/acceptance/mydocuments"));
            request.Headers.Add("appId",new []{this._appId});
            request.Headers.Add("userId",new []{this._userId});
            request.Headers.Add("culture",new []{this._culture.Name});
            var response = await this._httpClient.SendAsync(request);
            var payload = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<DocumentToAcceptDto[]>(payload, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task AcceptDocumentAsync(IEnumerable<AcceptDocumentTextRequest> acceptResult)
        {
            var dto = new AcceptDocumentRequest
            {
                AppId = this._appId,
                AppUserId = this._userId,
                AcceptedTexts = acceptResult
            };
            
            var request = new HttpRequestMessage(HttpMethod.Post,
                new Uri("http://localhost:7071/api/acceptance/acceptdoc"))
            {
                Content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json")
            };
            // todo manage result
            await this._httpClient.SendAsync(request);
        }

        public async Task<bool> CheckDocumentsAsync()
        {
            await Task.Delay(1000);
            return false;
        }
    }
}
