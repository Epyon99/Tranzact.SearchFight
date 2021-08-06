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
    class GoogleClient : IWebClient
    {
        private readonly GoogleSearchEngineConfig searchProvider;
        public const string SearchProviderName = "Google";
        public GoogleClient(GoogleSearchEngineConfig searchProvider)
        {
            this.searchProvider = searchProvider;
            Setup(this.searchProvider);
        }
        public HttpClient Client { get; set; }

        public async Task<SearchResult> GetSearchTotal(string query)
        {
            try
            {
                var response = await Client.GetAsync($"?key={searchProvider.Key}&cx={searchProvider.CustomEngine}&q={query}");
                return DeserializeDataToResult(query,await response.Content.ReadAsStringAsync());
            }
            catch
            {
                throw new Exception($"{SearchProviderName} API client failed to respond or extract data");
            }
        }

        public void Setup(SearchProviders searchProvider)
        {
            Client = new HttpClient();
            Client.BaseAddress = new Uri(searchProvider.URI);
        }

        private SearchResult DeserializeDataToResult(string query, string responseContent)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var searchInfo = JsonSerializer.Deserialize<GoogleResponse>(responseContent,options);
            return new SearchResult()
            {
                Query = query,
                SearchEngine = SearchProviderName,
                Total = long.Parse(searchInfo.SearchInformation.TotalResults)
            };
        }
    }
}
