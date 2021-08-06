using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Tranzact.SearchFight.Configuration;
using Tranzact.SearchFight.Models.Interfaces;
using Tranzact.SearchFight.Models.SearchModels;

namespace Tranzact.SearchFight.Models.Clients
{
    public class BingClient : IRequestClient
    {
        private readonly SearchProviders searchProvider;
        private const string SearchProviderName = "Bing";

        public HttpClient client { get; set; }

        public BingClient(SearchProviders[] searchProvider)
        {
            this.searchProvider = searchProvider.FirstOrDefault(g => g.Provider == SearchProviderName);
            Setup(this.searchProvider);
        }

        private CountResult DeserializeDataToResult(string query, string responseContent)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var searchInfo = JsonSerializer.Deserialize<BingResponse>(responseContent, options);
            return new CountResult()
            {
                Query = query,
                SearchEngine = SearchProviderName,
                Total = searchInfo.WebPages.TotalEstimatedMatches
            };
        }

        public async Task<CountResult> GetResultsTotal(string query)
        {
            try
            {
                HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, $"?q={query}");
                message.Headers.Add("Ocp-Apim-Subscription-Key", searchProvider.APIKey);
                var response = await client.SendAsync(message);
                return DeserializeDataToResult(query, await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                throw new Exception($"{SearchProviderName} API client failed to respond or extract data");
            }
        }

        public void Setup(SearchProviders searchProvider)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(searchProvider.BaseUri);
        }
    }
}
