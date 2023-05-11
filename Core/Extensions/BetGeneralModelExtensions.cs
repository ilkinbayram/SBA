using Core.Entities.Concrete.ComplexModels.ML;
using Core.Entities.Concrete.ComplexModels.RequestModelHelpers;
using Core.Entities.Concrete.ExternalDbEntities;
using Core.Resources.Enums;
using Core.Utilities.UsableModel;
using Core.Utilities.UsableModel.TempTableModels.Initialization;
using System;

namespace Core.Extensions
{
    public static class BetGeneralModelExtensions
    {
        public static List<TimeSerialContainer> MapToNewListTimeSerials(this List<TimeSerialContainer> timeSerials, bool analyseAnyTime = false)
        {
            List<TimeSerialContainer> result = new List<TimeSerialContainer>();

            foreach (var item in timeSerials)
            {
                if (!item.IsAnalysed)
                {
                    if (CheckIsAddable(item, analyseAnyTime))
                        result.Add(new TimeSerialContainer { Serial = item.Serial, Time = item.Time });
                }
            }

            return result;
        }


        public static LeagueStatisticsHolder GetLeagueStatistic(this LeagueHolder leagueHolder)
        {
            return new LeagueStatisticsHolder
            {
                LeagueName = leagueHolder.League,
                CountryName = leagueHolder.Country,
                CountFound = leagueHolder.CountFound,
                DateOfAnalyse = DateTime.Now.Date,
                FT_GoalsAverage = leagueHolder.GoalsAverage,
                HT_GoalsAverage = leagueHolder.HT_GoalsAverage,
                SH_GoalsAverage = leagueHolder.SH_GoalsAverage,
                FT_Over15_Percentage = leagueHolder.Over_1_5_Percentage,
                FT_Over25_Percentage = leagueHolder.Over_2_5_Percentage,
                FT_Over35_Percentage = leagueHolder.Over_3_5_Percentage,
                HT_Over05_Percentage = leagueHolder.HT_Over_0_5_Percentage,
                HT_Over15_Percentage = leagueHolder.HT_Over_1_5_Percentage,
                SH_Over05_Percentage = leagueHolder.SH_Over_0_5_Percentage,
                SH_Over15_Percentage = leagueHolder.SH_Over_1_5_Percentage,
                GG_Percentage = leagueHolder.GG_Percentage
            };
        }


        public static ComparisonStatisticsHolder GetComparisonStatistics(this JobAnalyseModel jobAnalyseModel, int bySideType)
        {
            var result = GenerateComparisonStatistic(jobAnalyseModel.ComparisonInfoContainer, jobAnalyseModel.ComparisonOnlyDB, bySideType);
            return result;
        }

        public static TeamPerformanceStatisticsHolder GetHomePerformanceStatistics(this JobAnalyseModel jobAnalyseModel, int bySideType)
        {
            var result = GenerateHomeTeamPerformanceStatistic(jobAnalyseModel.HomeTeam_FormPerformanceGuessContainer, bySideType);
            return result;
        }

        public static TeamPerformanceStatisticsHolder GetAwayPerformanceStatistics(this JobAnalyseModel jobAnalyseModel, int bySideType)
        {
            var result = GenerateAwayTeamPerformanceStatistic(jobAnalyseModel.AwayTeam_FormPerformanceGuessContainer, bySideType);
            return result;
        }

        public static AverageStatisticsHolder GetAverageStatistics(this JobAnalyseModel jobAnalyseModel, int bySideType)
        {
            var result = GenerateAverageStatistic(jobAnalyseModel.AverageProfilerHomeAway, jobAnalyseModel.AverageProfiler, bySideType);
            return result;
        }

