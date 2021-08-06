using System.Text.Json.Serialization;

namespace Tranzact.SearchFight.Configuration
{
    public class SearchProviders
    {
        [JsonInclude]
        public string Provider { get; set; }
        [JsonInclude]
        public string APIKey { get; set; }
        [JsonInclude]
        public string ApplicationId { get; set; }
        [JsonInclude]
        public string Other { get; set; }
        [JsonInclude]
        public string BaseUri { get; set; }
    }
}
