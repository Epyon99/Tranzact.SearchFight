using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tranzact.SearchFight.Models.SearchModels
{
    public class CountResult
    {
        public long Total { get; set; }
        public string SearchEngine { get; set; }
        public string Query { get; set; }
    }
}