        private static ComparisonStatisticsHolder GenerateComparisonStatistic(ComparisonGuessContainer comparison, ComparisonGuessContainer comparisonOnlyDB, int bySideType)
        {
            ComparisonStatisticsHolder result = null;
            if (comparison == null) return result;

            if ((int)BySideType.HomeAway == bySideType)
            {
                if (comparison.HomeAway != null)
                {
                    result = new ComparisonStatisticsHolder
                    {
                        BySideType = bySideType,
                        Average_FT_Goals_HomeTeam = comparison.HomeAway.Average_FT_Goals_HomeTeam,
                        Average_FT_Goals_AwayTeam = comparison.HomeAway.Average_FT_Goals_AwayTeam,
                        Average_HT_Goals_HomeTeam = comparison.HomeAway.Average_HT_Goals_HomeTeam,
                        Average_HT_Goals_AwayTeam = comparison.HomeAway.Average_HT_Goals_AwayTeam,
                        Average_SH_Goals_HomeTeam = comparison.HomeAway.Average_SH_Goals_HomeTeam,
                        Average_SH_Goals_AwayTeam = comparison.HomeAway.Average_SH_Goals_AwayTeam,

                        Away_FT_05_Over = comparison.HomeAway.Away_FT_05_Over.OverridePercentage(),
                        Away_FT_15_Over = comparison.HomeAway.Away_FT_15_Over.OverridePercentage(),
                        Away_HT_05_Over = comparison.HomeAway.Away_HT_05_Over.OverridePercentage(),
                        Away_HT_15_Over = comparison.HomeAway.Away_HT_15_Over.OverridePercentage(),
                        Away_SH_05_Over = comparison.HomeAway.Away_SH_05_Over.OverridePercentage(),
                        Away_SH_15_Over = comparison.HomeAway.Away_SH_15_Over.OverridePercentage(),
                        Home_FT_05_Over = comparison.HomeAway.Home_FT_05_Over.OverridePercentage(),
                        Home_FT_15_Over = comparison.HomeAway.Home_FT_15_Over.OverridePercentage(),
                        Home_HT_05_Over = comparison.HomeAway.Home_HT_05_Over.OverridePercentage(),
                        Home_HT_15_Over = comparison.HomeAway.Home_HT_15_Over.OverridePercentage(),
                        Home_SH_05_Over = comparison.HomeAway.Home_SH_05_Over.OverridePercentage(),
                        Home_SH_15_Over = comparison.HomeAway.Home_SH_15_Over.OverridePercentage(),
                        Home_Win_Any_Half = comparison.HomeAway.Home_Win_Any_Half.OverridePercentage(),
                        Away_Win_Any_Half = comparison.HomeAway.Away_Win_Any_Half.OverridePercentage(),

                        FT_15_Over = comparison.HomeAway.FT_15_Over.OverridePercentage(),
                        FT_25_Over = comparison.HomeAway.FT_25_Over.OverridePercentage(),
                        FT_35_Over = comparison.HomeAway.FT_35_Over.OverridePercentage(),

                        HT_05_Over = comparison.HomeAway.HT_05_Over.OverridePercentage(),
                        HT_15_Over = comparison.HomeAway.HT_15_Over.OverridePercentage(),

                        SH_05_Over = comparison.HomeAway.SH_05_Over.OverridePercentage(),
                        SH_15_Over = comparison.HomeAway.SH_15_Over.OverridePercentage(),

                        FT_GG = comparison.HomeAway.FT_GG.OverridePercentage(),
                        HT_GG = comparison.HomeAway.HT_GG.OverridePercentage(),
                        SH_GG = comparison.HomeAway.SH_GG.OverridePercentage(),

                        Is_FT_Win1 = comparison.HomeAway.Is_FT_Win1.OverridePercentage(),
                        Is_FT_X = comparison.HomeAway.Is_FT_X.OverridePercentage(),
                        Is_FT_Win2 = comparison.HomeAway.Is_FT_Win2.OverridePercentage(),

                        Is_HT_Win1 = comparison.HomeAway.Is_HT_Win1.OverridePercentage(),
                        Is_HT_X = comparison.HomeAway.Is_HT_X.OverridePercentage(),
                        Is_HT_Win2 = comparison.HomeAway.Is_HT_Win2.OverridePercentage(),

                        Is_SH_Win1 = comparison.HomeAway.Is_SH_Win1.OverridePercentage(),
                        Is_SH_X = comparison.HomeAway.Is_SH_X.OverridePercentage(),
                        Is_SH_Win2 = comparison.HomeAway.Is_SH_Win2.OverridePercentage()
                    };

                    if (comparisonOnlyDB != null)
                    {
                        if (comparisonOnlyDB.HomeAway != null)
                        {
                            result.Average_FT_Corners_HomeTeam = comparisonOnlyDB.HomeAway.Average_FT_Corners_HomeTeam;
                            result.Average_FT_Corners_AwayTeam = comparisonOnlyDB.HomeAway.Average_FT_Corners_AwayTeam;

                            result.Is_Corner_FT_Win1 = comparisonOnlyDB.HomeAway.Is_Corner_FT_Win1.OverridePercentage();
                            result.Is_Corner_FT_X = comparisonOnlyDB.HomeAway.Is_Corner_FT_X.OverridePercentage();
                            result.Is_Corner_FT_Win2 = comparisonOnlyDB.HomeAway.Is_Corner_FT_Win2.OverridePercentage();

                            result.Corner_7_5_Over = comparisonOnlyDB.HomeAway.Corner_7_5_Over.OverridePercentage();
                            result.Corner_8_5_Over = comparisonOnlyDB.HomeAway.Corner_8_5_Over.OverridePercentage();
                            result.Corner_9_5_Over = comparisonOnlyDB.HomeAway.Corner_9_5_Over.OverridePercentage();

                            result.Corner_Home_3_5_Over = comparisonOnlyDB.HomeAway.Corner_Home_3_5_Over.OverridePercentage();
                            result.Corner_Home_4_5_Over = comparisonOnlyDB.HomeAway.Corner_Home_4_5_Over.OverridePercentage();
                            result.Corner_Home_5_5_Over = comparisonOnlyDB.HomeAway.Corner_Home_5_5_Over.OverridePercentage();
                            result.Corner_Away_3_5_Over = comparisonOnlyDB.HomeAway.Corner_Away_3_5_Over.OverridePercentage();
                            result.Corner_Away_4_5_Over = comparisonOnlyDB.HomeAway.Corner_Away_4_5_Over.OverridePercentage();
                            result.Corner_Away_5_5_Over = comparisonOnlyDB.HomeAway.Corner_Away_5_5_Over.OverridePercentage();

                            result.Away_Possesion = comparisonOnlyDB.HomeAway.Average_FT_Possesion_AwayTeam.ConvertFromDecimal();
                            result.Home_Possesion = comparisonOnlyDB.HomeAway.Average_FT_Possesion_HomeTeam.ConvertFromDecimal();
                            result.Average_FT_Shut_AwayTeam = comparisonOnlyDB.HomeAway.Average_FT_Shot_AwayTeam;
                            result.Average_FT_Shut_HomeTeam = comparisonOnlyDB.HomeAway.Average_FT_Shot_HomeTeam;
                            result.Average_FT_ShutOnTarget_AwayTeam = comparisonOnlyDB.HomeAway.Average_FT_ShotOnTarget_AwayTeam;
                            result.Average_FT_ShutOnTarget_HomeTeam = comparisonOnlyDB.HomeAway.Average_FT_ShotOnTarget_HomeTeam;
                        }
                    }
                }
            }
            else
            {
                if (comparison.General != null)
                {
                    result = new ComparisonStatisticsHolder
                    {
                        BySideType = bySideType,
                        Average_FT_Goals_HomeTeam = comparison.General.Average_FT_Goals_HomeTeam,
                        Average_FT_Goals_AwayTeam = comparison.General.Average_FT_Goals_AwayTeam,
                        Average_HT_Goals_HomeTeam = comparison.General.Average_HT_Goals_HomeTeam,
                        Average_HT_Goals_AwayTeam = comparison.General.Average_HT_Goals_AwayTeam,
                        Average_SH_Goals_HomeTeam = comparison.General.Average_SH_Goals_HomeTeam,
                        Average_SH_Goals_AwayTeam = comparison.General.Average_SH_Goals_AwayTeam,

                        Away_FT_05_Over = comparison.General.Away_FT_05_Over.OverridePercentage(),
                        Away_FT_15_Over = comparison.General.Away_FT_15_Over.OverridePercentage(),
                        Away_HT_05_Over = comparison.General.Away_HT_05_Over.OverridePercentage(),
                        Away_HT_15_Over = comparison.General.Away_HT_15_Over.OverridePercentage(),
                        Away_SH_05_Over = comparison.General.Away_SH_05_Over.OverridePercentage(),
                        Away_SH_15_Over = comparison.General.Away_SH_15_Over.OverridePercentage(),
                        Home_FT_05_Over = comparison.General.Home_FT_05_Over.OverridePercentage(),
                        Home_FT_15_Over = comparison.General.Home_FT_15_Over.OverridePercentage(),
                        Home_HT_05_Over = comparison.General.Home_HT_05_Over.OverridePercentage(),
                        Home_HT_15_Over = comparison.General.Home_HT_15_Over.OverridePercentage(),
                        Home_SH_05_Over = comparison.General.Home_SH_05_Over.OverridePercentage(),
                        Home_SH_15_Over = comparison.General.Home_SH_15_Over.OverridePercentage(),
                        Home_Win_Any_Half = comparison.General.Home_Win_Any_Half.OverridePercentage(),
                        Away_Win_Any_Half = comparison.General.Away_Win_Any_Half.OverridePercentage(),

                        FT_15_Over = comparison.General.FT_15_Over.OverridePercentage(),
                        FT_25_Over = comparison.General.FT_25_Over.OverridePercentage(),
                        FT_35_Over = comparison.General.FT_35_Over.OverridePercentage(),

                        HT_05_Over = comparison.General.HT_05_Over.OverridePercentage(),
                        HT_15_Over = comparison.General.HT_15_Over.OverridePercentage(),

                        SH_05_Over = comparison.General.SH_05_Over.OverridePercentage(),
                        SH_15_Over = comparison.General.SH_15_Over.OverridePercentage(),

                        FT_GG = comparison.General.FT_GG.OverridePercentage(),
                        HT_GG = comparison.General.HT_GG.OverridePercentage(),
                        SH_GG = comparison.General.SH_GG.OverridePercentage(),

                        Is_FT_Win1 = comparison.General.Is_FT_Win1.OverridePercentage(),
                        Is_FT_X = comparison.General.Is_FT_X.OverridePercentage(),
                        Is_FT_Win2 = comparison.General.Is_FT_Win2.OverridePercentage(),

                        Is_HT_Win1 = comparison.General.Is_HT_Win1.OverridePercentage(),
                        Is_HT_X = comparison.General.Is_HT_X.OverridePercentage(),
                        Is_HT_Win2 = comparison.General.Is_HT_Win2.OverridePercentage(),

                        Is_SH_Win1 = comparison.General.Is_SH_Win1.OverridePercentage(),
                        Is_SH_X = comparison.General.Is_SH_X.OverridePercentage(),
                        Is_SH_Win2 = comparison.General.Is_SH_Win2.OverridePercentage()
                    };

                    if (comparisonOnlyDB != null)
                    {
                        if (comparisonOnlyDB.General != null)
                        {

                            result.Average_FT_Corners_HomeTeam = comparisonOnlyDB.General.Average_FT_Corners_HomeTeam;
                            result.Average_FT_Corners_AwayTeam = comparisonOnlyDB.General.Average_FT_Corners_AwayTeam;

                            result.Is_Corner_FT_Win1 = comparisonOnlyDB.General.Is_Corner_FT_Win1.OverridePercentage();
                            result.Is_Corner_FT_X = comparisonOnlyDB.General.Is_Corner_FT_X.OverridePercentage();
                            result.Is_Corner_FT_Win2 = comparisonOnlyDB.General.Is_Corner_FT_Win2.OverridePercentage();

                            result.Corner_7_5_Over = comparisonOnlyDB.General.Corner_7_5_Over.OverridePercentage();
                            result.Corner_8_5_Over = comparisonOnlyDB.General.Corner_8_5_Over.OverridePercentage();
                            result.Corner_9_5_Over = comparisonOnlyDB.General.Corner_9_5_Over.OverridePercentage();

                            result.Corner_Home_3_5_Over = comparisonOnlyDB.General.Corner_Home_3_5_Over.OverridePercentage();
                            result.Corner_Home_4_5_Over = comparisonOnlyDB.General.Corner_Home_4_5_Over.OverridePercentage();
                            result.Corner_Home_5_5_Over = comparisonOnlyDB.General.Corner_Home_5_5_Over.OverridePercentage();
                            result.Corner_Away_3_5_Over = comparisonOnlyDB.General.Corner_Away_3_5_Over.OverridePercentage();
                            result.Corner_Away_4_5_Over = comparisonOnlyDB.General.Corner_Away_4_5_Over.OverridePercentage();
                            result.Corner_Away_5_5_Over = comparisonOnlyDB.General.Corner_Away_5_5_Over.OverridePercentage();

                            result.Away_Possesion = comparisonOnlyDB.General.Average_FT_Possesion_AwayTeam.ConvertFromDecimal();
                            result.Home_Possesion = comparisonOnlyDB.General.Average_FT_Possesion_HomeTeam.ConvertFromDecimal();
                            result.Average_FT_Shut_AwayTeam = comparisonOnlyDB.General.Average_FT_Shot_AwayTeam;
                            result.Average_FT_Shut_HomeTeam = comparisonOnlyDB.General.Average_FT_Shot_HomeTeam;
                            result.Average_FT_ShutOnTarget_AwayTeam = comparisonOnlyDB.General.Average_FT_ShotOnTarget_AwayTeam;
                            result.Average_FT_ShutOnTarget_HomeTeam = comparisonOnlyDB.General.Average_FT_ShotOnTarget_HomeTeam;
                        }
                    }
                }
            }

            return result;
        }

