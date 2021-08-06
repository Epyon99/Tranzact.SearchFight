using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tranzact.SearchFight.Configuration;
using Tranzact.SearchFight.Models.SearchModels;

namespace Tranzact.SearchFight.Models.Interfaces
{
    public interface IRequestClient
    {
        HttpClient client { get; set; }
        Task<CountResult> GetResultsTotal(string query);
        void Setup(SearchProviders searchProvider);
    }
}
