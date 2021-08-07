using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tranzact.SearchFight.Clients;
using Tranzact.SearchFight.Models;
using Tranzact.SearchFight.Models.Interfaces;
using Tranzact.SearchFight.Models.Services;
using Tranzact.SearchFight.Services;
using Tranzact.SearchFight.Test.Mock;

namespace Tranzact.SearchFight.Test.Services
{
    [TestClass]
    public class SearchServiceArenaServiceTest
    {
        [TestMethod]
        public void Test_Matches()
        {
            List<IWebClient> mockRequestClients = new List<IWebClient>()
            {
                new GoogleClient(new Configuration.GoogleSearchEngineConfig(){
                    CustomEngine = "something",
                    Key = "something",
                    Provider = "Google",
                    URI = "something"
                    },new MockHttpClient()),
                new BingClient(new Configuration.BingSearchEngineConfig(){
                    Key = "something",
                    Provider = "Bing",
                    URI = "something"
                },new MockHttpClient())
            };

            IDisplayData displayService = new DisplayService();
            
            string[] inputWords = { ".net" };
            List<FightArenaRoundOutput> fightDataOutput = new List<FightArenaRoundOutput>()
            {
                new FightArenaRoundOutput()
                {
                    Word = ".net",
                    Performance = new List<Models.SearchModels.SearchResult>()
                    {
                        new Models.SearchModels.SearchResult()
                        {
                            Query = ".net",
                            SearchEngine = "Google",
                            Total = 125
                        },
                        new Models.SearchModels.SearchResult()
                        {
                            Query = ".net",
                            SearchEngine = "Bing",
                            Total = 120
                        }
                    },
                    Winner = "Google"
                }
            };

            SearchServiceArenaService service = new SearchServiceArenaService(mockRequestClients,displayService);
            var result = service.Matches(inputWords);


            Assert.AreEqual(fightDataOutput[0].Winner, result[0].Winner);
            Assert.AreEqual(fightDataOutput[0].Performance.Count(), result[0].Performance.Count());
            Assert.AreEqual(fightDataOutput[0].Word, result[0].Word);
        }
    }
}
