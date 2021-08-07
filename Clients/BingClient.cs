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
    public class BingClient : IWebClient
    {
        private readonly BingSearchEngineConfig searchProvider;
        public const string SearchProviderName = "Bing";

        public IHttpClient Client { get; set; }

        public BingClient(BingSearchEngineConfig searchProvider, IHttpClient client)
        {
            this.searchProvider = searchProvider;
            this.Client = client;
        }

        public SearchResult DeserializeDataToResult(string query, string responseContent)
        {
            try
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
            catch
            {
                throw new APIJsonParsingException($"There was a problem parsing the Json data for {SearchProviderName}");
            }
        }

        public async Task<SearchResult> GetSearchTotal(string query)
        {
            try
            {
                HttpRequestMessage message = new(HttpMethod.Get, $"?q={query}");
                message.Headers.Add("Ocp-Apim-Subscription-Key", searchProvider.Key);
                var response = await Client.SendAsync(message);
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
    }
}
