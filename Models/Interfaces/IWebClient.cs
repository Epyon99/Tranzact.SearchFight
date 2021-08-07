using System.Threading.Tasks;
using Tranzact.SearchFight.Configuration;
using Tranzact.SearchFight.Models.SearchModels;

namespace Tranzact.SearchFight.Models.Interfaces
{
    public interface IWebClient
    {
        IHttpClient Client { get; set; }
        Task<SearchResult> GetSearchTotal(string query);
        SearchResult DeserializeDataToResult(string query, string responseContent);
    }
}
