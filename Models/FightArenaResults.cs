using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranzact.SearchFight.Models.SearchModels;

namespace Tranzact.SearchFight.Models
{
    public class FightArenaRound
    {
        public string Word { get; set; }
        public List<CountResult> Performance { get; set; }
        public string ManualWinner { get; set; }
        public string Winner
        {
            get
            {
                return Performance.Aggregate((current, next) => current.Total > next.Total ? current : next).SearchEngine;
            }
        }

        public override string ToString()
        {
            var output = string.Empty;
            foreach(var data in Performance)
            {
                output += $"{data.SearchEngine}:{data.Total} ";
            }
            return $"{Word} {output}"; 
        }
}
}
