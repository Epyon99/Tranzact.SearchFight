using System.Text.Json.Serialization;

namespace Tranzact.SearchFight.Configuration
{
    public class Configuration
    {
        [JsonInclude]
        public string[] EnabledSearchProviders { get; set; }
        [JsonInclude]
        public GoogleSearchEngineConfig GoogleSearchEngine { get; set; }
        [JsonInclude]
        public BingSearchEngineConfig BingSearchEngine { get; set; }
    }
}
