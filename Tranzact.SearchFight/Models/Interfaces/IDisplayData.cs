using System.Collections.Generic;

namespace Tranzact.SearchFight.Models.Interfaces
{
    public interface IDisplayData
    {
        void ShowSearchScoreboard(List<FightArenaRoundOutput> arenaResults);
    }
}
