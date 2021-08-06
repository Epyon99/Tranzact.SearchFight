using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranzact.SearchFight.Configuration;
using Tranzact.SearchFight.Models.Clients;
using Tranzact.SearchFight.Models.Interfaces;
using Tranzact.SearchFight.Models.SearchModels;

namespace Tranzact.SearchFight.Models.Services
{
    public class SearchServiceArenaService
    {
        private readonly ConfigurationManager configurationManager;
        private List<IWebClient> requestClients;
        public SearchServiceArenaService(ConfigurationManager configurationManager)
        {
            this.configurationManager = configurationManager;
            Setup();
        }
        public void Matches(string[] args)
        {
            var competition = SearchBattles(args);
            var results = SearchResultEvaluation(competition);
            SearchScoreboard(results);
        }

        private void Setup()
        {
            requestClients = new List<IWebClient>();
            var config = configurationManager.GetConfiguration();
            if (config.EnabledSearchProviders.Any(g => g == BingClient.SearchProviderName))
            {
                requestClients.Add(new BingClient(config.BingSearchEngine));
            }
            if (config.EnabledSearchProviders.Any(g => g == GoogleClient.SearchProviderName))
            {
                requestClients.Add(new GoogleClient(config.GoogleSearchEngine));
            }
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
            List<SearchResult> competitionData = new List<SearchResult>();
            foreach (var z in taskList)
            {
                competitionData.Add(z.Result);
            }
            return competitionData;
        }

        private List<FightArenaRoundOutput> SearchResultEvaluation(List<SearchResult> results)
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

        private void SearchScoreboard(List<FightArenaRoundOutput> arenaResults)
        {
            // Display Scoreboard
            foreach (var round in arenaResults)
            {
                Console.WriteLine(round);
            }

            // Display Each group winner by contestant
            var groupWinners = arenaResults.GroupBy(g => g.Winner);
            foreach (var participant in groupWinners)
            {
                var text = $"{participant.Key} winner: ";
                foreach (var victories in participant)
                {
                    text += $"{victories.Word},";
                }
                Console.WriteLine(text.Remove(text.Length - 1));
            }

            // Display the contestant with the most victories
            var champion = groupWinners.OrderByDescending(g => g.Count()).FirstOrDefault();
            Console.WriteLine($"Total Winner: {champion?.Key}");
        }
    }
}
