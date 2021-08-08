using System.Text.Json.Serialization;

namespace Tranzact.SearchFight.Models.SearchModels
{
    public class BingWebPages
    {
        [JsonInclude]
        public string WebSearchUrl { get; set; }
        [JsonInclude]
        public long TotalEstimatedMatches { get; set; }
    }
}
