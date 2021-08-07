using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tranzact.SearchFight.Models.Interfaces;
using Tranzact.SearchFight.Models.SearchModels;

namespace Tranzact.SearchFight.Test.Mock
{
    public class MockHttpBingClient : IHttpClient
    {
        private BingResponse response = new BingResponse()
        {
            WebPages = new BingWebPages()
            {
                TotalEstimatedMatches = 120,
                WebSearchUrl = "random"
            }
        };
        public Task<HttpResponseMessage> GetAsync(string url)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage message)
        {
            return Task.Run(() =>
            {
                StringContent stringContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(response));
                HttpResponseMessage message = new HttpResponseMessage()
                {
                    Content = stringContent
                };
                return message;
            });
        }
    }
}
