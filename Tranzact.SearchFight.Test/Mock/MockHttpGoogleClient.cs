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
    public class MockHttpGoogleClient : IHttpClient
    {
        private GoogleResponse response = new GoogleResponse()
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
        public Task<HttpResponseMessage> GetAsync(string url)
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

        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
