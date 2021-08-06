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
    public interface IWebClient
    {
        HttpClient Client { get; set; }
        Task<SearchResult> GetSearchTotal(string query);
        void Setup(SearchProviders searchProvider);
    }
}