        private static AverageStatisticsHolder GenerateAverageStatistic(AveragePercentageContainer averageHomeAway, AveragePercentageContainer averageGeneral, int bySideType)
        {
            AverageStatisticsHolder result = null;
            if ((int)BySideType.HomeAway == bySideType)
            {
                if (averageHomeAway != null)
                {
                    result = new AverageStatisticsHolder
                    {
                        BySideType = bySideType,
                        Average_FT_Goals_HomeTeam = averageHomeAway.Average_FT_Goals_HomeTeam,
                        Average_FT_Goals_AwayTeam = averageHomeAway.Average_FT_Goals_AwayTeam,
                        Average_HT_Goals_HomeTeam = averageHomeAway.Average_HT_Goals_HomeTeam,
                        Average_HT_Goals_AwayTeam = averageHomeAway.Average_HT_Goals_AwayTeam,
                        Average_SH_Goals_HomeTeam = averageHomeAway.Average_SH_Goals_HomeTeam,
                        Average_SH_Goals_AwayTeam = averageHomeAway.Average_SH_Goals_AwayTeam,

                        Away_FT_05_Over = averageHomeAway.Away_FT_05_Over.OverridePercentage(),
                        Away_FT_15_Over = averageHomeAway.Away_FT_15_Over.OverridePercentage(),
                        Away_HT_05_Over = averageHomeAway.Away_HT_05_Over.OverridePercentage(),
                        Away_HT_15_Over = averageHomeAway.Away_HT_15_Over.OverridePercentage(),
                        Away_SH_05_Over = averageHomeAway.Away_SH_05_Over.OverridePercentage(),
                        Away_SH_15_Over = averageHomeAway.Away_SH_15_Over.OverridePercentage(),
                        Home_FT_05_Over = averageHomeAway.Home_FT_05_Over.OverridePercentage(),
                        Home_FT_15_Over = averageHomeAway.Home_FT_15_Over.OverridePercentage(),
                        Home_HT_05_Over = averageHomeAway.Home_HT_05_Over.OverridePercentage(),
                        Home_HT_15_Over = averageHomeAway.Home_HT_15_Over.OverridePercentage(),
                        Home_SH_05_Over = averageHomeAway.Home_SH_05_Over.OverridePercentage(),
                        Home_SH_15_Over = averageHomeAway.Home_SH_15_Over.OverridePercentage(),
                        Home_Win_Any_Half = averageHomeAway.Home_Win_Any_Half.OverridePercentage(),
                        Away_Win_Any_Half = averageHomeAway.Away_Win_Any_Half.OverridePercentage(),

                        FT_15_Over = averageHomeAway.FT_15_Over.OverridePercentage(),
                        FT_25_Over = averageHomeAway.FT_25_Over.OverridePercentage(),
                        FT_35_Over = averageHomeAway.FT_35_Over.OverridePercentage(),

                        HT_05_Over = averageHomeAway.HT_05_Over.OverridePercentage(),
                        HT_15_Over = averageHomeAway.HT_15_Over.OverridePercentage(),

                        SH_05_Over = averageHomeAway.SH_05_Over.OverridePercentage(),
                        SH_15_Over = averageHomeAway.SH_15_Over.OverridePercentage(),

                        FT_GG = averageHomeAway.FT_GG.OverridePercentage(),
                        HT_GG = averageHomeAway.HT_GG.OverridePercentage(),
                        SH_GG = averageHomeAway.SH_GG.OverridePercentage(),

                        Is_FT_Win1 = averageHomeAway.Is_FT_Win1.OverridePercentage(),
                        Is_FT_X = averageHomeAway.Is_FT_X.OverridePercentage(),
                        Is_FT_Win2 = averageHomeAway.Is_FT_Win2.OverridePercentage(),

                        Is_HT_Win1 = averageHomeAway.Is_HT_Win1.OverridePercentage(),
                        Is_HT_X = averageHomeAway.Is_HT_X.OverridePercentage(),
                        Is_HT_Win2 = averageHomeAway.Is_HT_Win2.OverridePercentage(),

                        Is_SH_Win1 = averageHomeAway.Is_SH_Win1.OverridePercentage(),
                        Is_SH_X = averageHomeAway.Is_SH_X.OverridePercentage(),
                        Is_SH_Win2 = averageHomeAway.Is_SH_Win2.OverridePercentage(),

                        Average_FT_Corners_HomeTeam = averageHomeAway.Average_FT_Corners_HomeTeam,
                        Average_FT_Corners_AwayTeam = averageHomeAway.Average_FT_Corners_AwayTeam,

                        Is_Corner_FT_Win1 = averageHomeAway.Is_Corner_FT_Win1.OverridePercentage(),
                        Is_Corner_FT_X = averageHomeAway.Is_Corner_FT_X.OverridePercentage(),
                        Is_Corner_FT_Win2 = averageHomeAway.Is_Corner_FT_Win2.OverridePercentage(),

                        Corner_7_5_Over = averageHomeAway.Corner_7_5_Over.OverridePercentage(),
                        Corner_8_5_Over = averageHomeAway.Corner_8_5_Over.OverridePercentage(),
                        Corner_9_5_Over = averageHomeAway.Corner_9_5_Over.OverridePercentage(),

                        Corner_Home_3_5_Over = averageHomeAway.Corner_Home_3_5_Over.OverridePercentage(),
                        Corner_Home_4_5_Over = averageHomeAway.Corner_Home_4_5_Over.OverridePercentage(),
                        Corner_Home_5_5_Over = averageHomeAway.Corner_Home_5_5_Over.OverridePercentage(),
                        Corner_Away_3_5_Over = averageHomeAway.Corner_Away_3_5_Over.OverridePercentage(),
                        Corner_Away_4_5_Over = averageHomeAway.Corner_Away_4_5_Over.OverridePercentage(),
                        Corner_Away_5_5_Over = averageHomeAway.Corner_Away_5_5_Over.OverridePercentage(),
                        Average_FT_Shut_AwayTeam = averageHomeAway.Average_FT_Shot_AwayTeam,
                        Average_FT_Shut_HomeTeam = averageHomeAway.Average_FT_Shot_HomeTeam,
                        Average_FT_ShutOnTarget_AwayTeam = averageHomeAway.Average_FT_ShotOnTarget_AwayTeam,
                        Average_FT_ShutOnTarget_HomeTeam = averageHomeAway.Average_FT_ShotOnTarget_HomeTeam,
                        Home_Possesion = averageHomeAway.Average_FT_Possesion_HomeTeam.ConvertFromDecimal(),
                        Away_Possesion = averageHomeAway.Average_FT_Possesion_AwayTeam.ConvertFromDecimal()
                    };
                }

            }
            else
            {
                if (averageGeneral != null)
                {
                    result = new AverageStatisticsHolder
                    {
                        BySideType = bySideType,
                        Average_FT_Goals_HomeTeam = averageGeneral.Average_FT_Goals_HomeTeam,
                        Average_FT_Goals_AwayTeam = averageGeneral.Average_FT_Goals_AwayTeam,
                        Average_HT_Goals_HomeTeam = averageGeneral.Average_HT_Goals_HomeTeam,
                        Average_HT_Goals_AwayTeam = averageGeneral.Average_HT_Goals_AwayTeam,
                        Average_SH_Goals_HomeTeam = averageGeneral.Average_SH_Goals_HomeTeam,
                        Average_SH_Goals_AwayTeam = averageGeneral.Average_SH_Goals_AwayTeam,

                        Away_FT_05_Over = averageGeneral.Away_FT_05_Over.OverridePercentage(),
                        Away_FT_15_Over = averageGeneral.Away_FT_15_Over.OverridePercentage(),
                        Away_HT_05_Over = averageGeneral.Away_HT_05_Over.OverridePercentage(),
                        Away_HT_15_Over = averageGeneral.Away_HT_15_Over.OverridePercentage(),
                        Away_SH_05_Over = averageGeneral.Away_SH_05_Over.OverridePercentage(),
                        Away_SH_15_Over = averageGeneral.Away_SH_15_Over.OverridePercentage(),
                        Home_FT_05_Over = averageGeneral.Home_FT_05_Over.OverridePercentage(),
                        Home_FT_15_Over = averageGeneral.Home_FT_15_Over.OverridePercentage(),
                        Home_HT_05_Over = averageGeneral.Home_HT_05_Over.OverridePercentage(),
                        Home_HT_15_Over = averageGeneral.Home_HT_15_Over.OverridePercentage(),
                        Home_SH_05_Over = averageGeneral.Home_SH_05_Over.OverridePercentage(),
                        Home_SH_15_Over = averageGeneral.Home_SH_15_Over.OverridePercentage(),
                        Home_Win_Any_Half = averageGeneral.Home_Win_Any_Half.OverridePercentage(),
                        Away_Win_Any_Half = averageGeneral.Away_Win_Any_Half.OverridePercentage(),

                        FT_15_Over = averageGeneral.FT_15_Over.OverridePercentage(),
                        FT_25_Over = averageGeneral.FT_25_Over.OverridePercentage(),
                        FT_35_Over = averageGeneral.FT_35_Over.OverridePercentage(),

                        HT_05_Over = averageGeneral.HT_05_Over.OverridePercentage(),
                        HT_15_Over = averageGeneral.HT_15_Over.OverridePercentage(),

                        SH_05_Over = averageGeneral.SH_05_Over.OverridePercentage(),
                        SH_15_Over = averageGeneral.SH_15_Over.OverridePercentage(),

                        FT_GG = averageGeneral.FT_GG.OverridePercentage(),
                        HT_GG = averageGeneral.HT_GG.OverridePercentage(),
                        SH_GG = averageGeneral.SH_GG.OverridePercentage(),

                        Is_FT_Win1 = averageGeneral.Is_FT_Win1.OverridePercentage(),
                        Is_FT_X = averageGeneral.Is_FT_X.OverridePercentage(),
                        Is_FT_Win2 = averageGeneral.Is_FT_Win2.OverridePercentage(),

                        Is_HT_Win1 = averageGeneral.Is_HT_Win1.OverridePercentage(),
                        Is_HT_X = averageGeneral.Is_HT_X.OverridePercentage(),
                        Is_HT_Win2 = averageGeneral.Is_HT_Win2.OverridePercentage(),

                        Is_SH_Win1 = averageGeneral.Is_SH_Win1.OverridePercentage(),
                        Is_SH_X = averageGeneral.Is_SH_X.OverridePercentage(),
                        Is_SH_Win2 = averageGeneral.Is_SH_Win2.OverridePercentage(),

                        Average_FT_Corners_HomeTeam = averageGeneral.Average_FT_Corners_HomeTeam,
                        Average_FT_Corners_AwayTeam = averageGeneral.Average_FT_Corners_AwayTeam,

                        Is_Corner_FT_Win1 = averageGeneral.Is_Corner_FT_Win1.OverridePercentage(),
                        Is_Corner_FT_X = averageGeneral.Is_Corner_FT_X.OverridePercentage(),
                        Is_Corner_FT_Win2 = averageGeneral.Is_Corner_FT_Win2.OverridePercentage(),

                        Corner_7_5_Over = averageGeneral.Corner_7_5_Over.OverridePercentage(),
                        Corner_8_5_Over = averageGeneral.Corner_8_5_Over.OverridePercentage(),
                        Corner_9_5_Over = averageGeneral.Corner_9_5_Over.OverridePercentage(),

                        Corner_Home_3_5_Over = averageGeneral.Corner_Home_3_5_Over.OverridePercentage(),
                        Corner_Home_4_5_Over = averageGeneral.Corner_Home_4_5_Over.OverridePercentage(),
                        Corner_Home_5_5_Over = averageGeneral.Corner_Home_5_5_Over.OverridePercentage(),
                        Corner_Away_3_5_Over = averageGeneral.Corner_Away_3_5_Over.OverridePercentage(),
                        Corner_Away_4_5_Over = averageGeneral.Corner_Away_4_5_Over.OverridePercentage(),
                        Corner_Away_5_5_Over = averageGeneral.Corner_Away_5_5_Over.OverridePercentage(),

                        Average_FT_Shut_AwayTeam = averageGeneral.Average_FT_Shot_AwayTeam,
                        Average_FT_Shut_HomeTeam = averageGeneral.Average_FT_Shot_HomeTeam,
                        Average_FT_ShutOnTarget_AwayTeam = averageGeneral.Average_FT_ShotOnTarget_AwayTeam,
                        Average_FT_ShutOnTarget_HomeTeam = averageGeneral.Average_FT_ShotOnTarget_HomeTeam,
                        Home_Possesion = averageGeneral.Average_FT_Possesion_HomeTeam.ConvertFromDecimal(),
                        Away_Possesion = averageGeneral.Average_FT_Possesion_AwayTeam.ConvertFromDecimal()
                    };
                }
            }

            return result;
        }

