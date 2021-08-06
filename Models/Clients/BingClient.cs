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
    public class BingClient : IWebClient
    {
        private readonly BingSearchEngineConfig searchProvider;
        public const string SearchProviderName = "Bing";

        public HttpClient Client { get; set; }

        public BingClient(BingSearchEngineConfig searchProvider)
        {
            this.searchProvider = searchProvider;
            Setup(this.searchProvider);
        }

        private SearchResult DeserializeDataToResult(string query, string responseContent)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var searchInfo = JsonSerializer.Deserialize<BingResponse>(responseContent, options);
            return new SearchResult()
            {
                Query = query,
                SearchEngine = SearchProviderName,
                Total = searchInfo.WebPages.TotalEstimatedMatches
            };
        }

        public async Task<SearchResult> GetSearchTotal(string query)
        {
            try
            {
                HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, $"?q={query}");
                message.Headers.Add("Ocp-Apim-Subscription-Key", searchProvider.Key);
                var response = await Client.SendAsync(message);
                return DeserializeDataToResult(query, await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                throw new Exception($"{SearchProviderName} API client failed to respond or extract data");
            }
        }

        public void Setup(SearchProviders searchProvider)
        {
            Client = new HttpClient();
            Client.BaseAddress = new Uri(searchProvider.URI);
        }
    }
}
