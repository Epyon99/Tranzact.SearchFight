using System.Text.Json.Serialization;

namespace Tranzact.SearchFight.Configuration
{
    public abstract class SearchProviders
    {
        [JsonInclude]
        public string Provider { get; set; }
        [JsonInclude]
        public string Key { get; set; }
        [JsonInclude]
        public string URI { get; set; }
    }
}