        private static TeamPerformanceStatisticsHolder? GenerateHomeTeamPerformanceStatistic(FormPerformanceGuessContainer homePerformance, int bySideType)
        {
            TeamPerformanceStatisticsHolder result = null;

            if (homePerformance == null) return result;

            if ((int)BySideType.HomeAway == bySideType)
            {
                if (homePerformance.HomeAway != null)
                {
                    result = new TeamPerformanceStatisticsHolder
                    {
                        BySideType = bySideType,
                        HomeOrAway = (int)HomeOrAway.Home,
                        Average_FT_Goals_Team = homePerformance.HomeAway.Average_FT_Goals_HomeTeam,
                        Average_HT_Goals_Team = homePerformance.HomeAway.Average_HT_Goals_HomeTeam,
                        Average_SH_Goals_Team = homePerformance.HomeAway.Average_SH_Goals_HomeTeam,
                        Average_FT_Corners_Team = homePerformance.HomeAway.Average_FT_Corners_HomeTeam,

                        Team_FT_05_Over = homePerformance.HomeAway.Home_FT_05_Over.OverridePercentage(),
                        Team_FT_15_Over = homePerformance.HomeAway.Home_FT_15_Over.OverridePercentage(),
                        Team_HT_05_Over = homePerformance.HomeAway.Home_HT_05_Over.OverridePercentage(),
                        Team_HT_15_Over = homePerformance.HomeAway.Home_HT_15_Over.OverridePercentage(),
                        Team_SH_05_Over = homePerformance.HomeAway.Home_SH_05_Over.OverridePercentage(),
                        Team_SH_15_Over = homePerformance.HomeAway.Home_SH_15_Over.OverridePercentage(),
                        Team_Win_Any_Half = homePerformance.HomeAway.Home_Win_Any_Half.OverridePercentage(),

                        FT_15_Over = homePerformance.HomeAway.FT_15_Over.OverridePercentage(),
                        FT_25_Over = homePerformance.HomeAway.FT_25_Over.OverridePercentage(),
                        FT_35_Over = homePerformance.HomeAway.FT_35_Over.OverridePercentage(),

                        HT_05_Over = homePerformance.HomeAway.HT_05_Over.OverridePercentage(),
                        HT_15_Over = homePerformance.HomeAway.HT_15_Over.OverridePercentage(),

                        SH_05_Over = homePerformance.HomeAway.SH_05_Over.OverridePercentage(),
                        SH_15_Over = homePerformance.HomeAway.SH_15_Over.OverridePercentage(),

                        FT_GG = homePerformance.HomeAway.FT_GG.OverridePercentage(),
                        HT_GG = homePerformance.HomeAway.HT_GG.OverridePercentage(),
                        SH_GG = homePerformance.HomeAway.SH_GG.OverridePercentage(),

                        Is_FT_Win = homePerformance.HomeAway.Is_FT_Win1.OverridePercentage(),
                        Is_FT_X = homePerformance.HomeAway.Is_FT_X.OverridePercentage(),

                        Is_HT_Win = homePerformance.HomeAway.Is_HT_Win1.OverridePercentage(),
                        Is_HT_X = homePerformance.HomeAway.Is_HT_X.OverridePercentage(),

                        Is_SH_Win = homePerformance.HomeAway.Is_SH_Win1.OverridePercentage(),
                        Is_SH_X = homePerformance.HomeAway.Is_SH_X.OverridePercentage(),

                        Is_Corner_FT_Win = homePerformance.HomeAway.Is_Corner_FT_Win1.OverridePercentage(),
                        Is_Corner_FT_X = homePerformance.HomeAway.Is_Corner_FT_X.OverridePercentage(),

                        Corner_7_5_Over = homePerformance.HomeAway.Corner_7_5_Over.OverridePercentage(),
                        Corner_8_5_Over = homePerformance.HomeAway.Corner_8_5_Over.OverridePercentage(),
                        Corner_9_5_Over = homePerformance.HomeAway.Corner_9_5_Over.OverridePercentage(),

                        Corner_Team_3_5_Over = homePerformance.HomeAway.Corner_Home_3_5_Over.OverridePercentage(),
                        Corner_Team_4_5_Over = homePerformance.HomeAway.Corner_Home_4_5_Over.OverridePercentage(),
                        Corner_Team_5_5_Over = homePerformance.HomeAway.Corner_Home_5_5_Over.OverridePercentage(),

                        Average_FT_Shut_Team = homePerformance.HomeAway.Average_FT_Shot_HomeTeam,
                        Average_FT_ShutOnTarget_Team = homePerformance.HomeAway.Average_FT_ShotOnTarget_HomeTeam,
                        Team_Possesion = homePerformance.HomeAway.Average_FT_Possesion_HomeTeam.ConvertFromDecimal()
                    };
                }
            }
            else
            {
                if (homePerformance.General != null)
                {
                    result = new TeamPerformanceStatisticsHolder
                    {
                        BySideType = bySideType,
                        HomeOrAway = (int)HomeOrAway.Home,
                        Average_FT_Goals_Team = homePerformance.General.Average_FT_Goals_HomeTeam,
                        Average_HT_Goals_Team = homePerformance.General.Average_HT_Goals_HomeTeam,
                        Average_SH_Goals_Team = homePerformance.General.Average_SH_Goals_HomeTeam,
                        Average_FT_Corners_Team = homePerformance.General.Average_FT_Corners_HomeTeam,

                        Team_FT_05_Over = homePerformance.General.Home_FT_05_Over.OverridePercentage(),
                        Team_FT_15_Over = homePerformance.General.Home_FT_15_Over.OverridePercentage(),
                        Team_HT_05_Over = homePerformance.General.Home_HT_05_Over.OverridePercentage(),
                        Team_HT_15_Over = homePerformance.General.Home_HT_15_Over.OverridePercentage(),
                        Team_SH_05_Over = homePerformance.General.Home_SH_05_Over.OverridePercentage(),
                        Team_SH_15_Over = homePerformance.General.Home_SH_15_Over.OverridePercentage(),
                        Team_Win_Any_Half = homePerformance.General.Home_Win_Any_Half.OverridePercentage(),

                        FT_15_Over = homePerformance.General.FT_15_Over.OverridePercentage(),
                        FT_25_Over = homePerformance.General.FT_25_Over.OverridePercentage(),
                        FT_35_Over = homePerformance.General.FT_35_Over.OverridePercentage(),

                        HT_05_Over = homePerformance.General.HT_05_Over.OverridePercentage(),
                        HT_15_Over = homePerformance.General.HT_15_Over.OverridePercentage(),

                        SH_05_Over = homePerformance.General.SH_05_Over.OverridePercentage(),
                        SH_15_Over = homePerformance.General.SH_15_Over.OverridePercentage(),

                        FT_GG = homePerformance.General.FT_GG.OverridePercentage(),
                        HT_GG = homePerformance.General.HT_GG.OverridePercentage(),
                        SH_GG = homePerformance.General.SH_GG.OverridePercentage(),

                        Is_FT_Win = homePerformance.General.Is_FT_Win1.OverridePercentage(),
                        Is_FT_X = homePerformance.General.Is_FT_X.OverridePercentage(),

                        Is_HT_Win = homePerformance.General.Is_HT_Win1.OverridePercentage(),
                        Is_HT_X = homePerformance.General.Is_HT_X.OverridePercentage(),

                        Is_SH_Win = homePerformance.General.Is_SH_Win1.OverridePercentage(),
                        Is_SH_X = homePerformance.General.Is_SH_X.OverridePercentage(),

                        Is_Corner_FT_Win = homePerformance.General.Is_Corner_FT_Win1.OverridePercentage(),
                        Is_Corner_FT_X = homePerformance.General.Is_Corner_FT_X.OverridePercentage(),

                        Corner_7_5_Over = homePerformance.General.Corner_7_5_Over.OverridePercentage(),
                        Corner_8_5_Over = homePerformance.General.Corner_8_5_Over.OverridePercentage(),
                        Corner_9_5_Over = homePerformance.General.Corner_9_5_Over.OverridePercentage(),

                        Corner_Team_3_5_Over = homePerformance.General.Corner_Home_3_5_Over.OverridePercentage(),
                        Corner_Team_4_5_Over = homePerformance.General.Corner_Home_4_5_Over.OverridePercentage(),
                        Corner_Team_5_5_Over = homePerformance.General.Corner_Home_5_5_Over.OverridePercentage(),

                        Average_FT_Shut_Team = homePerformance.General.Average_FT_Shot_HomeTeam,
                        Average_FT_ShutOnTarget_Team = homePerformance.General.Average_FT_ShotOnTarget_HomeTeam,
                        Team_Possesion = homePerformance.General.Average_FT_Possesion_HomeTeam.ConvertFromDecimal()
                    };
                }
            }

            return result;
        }

