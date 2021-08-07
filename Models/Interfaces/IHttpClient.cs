using System.Net.Http;
using System.Threading.Tasks;

namespace Tranzact.SearchFight.Models.Interfaces
{
    public interface IHttpClient
    {
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage message);
        Task<HttpResponseMessage> GetAsync(string url);
    }
}
