using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tranzact.SearchFight.Common.Exceptions;
using Tranzact.SearchFight.Models.Interfaces;
using Tranzact.SearchFight.Models.SearchModels;

namespace Tranzact.SearchFight.Test.Mock
{
    public class MockHttpGoogleClient : IHttpClient
    {
        public Task<HttpResponseMessage> GetAsync(string url)
        {
            throw new APIJsonParsingException();
        }

        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
