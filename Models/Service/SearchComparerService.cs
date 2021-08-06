using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranzact.SearchFight.Configuration;
using Tranzact.SearchFight.Models.Clients;
using Tranzact.SearchFight.Models.Interfaces;
using Tranzact.SearchFight.Models.SearchModels;

namespace Tranzact.SearchFight.Models.Service
{
    public class SearchComparerService
    {
        private readonly ConfigurationManager configurationManager;
        private List<IRequestClient> requestClients;
        public SearchComparerService(ConfigurationManager configurationManager)
        {
            this.configurationManager = configurationManager;
            Setup();
        }

        private void Setup()
        {
            requestClients = new List<IRequestClient>();
            var config = configurationManager.GetConfiguration();
            if (config.SearchProviders.Any(g => g.Provider == BingClient.SearchProviderName))
            {
                requestClients.Add(new BingClient(config.SearchProviders));
            }
            if (config.SearchProviders.Any(g => g.Provider == GoogleClient.SearchProviderName))
            {
                requestClients.Add(new GoogleClient(config.SearchProviders));
            }
        }

        public List<CountResult> Competition(string[] args)
        {
            var taskList = new List<Task<CountResult>>();
            foreach (var word in args)
            {
                var list = new List<CountResult>();
                foreach (var client in requestClients)
                {
                    taskList.Add(client.GetResultsTotal(word));
                }
            }
            Task.WaitAll(taskList.ToArray());
            List<CountResult> competitionData = new List<CountResult>();
            foreach (var z in taskList)
            {
                competitionData.Add(z.Result);
            }
            return competitionData;
        }

        public void Matches(string[] args)
        {
            var competition = Competition(args);
            var results = Judges(competition);
            DisplayScoreboard(results);
        }

        public List<FightArenaRound> Judges(List<CountResult> results)
        {
            var arenaResults = new List<FightArenaRound>();
            foreach (var row in results.GroupBy(g => g.Query).ToList())
            {
                var result = new FightArenaRound()
                {
                    Word = row.Key,
                    Performance = new List<CountResult>()
                };
                foreach (var column in row)
                {
                    result.Performance.Add(column);
                }
                result.ManualWinner = result.Performance.Aggregate((current, next) => current.Total > next.Total ? current : next).SearchEngine;
                arenaResults.Add(result);
            }
            return arenaResults;
        }

        public void DisplayScoreboard(List<FightArenaRound> arenaResults)
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
            var champion = groupWinners.OrderByDescending(g => g.Count()).First();
            Console.WriteLine($"Total Winner: {champion.Key}");
        }
    }
}
