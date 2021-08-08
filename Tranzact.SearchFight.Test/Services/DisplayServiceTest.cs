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
    public class DisplayServiceTest
    {
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
                            SearchEngine = "Bing",
                            Total = 120
                        },
                        new SearchResult()
                        {
                            Query = ".net",
                            SearchEngine = "Google",
                            Total = 125
                        }
                    },
                    Winner = "Google"
                }
            };

        [TestMethod]
        public void Test_ShowSearchScoreboard()
        {
            string expectedOutputRegex = @".net Bing:\d+ Google:\d+ 
Google winner: .net
Total Winner: Google";
            DisplayService service = new();
            Regex rx = new(expectedOutputRegex, RegexOptions.IgnoreCase);
            var result = false;

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                service.ShowSearchScoreboard(fightDataOutput);
                var text = sw.ToString().Trim();
                result = rx.IsMatch(text);
            }

            Assert.AreEqual(true, result);
        }

    }
}
