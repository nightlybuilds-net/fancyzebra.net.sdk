using System;
using System.Collections.Generic;
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
            
            // this._httpClient.DefaultRequestHeaders.Add("AppId", appId);
            // this._httpClient.DefaultRequestHeaders.Add("UserId", userId);
            // this._httpClient.DefaultRequestHeaders.Add("Culture", culture.Name); //four letters
        }

        public async Task<DocumentToAcceptDto[]> GetDocumentAsync()
        {
            try
            {
                var response = await this._httpClient.GetAsync($"http://localhost:7071/api/acceptance/mydocuments?appId={this._appId}&userId={this._userId}&lang={this._culture}");
                var payload = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<DocumentToAcceptDto[]>(payload);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task AcceptDocumentAsync()
        {
            await Task.Delay(1000);
        }

        public async Task<bool> CheckDocumentsAsync()
        {
            await Task.Delay(1000);
            return false;
        }
    }
}
