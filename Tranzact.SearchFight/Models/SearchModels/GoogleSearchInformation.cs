using System.Text.Json.Serialization;

namespace Tranzact.SearchFight.Models.SearchModels
{
    public class GoogleSearchInformation
    {
        [JsonInclude]
        public float SearchTime { get; set; }

        [JsonInclude]
        public string FormattedSearchTime { get; set; }

        [JsonInclude]
        public string TotalResults { get; set; }

        [JsonInclude]
        public string FormattedTotalResults { get; set; }
    }
}
