using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tranzact.SearchFight.Models.Interfaces;
using Tranzact.SearchFight.Models.SearchModels;

namespace Tranzact.SearchFight.Models.Services
{
    public class SearchServiceArenaService
    {
        private readonly List<IWebClient> requestClients;
        private readonly IDisplayData displayService;

        public SearchServiceArenaService(List<IWebClient> requestClients, IDisplayData displayService)
        {
            this.requestClients = requestClients;
            this.displayService = displayService;
        }

        public List<FightArenaRoundOutput> Matches(string[] args)
        {
            var competition = SearchBattles(args);
            var results = SearchResultEvaluation(competition);
            displayService.ShowSearchScoreboard(results);
            return results;
        }

        private List<SearchResult> SearchBattles(string[] args)
        {
            var taskList = new List<Task<SearchResult>>();
            foreach (var word in args)
            {
                var list = new List<SearchResult>();
                foreach (var client in requestClients)
                {
                    taskList.Add(client.GetSearchTotal(word));
                }
            }
            Task.WaitAll(taskList.ToArray());
            List<SearchResult> competitionData = new();
            foreach (var z in taskList)
            {
                competitionData.Add(z.Result);
            }
            return competitionData;
        }

        private static List<FightArenaRoundOutput> SearchResultEvaluation(List<SearchResult> results)
        {
            var arenaResults = new List<FightArenaRoundOutput>();
            foreach (var row in results.GroupBy(g => g.Query).ToList())
            {
                var result = new FightArenaRoundOutput()
                {
                    Word = row.Key,
                    Performance = new List<SearchResult>()
                };
                foreach (var column in row)
                {
                    result.Performance.Add(column);
                }
                result.Winner = result.Performance.Aggregate((current, next) => current.Total > next.Total ? current : next).SearchEngine;
                arenaResults.Add(result);
            }
            return arenaResults;
        }
    }
}
