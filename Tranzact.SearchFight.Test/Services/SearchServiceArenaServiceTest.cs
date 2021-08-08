using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tranzact.SearchFight.Clients;
using Tranzact.SearchFight.Common.Exceptions;
using Tranzact.SearchFight.Models;
using Tranzact.SearchFight.Models.Interfaces;
using Tranzact.SearchFight.Models.SearchModels;
using Tranzact.SearchFight.Models.Services;
using Tranzact.SearchFight.Services;
using Tranzact.SearchFight.Test.Mock;

namespace Tranzact.SearchFight.Test.Services
{
    [TestClass]
    public class SearchServiceArenaServiceTest
    {
        readonly string[] inputWords = { ".net" };
        readonly List<IWebClient> mockRequestClients = new()
        {
            new GoogleClient(new Configuration.GoogleSearchEngineConfig()
            {
                CustomEngine = "something",
                Key = "something",
                Provider = "Google",
                URI = "something"
            }, new MockHttpClient()),
            new BingClient(new Configuration.BingSearchEngineConfig()
            {
                Key = "something",
                Provider = "Bing",
                URI = "something"
            }, new MockHttpClient())
        };
        readonly List<FightArenaRoundOutput> fightDataOutput = new()
        {
            new FightArenaRoundOutput()
            {
                Word = ".net",
                Performance = new List<SearchResult>()
                    {
                        new SearchResult()
                        {
                            Query = ".net",
                            SearchEngine = "Google",
                            Total = 125
                        },
                        new SearchResult()
                        {
                            Query = ".net",
                            SearchEngine = "Bing",
                            Total = 120
                        }
                    },
                Winner = "Google"
            }
        };

        [TestMethod]
        public void Test_Matches()
        {
            IDisplayData displayService = new DisplayService();
            SearchServiceArenaService service = new(mockRequestClients, displayService);

            var result = service.Matches(inputWords);

            Assert.AreEqual(fightDataOutput[0].Winner, result[0].Winner);
            Assert.AreEqual(fightDataOutput[0].Performance.Count, result[0].Performance.Count);
            Assert.AreEqual(fightDataOutput[0].Word, result[0].Word);
        }


        [TestMethod]
        [ExpectedException(typeof(UnsupportedEngine))]
        public void Test_Matches_NoEngines()
        {
            List<IWebClient> mockRequestClients = new() { };
            IDisplayData displayService = new DisplayService();
            SearchServiceArenaService service = new(mockRequestClients, displayService);

            service.Matches(inputWords);
        }
    }
}
