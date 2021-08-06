using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
