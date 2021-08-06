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
    class GoogleClient : IRequestClient
    {
        private readonly SearchProviders searchProvider;
        public const string SearchProviderName = "Google";
        public GoogleClient(SearchProviders[] searchProvider)
        {
            this.searchProvider = searchProvider.FirstOrDefault(g => g.Provider == SearchProviderName);
            Setup(this.searchProvider);
        }
        public HttpClient client { get; set; }

        public async Task<CountResult> GetResultsTotal(string query)
        {
            try
            {
                var response = await client.GetAsync($"?key={searchProvider.APIKey}&cx={searchProvider.Other}&q={query}");
                return DeserializeDataToResult(query,await response.Content.ReadAsStringAsync());
            }
            catch
            {
                throw new Exception($"{SearchProviderName} API client failed to respond or extract data");
            }
        }

        public void Setup(SearchProviders searchProvider)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(searchProvider.BaseUri);
        }

        private CountResult DeserializeDataToResult(string query, string responseContent)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var searchInfo = JsonSerializer.Deserialize<GoogleResponse>(responseContent,options);
            return new CountResult()
            {
                Query = query,
                SearchEngine = SearchProviderName,
                Total = long.Parse(searchInfo.SearchInformation.TotalResults)
            };
        }
    }
}
