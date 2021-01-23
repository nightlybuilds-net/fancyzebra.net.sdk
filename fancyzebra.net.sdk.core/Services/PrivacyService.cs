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
        private const string ApiUrl = "https://foo.bar/";
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
            // var response = await this._httpClient.GetAsync("getdocument");
            // var payload = await response.Content.ReadAsStringAsync();
            // return JsonConvert.DeserializeObject<PrivacyResponseDto>(payload);
            await Task.Delay(1000);
            return this.FakeDocument;
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

        public PrivacyResponseDto FakeDocument { get; set; } = new PrivacyResponseDto()
        {
            Documents = new List<DocumentDto>()
            {
                new DocumentDto()
                {
                    Text = @"On the Ropes Meaning: Being in a situation that looks to be hopeless! It's Not Brain Surgery Meaning: A task that's easy to accomplish, a thing lacking complexity. Birds of a Feather Flock Together Meaning: People tend to associate with others who share similar interests or values. Roll With the Punches Meaning: To tolerate or endure through the unexpected mishappenings you may encounter from time to time. Playing Possum Meaning: Pretending to be dead, or to be deceitful about something.",
                    Clauses = new List<ClauseDto>()
                    {
                        new ClauseDto()
                        {
                            Text = "I Accept the Ropes",
                            IsMandatory = true
                        },
                        new ClauseDto()
                        {
                            Text = "I Accept Feather",
                            IsMandatory = false
                        }
                    }
                },
                new DocumentDto()
                {
                    Text = @"What Goes Up Must Come Down Meaning: Things that go up must eventually return to the earth due to gravity. Drive Me Nuts Meaning: To greatly frustrate someone. To drive someone crazy, insane, bonkers, or bananas. Fight Fire With Fire Meaning: To retaliate with an attack that is similar to the attack used against you. Ring Any Bells? Meaning: Recalling a memory; causing a person to remember something or someone. Yada Yada Meaning: A way to notify a person that what they're saying is predictable or boring.",
                    Clauses = new List<ClauseDto>()
                    {
                        new ClauseDto()
                        {
                            Text = "I Accept Nuts",
                            IsMandatory = true
                        },
                        new ClauseDto()
                        {
                            Text = "I Accept Yada",
                            IsMandatory = false
                        }
                    }
                }
            }
        };
    }
}