        private static TeamPerformanceStatisticsHolder GenerateAwayTeamPerformanceStatistic(FormPerformanceGuessContainer awayPerformance, int bySideType)
        {
            // Initialize the result variable
            TeamPerformanceStatisticsHolder result = null;

            // Check if the input object is null and return null if so
            if (awayPerformance == null)
            {
                return result;
            }

            // Check the value of the bySideType parameter
            if ((int)BySideType.HomeAway == bySideType)
            {
                // If the input object has the HomeAway property, create the result object with its values
                if (awayPerformance.HomeAway != null)
                {
                    result = new TeamPerformanceStatisticsHolder
                    {
                        BySideType = bySideType,
                        HomeOrAway = (int)HomeOrAway.Away,
                        Average_FT_Goals_Team = awayPerformance.HomeAway.Average_FT_Goals_AwayTeam,
                        Average_HT_Goals_Team = awayPerformance.HomeAway.Average_HT_Goals_AwayTeam,
                        Average_SH_Goals_Team = awayPerformance.HomeAway.Average_SH_Goals_AwayTeam,
                        Average_FT_Corners_Team = awayPerformance.HomeAway.Average_FT_Corners_AwayTeam,

                        Team_FT_05_Over = awayPerformance.HomeAway.Away_FT_05_Over.OverridePercentage(),
                        Team_FT_15_Over = awayPerformance.HomeAway.Away_FT_15_Over.OverridePercentage(),
                        Team_HT_05_Over = awayPerformance.HomeAway.Away_HT_05_Over.OverridePercentage(),
                        Team_HT_15_Over = awayPerformance.HomeAway.Away_HT_15_Over.OverridePercentage(),
                        Team_SH_05_Over = awayPerformance.HomeAway.Away_SH_05_Over.OverridePercentage(),
                        Team_SH_15_Over = awayPerformance.HomeAway.Away_SH_15_Over.OverridePercentage(),
                        Team_Win_Any_Half = awayPerformance.HomeAway.Away_Win_Any_Half.OverridePercentage(),

                        FT_15_Over = awayPerformance.HomeAway.FT_15_Over.OverridePercentage(),
                        FT_25_Over = awayPerformance.HomeAway.FT_25_Over.OverridePercentage(),
                        FT_35_Over = awayPerformance.HomeAway.FT_35_Over.OverridePercentage(),

                        HT_05_Over = awayPerformance.HomeAway.HT_05_Over.OverridePercentage(),
                        HT_15_Over = awayPerformance.HomeAway.HT_15_Over.OverridePercentage(),

                        SH_05_Over = awayPerformance.HomeAway.SH_05_Over.OverridePercentage(),
                        SH_15_Over = awayPerformance.HomeAway.SH_15_Over.OverridePercentage(),

                        FT_GG = awayPerformance.HomeAway.FT_GG.OverridePercentage(),
                        HT_GG = awayPerformance.HomeAway.HT_GG.OverridePercentage(),
                        SH_GG = awayPerformance.HomeAway.SH_GG.OverridePercentage(),

                        Is_FT_Win = awayPerformance.HomeAway.Is_FT_Win2.OverridePercentage(),
                        Is_FT_X = awayPerformance.HomeAway.Is_FT_X.OverridePercentage(),

                        Is_HT_Win = awayPerformance.HomeAway.Is_HT_Win2.OverridePercentage(),
                        Is_HT_X = awayPerformance.HomeAway.Is_HT_X.OverridePercentage(),

                        Is_SH_Win = awayPerformance.HomeAway.Is_SH_Win2.OverridePercentage(),
                        Is_SH_X = awayPerformance.HomeAway.Is_SH_X.OverridePercentage(),

                        Is_Corner_FT_Win = awayPerformance.HomeAway.Is_Corner_FT_Win2.OverridePercentage(),
                        Is_Corner_FT_X = awayPerformance.HomeAway.Is_Corner_FT_X.OverridePercentage(),

                        Corner_7_5_Over = awayPerformance.HomeAway.Corner_7_5_Over.OverridePercentage(),
                        Corner_8_5_Over = awayPerformance.HomeAway.Corner_8_5_Over.OverridePercentage(),
                        Corner_9_5_Over = awayPerformance.HomeAway.Corner_9_5_Over.OverridePercentage(),

                        Corner_Team_3_5_Over = awayPerformance.HomeAway.Corner_Away_3_5_Over.OverridePercentage(),
                        Corner_Team_4_5_Over = awayPerformance.HomeAway.Corner_Away_4_5_Over.OverridePercentage(),
                        Corner_Team_5_5_Over = awayPerformance.HomeAway.Corner_Away_5_5_Over.OverridePercentage(),

                        Average_FT_Shut_Team = awayPerformance.HomeAway.Average_FT_Shot_AwayTeam,
                        Average_FT_ShutOnTarget_Team = awayPerformance.HomeAway.Average_FT_ShotOnTarget_AwayTeam,
                        Team_Possesion = awayPerformance.HomeAway.Average_FT_Possesion_AwayTeam.ConvertFromDecimal()
                    };
                }
            }
            else
            {
                // If the input object has the General property, create the result object with its values
                if (awayPerformance.General != null)
                {
                    result = new TeamPerformanceStatisticsHolder
                    {
                        BySideType = bySideType,
                        HomeOrAway = (int)HomeOrAway.Away,
                        Average_FT_Goals_Team = awayPerformance.General.Average_FT_Goals_AwayTeam,
                        Average_HT_Goals_Team = awayPerformance.General.Average_HT_Goals_AwayTeam,
                        Average_SH_Goals_Team = awayPerformance.General.Average_SH_Goals_AwayTeam,
                        Average_FT_Corners_Team = awayPerformance.General.Average_FT_Corners_AwayTeam,

                        Team_FT_05_Over = awayPerformance.General.Away_FT_05_Over.OverridePercentage(),
                        Team_FT_15_Over = awayPerformance.General.Away_FT_15_Over.OverridePercentage(),
                        Team_HT_05_Over = awayPerformance.General.Away_HT_05_Over.OverridePercentage(),
                        Team_HT_15_Over = awayPerformance.General.Away_HT_15_Over.OverridePercentage(),
                        Team_SH_05_Over = awayPerformance.General.Away_SH_05_Over.OverridePercentage(),
                        Team_SH_15_Over = awayPerformance.General.Away_SH_15_Over.OverridePercentage(),
                        Team_Win_Any_Half = awayPerformance.General.Away_Win_Any_Half.OverridePercentage(),

                        FT_15_Over = awayPerformance.General.FT_15_Over.OverridePercentage(),
                        FT_25_Over = awayPerformance.General.FT_25_Over.OverridePercentage(),
                        FT_35_Over = awayPerformance.General.FT_35_Over.OverridePercentage(),

                        HT_05_Over = awayPerformance.General.HT_05_Over.OverridePercentage(),
                        HT_15_Over = awayPerformance.General.HT_15_Over.OverridePercentage(),

                        SH_05_Over = awayPerformance.General.SH_05_Over.OverridePercentage(),
                        SH_15_Over = awayPerformance.General.SH_15_Over.OverridePercentage(),

                        FT_GG = awayPerformance.General.FT_GG.OverridePercentage(),
                        HT_GG = awayPerformance.General.HT_GG.OverridePercentage(),
                        SH_GG = awayPerformance.General.SH_GG.OverridePercentage(),

                        Is_FT_Win = awayPerformance.General.Is_FT_Win2.OverridePercentage(),
                        Is_FT_X = awayPerformance.General.Is_FT_X.OverridePercentage(),

                        Is_HT_Win = awayPerformance.General.Is_HT_Win2.OverridePercentage(),
                        Is_HT_X = awayPerformance.General.Is_HT_X.OverridePercentage(),

                        Is_SH_Win = awayPerformance.General.Is_SH_Win2.OverridePercentage(),
                        Is_SH_X = awayPerformance.General.Is_SH_X.OverridePercentage(),

                        Is_Corner_FT_Win = awayPerformance.General.Is_Corner_FT_Win2.OverridePercentage(),
                        Is_Corner_FT_X = awayPerformance.General.Is_Corner_FT_X.OverridePercentage(),
                        Corner_7_5_Over = awayPerformance.General.Corner_7_5_Over.OverridePercentage(),
                        Corner_8_5_Over = awayPerformance.General.Corner_8_5_Over.OverridePercentage(),
                        Corner_9_5_Over = awayPerformance.General.Corner_9_5_Over.OverridePercentage(),

                        Corner_Team_3_5_Over = awayPerformance.General.Corner_Away_3_5_Over.OverridePercentage(),
                        Corner_Team_4_5_Over = awayPerformance.General.Corner_Away_4_5_Over.OverridePercentage(),
                        Corner_Team_5_5_Over = awayPerformance.General.Corner_Away_5_5_Over.OverridePercentage(),

                        Average_FT_Shut_Team = awayPerformance.General.Average_FT_Shot_AwayTeam,
                        Average_FT_ShutOnTarget_Team = awayPerformance.General.Average_FT_ShotOnTarget_AwayTeam,
                        Team_Possesion = awayPerformance.General.Average_FT_Possesion_AwayTeam.ConvertFromDecimal()
                    };
                }
            }

            return result;
        }

