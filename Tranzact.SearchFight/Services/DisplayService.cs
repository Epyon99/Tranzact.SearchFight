using System;
using System.Collections.Generic;
using System.Linq;
using Tranzact.SearchFight.Models;
using Tranzact.SearchFight.Models.Interfaces;

namespace Tranzact.SearchFight.Services
{
    public class DisplayService : IDisplayData
    {
        public void ShowSearchScoreboard(List<FightArenaRoundOutput> arenaResults)
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
            if (champion != null)
            {
                Console.WriteLine($"Total Winner: {champion.Key}");
            }
        }
    }
}
