using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
