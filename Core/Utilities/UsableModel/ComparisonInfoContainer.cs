using Core.Resources.Enums;
using Core.Utilities.UsableModel.BaseModels;

namespace Core.Utilities.UsableModel
{
    public class ComparisonInfoContainer : BaseComparerContainerModel
    {
        public ComparisonInfoContainer(string serial,
                                       string unchangableHomeTeam,
                                       string unchangableAwayTeam,
                                       string homeTeam,
                                       string awayTeam,
                                       string countryName,
                                       int hT_Goals_HomeTeam,
                                       int hT_Goals_AwayTeam,
                                       string leagueName,
                                       int fT_Goals_HomeTeam,
                                       int fT_Goals_AwayTeam,
                                       int homeCornersCount,
                                       int awayCornersCount,
                                       bool hasCorner) : base(serial,
                                                                    unchangableHomeTeam,
                                                                    unchangableAwayTeam,
                                                                    homeTeam,
                                                                    awayTeam,
                                                                    countryName,
                                                                    hT_Goals_HomeTeam,
                                                                    hT_Goals_AwayTeam,
                                                                    leagueName,
                                                                    fT_Goals_HomeTeam,
                                                                    fT_Goals_AwayTeam,
                                                                    homeCornersCount,
                                                                    awayCornersCount,
                                                                    hasCorner)
        {

        }

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
