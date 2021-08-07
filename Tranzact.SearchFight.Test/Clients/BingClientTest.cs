using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Text.RegularExpressions;
using Tranzact.SearchFight.Clients;
using Tranzact.SearchFight.Configuration;
using Tranzact.SearchFight.Models.SearchModels;
using Tranzact.SearchFight.Test.Mock;

namespace Tranzact.SearchFight.Test.Clients
{
    [TestClass]
    public class BingClientTest
    {
        [TestMethod]
        public void Test_GetSearchTotal()
        {
            var config = new Configuration.BingSearchEngineConfig()
            {
                Key = "something",
                Provider = "Bing",
                URI = "something"
            };
            var httpclient = new MockHttpClient();

            SearchResult expectedOutput = new SearchResult()
            {
                Query = "something",
                SearchEngine = "Bing",
                Total = 120
            };

            BingClient client = new(config,httpclient);
            
            
            var result = client.GetSearchTotal("something");
            result.Wait();

            Assert.AreEqual(expectedOutput.Query, result.Result.Query);
            Assert.AreEqual(expectedOutput.SearchEngine, result.Result.SearchEngine);
            Assert.AreEqual(expectedOutput.Total, result.Result.Total);
        }
    }
}
