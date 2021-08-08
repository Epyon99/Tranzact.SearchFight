using System.Text.Json.Serialization;

namespace Tranzact.SearchFight.Models.SearchModels
{
    public class GoogleResponse
    {

        [JsonInclude]
        public string Kind { get; set; }

        [JsonInclude]
        public GoogleSearchInformation SearchInformation { get; set; }
    }
}
