using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tranzact.SearchFight.Models.Interfaces;

namespace Tranzact.SearchFight.Clients
{
    public class BaseHttpClient : IHttpClient
    {
        private readonly HttpClient client;

        public BaseHttpClient(string baseUri)
        {
            client = new HttpClient()
            {
                BaseAddress = new Uri(baseUri)
            };
        }
        public Task<HttpResponseMessage> GetAsync(string url)
        {
            return client.GetAsync(url);
        }

        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage message)
        {
            return client.SendAsync(message);
        }
    }
}
