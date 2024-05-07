using Core.Entities.Concrete.ExternalDbEntities;
using Core.Resources.Constants;
using System.Text;

namespace SBA.WebAPI.Utilities.Helpers
{
    public static class ForecastChecker
    {
        public static ForecastDetails Check_GG(List<StatisticInfoHolder> statInfoHolders, int minFoundCount, int minPercentage)
        {
            if (statInfoHolders == null || statInfoHolders.Count == 0)
                return new ForecastDetails();

            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "FT_GG");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.HomePercent) >= minFoundCount && Convert.ToInt32(neededStat.HomePercent * 100) >= minPercentage)
            {
                return new ForecastDetails
                {
                    IsAcceptable = true,
                    Value = Convert.ToInt32(neededStat.HomePercent * 100)
                };
            }

            return new ForecastDetails();
        }

        public static ForecastDetails Check_NG(List<StatisticInfoHolder> statInfoHolders, int minFoundCount, int minPercentage)
        {
            if (statInfoHolders == null || statInfoHolders.Count == 0)
                return new ForecastDetails();

            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "FT_GG");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.HomePercent) >= minFoundCount && Convert.ToInt32(neededStat.HomePercent * 100) <= minPercentage)
            {
                return new ForecastDetails
                {
                    IsAcceptable = true,
                    Value = Convert.ToInt32(neededStat.HomePercent * 100)
                };
            }

            return new ForecastDetails();
        }

        public static ForecastDetails Check_FT_25_Over(List<StatisticInfoHolder> statInfoHolders, int minFoundCount, int minPercentage)
        {
            if (statInfoHolders == null || statInfoHolders.Count == 0)
                return new ForecastDetails();

            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "FT_25");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.HomePercent) >= minFoundCount && Convert.ToInt32(neededStat.HomePercent * 100) >= minPercentage)
            {
                return new ForecastDetails
                {
                    IsAcceptable = true,
                    Value = Convert.ToInt32(neededStat.HomePercent * 100)
                };
            }

            return new ForecastDetails();
        }

        public static ForecastDetails Check_FT_15_Over(List<StatisticInfoHolder> statInfoHolders, int minFoundCount, int minPercentage)
        {
            if (statInfoHolders == null || statInfoHolders.Count == 0)
                return new ForecastDetails();

            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "FT_15");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.HomePercent) >= minFoundCount && Convert.ToInt32(neededStat.HomePercent * 100) >= minPercentage)
            {
                return new ForecastDetails
                {
                    IsAcceptable = true,
                    Value = Convert.ToInt32(neededStat.HomePercent * 100)
                };
            }

            return new ForecastDetails();
        }

        public static ForecastDetails Check_HT_05_Over(List<StatisticInfoHolder> statInfoHolders, int minFoundCount, int minPercentage)
        {
            if (statInfoHolders == null || statInfoHolders.Count == 0)
                return new ForecastDetails();

            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "HT_05");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.HomePercent) >= minFoundCount && Convert.ToInt32(neededStat.HomePercent * 100) >= minPercentage)
            {
                return new ForecastDetails
                {
                    IsAcceptable = true,
                    Value = Convert.ToInt32(neededStat.HomePercent * 100)
                };
            }

            return new ForecastDetails();
        }

        public static ForecastDetails Check_SH_05_Over(List<StatisticInfoHolder> statInfoHolders, int minFoundCount, int minPercentage)
        {
            if (statInfoHolders == null || statInfoHolders.Count == 0)
                return new ForecastDetails();

            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "SH_05");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.HomePercent) >= minFoundCount && Convert.ToInt32(neededStat.HomePercent * 100) >= minPercentage)
            {
                return new ForecastDetails
                {
                    IsAcceptable = true,
                    Value = Convert.ToInt32(neededStat.HomePercent * 100)
                };
            }

            return new ForecastDetails();
        }

        public static ForecastDetails Check_FT_35_Under(List<StatisticInfoHolder> statInfoHolders, int minFoundCount, int minPercentage)
        {
            if (statInfoHolders == null || statInfoHolders.Count == 0)
                return new ForecastDetails();

            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "FT_35");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.HomePercent) >= minFoundCount && Convert.ToInt32(neededStat.HomePercent * 100) <= minPercentage)
            {
                return new ForecastDetails
                {
                    IsAcceptable = true,
                    Value = Convert.ToInt32(neededStat.HomePercent * 100)
                };
            }

            return new ForecastDetails();
        }

        public static ForecastDetails Check_HT_15_Under(List<StatisticInfoHolder> statInfoHolders, int minFoundCount, int minPercentage)
        {
            if (statInfoHolders == null || statInfoHolders.Count == 0)
                return new ForecastDetails();

            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "HT_15");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.HomePercent) >= minFoundCount && Convert.ToInt32(neededStat.HomePercent * 100) <= minPercentage)
            {
                return new ForecastDetails
                {
                    IsAcceptable = true,
                    Value = Convert.ToInt32(neededStat.HomePercent * 100)
                };
            }

            return new ForecastDetails();
        }

        public static ForecastDetails Check_SH_15_Under(List<StatisticInfoHolder> statInfoHolders, int minFoundCount, int minPercentage)
        {
            if (statInfoHolders == null || statInfoHolders.Count == 0)
                return new ForecastDetails();

            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "SH_15");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.HomePercent) >= minFoundCount && Convert.ToInt32(neededStat.HomePercent * 100) <= minPercentage)
            {
                return new ForecastDetails
                {
                    IsAcceptable = true,
                    Value = Convert.ToInt32(neededStat.HomePercent * 100)
                };
            }

            return new ForecastDetails();
        }


        public static ForecastDetails Check_Home_FT_05_Over(List<StatisticInfoHolder> statInfoHolders, int minFoundCount, int minPercentage)
        {
            if (statInfoHolders == null || statInfoHolders.Count == 0)
                return new ForecastDetails();

            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "Ind_FT_05");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.HomePercent) >= minFoundCount && Convert.ToInt32(neededStat.HomePercent * 100) >= minPercentage)
            {
                return new ForecastDetails
                {
                    IsAcceptable = true,
                    Value = Convert.ToInt32(neededStat.HomePercent * 100)
                };
            }

            return new ForecastDetails();
        }

        public static ForecastDetails Check_Home_FT_15_Over(List<StatisticInfoHolder> statInfoHolders, int minFoundCount, int minPercentage)
        {
            if (statInfoHolders == null || statInfoHolders.Count == 0)
                return new ForecastDetails();

            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "Ind_FT_15");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.HomePercent) >= minFoundCount && Convert.ToInt32(neededStat.HomePercent * 100) >= minPercentage)
            {
                return new ForecastDetails
                {
                    IsAcceptable = true,
                    Value = Convert.ToInt32(neededStat.HomePercent * 100)
                };
            }

            return new ForecastDetails();
        }

        public static ForecastDetails Check_Away_FT_05_Over(List<StatisticInfoHolder> statInfoHolders, int minFoundCount, int minPercentage)
        {
            if (statInfoHolders == null || statInfoHolders.Count == 0)
                return new ForecastDetails();

            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "Ind_FT_05");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.HomePercent) >= minFoundCount && Convert.ToInt32(neededStat.AwayPercent * 100) >= minPercentage)
            {
                return new ForecastDetails
                {
                    IsAcceptable = true,
                    Value = Convert.ToInt32(neededStat.AwayPercent * 100)
                };
            }

            return new ForecastDetails();
        }

        public static ForecastDetails Check_Away_FT_15_Over(List<StatisticInfoHolder> statInfoHolders, int minFoundCount, int minPercentage)
        {
            if (statInfoHolders == null || statInfoHolders.Count == 0)
                return new ForecastDetails();

            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "Ind_FT_15");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.HomePercent) >= minFoundCount && Convert.ToInt32(neededStat.AwayPercent * 100) >= minPercentage)
            {
                return new ForecastDetails
                {
                    IsAcceptable = true,
                    Value = Convert.ToInt32(neededStat.AwayPercent * 100)
                };
            }

            return new ForecastDetails();
        }

        public static ForecastDetails Check_Home_HT_05_Over(List<StatisticInfoHolder> statInfoHolders, int minFoundCount, int minPercentage)
        {
            if (statInfoHolders == null || statInfoHolders.Count == 0)
                return new ForecastDetails();

            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "Ind_HT_05");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.HomePercent) >= minFoundCount && Convert.ToInt32(neededStat.HomePercent * 100) >= minPercentage)
            {
                return new ForecastDetails
                {
                    IsAcceptable = true,
                    Value = Convert.ToInt32(neededStat.HomePercent * 100)
                };
            }

            return new ForecastDetails();
        }

        public static ForecastDetails Check_Away_HT_05_Over(List<StatisticInfoHolder> statInfoHolders, int minFoundCount, int minPercentage)
        {
            if (statInfoHolders == null || statInfoHolders.Count == 0)
                return new ForecastDetails();

            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "Ind_HT_05");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.HomePercent) >= minFoundCount && Convert.ToInt32(neededStat.AwayPercent * 100) >= minPercentage)
            {
                return new ForecastDetails
                {
                    IsAcceptable = true,
                    Value = Convert.ToInt32(neededStat.AwayPercent * 100)
                };
            }

            return new ForecastDetails();
        }


        public static ForecastDetails Check_Home_SH_05_Over(List<StatisticInfoHolder> statInfoHolders, int minFoundCount, int minPercentage)
        {
            if (statInfoHolders == null || statInfoHolders.Count == 0)
                return new ForecastDetails();

            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "Ind_SH_05");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.HomePercent) >= minFoundCount && Convert.ToInt32(neededStat.HomePercent * 100) >= minPercentage)
            {
                return new ForecastDetails
                {
                    IsAcceptable = true,
                    Value = Convert.ToInt32(neededStat.HomePercent * 100)
                };
            }

            return new ForecastDetails();
        }

        public static ForecastDetails Check_Away_SH_05_Over(List<StatisticInfoHolder> statInfoHolders, int minFoundCount, int minPercentage)
        {
            if (statInfoHolders == null || statInfoHolders.Count == 0)
                return new ForecastDetails();

            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "Ind_SH_05");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.HomePercent) >= minFoundCount && Convert.ToInt32(neededStat.AwayPercent * 100) >= minPercentage)
            {
                return new ForecastDetails
                {
                    IsAcceptable = true,
                    Value = Convert.ToInt32(neededStat.AwayPercent * 100)
                };
            }

            return new ForecastDetails();
        }


        public static ForecastDetails Check_Home_FT_Win(List<StatisticInfoHolder> statInfoHolders, int minFoundCount, int minPercentage)
        {
            if (statInfoHolders == null || statInfoHolders.Count == 0)
                return new ForecastDetails();

            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "Ind_FT_Win");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.HomePercent) >= minFoundCount && Convert.ToInt32(neededStat.HomePercent * 100) >= minPercentage)
            {
                return new ForecastDetails
                {
                    IsAcceptable = true,
                    Value = Convert.ToInt32(neededStat.HomePercent * 100)
                };
            }

            return new ForecastDetails();
        }


        public static ForecastDetails Check_Home_HT_Win(List<StatisticInfoHolder> statInfoHolders, int minFoundCount, int minPercentage)
        {
            if (statInfoHolders == null || statInfoHolders.Count == 0)
                return new ForecastDetails();

            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "Ind_HT_Win");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.HomePercent) >= minFoundCount && Convert.ToInt32(neededStat.HomePercent * 100) >= minPercentage)
            {
                return new ForecastDetails
                {
                    IsAcceptable = true,
                    Value = Convert.ToInt32(neededStat.HomePercent * 100)
                };
            }

            return new ForecastDetails();
        }

        public static ForecastDetails Check_Home_SH_Win(List<StatisticInfoHolder> statInfoHolders, int minFoundCount, int minPercentage)
        {
            if (statInfoHolders == null || statInfoHolders.Count == 0)
                return new ForecastDetails();

            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "Ind_SH_Win");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.HomePercent) >= minFoundCount && Convert.ToInt32(neededStat.HomePercent * 100) >= minPercentage)
            {
                return new ForecastDetails
                {
                    IsAcceptable = true,
                    Value = Convert.ToInt32(neededStat.HomePercent * 100)
                };
            }

            return new ForecastDetails();
        }


        public static ForecastDetails Check_Away_FT_Win(List<StatisticInfoHolder> statInfoHolders, int minFoundCount, int minPercentage)
        {
            if (statInfoHolders == null || statInfoHolders.Count == 0)
                return new ForecastDetails();

            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "Ind_FT_Win");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.HomePercent) >= minFoundCount && Convert.ToInt32(neededStat.AwayPercent * 100) >= minPercentage)
            {
                return new ForecastDetails
                {
                    IsAcceptable = true,
                    Value = Convert.ToInt32(neededStat.AwayPercent * 100)
                };
            }

            return new ForecastDetails();
        }


        public static ForecastDetails Check_Away_HT_Win(List<StatisticInfoHolder> statInfoHolders, int minFoundCount, int minPercentage)
        {
            if (statInfoHolders == null || statInfoHolders.Count == 0)
                return new ForecastDetails();

            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "Ind_HT_Win");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.HomePercent) >= minFoundCount && Convert.ToInt32(neededStat.AwayPercent * 100) >= minPercentage)
            {
                return new ForecastDetails
                {
                    IsAcceptable = true,
                    Value = Convert.ToInt32(neededStat.AwayPercent * 100)
                };
            }

            return new ForecastDetails();
        }

        public static ForecastDetails Check_Away_SH_Win(List<StatisticInfoHolder> statInfoHolders, int minFoundCount, int minPercentage)
        {
            if (statInfoHolders == null || statInfoHolders.Count == 0)
                return new ForecastDetails();

            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "Ind_SH_Win");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.HomePercent) >= minFoundCount && Convert.ToInt32(neededStat.AwayPercent * 100) >= minPercentage)
            {
                return new ForecastDetails
                {
                    IsAcceptable = true,
                    Value = Convert.ToInt32(neededStat.AwayPercent * 100)
                };
            }

            return new ForecastDetails();
        }


        public static ForecastDetails Check_FT_Draw(List<StatisticInfoHolder> statInfoHolders, int minFoundCount, int minPercentage)
        {
            if (statInfoHolders == null || statInfoHolders.Count == 0)
                return new ForecastDetails();

            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "FT_X");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.HomePercent) >= minFoundCount && Convert.ToInt32(neededStat.HomePercent * 100) >= minPercentage)
            {
                return new ForecastDetails
                {
                    IsAcceptable = true,
                    Value = Convert.ToInt32(neededStat.HomePercent * 100)
                };
            }

            return new ForecastDetails();
        }

        public static ForecastDetails Check_HT_Draw(List<StatisticInfoHolder> statInfoHolders, int minFoundCount, int minPercentage)
        {
            if (statInfoHolders == null || statInfoHolders.Count == 0)
                return new ForecastDetails();

            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "HT_X");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.HomePercent) >= minFoundCount && Convert.ToInt32(neededStat.HomePercent * 100) >= minPercentage)
            {
                return new ForecastDetails
                {
                    IsAcceptable = true,
                    Value = Convert.ToInt32(neededStat.HomePercent * 100)
                };
            }

            return new ForecastDetails();
        }

        public static ForecastDetails Check_SH_Draw(List<StatisticInfoHolder> statInfoHolders, int minFoundCount, int minPercentage)
        {
            if (statInfoHolders == null || statInfoHolders.Count == 0)
                return new ForecastDetails();

            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "SH_X");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.HomePercent) >= minFoundCount && Convert.ToInt32(neededStat.HomePercent * 100) >= minPercentage)
            {
                return new ForecastDetails
                {
                    IsAcceptable = true,
                    Value = Convert.ToInt32(neededStat.HomePercent * 100)
                };
            }

            return new ForecastDetails();
        }


        public static ForecastDetails Check_FT_Win1_Or_Draw(List<StatisticInfoHolder> statInfoHolders, int minFoundCount, int minPercentage)
        {
            if (statInfoHolders == null || statInfoHolders.Count == 0)
                return new ForecastDetails();

            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "Ind_FT_Win");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.HomePercent) >= minFoundCount && Convert.ToInt32(100 - (neededStat.AwayPercent * 100)) >= minPercentage)
            {
                return new ForecastDetails
                {
                    IsAcceptable = true,
                    Value = Convert.ToInt32(100 - (neededStat.AwayPercent * 100))
                };
            }

            return new ForecastDetails();
        }

        public static ForecastDetails Check_HT_Win1_Or_Draw(List<StatisticInfoHolder> statInfoHolders, int minFoundCount, int minPercentage)
        {
            if (statInfoHolders == null || statInfoHolders.Count == 0)
                return new ForecastDetails();

            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "Ind_HT_Win");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.HomePercent) >= minFoundCount && Convert.ToInt32(100 - (neededStat.AwayPercent * 100)) >= minPercentage)
            {
                return new ForecastDetails
                {
                    IsAcceptable = true,
                    Value = Convert.ToInt32(100 - (neededStat.AwayPercent * 100))
                };
            }

            return new ForecastDetails();
        }

        public static ForecastDetails Check_SH_Win1_Or_Draw(List<StatisticInfoHolder> statInfoHolders, int minFoundCount, int minPercentage)
        {
            if (statInfoHolders == null || statInfoHolders.Count == 0)
                return new ForecastDetails();

            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "Ind_SH_Win");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.HomePercent) >= minFoundCount && Convert.ToInt32(100 - (neededStat.AwayPercent * 100)) >= minPercentage)
            {
                return new ForecastDetails
                {
                    IsAcceptable = true,
                    Value = Convert.ToInt32(100 - (neededStat.AwayPercent * 100))
                };
            }

            return new ForecastDetails();
        }

        public static ForecastDetails Check_FT_Draw_Or_Win2(List<StatisticInfoHolder> statInfoHolders, int minFoundCount, int minPercentage)
        {
            if (statInfoHolders == null || statInfoHolders.Count == 0)
                return new ForecastDetails();

            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "Ind_FT_Win");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.HomePercent) >= minFoundCount && Convert.ToInt32(100 - (neededStat.HomePercent * 100)) >= minPercentage)
            {
                return new ForecastDetails
                {
                    IsAcceptable = true,
                    Value = Convert.ToInt32(100 - (neededStat.HomePercent * 100))
                };
            }

            return new ForecastDetails();
        }

        public static ForecastDetails Check_HT_Draw_Or_Win2(List<StatisticInfoHolder> statInfoHolders, int minFoundCount, int minPercentage)
        {
            if (statInfoHolders == null || statInfoHolders.Count == 0)
                return new ForecastDetails();

            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "Ind_HT_Win");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.HomePercent) >= minFoundCount && Convert.ToInt32(100 - (neededStat.HomePercent * 100)) >= minPercentage)
            {
                return new ForecastDetails
                {
                    IsAcceptable = true,
                    Value = Convert.ToInt32(100 - (neededStat.HomePercent * 100))
                };
            }

            return new ForecastDetails();
        }

        public static ForecastDetails Check_SH_Draw_Or_Win2(List<StatisticInfoHolder> statInfoHolders, int minFoundCount, int minPercentage)
        {
            if (statInfoHolders == null || statInfoHolders.Count == 0)
                return new ForecastDetails();

            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "Ind_SH_Win");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.HomePercent) >= minFoundCount && Convert.ToInt32(100 - (neededStat.HomePercent * 100)) >= minPercentage)
            {
                return new ForecastDetails
                {
                    IsAcceptable = true,
                    Value = Convert.ToInt32(100 - (neededStat.HomePercent * 100))
                };
            }

            return new ForecastDetails();
        }

        public static ForecastDetails Check_FT_Win1_Or_Win2(List<StatisticInfoHolder> statInfoHolders, int minFoundCount, int minPercentage)
        {
            if (statInfoHolders == null || statInfoHolders.Count == 0)
                return new ForecastDetails();

            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "FT_X");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.HomePercent) >= minFoundCount && Convert.ToInt32(100 - (neededStat.HomePercent * 100)) >= minPercentage)
            {
                return new ForecastDetails
                {
                    IsAcceptable = true,
                    Value = Convert.ToInt32(100 - (neededStat.HomePercent * 100))
                };
            }

            return new ForecastDetails();
        }

        public static ForecastDetails Check_HT_Win1_Or_Win2(List<StatisticInfoHolder> statInfoHolders, int minFoundCount, int minPercentage)
        {
            if (statInfoHolders == null || statInfoHolders.Count == 0)
                return new ForecastDetails();

            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "HT_X");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.HomePercent) >= minFoundCount && Convert.ToInt32(100 - (neededStat.HomePercent * 100)) >= minPercentage)
            {
                return new ForecastDetails
                {
                    IsAcceptable = true,
                    Value = Convert.ToInt32(100 - (neededStat.HomePercent * 100))
                };
            }

            return new ForecastDetails();
        }

        public static ForecastDetails Check_SH_Win1_Or_Win2(List<StatisticInfoHolder> statInfoHolders, int minFoundCount, int minPercentage)
        {
            if (statInfoHolders == null || statInfoHolders.Count == 0)
                return new ForecastDetails();

            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "SH_X");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.HomePercent) >= minFoundCount && Convert.ToInt32(100 - (neededStat.HomePercent * 100)) >= minPercentage)
            {
                return new ForecastDetails
                {
                    IsAcceptable = true,
                    Value = Convert.ToInt32(100 - (neededStat.HomePercent * 100))
                };
            }

            return new ForecastDetails();
        }



        public static string JoinForecast(JobForecast jobForecast)
        {
            var builder = new StringBuilder();

            if (jobForecast.IsFT_15Over.IsAcceptable)
                builder.AppendLine($"🔘  MS 1,5 Üst   ~({jobForecast.IsFT_15Over.Value} %)");

            if (jobForecast.IsFT_25Over.IsAcceptable)
                builder.AppendLine($"🔘  MS 2,5 Üst   ~({jobForecast.IsFT_25Over.Value} %)");

            if (jobForecast.IsFT_GG.IsAcceptable)
                builder.AppendLine($"🔘  MS Gol/Gol   ~({jobForecast.IsFT_GG.Value} %)");

            if (jobForecast.IsFT_NG.IsAcceptable)
                builder.AppendLine($"🔘  MS Yox/Gol   ~({100 - jobForecast.IsFT_NG.Value} %)");

            if (jobForecast.IsHT_05Over.IsAcceptable)
                builder.AppendLine($"🔘  BH 0,5 Üst   ~({jobForecast.IsHT_05Over.Value} %)");

            if (jobForecast.IsSH_05Over.IsAcceptable)
                builder.AppendLine($"🔘  İH 0,5 Üst   ~({jobForecast.IsSH_05Over.Value} %)");

            if (jobForecast.IsFT_35Under.IsAcceptable)
                builder.AppendLine($"🔘  MS 3,5 Alt   ~({100 - jobForecast.IsFT_35Under.Value} %)");

            if (jobForecast.IsHT_15Under.IsAcceptable)
                builder.AppendLine($"🔘  BH 1,5 Alt   ~({100 - jobForecast.IsHT_15Under.Value} %)");

            if (jobForecast.IsSH_15Under.IsAcceptable)
                builder.AppendLine($"🔘  İH 1,5 Alt   ~({100 - jobForecast.IsSH_15Under.Value} %)");

            if (jobForecast.IsHome_FT_05_Over.IsAcceptable)
                builder.AppendLine($"🔘  Tkm-1 MS 0,5 Üst   ~({jobForecast.IsHome_FT_05_Over.Value} %)");

            if (jobForecast.IsHome_FT_15_Over.IsAcceptable)
                builder.AppendLine($"🔘  Tkm-1 MS 1,5 Üst   ~({jobForecast.IsHome_FT_15_Over.Value} %)");

            if (jobForecast.IsHome_HT_05_Over.IsAcceptable)
                builder.AppendLine($"🔘  Tkm-1 BH 0,5 Üst   ~({jobForecast.IsHome_HT_05_Over.Value} %)");

            if (jobForecast.IsHome_SH_05_Over.IsAcceptable)
                builder.AppendLine($"🔘  Tkm-1 İH 0,5 Üst   ~({jobForecast.IsHome_SH_05_Over.Value} %)");

            if (jobForecast.IsAway_FT_05_Over.IsAcceptable)
                builder.AppendLine($"🔘  Tkm-2 MS 0,5 Üst   ~({jobForecast.IsAway_FT_05_Over.Value} %)");

            if (jobForecast.IsAway_FT_15_Over.IsAcceptable)
                builder.AppendLine($"🔘  Tkm-2 MS 1,5 Üst   ~({jobForecast.IsAway_FT_15_Over.Value} %)");

            if (jobForecast.IsAway_HT_05_Over.IsAcceptable)
                builder.AppendLine($"🔘  Tkm-2 BH 0,5 Üst   ~({jobForecast.IsAway_HT_05_Over.Value} %)");

            if (jobForecast.IsAway_SH_05_Over.IsAcceptable)
                builder.AppendLine($"🔘  Tkm-2 İH 0,5 Üst   ~({jobForecast.IsAway_SH_05_Over.Value} %)");

            if (jobForecast.IsHome_FT_Win.IsAcceptable)
                builder.AppendLine($"🔘  MS Tkm-1 Qələbə   ~({jobForecast.IsHome_FT_Win.Value} %)");

            if (jobForecast.IsHome_HT_Win.IsAcceptable)
                builder.AppendLine($"🔘  BH Tkm-1 Qələbə   ~({jobForecast.IsHome_HT_Win.Value} %)");

            if (jobForecast.IsHome_SH_Win.IsAcceptable)
                builder.AppendLine($"🔘  İH Tkm-1 Qələbə   ~({jobForecast.IsHome_SH_Win.Value} %)");

            if (jobForecast.IsAway_FT_Win.IsAcceptable)
                builder.AppendLine($"🔘  MS Tkm-2 Qələbə   ~({jobForecast.IsAway_FT_Win.Value} %)");

            if (jobForecast.IsAway_HT_Win.IsAcceptable)
                builder.AppendLine($"🔘  BH Tkm-2 Qələbə   ~({jobForecast.IsAway_HT_Win.Value} %)");

            if (jobForecast.IsAway_SH_Win.IsAcceptable)
                builder.AppendLine($"🔘  İH Tkm-2 Qələbə   ~({jobForecast.IsAway_SH_Win.Value} %)");

            if (jobForecast.Is_FT_Draw.IsAcceptable)
                builder.AppendLine($"🔘  MS Heç-Heçə   ~({jobForecast.Is_FT_Draw.Value} %)");

            if (jobForecast.Is_HT_Draw.IsAcceptable)
                builder.AppendLine($"🔘  BH Heç-Heçə   ~({jobForecast.Is_HT_Draw.Value} %)");

            if (jobForecast.Is_SH_Draw.IsAcceptable)
                builder.AppendLine($"🔘  İH Heç-Heçə   ~({jobForecast.Is_SH_Draw.Value} %)");

            if (jobForecast.Is_FT_Win1_Or_X.IsAcceptable)
                builder.AppendLine($"🔘  MS Tkm-1 Qələbə/HeçHeçə   ~({jobForecast.Is_FT_Win1_Or_X.Value} %)");

            if (jobForecast.Is_HT_Win1_Or_X.IsAcceptable)
                builder.AppendLine($"🔘  BH Tkm-1 Qələbə/HeçHeçə   ~({jobForecast.Is_HT_Win1_Or_X.Value} %)");

            if (jobForecast.Is_SH_Win1_Or_X.IsAcceptable)
                builder.AppendLine($"🔘  İH Tkm-1 Qələbə/HeçHeçə   ~({jobForecast.Is_SH_Win1_Or_X.Value} %)");

            if (jobForecast.Is_FT_X_Or_Win2.IsAcceptable)
                builder.AppendLine($"🔘  MS Tkm-2 Qələbə/HeçHeçə   ~({jobForecast.Is_FT_X_Or_Win2.Value} %)");

            if (jobForecast.Is_HT_X_Or_Win2.IsAcceptable)
                builder.AppendLine($"🔘  BH Tkm-2 Qələbə/HeçHeçə   ~({jobForecast.Is_HT_X_Or_Win2.Value} %)");

            if (jobForecast.Is_SH_X_Or_Win2.IsAcceptable)
                builder.AppendLine($"🔘  İH Tkm-2 Qələbə/HeçHeçə   ~({jobForecast.Is_SH_X_Or_Win2.Value} %)");

            if (jobForecast.Is_FT_Win1_Or_Win2.IsAcceptable)
                builder.AppendLine($"🔘  MS Tkm-1/Tkm-2 Qələbə   ~({jobForecast.Is_FT_Win1_Or_Win2.Value} %)");

            if (jobForecast.Is_HT_Win1_Or_Win2.IsAcceptable)
                builder.AppendLine($"🔘  BH Tkm-1/Tkm-2 Qələbə   ~({jobForecast.Is_HT_Win1_Or_Win2.Value} %)");

            if (jobForecast.Is_SH_Win1_Or_Win2.IsAcceptable)
                builder.AppendLine($"🔘  İH Tkm-1/Tkm-2 Qələbə   ~({jobForecast.Is_SH_Win1_Or_Win2.Value} %)");

            return builder.ToString().Trim();
        }

        public static string JoinForecast(JobForecast jobForecast, List<Forecast> matchedForecasts)
        {
            var builder = new StringBuilder();

            if (jobForecast.IsFT_15Over.IsAcceptable && matchedForecasts.Any(x=>x.Key.ToLower() == ForecastKeys.FT_15_Over.ToLower()))
                builder.AppendLine($"🔘  MS 1,5 Üst   ~({jobForecast.IsFT_15Over.Value} %)");

            if (jobForecast.IsFT_25Over.IsAcceptable && matchedForecasts.Any(x => x.Key.ToLower() == ForecastKeys.FT_25_Over.ToLower()))
                builder.AppendLine($"🔘  MS 2,5 Üst   ~({jobForecast.IsFT_25Over.Value} %)");

            if (jobForecast.IsFT_GG.IsAcceptable && matchedForecasts.Any(x=>x.Key.ToLower() == ForecastKeys.FT_GG.ToLower()))
                builder.AppendLine($"🔘  MS Gol/Gol   ~({jobForecast.IsFT_GG.Value} %)");

            if (jobForecast.IsFT_NG.IsAcceptable && matchedForecasts.Any(x=>x.Key.ToLower() == ForecastKeys.FT_NG.ToLower()))
                builder.AppendLine($"🔘  MS Yox/Gol   ~({100 - jobForecast.IsFT_NG.Value} %)");

            if (jobForecast.IsHT_05Over.IsAcceptable && matchedForecasts.Any(x=>x.Key.ToLower() == ForecastKeys.HT_05_Over.ToLower()))
                builder.AppendLine($"🔘  BH 0,5 Üst   ~({jobForecast.IsHT_05Over.Value} %)");

            if (jobForecast.IsSH_05Over.IsAcceptable && matchedForecasts.Any(x=>x.Key.ToLower() == ForecastKeys.SH_05_Over.ToLower()))
                builder.AppendLine($"🔘  İH 0,5 Üst   ~({jobForecast.IsSH_05Over.Value} %)");

            if (jobForecast.IsFT_35Under.IsAcceptable && matchedForecasts.Any(x=>x.Key.ToLower() == ForecastKeys.FT_35_Under.ToLower()))
                builder.AppendLine($"🔘  MS 3,5 Alt   ~({100 - jobForecast.IsFT_35Under.Value} %)");

            if (jobForecast.IsHT_15Under.IsAcceptable && matchedForecasts.Any(x=>x.Key.ToLower() == ForecastKeys.HT_15_Under.ToLower()))
                builder.AppendLine($"🔘  BH 1,5 Alt   ~({100 - jobForecast.IsHT_15Under.Value} %)");

            if (jobForecast.IsSH_15Under.IsAcceptable && matchedForecasts.Any(x=>x.Key.ToLower() == ForecastKeys.SH_15_Under.ToLower()))
                builder.AppendLine($"🔘  İH 1,5 Alt   ~({100 - jobForecast.IsSH_15Under.Value} %)");

            if (jobForecast.IsHome_FT_05_Over.IsAcceptable && matchedForecasts.Any(x=>x.Key.ToLower() == ForecastKeys.FT_Home_Goal.ToLower()))
                builder.AppendLine($"🔘  Tkm-1 MS 0,5 Üst   ~({jobForecast.IsHome_FT_05_Over.Value} %)");

            if (jobForecast.IsHome_FT_15_Over.IsAcceptable && matchedForecasts.Any(x=>x.Key.ToLower() == ForecastKeys.FT_Home_2nd_Goal.ToLower()))
                builder.AppendLine($"🔘  Tkm-1 MS 1,5 Üst   ~({jobForecast.IsHome_FT_15_Over.Value} %)");

            if (jobForecast.IsHome_HT_05_Over.IsAcceptable && matchedForecasts.Any(x=>x.Key.ToLower() == ForecastKeys.HT_Home_Goal.ToLower()))
                builder.AppendLine($"🔘  Tkm-1 BH 0,5 Üst   ~({jobForecast.IsHome_HT_05_Over.Value} %)");

            if (jobForecast.IsHome_SH_05_Over.IsAcceptable && matchedForecasts.Any(x=>x.Key.ToLower() == ForecastKeys.SH_Home_Goal.ToLower()))
                builder.AppendLine($"🔘  Tkm-1 İH 0,5 Üst   ~({jobForecast.IsHome_SH_05_Over.Value} %)");

            if (jobForecast.IsAway_FT_05_Over.IsAcceptable && matchedForecasts.Any(x=>x.Key.ToLower() == ForecastKeys.FT_Away_Goal.ToLower()))
                builder.AppendLine($"🔘  Tkm-2 MS 0,5 Üst   ~({jobForecast.IsAway_FT_05_Over.Value} %)");

            if (jobForecast.IsAway_FT_15_Over.IsAcceptable && matchedForecasts.Any(x=>x.Key.ToLower() == ForecastKeys.FT_Away_2nd_Goal.ToLower()))
                builder.AppendLine($"🔘  Tkm-2 MS 1,5 Üst   ~({jobForecast.IsAway_FT_15_Over.Value} %)");

            if (jobForecast.IsAway_HT_05_Over.IsAcceptable && matchedForecasts.Any(x=>x.Key.ToLower() == ForecastKeys.HT_Away_Goal.ToLower()))
                builder.AppendLine($"🔘  Tkm-2 BH 0,5 Üst   ~({jobForecast.IsAway_HT_05_Over.Value} %)");

            if (jobForecast.IsAway_SH_05_Over.IsAcceptable && matchedForecasts.Any(x=>x.Key.ToLower() == ForecastKeys.SH_Away_Goal.ToLower()))
                builder.AppendLine($"🔘  Tkm-2 İH 0,5 Üst   ~({jobForecast.IsAway_SH_05_Over.Value} %)");

            if (jobForecast.IsHome_FT_Win.IsAcceptable && matchedForecasts.Any(x=>x.Key.ToLower() == ForecastKeys.FT_Win_1.ToLower()))
                builder.AppendLine($"🔘  MS Tkm-1 Qələbə   ~({jobForecast.IsHome_FT_Win.Value} %)");

            if (jobForecast.IsHome_HT_Win.IsAcceptable && matchedForecasts.Any(x=>x.Key.ToLower() == ForecastKeys.HT_Win_1.ToLower()))
                builder.AppendLine($"🔘  BH Tkm-1 Qələbə   ~({jobForecast.IsHome_HT_Win.Value} %)");

            if (jobForecast.IsHome_SH_Win.IsAcceptable && matchedForecasts.Any(x=>x.Key.ToLower() == ForecastKeys.SH_Win_1.ToLower()))
                builder.AppendLine($"🔘  İH Tkm-1 Qələbə   ~({jobForecast.IsHome_SH_Win.Value} %)");

            if (jobForecast.IsAway_FT_Win.IsAcceptable && matchedForecasts.Any(x=>x.Key.ToLower() == ForecastKeys.FT_Win_2.ToLower()))
                builder.AppendLine($"🔘  MS Tkm-2 Qələbə   ~({jobForecast.IsAway_FT_Win.Value} %)");

            if (jobForecast.IsAway_HT_Win.IsAcceptable && matchedForecasts.Any(x=>x.Key.ToLower() == ForecastKeys.HT_Win_2.ToLower()))
                builder.AppendLine($"🔘  BH Tkm-2 Qələbə   ~({jobForecast.IsAway_HT_Win.Value} %)");

            if (jobForecast.IsAway_SH_Win.IsAcceptable && matchedForecasts.Any(x=>x.Key.ToLower() == ForecastKeys.SH_Win_2.ToLower()))
                builder.AppendLine($"🔘  İH Tkm-2 Qələbə   ~({jobForecast.IsAway_SH_Win.Value} %)");

            //if (jobForecast.Is_FT_Draw.IsAcceptable && matchedForecasts.Any(x=>x.Key.ToLower() == ForecastKeys.FT_15_Over))
            //    builder.AppendLine($"🔘  MS Heç-Heçə   ~({jobForecast.Is_FT_Draw.Value} %)");

            //if (jobForecast.Is_HT_Draw.IsAcceptable && matchedForecasts.Any(x=>x.Key.ToLower() == ForecastKeys.FT_15_Over))
            //    builder.AppendLine($"🔘  BH Heç-Heçə   ~({jobForecast.Is_HT_Draw.Value} %)");

            //if (jobForecast.Is_SH_Draw.IsAcceptable && matchedForecasts.Any(x=>x.Key.ToLower() == ForecastKeys.FT_15_Over))
            //    builder.AppendLine($"🔘  İH Heç-Heçə   ~({jobForecast.Is_SH_Draw.Value} %)");

            //if (jobForecast.Is_FT_Win1_Or_X.IsAcceptable && matchedForecasts.Any(x=>x.Key.ToLower() == ForecastKeys.FT_15_Over))
            //    builder.AppendLine($"🔘  MS Tkm-1 Qələbə/HeçHeçə   ~({jobForecast.Is_FT_Win1_Or_X.Value} %)");

            //if (jobForecast.Is_HT_Win1_Or_X.IsAcceptable && matchedForecasts.Any(x=>x.Key.ToLower() == ForecastKeys.FT_15_Over))
            //    builder.AppendLine($"🔘  BH Tkm-1 Qələbə/HeçHeçə   ~({jobForecast.Is_HT_Win1_Or_X.Value} %)");

            //if (jobForecast.Is_SH_Win1_Or_X.IsAcceptable && matchedForecasts.Any(x=>x.Key.ToLower() == ForecastKeys.FT_15_Over))
            //    builder.AppendLine($"🔘  İH Tkm-1 Qələbə/HeçHeçə   ~({jobForecast.Is_SH_Win1_Or_X.Value} %)");

            //if (jobForecast.Is_FT_X_Or_Win2.IsAcceptable && matchedForecasts.Any(x=>x.Key.ToLower() == ForecastKeys.FT_15_Over))
            //    builder.AppendLine($"🔘  MS Tkm-2 Qələbə/HeçHeçə   ~({jobForecast.Is_FT_X_Or_Win2.Value} %)");

            //if (jobForecast.Is_HT_X_Or_Win2.IsAcceptable && matchedForecasts.Any(x=>x.Key.ToLower() == ForecastKeys.FT_15_Over))
            //    builder.AppendLine($"🔘  BH Tkm-2 Qələbə/HeçHeçə   ~({jobForecast.Is_HT_X_Or_Win2.Value} %)");

            //if (jobForecast.Is_SH_X_Or_Win2.IsAcceptable && matchedForecasts.Any(x=>x.Key.ToLower() == ForecastKeys.FT_15_Over))
            //    builder.AppendLine($"🔘  İH Tkm-2 Qələbə/HeçHeçə   ~({jobForecast.Is_SH_X_Or_Win2.Value} %)");

            //if (jobForecast.Is_FT_Win1_Or_Win2.IsAcceptable && matchedForecasts.Any(x=>x.Key.ToLower() == ForecastKeys.FT_15_Over))
            //    builder.AppendLine($"🔘  MS Tkm-1/Tkm-2 Qələbə   ~({jobForecast.Is_FT_Win1_Or_Win2.Value} %)");

            //if (jobForecast.Is_HT_Win1_Or_Win2.IsAcceptable && matchedForecasts.Any(x=>x.Key.ToLower() == ForecastKeys.FT_15_Over))
            //    builder.AppendLine($"🔘  BH Tkm-1/Tkm-2 Qələbə   ~({jobForecast.Is_HT_Win1_Or_Win2.Value} %)");

            //if (jobForecast.Is_SH_Win1_Or_Win2.IsAcceptable && matchedForecasts.Any(x=>x.Key.ToLower() == ForecastKeys.FT_15_Over))
            //    builder.AppendLine($"🔘  İH Tkm-1/Tkm-2 Qələbə   ~({jobForecast.Is_SH_Win1_Or_Win2.Value} %)");

            return builder.ToString().Trim();
        }

        public static List<Forecast> PrepareForecastList(JobForecast jobForecast, int serial, bool is99Percent = false)
        {
            var forecastList = new List<Forecast>();

            if (jobForecast != null)
            {
                if (jobForecast.IsFT_15Over.IsAcceptable)
                    forecastList.Add(new Forecast(ForecastKeys.FT_15_Over, serial, jobForecast.MatchIdentifier, is99Percent));
                if (jobForecast.IsFT_25Over.IsAcceptable)
                    forecastList.Add(new Forecast(ForecastKeys.FT_25_Over, serial, jobForecast.MatchIdentifier, is99Percent));
                if (jobForecast.IsFT_GG.IsAcceptable)
                    forecastList.Add(new Forecast(ForecastKeys.FT_GG, serial, jobForecast.MatchIdentifier, is99Percent));
                if (jobForecast.IsFT_NG.IsAcceptable)
                    forecastList.Add(new Forecast(ForecastKeys.FT_NG, serial, jobForecast.MatchIdentifier, is99Percent));
                if (jobForecast.IsHT_05Over.IsAcceptable)
                    forecastList.Add(new Forecast(ForecastKeys.HT_05_Over, serial, jobForecast.MatchIdentifier, is99Percent));
                if (jobForecast.IsSH_05Over.IsAcceptable)
                    forecastList.Add(new Forecast(ForecastKeys.SH_05_Over, serial, jobForecast.MatchIdentifier, is99Percent));
                if (jobForecast.IsFT_35Under.IsAcceptable)
                    forecastList.Add(new Forecast(ForecastKeys.FT_35_Under, serial, jobForecast.MatchIdentifier, is99Percent));
                if (jobForecast.IsHT_15Under.IsAcceptable)
                    forecastList.Add(new Forecast(ForecastKeys.HT_15_Under, serial, jobForecast.MatchIdentifier, is99Percent));
                if (jobForecast.IsSH_15Under.IsAcceptable)
                    forecastList.Add(new Forecast(ForecastKeys.SH_15_Under, serial, jobForecast.MatchIdentifier, is99Percent));

                if (jobForecast.IsHome_FT_05_Over.IsAcceptable)
                    forecastList.Add(new Forecast(ForecastKeys.FT_Home_Goal, serial, jobForecast.MatchIdentifier, is99Percent));
                if (jobForecast.IsHome_FT_15_Over.IsAcceptable)
                    forecastList.Add(new Forecast(ForecastKeys.FT_Home_2nd_Goal, serial, jobForecast.MatchIdentifier, is99Percent));
                if (jobForecast.IsHome_HT_05_Over.IsAcceptable)
                    forecastList.Add(new Forecast(ForecastKeys.HT_Home_Goal, serial, jobForecast.MatchIdentifier, is99Percent));
                if (jobForecast.IsHome_SH_05_Over.IsAcceptable)
                    forecastList.Add(new Forecast(ForecastKeys.SH_Home_Goal, serial, jobForecast.MatchIdentifier, is99Percent));
                if (jobForecast.IsAway_FT_05_Over.IsAcceptable)
                    forecastList.Add(new Forecast(ForecastKeys.FT_Away_Goal, serial, jobForecast.MatchIdentifier, is99Percent));
                if (jobForecast.IsAway_FT_15_Over.IsAcceptable)
                    forecastList.Add(new Forecast(ForecastKeys.FT_Away_2nd_Goal, serial, jobForecast.MatchIdentifier, is99Percent));
                if (jobForecast.IsAway_HT_05_Over.IsAcceptable)
                    forecastList.Add(new Forecast(ForecastKeys.HT_Away_Goal, serial, jobForecast.MatchIdentifier, is99Percent));
                if (jobForecast.IsAway_SH_05_Over.IsAcceptable)
                    forecastList.Add(new Forecast(ForecastKeys.SH_Away_Goal, serial, jobForecast.MatchIdentifier, is99Percent));
                if (jobForecast.IsHome_FT_Win.IsAcceptable)
                    forecastList.Add(new Forecast(ForecastKeys.FT_Win_1, serial, jobForecast.MatchIdentifier, is99Percent));
                if (jobForecast.IsHome_HT_Win.IsAcceptable)
                    forecastList.Add(new Forecast(ForecastKeys.HT_Win_1, serial, jobForecast.MatchIdentifier, is99Percent));
                if (jobForecast.IsHome_SH_Win.IsAcceptable)
                    forecastList.Add(new Forecast(ForecastKeys.SH_Win_1, serial, jobForecast.MatchIdentifier, is99Percent));
                if (jobForecast.IsAway_FT_Win.IsAcceptable)
                    forecastList.Add(new Forecast(ForecastKeys.FT_Win_2, serial, jobForecast.MatchIdentifier, is99Percent));
                if (jobForecast.IsAway_HT_Win.IsAcceptable)
                    forecastList.Add(new Forecast(ForecastKeys.HT_Win_2, serial, jobForecast.MatchIdentifier, is99Percent));
                if (jobForecast.IsAway_SH_Win.IsAcceptable)
                    forecastList.Add(new Forecast(ForecastKeys.SH_Win_2, serial, jobForecast.MatchIdentifier, is99Percent));
                if (jobForecast.Is_FT_Draw.IsAcceptable)
                    forecastList.Add(new Forecast(ForecastKeys.FT_X, serial, jobForecast.MatchIdentifier, is99Percent));
                if (jobForecast.Is_HT_Draw.IsAcceptable)
                    forecastList.Add(new Forecast(ForecastKeys.HT_X, serial, jobForecast.MatchIdentifier, is99Percent));
                if (jobForecast.Is_SH_Draw.IsAcceptable)
                    forecastList.Add(new Forecast(ForecastKeys.SH_X, serial, jobForecast.MatchIdentifier, is99Percent));

                if (jobForecast.Is_FT_Win1_Or_X.IsAcceptable)
                    forecastList.Add(new Forecast(ForecastKeys.FT_Win1_Or_X, serial, jobForecast.MatchIdentifier, is99Percent));
                if (jobForecast.Is_HT_Win1_Or_X.IsAcceptable)
                    forecastList.Add(new Forecast(ForecastKeys.HT_Win1_Or_X, serial, jobForecast.MatchIdentifier, is99Percent));
                if (jobForecast.Is_SH_Win1_Or_X.IsAcceptable)
                    forecastList.Add(new Forecast(ForecastKeys.SH_Win1_Or_X, serial, jobForecast.MatchIdentifier, is99Percent));
                if (jobForecast.Is_FT_X_Or_Win2.IsAcceptable)
                    forecastList.Add(new Forecast(ForecastKeys.FT_X_Or_Win2, serial, jobForecast.MatchIdentifier, is99Percent));
                if (jobForecast.Is_HT_X_Or_Win2.IsAcceptable)
                    forecastList.Add(new Forecast(ForecastKeys.HT_X_Or_Win2, serial, jobForecast.MatchIdentifier, is99Percent));
                if (jobForecast.Is_SH_X_Or_Win2.IsAcceptable)
                    forecastList.Add(new Forecast(ForecastKeys.SH_X_Or_Win2, serial, jobForecast.MatchIdentifier, is99Percent));
                if (jobForecast.Is_FT_Win1_Or_Win2.IsAcceptable)
                    forecastList.Add(new Forecast(ForecastKeys.FT_Win1_Or_Win2, serial, jobForecast.MatchIdentifier, is99Percent));
                if (jobForecast.Is_HT_Win1_Or_Win2.IsAcceptable)
                    forecastList.Add(new Forecast(ForecastKeys.HT_Win1_Or_Win2, serial, jobForecast.MatchIdentifier, is99Percent));
                if (jobForecast.Is_SH_Win1_Or_Win2.IsAcceptable)
                    forecastList.Add(new Forecast(ForecastKeys.SH_Win1_Or_Win2, serial, jobForecast.MatchIdentifier, is99Percent));
            }

            return forecastList;
        }
    }
}
