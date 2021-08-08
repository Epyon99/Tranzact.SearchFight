using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tranzact.SearchFight.Clients;
using Tranzact.SearchFight.Common.Exceptions;
using Tranzact.SearchFight.Configuration;
using Tranzact.SearchFight.Models.SearchModels;
using Tranzact.SearchFight.Test.Mock;

namespace Tranzact.SearchFight.Test.Clients
{
    [TestClass]
    public class BingClientTest
    {
        private readonly BingSearchEngineConfig config = new()
        {
            Key = RandomText,
            Provider = SearchEngine,
            URI = RandomText
        };

        private static readonly string SearchEngine = "Bing";
        private static readonly long Total = 120;
        private static readonly string RandomText = "Something";
        readonly SearchResult expectedSearchResult = new()
        {
            Query = RandomText,
            SearchEngine = SearchEngine,
            Total = Total
        };

        [TestMethod]
        public void Test_GetSearchTotal()
        {

            var httpclient = new MockHttpClient();
            BingClient client = new(config, httpclient);
            
            var result = client.GetSearchTotal(RandomText);
            result.Wait();

            Assert.AreEqual(expectedSearchResult.Query, result.Result.Query);
            Assert.AreEqual(expectedSearchResult.SearchEngine, result.Result.SearchEngine);
            Assert.AreEqual(expectedSearchResult.Total, result.Result.Total);
        }

        [TestMethod]
        public void Test_DeserializeDataToResult()
        {
            var httpclient = new MockHttpClient();
            BingClient client = new(config, httpclient);

            var message = httpclient.SendAsync(new System.Net.Http.HttpRequestMessage());
            message.Wait();
            var jsonResponse = message.Result.Content.ReadAsStringAsync();
            jsonResponse.Wait();
            var result = client.DeserializeDataToResult(RandomText, jsonResponse.Result);

            Assert.AreEqual(expectedSearchResult.Query, result.Query);
            Assert.AreEqual(expectedSearchResult.SearchEngine, result.SearchEngine);
            Assert.AreEqual(expectedSearchResult.Total, result.Total);
        }

        [TestMethod]
        [ExpectedException(typeof(APIJsonParsingException))]
        public void Test_DeserializeDataToResult_BadJson()
        {
            var httpclient = new MockHttpClient();

            BingClient client = new(config, httpclient);

            client.DeserializeDataToResult(RandomText, string.Empty);

        }

        [TestMethod]
        [ExpectedException(typeof(APIJsonParsingException))]
        public async Task Test_GetSearchResult_BadJson()
        {
            var httpclient = new MockHttpBingClient();

            BingClient client = new(config, httpclient);

            await client.GetSearchTotal(RandomText);
        }

        [TestMethod]
        [ExpectedException(typeof(NoConnectivityException))]
        public async Task Test_DeserializeDataToResult_NoClient()
        {
            BingClient client = new(config, null);
            await client.GetSearchTotal(RandomText);
        }
    }
}
