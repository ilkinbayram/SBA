using Core.Entities.Concrete;
using Core.Entities.Concrete.System;
using Core.Entities.Dtos.SystemModels;
using Core.Extensions;
using Core.Resources.Enums;
using Core.Utilities.UsableModel;
using Core.Utilities.UsableModel.Visualisers;

namespace SBA.Business.Mapping
{
    public static class CustomMapperExtension
    {

        private static TimeZoneInfo azerbaycanZone = TimeZoneInfo.FindSystemTimeZoneById("Azerbaijan Standard Time");
        private static DateTime azerbaycanTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, azerbaycanZone);

        public static MatchBet MapToMatchBetFromMatchInfo(this MatchInfoContainer matchInfo)
        {
            if (!DateTime.TryParseExact(matchInfo.DateMatch, "dd.MM.yyyy",
        System.Globalization.CultureInfo.InvariantCulture,
        System.Globalization.DateTimeStyles.None, out DateTime _date) || string.IsNullOrEmpty(matchInfo.Serial) || matchInfo == null)
            {
                return null;
            }

            bool isNOTValid = string.IsNullOrEmpty(matchInfo.LeagueId) ||
                           string.IsNullOrEmpty(matchInfo.Home) ||
                           string.IsNullOrEmpty(matchInfo.Away) ||
                           string.IsNullOrEmpty(matchInfo.FT_Result) ||
                           string.IsNullOrEmpty(matchInfo.HT_Result) ||
                           matchInfo.FT_Result.Contains("P") ||
                           matchInfo.HT_Result.Contains("P");
            if (isNOTValid) return null;

            var result = new MatchBet
            {
                AwayTeam = matchInfo.Away,
                HomeTeam = matchInfo.Home,
                Country = matchInfo.Country,
                LeagueName = matchInfo.League,
                LeagueId = Convert.ToInt32(matchInfo.LeagueId),
                MatchDate = DateTime.ParseExact(matchInfo.DateMatch, "dd.MM.yyyy", null),
                SerialUniqueID = Convert.ToInt32(matchInfo.Serial),
                FTDraw_Odd = matchInfo.FT_X,
                FTWin1_Odd = matchInfo.FT_W1,
                FTWin2_Odd = matchInfo.FT_W2,
                FT_01_Odd = matchInfo.Goals01,
                FT_23_Odd = matchInfo.Goals23,
                FT_45_Odd = matchInfo.Goals45,
                FT_6_Odd = matchInfo.Goals6,
                FT_GG_Odd = matchInfo.GG,
                FT_NG_Odd = matchInfo.NG,
                FT_Match_Result = matchInfo.FT_Result,
                HT_Match_Result = matchInfo.HT_Result,
                FT_Over_1_5_Odd = matchInfo.FT_15_Over,
                FT_Over_2_5_Odd = matchInfo.FT_25_Over,
                FT_Over_3_5_Odd = matchInfo.FT_35_Over,
                FT_Under_1_5_Odd = matchInfo.FT_15_Under,
                FT_Under_2_5_Odd = matchInfo.FT_25_Under,
                FT_Under_3_5_Odd = matchInfo.FT_35_Under,
                HTDraw_Odd = matchInfo.HT_X,
                HTWin1_Odd = matchInfo.HT_W1,
                HTWin2_Odd = matchInfo.HT_W2,
                HT_Over_1_5_Odd = matchInfo.HT_15_Over,
                HT_Under_1_5_Odd = matchInfo.HT_15_Under,
                IsActive = true,
                ModelType = ProjectModelType.MatchBet
            };

            return result;
        }

