using System.Text.Json.Serialization;

namespace Tranzact.SearchFight.Models.SearchModels
{
    public class BingResponse
    {
        [JsonInclude]
        public BingWebPages WebPages { get; set; }
    }
}
