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
    public class MockHttpClient : IHttpClient
    {
        private GoogleResponse googleResponse = new GoogleResponse()
        {
            Kind = "random",
            SearchInformation = new GoogleSearchInformation()
            {
                FormattedSearchTime = "10:00:00",
                FormattedTotalResults = "125",
                SearchTime = 0,
                TotalResults = "125"
            }
        };
        private BingResponse bingResponse = new BingResponse()
        {
            WebPages = new BingWebPages()
            {
                TotalEstimatedMatches = 120,
                WebSearchUrl = "random"
            }
        };
        public Task<HttpResponseMessage> GetAsync(string url)
        {
            return Task.Run(() =>
            {
                StringContent stringContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(googleResponse));
                HttpResponseMessage message = new HttpResponseMessage()
                {
                    Content = stringContent
                };
                return message;
            });
        }

        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage message)
        {
            return Task.Run(() =>
            {
                StringContent stringContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(bingResponse));
                HttpResponseMessage message = new HttpResponseMessage()
                {
                    Content = stringContent
                };
                return message;
            });
        }
    }
}