        public static AnalyseResultVisualiser MapToDataVisualiserFromProfiler(this JobAnalyseModel profiler, int viewOid = 0)
        {
            AnalyseResultVisualiser result = null;
            switch (viewOid)
            {
                case 0:
                    result = new AnalyseResultVisualiser
                    {
                        HomeTeamVsAwayTeam = string.Format("{0} vs {1}", profiler.TeamPercentageProfiler.HomeTeam, profiler.TeamPercentageProfiler.AwayTeam),
                        TargetURL = profiler.TeamPercentageProfiler.TargetURL,
                        ODD_PERCENTAGE_Visualiser = new OddAnalyseVisualiser
                        {
                            AllCountFound = profiler.TeamPercentageProfiler.FT_Result.CountAll,
                            FT_1_5_Over = profiler.TeamPercentageProfiler.FT_1_5_Over.ToHtmlVisualPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            Home_HT_0_5_Over = profiler.TeamPercentageProfiler.Home_HT_0_5_Over.ToHtmlVisualPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            Home_HT_1_5_Over = profiler.TeamPercentageProfiler.Home_HT_1_5_Over.ToHtmlVisualPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            Home_SH_0_5_Over = profiler.TeamPercentageProfiler.Home_SH_0_5_Over.ToHtmlVisualPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            Home_SH_1_5_Over = profiler.TeamPercentageProfiler.Home_SH_1_5_Over.ToHtmlVisualPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            Home_FT_0_5_Over = profiler.TeamPercentageProfiler.Home_FT_0_5_Over.ToHtmlVisualPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            Home_FT_1_5_Over = profiler.TeamPercentageProfiler.Home_FT_1_5_Over.ToHtmlVisualPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            Home_Win_Any_Half = profiler.TeamPercentageProfiler.Home_Win_Any_Half.ToHtmlVisualPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),

                            Away_HT_0_5_Over = profiler.TeamPercentageProfiler.Away_HT_0_5_Over.ToHtmlVisualPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            Away_HT_1_5_Over = profiler.TeamPercentageProfiler.Away_HT_1_5_Over.ToHtmlVisualPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            Away_SH_0_5_Over = profiler.TeamPercentageProfiler.Away_SH_0_5_Over.ToHtmlVisualPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            Away_SH_1_5_Over = profiler.TeamPercentageProfiler.Away_SH_1_5_Over.ToHtmlVisualPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            Away_FT_0_5_Over = profiler.TeamPercentageProfiler.Away_FT_0_5_Over.ToHtmlVisualPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            Away_FT_1_5_Over = profiler.TeamPercentageProfiler.Away_FT_1_5_Over.ToHtmlVisualPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            Away_Win_Any_Half = profiler.TeamPercentageProfiler.Away_Win_Any_Half.ToHtmlVisualPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),

                            FT_2_5_Over = profiler.TeamPercentageProfiler.FT_2_5_Over.ToHtmlVisualPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            FT_3_5_Over = profiler.TeamPercentageProfiler.FT_3_5_Over.ToHtmlVisualPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            FT_GG = profiler.TeamPercentageProfiler.FT_GG.ToHtmlVisualPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            FT_Result = profiler.TeamPercentageProfiler.FT_Result.ToHtmlVisualPercentage((int)StaticPercentageDefinerEnum.ThreeWayStandard),
                            FT_TotalBetween = profiler.TeamPercentageProfiler.FT_TotalBetween.ToHtmlVisualPercentage((int)StaticPercentageDefinerEnum.FourWayStandard),
                            HT_0_5_Over = profiler.TeamPercentageProfiler.HT_0_5_Over.ToHtmlVisualPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            HT_1_5_Over = profiler.TeamPercentageProfiler.HT_1_5_Over.ToHtmlVisualPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            HT_FT_Result = profiler.TeamPercentageProfiler.HT_FT_Result.ToHtmlVisualPercentage((int)StaticPercentageDefinerEnum.NineWayStandard),
                            HT_GG = profiler.TeamPercentageProfiler.HT_GG.ToHtmlVisualPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            HT_Result = profiler.TeamPercentageProfiler.HT_Result.ToHtmlVisualPercentage((int)StaticPercentageDefinerEnum.ThreeWayStandard),
                            MoreGoalsBetweenTimes = profiler.TeamPercentageProfiler.MoreGoalsBetweenTimes.ToHtmlVisualPercentage((int)StaticPercentageDefinerEnum.ThreeWayStandard),
                            SH_0_5_Over = profiler.TeamPercentageProfiler.SH_0_5_Over.ToHtmlVisualPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            SH_1_5_Over = profiler.TeamPercentageProfiler.SH_1_5_Over.ToHtmlVisualPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            SH_GG = profiler.TeamPercentageProfiler.SH_GG.ToHtmlVisualPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            SH_Result = profiler.TeamPercentageProfiler.SH_Result.ToHtmlVisualPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard)
                        },
                        AVERAGE_Visualiser = new AverageVisualiser
                        {
                            FT_Result = profiler.AverageProfiler.FT_Result.ToHtmlVisualPercentage(),
                            HT_Result = profiler.AverageProfiler.HT_Result.ToHtmlVisualPercentage(),
                            SH_Result = profiler.AverageProfiler.SH_Result.ToHtmlVisualPercentage(),

                            HT_05_Over_Home = profiler.AverageProfiler.HT_05_Over_Home.ToHtmlVisualPercentage(),
                            HT_15_Over_Home = profiler.AverageProfiler.HT_15_Over_Home.ToHtmlVisualPercentage(),
                            HT_05_Over_Away = profiler.AverageProfiler.HT_05_Over_Away.ToHtmlVisualPercentage(),
                            HT_15_Over_Away = profiler.AverageProfiler.HT_15_Over_Away.ToHtmlVisualPercentage(),
                            Home_HT_05_Over = profiler.AverageProfiler.Home_HT_05_Over.ToHtmlVisualPercentage(),
                            Home_HT_15_Over = profiler.AverageProfiler.Home_HT_15_Over.ToHtmlVisualPercentage(),
                            Away_HT_05_Over = profiler.AverageProfiler.Away_HT_05_Over.ToHtmlVisualPercentage(),
                            Away_HT_15_Over = profiler.AverageProfiler.Away_HT_15_Over.ToHtmlVisualPercentage(),

                            SH_05_Over_Home = profiler.AverageProfiler.SH_05_Over_Home.ToHtmlVisualPercentage(),
                            SH_15_Over_Home = profiler.AverageProfiler.SH_15_Over_Home.ToHtmlVisualPercentage(),
                            SH_05_Over_Away = profiler.AverageProfiler.SH_05_Over_Away.ToHtmlVisualPercentage(),
                            SH_15_Over_Away = profiler.AverageProfiler.SH_15_Over_Away.ToHtmlVisualPercentage(),
                            Home_SH_05_Over = profiler.AverageProfiler.Home_SH_05_Over.ToHtmlVisualPercentage(),
                            Home_SH_15_Over = profiler.AverageProfiler.Home_SH_15_Over.ToHtmlVisualPercentage(),
                            Away_SH_05_Over = profiler.AverageProfiler.Away_SH_05_Over.ToHtmlVisualPercentage(),
                            Away_SH_15_Over = profiler.AverageProfiler.Away_SH_15_Over.ToHtmlVisualPercentage(),

                            FT_15_Over_Home = profiler.AverageProfiler.FT_15_Over_Home.ToHtmlVisualPercentage(),
                            FT_25_Over_Home = profiler.AverageProfiler.FT_25_Over_Home.ToHtmlVisualPercentage(),
                            FT_35_Over_Home = profiler.AverageProfiler.FT_35_Over_Home.ToHtmlVisualPercentage(),
                            FT_15_Over_Away = profiler.AverageProfiler.FT_15_Over_Away.ToHtmlVisualPercentage(),
                            FT_25_Over_Away = profiler.AverageProfiler.FT_25_Over_Away.ToHtmlVisualPercentage(),
                            FT_35_Over_Away = profiler.AverageProfiler.FT_35_Over_Away.ToHtmlVisualPercentage(),
                            Home_FT_05_Over = profiler.AverageProfiler.Home_FT_05_Over.ToHtmlVisualPercentage(),
                            Home_FT_15_Over = profiler.AverageProfiler.Home_FT_15_Over.ToHtmlVisualPercentage(),
                            Away_FT_05_Over = profiler.AverageProfiler.Away_FT_05_Over.ToHtmlVisualPercentage(),
                            Away_FT_15_Over = profiler.AverageProfiler.Away_FT_15_Over.ToHtmlVisualPercentage(),

                            HT_GG_Home = profiler.AverageProfiler.HT_GG_Home.ToHtmlVisualPercentage(),
                            SH_GG_Home = profiler.AverageProfiler.SH_GG_Home.ToHtmlVisualPercentage(),
                            FT_GG_Home = profiler.AverageProfiler.FT_GG_Home.ToHtmlVisualPercentage(),
                            HT_GG_Away = profiler.AverageProfiler.HT_GG_Away.ToHtmlVisualPercentage(),
                            SH_GG_Away = profiler.AverageProfiler.SH_GG_Away.ToHtmlVisualPercentage(),
                            FT_GG_Away = profiler.AverageProfiler.FT_GG_Away.ToHtmlVisualPercentage(),

                            Home_Win_Any_Half = profiler.AverageProfiler.Home_Win_Any_Half.ToHtmlVisualPercentage(),
                            Away_Win_Any_Half = profiler.AverageProfiler.Away_Win_Any_Half.ToHtmlVisualPercentage(),

                            HT_FT_Result = profiler.AverageProfiler.HT_FT_Result.ToHtmlVisualPercentage(),
                            MoreGoalsBetweenTimes = profiler.AverageProfiler.MoreGoalsBetweenTimes.ToHtmlVisualPercentage(),
                            Total_BetweenGoals = profiler.AverageProfiler.Total_BetweenGoals.ToHtmlVisualPercentage(),

                            Average_HT_Goals_HomeTeam = profiler.AverageProfiler.Average_HT_Goals_HomeTeam.ToDecimalHtmlVisual(),
                            Average_HT_Goals_AwayTeam = profiler.AverageProfiler.Average_HT_Goals_AwayTeam.ToDecimalHtmlVisual(),
                            Average_SH_Goals_HomeTeam = profiler.AverageProfiler.Average_SH_Goals_HomeTeam.ToDecimalHtmlVisual(),
                            Average_SH_Goals_AwayTeam = profiler.AverageProfiler.Average_SH_Goals_AwayTeam.ToDecimalHtmlVisual(),
                            Average_FT_Goals_HomeTeam = profiler.AverageProfiler.Average_FT_Goals_HomeTeam.ToDecimalHtmlVisual(),
                            Average_FT_Goals_AwayTeam = profiler.AverageProfiler.Average_FT_Goals_AwayTeam.ToDecimalHtmlVisual()
                        },
                        COMPARISON_Visualiser = new ComparisonVisualiser
                        {
                            BY_SIDE = profiler.ComparisonInfoContainer != null ? new SideVisualiserModel
                            {
                                CountFound = profiler.ComparisonInfoContainer.HomeAway.CountFound.ToIntHtmlVisual(),
                                FT_Result = profiler.ComparisonInfoContainer.HomeAway.FT_Result.ToHtmlVisualPercentage(),
                                HT_Result = profiler.ComparisonInfoContainer.HomeAway.HT_Result.ToHtmlVisualPercentage(),
                                SH_Result = profiler.ComparisonInfoContainer.HomeAway.SH_Result.ToHtmlVisualPercentage(),

                                HT_05_Over = profiler.ComparisonInfoContainer.HomeAway.HT_05_Over.ToHtmlVisualPercentage(),
                                HT_15_Over = profiler.ComparisonInfoContainer.HomeAway.HT_15_Over.ToHtmlVisualPercentage(),
                                Home_HT_05_Over = profiler.ComparisonInfoContainer.HomeAway.Home_HT_05_Over.ToHtmlVisualPercentage(),
                                Home_HT_15_Over = profiler.ComparisonInfoContainer.HomeAway.Home_HT_15_Over.ToHtmlVisualPercentage(),
                                Away_HT_05_Over = profiler.ComparisonInfoContainer.HomeAway.Away_HT_05_Over.ToHtmlVisualPercentage(),
                                Away_HT_15_Over = profiler.ComparisonInfoContainer.HomeAway.Away_HT_15_Over.ToHtmlVisualPercentage(),

                                SH_05_Over = profiler.ComparisonInfoContainer.HomeAway.SH_05_Over.ToHtmlVisualPercentage(),
                                SH_15_Over = profiler.ComparisonInfoContainer.HomeAway.SH_15_Over.ToHtmlVisualPercentage(),
                                Home_SH_05_Over = profiler.ComparisonInfoContainer.HomeAway.Home_SH_05_Over.ToHtmlVisualPercentage(),
                                Home_SH_15_Over = profiler.ComparisonInfoContainer.HomeAway.Home_SH_15_Over.ToHtmlVisualPercentage(),
                                Away_SH_05_Over = profiler.ComparisonInfoContainer.HomeAway.Away_SH_05_Over.ToHtmlVisualPercentage(),
                                Away_SH_15_Over = profiler.ComparisonInfoContainer.HomeAway.Away_SH_15_Over.ToHtmlVisualPercentage(),

                                FT_15_Over = profiler.ComparisonInfoContainer.HomeAway.FT_15_Over.ToHtmlVisualPercentage(),
                                FT_25_Over = profiler.ComparisonInfoContainer.HomeAway.FT_25_Over.ToHtmlVisualPercentage(),
                                FT_35_Over = profiler.ComparisonInfoContainer.HomeAway.FT_35_Over.ToHtmlVisualPercentage(),
                                Home_FT_05_Over = profiler.ComparisonInfoContainer.HomeAway.Home_FT_05_Over.ToHtmlVisualPercentage(),
                                Home_FT_15_Over = profiler.ComparisonInfoContainer.HomeAway.Home_FT_15_Over.ToHtmlVisualPercentage(),
                                Away_FT_05_Over = profiler.ComparisonInfoContainer.HomeAway.Away_FT_05_Over.ToHtmlVisualPercentage(),
                                Away_FT_15_Over = profiler.ComparisonInfoContainer.HomeAway.Away_FT_15_Over.ToHtmlVisualPercentage(),

                                HT_GG = profiler.ComparisonInfoContainer.HomeAway.HT_GG.ToHtmlVisualPercentage(),
                                SH_GG = profiler.ComparisonInfoContainer.HomeAway.SH_GG.ToHtmlVisualPercentage(),
                                FT_GG = profiler.ComparisonInfoContainer.HomeAway.FT_GG.ToHtmlVisualPercentage(),

                                Home_Win_Any_Half = profiler.ComparisonInfoContainer.HomeAway.Home_Win_Any_Half.ToHtmlVisualPercentage(),
                                Away_Win_Any_Half = profiler.ComparisonInfoContainer.HomeAway.Away_Win_Any_Half.ToHtmlVisualPercentage(),

                                HT_FT_Result = profiler.ComparisonInfoContainer.HomeAway.HT_FT_Result.ToHtmlVisualPercentage(),
                                MoreGoalsBetweenTimes = profiler.ComparisonInfoContainer.HomeAway.MoreGoalsBetweenTimes.ToHtmlVisualPercentage(),
                                Total_BetweenGoals = profiler.ComparisonInfoContainer.HomeAway.Total_BetweenGoals.ToHtmlVisualPercentage(),

                                Average_HT_Goals_HomeTeam = profiler.ComparisonInfoContainer.HomeAway.Average_HT_Goals_HomeTeam.ToDecimalHtmlVisual(),
                                Average_HT_Goals_AwayTeam = profiler.ComparisonInfoContainer.HomeAway.Average_HT_Goals_AwayTeam.ToDecimalHtmlVisual(),
                                Average_SH_Goals_HomeTeam = profiler.ComparisonInfoContainer.HomeAway.Average_SH_Goals_HomeTeam.ToDecimalHtmlVisual(),
                                Average_SH_Goals_AwayTeam = profiler.ComparisonInfoContainer.HomeAway.Average_SH_Goals_AwayTeam.ToDecimalHtmlVisual(),
                                Average_FT_Goals_HomeTeam = profiler.ComparisonInfoContainer.HomeAway.Average_FT_Goals_HomeTeam.ToDecimalHtmlVisual(),
                                Average_FT_Goals_AwayTeam = profiler.ComparisonInfoContainer.HomeAway.Average_FT_Goals_AwayTeam.ToDecimalHtmlVisual()
                            } : null,
                            GENERAL = profiler.ComparisonInfoContainer != null ? new SideVisualiserModel
                            {
                                CountFound = profiler.ComparisonInfoContainer.General.CountFound.ToIntHtmlVisual(),
                                FT_Result = profiler.ComparisonInfoContainer.General.FT_Result.ToHtmlVisualPercentage(),
                                HT_Result = profiler.ComparisonInfoContainer.General.HT_Result.ToHtmlVisualPercentage(),
                                SH_Result = profiler.ComparisonInfoContainer.General.SH_Result.ToHtmlVisualPercentage(),

                                HT_05_Over = profiler.ComparisonInfoContainer.General.HT_05_Over.ToHtmlVisualPercentage(),
                                HT_15_Over = profiler.ComparisonInfoContainer.General.HT_15_Over.ToHtmlVisualPercentage(),
                                Home_HT_05_Over = profiler.ComparisonInfoContainer.General.Home_HT_05_Over.ToHtmlVisualPercentage(),
                                Home_HT_15_Over = profiler.ComparisonInfoContainer.General.Home_HT_15_Over.ToHtmlVisualPercentage(),
                                Away_HT_05_Over = profiler.ComparisonInfoContainer.General.Away_HT_05_Over.ToHtmlVisualPercentage(),
                                Away_HT_15_Over = profiler.ComparisonInfoContainer.General.Away_HT_15_Over.ToHtmlVisualPercentage(),

                                SH_05_Over = profiler.ComparisonInfoContainer.General.SH_05_Over.ToHtmlVisualPercentage(),
                                SH_15_Over = profiler.ComparisonInfoContainer.General.SH_15_Over.ToHtmlVisualPercentage(),
                                Home_SH_05_Over = profiler.ComparisonInfoContainer.General.Home_SH_05_Over.ToHtmlVisualPercentage(),
                                Home_SH_15_Over = profiler.ComparisonInfoContainer.General.Home_SH_15_Over.ToHtmlVisualPercentage(),
                                Away_SH_05_Over = profiler.ComparisonInfoContainer.General.Away_SH_05_Over.ToHtmlVisualPercentage(),
                                Away_SH_15_Over = profiler.ComparisonInfoContainer.General.Away_SH_15_Over.ToHtmlVisualPercentage(),

                                FT_15_Over = profiler.ComparisonInfoContainer.General.FT_15_Over.ToHtmlVisualPercentage(),
                                FT_25_Over = profiler.ComparisonInfoContainer.General.FT_25_Over.ToHtmlVisualPercentage(),
                                FT_35_Over = profiler.ComparisonInfoContainer.General.FT_35_Over.ToHtmlVisualPercentage(),
                                Home_FT_05_Over = profiler.ComparisonInfoContainer.General.Home_FT_05_Over.ToHtmlVisualPercentage(),
                                Home_FT_15_Over = profiler.ComparisonInfoContainer.General.Home_FT_15_Over.ToHtmlVisualPercentage(),
                                Away_FT_05_Over = profiler.ComparisonInfoContainer.General.Away_FT_05_Over.ToHtmlVisualPercentage(),
                                Away_FT_15_Over = profiler.ComparisonInfoContainer.General.Away_FT_15_Over.ToHtmlVisualPercentage(),

                                HT_GG = profiler.ComparisonInfoContainer.General.HT_GG.ToHtmlVisualPercentage(),
                                SH_GG = profiler.ComparisonInfoContainer.General.SH_GG.ToHtmlVisualPercentage(),
                                FT_GG = profiler.ComparisonInfoContainer.General.FT_GG.ToHtmlVisualPercentage(),

                                Home_Win_Any_Half = profiler.ComparisonInfoContainer.General.Home_Win_Any_Half.ToHtmlVisualPercentage(),
                                Away_Win_Any_Half = profiler.ComparisonInfoContainer.General.Away_Win_Any_Half.ToHtmlVisualPercentage(),

                                HT_FT_Result = profiler.ComparisonInfoContainer.General.HT_FT_Result.ToHtmlVisualPercentage(),
                                MoreGoalsBetweenTimes = profiler.ComparisonInfoContainer.General.MoreGoalsBetweenTimes.ToHtmlVisualPercentage(),
                                Total_BetweenGoals = profiler.ComparisonInfoContainer.General.Total_BetweenGoals.ToHtmlVisualPercentage(),

                                Average_HT_Goals_HomeTeam = profiler.ComparisonInfoContainer.General.Average_HT_Goals_HomeTeam.ToDecimalHtmlVisual(),
                                Average_HT_Goals_AwayTeam = profiler.ComparisonInfoContainer.General.Average_HT_Goals_AwayTeam.ToDecimalHtmlVisual(),
                                Average_SH_Goals_HomeTeam = profiler.ComparisonInfoContainer.General.Average_SH_Goals_HomeTeam.ToDecimalHtmlVisual(),
                                Average_SH_Goals_AwayTeam = profiler.ComparisonInfoContainer.General.Average_SH_Goals_AwayTeam.ToDecimalHtmlVisual(),
                                Average_FT_Goals_HomeTeam = profiler.ComparisonInfoContainer.General.Average_FT_Goals_HomeTeam.ToDecimalHtmlVisual(),
                                Average_FT_Goals_AwayTeam = profiler.ComparisonInfoContainer.General.Average_FT_Goals_AwayTeam.ToDecimalHtmlVisual()
                            } : null
                        },
                        PERFORMANCE_HOME_Visualiser = new FormHomePerformanceVisualiser
                        {
                            AT_HOME_PERFORMANCE = profiler.HomeTeam_FormPerformanceGuessContainer != null ? new HomeSideVisualiserModel
                            {
                                CountFound = profiler.HomeTeam_FormPerformanceGuessContainer.HomeAway.CountFound.ToIntHtmlVisual(),
                                FT_Result = profiler.HomeTeam_FormPerformanceGuessContainer.HomeAway.FT_Result.ToHtmlVisualPercentage(),
                                HT_Result = profiler.HomeTeam_FormPerformanceGuessContainer.HomeAway.HT_Result.ToHtmlVisualPercentage(),
                                SH_Result = profiler.HomeTeam_FormPerformanceGuessContainer.HomeAway.SH_Result.ToHtmlVisualPercentage(),

                                HT_05_Over = profiler.HomeTeam_FormPerformanceGuessContainer.HomeAway.HT_05_Over.ToHtmlVisualPercentage(),
                                HT_15_Over = profiler.HomeTeam_FormPerformanceGuessContainer.HomeAway.HT_15_Over.ToHtmlVisualPercentage(),
                                Home_HT_05_Over = profiler.HomeTeam_FormPerformanceGuessContainer.HomeAway.Home_HT_05_Over.ToHtmlVisualPercentage(),
                                Home_HT_15_Over = profiler.HomeTeam_FormPerformanceGuessContainer.HomeAway.Home_HT_15_Over.ToHtmlVisualPercentage(),

                                SH_05_Over = profiler.HomeTeam_FormPerformanceGuessContainer.HomeAway.SH_05_Over.ToHtmlVisualPercentage(),
                                SH_15_Over = profiler.HomeTeam_FormPerformanceGuessContainer.HomeAway.SH_15_Over.ToHtmlVisualPercentage(),
                                Home_SH_05_Over = profiler.HomeTeam_FormPerformanceGuessContainer.HomeAway.Home_SH_05_Over.ToHtmlVisualPercentage(),
                                Home_SH_15_Over = profiler.HomeTeam_FormPerformanceGuessContainer.HomeAway.Home_SH_15_Over.ToHtmlVisualPercentage(),

                                FT_15_Over = profiler.HomeTeam_FormPerformanceGuessContainer.HomeAway.FT_15_Over.ToHtmlVisualPercentage(),
                                FT_25_Over = profiler.HomeTeam_FormPerformanceGuessContainer.HomeAway.FT_25_Over.ToHtmlVisualPercentage(),
                                FT_35_Over = profiler.HomeTeam_FormPerformanceGuessContainer.HomeAway.FT_35_Over.ToHtmlVisualPercentage(),
                                Home_FT_05_Over = profiler.HomeTeam_FormPerformanceGuessContainer.HomeAway.Home_FT_05_Over.ToHtmlVisualPercentage(),
                                Home_FT_15_Over = profiler.HomeTeam_FormPerformanceGuessContainer.HomeAway.Home_FT_15_Over.ToHtmlVisualPercentage(),

                                HT_GG = profiler.HomeTeam_FormPerformanceGuessContainer.HomeAway.HT_GG.ToHtmlVisualPercentage(),
                                SH_GG = profiler.HomeTeam_FormPerformanceGuessContainer.HomeAway.SH_GG.ToHtmlVisualPercentage(),
                                FT_GG = profiler.HomeTeam_FormPerformanceGuessContainer.HomeAway.FT_GG.ToHtmlVisualPercentage(),

                                Home_Win_Any_Half = profiler.HomeTeam_FormPerformanceGuessContainer.HomeAway.Home_Win_Any_Half.ToHtmlVisualPercentage(),

                                HT_FT_Result = profiler.HomeTeam_FormPerformanceGuessContainer.HomeAway.HT_FT_Result.ToHtmlVisualPercentage(),
                                MoreGoalsBetweenTimes = profiler.HomeTeam_FormPerformanceGuessContainer.HomeAway.MoreGoalsBetweenTimes.ToHtmlVisualPercentage(),
                                Total_BetweenGoals = profiler.HomeTeam_FormPerformanceGuessContainer.HomeAway.Total_BetweenGoals.ToHtmlVisualPercentage(),

                                Average_HT_Goals_HomeTeam = profiler.HomeTeam_FormPerformanceGuessContainer.HomeAway.Average_HT_Goals_HomeTeam.ToDecimalHtmlVisual(),
                                Average_SH_Goals_HomeTeam = profiler.HomeTeam_FormPerformanceGuessContainer.HomeAway.Average_SH_Goals_HomeTeam.ToDecimalHtmlVisual(),
                                Average_FT_Goals_HomeTeam = profiler.HomeTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_Goals_HomeTeam.ToDecimalHtmlVisual(),
                            } : null,
                            ALL_PERFORMANCE = profiler.HomeTeam_FormPerformanceGuessContainer != null ? new HomeSideVisualiserModel
                            {
                                CountFound = profiler.HomeTeam_FormPerformanceGuessContainer.General.CountFound.ToIntHtmlVisual(),
                                FT_Result = profiler.HomeTeam_FormPerformanceGuessContainer.General.FT_Result.ToHtmlVisualPercentage(),
                                HT_Result = profiler.HomeTeam_FormPerformanceGuessContainer.General.HT_Result.ToHtmlVisualPercentage(),
                                SH_Result = profiler.HomeTeam_FormPerformanceGuessContainer.General.SH_Result.ToHtmlVisualPercentage(),

                                HT_05_Over = profiler.HomeTeam_FormPerformanceGuessContainer.General.HT_05_Over.ToHtmlVisualPercentage(),
                                HT_15_Over = profiler.HomeTeam_FormPerformanceGuessContainer.General.HT_15_Over.ToHtmlVisualPercentage(),
                                Home_HT_05_Over = profiler.HomeTeam_FormPerformanceGuessContainer.General.Home_HT_05_Over.ToHtmlVisualPercentage(),
                                Home_HT_15_Over = profiler.HomeTeam_FormPerformanceGuessContainer.General.Home_HT_15_Over.ToHtmlVisualPercentage(),

                                SH_05_Over = profiler.HomeTeam_FormPerformanceGuessContainer.General.SH_05_Over.ToHtmlVisualPercentage(),
                                SH_15_Over = profiler.HomeTeam_FormPerformanceGuessContainer.General.SH_15_Over.ToHtmlVisualPercentage(),
                                Home_SH_05_Over = profiler.HomeTeam_FormPerformanceGuessContainer.General.Home_SH_05_Over.ToHtmlVisualPercentage(),
                                Home_SH_15_Over = profiler.HomeTeam_FormPerformanceGuessContainer.General.Home_SH_15_Over.ToHtmlVisualPercentage(),

                                FT_15_Over = profiler.HomeTeam_FormPerformanceGuessContainer.General.FT_15_Over.ToHtmlVisualPercentage(),
                                FT_25_Over = profiler.HomeTeam_FormPerformanceGuessContainer.General.FT_25_Over.ToHtmlVisualPercentage(),
                                FT_35_Over = profiler.HomeTeam_FormPerformanceGuessContainer.General.FT_35_Over.ToHtmlVisualPercentage(),
                                Home_FT_05_Over = profiler.HomeTeam_FormPerformanceGuessContainer.General.Home_FT_05_Over.ToHtmlVisualPercentage(),
                                Home_FT_15_Over = profiler.HomeTeam_FormPerformanceGuessContainer.General.Home_FT_15_Over.ToHtmlVisualPercentage(),

                                HT_GG = profiler.HomeTeam_FormPerformanceGuessContainer.General.HT_GG.ToHtmlVisualPercentage(),
                                SH_GG = profiler.HomeTeam_FormPerformanceGuessContainer.General.SH_GG.ToHtmlVisualPercentage(),
                                FT_GG = profiler.HomeTeam_FormPerformanceGuessContainer.General.FT_GG.ToHtmlVisualPercentage(),

                                Home_Win_Any_Half = profiler.HomeTeam_FormPerformanceGuessContainer.General.Home_Win_Any_Half.ToHtmlVisualPercentage(),

                                HT_FT_Result = profiler.HomeTeam_FormPerformanceGuessContainer.General.HT_FT_Result.ToHtmlVisualPercentage(),
                                MoreGoalsBetweenTimes = profiler.HomeTeam_FormPerformanceGuessContainer.General.MoreGoalsBetweenTimes.ToHtmlVisualPercentage(),
                                Total_BetweenGoals = profiler.HomeTeam_FormPerformanceGuessContainer.General.Total_BetweenGoals.ToHtmlVisualPercentage(),

                                Average_HT_Goals_HomeTeam = profiler.HomeTeam_FormPerformanceGuessContainer.General.Average_HT_Goals_HomeTeam.ToDecimalHtmlVisual(),
                                Average_SH_Goals_HomeTeam = profiler.HomeTeam_FormPerformanceGuessContainer.General.Average_SH_Goals_HomeTeam.ToDecimalHtmlVisual(),
                                Average_FT_Goals_HomeTeam = profiler.HomeTeam_FormPerformanceGuessContainer.General.Average_FT_Goals_HomeTeam.ToDecimalHtmlVisual(),
                            } : null
                        },
                        PERFORMANCE_AWAY_Visualiser = new FormAwayPerformanceVisualiser
                        {
                            AT_AWAY_PERFORMANCE = profiler.AwayTeam_FormPerformanceGuessContainer != null ? new AwaySideVisualiserModel
                            {
                                CountFound = profiler.AwayTeam_FormPerformanceGuessContainer.HomeAway.CountFound.ToIntHtmlVisual(),
                                FT_Result = profiler.AwayTeam_FormPerformanceGuessContainer.HomeAway.FT_Result.ToHtmlVisualPercentage(),
                                HT_Result = profiler.AwayTeam_FormPerformanceGuessContainer.HomeAway.HT_Result.ToHtmlVisualPercentage(),
                                SH_Result = profiler.AwayTeam_FormPerformanceGuessContainer.HomeAway.SH_Result.ToHtmlVisualPercentage(),

                                HT_05_Over = profiler.AwayTeam_FormPerformanceGuessContainer.HomeAway.HT_05_Over.ToHtmlVisualPercentage(),
                                HT_15_Over = profiler.AwayTeam_FormPerformanceGuessContainer.HomeAway.HT_15_Over.ToHtmlVisualPercentage(),
                                Away_HT_05_Over = profiler.AwayTeam_FormPerformanceGuessContainer.HomeAway.Away_HT_05_Over.ToHtmlVisualPercentage(),
                                Away_HT_15_Over = profiler.AwayTeam_FormPerformanceGuessContainer.HomeAway.Away_HT_15_Over.ToHtmlVisualPercentage(),

                                SH_05_Over = profiler.AwayTeam_FormPerformanceGuessContainer.HomeAway.SH_05_Over.ToHtmlVisualPercentage(),
                                SH_15_Over = profiler.AwayTeam_FormPerformanceGuessContainer.HomeAway.SH_15_Over.ToHtmlVisualPercentage(),
                                Away_SH_05_Over = profiler.AwayTeam_FormPerformanceGuessContainer.HomeAway.Away_SH_05_Over.ToHtmlVisualPercentage(),
                                Away_SH_15_Over = profiler.AwayTeam_FormPerformanceGuessContainer.HomeAway.Away_SH_15_Over.ToHtmlVisualPercentage(),

                                FT_15_Over = profiler.AwayTeam_FormPerformanceGuessContainer.HomeAway.FT_15_Over.ToHtmlVisualPercentage(),
                                FT_25_Over = profiler.AwayTeam_FormPerformanceGuessContainer.HomeAway.FT_25_Over.ToHtmlVisualPercentage(),
                                FT_35_Over = profiler.AwayTeam_FormPerformanceGuessContainer.HomeAway.FT_35_Over.ToHtmlVisualPercentage(),
                                Away_FT_05_Over = profiler.AwayTeam_FormPerformanceGuessContainer.HomeAway.Away_FT_05_Over.ToHtmlVisualPercentage(),
                                Away_FT_15_Over = profiler.AwayTeam_FormPerformanceGuessContainer.HomeAway.Away_FT_15_Over.ToHtmlVisualPercentage(),

                                HT_GG = profiler.AwayTeam_FormPerformanceGuessContainer.HomeAway.HT_GG.ToHtmlVisualPercentage(),
                                SH_GG = profiler.AwayTeam_FormPerformanceGuessContainer.HomeAway.SH_GG.ToHtmlVisualPercentage(),
                                FT_GG = profiler.AwayTeam_FormPerformanceGuessContainer.HomeAway.FT_GG.ToHtmlVisualPercentage(),

                                Away_Win_Any_Half = profiler.AwayTeam_FormPerformanceGuessContainer.HomeAway.Away_Win_Any_Half.ToHtmlVisualPercentage(),

                                HT_FT_Result = profiler.AwayTeam_FormPerformanceGuessContainer.HomeAway.HT_FT_Result.ToHtmlVisualPercentage(),
                                MoreGoalsBetweenTimes = profiler.AwayTeam_FormPerformanceGuessContainer.HomeAway.MoreGoalsBetweenTimes.ToHtmlVisualPercentage(),
                                Total_BetweenGoals = profiler.AwayTeam_FormPerformanceGuessContainer.HomeAway.Total_BetweenGoals.ToHtmlVisualPercentage(),

                                Average_HT_Goals_AwayTeam = profiler.AwayTeam_FormPerformanceGuessContainer.HomeAway.Average_HT_Goals_AwayTeam.ToDecimalHtmlVisual(),
                                Average_SH_Goals_AwayTeam = profiler.AwayTeam_FormPerformanceGuessContainer.HomeAway.Average_SH_Goals_AwayTeam.ToDecimalHtmlVisual(),
                                Average_FT_Goals_AwayTeam = profiler.AwayTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_Goals_AwayTeam.ToDecimalHtmlVisual()
                            } : null,
                            ALL_PERFORMANCE = profiler.AwayTeam_FormPerformanceGuessContainer != null ? new AwaySideVisualiserModel
                            {
                                CountFound = profiler.AwayTeam_FormPerformanceGuessContainer.General.CountFound.ToIntHtmlVisual(),
                                FT_Result = profiler.AwayTeam_FormPerformanceGuessContainer.General.FT_Result.ToHtmlVisualPercentage(),
                                HT_Result = profiler.AwayTeam_FormPerformanceGuessContainer.General.HT_Result.ToHtmlVisualPercentage(),
                                SH_Result = profiler.AwayTeam_FormPerformanceGuessContainer.General.SH_Result.ToHtmlVisualPercentage(),

                                HT_05_Over = profiler.AwayTeam_FormPerformanceGuessContainer.General.HT_05_Over.ToHtmlVisualPercentage(),
                                HT_15_Over = profiler.AwayTeam_FormPerformanceGuessContainer.General.HT_15_Over.ToHtmlVisualPercentage(),
                                Away_HT_05_Over = profiler.AwayTeam_FormPerformanceGuessContainer.General.Away_HT_05_Over.ToHtmlVisualPercentage(),
                                Away_HT_15_Over = profiler.AwayTeam_FormPerformanceGuessContainer.General.Away_HT_15_Over.ToHtmlVisualPercentage(),

                                SH_05_Over = profiler.AwayTeam_FormPerformanceGuessContainer.General.SH_05_Over.ToHtmlVisualPercentage(),
                                SH_15_Over = profiler.AwayTeam_FormPerformanceGuessContainer.General.SH_15_Over.ToHtmlVisualPercentage(),
                                Away_SH_05_Over = profiler.AwayTeam_FormPerformanceGuessContainer.General.Away_SH_05_Over.ToHtmlVisualPercentage(),
                                Away_SH_15_Over = profiler.AwayTeam_FormPerformanceGuessContainer.General.Away_SH_15_Over.ToHtmlVisualPercentage(),

                                FT_15_Over = profiler.AwayTeam_FormPerformanceGuessContainer.General.FT_15_Over.ToHtmlVisualPercentage(),
                                FT_25_Over = profiler.AwayTeam_FormPerformanceGuessContainer.General.FT_25_Over.ToHtmlVisualPercentage(),
                                FT_35_Over = profiler.AwayTeam_FormPerformanceGuessContainer.General.FT_35_Over.ToHtmlVisualPercentage(),
                                Away_FT_05_Over = profiler.AwayTeam_FormPerformanceGuessContainer.General.Away_FT_05_Over.ToHtmlVisualPercentage(),
                                Away_FT_15_Over = profiler.AwayTeam_FormPerformanceGuessContainer.General.Away_FT_15_Over.ToHtmlVisualPercentage(),

                                HT_GG = profiler.AwayTeam_FormPerformanceGuessContainer.General.HT_GG.ToHtmlVisualPercentage(),
                                SH_GG = profiler.AwayTeam_FormPerformanceGuessContainer.General.SH_GG.ToHtmlVisualPercentage(),
                                FT_GG = profiler.AwayTeam_FormPerformanceGuessContainer.General.FT_GG.ToHtmlVisualPercentage(),

                                Away_Win_Any_Half = profiler.AwayTeam_FormPerformanceGuessContainer.General.Away_Win_Any_Half.ToHtmlVisualPercentage(),

                                HT_FT_Result = profiler.AwayTeam_FormPerformanceGuessContainer.General.HT_FT_Result.ToHtmlVisualPercentage(),
                                MoreGoalsBetweenTimes = profiler.AwayTeam_FormPerformanceGuessContainer.General.MoreGoalsBetweenTimes.ToHtmlVisualPercentage(),
                                Total_BetweenGoals = profiler.AwayTeam_FormPerformanceGuessContainer.General.Total_BetweenGoals.ToHtmlVisualPercentage(),

                                Average_HT_Goals_AwayTeam = profiler.AwayTeam_FormPerformanceGuessContainer.General.Average_HT_Goals_AwayTeam.ToDecimalHtmlVisual(),
                                Average_SH_Goals_AwayTeam = profiler.AwayTeam_FormPerformanceGuessContainer.General.Average_SH_Goals_AwayTeam.ToDecimalHtmlVisual(),
                                Average_FT_Goals_AwayTeam = profiler.AwayTeam_FormPerformanceGuessContainer.General.Average_FT_Goals_AwayTeam.ToDecimalHtmlVisual()
                            } : null
                        },
                        TABLE_Visualiser = new TableStandingVisualiser
                        {
                            UpTeam = profiler.StandingInfoModel != null ? new TableStandingDetailVisualiser
                            {
                                TeamName = profiler.StandingInfoModel.UpTeam.TeamName.ToStringHtmlVisual(),
                                Order = profiler.StandingInfoModel.UpTeam.Order.ToIntHtmlVisual(),
                                MatchesCount = profiler.StandingInfoModel.UpTeam.MatchesCount.ToIntHtmlVisual(),
                                WinsCount = profiler.StandingInfoModel.UpTeam.WinsCount.ToIntHtmlVisual(),
                                DrawsCount = profiler.StandingInfoModel.UpTeam.DrawsCount.ToIntHtmlVisual(),
                                LostsCount = profiler.StandingInfoModel.UpTeam.LostsCount.ToIntHtmlVisual(),
                                Point = profiler.StandingInfoModel.UpTeam.Point.ToIntHtmlVisual(),
                                Indicator = profiler.StandingInfoModel.UpTeam.Indicator.ToDecimalHtmlVisual()
                            } : null,
                            DownTeam = profiler.StandingInfoModel != null ? new TableStandingDetailVisualiser
                            {
                                TeamName = profiler.StandingInfoModel.DownTeam.TeamName.ToStringHtmlVisual(),
                                Order = profiler.StandingInfoModel.DownTeam.Order.ToIntHtmlVisual(),
                                MatchesCount = profiler.StandingInfoModel.DownTeam.MatchesCount.ToIntHtmlVisual(),
                                WinsCount = profiler.StandingInfoModel.DownTeam.WinsCount.ToIntHtmlVisual(),
                                DrawsCount = profiler.StandingInfoModel.DownTeam.DrawsCount.ToIntHtmlVisual(),
                                LostsCount = profiler.StandingInfoModel.DownTeam.LostsCount.ToIntHtmlVisual(),
                                Point = profiler.StandingInfoModel.DownTeam.Point.ToIntHtmlVisual(),
                                Indicator = profiler.StandingInfoModel.DownTeam.Indicator.ToDecimalHtmlVisual()
                            } : null
                        }
                    };
                    break;
                case 1:
                    result = new AnalyseResultVisualiser
                    {
                        HomeTeamVsAwayTeam = string.Format("{0} vs {1}", profiler.TeamPercentageProfiler.HomeTeam, profiler.TeamPercentageProfiler.AwayTeam),
                        TargetURL = profiler.TeamPercentageProfiler.TargetURL,
                        ODD_PERCENTAGE_Visualiser = new OddAnalyseVisualiser
                        {
                            AllCountFound = profiler.TeamPercentageProfiler.FT_Result.CountAll,
                            FT_1_5_Over = profiler.TeamPercentageProfiler.FT_1_5_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),

                            Home_HT_0_5_Over = profiler.TeamPercentageProfiler.Home_HT_0_5_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            Home_HT_1_5_Over = profiler.TeamPercentageProfiler.Home_HT_1_5_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            Home_SH_0_5_Over = profiler.TeamPercentageProfiler.Home_SH_0_5_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            Home_SH_1_5_Over = profiler.TeamPercentageProfiler.Home_SH_1_5_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            Home_FT_0_5_Over = profiler.TeamPercentageProfiler.Home_FT_0_5_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            Home_FT_1_5_Over = profiler.TeamPercentageProfiler.Home_FT_1_5_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            Home_Win_Any_Half = profiler.TeamPercentageProfiler.Home_Win_Any_Half.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),

                            Away_HT_0_5_Over = profiler.TeamPercentageProfiler.Away_HT_0_5_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            Away_HT_1_5_Over = profiler.TeamPercentageProfiler.Away_HT_1_5_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            Away_SH_0_5_Over = profiler.TeamPercentageProfiler.Away_SH_0_5_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            Away_SH_1_5_Over = profiler.TeamPercentageProfiler.Away_SH_1_5_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            Away_FT_0_5_Over = profiler.TeamPercentageProfiler.Away_FT_0_5_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            Away_FT_1_5_Over = profiler.TeamPercentageProfiler.Away_FT_1_5_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            Away_Win_Any_Half = profiler.TeamPercentageProfiler.Away_Win_Any_Half.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),

                            FT_2_5_Over = profiler.TeamPercentageProfiler.FT_2_5_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            FT_3_5_Over = profiler.TeamPercentageProfiler.FT_3_5_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            FT_GG = profiler.TeamPercentageProfiler.FT_GG.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            FT_Result = profiler.TeamPercentageProfiler.FT_Result.ToPercentage((int)StaticPercentageDefinerEnum.ThreeWayStandard),
                            FT_TotalBetween = profiler.TeamPercentageProfiler.FT_TotalBetween.ToPercentage((int)StaticPercentageDefinerEnum.FourWayStandard),
                            HT_0_5_Over = profiler.TeamPercentageProfiler.HT_0_5_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            HT_1_5_Over = profiler.TeamPercentageProfiler.HT_1_5_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            HT_FT_Result = profiler.TeamPercentageProfiler.HT_FT_Result.ToPercentage((int)StaticPercentageDefinerEnum.NineWayStandard),
                            HT_GG = profiler.TeamPercentageProfiler.HT_GG.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            HT_Result = profiler.TeamPercentageProfiler.HT_Result.ToPercentage((int)StaticPercentageDefinerEnum.ThreeWayStandard),
                            MoreGoalsBetweenTimes = profiler.TeamPercentageProfiler.MoreGoalsBetweenTimes.ToPercentage((int)StaticPercentageDefinerEnum.ThreeWayStandard),
                            SH_0_5_Over = profiler.TeamPercentageProfiler.SH_0_5_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            SH_1_5_Over = profiler.TeamPercentageProfiler.SH_1_5_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            SH_GG = profiler.TeamPercentageProfiler.SH_GG.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            SH_Result = profiler.TeamPercentageProfiler.SH_Result.ToPercentage((int)StaticPercentageDefinerEnum.ThreeWayStandard)
                        },
                        AVERAGE_Visualiser = new AverageVisualiser
                        {
                            FT_Result = profiler.AverageProfiler.FT_Result.ToPercentage((int)StaticPercentageDefinerEnum.ThreeWayStandard),
                            HT_Result = profiler.AverageProfiler.HT_Result.ToPercentage((int)StaticPercentageDefinerEnum.ThreeWayStandard),
                            SH_Result = profiler.AverageProfiler.SH_Result.ToPercentage((int)StaticPercentageDefinerEnum.ThreeWayStandard),

                            HT_05_Over_Home = profiler.AverageProfiler.HT_05_Over_Home.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            HT_15_Over_Home = profiler.AverageProfiler.HT_15_Over_Home.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            HT_05_Over_Away = profiler.AverageProfiler.HT_05_Over_Away.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            HT_15_Over_Away = profiler.AverageProfiler.HT_15_Over_Away.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            Home_HT_05_Over = profiler.AverageProfiler.Home_HT_05_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            Home_HT_15_Over = profiler.AverageProfiler.Home_HT_15_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            Away_HT_05_Over = profiler.AverageProfiler.Away_HT_05_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            Away_HT_15_Over = profiler.AverageProfiler.Away_HT_15_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),

                            SH_05_Over_Home = profiler.AverageProfiler.SH_05_Over_Home.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            SH_15_Over_Home = profiler.AverageProfiler.SH_15_Over_Home.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            SH_05_Over_Away = profiler.AverageProfiler.SH_05_Over_Away.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            SH_15_Over_Away = profiler.AverageProfiler.SH_15_Over_Away.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            Home_SH_05_Over = profiler.AverageProfiler.Home_SH_05_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            Home_SH_15_Over = profiler.AverageProfiler.Home_SH_15_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            Away_SH_05_Over = profiler.AverageProfiler.Away_SH_05_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            Away_SH_15_Over = profiler.AverageProfiler.Away_SH_15_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),

                            FT_15_Over_Home = profiler.AverageProfiler.FT_15_Over_Home.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            FT_25_Over_Home = profiler.AverageProfiler.FT_25_Over_Home.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            FT_35_Over_Home = profiler.AverageProfiler.FT_35_Over_Home.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            FT_15_Over_Away = profiler.AverageProfiler.FT_15_Over_Away.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            FT_25_Over_Away = profiler.AverageProfiler.FT_25_Over_Away.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            FT_35_Over_Away = profiler.AverageProfiler.FT_35_Over_Away.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            Home_FT_05_Over = profiler.AverageProfiler.Home_FT_05_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            Home_FT_15_Over = profiler.AverageProfiler.Home_FT_15_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            Away_FT_05_Over = profiler.AverageProfiler.Away_FT_05_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            Away_FT_15_Over = profiler.AverageProfiler.Away_FT_15_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),

                            HT_GG_Home = profiler.AverageProfiler.HT_GG_Home.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            SH_GG_Home = profiler.AverageProfiler.SH_GG_Home.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            FT_GG_Home = profiler.AverageProfiler.FT_GG_Home.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            HT_GG_Away = profiler.AverageProfiler.HT_GG_Away.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            SH_GG_Away = profiler.AverageProfiler.SH_GG_Away.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            FT_GG_Away = profiler.AverageProfiler.FT_GG_Away.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),

                            Home_Win_Any_Half = profiler.AverageProfiler.Home_Win_Any_Half.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                            Away_Win_Any_Half = profiler.AverageProfiler.Away_Win_Any_Half.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),

                            HT_FT_Result = profiler.AverageProfiler.HT_FT_Result.ToPercentage((int)StaticPercentageDefinerEnum.NineWayStandard),
                            MoreGoalsBetweenTimes = profiler.AverageProfiler.MoreGoalsBetweenTimes.ToPercentage((int)StaticPercentageDefinerEnum.ThreeWayStandard),
                            Total_BetweenGoals = profiler.AverageProfiler.Total_BetweenGoals.ToPercentage((int)StaticPercentageDefinerEnum.FourWayStandard),

                            Average_HT_Goals_HomeTeam = profiler.AverageProfiler.Average_HT_Goals_HomeTeam.ToDecimalVisual(),
                            Average_HT_Goals_AwayTeam = profiler.AverageProfiler.Average_HT_Goals_AwayTeam.ToDecimalVisual(),
                            Average_SH_Goals_HomeTeam = profiler.AverageProfiler.Average_SH_Goals_HomeTeam.ToDecimalVisual(),
                            Average_SH_Goals_AwayTeam = profiler.AverageProfiler.Average_SH_Goals_AwayTeam.ToDecimalVisual(),
                            Average_FT_Goals_HomeTeam = profiler.AverageProfiler.Average_FT_Goals_HomeTeam.ToDecimalVisual(),
                            Average_FT_Goals_AwayTeam = profiler.AverageProfiler.Average_FT_Goals_AwayTeam.ToDecimalVisual()
                        },
                        COMPARISON_Visualiser = new ComparisonVisualiser
                        {
                            BY_SIDE = profiler.ComparisonInfoContainer != null ? new SideVisualiserModel
                            {
                                CountFound = profiler.ComparisonInfoContainer.HomeAway.CountFound.ToIntVisual(),
                                FT_Result = profiler.ComparisonInfoContainer.HomeAway.FT_Result.ToPercentage((int)StaticPercentageDefinerEnum.ThreeWayStandard),
                                HT_Result = profiler.ComparisonInfoContainer.HomeAway.HT_Result.ToPercentage((int)StaticPercentageDefinerEnum.ThreeWayStandard),
                                SH_Result = profiler.ComparisonInfoContainer.HomeAway.SH_Result.ToPercentage((int)StaticPercentageDefinerEnum.ThreeWayStandard),

                                HT_05_Over = profiler.ComparisonInfoContainer.HomeAway.HT_05_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                HT_15_Over = profiler.ComparisonInfoContainer.HomeAway.HT_15_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                Home_HT_05_Over = profiler.ComparisonInfoContainer.HomeAway.Home_HT_05_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                Home_HT_15_Over = profiler.ComparisonInfoContainer.HomeAway.Home_HT_15_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                Away_HT_05_Over = profiler.ComparisonInfoContainer.HomeAway.Away_HT_05_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                Away_HT_15_Over = profiler.ComparisonInfoContainer.HomeAway.Away_HT_15_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),

                                SH_05_Over = profiler.ComparisonInfoContainer.HomeAway.SH_05_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                SH_15_Over = profiler.ComparisonInfoContainer.HomeAway.SH_15_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                Home_SH_05_Over = profiler.ComparisonInfoContainer.HomeAway.Home_SH_05_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                Home_SH_15_Over = profiler.ComparisonInfoContainer.HomeAway.Home_SH_15_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                Away_SH_05_Over = profiler.ComparisonInfoContainer.HomeAway.Away_SH_05_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                Away_SH_15_Over = profiler.ComparisonInfoContainer.HomeAway.Away_SH_15_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),

                                FT_15_Over = profiler.ComparisonInfoContainer.HomeAway.FT_15_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                FT_25_Over = profiler.ComparisonInfoContainer.HomeAway.FT_25_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                FT_35_Over = profiler.ComparisonInfoContainer.HomeAway.FT_35_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                Home_FT_05_Over = profiler.ComparisonInfoContainer.HomeAway.Home_FT_05_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                Home_FT_15_Over = profiler.ComparisonInfoContainer.HomeAway.Home_FT_15_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                Away_FT_05_Over = profiler.ComparisonInfoContainer.HomeAway.Away_FT_05_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                Away_FT_15_Over = profiler.ComparisonInfoContainer.HomeAway.Away_FT_15_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),

                                HT_GG = profiler.ComparisonInfoContainer.HomeAway.HT_GG.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                SH_GG = profiler.ComparisonInfoContainer.HomeAway.SH_GG.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                FT_GG = profiler.ComparisonInfoContainer.HomeAway.FT_GG.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),

                                Home_Win_Any_Half = profiler.ComparisonInfoContainer.HomeAway.Home_Win_Any_Half.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                Away_Win_Any_Half = profiler.ComparisonInfoContainer.HomeAway.Away_Win_Any_Half.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),

                                HT_FT_Result = profiler.ComparisonInfoContainer.HomeAway.HT_FT_Result.ToPercentage((int)StaticPercentageDefinerEnum.NineWayStandard),
                                MoreGoalsBetweenTimes = profiler.ComparisonInfoContainer.HomeAway.MoreGoalsBetweenTimes.ToPercentage((int)StaticPercentageDefinerEnum.ThreeWayStandard),
                                Total_BetweenGoals = profiler.ComparisonInfoContainer.HomeAway.Total_BetweenGoals.ToPercentage((int)StaticPercentageDefinerEnum.FourWayStandard),

                                Average_HT_Goals_HomeTeam = profiler.ComparisonInfoContainer.HomeAway.Average_HT_Goals_HomeTeam.ToDecimalVisual(),
                                Average_HT_Goals_AwayTeam = profiler.ComparisonInfoContainer.HomeAway.Average_HT_Goals_AwayTeam.ToDecimalVisual(),
                                Average_SH_Goals_HomeTeam = profiler.ComparisonInfoContainer.HomeAway.Average_SH_Goals_HomeTeam.ToDecimalVisual(),
                                Average_SH_Goals_AwayTeam = profiler.ComparisonInfoContainer.HomeAway.Average_SH_Goals_AwayTeam.ToDecimalVisual(),
                                Average_FT_Goals_HomeTeam = profiler.ComparisonInfoContainer.HomeAway.Average_FT_Goals_HomeTeam.ToDecimalVisual(),
                                Average_FT_Goals_AwayTeam = profiler.ComparisonInfoContainer.HomeAway.Average_FT_Goals_AwayTeam.ToDecimalVisual()
                            } : null,
                            GENERAL = profiler.ComparisonInfoContainer != null ? new SideVisualiserModel
                            {
                                CountFound = profiler.ComparisonInfoContainer.General.CountFound.ToIntVisual(),
                                FT_Result = profiler.ComparisonInfoContainer.General.FT_Result.ToPercentage((int)StaticPercentageDefinerEnum.ThreeWayStandard),
                                HT_Result = profiler.ComparisonInfoContainer.General.HT_Result.ToPercentage((int)StaticPercentageDefinerEnum.ThreeWayStandard),
                                SH_Result = profiler.ComparisonInfoContainer.General.SH_Result.ToPercentage((int)StaticPercentageDefinerEnum.ThreeWayStandard),

                                HT_05_Over = profiler.ComparisonInfoContainer.General.HT_05_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                HT_15_Over = profiler.ComparisonInfoContainer.General.HT_15_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                Home_HT_05_Over = profiler.ComparisonInfoContainer.General.Home_HT_05_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                Home_HT_15_Over = profiler.ComparisonInfoContainer.General.Home_HT_15_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                Away_HT_05_Over = profiler.ComparisonInfoContainer.General.Away_HT_05_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                Away_HT_15_Over = profiler.ComparisonInfoContainer.General.Away_HT_15_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),

                                SH_05_Over = profiler.ComparisonInfoContainer.General.SH_05_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                SH_15_Over = profiler.ComparisonInfoContainer.General.SH_15_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                Home_SH_05_Over = profiler.ComparisonInfoContainer.General.Home_SH_05_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                Home_SH_15_Over = profiler.ComparisonInfoContainer.General.Home_SH_15_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                Away_SH_05_Over = profiler.ComparisonInfoContainer.General.Away_SH_05_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                Away_SH_15_Over = profiler.ComparisonInfoContainer.General.Away_SH_15_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),

                                FT_15_Over = profiler.ComparisonInfoContainer.General.FT_15_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                FT_25_Over = profiler.ComparisonInfoContainer.General.FT_25_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                FT_35_Over = profiler.ComparisonInfoContainer.General.FT_35_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                Home_FT_05_Over = profiler.ComparisonInfoContainer.General.Home_FT_05_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                Home_FT_15_Over = profiler.ComparisonInfoContainer.General.Home_FT_15_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                Away_FT_05_Over = profiler.ComparisonInfoContainer.General.Away_FT_05_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                Away_FT_15_Over = profiler.ComparisonInfoContainer.General.Away_FT_15_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),

                                HT_GG = profiler.ComparisonInfoContainer.General.HT_GG.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                SH_GG = profiler.ComparisonInfoContainer.General.SH_GG.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                FT_GG = profiler.ComparisonInfoContainer.General.FT_GG.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),

                                Home_Win_Any_Half = profiler.ComparisonInfoContainer.General.Home_Win_Any_Half.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                Away_Win_Any_Half = profiler.ComparisonInfoContainer.General.Away_Win_Any_Half.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),

                                HT_FT_Result = profiler.ComparisonInfoContainer.General.HT_FT_Result.ToPercentage((int)StaticPercentageDefinerEnum.NineWayStandard),
                                MoreGoalsBetweenTimes = profiler.ComparisonInfoContainer.General.MoreGoalsBetweenTimes.ToPercentage((int)StaticPercentageDefinerEnum.ThreeWayStandard),
                                Total_BetweenGoals = profiler.ComparisonInfoContainer.General.Total_BetweenGoals.ToPercentage((int)StaticPercentageDefinerEnum.FourWayStandard),

                                Average_HT_Goals_HomeTeam = profiler.ComparisonInfoContainer.General.Average_HT_Goals_HomeTeam.ToDecimalVisual(),
                                Average_HT_Goals_AwayTeam = profiler.ComparisonInfoContainer.General.Average_HT_Goals_AwayTeam.ToDecimalVisual(),
                                Average_SH_Goals_HomeTeam = profiler.ComparisonInfoContainer.General.Average_SH_Goals_HomeTeam.ToDecimalVisual(),
                                Average_SH_Goals_AwayTeam = profiler.ComparisonInfoContainer.General.Average_SH_Goals_AwayTeam.ToDecimalVisual(),
                                Average_FT_Goals_HomeTeam = profiler.ComparisonInfoContainer.General.Average_FT_Goals_HomeTeam.ToDecimalVisual(),
                                Average_FT_Goals_AwayTeam = profiler.ComparisonInfoContainer.General.Average_FT_Goals_AwayTeam.ToDecimalVisual()
                            } : null
                        },
                        PERFORMANCE_HOME_Visualiser = new FormHomePerformanceVisualiser
                        {
                            AT_HOME_PERFORMANCE = profiler.HomeTeam_FormPerformanceGuessContainer != null ? new HomeSideVisualiserModel
                            {
                                CountFound = profiler.HomeTeam_FormPerformanceGuessContainer.HomeAway.CountFound.ToIntVisual(),
                                FT_Result = profiler.HomeTeam_FormPerformanceGuessContainer.HomeAway.FT_Result.ToPercentage((int)StaticPercentageDefinerEnum.ThreeWayStandard),
                                HT_Result = profiler.HomeTeam_FormPerformanceGuessContainer.HomeAway.HT_Result.ToPercentage((int)StaticPercentageDefinerEnum.ThreeWayStandard),
                                SH_Result = profiler.HomeTeam_FormPerformanceGuessContainer.HomeAway.SH_Result.ToPercentage((int)StaticPercentageDefinerEnum.ThreeWayStandard),

                                HT_05_Over = profiler.HomeTeam_FormPerformanceGuessContainer.HomeAway.HT_05_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                HT_15_Over = profiler.HomeTeam_FormPerformanceGuessContainer.HomeAway.HT_15_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                Home_HT_05_Over = profiler.HomeTeam_FormPerformanceGuessContainer.HomeAway.Home_HT_05_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                Home_HT_15_Over = profiler.HomeTeam_FormPerformanceGuessContainer.HomeAway.Home_HT_15_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),

                                SH_05_Over = profiler.HomeTeam_FormPerformanceGuessContainer.HomeAway.SH_05_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                SH_15_Over = profiler.HomeTeam_FormPerformanceGuessContainer.HomeAway.SH_15_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                Home_SH_05_Over = profiler.HomeTeam_FormPerformanceGuessContainer.HomeAway.Home_SH_05_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                Home_SH_15_Over = profiler.HomeTeam_FormPerformanceGuessContainer.HomeAway.Home_SH_15_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),

                                FT_15_Over = profiler.HomeTeam_FormPerformanceGuessContainer.HomeAway.FT_15_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                FT_25_Over = profiler.HomeTeam_FormPerformanceGuessContainer.HomeAway.FT_25_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                FT_35_Over = profiler.HomeTeam_FormPerformanceGuessContainer.HomeAway.FT_35_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                Home_FT_05_Over = profiler.HomeTeam_FormPerformanceGuessContainer.HomeAway.Home_FT_05_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                Home_FT_15_Over = profiler.HomeTeam_FormPerformanceGuessContainer.HomeAway.Home_FT_15_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),

                                HT_GG = profiler.HomeTeam_FormPerformanceGuessContainer.HomeAway.HT_GG.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                SH_GG = profiler.HomeTeam_FormPerformanceGuessContainer.HomeAway.SH_GG.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                FT_GG = profiler.HomeTeam_FormPerformanceGuessContainer.HomeAway.FT_GG.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),

                                Home_Win_Any_Half = profiler.HomeTeam_FormPerformanceGuessContainer.HomeAway.Home_Win_Any_Half.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),

                                HT_FT_Result = profiler.HomeTeam_FormPerformanceGuessContainer.HomeAway.HT_FT_Result.ToPercentage((int)StaticPercentageDefinerEnum.NineWayStandard),
                                MoreGoalsBetweenTimes = profiler.HomeTeam_FormPerformanceGuessContainer.HomeAway.MoreGoalsBetweenTimes.ToPercentage((int)StaticPercentageDefinerEnum.ThreeWayStandard),
                                Total_BetweenGoals = profiler.HomeTeam_FormPerformanceGuessContainer.HomeAway.Total_BetweenGoals.ToPercentage((int)StaticPercentageDefinerEnum.FourWayStandard),

                                Average_HT_Goals_HomeTeam = profiler.HomeTeam_FormPerformanceGuessContainer.HomeAway.Average_HT_Goals_HomeTeam.ToDecimalVisual(),
                                Average_SH_Goals_HomeTeam = profiler.HomeTeam_FormPerformanceGuessContainer.HomeAway.Average_SH_Goals_HomeTeam.ToDecimalVisual(),
                                Average_FT_Goals_HomeTeam = profiler.HomeTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_Goals_HomeTeam.ToDecimalVisual(),
                            } : null,
                            ALL_PERFORMANCE = profiler.HomeTeam_FormPerformanceGuessContainer != null ? new HomeSideVisualiserModel
                            {
                                CountFound = profiler.HomeTeam_FormPerformanceGuessContainer.General.CountFound.ToIntVisual(),
                                FT_Result = profiler.HomeTeam_FormPerformanceGuessContainer.General.FT_Result.ToPercentage((int)StaticPercentageDefinerEnum.ThreeWayStandard),
                                HT_Result = profiler.HomeTeam_FormPerformanceGuessContainer.General.HT_Result.ToPercentage((int)StaticPercentageDefinerEnum.ThreeWayStandard),
                                SH_Result = profiler.HomeTeam_FormPerformanceGuessContainer.General.SH_Result.ToPercentage((int)StaticPercentageDefinerEnum.ThreeWayStandard),

                                HT_05_Over = profiler.HomeTeam_FormPerformanceGuessContainer.General.HT_05_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                HT_15_Over = profiler.HomeTeam_FormPerformanceGuessContainer.General.HT_15_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                Home_HT_05_Over = profiler.HomeTeam_FormPerformanceGuessContainer.General.Home_HT_05_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                Home_HT_15_Over = profiler.HomeTeam_FormPerformanceGuessContainer.General.Home_HT_15_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),

                                SH_05_Over = profiler.HomeTeam_FormPerformanceGuessContainer.General.SH_05_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                SH_15_Over = profiler.HomeTeam_FormPerformanceGuessContainer.General.SH_15_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                Home_SH_05_Over = profiler.HomeTeam_FormPerformanceGuessContainer.General.Home_SH_05_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                Home_SH_15_Over = profiler.HomeTeam_FormPerformanceGuessContainer.General.Home_SH_15_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),

                                FT_15_Over = profiler.HomeTeam_FormPerformanceGuessContainer.General.FT_15_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                FT_25_Over = profiler.HomeTeam_FormPerformanceGuessContainer.General.FT_25_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                FT_35_Over = profiler.HomeTeam_FormPerformanceGuessContainer.General.FT_35_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                Home_FT_05_Over = profiler.HomeTeam_FormPerformanceGuessContainer.General.Home_FT_05_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                Home_FT_15_Over = profiler.HomeTeam_FormPerformanceGuessContainer.General.Home_FT_15_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),

                                HT_GG = profiler.HomeTeam_FormPerformanceGuessContainer.General.HT_GG.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                SH_GG = profiler.HomeTeam_FormPerformanceGuessContainer.General.SH_GG.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                FT_GG = profiler.HomeTeam_FormPerformanceGuessContainer.General.FT_GG.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),

                                Home_Win_Any_Half = profiler.HomeTeam_FormPerformanceGuessContainer.General.Home_Win_Any_Half.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),

                                HT_FT_Result = profiler.HomeTeam_FormPerformanceGuessContainer.General.HT_FT_Result.ToPercentage((int)StaticPercentageDefinerEnum.NineWayStandard),
                                MoreGoalsBetweenTimes = profiler.HomeTeam_FormPerformanceGuessContainer.General.MoreGoalsBetweenTimes.ToPercentage((int)StaticPercentageDefinerEnum.ThreeWayStandard),
                                Total_BetweenGoals = profiler.HomeTeam_FormPerformanceGuessContainer.General.Total_BetweenGoals.ToPercentage((int)StaticPercentageDefinerEnum.FourWayStandard),

                                Average_HT_Goals_HomeTeam = profiler.HomeTeam_FormPerformanceGuessContainer.General.Average_HT_Goals_HomeTeam.ToDecimalVisual(),
                                Average_SH_Goals_HomeTeam = profiler.HomeTeam_FormPerformanceGuessContainer.General.Average_SH_Goals_HomeTeam.ToDecimalVisual(),
                                Average_FT_Goals_HomeTeam = profiler.HomeTeam_FormPerformanceGuessContainer.General.Average_FT_Goals_HomeTeam.ToDecimalVisual(),
                            } : null
                        },
                        PERFORMANCE_AWAY_Visualiser = new FormAwayPerformanceVisualiser
                        {
                            AT_AWAY_PERFORMANCE = profiler.AwayTeam_FormPerformanceGuessContainer != null ? new AwaySideVisualiserModel
                            {
                                CountFound = profiler.AwayTeam_FormPerformanceGuessContainer.HomeAway.CountFound.ToIntVisual(),
                                FT_Result = profiler.AwayTeam_FormPerformanceGuessContainer.HomeAway.FT_Result.ToPercentage((int)StaticPercentageDefinerEnum.ThreeWayStandard),
                                HT_Result = profiler.AwayTeam_FormPerformanceGuessContainer.HomeAway.HT_Result.ToPercentage((int)StaticPercentageDefinerEnum.ThreeWayStandard),
                                SH_Result = profiler.AwayTeam_FormPerformanceGuessContainer.HomeAway.SH_Result.ToPercentage((int)StaticPercentageDefinerEnum.ThreeWayStandard),

                                HT_05_Over = profiler.AwayTeam_FormPerformanceGuessContainer.HomeAway.HT_05_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                HT_15_Over = profiler.AwayTeam_FormPerformanceGuessContainer.HomeAway.HT_15_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                Away_HT_05_Over = profiler.AwayTeam_FormPerformanceGuessContainer.HomeAway.Away_HT_05_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                Away_HT_15_Over = profiler.AwayTeam_FormPerformanceGuessContainer.HomeAway.Away_HT_15_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),

                                SH_05_Over = profiler.AwayTeam_FormPerformanceGuessContainer.HomeAway.SH_05_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                SH_15_Over = profiler.AwayTeam_FormPerformanceGuessContainer.HomeAway.SH_15_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                Away_SH_05_Over = profiler.AwayTeam_FormPerformanceGuessContainer.HomeAway.Away_SH_05_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                Away_SH_15_Over = profiler.AwayTeam_FormPerformanceGuessContainer.HomeAway.Away_SH_15_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),

                                FT_15_Over = profiler.AwayTeam_FormPerformanceGuessContainer.HomeAway.FT_15_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                FT_25_Over = profiler.AwayTeam_FormPerformanceGuessContainer.HomeAway.FT_25_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                FT_35_Over = profiler.AwayTeam_FormPerformanceGuessContainer.HomeAway.FT_35_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                Away_FT_05_Over = profiler.AwayTeam_FormPerformanceGuessContainer.HomeAway.Away_FT_05_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                Away_FT_15_Over = profiler.AwayTeam_FormPerformanceGuessContainer.HomeAway.Away_FT_15_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),

                                HT_GG = profiler.AwayTeam_FormPerformanceGuessContainer.HomeAway.HT_GG.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                SH_GG = profiler.AwayTeam_FormPerformanceGuessContainer.HomeAway.SH_GG.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                FT_GG = profiler.AwayTeam_FormPerformanceGuessContainer.HomeAway.FT_GG.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),

                                Away_Win_Any_Half = profiler.AwayTeam_FormPerformanceGuessContainer.HomeAway.Away_Win_Any_Half.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),

                                HT_FT_Result = profiler.AwayTeam_FormPerformanceGuessContainer.HomeAway.HT_FT_Result.ToPercentage((int)StaticPercentageDefinerEnum.NineWayStandard),
                                MoreGoalsBetweenTimes = profiler.AwayTeam_FormPerformanceGuessContainer.HomeAway.MoreGoalsBetweenTimes.ToPercentage((int)StaticPercentageDefinerEnum.ThreeWayStandard),
                                Total_BetweenGoals = profiler.AwayTeam_FormPerformanceGuessContainer.HomeAway.Total_BetweenGoals.ToPercentage((int)StaticPercentageDefinerEnum.FourWayStandard),

                                Average_HT_Goals_AwayTeam = profiler.AwayTeam_FormPerformanceGuessContainer.HomeAway.Average_HT_Goals_AwayTeam.ToDecimalVisual(),
                                Average_SH_Goals_AwayTeam = profiler.AwayTeam_FormPerformanceGuessContainer.HomeAway.Average_SH_Goals_AwayTeam.ToDecimalVisual(),
                                Average_FT_Goals_AwayTeam = profiler.AwayTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_Goals_AwayTeam.ToDecimalVisual()
                            } : null,
                            ALL_PERFORMANCE = profiler.AwayTeam_FormPerformanceGuessContainer != null ? new AwaySideVisualiserModel
                            {
                                CountFound = profiler.AwayTeam_FormPerformanceGuessContainer.General.CountFound.ToIntVisual(),
                                FT_Result = profiler.AwayTeam_FormPerformanceGuessContainer.General.FT_Result.ToPercentage((int)StaticPercentageDefinerEnum.ThreeWayStandard),
                                HT_Result = profiler.AwayTeam_FormPerformanceGuessContainer.General.HT_Result.ToPercentage((int)StaticPercentageDefinerEnum.ThreeWayStandard),
                                SH_Result = profiler.AwayTeam_FormPerformanceGuessContainer.General.SH_Result.ToPercentage((int)StaticPercentageDefinerEnum.ThreeWayStandard),

                                HT_05_Over = profiler.AwayTeam_FormPerformanceGuessContainer.General.HT_05_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                HT_15_Over = profiler.AwayTeam_FormPerformanceGuessContainer.General.HT_15_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                Away_HT_05_Over = profiler.AwayTeam_FormPerformanceGuessContainer.General.Away_HT_05_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                Away_HT_15_Over = profiler.AwayTeam_FormPerformanceGuessContainer.General.Away_HT_15_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),

                                SH_05_Over = profiler.AwayTeam_FormPerformanceGuessContainer.General.SH_05_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                SH_15_Over = profiler.AwayTeam_FormPerformanceGuessContainer.General.SH_15_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                Away_SH_05_Over = profiler.AwayTeam_FormPerformanceGuessContainer.General.Away_SH_05_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                Away_SH_15_Over = profiler.AwayTeam_FormPerformanceGuessContainer.General.Away_SH_15_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),

                                FT_15_Over = profiler.AwayTeam_FormPerformanceGuessContainer.General.FT_15_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                FT_25_Over = profiler.AwayTeam_FormPerformanceGuessContainer.General.FT_25_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                FT_35_Over = profiler.AwayTeam_FormPerformanceGuessContainer.General.FT_35_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                Away_FT_05_Over = profiler.AwayTeam_FormPerformanceGuessContainer.General.Away_FT_05_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                Away_FT_15_Over = profiler.AwayTeam_FormPerformanceGuessContainer.General.Away_FT_15_Over.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),

                                HT_GG = profiler.AwayTeam_FormPerformanceGuessContainer.General.HT_GG.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                SH_GG = profiler.AwayTeam_FormPerformanceGuessContainer.General.SH_GG.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),
                                FT_GG = profiler.AwayTeam_FormPerformanceGuessContainer.General.FT_GG.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),

                                Away_Win_Any_Half = profiler.AwayTeam_FormPerformanceGuessContainer.General.Away_Win_Any_Half.ToPercentage((int)StaticPercentageDefinerEnum.TwoWayStandard),

                                HT_FT_Result = profiler.AwayTeam_FormPerformanceGuessContainer.General.HT_FT_Result.ToPercentage((int)StaticPercentageDefinerEnum.NineWayStandard),
                                MoreGoalsBetweenTimes = profiler.AwayTeam_FormPerformanceGuessContainer.General.MoreGoalsBetweenTimes.ToPercentage((int)StaticPercentageDefinerEnum.ThreeWayStandard),
                                Total_BetweenGoals = profiler.AwayTeam_FormPerformanceGuessContainer.General.Total_BetweenGoals.ToPercentage((int)StaticPercentageDefinerEnum.FourWayStandard),

                                Average_HT_Goals_AwayTeam = profiler.AwayTeam_FormPerformanceGuessContainer.General.Average_HT_Goals_AwayTeam.ToDecimalVisual(),
                                Average_SH_Goals_AwayTeam = profiler.AwayTeam_FormPerformanceGuessContainer.General.Average_SH_Goals_AwayTeam.ToDecimalVisual(),
                                Average_FT_Goals_AwayTeam = profiler.AwayTeam_FormPerformanceGuessContainer.General.Average_FT_Goals_AwayTeam.ToDecimalVisual()
                            } : null
                        },
                        TABLE_Visualiser = new TableStandingVisualiser
                        {
                            UpTeam = profiler.StandingInfoModel != null ? new TableStandingDetailVisualiser
                            {
                                TeamName = profiler.StandingInfoModel.UpTeam.TeamName,
                                Order = profiler.StandingInfoModel.UpTeam.Order.ToIntVisual(),
                                MatchesCount = profiler.StandingInfoModel.UpTeam.MatchesCount.ToIntVisual(),
                                WinsCount = profiler.StandingInfoModel.UpTeam.WinsCount.ToIntVisual(),
                                DrawsCount = profiler.StandingInfoModel.UpTeam.DrawsCount.ToIntVisual(),
                                LostsCount = profiler.StandingInfoModel.UpTeam.LostsCount.ToIntVisual(),
                                Point = profiler.StandingInfoModel.UpTeam.Point.ToIntVisual(),
                                Indicator = profiler.StandingInfoModel.UpTeam.Indicator.ToDecimalVisual()
                            } : null,
                            DownTeam = profiler.StandingInfoModel != null ? new TableStandingDetailVisualiser
                            {
                                TeamName = profiler.StandingInfoModel.DownTeam.TeamName,
                                Order = profiler.StandingInfoModel.DownTeam.Order.ToIntVisual(),
                                MatchesCount = profiler.StandingInfoModel.DownTeam.MatchesCount.ToIntVisual(),
                                WinsCount = profiler.StandingInfoModel.DownTeam.WinsCount.ToIntVisual(),
                                DrawsCount = profiler.StandingInfoModel.DownTeam.DrawsCount.ToIntVisual(),
                                LostsCount = profiler.StandingInfoModel.DownTeam.LostsCount.ToIntVisual(),
                                Point = profiler.StandingInfoModel.DownTeam.Point.ToIntVisual(),
                                Indicator = profiler.StandingInfoModel.DownTeam.Indicator.ToDecimalVisual()
                            } : null
                        }
                    };
                    break;
                default:
                    break;
            }

            return result;
        }


        public static AnalyseResultVisualiser MapToDataVisualiserFromProfiler_TEST(this JobAnalyseModel profiler)
        {
            try
            {
                AnalyseResultVisualiser result = new AnalyseResultVisualiser
                {
                    HomeTeamVsAwayTeam = string.Format("{0} vs {1}", profiler.ComparisonInfoContainer.Home, profiler.ComparisonInfoContainer.Away),
                    TargetURL = String.Format("{0}{1}", "http://arsiv.mackolik.com/Match/Default.aspx?id=", profiler.ComparisonInfoContainer.Serial),

                    Is_FT_25_Over = profiler.Is_FT_25_Over,
                    Is_FT_25_Under = profiler.Is_FT_25_Under,
                    Is_GG = profiler.Is_GG,
                    Is_HT_15_Over = profiler.Is_HT_15_Over
                };

                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }


        #region BetSystem Mapping Area

        public static BetSystem MapToSystemEntityForCreation(this CreateSystemDto createSystemDto)
        {
            if (createSystemDto == null)
                throw new Exception("mapping model can not be created from the null object.");

            var result = new BetSystem()
            {
                IsActive = true,
                ModelType = ProjectModelType.BetSystem,
                ModifiedDateTime = azerbaycanTime,
                ModifiedBy = "System.Admin",
                Name = createSystemDto.Name,
                AcceptedOdd = createSystemDto.AcceptedOdd,
                AcceptedDivider = createSystemDto.AcceptedDivider,
                StartingAmount = createSystemDto.StartingAmount,
                StepsGoalCount = createSystemDto.StepsGoalCount,
                MaxBundleCount = createSystemDto.MaxBundleCount,
                Steps = new List<Step>(),
                Bundles = new List<Bundle>(),
                CreatedBy = "System.Admin",
                CreatedDateTime = azerbaycanTime,
            };

            decimal insuredBetAmount = result.StartingAmount;

            for (var i = 0; i < result.StepsGoalCount; i++)
            {
                if (i > 0)
                {
                    insuredBetAmount = Math.Floor(insuredBetAmount * result.AcceptedOdd / 4) * 2;
                }

                var newStep = new Step()
                {
                    ModifiedDateTime = azerbaycanTime,
                    ModifiedBy = "System.Admin",
                    CreatedBy = "System.Admin",
                    CreatedDateTime = azerbaycanTime,
                    IsActive = true,
                    IsSuccess = false,
                    ModelType = ProjectModelType.Step,
                    LinkedFrom = i,
                    Number = i+1,
                    LinkedTo = i+2,
                    Status = StepStatus.New,
                    InsuredBetAmount = insuredBetAmount,
                    System = result
                };

                result.Steps.Add(newStep);
            }

            int highPriority = 1;

            for (int i = 0; i < result.MaxBundleCount; i++)
            {
                var bundleOne = new Bundle()
                {
                    ModifiedDateTime = azerbaycanTime,
                    ModifiedBy = "System.Admin",
                    CreatedBy = "System.Admin",
                    CreatedDateTime = azerbaycanTime,
                    IsActive = true,
                    ModelType= ProjectModelType.Bundle,
                    System = result,
                    BundlePriority = highPriority
                };

                result.Bundles.Add(bundleOne);
                highPriority++;
            }

            return result;
        }

        #endregion
    }
}
