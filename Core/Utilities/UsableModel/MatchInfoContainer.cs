using Core.Utilities.UsableModel.BaseModels;

namespace Core.Utilities.UsableModel
{
    public class MatchInfoContainer : BaseMatchInfo
    {
        public MatchInfoContainer()
        {
            HT_Goals_Count = 0;
            FT_Goals_Count = 0;
            SH_Goals_Count = 0;

            Home_HT_Goals_Count = 0;
            Home_FT_Goals_Count = 0;
            Home_SH_Goals_Count = 0;
            Away_SH_Goals_Count = 0;
            Away_HT_Goals_Count = 0;
            Away_FT_Goals_Count = 0;


            Home_15_Over = (decimal)-1.00;
            Home_15_Under = (decimal)-1.00;
            Away_15_Over = (decimal)-1.00;
            Away_15_Under = (decimal)-1.00;


            Serial = string.Empty;
            Home = string.Empty;
            Away = string.Empty;
            DateMatch = string.Empty;
            League = string.Empty;
            LeagueId = string.Empty;
            Country = string.Empty;
            HT_Result = string.Empty;
            FT_Result = string.Empty;
            HT_W1 = (decimal)-1.00;
            HT_X = (decimal)-1.00;
            HT_W2 = (decimal)-1.00;
            FT_W1 = (decimal)-1.00;
            FT_X = (decimal)-1.00;
            FT_W2 = (decimal)-1.00;
            Goals01 = (decimal)-1.00;
            Goals23 = (decimal)-1.00;
            Goals45 = (decimal)-1.00;
            Goals6 = (decimal)-1.00;
            GG = (decimal)-1.00;
            NG = (decimal)-1.00;
            FT_15_Under = (decimal)-1.00;
            FT_15_Over = (decimal)-1.00;
            FT_25_Under = (decimal)-1.00;
            FT_25_Over = (decimal)-1.00;
            FT_35_Under = (decimal)-1.00;
            FT_35_Over = (decimal)-1.00;
            HT_15_Under = (decimal)-1.00;
            HT_15_Over = (decimal)-1.00;
        }
    }
}