        // OverridePercentage method is used to convert a PercentageComplainer object's percentage value to either itself or its complement
        private static int OverridePercentage(this PercentageComplainer percentageComplainer)
        {
            if (percentageComplainer == null) return -999;

            bool isFeatureEnabled = percentageComplainer.FeatureName.ToLower() == "true";

            if (!isFeatureEnabled) // false
            {
                return 100 - percentageComplainer.Percentage;
            }
            else
            {
                return percentageComplainer.Percentage;
            }
        }

        private static int ConvertFromDecimal(this decimal pointerValue)
        {
            if (pointerValue < 0) return -1;
            return Convert.ToInt32(pointerValue);
        }


        public static StandingAiModel? MapAiStandingModel(this StandingInfoModel? inputModel, string homeTeamName)
        {
            if(inputModel == null) return null;

            var homeStandingInfo = inputModel.UpTeam.TeamName == homeTeamName ? inputModel.UpTeam : inputModel.DownTeam;
            var awayStandingInfo = inputModel.UpTeam.TeamName == homeTeamName ? inputModel.DownTeam : inputModel.UpTeam;

            return new StandingAiModel
            {
                HomeTeam_StandingDetails = new StandingTeamAiDetailsModel
                {
                    TeamName = homeStandingInfo.TeamName,
                    Order = homeStandingInfo.Order,
                    Point = homeStandingInfo.Point,
                    MatchesCount = homeStandingInfo.MatchesCount,
                    WinsCount = homeStandingInfo.WinsCount,
                    DrawsCount = homeStandingInfo.DrawsCount,
                    LostsCount = homeStandingInfo.LostsCount
                },
                AwayTeam_StandingDetails = new StandingTeamAiDetailsModel
                {
                    TeamName = awayStandingInfo.TeamName,
                    Order = awayStandingInfo.Order,
                    Point = awayStandingInfo.Point,
                    MatchesCount = awayStandingInfo.MatchesCount,
                    WinsCount = awayStandingInfo.WinsCount,
                    DrawsCount = awayStandingInfo.DrawsCount,
                    LostsCount = awayStandingInfo.LostsCount
                }
            };
        }


