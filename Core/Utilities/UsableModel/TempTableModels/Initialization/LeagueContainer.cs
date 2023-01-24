using System.Collections.Generic;

namespace Core.Utilities.UsableModel.TempTableModels.Initialization
{
    public class LeagueContainer
    {
        public LeagueContainer()
        {
            if (LeagueHolders == null)
                LeagueHolders = new List<LeagueHolder>();
        }
        public List<LeagueHolder> LeagueHolders { get; set; }
    }
}
