using Core.Resources.Enums;
using Core.Utilities.UsableModel.BaseModels;

namespace Core.Utilities.UsableModel
{
    public class ComparisonInfoContainer : BaseComparerContainerModel
    {
        public ComparisonInfoContainer()
        {
            Serial = string.Empty;
            HomeTeam = string.Empty;
            AwayTeam = string.Empty;
            UnchangableAwayTeam = string.Empty;
            UnchangableHomeTeam = string.Empty;
            HT_Goals_HomeTeam = -1;
            HT_Goals_AwayTeam = -1;
            FT_Goals_HomeTeam = -1;
            FT_Goals_AwayTeam = -1;
        }
    }
}