        public static LeagueStatisticsAiModel? MapLeagueStatisticsAiModel(this LeagueStatisticsHolder? inputModel)
        {
            if (inputModel == null) return null;

            return new LeagueStatisticsAiModel
            {
                CountryName = inputModel.CountryName,
                LeagueName = inputModel.LeagueName,
                FullTime_Goals_Average = Math.Round(inputModel.FT_GoalsAverage, 2),
                HalfTime_Goals_Average = Math.Round(inputModel.HT_GoalsAverage, 2),
                SecondHald_Goals_Average = Math.Round(inputModel.SH_GoalsAverage, 2),
                BothTeamsToScore_Percentage = inputModel.GG_Percentage,
                FullTime_Over15_Percentage = inputModel.FT_Over15_Percentage,
                FullTime_Over25_Percentage = inputModel.FT_Over25_Percentage,
                FullTime_Over35_Percentage = inputModel.FT_Over35_Percentage,
                HalfTime_Over05_Percentage = inputModel.HT_Over05_Percentage,
                HalfTime_Over15_Percentage = inputModel.HT_Over15_Percentage,
                SecondHalf_Over05_Percentage = inputModel.SH_Over05_Percentage,
                SecondHalf_Over15_Percentage = inputModel.SH_Over15_Percentage
            };
        }

