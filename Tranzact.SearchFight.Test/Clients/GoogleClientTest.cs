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
    public class GoogleClientTest
    {

        private static readonly string SearchEngine = "Google";
        private static readonly long Total = 125;
        private static readonly string RandomText = "Something";
        private readonly GoogleSearchEngineConfig config = new()
        {
            Key = RandomText,
            Provider = SearchEngine,
            URI = RandomText
        };
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
            GoogleClient client = new(config,httpclient);         
            
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
            GoogleClient client = new(config, httpclient);

            var message = httpclient.GetAsync(RandomText);
            message.Wait();
            var jsonResponse = message.Result.Content.ReadAsStringAsync();
            jsonResponse.Wait();
            var result = client.DeserializeDataToResult(RandomText,jsonResponse.Result);

            Assert.AreEqual(expectedSearchResult.Query, result.Query);
            Assert.AreEqual(expectedSearchResult.SearchEngine, result.SearchEngine);
            Assert.AreEqual(expectedSearchResult.Total, result.Total);
        }

        [TestMethod]
        [ExpectedException(typeof(APIJsonParsingException))]
        public void Test_DeserializeDataToResult_BadJson()
        {
            var httpclient = new MockHttpClient();

            GoogleClient client = new(config, httpclient);

            client.DeserializeDataToResult(RandomText,string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(APIJsonParsingException))]
        public async Task Test_GetSearchResult_BadJson()
        {
            var httpclient = new MockHttpGoogleClient();

            GoogleClient client = new(config, httpclient);

            await client.GetSearchTotal(RandomText);
        }

        [TestMethod]
        [ExpectedException(typeof(NoConnectivityException))]
        public async Task Test_DeserializeDataToResult_NoClient()
        {            
            GoogleClient client = new(config, null);
            await client.GetSearchTotal(RandomText);
        }
    }
}
