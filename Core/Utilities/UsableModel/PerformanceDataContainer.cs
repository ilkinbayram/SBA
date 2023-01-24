using Core.Utilities.UsableModel.BaseModels;

namespace Core.Utilities.UsableModel
{
    public class PerformanceDataContainer : BaseComparerContainerModel
    {
        public PerformanceDataContainer()
        {
            Serial = string.Empty;
            HomeTeam = string.Empty;
            AwayTeam = string.Empty;
            UnchangableHomeTeam =string.Empty;
            UnchangableAwayTeam =string.Empty;

            HT_Goals_HomeTeam = -1;
            HT_Goals_AwayTeam = -1;
            FT_Goals_HomeTeam = -1;
            FT_Goals_AwayTeam = -1;
        }
    }
}
