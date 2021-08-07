using System.Collections.Generic;
using Tranzact.SearchFight.Models.SearchModels;

namespace Tranzact.SearchFight.Models
{
    public class FightArenaRoundOutput
    {
        public string Word { get; set; }
        public List<SearchResult> Performance { get; set; }
        public string Winner { get; set; }

        public override string ToString()
        {
            var output = string.Empty;
            foreach (var data in Performance)
            {
                output += $"{data.SearchEngine}:{data.Total} ";
            }
            return $"{Word} {output}";
        }
    }
}
