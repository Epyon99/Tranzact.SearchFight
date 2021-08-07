using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Tranzact.SearchFight.Common.Exceptions;
using Tranzact.SearchFight.Configuration;
using Tranzact.SearchFight.Models.Interfaces;
using Tranzact.SearchFight.Models.SearchModels;

namespace Tranzact.SearchFight.Clients
{
    public class GoogleClient : IWebClient
    {
        private readonly GoogleSearchEngineConfig searchProvider;
        public const string SearchProviderName = "Google";
        public IHttpClient Client { get; set; }

        public GoogleClient(GoogleSearchEngineConfig searchProvider, IHttpClient client)
        {
            this.searchProvider = searchProvider;
            this.Client = client;
        }

        public async Task<SearchResult> GetSearchTotal(string query)
        {
            try
            {
                var response = await Client.GetAsync($"?key={searchProvider.Key}&cx={searchProvider.CustomEngine}&q={query}");
                return DeserializeDataToResult(query, await response.Content.ReadAsStringAsync());
            }

            catch (APIJsonParsingException)
            {
                throw;
            }
            catch
            {
                throw new NoConnectivityException($"{SearchProviderName} API client failed to respond or extract data");
            }
        }

        public SearchResult DeserializeDataToResult(string query, string responseContent)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var searchInfo = JsonSerializer.Deserialize<GoogleResponse>(responseContent, options);
                return new SearchResult()
                {
                    Query = query,
                    SearchEngine = SearchProviderName,
                    Total = long.Parse(searchInfo.SearchInformation.TotalResults)
                };
            }
            catch
            {
                throw new APIJsonParsingException($"There was a problem parsing the Json data for {SearchProviderName}");
            }
        }
    }
}
