using System.Text.Json.Serialization;

namespace Tranzact.SearchFight.Configuration
{
    public class Configuration
    {
        [JsonInclude]
        public SearchProviders[] SearchProviders { get; set; }
    }
}
