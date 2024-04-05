using Core.Entities.Concrete;
using Core.Resources.Constants;

namespace Core.Utilities.Helpers
{
    public static class ForecastHandler
    {
        public static bool CheckForecast(FilterResult filterResult, string key)
        {
            bool result = false;

            if (key.ToLower() == ForecastKeys.AwayWinAnyHalf.ToLower())
            {
                result = filterResult.Away_Win_Any_Half;
            }
            else if (key.ToLower() == ForecastKeys.HomeWinAnyHalf.ToLower())
            {
                result = filterResult.Home_Win_Any_Half;
            }


            else if (key.ToLower() == ForecastKeys.HT_05_Over.ToLower())
            {
                result = filterResult.HT_0_5_Over;
            }
            else if (key.ToLower() == ForecastKeys.HT_15_Over.ToLower())
            {
                result = filterResult.HT_1_5_Over;
            }
            else if (key.ToLower() == ForecastKeys.HT_05_Under.ToLower())
            {
                result = !filterResult.HT_0_5_Over;
            }
            else if (key.ToLower() == ForecastKeys.HT_15_Under.ToLower())
            {
                result = !filterResult.HT_1_5_Over;
            }
            else if (key.ToLower() == ForecastKeys.HT_Home_Goal.ToLower())
            {
                result = filterResult.Home_HT_0_5_Over;
            }
            else if (key.ToLower() == ForecastKeys.HT_Away_Goal.ToLower())
            {
                result = filterResult.Away_HT_0_5_Over;
            }
            else if (key.ToLower() == ForecastKeys.HT_Win_1.ToLower())
            {
                result = filterResult.Is_HT_Win1;
            }
            else if (key.ToLower() == ForecastKeys.HT_X.ToLower())
            {
                result = filterResult.Is_HT_X;
            }
            else if (key.ToLower() == ForecastKeys.HT_Win_2.ToLower())
            {
                result = filterResult.Is_HT_Win2;
            }


            else if (key.ToLower() == ForecastKeys.SH_05_Over.ToLower())
            {
                result = filterResult.SH_0_5_Over;
            }
            else if (key.ToLower() == ForecastKeys.SH_15_Over.ToLower())
            {
                result = filterResult.SH_1_5_Over;
            }
            else if (key.ToLower() == ForecastKeys.SH_05_Under.ToLower())
            {
                result = !filterResult.SH_0_5_Over;
            }
            else if (key.ToLower() == ForecastKeys.SH_15_Under.ToLower())
            {
                result = !filterResult.SH_1_5_Over;
            }
            else if (key.ToLower() == ForecastKeys.SH_Home_Goal.ToLower())
            {
                result = filterResult.Home_SH_0_5_Over;
            }
            else if (key.ToLower() == ForecastKeys.SH_Away_Goal.ToLower())
            {
                result = filterResult.Away_SH_0_5_Over;
            }
            else if (key.ToLower() == ForecastKeys.SH_Win_1.ToLower())
            {
                result = filterResult.Is_SH_Win1;
            }
            else if (key.ToLower() == ForecastKeys.SH_X.ToLower())
            {
                result = filterResult.Is_SH_X;
            }
            else if (key.ToLower() == ForecastKeys.SH_Win_2.ToLower())
            {
                result = filterResult.Is_SH_Win2;
            }


            else if (key.ToLower() == ForecastKeys.FT_15_Over.ToLower())
            {
                result = filterResult.FT_1_5_Over;
            }
            else if (key.ToLower() == ForecastKeys.FT_15_Under.ToLower())
            {
                result = !filterResult.FT_1_5_Over;
            }
            else if (key.ToLower() == ForecastKeys.FT_25_Over.ToLower())
            {
                result = filterResult.FT_2_5_Over;
            }
            else if (key.ToLower() == ForecastKeys.FT_25_Under.ToLower())
            {
                result = !filterResult.FT_2_5_Over;
            }
            else if (key.ToLower() == ForecastKeys.FT_35_Over.ToLower())
            {
                result = filterResult.FT_3_5_Over;
            }
            else if (key.ToLower() == ForecastKeys.FT_35_Under.ToLower())
            {
                result = !filterResult.FT_3_5_Over;
            }
            else if (key.ToLower() == ForecastKeys.FT_Home_Goal.ToLower())
            {
                result = filterResult.Home_FT_0_5_Over;
            }
            else if (key.ToLower() == ForecastKeys.FT_Away_Goal.ToLower())
            {
                result = filterResult.Away_FT_0_5_Over;
            }
            else if (key.ToLower() == ForecastKeys.FT_Win_1.ToLower())
            {
                result = filterResult.Is_FT_Win1;
            }
            else if (key.ToLower() == ForecastKeys.FT_X.ToLower())
            {
                result = filterResult.Is_FT_X;
            }
            else if (key.ToLower() == ForecastKeys.FT_Win_2.ToLower())
            {
                result = filterResult.Is_FT_Win2;
            }
            else if (key.ToLower() == ForecastKeys.FT_GG.ToLower())
            {
                result = filterResult.FT_GG;
            }
            else if (key.ToLower() == ForecastKeys.FT_NG.ToLower())
            {
                result = !filterResult.FT_GG;
            }

            return result;
        }
    }
}