        public static ComparisonAiStatisticsHolder? MapToComparisonAiModel(this ComparisonStatisticsHolder? input)
        {
            if (input == null)
                return null;

            return new ComparisonAiStatisticsHolder
            {
                Average_FT_Goals_AwayTeam = Math.Round(input.Average_FT_Goals_AwayTeam, 2),
                Average_FT_Goals_HomeTeam = Math.Round(input.Average_FT_Goals_HomeTeam, 2),
                Average_HT_Goals_AwayTeam = Math.Round(input.Average_HT_Goals_AwayTeam, 2),
                Average_HT_Goals_HomeTeam = Math.Round(input.Average_HT_Goals_HomeTeam, 2),
                Average_SH_Goals_AwayTeam = Math.Round(input.Average_SH_Goals_AwayTeam, 2),
                Average_SH_Goals_HomeTeam = Math.Round(input.Average_SH_Goals_HomeTeam, 2),
                Away_FT_05_Over_Percent = input.Away_FT_05_Over,
                Away_FT_15_Over_Percent = input.Away_FT_15_Over,
                Away_HT_05_Over_Percent = input.Away_HT_05_Over,
                Away_HT_15_Over_Percent = input.Away_HT_15_Over,
                Away_SH_05_Over_Percent = input.Away_SH_05_Over,
                Away_SH_15_Over_Percent = input.Away_SH_15_Over,
                Home_FT_05_Over_Percent = input.Home_FT_05_Over,
                Home_FT_15_Over_Percent = input.Home_FT_15_Over,
                Home_HT_05_Over_Percent = input.Home_HT_05_Over,
                Home_HT_15_Over_Percent = input.Home_HT_15_Over,
                Home_SH_05_Over_Percent = input.Home_SH_05_Over,
                Home_SH_15_Over_Percent = input.Home_SH_15_Over,
                Home_Win_Any_Half_Percent = input.Home_Win_Any_Half,
                Away_Win_Any_Half_Percent = input.Away_Win_Any_Half,
                FT_15_Over_Percent = input.FT_15_Over,
                FT_25_Over_Percent = input.FT_25_Over,
                FT_35_Over_Percent = input.FT_35_Over,
                HT_05_Over_Percent = input.HT_05_Over,
                HT_15_Over_Percent = input.HT_15_Over,
                SH_05_Over_Percent = input.SH_05_Over,
                SH_15_Over_Percent = input.SH_15_Over,
                FT_BothTeamToScore_Percent = input.FT_GG,
                HT_BothTeamToScore_Percent = input.HT_GG,
                SH_BothTeamToScore_Percent = input.SH_GG,
                FT_Home_Win_Percent = input.Is_FT_Win1,
                FT_Draw_Percent = input.Is_FT_X,
                FT_Away_Win_Percent = input.Is_FT_Win2,
                HT_Home_Win_Percent = input.Is_HT_Win1,
                HT_Draw_Percent = input.Is_HT_X,
                HT_Away_Win_Percent = input.Is_HT_Win2,
                SH_Home_Win_Percent = input.Is_SH_Win1,
                SH_Draw_Percent = input.Is_SH_X,
                SH_Away_Win_Percent = input.Is_SH_Win2
            };
        }

        public static PerformanceAiStatisticsHolder? MapToComparisonAiModel(this TeamPerformanceStatisticsHolder? input)
        {
            if (input == null)
                return null;

            return new PerformanceAiStatisticsHolder
            {
                Team_Average_FT_Goals = Math.Round(input.Average_FT_Goals_Team, 2),
                Team_Average_HT_Goals = Math.Round(input.Average_HT_Goals_Team, 2),
                Team_Average_SH_Goals = Math.Round(input.Average_SH_Goals_Team, 2),
                Team_FT_05_Over_Percent = input.Team_FT_05_Over,
                Team_FT_15_Over_Percent = input.Team_FT_15_Over,
                Team_HT_05_Over_Percent = input.Team_HT_05_Over,
                Team_HT_15_Over_Percent = input.Team_HT_15_Over,
                Team_SH_05_Over_Percent = input.Team_SH_05_Over,
                Team_SH_15_Over_Percent = input.Team_SH_15_Over,
                Team_Win_Any_Half_Percent = input.Team_Win_Any_Half,
                FT_15_Over_Percent = input.FT_15_Over,
                FT_25_Over_Percent = input.FT_25_Over,
                FT_35_Over_Percent = input.FT_35_Over,
                HT_05_Over_Percent = input.HT_05_Over,
                HT_15_Over_Percent = input.HT_15_Over,
                SH_05_Over_Percent = input.SH_05_Over,
                SH_15_Over_Percent = input.SH_15_Over,
                FT_BothTeamToScore_Percent = input.FT_GG,
                HT_BothTeamToScore_Percent = input.HT_GG,
                SH_BothTeamToScore_Percent = input.SH_GG,
                FT_Win_Percent = input.Is_FT_Win,
                FT_Draw_Percent = input.Is_FT_X,
                HT_Win_Percent = input.Is_HT_Win,
                HT_Draw_Percent = input.Is_HT_X,
                SH_Win_Percent = input.Is_SH_Win,
                SH_Draw_Percent = input.Is_SH_X,
                MoreMatchInfoDetails = input.Average_FT_Shut_Team >= 0 && input.Team_Possesion >= 1 ? new PerformanceAiMoreDetailsHolder
                {
                    Team_Average_BallPossesion_Percent = input.Team_Possesion,
                    Team_Average_FT_Shot = Math.Round(input.Average_FT_Shut_Team, 2),
                    Team_Average_FT_ShotOnTarget = Math.Round(input.Average_FT_ShutOnTarget_Team, 2)
                } : null
            };
        }


        private static bool CheckIsAddable(TimeSerialContainer model, bool analyseAnyTime = false)
        {
            if (analyseAnyTime)
            {
                return analyseAnyTime;
            }

            if (model.IsAnalysed || string.IsNullOrEmpty(model.Time) || model.Time.Length < 5)
            {
                return false;
            }

            // Convert server time to local time
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time");
            DateTimeOffset serverTime = DateTimeOffset.Now;
            DateTimeOffset localTime = TimeZoneInfo.ConvertTime(serverTime, timeZoneInfo);

            // Define time limits for adding time
            TimeSpan nowTime = localTime.TimeOfDay;
            TimeSpan minTime = nowTime.Subtract(TimeSpan.Parse("00:05"));
            TimeSpan maxTime = nowTime.Add(TimeSpan.Parse("00:20"));
            TimeSpan matchTime = TimeSpan.Parse(model.Time);

            // Check if the match time is within the time limits
            return minTime <= matchTime && maxTime >= matchTime;
        }
    }
}
