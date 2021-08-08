using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tranzact.SearchFight.Models;
using Tranzact.SearchFight.Models.SearchModels;

namespace Tranzact.SearchFight.Test.Models
{
    [TestClass]
    public class ModelTest
    {
        private static readonly string SearchEngine = "Bing";
        private static readonly long Total = 120;
        private static readonly string RandomText = "Something";
        readonly SearchResult expectedSearchResult = new()
        {
            Query = RandomText,
            SearchEngine = SearchEngine,
            Total = Total
        };

        List<FightArenaRoundOutput> fightDataOutput = new()
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
        public void Test_FightArenaRoundOutput_ToString()
        {
            Regex regex = new($"Search Engine: \\w | Query \\w | Total \\d+");
            var result = regex.IsMatch(expectedSearchResult.ToString());
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void Test_SearchResult_ToString()
        {
            Regex regex = new($"Search Engine: \\w | Query \\w | Total \\d+");
            var result = regex.IsMatch(expectedSearchResult.ToString());
            Assert.AreEqual(true, result);
        }
    }
}
