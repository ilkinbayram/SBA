using Core.Entities.Concrete;
using Core.Entities.Concrete.Base;
using Core.Entities.Concrete.ComplexModels.ML;
using Core.Entities.Concrete.ComplexModels.Sql;
using Core.Entities.Concrete.ExternalDbEntities;
using Core.Extensions;
using Core.Resources.Constants;
using Core.Resources.Enums;
using Core.Utilities.Helpers;
using Core.Utilities.Results;
using Core.Utilities.UsableModel;
using Core.Utilities.UsableModel.BaseModels;
using Core.Utilities.UsableModel.TempTableModels.Country;
using Core.Utilities.UsableModel.TempTableModels.Initialization;
using Core.Utilities.UsableModel.Visualisers;
using Newtonsoft.Json;
using SBA.Business.Abstract;
using SBA.Business.FunctionalServices.Concrete;
using SBA.Business.Mapping;
using System.Text.RegularExpressions;

namespace SBA.Business.BusinessHelper
{
    public static class OperationalProcessor
    {
        private static string _defaultMatchUrl = ConfigHelper.GetSettingsDataStatic<string>(ParentKeySettings.UriTemplate.ToString(), ChildKeySettings.ConcatSerialDefaultMatch.ToString());
        private static string _comparisonMatchUrl = ConfigHelper.GetSettingsDataStatic<string>(ParentKeySettings.UriTemplate.ToString(), ChildKeySettings.ConcatComparisonMatch.ToString());

        private static string _pathTextFiles = ConfigHelper.GetSettingsDataStatic<string>(ParentKeySettings.PathConstant.ToString(), ChildKeySettings.TextPathFormat.ToString());
        private static string _pathJsonFiles = ConfigHelper.GetSettingsDataStatic<string>(ParentKeySettings.PathConstant.ToString(), ChildKeySettings.JsonPathFormat.ToString());

        public static List<OfferanceAnalyserContainer> OfferanceAnalysers = new List<OfferanceAnalyserContainer>();
        public static List<TimeSerialContainer> TimeSerialListContainer = new List<TimeSerialContainer>();

        private static readonly QuickConvert _quickConvertService = new QuickConvert();

        private static readonly WebOperation _webOperator = new WebOperation();

        private static readonly MatchInfoProceeder _proceeder = new MatchInfoProceeder();

        public static void InitialiseTimeSerials(string filePath)
        {
            List<TimeSerialContainer> cachedTimeSerials;

            using (StreamReader srCacheReader = new StreamReader(filePath))
            {
                var existFileData = srCacheReader.ReadToEnd();
                cachedTimeSerials = JsonConvert.DeserializeObject<List<TimeSerialContainer>>(existFileData);
            }

            if (cachedTimeSerials != null)
            {
                TimeSerialListContainer = cachedTimeSerials;
                return;
            }

            using (StreamReader sr = new StreamReader(filePath))
            {
                var serials = sr.ReadToEnd().Split(new string[] { "\r\n", "\t\n", "\n", "|" }, StringSplitOptions.None).ToList();

                var result = _proceeder.GetTimeSerialContainers(serials);
                cachedTimeSerials = result;
                TimeSerialListContainer = result;
            }

            using (StreamWriter sw = new StreamWriter(filePath))
            {
                var serializedCahceTimeSerial = JsonConvert.SerializeObject(TimeSerialListContainer, Formatting.Indented);
                sw.Write(serializedCahceTimeSerial);
            }
        }

        public static List<FilterResult> GetFilterResultsFromMatchInfoContainer(List<MatchInfoContainer> matchInfoContainers)
        {
            List<FilterResult> result = new List<FilterResult>();
            foreach (var matchInfo in matchInfoContainers)
            {
                result.Add(ConvertToFilterResultFromMatchInfoContainer(matchInfo));
            }
            return result;
        }

        public static FilterResult GetSingleFilterResultFromMatchInfoContainer(MatchInfoContainer matchInfoContainer)
        {
            FilterResult result = ConvertToFilterResultFromMatchInfoContainer(matchInfoContainer);
            return result;
        }

        private static FilterResult ConvertToFilterResultFromTeamPercentageProfiler(TeamPercentageProfiler profiler)
        {
            bool checkMatchRes = false;
            if (profiler != null &&
                !string.IsNullOrEmpty(profiler.ZEND_HT_Result) &&
                !string.IsNullOrEmpty(profiler.ZEND_FT_Result))
            {
                checkMatchRes = profiler.ZEND_HT_Result.Replace(" ", string.Empty).Replace("P", string.Empty).Split("-").Length == 2 &&
                profiler.ZEND_FT_Result.Replace(" ", string.Empty).Replace("P", string.Empty).Split("-").Length == 2;
            }

            if (!checkMatchRes) return null;

            var HT_HomeGoals = Convert.ToInt32(profiler.ZEND_HT_Result.Replace(" ", "").Trim().Split('-')[0]);
            var HT_AwayGoals = Convert.ToInt32(profiler.ZEND_HT_Result.Replace(" ", "").Trim().Split('-')[1]);

            var FT_HomeGoals = Convert.ToInt32(profiler.ZEND_FT_Result.Replace(" ", "").Trim().Split('-')[0]);
            var FT_AwayGoals = Convert.ToInt32(profiler.ZEND_FT_Result.Replace(" ", "").Trim().Split('-')[1]);

            var SH_HomeGoals = FT_HomeGoals - HT_HomeGoals;
            var SH_AwayGoals = FT_AwayGoals - HT_AwayGoals;

            var result = new FilterResult
            {
                Away_HT_0_5_Over = HT_AwayGoals > 0,
                Away_HT_1_5_Over = HT_AwayGoals > 1,
                Away_SH_0_5_Over = SH_AwayGoals > 0,
                Away_SH_1_5_Over = SH_AwayGoals > 1,
                Away_FT_0_5_Over = FT_AwayGoals > 0,
                Away_FT_1_5_Over = FT_AwayGoals > 1,

                Home_HT_0_5_Over = HT_HomeGoals > 0,
                Home_HT_1_5_Over = HT_HomeGoals > 1,
                Home_SH_0_5_Over = SH_HomeGoals > 0,
                Home_SH_1_5_Over = SH_HomeGoals > 1,
                Home_FT_0_5_Over = FT_HomeGoals > 0,
                Home_FT_1_5_Over = FT_HomeGoals > 1,

                Away_Win_Any_Half = HT_AwayGoals > HT_HomeGoals || SH_AwayGoals > SH_HomeGoals,
                Home_Win_Any_Half = HT_HomeGoals > HT_AwayGoals || SH_HomeGoals > SH_AwayGoals,

                FT_1_5_Over = (FT_HomeGoals + FT_AwayGoals) > 1,
                FT_2_5_Over = (FT_HomeGoals + FT_AwayGoals) > 2,
                FT_3_5_Over = (FT_HomeGoals + FT_AwayGoals) > 3,

                HT_0_5_Over = (HT_HomeGoals + HT_AwayGoals) > 0,
                HT_1_5_Over = (HT_HomeGoals + HT_AwayGoals) > 1,
                SH_0_5_Over = (SH_HomeGoals + SH_AwayGoals) > 0,
                SH_1_5_Over = (SH_HomeGoals + SH_AwayGoals) > 1,

                SH_Result = CalculateMatchWinner(SH_HomeGoals, SH_AwayGoals),
                HT_Result = CalculateMatchWinner(HT_HomeGoals, HT_AwayGoals),
                FT_Result = CalculateMatchWinner(FT_HomeGoals, FT_AwayGoals),

                ModelType = ProjectModelType.FilterResult,
                FT_GG = FT_HomeGoals > 0 && FT_AwayGoals > 0,
                SH_GG = SH_HomeGoals > 0 && SH_AwayGoals > 0,
                HT_GG = HT_HomeGoals > 0 && HT_AwayGoals > 0,
                SerialUniqueID = Convert.ToInt32(profiler.Serial),
                MoreGoalsBetweenTimes = CalculateMoreGoalsBetweenTimes((HT_HomeGoals + HT_AwayGoals), (SH_HomeGoals + SH_AwayGoals)),
                FT_TotalBetween = CalculateGoalsBetweenCount(FT_HomeGoals + FT_AwayGoals),
                HT_FT_Result = CalculateHalfFullResult(HT_HomeGoals, FT_HomeGoals, HT_AwayGoals, FT_AwayGoals)
            };

            return result;
        }

        private static FilterResult ConvertToFilterResultFromMatchInfoContainer(MatchInfoContainer matchInfo)
        {
            var result = new FilterResult
            {
                Away_HT_0_5_Over = matchInfo.Away_HT_Goals_Count > 0,
                Away_HT_1_5_Over = matchInfo.Away_HT_Goals_Count > 1,
                Away_SH_0_5_Over = matchInfo.Away_SH_Goals_Count > 0,
                Away_SH_1_5_Over = matchInfo.Away_SH_Goals_Count > 1,
                Away_FT_0_5_Over = matchInfo.Away_FT_Goals_Count > 0,
                Away_FT_1_5_Over = matchInfo.Away_FT_Goals_Count > 1,

                Home_HT_0_5_Over = matchInfo.Home_HT_Goals_Count > 0,
                Home_HT_1_5_Over = matchInfo.Home_HT_Goals_Count > 1,
                Home_SH_0_5_Over = matchInfo.Home_SH_Goals_Count > 0,
                Home_SH_1_5_Over = matchInfo.Home_SH_Goals_Count > 1,
                Home_FT_0_5_Over = matchInfo.Home_FT_Goals_Count > 0,
                Home_FT_1_5_Over = matchInfo.Home_FT_Goals_Count > 1,

                Away_Win_Any_Half = matchInfo.Away_HT_Goals_Count > matchInfo.Home_HT_Goals_Count || matchInfo.Away_SH_Goals_Count > matchInfo.Home_SH_Goals_Count,
                Home_Win_Any_Half = matchInfo.Home_HT_Goals_Count > matchInfo.Away_HT_Goals_Count || matchInfo.Home_SH_Goals_Count > matchInfo.Away_SH_Goals_Count,

                FT_1_5_Over = matchInfo.FT_Goals_Count > 1,
                FT_2_5_Over = matchInfo.FT_Goals_Count > 2,
                FT_3_5_Over = matchInfo.FT_Goals_Count > 3,

                HT_0_5_Over = matchInfo.HT_Goals_Count > 0,
                HT_1_5_Over = matchInfo.HT_Goals_Count > 1,
                SH_0_5_Over = matchInfo.SH_Goals_Count > 0,
                SH_1_5_Over = matchInfo.SH_Goals_Count > 1,

                SH_Result = CalculateMatchWinner(matchInfo.Home_SH_Goals_Count, matchInfo.Away_SH_Goals_Count),
                HT_Result = CalculateMatchWinner(matchInfo.Home_HT_Goals_Count, matchInfo.Away_HT_Goals_Count),
                FT_Result = CalculateMatchWinner(matchInfo.Home_FT_Goals_Count, matchInfo.Away_FT_Goals_Count),

                ModelType = ProjectModelType.FilterResult,
                FT_GG = matchInfo.Home_FT_Goals_Count > 0 && matchInfo.Away_FT_Goals_Count > 0,
                SH_GG = matchInfo.Home_SH_Goals_Count > 0 && matchInfo.Away_SH_Goals_Count > 0,
                HT_GG = matchInfo.Home_HT_Goals_Count > 0 && matchInfo.Away_HT_Goals_Count > 0,
                SerialUniqueID = Convert.ToInt32(matchInfo.Serial),
                MoreGoalsBetweenTimes = CalculateMoreGoalsBetweenTimes(matchInfo.HT_Goals_Count, matchInfo.SH_Goals_Count),
                FT_TotalBetween = CalculateGoalsBetweenCount(matchInfo.FT_Goals_Count),
                HT_FT_Result = CalculateHalfFullResult(matchInfo.Home_HT_Goals_Count, matchInfo.Home_FT_Goals_Count, matchInfo.Away_HT_Goals_Count, matchInfo.Away_FT_Goals_Count)
            };

            return result;
        }

        private static int CalculateMoreGoalsBetweenTimes(int firstTimeGoals, int secondTimeGoals)
        {
            int result = 9;

            if (firstTimeGoals > secondTimeGoals)
                result = 1;
            if (firstTimeGoals < secondTimeGoals)
                result = 2;

            return result;
        }

        private static HalfFullResultEnum CalculateHalfFullResult(int homeHT_goals, int homeFT_goals, int awayHT_goals, int awayFT_goals)
        {
            int htResult = CalculateMatchWinner(homeHT_goals, awayHT_goals);
            int ftResult = CalculateMatchWinner(homeFT_goals, awayFT_goals);
            var indexEnum = (htResult * 10) + ftResult;
            HalfFullResultEnum result = (HalfFullResultEnum)indexEnum;
            return result;
        }

        private static int CalculateGoalsBetweenCount(int ftGoalsCount)
        {
            int result = 10;

            if (ftGoalsCount >= 2 && ftGoalsCount <= 3)
                result = 23;
            if (ftGoalsCount >= 4 && ftGoalsCount <= 5)
                result = 45;
            if (ftGoalsCount >= 6)
                result = 6;

            return result;
        }

        private static int CalculateMatchWinner(int homeGoals, int awayGoals)
        {
            int result = 9;

            if (homeGoals > awayGoals)
                result = 1;
            if (homeGoals < awayGoals)
                result = 2;

            return result;
        }

        public static List<FilterResult> GetSpecializedFilterResults(MatchOddResponseInTimeModel inTimeModel, SystemCheckerContainer checkerContainer, IMatchBetService matchBetService, IFilterResultService filterResultService)
        {
            var allMatchBet = matchBetService.GetList(x => x.MatchDate > checkerContainer.FilterFromDate && x.MatchDate < checkerContainer.FilterToDate).Data;

            if (checkerContainer.IsHT_15_OU_Checked && allMatchBet != null && allMatchBet.Count > 0)
                allMatchBet = allMatchBet.Where(x => x.HT_Over_1_5_Odd == inTimeModel.HT_15_Over && x.HT_Under_1_5_Odd == inTimeModel.HT_15_Under).ToList();
            if (checkerContainer.IsFT_15_OU_Checked && allMatchBet != null && allMatchBet.Count > 0)
                allMatchBet = allMatchBet.Where(x => x.FT_Over_1_5_Odd == inTimeModel.FT_15_Over && x.FT_Under_1_5_Odd == inTimeModel.FT_15_Under).ToList();
            if (checkerContainer.IsFT_25_OU_Checked && allMatchBet != null && allMatchBet.Count > 0)
                allMatchBet = allMatchBet.Where(x => x.FT_Over_2_5_Odd == inTimeModel.FT_25_Over && x.FT_Under_2_5_Odd == inTimeModel.FT_25_Under).ToList();
            if (checkerContainer.IsFT_35_OU_Checked && allMatchBet != null && allMatchBet.Count > 0)
                allMatchBet = allMatchBet.Where(x => x.FT_Over_3_5_Odd == inTimeModel.FT_35_Over && x.FT_Under_3_5_Odd == inTimeModel.FT_35_Under).ToList();
            if (checkerContainer.IsHT_ResultChecked && allMatchBet != null && allMatchBet.Count > 0)
                allMatchBet = allMatchBet.Where(x => x.HTWin1_Odd == inTimeModel.HT_W1 && x.HTWin2_Odd == inTimeModel.HT_W2 && x.HTDraw_Odd == inTimeModel.HT_X).ToList();
            if (checkerContainer.IsFT_ResultChecked && allMatchBet != null && allMatchBet.Count > 0)
                allMatchBet = allMatchBet.Where(x => x.FTWin1_Odd == inTimeModel.FT_W1 && x.FTWin2_Odd == inTimeModel.FT_W2 && x.FTDraw_Odd == inTimeModel.FT_X).ToList();
            if (checkerContainer.IsGoalBetween_Checked && allMatchBet != null && allMatchBet.Count > 0)
                allMatchBet = allMatchBet.Where(x => x.FT_01_Odd == inTimeModel.Goals01 && x.FT_23_Odd == inTimeModel.Goals23 && x.FT_45_Odd == inTimeModel.Goals45 && x.FT_6_Odd == inTimeModel.Goals6).ToList();
            if (checkerContainer.IsGG_NG_Checked && allMatchBet != null && allMatchBet.Count > 0)
                allMatchBet = allMatchBet.Where(x => x.FT_GG_Odd == inTimeModel.GG && x.FT_NG_Odd == inTimeModel.NG).ToList();
            if (checkerContainer.IsCountry_Checked && allMatchBet != null && allMatchBet.Count > 0)
                allMatchBet = allMatchBet.Where(x => x.Country == inTimeModel.Country).ToList();
            if (checkerContainer.IsLeague_Checked && allMatchBet != null && allMatchBet.Count > 0)
                allMatchBet = allMatchBet.Where(x => x.LeagueName == inTimeModel.League).ToList();

            List<FilterResult> result = new List<FilterResult>();

            if (allMatchBet != null && allMatchBet.Count >= checkerContainer.MinimumFoundMatch)
                allMatchBet.Select(k => k.SerialUniqueID).ToList().ForEach(x =>
                {
                    result.Add(filterResultService.Get(p => p.SerialUniqueID == x).Data);
                });
            return result;
        }

        public static List<AnalyseResultVisualiser> GetDataVisualisers(List<JobAnalyseModel> percentageProfilers, int projectViewOid = 0)
        {
            List<AnalyseResultVisualiser> result = new List<AnalyseResultVisualiser>();

            percentageProfilers.ForEach(x =>
            {
                var mappedModel = x.MapToDataVisualiserFromProfiler(projectViewOid);
                result.Add(mappedModel);
            });

            return result;
        }


        public static List<AnalyseResultVisualiser> GetDataVisualisers_TEST(List<JobAnalyseModel> percentageProfilers)
        {
            List<AnalyseResultVisualiser> result = new List<AnalyseResultVisualiser>();

            percentageProfilers.ForEach(x =>
            {
                var mappedModel = x.MapToDataVisualiserFromProfiler_TEST();
                result.Add(mappedModel);
            });

            return result;
        }

        public static List<string> SplitSerials(string serialsText)
        {
            List<string> serials = new List<string>();

            if (!string.IsNullOrEmpty(serialsText))
            {
                serialsText.Split(new string[] { "\r\n", "\t\n", "\n", "|" }, StringSplitOptions.None).ToList().ForEach(x =>
                {
                    if (x.Trim().Length > 4)
                        serials.Add(x.Trim());
                });
            }

            return serials;
        }

        public static List<TeamPercentageProfiler> GetInitializingTeamProfileResult(SystemCheckerContainer model, IMatchBetService matchBetService, IFilterResultService filterResultService, CountryContainerTemp containerTemp)
        {
            var listSerials = model.SerialsBeforeGenerated == null || model.SerialsBeforeGenerated.Count <= 0 ? SplitSerials(model.SerialsText) : model.SerialsBeforeGenerated;

            string mainUrl = _defaultMatchUrl;

            var result = _proceeder.GenerateForInitialisingCalculationModelInTimeInformations(listSerials, containerTemp);
            var removableSerials = new List<string>();
            List<InTimeFilterResultProfilerContainer> containers = new List<InTimeFilterResultProfilerContainer>();
            result.ForEach(x =>
            {
                var resFilter = GetSpecializedFilterResults(x, model, matchBetService, filterResultService);
                if (resFilter != null && resFilter.Count > 0)
                    containers.Add(new InTimeFilterResultProfilerContainer
                    {
                        Serial = x.Serial,
                        Away = x.Away,
                        Home = x.Home,
                        ZEND_FT_Result = x.ZEND_FT_Result,
                        ZEND_HT_Result = x.ZEND_HT_Result,
                        MatchURL = string.Format("{0}{1}", mainUrl, x.Serial),
                        FilterResults = resFilter,
                    });
                else
                    removableSerials.Add(x.Serial);
            });

            for (int i = 0; i < removableSerials.Count; i++)
            {
                result.Remove(result.FirstOrDefault(x => x.Serial == removableSerials[i]));
                listSerials.Remove(removableSerials[i]);
            }

            List<TeamPercentageProfiler> listProfiler = new List<TeamPercentageProfiler>();

            if (containers.Count > 0)
            {
                for (int i = 0; i < containers.Count; i++)
                {
                    var res = new TeamPercentageProfiler
                    {
                        AwayTeam = containers[i].Away,
                        HomeTeam = containers[i].Home,
                        ZEND_FT_Result = containers[i].ZEND_FT_Result,
                        ZEND_HT_Result = containers[i].ZEND_HT_Result,
                        Serial = containers[i].Serial,
                        TargetURL = containers[i].MatchURL,

                        Home_HT_0_5_Over = GenerateTeamPercentage(containers, "Home_HT_0_5_Over", i),
                        Home_HT_1_5_Over = GenerateTeamPercentage(containers, "Home_HT_1_5_Over", i),
                        Home_SH_0_5_Over = GenerateTeamPercentage(containers, "Home_SH_0_5_Over", i),
                        Home_SH_1_5_Over = GenerateTeamPercentage(containers, "Home_SH_1_5_Over", i),
                        Home_FT_0_5_Over = GenerateTeamPercentage(containers, "Home_FT_0_5_Over", i),
                        Home_FT_1_5_Over = GenerateTeamPercentage(containers, "Home_FT_1_5_Over", i),
                        Away_HT_0_5_Over = GenerateTeamPercentage(containers, "Away_HT_0_5_Over", i),
                        Away_HT_1_5_Over = GenerateTeamPercentage(containers, "Away_HT_1_5_Over", i),
                        Away_SH_0_5_Over = GenerateTeamPercentage(containers, "Away_SH_0_5_Over", i),
                        Away_SH_1_5_Over = GenerateTeamPercentage(containers, "Away_SH_1_5_Over", i),
                        Away_FT_0_5_Over = GenerateTeamPercentage(containers, "Away_FT_0_5_Over", i),
                        Away_FT_1_5_Over = GenerateTeamPercentage(containers, "Away_FT_1_5_Over", i),
                        Away_Win_Any_Half = GenerateTeamPercentage(containers, "Away_Win_Any_Half", i),
                        Home_Win_Any_Half = GenerateTeamPercentage(containers, "Home_Win_Any_Half", i),

                        HT_0_5_Over = GenerateTeamPercentage(containers, "HT_0_5_Over", i),
                        HT_1_5_Over = GenerateTeamPercentage(containers, "HT_1_5_Over", i),
                        SH_0_5_Over = GenerateTeamPercentage(containers, "SH_0_5_Over", i),
                        SH_1_5_Over = GenerateTeamPercentage(containers, "SH_1_5_Over", i),
                        FT_1_5_Over = GenerateTeamPercentage(containers, "FT_1_5_Over", i),
                        FT_2_5_Over = GenerateTeamPercentage(containers, "FT_2_5_Over", i),
                        FT_3_5_Over = GenerateTeamPercentage(containers, "FT_3_5_Over", i),

                        FT_GG = GenerateTeamPercentage(containers, "FT_GG", i),
                        HT_GG = GenerateTeamPercentage(containers, "HT_GG", i),
                        SH_GG = GenerateTeamPercentage(containers, "SH_GG", i),

                        FT_TotalBetween = GenerateTeamPercentage(containers, "FT_TotalBetween", i),

                        FT_Result = GenerateTeamPercentage(containers, "FT_Result", i),
                        HT_Result = GenerateTeamPercentage(containers, "HT_Result", i),
                        SH_Result = GenerateTeamPercentage(containers, "SH_Result", i),

                        HT_FT_Result = GenerateTeamPercentage(containers, "HT_FT_Result", i),
                        MoreGoalsBetweenTimes = GenerateTeamPercentage(containers, "MoreGoalsBetweenTimes", i)
                    };
                    listProfiler.Add(res);
                }
            }

            return listProfiler;
        }


        private static TeamPercentageProfiler GetTeamPercentageProfiler(MatchOddResponseInTimeModel inTimeModel, List<List<FilterResult>> nestedListFilterResult, int index, string mainUrl, string serial)
        {
            var result = new TeamPercentageProfiler
            {
                AwayTeam = inTimeModel.Away,
                HomeTeam = inTimeModel.Home,

                Home_HT_0_5_Over = GenerateTeamPercentage(nestedListFilterResult, "Home_HT_0_5_Over", index),
                Home_HT_1_5_Over = GenerateTeamPercentage(nestedListFilterResult, "Home_HT_1_5_Over", index),
                Home_SH_0_5_Over = GenerateTeamPercentage(nestedListFilterResult, "Home_SH_0_5_Over", index),
                Home_SH_1_5_Over = GenerateTeamPercentage(nestedListFilterResult, "Home_SH_1_5_Over", index),
                Home_FT_0_5_Over = GenerateTeamPercentage(nestedListFilterResult, "Home_FT_0_5_Over", index),
                Home_FT_1_5_Over = GenerateTeamPercentage(nestedListFilterResult, "Home_FT_1_5_Over", index),
                Away_HT_0_5_Over = GenerateTeamPercentage(nestedListFilterResult, "Away_HT_0_5_Over", index),
                Away_HT_1_5_Over = GenerateTeamPercentage(nestedListFilterResult, "Away_HT_1_5_Over", index),
                Away_SH_0_5_Over = GenerateTeamPercentage(nestedListFilterResult, "Away_SH_0_5_Over", index),
                Away_SH_1_5_Over = GenerateTeamPercentage(nestedListFilterResult, "Away_SH_1_5_Over", index),
                Away_FT_0_5_Over = GenerateTeamPercentage(nestedListFilterResult, "Away_FT_0_5_Over", index),
                Away_FT_1_5_Over = GenerateTeamPercentage(nestedListFilterResult, "Away_FT_1_5_Over", index),
                Away_Win_Any_Half = GenerateTeamPercentage(nestedListFilterResult, "Away_Win_Any_Half", index),
                Home_Win_Any_Half = GenerateTeamPercentage(nestedListFilterResult, "Home_Win_Any_Half", index),
                HT_0_5_Over = GenerateTeamPercentage(nestedListFilterResult, "HT_0_5_Over", index),
                HT_1_5_Over = GenerateTeamPercentage(nestedListFilterResult, "HT_1_5_Over", index),
                SH_0_5_Over = GenerateTeamPercentage(nestedListFilterResult, "SH_0_5_Over", index),
                SH_1_5_Over = GenerateTeamPercentage(nestedListFilterResult, "SH_1_5_Over", index),
                FT_1_5_Over = GenerateTeamPercentage(nestedListFilterResult, "FT_1_5_Over", index),
                FT_2_5_Over = GenerateTeamPercentage(nestedListFilterResult, "FT_2_5_Over", index),
                FT_3_5_Over = GenerateTeamPercentage(nestedListFilterResult, "FT_3_5_Over", index),
                FT_GG = GenerateTeamPercentage(nestedListFilterResult, "FT_GG", index),
                HT_GG = GenerateTeamPercentage(nestedListFilterResult, "HT_GG", index),
                SH_GG = GenerateTeamPercentage(nestedListFilterResult, "SH_GG", index),
                FT_TotalBetween = GenerateTeamPercentage(nestedListFilterResult, "FT_TotalBetween", index),
                FT_Result = GenerateTeamPercentage(nestedListFilterResult, "FT_Result", index),
                HT_Result = GenerateTeamPercentage(nestedListFilterResult, "HT_Result", index),
                SH_Result = GenerateTeamPercentage(nestedListFilterResult, "SH_Result", index),
                HT_FT_Result = GenerateTeamPercentage(nestedListFilterResult, "HT_FT_Result", index),
                MoreGoalsBetweenTimes = GenerateTeamPercentage(nestedListFilterResult, "MoreGoalsBetweenTimes", index),
                TargetURL = string.Format("{0}{1}", mainUrl, serial),
                Serial = serial
            };
            return result;
        }

        private static OddResultPercentageProfayler GetOddPercentageInTime(string serial, List<FilterResultMutateModel> resultContainer, decimal range)
        {
            var result = new OddResultPercentageProfayler
            {
                CountFound = resultContainer.Count,
                Average_FT_Goals_AwayTeam = GenerateOddTeamAverage(resultContainer, "Away_FT_GoalsCount"),
                Average_FT_Goals_HomeTeam = GenerateOddTeamAverage(resultContainer, "Home_FT_GoalsCount"),
                Average_HT_Goals_AwayTeam = GenerateOddTeamAverage(resultContainer, "Away_HT_GoalsCount"),
                Average_HT_Goals_HomeTeam = GenerateOddTeamAverage(resultContainer, "Home_HT_GoalsCount"),
                Average_SH_Goals_AwayTeam = GenerateOddTeamAverage(resultContainer, "Away_SH_GoalsCount"),
                Average_SH_Goals_HomeTeam = GenerateOddTeamAverage(resultContainer, "Home_SH_GoalsCount"),
                Average_FT_Conceded_Goals_AwayTeam = GenerateOddTeamAverage(resultContainer, "Home_FT_GoalsCount"),
                Average_FT_Conceded_Goals_HomeTeam = GenerateOddTeamAverage(resultContainer, "Away_FT_GoalsCount"),
                Average_HT_Conceded_Goals_AwayTeam = GenerateOddTeamAverage(resultContainer, "Home_HT_GoalsCount"),
                Average_HT_Conceded_Goals_HomeTeam = GenerateOddTeamAverage(resultContainer, "Away_HT_GoalsCount"),
                Average_SH_Conceded_Goals_AwayTeam = GenerateOddTeamAverage(resultContainer, "Home_SH_GoalsCount"),
                Average_SH_Conceded_Goals_HomeTeam = GenerateOddTeamAverage(resultContainer, "Away_SH_GoalsCount"),
                Average_FT_Corners_AwayTeam = GenerateOddCornersPossesionShutAverage(resultContainer, "AwayCornerCount", x => x.IsCornerFound == true),
                Average_FT_Corners_HomeTeam = GenerateOddCornersPossesionShutAverage(resultContainer, "HomeCornerCount", x => x.IsCornerFound == true),

                Average_FT_Possesion_HomeTeam = GenerateOddCornersPossesionShutAverage(resultContainer, "HomePossesion", x => x.IsPossesionFound == true),
                Average_FT_Possesion_AwayTeam = GenerateOddCornersPossesionShutAverage(resultContainer, "AwayPossesion", x => x.IsPossesionFound == true),
                Average_FT_Shot_AwayTeam = GenerateOddCornersPossesionShutAverage(resultContainer, "AwayShotCount", x => x.IsShotFound == true),
                Average_FT_Shot_HomeTeam = GenerateOddCornersPossesionShutAverage(resultContainer, "HomeShotCount", x => x.IsShotFound == true),
                Average_FT_ShotOnTarget_AwayTeam = GenerateOddCornersPossesionShutAverage(resultContainer, "AwayShotOnTargetCount", x => x.IsShotOnTargetFound == true),
                Average_FT_ShotOnTarget_HomeTeam = GenerateOddCornersPossesionShutAverage(resultContainer, "HomeShotOnTargetCount", x => x.IsShotOnTargetFound == true),
                Average_FT_GK_Saves_HomeTeam = GenerateOddCornersPossesionShutAverage(resultContainer, "Home_GK_SavesCount", x => x.IsShotOnTargetFound),
                Average_FT_GK_Saves_AwayTeam = GenerateOddCornersPossesionShutAverage(resultContainer, "Away_GK_SavesCount", x => x.IsShotOnTargetFound),

                Corner_Away_3_5_Over = GenerateOddTeamPercentage(resultContainer, "Corner_Away_3_5_Over", x => x.IsCornerFound == true),
                Corner_Away_4_5_Over = GenerateOddTeamPercentage(resultContainer, "Corner_Away_4_5_Over", x => x.IsCornerFound == true),
                Corner_Away_5_5_Over = GenerateOddTeamPercentage(resultContainer, "Corner_Away_5_5_Over", x => x.IsCornerFound == true),
                Corner_Home_3_5_Over = GenerateOddTeamPercentage(resultContainer, "Corner_Home_3_5_Over", x => x.IsCornerFound == true),
                Corner_Home_4_5_Over = GenerateOddTeamPercentage(resultContainer, "Corner_Home_4_5_Over", x => x.IsCornerFound == true),
                Corner_Home_5_5_Over = GenerateOddTeamPercentage(resultContainer, "Corner_Home_5_5_Over", x => x.IsCornerFound == true),

                Corner_7_5_Over = GenerateOddTeamPercentage(resultContainer, "Corner_7_5_Over", x => x.IsCornerFound == true),
                Corner_8_5_Over = GenerateOddTeamPercentage(resultContainer, "Corner_8_5_Over", x => x.IsCornerFound == true),
                Corner_9_5_Over = GenerateOddTeamPercentage(resultContainer, "Corner_9_5_Over", x => x.IsCornerFound == true),

                Away_FT_05_Over = GenerateOddTeamPercentage(resultContainer, "Away_FT_0_5_Over"),
                Away_FT_15_Over = GenerateOddTeamPercentage(resultContainer, "Away_FT_1_5_Over"),
                Away_HT_05_Over = GenerateOddTeamPercentage(resultContainer, "Away_HT_0_5_Over"),
                Away_HT_15_Over = GenerateOddTeamPercentage(resultContainer, "Away_HT_1_5_Over"),
                Away_SH_05_Over = GenerateOddTeamPercentage(resultContainer, "Away_SH_0_5_Over"),
                Away_SH_15_Over = GenerateOddTeamPercentage(resultContainer, "Away_SH_1_5_Over"),
                Away_Win_Any_Half = GenerateOddTeamPercentage(resultContainer, "Away_Win_Any_Half"),
                Home_FT_05_Over = GenerateOddTeamPercentage(resultContainer, "Home_FT_0_5_Over"),
                Home_FT_15_Over = GenerateOddTeamPercentage(resultContainer, "Home_FT_1_5_Over"),
                Home_HT_05_Over = GenerateOddTeamPercentage(resultContainer, "Home_HT_0_5_Over"),
                Home_HT_15_Over = GenerateOddTeamPercentage(resultContainer, "Home_HT_1_5_Over"),
                Home_SH_05_Over = GenerateOddTeamPercentage(resultContainer, "Home_SH_0_5_Over"),
                Home_SH_15_Over = GenerateOddTeamPercentage(resultContainer, "Home_SH_1_5_Over"),
                Home_Win_Any_Half = GenerateOddTeamPercentage(resultContainer, "Home_Win_Any_Half"),
                FT_15_Over = GenerateOddTeamPercentage(resultContainer, "FT_1_5_Over"),
                FT_25_Over = GenerateOddTeamPercentage(resultContainer, "FT_2_5_Over"),
                FT_35_Over = GenerateOddTeamPercentage(resultContainer, "FT_3_5_Over"),
                FT_GG = GenerateOddTeamPercentage(resultContainer, "FT_GG"),
                HT_GG = GenerateOddTeamPercentage(resultContainer, "HT_GG"),
                SH_GG = GenerateOddTeamPercentage(resultContainer, "SH_GG"),
                HT_05_Over = GenerateOddTeamPercentage(resultContainer, "HT_0_5_Over"),
                HT_15_Over = GenerateOddTeamPercentage(resultContainer, "HT_1_5_Over"),
                SH_05_Over = GenerateOddTeamPercentage(resultContainer, "SH_0_5_Over"),
                SH_15_Over = GenerateOddTeamPercentage(resultContainer, "SH_1_5_Over"),
                Is_FT_Win1 = GenerateOddTeamPercentage(resultContainer, "Is_FT_Win1"),
                Is_FT_X = GenerateOddTeamPercentage(resultContainer, "Is_FT_X"),
                Is_FT_Win2 = GenerateOddTeamPercentage(resultContainer, "Is_FT_Win2"),
                Is_HT_Win1 = GenerateOddTeamPercentage(resultContainer, "Is_HT_Win1"),
                Is_HT_X = GenerateOddTeamPercentage(resultContainer, "Is_HT_X"),
                Is_HT_Win2 = GenerateOddTeamPercentage(resultContainer, "Is_HT_Win2"),
                Is_SH_Win1 = GenerateOddTeamPercentage(resultContainer, "Is_SH_Win1"),
                Is_SH_X = GenerateOddTeamPercentage(resultContainer, "Is_SH_X"),
                Is_SH_Win2 = GenerateOddTeamPercentage(resultContainer, "Is_SH_Win2"),
                Is_Corner_FT_Win1 = GenerateOddTeamPercentage(resultContainer, "Is_Corner_FT_Win1", x => x.IsCornerFound == true),
                Is_Corner_FT_X = GenerateOddTeamPercentage(resultContainer, "Is_Corner_FT_X", x => x.IsCornerFound == true),
                Is_Corner_FT_Win2 = GenerateOddTeamPercentage(resultContainer, "Is_Corner_FT_Win2", x => x.IsCornerFound == true)
            };
            return result;
        }

        public static List<JobAnalyseModel> GetJobAnalyseModelResult(SystemCheckerContainer model, IMatchBetService matchBetService, IFilterResultService filterResultService, CountryContainerTemp containerTemp)
        {
            var listSerials = model.SerialsBeforeGenerated == null || model.SerialsBeforeGenerated.Count <= 0 ? SplitSerials(model.SerialsText) : model.SerialsBeforeGenerated;

            string mainUrl = _defaultMatchUrl;

            var result = _proceeder.GenerateCalculationModelInTimeInformations(listSerials, model.CountDownMinutes, model.IsAnalyseAnyTime, containerTemp);
            var removableSerials = new List<string>();
            List<List<FilterResult>> filterResults = new List<List<FilterResult>>();
            result.ForEach(x =>
            {
                // TODO : START
                var resFilter = GetSpecializedFilterResults(x, model, matchBetService, filterResultService);
                if (resFilter != null && resFilter.Count > 0)
                    filterResults.Add(resFilter);
                else
                    removableSerials.Add(x.Serial);
            });

            for (int i = 0; i < removableSerials.Count; i++)
            {
                result.Remove(result.FirstOrDefault(x => x.Serial == removableSerials[i]));
                listSerials.Remove(removableSerials[i]);
            }
            List<JobAnalyseModel> listAnalyseModel = new List<JobAnalyseModel>();

            for (int i = 0; i < filterResults.Count; i++)
            {
                var analyseModelOne = new JobAnalyseModel
                {
                    TeamPercentageProfiler = GetTeamPercentageProfiler(result[i], filterResults, i, mainUrl, listSerials[i]),
                    ComparisonInfoContainer = GetComparisonProfilerResult(listSerials[i], TeamSide.Home),
                    HomeTeam_FormPerformanceGuessContainer = GetFormPerformanceProfiler(listSerials[i], matchBetService, containerTemp, TeamSide.Home),
                    AwayTeam_FormPerformanceGuessContainer = GetFormPerformanceProfiler(listSerials[i], matchBetService, containerTemp, TeamSide.Away),
                    StandingInfoModel = GetStandingInfoModel(listSerials[i])
                };

                listAnalyseModel.Add(analyseModelOne);
            }

            return listAnalyseModel;
        }


        public static List<JobAnalyseModel> GetJobAnalyseModelResultTest2222(IMatchBetService matchBetService, CountryContainerTemp containerTemp, LeagueContainer leagueContainer, List<string> serials)
        {
            string mainUrl = _defaultMatchUrl;

            List<JobAnalyseModel> listAnalyseModel = new List<JobAnalyseModel>();

            for (int i = 0; i < serials.Count; i++)
            {
                var rgxCountryLeagueMix = new Regex(PatternConstant.UnstartedMatchPattern.CountryAndLeague);
                var rgxLeague = new Regex(PatternConstant.UnstartedMatchPattern.League);
                var rgxCountry = new Regex(PatternConstant.UnstartedMatchPattern.Country);

                string src = _webOperator.GetMinifiedString($"{mainUrl}{serials[i]}");

                string leagueName = src.ResolveLeagueByRegex(containerTemp, rgxCountryLeagueMix, rgxLeague);
                string countryName = src.ResolveCountryByRegex(containerTemp, rgxCountryLeagueMix, rgxCountry);

                bool validToGo = leagueContainer.LeagueHolders.Any(x => x.Country.Trim().ToLower() == countryName.ToLower().Trim() && x.League.ToLower().Trim() == leagueName.ToLower().Trim());

                if (!validToGo) continue;

                var analyseModelOne = new JobAnalyseModel
                {
                    ComparisonInfoContainer = GetComparisonProfilerResult(serials[i], TeamSide.Home),
                    HomeTeam_FormPerformanceGuessContainer = GetFormPerformanceProfiler(serials[i], matchBetService, containerTemp, TeamSide.Home),
                    AwayTeam_FormPerformanceGuessContainer = GetFormPerformanceProfiler(serials[i], matchBetService, containerTemp, TeamSide.Away),
                    StandingInfoModel = GetStandingInfoModel(serials[i])
                };

                if (analyseModelOne.ComparisonInfoContainer == null || analyseModelOne.HomeTeam_FormPerformanceGuessContainer == null || analyseModelOne.AwayTeam_FormPerformanceGuessContainer == null)
                {
                    continue;
                }

                listAnalyseModel.Add(analyseModelOne);
            }

            return listAnalyseModel;
        }

        public static List<JobAnalyseModel> GetJobAnalyseModelResultTest7777(IMatchBetService matchBetService, IFilterResultService filterResultService, IComparisonStatisticsHolderService comparisonStatisticsHolderService, IAverageStatisticsHolderService averageStatisticsHolderService, ITeamPerformanceStatisticsHolderService performanceStatisticsHolderService, ILeagueStatisticsHolderService leagueStatisticsHolderService, IMatchIdentifierService matchIdentifierService, IAiDataHolderService aiDataHolderService, IStatisticInfoHolderService statisticInfoHolderService, CountryContainerTemp containerTemp, LeagueContainer leagueContainer, List<string> serials)
        {
            string mainUrl = _defaultMatchUrl;
            List<JobAnalyseModel> listAnalyseModel = new List<JobAnalyseModel>();

            foreach (string serial in serials)
            {
                string src = _webOperator.GetMinifiedString($"{mainUrl}{serial}");

                string leagueName = ExtractLeagueName(src, containerTemp);
                string countryName = ExtractCountryName(src, containerTemp);
                string timeMatch = ExtractTimeMatch(src);

                var currentLeagueContainer = FindCurrentLeagueContainer(leagueContainer, countryName, leagueName);

                if (currentLeagueContainer == null) continue;

                var analyseModelOne = CreateAnalyseModel(serial, matchBetService, filterResultService, containerTemp);

                if (analyseModelOne == null) continue;

                var leagueStatistic = currentLeagueContainer.GetLeagueStatistic();

                DateTime matchDateTime = CalculateMatchDateTime(timeMatch);

                var matchIdentity = CreateMatchIdentifier(analyseModelOne, serial, matchDateTime);

                int convertedSerial = Convert.ToInt32(serial);

                if (matchIdentifierService.Get(x => x.Serial == convertedSerial).Data == null)
                {
                    AiAnalyseModel? aiAnalyseModel = GenerateAiAnalyseModel(analyseModelOne, leagueStatistic, matchBetService);

                    if (aiAnalyseModel != null)
                    {
                        var aiDataHolder = new AiDataHolder
                        {
                            Serial = Convert.ToInt32(serial),
                            DataType = AiDataType.GuessAnalysing,
                            JsonTextContent = JsonConvert.SerializeObject(aiAnalyseModel)
                        };

                        aiDataHolderService.Add(aiDataHolder);
                    }

                    matchIdentifierService.Add(matchIdentity);

                    AddStatisticsToLeagueStatistic(analyseModelOne, leagueStatistic, matchIdentity.Id);

                    var statisticInfoesAverageBySide = GenerateAverageStatInfoes(leagueStatistic.AverageStatisticsHolders.FirstOrDefault(x => x.BySideType == (int)BySideType.HomeAway), Convert.ToInt32(serial), (int)BySideType.HomeAway);
                    var statisticInfoesAverageGeneral = GenerateAverageStatInfoes(leagueStatistic.AverageStatisticsHolders.FirstOrDefault(x => x.BySideType == (int)BySideType.General), Convert.ToInt32(serial), (int)BySideType.General);
                    var statisticInfoesComparisonBySide = GenerateComparisonStatInfoes(leagueStatistic.ComparisonStatisticsHolders.FirstOrDefault(x => x.BySideType == (int)BySideType.HomeAway), Convert.ToInt32(serial), (int)BySideType.HomeAway);
                    var statisticInfoesComparisonGeneral = GenerateComparisonStatInfoes(leagueStatistic.ComparisonStatisticsHolders.FirstOrDefault(x => x.BySideType == (int)BySideType.General), Convert.ToInt32(serial), (int)BySideType.General);

                    var statisticInfoesPerformanceBySide =
                        GeneratePerformanceStatInfoes(leagueStatistic.TeamPerformanceStatisticsHolders
                        .FirstOrDefault(x => x.BySideType == (int)BySideType.HomeAway && x.HomeOrAway == (int)HomeOrAway.Home), leagueStatistic.TeamPerformanceStatisticsHolders
                        .FirstOrDefault(x => x.BySideType == (int)BySideType.HomeAway && x.HomeOrAway == (int)HomeOrAway.Away), Convert.ToInt32(serial), (int)BySideType.HomeAway);

                    var statisticInfoesPerformanceGeneral =
                        GeneratePerformanceStatInfoes(leagueStatistic.TeamPerformanceStatisticsHolders
                        .FirstOrDefault(x => x.BySideType == (int)BySideType.General && x.HomeOrAway == (int)HomeOrAway.Home), leagueStatistic.TeamPerformanceStatisticsHolders
                        .FirstOrDefault(x => x.BySideType == (int)BySideType.General && x.HomeOrAway == (int)HomeOrAway.Away), Convert.ToInt32(serial), (int)BySideType.General);

                    leagueStatisticsHolderService.Add(leagueStatistic);

                    statisticInfoHolderService.AddRange(statisticInfoesAverageBySide);
                    statisticInfoHolderService.AddRange(statisticInfoesAverageGeneral);

                    statisticInfoHolderService.AddRange(statisticInfoesComparisonBySide);
                    statisticInfoHolderService.AddRange(statisticInfoesComparisonGeneral);

                    statisticInfoHolderService.AddRange(statisticInfoesPerformanceBySide);
                    statisticInfoHolderService.AddRange(statisticInfoesPerformanceGeneral);
                }

                listAnalyseModel.Add(analyseModelOne);
            }

            return listAnalyseModel;
        }

        private static AiAnalyseModel? GenerateAiAnalyseModel(JobAnalyseModel? jobAnalyseModel, LeagueStatisticsHolder leagueStatisticsHolder, IMatchBetService matchBetService)
        {
            if (jobAnalyseModel == null) return null;

            string uri = $"{_defaultMatchUrl}{jobAnalyseModel.Serial}";

            var matchDateTimeRgx = new Regex(PatternConstant.UnstartedMatchPattern.DateMatch);

            var src = _webOperator.GetMinifiedString(uri);

            string matchDateTimeTxt = src.ResolveTextByRegex(matchDateTimeRgx);

            DateTime matchDateTime = DateTime.Now.Date;

            if (DateTime.TryParse(matchDateTimeTxt, out DateTime _dt))
                matchDateTime = DateTime.Parse(matchDateTimeTxt);

            var performanceResult = GeneratePerformanceAiModel(matchBetService, leagueStatisticsHolder.CountryName, jobAnalyseModel.ComparisonInfoContainer.Home, jobAnalyseModel.ComparisonInfoContainer.Away);

            return new AiAnalyseModel
            {
                MatchInformation = new MatchDataAiModel(leagueStatisticsHolder.CountryName, leagueStatisticsHolder.LeagueName, jobAnalyseModel.ComparisonInfoContainer.Home, jobAnalyseModel.ComparisonInfoContainer.Away, matchDateTime),
                StandingInfoes = jobAnalyseModel.StandingInfoModel.MapAiStandingModel(jobAnalyseModel.ComparisonInfoContainer.Home),
                LeagueStatistics = leagueStatisticsHolder.MapLeagueStatisticsAiModel(),
                ComparisonDataes = _proceeder.SelectListComparisonAiModel(jobAnalyseModel.Serial, 10),
                HomeTeamPerformanceMatches = performanceResult.HomePerformance,
                AwayTeamPerformanceMatches = performanceResult.AwayPerformance,
                StatisticPercentageModel = GetStatisticalPercentAiModel(jobAnalyseModel)
            };
        }

        private static StatisticalPercentAiModel GetStatisticalPercentAiModel(JobAnalyseModel jobAnalyseModel)
        {
            var teamPerformanceHomeBySide = jobAnalyseModel.GetHomePerformanceStatistics((int)BySideType.HomeAway);
            var teamPerformanceAwayBySide = jobAnalyseModel.GetAwayPerformanceStatistics((int)BySideType.HomeAway);
            var teamPerformanceHomeGeneral = jobAnalyseModel.GetHomePerformanceStatistics((int)BySideType.General);
            var teamPerformanceAwayGeneral = jobAnalyseModel.GetAwayPerformanceStatistics((int)BySideType.General);
            var comparisonBySide = jobAnalyseModel.GetComparisonStatistics((int)BySideType.HomeAway);
            var comparisonGeneral = jobAnalyseModel.GetComparisonStatistics((int)BySideType.General);

            return new StatisticalPercentAiModel
            {
                HomeTeam = jobAnalyseModel.ComparisonInfoContainer.Home,
                AwayTeam = jobAnalyseModel.ComparisonInfoContainer.Away,
                HomeAtHome_AwayAtAway_H2H = comparisonBySide.MapToComparisonAiModel(),
                General_H2H = comparisonGeneral.MapToComparisonAiModel(),
                HomeAtHome_Form_HomeTeam = teamPerformanceHomeBySide.MapToComparisonAiModel(),
                AwayAtAway_Form_AwayTeam = teamPerformanceAwayBySide.MapToComparisonAiModel(),
                General_Form_HomeTeam = teamPerformanceHomeGeneral.MapToComparisonAiModel(),
                General_Form_AwayTeam = teamPerformanceAwayGeneral.MapToComparisonAiModel()
            };
        }

        private static PerformanceAiModel GeneratePerformanceAiModel(IMatchBetService matchBetService, string countryName, string homeTeam, string awayTeam)
        {
            var matchQueryResultHome = matchBetService.GetMatchBetQueryModels(countryName, homeTeam, 10).Data;
            var matchQueryResultAway = matchBetService.GetMatchBetQueryModels(countryName, awayTeam, 10).Data;

            var result = new PerformanceAiModel
            {
                HomePerformance = matchQueryResultHome.Select(x => new TeamAiPerformanceHolder
                {
                    HomeTeam = x.HomeTeam,
                    AwayTeam = x.AwayTeam,
                    MatchDate = x.MatchDate.Date,
                    HT_Goals_AwayTeam = Convert.ToInt32(x.HT_Match_Result.Split('-')[1].Trim()),
                    HT_Goals_HomeTeam = Convert.ToInt32(x.HT_Match_Result.Split('-')[0].Trim()),
                    FT_Goals_AwayTeam = Convert.ToInt32(x.FT_Match_Result.Split('-')[1].Trim()),
                    FT_Goals_HomeTeam = Convert.ToInt32(x.FT_Match_Result.Split('-')[0].Trim()),
                    SH_Goals_AwayTeam = (Convert.ToInt32(x.FT_Match_Result.Split('-')[1].Trim())) - (Convert.ToInt32(x.HT_Match_Result.Split('-')[1].Trim())),
                    SH_Goals_HomeTeam = (Convert.ToInt32(x.FT_Match_Result.Split('-')[0].Trim())) - (Convert.ToInt32(x.HT_Match_Result.Split('-')[0].Trim())),
                    FullTime_Result = GenerateResult(Convert.ToInt32(x.FT_Match_Result.Split('-')[0].Trim()), Convert.ToInt32(x.FT_Match_Result.Split('-')[1].Trim())),
                    HalfTime_Result = GenerateResult(Convert.ToInt32(x.HT_Match_Result.Split('-')[0].Trim()), Convert.ToInt32(x.HT_Match_Result.Split('-')[1].Trim())),
                    SecondHalf_Result = GenerateResult((Convert.ToInt32(x.FT_Match_Result.Split('-')[0].Trim())) - (Convert.ToInt32(x.HT_Match_Result.Split('-')[0].Trim())), (Convert.ToInt32(x.FT_Match_Result.Split('-')[1].Trim())) - (Convert.ToInt32(x.HT_Match_Result.Split('-')[1].Trim()))),
                    MoreDetails = !x.HasCorner ? null : new PerformanceMoreDetails
                    {
                        Home_Ball_Possesion = x.HomePossesion,
                        Away_Ball_Possesion = x.AwayPossesion,
                        HomeShotsCount = x.HomeShotCount,
                        AwayShotsCount = x.AwayShotCount,
                        HomeShotsOnTargetCount = x.HomeShotOnTargetCount,
                        AwayShotsOnTargetCount = x.AwayShotOnTargetCount,
                        HomeGK_SavesCount = x.AwayShotOnTargetCount - Convert.ToInt32(x.FT_Match_Result.Split('-')[1].Trim()),
                        AwayGK_SavesCount = x.HomeShotOnTargetCount - Convert.ToInt32(x.FT_Match_Result.Split('-')[0].Trim())
                    }
                }).OrderByDescending(x => x.MatchDate).ToList(),
                AwayPerformance = matchQueryResultAway.Select(x => new TeamAiPerformanceHolder
                {
                    HomeTeam = x.HomeTeam,
                    AwayTeam = x.AwayTeam,
                    MatchDate = x.MatchDate.Date,
                    HT_Goals_AwayTeam = Convert.ToInt32(x.HT_Match_Result.Split('-')[1].Trim()),
                    HT_Goals_HomeTeam = Convert.ToInt32(x.HT_Match_Result.Split('-')[0].Trim()),
                    FT_Goals_AwayTeam = Convert.ToInt32(x.FT_Match_Result.Split('-')[1].Trim()),
                    FT_Goals_HomeTeam = Convert.ToInt32(x.FT_Match_Result.Split('-')[0].Trim()),
                    SH_Goals_AwayTeam = (Convert.ToInt32(x.FT_Match_Result.Split('-')[1].Trim())) - (Convert.ToInt32(x.HT_Match_Result.Split('-')[1].Trim())),
                    SH_Goals_HomeTeam = (Convert.ToInt32(x.FT_Match_Result.Split('-')[0].Trim())) - (Convert.ToInt32(x.HT_Match_Result.Split('-')[0].Trim())),
                    FullTime_Result = GenerateResult(Convert.ToInt32(x.FT_Match_Result.Split('-')[0].Trim()), Convert.ToInt32(x.FT_Match_Result.Split('-')[1].Trim())),
                    HalfTime_Result = GenerateResult(Convert.ToInt32(x.HT_Match_Result.Split('-')[0].Trim()), Convert.ToInt32(x.HT_Match_Result.Split('-')[1].Trim())),
                    SecondHalf_Result = GenerateResult((Convert.ToInt32(x.FT_Match_Result.Split('-')[0].Trim())) - (Convert.ToInt32(x.HT_Match_Result.Split('-')[0].Trim())), (Convert.ToInt32(x.FT_Match_Result.Split('-')[1].Trim())) - (Convert.ToInt32(x.HT_Match_Result.Split('-')[1].Trim()))),
                    MoreDetails = !x.HasCorner ? null : new PerformanceMoreDetails
                    {
                        Home_Ball_Possesion = x.HomePossesion,
                        Away_Ball_Possesion = x.AwayPossesion,
                        HomeShotsCount = x.HomeShotCount,
                        AwayShotsCount = x.AwayShotCount,
                        HomeShotsOnTargetCount = x.HomeShotOnTargetCount,
                        AwayShotsOnTargetCount = x.AwayShotOnTargetCount,
                        HomeGK_SavesCount = x.AwayShotOnTargetCount - Convert.ToInt32(x.FT_Match_Result.Split('-')[1].Trim()),
                        AwayGK_SavesCount = x.HomeShotOnTargetCount - Convert.ToInt32(x.FT_Match_Result.Split('-')[0].Trim())
                    }
                }).OrderByDescending(x => x.MatchDate).ToList()
            };

            return result;
        }

        public static string GenerateResult(int homeGoals, int awayGoals)
        {
            if (homeGoals > awayGoals)
            {
                return "Home Won";
            }
            else if (homeGoals < awayGoals)
            {
                return "Away Won";
            }
            else
            {
                return "Draw";
            }
        }

        private static List<StatisticInfoHolder> GenerateAverageStatInfoes(AverageStatisticsHolder? input, int serial, int bySide)
        {
            if (input == null) return null;

            var result = new List<StatisticInfoHolder>
            {
                new StatisticInfoHolder(input.UniqueIdentity, 100, "Ind_Avg_Goal_FT", input.Average_FT_Goals_HomeTeam.ToString("0.00"), input.Average_FT_Goals_AwayTeam.ToString("0.00"), input.Average_FT_Goals_HomeTeam, input.Average_FT_Goals_AwayTeam, serial, (int)StatisticType.Average, bySide),
                new StatisticInfoHolder(input.UniqueIdentity, 105, "Ind_Avg_Goal_HT", input.Average_HT_Goals_HomeTeam.ToString("0.00"), input.Average_HT_Goals_AwayTeam.ToString("0.00"), input.Average_HT_Goals_HomeTeam, input.Average_HT_Goals_AwayTeam, serial, (int)StatisticType.Average, bySide),
                new StatisticInfoHolder(input.UniqueIdentity, 110, "Ind_Avg_Goal_SH", input.Average_SH_Goals_HomeTeam.ToString("0.00"), input.Average_SH_Goals_AwayTeam.ToString("0.00"), input.Average_SH_Goals_HomeTeam, input.Average_SH_Goals_AwayTeam, serial, (int)StatisticType.Average, bySide),
                new StatisticInfoHolder(input.UniqueIdentity, 115, "Ind_FT_Win", string.Format("{0}%", input.Is_FT_Win1), string.Format("{0}%", input.Is_FT_Win2), (decimal)input.Is_FT_Win1 / 100, (decimal)input.Is_FT_Win2 / 100, serial, (int)StatisticType.Average, bySide),
                new StatisticInfoHolder(input.UniqueIdentity, 120, "FT_X", string.Format("{0}%", input.Is_FT_X), string.Format("{0}%", input.Is_FT_X), (decimal)input.Is_FT_X / 100, (decimal)input.Is_FT_X / 100, serial, (int)StatisticType.Average, bySide),
                new StatisticInfoHolder(input.UniqueIdentity, 125, "Ind_HT_Win", string.Format("{0}%", input.Is_HT_Win1), string.Format("{0}%", input.Is_HT_Win2), (decimal)input.Is_HT_Win1 / 100, (decimal)input.Is_HT_Win2 / 100, serial, (int)StatisticType.Average, bySide),
                new StatisticInfoHolder(input.UniqueIdentity, 130, "HT_X", string.Format("{0}%", input.Is_HT_X), string.Format("{0}%", input.Is_HT_X), (decimal)input.Is_HT_X / 100, (decimal)input.Is_HT_X / 100, serial, (int)StatisticType.Average, bySide),
                new StatisticInfoHolder(input.UniqueIdentity, 135, "Ind_SH_Win", string.Format("{0}%", input.Is_SH_Win1), string.Format("{0}%", input.Is_SH_Win2), (decimal)input.Is_SH_Win1 / 100, (decimal)input.Is_SH_Win2 / 100, serial, (int)StatisticType.Average, bySide),
                new StatisticInfoHolder(input.UniqueIdentity, 140, "SH_X", string.Format("{0}%", input.Is_SH_X), string.Format("{0}%", input.Is_SH_X), (decimal)input.Is_SH_X / 100, (decimal)input.Is_SH_X / 100, serial, (int)StatisticType.Average, bySide),
                new StatisticInfoHolder(input.UniqueIdentity, 145, "Ind_FT_05", string.Format("{0}%", input.Home_FT_05_Over), string.Format("{0}%", input.Away_FT_05_Over), (decimal)input.Home_FT_05_Over / 100, (decimal)input.Away_FT_05_Over / 100, serial, (int)StatisticType.Average, bySide),
                new StatisticInfoHolder(input.UniqueIdentity, 150, "Ind_FT_15", string.Format("{0}%", input.Home_FT_15_Over), string.Format("{0}%", input.Away_FT_15_Over), (decimal)input.Home_FT_15_Over / 100, (decimal)input.Away_FT_15_Over / 100, serial, (int)StatisticType.Average, bySide),
                new StatisticInfoHolder(input.UniqueIdentity, 155, "Ind_HT_05", string.Format("{0}%", input.Home_HT_05_Over), string.Format("{0}%", input.Away_HT_05_Over), (decimal)input.Home_HT_05_Over / 100, (decimal)input.Away_HT_05_Over / 100, serial, (int)StatisticType.Average, bySide),
                new StatisticInfoHolder(input.UniqueIdentity, 160, "Ind_HT_15", string.Format("{0}%", input.Home_HT_15_Over), string.Format("{0}%", input.Away_HT_15_Over), (decimal)input.Home_HT_15_Over / 100, (decimal)input.Away_HT_15_Over / 100, serial, (int)StatisticType.Average, bySide),
                new StatisticInfoHolder(input.UniqueIdentity, 165, "Ind_SH_05", string.Format("{0}%", input.Home_SH_05_Over), string.Format("{0}%", input.Away_SH_05_Over), (decimal)input.Home_SH_05_Over / 100, (decimal)input.Away_SH_05_Over / 100, serial, (int)StatisticType.Average, bySide),
                new StatisticInfoHolder(input.UniqueIdentity, 170, "Ind_SH_15", string.Format("{0}%", input.Home_SH_15_Over), string.Format("{0}%", input.Away_SH_15_Over), (decimal)input.Home_SH_15_Over / 100, (decimal)input.Away_SH_15_Over / 100, serial, (int)StatisticType.Average, bySide),
                new StatisticInfoHolder(input.UniqueIdentity, 142, "Ind_WinAny", string.Format("{0}%", input.Home_Win_Any_Half), string.Format("{0}%", input.Away_Win_Any_Half), (decimal)input.Home_Win_Any_Half / 100, (decimal)input.Away_Win_Any_Half / 100, serial, (int)StatisticType.Average, bySide),

                new StatisticInfoHolder(input.UniqueIdentity, 175, "FT_GG", string.Format("{0}%", input.FT_GG), string.Format("{0}%", input.FT_GG), (decimal)input.FT_GG / 100, (decimal)input.FT_GG / 100, serial, (int)StatisticType.Average, bySide),
                new StatisticInfoHolder(input.UniqueIdentity, 180, "HT_GG", string.Format("{0}%", input.HT_GG), string.Format("{0}%", input.HT_GG), (decimal)input.HT_GG / 100, (decimal)input.HT_GG / 100, serial, (int)StatisticType.Average, bySide),
                new StatisticInfoHolder(input.UniqueIdentity, 185, "SH_GG", string.Format("{0}%", input.SH_GG), string.Format("{0}%", input.SH_GG), (decimal)input.SH_GG / 100, (decimal)input.SH_GG / 100, serial, (int)StatisticType.Average, bySide),
                new StatisticInfoHolder(input.UniqueIdentity, 190, "FT_15", string.Format("{0}%", input.FT_15_Over), string.Format("{0}%", input.FT_15_Over), (decimal)input.FT_15_Over / 100, (decimal)input.FT_15_Over / 100, serial, (int)StatisticType.Average, bySide),
                new StatisticInfoHolder(input.UniqueIdentity, 195, "FT_25", string.Format("{0}%", input.FT_25_Over), string.Format("{0}%", input.FT_25_Over), (decimal)input.FT_25_Over / 100, (decimal)input.FT_25_Over / 100, serial, (int)StatisticType.Average, bySide),
                new StatisticInfoHolder(input.UniqueIdentity, 200, "FT_35", string.Format("{0}%", input.FT_35_Over), string.Format("{0}%", input.FT_35_Over), (decimal)input.FT_35_Over / 100, (decimal)input.FT_35_Over / 100, serial, (int)StatisticType.Average, bySide),
                new StatisticInfoHolder(input.UniqueIdentity, 205, "HT_05", string.Format("{0}%", input.HT_05_Over), string.Format("{0}%", input.HT_05_Over), (decimal)input.HT_05_Over / 100, (decimal)input.HT_05_Over / 100, serial, (int)StatisticType.Average, bySide),
                new StatisticInfoHolder(input.UniqueIdentity, 210, "HT_15", string.Format("{0}%", input.HT_15_Over), string.Format("{0}%", input.HT_15_Over), (decimal)input.HT_15_Over / 100, (decimal)input.HT_15_Over / 100, serial, (int)StatisticType.Average, bySide),
                new StatisticInfoHolder(input.UniqueIdentity, 215, "SH_05", string.Format("{0}%", input.SH_05_Over), string.Format("{0}%", input.SH_05_Over), (decimal)input.SH_05_Over / 100, (decimal)input.SH_05_Over / 100, serial, (int)StatisticType.Average, bySide),
                new StatisticInfoHolder(input.UniqueIdentity, 220, "SH_15", string.Format("{0}%", input.SH_15_Over), string.Format("{0}%", input.SH_15_Over), (decimal)input.SH_15_Over / 100, (decimal)input.SH_15_Over / 100, serial, (int)StatisticType.Average, bySide)
            };

            if (input.Average_FT_Corners_HomeTeam >= 0 && input.Average_FT_Corners_AwayTeam >= 0)
            {
                result.Add(new StatisticInfoHolder(input.UniqueIdentity, 50, "Ind_Poss_FT", string.Format("{0}%", input.Home_Possesion), string.Format("{0}%", input.Away_Possesion), (decimal)input.Home_Possesion / 100, (decimal)input.Away_Possesion / 100, serial, (int)StatisticType.Average, bySide));

                result.Add(new StatisticInfoHolder(input.UniqueIdentity, 55, "Ind_Avg_Shut_FT", input.Average_FT_Shut_HomeTeam.ToString("0.00"), input.Average_FT_Shut_AwayTeam.ToString("0.00"), input.Average_FT_Shut_HomeTeam, input.Average_FT_Shut_AwayTeam, serial, (int)StatisticType.Average, bySide));

                result.Add(new StatisticInfoHolder(input.UniqueIdentity, 60, "Ind_Avg_ShutOnTrg_FT", input.Average_FT_ShutOnTarget_HomeTeam.ToString("0.00"), input.Average_FT_ShutOnTarget_AwayTeam.ToString("0.00"), input.Average_FT_ShutOnTarget_HomeTeam, input.Average_FT_ShutOnTarget_AwayTeam, serial, (int)StatisticType.Average, bySide));

                result.Add(new StatisticInfoHolder(input.UniqueIdentity, 104, "Ind_Avg_GK_Saves_FT", input.Average_FT_GK_Saves_HomeTeam.ToString("0.00"), input.Average_FT_GK_Saves_AwayTeam.ToString("0.00"), input.Average_FT_GK_Saves_HomeTeam, input.Average_FT_GK_Saves_AwayTeam, serial, (int)StatisticType.Average, bySide));

                result.Add(new StatisticInfoHolder(input.UniqueIdentity, 300, "Ind_Avg_Corner_FT", input.Average_FT_Corners_HomeTeam.ToString("0.00"), input.Average_FT_Corners_AwayTeam.ToString("0.00"), input.Average_FT_Corners_HomeTeam, input.Average_FT_Corners_AwayTeam, serial, (int)StatisticType.Average, bySide));

                result.Add(new StatisticInfoHolder(input.UniqueIdentity, 330, "Ind_Cor3_5_FT", string.Format("{0}%", input.Corner_Home_3_5_Over), string.Format("{0}%", input.Corner_Away_3_5_Over), (decimal)input.Corner_Home_3_5_Over / 100, (decimal)input.Corner_Away_3_5_Over / 100, serial, (int)StatisticType.Average, bySide));
                result.Add(new StatisticInfoHolder(input.UniqueIdentity, 335, "Ind_Cor4_5_FT", string.Format("{0}%", input.Corner_Home_4_5_Over), string.Format("{0}%", input.Corner_Away_4_5_Over), (decimal)input.Corner_Home_4_5_Over / 100, (decimal)input.Corner_Away_4_5_Over / 100, serial, (int)StatisticType.Average, bySide));
                result.Add(new StatisticInfoHolder(input.UniqueIdentity, 340, "Ind_Cor5_5_FT", string.Format("{0}%", input.Corner_Home_5_5_Over), string.Format("{0}%", input.Corner_Away_5_5_Over), (decimal)input.Corner_Home_5_5_Over / 100, (decimal)input.Corner_Away_5_5_Over / 100, serial, (int)StatisticType.Average, bySide));

                result.Add(new StatisticInfoHolder(input.UniqueIdentity, 345, "Cor7_5_FT", string.Format("{0}%", input.Corner_7_5_Over), string.Format("{0}%", input.Corner_7_5_Over), (decimal)input.Corner_7_5_Over / 100, (decimal)input.Corner_7_5_Over / 100, serial, (int)StatisticType.Average, bySide));
                result.Add(new StatisticInfoHolder(input.UniqueIdentity, 350, "Cor8_5_FT", string.Format("{0}%", input.Corner_8_5_Over), string.Format("{0}%", input.Corner_8_5_Over), (decimal)input.Corner_8_5_Over / 100, (decimal)input.Corner_8_5_Over / 100, serial, (int)StatisticType.Average, bySide));
                result.Add(new StatisticInfoHolder(input.UniqueIdentity, 355, "Cor9_5_FT", string.Format("{0}%", input.Corner_9_5_Over), string.Format("{0}%", input.Corner_9_5_Over), (decimal)input.Corner_9_5_Over / 100, (decimal)input.Corner_9_5_Over / 100, serial, (int)StatisticType.Average, bySide));

                result.Add(new StatisticInfoHolder(input.UniqueIdentity, 360, "Ind_Cor_Win", string.Format("{0}%", input.Is_Corner_FT_Win1), string.Format("{0}%", input.Is_Corner_FT_Win2), (decimal)input.Is_Corner_FT_Win1 / 100, (decimal)input.Is_Corner_FT_Win2 / 100, serial, (int)StatisticType.Average, bySide));
                result.Add(new StatisticInfoHolder(input.UniqueIdentity, 365, "Cor_X", string.Format("{0}%", input.Is_Corner_FT_X), string.Format("{0}%", input.Is_Corner_FT_X), (decimal)input.Is_Corner_FT_X / 100, (decimal)input.Is_Corner_FT_X / 100, serial, (int)StatisticType.Average, bySide));
            }

            return result;
        }

        private static List<StatisticInfoHolder> GenerateComparisonStatInfoes(ComparisonStatisticsHolder? input, int serial, int bySide)
        {
            if (input == null) return null;

            var result = new List<StatisticInfoHolder>
            {
                new StatisticInfoHolder(input.UniqueIdentity, 100, "Ind_Avg_Goal_FT", input.Average_FT_Goals_HomeTeam.ToString("0.00"), input.Average_FT_Goals_AwayTeam.ToString("0.00"), input.Average_FT_Goals_HomeTeam, input.Average_FT_Goals_AwayTeam, serial, (int)StatisticType.Comparison, bySide),
                new StatisticInfoHolder(input.UniqueIdentity, 103, "Ind_Avg_Conc_Goal_FT", input.Average_FT_Conceded_Goals_HomeTeam.ToString("0.00"), input.Average_FT_Conceded_Goals_AwayTeam.ToString("0.00"), input.Average_FT_Conceded_Goals_HomeTeam, input.Average_FT_Conceded_Goals_AwayTeam, serial, (int)StatisticType.Comparison, bySide),
                new StatisticInfoHolder(input.UniqueIdentity, 105, "Ind_Avg_Goal_HT", input.Average_HT_Goals_HomeTeam.ToString("0.00"), input.Average_HT_Goals_AwayTeam.ToString("0.00"), input.Average_HT_Goals_HomeTeam, input.Average_HT_Goals_AwayTeam, serial, (int)StatisticType.Comparison, bySide),
                new StatisticInfoHolder(input.UniqueIdentity, 107, "Ind_Avg_Conc_Goal_HT", input.Average_HT_Conceded_Goals_HomeTeam.ToString("0.00"), input.Average_HT_Conceded_Goals_AwayTeam.ToString("0.00"), input.Average_HT_Conceded_Goals_HomeTeam, input.Average_HT_Conceded_Goals_AwayTeam, serial, (int)StatisticType.Comparison, bySide),
                new StatisticInfoHolder(input.UniqueIdentity, 110, "Ind_Avg_Goal_SH", input.Average_SH_Goals_HomeTeam.ToString("0.00"), input.Average_SH_Goals_AwayTeam.ToString("0.00"), input.Average_SH_Goals_HomeTeam, input.Average_SH_Goals_AwayTeam, serial, (int)StatisticType.Comparison, bySide),
                new StatisticInfoHolder(input.UniqueIdentity, 113, "Ind_Avg_Conc_Goal_SH", input.Average_SH_Conceded_Goals_HomeTeam.ToString("0.00"), input.Average_SH_Conceded_Goals_AwayTeam.ToString("0.00"), input.Average_SH_Conceded_Goals_HomeTeam, input.Average_SH_Conceded_Goals_AwayTeam, serial, (int)StatisticType.Comparison, bySide),
                new StatisticInfoHolder(input.UniqueIdentity, 115, "Ind_FT_Win", string.Format("{0}%", input.Is_FT_Win1), string.Format("{0}%", input.Is_FT_Win2), (decimal)input.Is_FT_Win1 / 100, (decimal)input.Is_FT_Win2 / 100, serial, (int)StatisticType.Comparison, bySide),
                new StatisticInfoHolder(input.UniqueIdentity, 120, "FT_X", string.Format("{0}%", input.Is_FT_X), string.Format("{0}%", input.Is_FT_X), (decimal)input.Is_FT_X / 100, (decimal)input.Is_FT_X / 100, serial, (int)StatisticType.Comparison, bySide),
                new StatisticInfoHolder(input.UniqueIdentity, 125, "Ind_HT_Win", string.Format("{0}%", input.Is_HT_Win1), string.Format("{0}%", input.Is_HT_Win2), (decimal)input.Is_HT_Win1 / 100, (decimal)input.Is_HT_Win2 / 100, serial, (int)StatisticType.Comparison, bySide),
                new StatisticInfoHolder(input.UniqueIdentity, 130, "HT_X", string.Format("{0}%", input.Is_HT_X), string.Format("{0}%", input.Is_HT_X), (decimal)input.Is_HT_X / 100, (decimal)input.Is_HT_X / 100, serial, (int)StatisticType.Comparison, bySide),
                new StatisticInfoHolder(input.UniqueIdentity, 135, "Ind_SH_Win", string.Format("{0}%", input.Is_SH_Win1), string.Format("{0}%", input.Is_SH_Win2), (decimal)input.Is_SH_Win1 / 100, (decimal)input.Is_SH_Win2 / 100, serial, (int)StatisticType.Comparison, bySide),
                new StatisticInfoHolder(input.UniqueIdentity, 140, "SH_X", string.Format("{0}%", input.Is_SH_X), string.Format("{0}%", input.Is_SH_X), (decimal)input.Is_SH_X / 100, (decimal)input.Is_SH_X / 100, serial, (int)StatisticType.Comparison, bySide),
                new StatisticInfoHolder(input.UniqueIdentity, 145, "Ind_FT_05", string.Format("{0}%", input.Home_FT_05_Over), string.Format("{0}%", input.Away_FT_05_Over), (decimal)input.Home_FT_05_Over / 100, (decimal)input.Away_FT_05_Over / 100, serial, (int)StatisticType.Comparison, bySide),
                new StatisticInfoHolder(input.UniqueIdentity, 150, "Ind_FT_15", string.Format("{0}%", input.Home_FT_15_Over), string.Format("{0}%", input.Away_FT_15_Over), (decimal)input.Home_FT_15_Over / 100, (decimal)input.Away_FT_15_Over / 100, serial, (int)StatisticType.Comparison, bySide),
                new StatisticInfoHolder(input.UniqueIdentity, 155, "Ind_HT_05", string.Format("{0}%", input.Home_HT_05_Over), string.Format("{0}%", input.Away_HT_05_Over), (decimal)input.Home_HT_05_Over / 100, (decimal)input.Away_HT_05_Over / 100, serial, (int)StatisticType.Comparison, bySide),
                new StatisticInfoHolder(input.UniqueIdentity, 160, "Ind_HT_15", string.Format("{0}%", input.Home_HT_15_Over), string.Format("{0}%", input.Away_HT_15_Over), (decimal)input.Home_HT_15_Over / 100, (decimal)input.Away_HT_15_Over / 100, serial, (int)StatisticType.Comparison, bySide),
                new StatisticInfoHolder(input.UniqueIdentity, 165, "Ind_SH_05", string.Format("{0}%", input.Home_SH_05_Over), string.Format("{0}%", input.Away_SH_05_Over), (decimal)input.Home_SH_05_Over / 100, (decimal)input.Away_SH_05_Over / 100, serial, (int)StatisticType.Comparison, bySide),
                new StatisticInfoHolder(input.UniqueIdentity, 170, "Ind_SH_15", string.Format("{0}%", input.Home_SH_15_Over), string.Format("{0}%", input.Away_SH_15_Over), (decimal)input.Home_SH_15_Over / 100, (decimal)input.Away_SH_15_Over / 100, serial, (int)StatisticType.Comparison, bySide),
                new StatisticInfoHolder(input.UniqueIdentity, 142, "Ind_WinAny", string.Format("{0}%", input.Home_Win_Any_Half), string.Format("{0}%", input.Away_Win_Any_Half), (decimal)input.Home_Win_Any_Half / 100, (decimal)input.Away_Win_Any_Half / 100, serial, (int)StatisticType.Comparison, bySide),

                new StatisticInfoHolder(input.UniqueIdentity, 175, "FT_GG", string.Format("{0}%", input.FT_GG), string.Format("{0}%", input.FT_GG), (decimal)input.FT_GG / 100, (decimal)input.FT_GG / 100, serial, (int)StatisticType.Comparison, bySide),
                new StatisticInfoHolder(input.UniqueIdentity, 180, "HT_GG", string.Format("{0}%", input.HT_GG), string.Format("{0}%", input.HT_GG), (decimal)input.HT_GG / 100, (decimal)input.HT_GG / 100, serial, (int)StatisticType.Comparison, bySide),
                new StatisticInfoHolder(input.UniqueIdentity, 185, "SH_GG", string.Format("{0}%", input.SH_GG), string.Format("{0}%", input.SH_GG), (decimal)input.SH_GG / 100, (decimal)input.SH_GG / 100, serial, (int)StatisticType.Comparison, bySide),
                new StatisticInfoHolder(input.UniqueIdentity, 190, "FT_15", string.Format("{0}%", input.FT_15_Over), string.Format("{0}%", input.FT_15_Over), (decimal)input.FT_15_Over / 100, (decimal)input.FT_15_Over / 100, serial, (int)StatisticType.Comparison, bySide),
                new StatisticInfoHolder(input.UniqueIdentity, 195, "FT_25", string.Format("{0}%", input.FT_25_Over), string.Format("{0}%", input.FT_25_Over), (decimal)input.FT_25_Over / 100, (decimal)input.FT_25_Over / 100, serial, (int)StatisticType.Comparison, bySide),
                new StatisticInfoHolder(input.UniqueIdentity, 200, "FT_35", string.Format("{0}%", input.FT_35_Over), string.Format("{0}%", input.FT_35_Over), (decimal)input.FT_35_Over / 100, (decimal)input.FT_35_Over / 100, serial, (int)StatisticType.Comparison, bySide),
                new StatisticInfoHolder(input.UniqueIdentity, 205, "HT_05", string.Format("{0}%", input.HT_05_Over), string.Format("{0}%", input.HT_05_Over), (decimal)input.HT_05_Over / 100, (decimal)input.HT_05_Over / 100, serial, (int)StatisticType.Comparison, bySide),
                new StatisticInfoHolder(input.UniqueIdentity, 210, "HT_15", string.Format("{0}%", input.HT_15_Over), string.Format("{0}%", input.HT_15_Over), (decimal)input.HT_15_Over / 100, (decimal)input.HT_15_Over / 100, serial, (int)StatisticType.Comparison, bySide),
                new StatisticInfoHolder(input.UniqueIdentity, 215, "SH_05", string.Format("{0}%", input.SH_05_Over), string.Format("{0}%", input.SH_05_Over), (decimal)input.SH_05_Over / 100, (decimal)input.SH_05_Over / 100, serial, (int)StatisticType.Comparison, bySide),
                new StatisticInfoHolder(input.UniqueIdentity, 220, "SH_15", string.Format("{0}%", input.SH_15_Over), string.Format("{0}%", input.SH_15_Over), (decimal)input.SH_15_Over / 100, (decimal)input.SH_15_Over / 100, serial, (int)StatisticType.Comparison, bySide)
            };

            if (input.Average_FT_Corners_HomeTeam >= 0 && input.Average_FT_Corners_AwayTeam >= 0)
            {
                result.Add(new StatisticInfoHolder(input.UniqueIdentity, 50, "Ind_Poss_FT", string.Format("{0}%", input.Home_Possesion), string.Format("{0}%", input.Away_Possesion), (decimal)input.Home_Possesion / 100, (decimal)input.Away_Possesion / 100, serial, (int)StatisticType.Comparison, bySide));
                result.Add(new StatisticInfoHolder(input.UniqueIdentity, 104, "Ind_Avg_GK_Saves_FT", input.Average_FT_GK_Saves_HomeTeam.ToString("0.00"), input.Average_FT_GK_Saves_AwayTeam.ToString("0.00"), input.Average_FT_GK_Saves_HomeTeam, input.Average_FT_GK_Saves_AwayTeam, serial, (int)StatisticType.Comparison, bySide));

                result.Add(new StatisticInfoHolder(input.UniqueIdentity, 55, "Ind_Avg_Shut_FT", input.Average_FT_Shut_HomeTeam.ToString("0.00"), input.Average_FT_Shut_AwayTeam.ToString("0.00"), input.Average_FT_Shut_HomeTeam, input.Average_FT_Shut_AwayTeam, serial, (int)StatisticType.Comparison, bySide));
                result.Add(new StatisticInfoHolder(input.UniqueIdentity, 60, "Ind_Avg_ShutOnTrg_FT", input.Average_FT_ShutOnTarget_HomeTeam.ToString("0.00"), input.Average_FT_ShutOnTarget_AwayTeam.ToString("0.00"), input.Average_FT_ShutOnTarget_HomeTeam, input.Average_FT_ShutOnTarget_AwayTeam, serial, (int)StatisticType.Comparison, bySide));

                result.Add(new StatisticInfoHolder(input.UniqueIdentity, 300, "Ind_Avg_Corner_FT", input.Average_FT_Corners_HomeTeam.ToString("0.00"), input.Average_FT_Corners_AwayTeam.ToString("0.00"), input.Average_FT_Corners_HomeTeam, input.Average_FT_Corners_AwayTeam, serial, (int)StatisticType.Comparison, bySide));

                result.Add(new StatisticInfoHolder(input.UniqueIdentity, 330, "Ind_Cor3_5_FT", string.Format("{0}%", input.Corner_Home_3_5_Over), string.Format("{0}%", input.Corner_Away_3_5_Over), (decimal)input.Corner_Home_3_5_Over / 100, (decimal)input.Corner_Away_3_5_Over / 100, serial, (int)StatisticType.Comparison, bySide));
                result.Add(new StatisticInfoHolder(input.UniqueIdentity, 335, "Ind_Cor4_5_FT", string.Format("{0}%", input.Corner_Home_4_5_Over), string.Format("{0}%", input.Corner_Away_4_5_Over), (decimal)input.Corner_Home_4_5_Over / 100, (decimal)input.Corner_Away_4_5_Over / 100, serial, (int)StatisticType.Comparison, bySide));
                result.Add(new StatisticInfoHolder(input.UniqueIdentity, 340, "Ind_Cor5_5_FT", string.Format("{0}%", input.Corner_Home_5_5_Over), string.Format("{0}%", input.Corner_Away_5_5_Over), (decimal)input.Corner_Home_5_5_Over / 100, (decimal)input.Corner_Away_5_5_Over / 100, serial, (int)StatisticType.Comparison, bySide));

                result.Add(new StatisticInfoHolder(input.UniqueIdentity, 345, "Cor7_5_FT", string.Format("{0}%", input.Corner_7_5_Over), string.Format("{0}%", input.Corner_7_5_Over), (decimal)input.Corner_7_5_Over / 100, (decimal)input.Corner_7_5_Over / 100, serial, (int)StatisticType.Comparison, bySide));
                result.Add(new StatisticInfoHolder(input.UniqueIdentity, 350, "Cor8_5_FT", string.Format("{0}%", input.Corner_8_5_Over), string.Format("{0}%", input.Corner_8_5_Over), (decimal)input.Corner_8_5_Over / 100, (decimal)input.Corner_8_5_Over / 100, serial, (int)StatisticType.Comparison, bySide));
                result.Add(new StatisticInfoHolder(input.UniqueIdentity, 355, "Cor9_5_FT", string.Format("{0}%", input.Corner_9_5_Over), string.Format("{0}%", input.Corner_9_5_Over), (decimal)input.Corner_9_5_Over / 100, (decimal)input.Corner_9_5_Over / 100, serial, (int)StatisticType.Comparison, bySide));

                result.Add(new StatisticInfoHolder(input.UniqueIdentity, 360, "Ind_Cor_Win", string.Format("{0}%", input.Is_Corner_FT_Win1), string.Format("{0}%", input.Is_Corner_FT_Win2), (decimal)input.Is_Corner_FT_Win1 / 100, (decimal)input.Is_Corner_FT_Win2 / 100, serial, (int)StatisticType.Comparison, bySide));
                result.Add(new StatisticInfoHolder(input.UniqueIdentity, 365, "Cor_X", string.Format("{0}%", input.Is_Corner_FT_X), string.Format("{0}%", input.Is_Corner_FT_X), (decimal)input.Is_Corner_FT_X / 100, (decimal)input.Is_Corner_FT_X / 100, serial, (int)StatisticType.Comparison, bySide));
            }

            return result;
        }


        public static List<StatisticInfoHolder> GenerateOddPercentageStatInfoes(int serial, List<FilterResultMutateModel> resultModel, decimal range)
        {
            var oddPercentageProf = GetOddPercentageInTime(serial.ToString(), resultModel, range);

            if (oddPercentageProf == null) return null;

            var input = oddPercentageProf.GetOddPercentageStatistics();

            var result = new List<StatisticInfoHolder>
            {
                new StatisticInfoHolder(Guid.Empty, 100, "Ind_Avg_Goal_FT", input.Average_FT_Goals_HomeTeam.ToString("0.00"), input.Average_FT_Goals_AwayTeam.ToString("0.00"), input.Average_FT_Goals_HomeTeam, input.Average_FT_Goals_AwayTeam, serial, (int)StatisticType.Comparison, 0),
                new StatisticInfoHolder(Guid.Empty, 103, "Ind_Avg_Conc_Goal_FT", input.Average_FT_Conceded_Goals_HomeTeam.ToString("0.00"), input.Average_FT_Conceded_Goals_AwayTeam.ToString("0.00"), input.Average_FT_Conceded_Goals_HomeTeam, input.Average_FT_Conceded_Goals_AwayTeam, serial, (int)StatisticType.Comparison, 0),
                new StatisticInfoHolder(Guid.Empty, 105, "Ind_Avg_Goal_HT", input.Average_HT_Goals_HomeTeam.ToString("0.00"), input.Average_HT_Goals_AwayTeam.ToString("0.00"), input.Average_HT_Goals_HomeTeam, input.Average_HT_Goals_AwayTeam, serial, (int)StatisticType.Comparison, 0),
                new StatisticInfoHolder(Guid.Empty, 107, "Ind_Avg_Conc_Goal_HT", input.Average_HT_Conceded_Goals_HomeTeam.ToString("0.00"), input.Average_HT_Conceded_Goals_AwayTeam.ToString("0.00"), input.Average_HT_Conceded_Goals_HomeTeam, input.Average_HT_Conceded_Goals_AwayTeam, serial, (int)StatisticType.Comparison, 0),
                new StatisticInfoHolder(Guid.Empty, 110, "Ind_Avg_Goal_SH", input.Average_SH_Goals_HomeTeam.ToString("0.00"), input.Average_SH_Goals_AwayTeam.ToString("0.00"), input.Average_SH_Goals_HomeTeam, input.Average_SH_Goals_AwayTeam, serial, (int)StatisticType.Comparison, 0),
                new StatisticInfoHolder(Guid.Empty, 113, "Ind_Avg_Conc_Goal_SH", input.Average_SH_Conceded_Goals_HomeTeam.ToString("0.00"), input.Average_SH_Conceded_Goals_AwayTeam.ToString("0.00"), input.Average_SH_Conceded_Goals_HomeTeam, input.Average_SH_Conceded_Goals_AwayTeam, serial, (int)StatisticType.Comparison, 0),
                new StatisticInfoHolder(Guid.Empty, 115, "Ind_FT_Win", string.Format("{0}%", input.Is_FT_Win1), string.Format("{0}%", input.Is_FT_Win2), (decimal)input.Is_FT_Win1 / 100, (decimal)input.Is_FT_Win2 / 100, serial, (int)StatisticType.Comparison, 0),
                new StatisticInfoHolder(Guid.Empty, 120, "FT_X", string.Format("{0}%", input.Is_FT_X), string.Format("{0}%", input.Is_FT_X), (decimal)input.Is_FT_X / 100, (decimal)input.Is_FT_X / 100, serial, (int)StatisticType.Comparison, 0),
                new StatisticInfoHolder(Guid.Empty, 125, "Ind_HT_Win", string.Format("{0}%", input.Is_HT_Win1), string.Format("{0}%", input.Is_HT_Win2), (decimal)input.Is_HT_Win1 / 100, (decimal)input.Is_HT_Win2 / 100, serial, (int)StatisticType.Comparison, 0),
                new StatisticInfoHolder(Guid.Empty, 130, "HT_X", string.Format("{0}%", input.Is_HT_X), string.Format("{0}%", input.Is_HT_X), (decimal)input.Is_HT_X / 100, (decimal)input.Is_HT_X / 100, serial, (int)StatisticType.Comparison, 0),
                new StatisticInfoHolder(Guid.Empty, 135, "Ind_SH_Win", string.Format("{0}%", input.Is_SH_Win1), string.Format("{0}%", input.Is_SH_Win2), (decimal)input.Is_SH_Win1 / 100, (decimal)input.Is_SH_Win2 / 100, serial, (int)StatisticType.Comparison, 0),
                new StatisticInfoHolder(Guid.Empty, 140, "SH_X", string.Format("{0}%", input.Is_SH_X), string.Format("{0}%", input.Is_SH_X), (decimal)input.Is_SH_X / 100, (decimal)input.Is_SH_X / 100, serial, (int)StatisticType.Comparison, 0),
                new StatisticInfoHolder(Guid.Empty, 145, "Ind_FT_05", string.Format("{0}%", input.Home_FT_05_Over), string.Format("{0}%", input.Away_FT_05_Over), (decimal)input.Home_FT_05_Over / 100, (decimal)input.Away_FT_05_Over / 100, serial, (int)StatisticType.Comparison, 0),
                new StatisticInfoHolder(Guid.Empty, 150, "Ind_FT_15", string.Format("{0}%", input.Home_FT_15_Over), string.Format("{0}%", input.Away_FT_15_Over), (decimal)input.Home_FT_15_Over / 100, (decimal)input.Away_FT_15_Over / 100, serial, (int)StatisticType.Comparison, 0),
                new StatisticInfoHolder(Guid.Empty, 155, "Ind_HT_05", string.Format("{0}%", input.Home_HT_05_Over), string.Format("{0}%", input.Away_HT_05_Over), (decimal)input.Home_HT_05_Over / 100, (decimal)input.Away_HT_05_Over / 100, serial, (int)StatisticType.Comparison, 0),
                new StatisticInfoHolder(Guid.Empty, 160, "Ind_HT_15", string.Format("{0}%", input.Home_HT_15_Over), string.Format("{0}%", input.Away_HT_15_Over), (decimal)input.Home_HT_15_Over / 100, (decimal)input.Away_HT_15_Over / 100, serial, (int)StatisticType.Comparison, 0),
                new StatisticInfoHolder(Guid.Empty, 165, "Ind_SH_05", string.Format("{0}%", input.Home_SH_05_Over), string.Format("{0}%", input.Away_SH_05_Over), (decimal)input.Home_SH_05_Over / 100, (decimal)input.Away_SH_05_Over / 100, serial, (int)StatisticType.Comparison, 0),
                new StatisticInfoHolder(Guid.Empty, 170, "Ind_SH_15", string.Format("{0}%", input.Home_SH_15_Over), string.Format("{0}%", input.Away_SH_15_Over), (decimal)input.Home_SH_15_Over / 100, (decimal)input.Away_SH_15_Over / 100, serial, (int)StatisticType.Comparison, 0),
                new StatisticInfoHolder(Guid.Empty, 142, "Ind_WinAny", string.Format("{0}%", input.Home_Win_Any_Half), string.Format("{0}%", input.Away_Win_Any_Half), (decimal)input.Home_Win_Any_Half / 100, (decimal)input.Away_Win_Any_Half / 100, serial, (int)StatisticType.Comparison, 0),

                new StatisticInfoHolder(Guid.Empty, 175, "FT_GG", string.Format("{0}%", input.FT_GG), string.Format("{0}%", input.FT_GG), (decimal)input.FT_GG / 100, (decimal)input.FT_GG / 100, serial, (int)StatisticType.Comparison, 0),
                new StatisticInfoHolder(Guid.Empty, 180, "HT_GG", string.Format("{0}%", input.HT_GG), string.Format("{0}%", input.HT_GG), (decimal)input.HT_GG / 100, (decimal)input.HT_GG / 100, serial, (int)StatisticType.Comparison, 0),
                new StatisticInfoHolder(Guid.Empty, 185, "SH_GG", string.Format("{0}%", input.SH_GG), string.Format("{0}%", input.SH_GG), (decimal)input.SH_GG / 100, (decimal)input.SH_GG / 100, serial, (int)StatisticType.Comparison, 0),
                new StatisticInfoHolder(Guid.Empty, 190, "FT_15", string.Format("{0}%", input.FT_15_Over), string.Format("{0}%", input.FT_15_Over), (decimal)input.FT_15_Over / 100, (decimal)input.FT_15_Over / 100, serial, (int)StatisticType.Comparison, 0),
                new StatisticInfoHolder(Guid.Empty, 195, "FT_25", string.Format("{0}%", input.FT_25_Over), string.Format("{0}%", input.FT_25_Over), (decimal)input.FT_25_Over / 100, (decimal)input.FT_25_Over / 100, serial, (int)StatisticType.Comparison, 0),
                new StatisticInfoHolder(Guid.Empty, 200, "FT_35", string.Format("{0}%", input.FT_35_Over), string.Format("{0}%", input.FT_35_Over), (decimal)input.FT_35_Over / 100, (decimal)input.FT_35_Over / 100, serial, (int)StatisticType.Comparison, 0),
                new StatisticInfoHolder(Guid.Empty, 205, "HT_05", string.Format("{0}%", input.HT_05_Over), string.Format("{0}%", input.HT_05_Over), (decimal)input.HT_05_Over / 100, (decimal)input.HT_05_Over / 100, serial, (int)StatisticType.Comparison, 0),
                new StatisticInfoHolder(Guid.Empty, 210, "HT_15", string.Format("{0}%", input.HT_15_Over), string.Format("{0}%", input.HT_15_Over), (decimal)input.HT_15_Over / 100, (decimal)input.HT_15_Over / 100, serial, (int)StatisticType.Comparison, 0),
                new StatisticInfoHolder(Guid.Empty, 215, "SH_05", string.Format("{0}%", input.SH_05_Over), string.Format("{0}%", input.SH_05_Over), (decimal)input.SH_05_Over / 100, (decimal)input.SH_05_Over / 100, serial, (int)StatisticType.Comparison, 0),
                new StatisticInfoHolder(Guid.Empty, 220, "SH_15", string.Format("{0}%", input.SH_15_Over), string.Format("{0}%", input.SH_15_Over), (decimal)input.SH_15_Over / 100, (decimal)input.SH_15_Over / 100, serial, (int)StatisticType.Comparison, 0)
            };

            if (input.Average_FT_Corners_HomeTeam >= 0 && input.Average_FT_Corners_AwayTeam >= 0)
            {
                result.Add(new StatisticInfoHolder(Guid.Empty, 50, "Ind_Poss_FT", string.Format("{0}%", input.Home_Possesion), string.Format("{0}%", input.Away_Possesion), (decimal)input.Home_Possesion / 100, (decimal)input.Away_Possesion / 100, serial, (int)StatisticType.Comparison, 0));
                result.Add(new StatisticInfoHolder(Guid.Empty, 104, "Ind_Avg_GK_Saves_FT", input.Average_FT_GK_Saves_HomeTeam.ToString("0.00"), input.Average_FT_GK_Saves_AwayTeam.ToString("0.00"), input.Average_FT_GK_Saves_HomeTeam, input.Average_FT_GK_Saves_AwayTeam, serial, (int)StatisticType.Comparison, 0));

                result.Add(new StatisticInfoHolder(Guid.Empty, 55, "Ind_Avg_Shut_FT", input.Average_FT_Shut_HomeTeam.ToString("0.00"), input.Average_FT_Shut_AwayTeam.ToString("0.00"), input.Average_FT_Shut_HomeTeam, input.Average_FT_Shut_AwayTeam, serial, (int)StatisticType.Comparison, 0));
                result.Add(new StatisticInfoHolder(Guid.Empty, 60, "Ind_Avg_ShutOnTrg_FT", input.Average_FT_ShutOnTarget_HomeTeam.ToString("0.00"), input.Average_FT_ShutOnTarget_AwayTeam.ToString("0.00"), input.Average_FT_ShutOnTarget_HomeTeam, input.Average_FT_ShutOnTarget_AwayTeam, serial, (int)StatisticType.Comparison, 0));

                result.Add(new StatisticInfoHolder(Guid.Empty, 300, "Ind_Avg_Corner_FT", input.Average_FT_Corners_HomeTeam.ToString("0.00"), input.Average_FT_Corners_AwayTeam.ToString("0.00"), input.Average_FT_Corners_HomeTeam, input.Average_FT_Corners_AwayTeam, serial, (int)StatisticType.Comparison, 0));

                result.Add(new StatisticInfoHolder(Guid.Empty, 330, "Ind_Cor3_5_FT", string.Format("{0}%", input.Corner_Home_3_5_Over), string.Format("{0}%", input.Corner_Away_3_5_Over), (decimal)input.Corner_Home_3_5_Over / 100, (decimal)input.Corner_Away_3_5_Over / 100, serial, (int)StatisticType.Comparison, 0));
                result.Add(new StatisticInfoHolder(Guid.Empty, 335, "Ind_Cor4_5_FT", string.Format("{0}%", input.Corner_Home_4_5_Over), string.Format("{0}%", input.Corner_Away_4_5_Over), (decimal)input.Corner_Home_4_5_Over / 100, (decimal)input.Corner_Away_4_5_Over / 100, serial, (int)StatisticType.Comparison, 0));
                result.Add(new StatisticInfoHolder(Guid.Empty, 340, "Ind_Cor5_5_FT", string.Format("{0}%", input.Corner_Home_5_5_Over), string.Format("{0}%", input.Corner_Away_5_5_Over), (decimal)input.Corner_Home_5_5_Over / 100, (decimal)input.Corner_Away_5_5_Over / 100, serial, (int)StatisticType.Comparison, 0));

                result.Add(new StatisticInfoHolder(Guid.Empty, 345, "Cor7_5_FT", string.Format("{0}%", input.Corner_7_5_Over), string.Format("{0}%", input.Corner_7_5_Over), (decimal)input.Corner_7_5_Over / 100, (decimal)input.Corner_7_5_Over / 100, serial, (int)StatisticType.Comparison, 0));
                result.Add(new StatisticInfoHolder(Guid.Empty, 350, "Cor8_5_FT", string.Format("{0}%", input.Corner_8_5_Over), string.Format("{0}%", input.Corner_8_5_Over), (decimal)input.Corner_8_5_Over / 100, (decimal)input.Corner_8_5_Over / 100, serial, (int)StatisticType.Comparison, 0));
                result.Add(new StatisticInfoHolder(Guid.Empty, 355, "Cor9_5_FT", string.Format("{0}%", input.Corner_9_5_Over), string.Format("{0}%", input.Corner_9_5_Over), (decimal)input.Corner_9_5_Over / 100, (decimal)input.Corner_9_5_Over / 100, serial, (int)StatisticType.Comparison, 0));

                result.Add(new StatisticInfoHolder(Guid.Empty, 360, "Ind_Cor_Win", string.Format("{0}%", input.Is_Corner_FT_Win1), string.Format("{0}%", input.Is_Corner_FT_Win2), (decimal)input.Is_Corner_FT_Win1 / 100, (decimal)input.Is_Corner_FT_Win2 / 100, serial, (int)StatisticType.Comparison, 0));
                result.Add(new StatisticInfoHolder(Guid.Empty, 365, "Cor_X", string.Format("{0}%", input.Is_Corner_FT_X), string.Format("{0}%", input.Is_Corner_FT_X), (decimal)input.Is_Corner_FT_X / 100, (decimal)input.Is_Corner_FT_X / 100, serial, (int)StatisticType.Comparison, 0));
            }

            return result;
        }


        private static List<StatisticInfoHolder> GeneratePerformanceStatInfoes(TeamPerformanceStatisticsHolder? inputHome, TeamPerformanceStatisticsHolder? inputAway, int serial, int bySide)
        {
            if (inputHome == null || inputAway == null) return null;

            var result = new List<StatisticInfoHolder>
            {
                new StatisticInfoHolder(inputHome.UniqueIdentity, 100, "Ind_Avg_Goal_FT", inputHome.Average_FT_Goals_Team.ToString("0.00"), inputAway.Average_FT_Goals_Team.ToString("0.00"), inputHome.Average_FT_Goals_Team, inputAway.Average_FT_Goals_Team, serial, (int)StatisticType.Performance, bySide),
                new StatisticInfoHolder(inputHome.UniqueIdentity, 103, "Ind_Avg_Conc_Goal_FT", inputHome.Average_FT_Conceded_Goals_Team.ToString("0.00"), inputAway.Average_FT_Conceded_Goals_Team.ToString("0.00"), inputHome.Average_FT_Conceded_Goals_Team, inputAway.Average_FT_Conceded_Goals_Team, serial, (int)StatisticType.Performance, bySide),
                new StatisticInfoHolder(inputHome.UniqueIdentity, 105, "Ind_Avg_Goal_HT", inputHome.Average_HT_Goals_Team.ToString("0.00"), inputAway.Average_HT_Goals_Team.ToString("0.00"), inputHome.Average_HT_Goals_Team, inputAway.Average_HT_Goals_Team, serial, (int)StatisticType.Performance, bySide),
                new StatisticInfoHolder(inputHome.UniqueIdentity, 107, "Ind_Avg_Conc_Goal_HT", inputHome.Average_HT_Conceded_Goals_Team.ToString("0.00"), inputAway.Average_HT_Conceded_Goals_Team.ToString("0.00"), inputHome.Average_HT_Conceded_Goals_Team, inputAway.Average_HT_Conceded_Goals_Team, serial, (int)StatisticType.Performance, bySide),
                new StatisticInfoHolder(inputHome.UniqueIdentity, 110, "Ind_Avg_Goal_SH", inputHome.Average_SH_Goals_Team.ToString("0.00"), inputAway.Average_SH_Goals_Team.ToString("0.00"), inputHome.Average_SH_Goals_Team, inputAway.Average_SH_Goals_Team, serial, (int)StatisticType.Performance, bySide),
                new StatisticInfoHolder(inputHome.UniqueIdentity, 113, "Ind_Avg_Conc_Goal_SH", inputHome.Average_SH_Conceded_Goals_Team.ToString("0.00"), inputAway.Average_SH_Conceded_Goals_Team.ToString("0.00"), inputHome.Average_SH_Conceded_Goals_Team, inputAway.Average_SH_Conceded_Goals_Team, serial, (int)StatisticType.Performance, bySide),
                new StatisticInfoHolder(inputHome.UniqueIdentity, 115, "Ind_FT_Win", string.Format("{0}%", inputHome.Is_FT_Win), string.Format("{0}%", inputAway.Is_FT_Win), (decimal)inputHome.Is_FT_Win / 100, (decimal)inputAway.Is_FT_Win / 100, serial, (int)StatisticType.Performance, bySide),
                new StatisticInfoHolder(inputHome.UniqueIdentity, 120, "FT_X", string.Format("{0}%", inputHome.Is_FT_X), string.Format("{0}%", inputAway.Is_FT_X), (decimal)inputHome.Is_FT_X / 100, (decimal)inputAway.Is_FT_X / 100, serial, (int)StatisticType.Performance, bySide),
                new StatisticInfoHolder(inputHome.UniqueIdentity, 125, "Ind_HT_Win", string.Format("{0}%", inputHome.Is_HT_Win), string.Format("{0}%", inputAway.Is_HT_Win), (decimal)inputHome.Is_HT_Win / 100, (decimal)inputAway.Is_HT_Win / 100, serial, (int)StatisticType.Performance, bySide),
                new StatisticInfoHolder(inputHome.UniqueIdentity, 130, "HT_X", string.Format("{0}%", inputHome.Is_HT_X), string.Format("{0}%", inputAway.Is_HT_X), (decimal)inputHome.Is_HT_X / 100, (decimal)inputAway.Is_HT_X / 100, serial, (int)StatisticType.Performance, bySide),
                new StatisticInfoHolder(inputHome.UniqueIdentity, 135, "Ind_SH_Win", string.Format("{0}%", inputHome.Is_SH_Win), string.Format("{0}%", inputAway.Is_SH_Win), (decimal)inputHome.Is_SH_Win / 100, (decimal)inputAway.Is_SH_Win / 100, serial, (int)StatisticType.Performance, bySide),
                new StatisticInfoHolder(inputHome.UniqueIdentity, 140, "SH_X", string.Format("{0}%", inputHome.Is_SH_X), string.Format("{0}%", inputAway.Is_SH_X), (decimal)inputHome.Is_SH_X / 100, (decimal)inputAway.Is_SH_X / 100, serial, (int)StatisticType.Performance, bySide),
                new StatisticInfoHolder(inputHome.UniqueIdentity, 145, "Ind_FT_05", string.Format("{0}%", inputHome.Team_FT_05_Over), string.Format("{0}%", inputAway.Team_FT_05_Over), (decimal)inputHome.Team_FT_05_Over / 100, (decimal)inputAway.Team_FT_05_Over / 100, serial, (int)StatisticType.Performance, bySide),
                new StatisticInfoHolder(inputHome.UniqueIdentity, 150, "Ind_FT_15", string.Format("{0}%", inputHome.Team_FT_15_Over), string.Format("{0}%", inputAway.Team_FT_15_Over), (decimal)inputHome.Team_FT_15_Over / 100, (decimal)inputAway.Team_FT_15_Over / 100, serial, (int)StatisticType.Performance, bySide),
                new StatisticInfoHolder(inputHome.UniqueIdentity, 155, "Ind_HT_05", string.Format("{0}%", inputHome.Team_HT_05_Over), string.Format("{0}%", inputAway.Team_HT_05_Over), (decimal)inputHome.Team_HT_05_Over / 100, (decimal)inputAway.Team_HT_05_Over / 100, serial, (int)StatisticType.Performance, bySide),
                new StatisticInfoHolder(inputHome.UniqueIdentity, 160, "Ind_HT_15", string.Format("{0}%", inputHome.Team_HT_15_Over), string.Format("{0}%", inputAway.Team_HT_15_Over), (decimal)inputHome.Team_HT_15_Over / 100, (decimal)inputAway.Team_HT_15_Over / 100, serial, (int)StatisticType.Performance, bySide),
                new StatisticInfoHolder(inputHome.UniqueIdentity, 165, "Ind_SH_05", string.Format("{0}%", inputHome.Team_SH_05_Over), string.Format("{0}%", inputAway.Team_SH_05_Over), (decimal)inputHome.Team_SH_05_Over / 100, (decimal)inputAway.Team_SH_05_Over / 100, serial, (int)StatisticType.Performance, bySide),
                new StatisticInfoHolder(inputHome.UniqueIdentity, 170, "Ind_SH_15", string.Format("{0}%", inputHome.Team_SH_15_Over), string.Format("{0}%", inputAway.Team_SH_15_Over), (decimal)inputHome.Team_SH_15_Over / 100, (decimal)inputAway.Team_SH_15_Over / 100, serial, (int)StatisticType.Performance, bySide),
                new StatisticInfoHolder(inputHome.UniqueIdentity, 142, "Ind_WinAny", string.Format("{0}%", inputHome.Team_Win_Any_Half), string.Format("{0}%", inputAway.Team_Win_Any_Half), (decimal)inputHome.Team_Win_Any_Half / 100, (decimal)inputAway.Team_Win_Any_Half / 100, serial, (int)StatisticType.Performance, bySide),

                new StatisticInfoHolder(inputHome.UniqueIdentity, 175, "FT_GG", string.Format("{0}%", inputHome.FT_GG), string.Format("{0}%", inputAway.FT_GG), (decimal)inputHome.FT_GG / 100, (decimal)inputAway.FT_GG / 100, serial, (int)StatisticType.Performance, bySide),
                new StatisticInfoHolder(inputHome.UniqueIdentity, 180, "HT_GG", string.Format("{0}%", inputHome.HT_GG), string.Format("{0}%", inputAway.HT_GG), (decimal)inputHome.HT_GG / 100, (decimal)inputAway.HT_GG / 100, serial, (int)StatisticType.Performance, bySide),
                new StatisticInfoHolder(inputHome.UniqueIdentity, 185, "SH_GG", string.Format("{0}%", inputHome.SH_GG), string.Format("{0}%", inputAway.SH_GG), (decimal)inputHome.SH_GG / 100, (decimal)inputAway.SH_GG / 100, serial, (int)StatisticType.Performance, bySide),
                new StatisticInfoHolder(inputHome.UniqueIdentity, 190, "FT_15", string.Format("{0}%", inputHome.FT_15_Over), string.Format("{0}%", inputAway.FT_15_Over), (decimal)inputHome.FT_15_Over / 100, (decimal)inputAway.FT_15_Over / 100, serial, (int)StatisticType.Performance, bySide),
                new StatisticInfoHolder(inputHome.UniqueIdentity, 195, "FT_25", string.Format("{0}%", inputHome.FT_25_Over), string.Format("{0}%", inputAway.FT_25_Over), (decimal)inputHome.FT_25_Over / 100, (decimal)inputAway.FT_25_Over / 100, serial, (int)StatisticType.Performance, bySide),
                new StatisticInfoHolder(inputHome.UniqueIdentity, 200, "FT_35", string.Format("{0}%", inputHome.FT_35_Over), string.Format("{0}%", inputAway.FT_35_Over), (decimal)inputHome.FT_35_Over / 100, (decimal)inputAway.FT_35_Over / 100, serial, (int)StatisticType.Performance, bySide),
                new StatisticInfoHolder(inputHome.UniqueIdentity, 205, "HT_05", string.Format("{0}%", inputHome.HT_05_Over), string.Format("{0}%", inputAway.HT_05_Over), (decimal)inputHome.HT_05_Over / 100, (decimal)inputAway.HT_05_Over / 100, serial, (int)StatisticType.Performance, bySide),
                new StatisticInfoHolder(inputHome.UniqueIdentity, 210, "HT_15", string.Format("{0}%", inputHome.HT_15_Over), string.Format("{0}%", inputAway.HT_15_Over), (decimal)inputHome.HT_15_Over / 100, (decimal)inputAway.HT_15_Over / 100, serial, (int)StatisticType.Performance, bySide),
                new StatisticInfoHolder(inputHome.UniqueIdentity, 215, "SH_05", string.Format("{0}%", inputHome.SH_05_Over), string.Format("{0}%", inputAway.SH_05_Over), (decimal)inputHome.SH_05_Over / 100, (decimal)inputAway.SH_05_Over / 100, serial, (int)StatisticType.Performance, bySide),
                new StatisticInfoHolder(inputHome.UniqueIdentity, 220, "SH_15", string.Format("{0}%", inputHome.SH_15_Over), string.Format("{0}%", inputAway.SH_15_Over), (decimal)inputHome.SH_15_Over / 100, (decimal)inputAway.SH_15_Over / 100, serial, (int)StatisticType.Performance, bySide)
            };

            if (inputHome.Average_FT_Corners_Team >= 0 && inputAway.Average_FT_Corners_Team >= 0)
            {
                result.Add(new StatisticInfoHolder(inputHome.UniqueIdentity, 50, "Ind_Poss_FT", string.Format("{0}%", inputHome.Team_Possesion), string.Format("{0}%", inputAway.Team_Possesion), (decimal)inputHome.Team_Possesion / 100, (decimal)inputAway.Team_Possesion / 100, serial, (int)StatisticType.Performance, bySide));

                result.Add(new StatisticInfoHolder(inputHome.UniqueIdentity, 104, "Ind_Avg_GK_Saves_FT", inputHome.Average_FT_GK_Saves_Team.ToString("0.00"), inputAway.Average_FT_GK_Saves_Team.ToString("0.00"), inputHome.Average_FT_GK_Saves_Team, inputAway.Average_FT_GK_Saves_Team, serial, (int)StatisticType.Performance, bySide));

                result.Add(new StatisticInfoHolder(inputHome.UniqueIdentity, 55, "Ind_Avg_Shut_FT", inputHome.Average_FT_Shut_Team.ToString("0.00"), inputAway.Average_FT_Shut_Team.ToString("0.00"), inputHome.Average_FT_Shut_Team, inputAway.Average_FT_Shut_Team, serial, (int)StatisticType.Performance, bySide));
                result.Add(new StatisticInfoHolder(inputHome.UniqueIdentity, 60, "Ind_Avg_ShutOnTrg_FT", inputHome.Average_FT_ShutOnTarget_Team.ToString("0.00"), inputAway.Average_FT_ShutOnTarget_Team.ToString("0.00"), inputHome.Average_FT_ShutOnTarget_Team, inputAway.Average_FT_ShutOnTarget_Team, serial, (int)StatisticType.Performance, bySide));

                result.Add(new StatisticInfoHolder(inputHome.UniqueIdentity, 300, "Ind_Avg_Corner_FT", inputHome.Average_FT_Corners_Team.ToString("0.00"), inputAway.Average_FT_Corners_Team.ToString("0.00"), inputHome.Average_FT_Corners_Team, inputAway.Average_FT_Corners_Team, serial, (int)StatisticType.Performance, bySide));

                result.Add(new StatisticInfoHolder(inputHome.UniqueIdentity, 330, "Ind_Cor3_5_FT", string.Format("{0}%", inputHome.Corner_Team_3_5_Over), string.Format("{0}%", inputAway.Corner_Team_3_5_Over), (decimal)inputHome.Corner_Team_3_5_Over / 100, (decimal)inputAway.Corner_Team_3_5_Over / 100, serial, (int)StatisticType.Performance, bySide));
                result.Add(new StatisticInfoHolder(inputHome.UniqueIdentity, 335, "Ind_Cor4_5_FT", string.Format("{0}%", inputHome.Corner_Team_4_5_Over), string.Format("{0}%", inputAway.Corner_Team_4_5_Over), (decimal)inputHome.Corner_Team_4_5_Over / 100, (decimal)inputAway.Corner_Team_4_5_Over / 100, serial, (int)StatisticType.Performance, bySide));
                result.Add(new StatisticInfoHolder(inputHome.UniqueIdentity, 340, "Ind_Cor5_5_FT", string.Format("{0}%", inputHome.Corner_Team_5_5_Over), string.Format("{0}%", inputAway.Corner_Team_5_5_Over), (decimal)inputHome.Corner_Team_5_5_Over / 100, (decimal)inputAway.Corner_Team_5_5_Over / 100, serial, (int)StatisticType.Performance, bySide));

                result.Add(new StatisticInfoHolder(inputHome.UniqueIdentity, 345, "Cor7_5_FT", string.Format("{0}%", inputHome.Corner_7_5_Over), string.Format("{0}%", inputAway.Corner_7_5_Over), (decimal)inputHome.Corner_7_5_Over / 100, (decimal)inputAway.Corner_7_5_Over / 100, serial, (int)StatisticType.Performance, bySide));
                result.Add(new StatisticInfoHolder(inputHome.UniqueIdentity, 350, "Cor8_5_FT", string.Format("{0}%", inputHome.Corner_8_5_Over), string.Format("{0}%", inputAway.Corner_8_5_Over), (decimal)inputHome.Corner_8_5_Over / 100, (decimal)inputAway.Corner_8_5_Over / 100, serial, (int)StatisticType.Performance, bySide));
                result.Add(new StatisticInfoHolder(inputHome.UniqueIdentity, 355, "Cor9_5_FT", string.Format("{0}%", inputHome.Corner_9_5_Over), string.Format("{0}%", inputAway.Corner_9_5_Over), (decimal)inputHome.Corner_9_5_Over / 100, (decimal)inputAway.Corner_9_5_Over / 100, serial, (int)StatisticType.Performance, bySide));

                result.Add(new StatisticInfoHolder(inputHome.UniqueIdentity, 360, "Ind_Cor_Win", string.Format("{0}%", inputHome.Is_Corner_FT_Win), string.Format("{0}%", inputAway.Is_Corner_FT_Win), (decimal)inputHome.Is_Corner_FT_Win / 100, (decimal)inputAway.Is_Corner_FT_Win / 100, serial, (int)StatisticType.Performance, bySide));
                result.Add(new StatisticInfoHolder(inputHome.UniqueIdentity, 365, "Cor_X", string.Format("{0}%", inputHome.Is_Corner_FT_X), string.Format("{0}%", inputAway.Is_Corner_FT_X), (decimal)inputHome.Is_Corner_FT_X / 100, (decimal)inputAway.Is_Corner_FT_X / 100, serial, (int)StatisticType.Performance, bySide));
            }

            return result;
        }

        private static string ExtractLeagueName(string src, CountryContainerTemp containerTemp)
        {
            var rgxCountryLeagueMix = new Regex(PatternConstant.UnstartedMatchPattern.CountryAndLeague);
            var rgxLeague = new Regex(PatternConstant.UnstartedMatchPattern.League);
            return src.ResolveLeagueByRegex(containerTemp, rgxCountryLeagueMix, rgxLeague);
        }

        private static string ExtractCountryName(string src, CountryContainerTemp containerTemp)
        {
            var rgxCountryLeagueMix = new Regex(PatternConstant.UnstartedMatchPattern.CountryAndLeague);
            var rgxCountry = new Regex(PatternConstant.UnstartedMatchPattern.Country);
            return src.ResolveCountryByRegex(containerTemp, rgxCountryLeagueMix, rgxCountry);
        }

        private static string ExtractTimeMatch(string src)
        {
            var rgxTime = new Regex(PatternConstant.UnstartedMatchPattern.Time);
            return rgxTime.Matches(src)[0].Groups[1].Value.Trim();
        }

        private static LeagueHolder? FindCurrentLeagueContainer(LeagueContainer leagueContainer, string countryName, string leagueName)
        {
            return leagueContainer.LeagueHolders.FirstOrDefault(x => x.Country.Trim().ToLower() == countryName.ToLower().Trim() && x.League.ToLower().Trim() == leagueName.ToLower().Trim());
        }

        private static JobAnalyseModel CreateAnalyseModel(string serial, IMatchBetService matchBetService, IFilterResultService filterResultService, CountryContainerTemp containerTemp)
        {
            try
            {
                var compRes = GetComparisonOnlyDbProfilerResult(serial, matchBetService, filterResultService, containerTemp, TeamSide.Home);
                var compDbRes = GetComparisonProfilerResult(serial, TeamSide.Home);
                var perfHome = GetFormPerformanceProfiler(serial, matchBetService, containerTemp, TeamSide.Home);
                var perfAway = GetFormPerformanceProfiler(serial, matchBetService, containerTemp, TeamSide.Away);

                var analyseModel = new JobAnalyseModel
                {
                    ComparisonOnlyDB = compRes,
                    ComparisonInfoContainer = compDbRes,
                    HomeTeam_FormPerformanceGuessContainer = perfHome,
                    AwayTeam_FormPerformanceGuessContainer = perfAway,
                    StandingInfoModel = GetStandingInfoModel(serial)
                };

                if (analyseModel.ComparisonInfoContainer == null || analyseModel.HomeTeam_FormPerformanceGuessContainer == null || analyseModel.AwayTeam_FormPerformanceGuessContainer == null)
                {
                    return null;
                }

                return analyseModel;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private static DateTime CalculateMatchDateTime(string timeMatch)
        {
            TimeSpan matchTime = TimeSpan.Parse(timeMatch).Add(TimeSpan.FromHours(1));
            return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, matchTime.Hours, matchTime.Minutes, 0);
        }

        private static MatchIdentifier CreateMatchIdentifier(JobAnalyseModel analyseModel, string serial, DateTime matchDateTime)
        {
            return new MatchIdentifier
            {
                HomeTeam = analyseModel.ComparisonInfoContainer.Home,
                AwayTeam = analyseModel.ComparisonInfoContainer.Away,
                Serial = Convert.ToInt32(serial),
                MatchDateTime = matchDateTime
            };
        }

        private static void AddStatisticsToLeagueStatistic(JobAnalyseModel analyseModel, LeagueStatisticsHolder leagueStatistic, int matchIdentifierId)
        {
            AddIfNotNull(leagueStatistic.AverageStatisticsHolders, analyseModel.GetAverageStatistics((int)BySideType.HomeAway), matchIdentifierId);
            AddIfNotNull(leagueStatistic.AverageStatisticsHolders, analyseModel.GetAverageStatistics((int)BySideType.General), matchIdentifierId);

            AddIfNotNull(leagueStatistic.ComparisonStatisticsHolders, analyseModel.GetComparisonStatistics((int)BySideType.HomeAway), matchIdentifierId);
            AddIfNotNull(leagueStatistic.ComparisonStatisticsHolders, analyseModel.GetComparisonStatistics((int)BySideType.General), matchIdentifierId);

            AddIfNotNull(leagueStatistic.TeamPerformanceStatisticsHolders, analyseModel.GetHomePerformanceStatistics((int)BySideType.HomeAway), matchIdentifierId);
            AddIfNotNull(leagueStatistic.TeamPerformanceStatisticsHolders, analyseModel.GetHomePerformanceStatistics((int)BySideType.General), matchIdentifierId);
            AddIfNotNull(leagueStatistic.TeamPerformanceStatisticsHolders, analyseModel.GetAwayPerformanceStatistics((int)BySideType.HomeAway), matchIdentifierId);
            AddIfNotNull(leagueStatistic.TeamPerformanceStatisticsHolders, analyseModel.GetAwayPerformanceStatistics((int)BySideType.General), matchIdentifierId);
        }

        private static void AddIfNotNull<T>(ICollection<T> collection, T item, int matchIdentifierId) where T : BaseStatisticsHolder
        {
            if (item != null)
            {
                item.MatchIdentifierId = matchIdentifierId;
                collection.Add(item);
            }
        }

        public static List<JobAnalyseModelNisbi> GetJobAnalyseModelResultNisbi(IMatchBetService matchBetService, IFilterResultService filterResultService, CountryContainerTemp containerTemp, LeagueContainer leagueContainer, List<string> serials)
        {
            string mainUrl = _defaultMatchUrl;

            List<JobAnalyseModelNisbi> listAnalyseModel = new List<JobAnalyseModelNisbi>();

            for (int i = 0; i < serials.Count; i++)
            {
                var rgxCountryLeagueMix = new Regex(PatternConstant.UnstartedMatchPattern.CountryAndLeague);
                var rgxLeague = new Regex(PatternConstant.UnstartedMatchPattern.League);
                var rgxCountry = new Regex(PatternConstant.UnstartedMatchPattern.Country);

                string src = _webOperator.GetMinifiedString($"{mainUrl}{serials[i]}");

                string leagueName = src.ResolveLeagueByRegex(containerTemp, rgxCountryLeagueMix, rgxLeague);
                string countryName = src.ResolveCountryByRegex(containerTemp, rgxCountryLeagueMix, rgxCountry);

                bool validToGo = leagueContainer.LeagueHolders.Any(x => x.Country.Trim().ToLower() == countryName.ToLower().Trim() && x.League.ToLower().Trim() == leagueName.ToLower().Trim());

                if (!validToGo) continue;

                var analyseModelOne = new JobAnalyseModelNisbi
                {
                    ComparisonInfoContainer = GetComparisonProfilerResult(serials[i], TeamSide.Home),

                    HomeTeam_FormPerformanceGuessContainer = GetFormPerformanceProfiler(serials[i], matchBetService, containerTemp, TeamSide.Home),
                    AwayTeam_FormPerformanceGuessContainer = GetFormPerformanceProfiler(serials[i], matchBetService, containerTemp, TeamSide.Away)
                };

                if (analyseModelOne.ComparisonInfoContainer == null || analyseModelOne.HomeTeam_FormPerformanceGuessContainer == null || analyseModelOne.AwayTeam_FormPerformanceGuessContainer == null)
                {
                    continue;
                }

                listAnalyseModel.Add(analyseModelOne);
            }

            return listAnalyseModel;
        }


        public static List<VirtualOddAnalyseInTimeModel> GetJobAnalyseModelResult_TEST(SystemCheckerContainer model, IMatchBetService matchBetService, IFilterResultService filterResultService, CountryContainerTemp containerTemp)
        {
            var listSerials = model.SerialsBeforeGenerated == null || model.SerialsBeforeGenerated.Count <= 0 ? SplitSerials(model.SerialsText) : model.SerialsBeforeGenerated;

            List<MatchOddResponseInTimeModel> result = new List<MatchOddResponseInTimeModel>();
            //result = _proceeder.GenerateCalculationModelInTimeInformations(listSerials, model.CountDownMinutes, model.IsAnalyseAnyTime, containerTemp);

            //using (var writer = new StreamWriter(_pathJsonFiles.GetJsonFileByFormat("InTimeContainer")))
            //{
            //    writer.Write(JsonConvert.SerializeObject(result, Formatting.Indented));
            //}

            using (var reader = new StreamReader(_pathJsonFiles.GetJsonFileByFormat("InTimeContainer")))
            {
                var jsonFile = reader.ReadToEnd();
                result = JsonConvert.DeserializeObject<List<MatchOddResponseInTimeModel>>(jsonFile);
            }

            List<VirtualOddAnalyseInTimeModel> listAnalyseModel = new List<VirtualOddAnalyseInTimeModel>();

            for (int i = 0; i < result.Count; i++)
            {
                var analyseModelOne = new VirtualOddAnalyseInTimeModel
                {
                    Serial = result[i].Serial,
                    TargetUrl = String.Format("{0}{1}", _defaultMatchUrl, result[i].Serial),
                    TeamVsTeam = String.Format("{0} vs {1}", result[i].Home, result[i].Away),
                    Is_25_Over = CalculateVirtual_FT_25_Over(result[i]),
                    Is_GG = CalculateVirtual_FT_GG(result[i]),
                    Is_HT_15_Over = CalculateVirtual_HT_15_Over(result[i]),
                    Is_35_Over = CalculateVirtual_FT_35_Over(result[i]),
                    Is_Goals_23 = CalculateVirtual_FT_2_3_Goal(result[i]),
                    Is_Score_1_1 = CalculateVirtual_FT_Score_1_1(result[i])
                };

                if (analyseModelOne.Is_GG.Forecast == false &&
                    analyseModelOne.Is_HT_15_Over.Forecast == false &&
                    analyseModelOne.Is_25_Over.Forecast == false &&
                    analyseModelOne.Is_35_Over.Forecast == false &&
                    analyseModelOne.Is_Goals_23.Forecast == false &&
                    analyseModelOne.Is_Score_1_1.Forecast == false &&
                    analyseModelOne.Is_35_Under.Forecast == false)
                {
                    continue;
                }

                listAnalyseModel.Add(analyseModelOne);
            }

            return listAnalyseModel;
        }

        public static InTimeModelResultContainer CalculateVirtual_FT_GG(MatchOddResponseInTimeModel inTimeModel)
        {
            var result = new InTimeModelResultContainer() { CheckingType = "FT GG Var", Forecast = false, RealityResult = false };

            if (inTimeModel != null)
            {
                if (inTimeModel.Home_15_Under >= (decimal)1.40 && inTimeModel.Away_15_Under >= (decimal)1.40)
                {
                    result.Forecast = true;
                }

                int ftHomeGoals = Convert.ToInt32(inTimeModel.FT_Result.Split("-")[0].Trim());
                int ftAwayGoals = Convert.ToInt32(inTimeModel.FT_Result.Split("-")[1].Trim());

                if (ftHomeGoals > 0 && ftAwayGoals > 0)
                {
                    if (result.Forecast)
                    {
                        result.RealityResult = true;
                    }
                }
            }

            return result;
        }

        public static InTimeModelResultContainer CalculateVirtual_HT_15_Over(MatchOddResponseInTimeModel inTimeModel)
        {

            var result = new InTimeModelResultContainer() { CheckingType = "HT 1,5 Ust", Forecast = false, RealityResult = false };

            if (inTimeModel != null)
            {
                if (inTimeModel.HT_Double_12 < (decimal)1.20)
                {
                    if (inTimeModel.HT_Double_1X >= (decimal)2.10 || inTimeModel.HT_Double_X2 >= (decimal)2.10)
                    {
                        result.Forecast = true;
                    }
                }

                int htHomeGoals = Convert.ToInt32(inTimeModel.HT_Result.Split("-")[0].Trim());
                int htAwayGoals = Convert.ToInt32(inTimeModel.HT_Result.Split("-")[1].Trim());

                var totalHTGoals = htHomeGoals + htAwayGoals;

                if (totalHTGoals > 1)
                {
                    if (result.Forecast)
                    {
                        result.RealityResult = true;
                    }
                }
            }

            return result;
        }

        public static InTimeModelResultContainer CalculateVirtual_FT_25_Over(MatchOddResponseInTimeModel inTimeModel)
        {
            var result = new InTimeModelResultContainer() { CheckingType = "2.5 Over", Forecast = false, RealityResult = false };

            if (inTimeModel != null)
            {
                if (inTimeModel.FT_W1 >= (decimal)2.20 && inTimeModel.FT_W1 <= (decimal)2.27)
                {
                    if (inTimeModel.FT_W2 >= (decimal)2.40 && inTimeModel.FT_W2 <= (decimal)2.47)
                    {
                        result.Forecast = true;
                    }
                }

                if (inTimeModel.FT_W2 >= (decimal)2.20 && inTimeModel.FT_W2 <= (decimal)2.27)
                {
                    if (inTimeModel.FT_W1 >= (decimal)2.40 && inTimeModel.FT_W1 <= (decimal)2.47)
                    {
                        result.Forecast = true;
                    }
                }

                int ftHomeGoals = Convert.ToInt32(inTimeModel.FT_Result.Split("-")[0].Trim());
                int ftAwayGoals = Convert.ToInt32(inTimeModel.FT_Result.Split("-")[1].Trim());

                int totalGoals = ftHomeGoals + ftAwayGoals;

                if (totalGoals > 2)
                {
                    if (result.Forecast)
                    {
                        result.RealityResult = true;
                    }
                }
            }

            return result;
        }

        public static InTimeModelResultContainer CalculateVirtual_FT_35_Over(MatchOddResponseInTimeModel inTimeModel)
        {

            var result = new InTimeModelResultContainer() { CheckingType = "FT 3,5 Ust", Forecast = false, RealityResult = false };

            if (inTimeModel != null)
            {
                if (inTimeModel.Score_1_1 >= (decimal)10.00)
                {
                    if (inTimeModel.Score_0_0 >= (decimal)20.00 || inTimeModel.Score_3_3 >= (decimal)20.00)
                    {
                        result.Forecast = true;
                    }
                }

                int ftHomeGoals = Convert.ToInt32(inTimeModel.FT_Result.Split("-")[0].Trim());
                int ftAwayGoals = Convert.ToInt32(inTimeModel.FT_Result.Split("-")[1].Trim());

                var totalFTGoals = ftHomeGoals + ftAwayGoals;

                if (totalFTGoals > 3)
                {
                    if (result.Forecast)
                    {
                        result.RealityResult = true;
                    }
                }
            }

            return result;
        }

        public static InTimeModelResultContainer CalculateVirtual_FT_2_3_Goal(MatchOddResponseInTimeModel inTimeModel)
        {

            var result = new InTimeModelResultContainer() { CheckingType = "FT 2-3 Goals", Forecast = false, RealityResult = false };

            if (inTimeModel != null)
            {
                if (inTimeModel.Goals01 >= (decimal)3.20 && inTimeModel.Goals01 <= (decimal)3.80)
                {
                    if (inTimeModel.Goals45 >= (decimal)3.20 && inTimeModel.Goals45 <= (decimal)3.80)
                    {
                        result.Forecast = true;
                    }
                }

                int ftHomeGoals = Convert.ToInt32(inTimeModel.FT_Result.Split("-")[0].Trim());
                int ftAwayGoals = Convert.ToInt32(inTimeModel.FT_Result.Split("-")[1].Trim());

                var totalFTGoals = ftHomeGoals + ftAwayGoals;

                if (totalFTGoals >= 2 && totalFTGoals <= 3)
                {
                    if (result.Forecast)
                    {
                        result.RealityResult = true;
                    }
                }
            }

            return result;
        }

        public static InTimeModelResultContainer CalculateVirtual_FT_Score_1_1(MatchOddResponseInTimeModel inTimeModel)
        {

            var result = new InTimeModelResultContainer() { CheckingType = "FT Score 1-1", Forecast = false, RealityResult = false };

            if (inTimeModel != null)
            {
                if (inTimeModel.HT_0_5_Over > (decimal)1.30)
                {
                    if (inTimeModel.Goals23 >= (decimal)1.78 && inTimeModel.Goals23 <= (decimal)1.79)
                    {
                        if (inTimeModel.Score_1_1 < (decimal)6.20)
                            result.Forecast = true;
                    }
                }

                int ftHomeGoals = Convert.ToInt32(inTimeModel.FT_Result.Split("-")[0].Trim());
                int ftAwayGoals = Convert.ToInt32(inTimeModel.FT_Result.Split("-")[1].Trim());

                if (ftHomeGoals == 1 && ftAwayGoals == 1)
                {
                    if (result.Forecast)
                    {
                        result.RealityResult = true;
                    }
                }
            }

            return result;
        }


        //public static bool CalculateVirtual_FT_35_Under(MatchOddResponseInTimeModel inTimeModel)
        //{

        //}




        public static StandingInfoModel? GetStandingInfoModel(string serial)
        {
            return _proceeder.GetStandingInfoByPattern(serial);
        }

        public static T ReadObject<T>(string fileName, FileType fileType) where T : class, new()
        {
            if (fileType == FileType.Text)
            {
                using (var str = new StreamReader(_pathTextFiles.GetTextFileByFormat(fileName)))
                {
                    return JsonConvert.DeserializeObject<T>(str.ReadToEnd());
                }
            }
            else if (fileType == FileType.Json)
            {
                using (var str = new StreamReader(_pathJsonFiles.GetJsonFileByFormat(fileName)))
                {
                    return JsonConvert.DeserializeObject<T>(str.ReadToEnd());
                }
            }

            return null;
        }

        public static string GetLeagueName(string countryLeagueCombine, CountryContainerTemp containerTemp)
        {
            if (containerTemp.Countries.Any(name => countryLeagueCombine.Contains(name.Name)))
            {
                var countryModel = containerTemp.Countries
                        .FirstOrDefault(x =>
                            countryLeagueCombine.Contains(x.Name));
                return countryLeagueCombine
                    .Split(countryModel.Name)[1].Trim()
                    .Split($"@")[0].Trim();
            }
            else
            {
                return countryLeagueCombine
                    .Split(countryLeagueCombine.Split("-")[2].Trim()
                    .Split(" ")[0])[1]
                    .Split($"@")[0].Trim();
            }
        }

        public static string GetCountryName(string countryLeagueCombine, CountryContainerTemp containerTemp)
        {
            if (containerTemp.Countries.Any(name => countryLeagueCombine.Contains(name.Name)))
            {
                return containerTemp.Countries
                        .FirstOrDefault(x =>
                            countryLeagueCombine.Contains(x.Name)).Name.Trim();
            }
            else
            {
                return countryLeagueCombine.Split("-")[2].Trim().Split(" ")[0];
            }
        }

        public static T WriteObject<T>(T entity, string fileName, FileType fileType) where T : class, new()
        {
            if (fileType == FileType.Text)
            {
                using (var str = new StreamWriter(_pathTextFiles.GetTextFileByFormat(fileName)))
                {
                    str.Write(JsonConvert.SerializeObject(entity, Formatting.Indented));
                    return entity;
                }
            }
            else if (fileType == FileType.Json)
            {
                using (var str = new StreamWriter(_pathJsonFiles.GetJsonFileByFormat(fileName)))
                {
                    var serializedData = JsonConvert.SerializeObject(entity, Formatting.Indented);
                    str.Write(serializedData);
                    return entity;
                }
            }
            return null;
        }

        public static string ReadTextObject(string fileName)
        {
            using (var str = new StreamReader(_pathTextFiles.GetTextFileByFormat(fileName)))
            {
                return str.ReadToEnd();
            }
        }

        public static FormPerformanceGuessContainer GetFormPerformanceProfiler(string serial, IMatchBetService matchBetService, CountryContainerTemp containerTemp, TeamSide teamSide)
        {
            try
            {
                var url = string.Format("{0}{1}", _defaultMatchUrl, serial);

                var result = new FormPerformanceGuessContainer() { Serial = serial };

                var minifiedSrc = _webOperator.GetMinifiedStringAsync(url).Result;

                if (string.IsNullOrEmpty(minifiedSrc)) return null;

                var regxCountryAndLeagueName = new Regex(PatternConstant.UnstartedMatchPattern.CountryAndLeague);
                var regxTeamName = teamSide == TeamSide.Away
                    ? new Regex(PatternConstant.UnstartedMatchPattern.AwayTeam)
                    : new Regex(PatternConstant.UnstartedMatchPattern.HomeTeam);

                var unchangableTeam = minifiedSrc.ResolveTextByRegex(regxTeamName);
                var countryName = minifiedSrc.ResolveCountryByRegex(containerTemp, regxCountryAndLeagueName);

                if (teamSide == TeamSide.Away)
                    result.Away = unchangableTeam;
                else
                    result.Home = unchangableTeam;

                ////////////////////// Home Away
                ///
                // Turn Back

                var latest6GamesMatchBet = teamSide == TeamSide.Away
                    ? matchBetService.GetMatchBetQueryModels(countryName, unchangableTeam, 6, x => x.AwayTeam == unchangableTeam).Data
                    : matchBetService.GetMatchBetQueryModels(countryName, unchangableTeam, 6, x => x.HomeTeam == unchangableTeam).Data;

                var list6PerformanceData = _proceeder.SelectListPerformanceDataContainers(latest6GamesMatchBet, teamSide, unchangableTeam);

                result.HomeAway = new GuessModel
                {
                    CountFound = GetBeSideCountFound(list6PerformanceData, teamSide),
                    Average_FT_Goals_AwayTeam = GenerateHomeAwayGoalsAverage(list6PerformanceData, "FT_Goals_AwayTeam", teamSide),
                    Average_FT_Goals_HomeTeam = GenerateHomeAwayGoalsAverage(list6PerformanceData, "FT_Goals_HomeTeam", teamSide),
                    Average_HT_Goals_AwayTeam = GenerateHomeAwayGoalsAverage(list6PerformanceData, "HT_Goals_AwayTeam", teamSide),
                    Average_HT_Goals_HomeTeam = GenerateHomeAwayGoalsAverage(list6PerformanceData, "HT_Goals_HomeTeam", teamSide),
                    Average_SH_Goals_AwayTeam = GenerateHomeAwayGoalsAverage(list6PerformanceData, "SH_Goals_AwayTeam", teamSide),
                    Average_SH_Goals_HomeTeam = GenerateHomeAwayGoalsAverage(list6PerformanceData, "SH_Goals_HomeTeam", teamSide),
                    Average_FT_Conceded_Goals_AwayTeam = GenerateHomeAwayGoalsAverage(list6PerformanceData, "FT_Conceded_Goals_AwayTeam", teamSide),
                    Average_FT_Conceded_Goals_HomeTeam = GenerateHomeAwayGoalsAverage(list6PerformanceData, "FT_Conceded_Goals_HomeTeam", teamSide),
                    Average_HT_Conceded_Goals_AwayTeam = GenerateHomeAwayGoalsAverage(list6PerformanceData, "HT_Conceded_Goals_AwayTeam", teamSide),
                    Average_HT_Conceded_Goals_HomeTeam = GenerateHomeAwayGoalsAverage(list6PerformanceData, "HT_Conceded_Goals_HomeTeam", teamSide),
                    Average_SH_Conceded_Goals_AwayTeam = GenerateHomeAwayGoalsAverage(list6PerformanceData, "SH_Conceded_Goals_AwayTeam", teamSide),
                    Average_SH_Conceded_Goals_HomeTeam = GenerateHomeAwayGoalsAverage(list6PerformanceData, "SH_Conceded_Goals_HomeTeam", teamSide),
                    Average_FT_Corners_AwayTeam = GenerateHomeAwayCornersPossesionShutAverage(list6PerformanceData, "AwayCornersCount", teamSide, x => x.HasCorner == true),
                    Average_FT_Corners_HomeTeam = GenerateHomeAwayCornersPossesionShutAverage(list6PerformanceData, "HomeCornersCount", teamSide, x => x.HasCorner == true),

                    Average_FT_Possesion_HomeTeam = GenerateHomeAwayCornersPossesionShutAverage(list6PerformanceData, "HomePossesionCount", teamSide, x => x.HasPossesion == true),
                    Average_FT_Possesion_AwayTeam = GenerateHomeAwayCornersPossesionShutAverage(list6PerformanceData, "AwayPossesionCount", teamSide, x => x.HasPossesion == true),
                    Average_FT_Shot_AwayTeam = GenerateHomeAwayCornersPossesionShutAverage(list6PerformanceData, "AwayShutCount", teamSide, x => x.HasShut == true),
                    Average_FT_Shot_HomeTeam = GenerateHomeAwayCornersPossesionShutAverage(list6PerformanceData, "HomeShutCount", teamSide, x => x.HasShut == true),
                    Average_FT_ShotOnTarget_AwayTeam = GenerateHomeAwayCornersPossesionShutAverage(list6PerformanceData, "AwayShutOnTargetCount", teamSide, x => x.HasShutOnTarget == true),
                    Average_FT_ShotOnTarget_HomeTeam = GenerateHomeAwayCornersPossesionShutAverage(list6PerformanceData, "HomeShutOnTargetCount", teamSide, x => x.HasShutOnTarget == true),
                    Average_FT_GK_Saves_HomeTeam = GenerateHomeAwayCornersPossesionShutAverage(list6PerformanceData, "FT_GK_Saves_HomeTeam", teamSide, x => x.HasGK_Saves == true),
                    Average_FT_GK_Saves_AwayTeam = GenerateHomeAwayCornersPossesionShutAverage(list6PerformanceData, "FT_GK_Saves_AwayTeam", teamSide, x => x.HasGK_Saves == true),

                    Corner_Away_3_5_Over = GenerateBySideCornerPossesionShutComparison(list6PerformanceData, "Corner_Away_3_5_Over", teamSide, x => x.HasCorner == true),
                    Corner_Away_4_5_Over = GenerateBySideCornerPossesionShutComparison(list6PerformanceData, "Corner_Away_4_5_Over", teamSide, x => x.HasCorner == true),
                    Corner_Away_5_5_Over = GenerateBySideCornerPossesionShutComparison(list6PerformanceData, "Corner_Away_5_5_Over", teamSide, x => x.HasCorner == true),

                    Corner_Home_3_5_Over = GenerateBySideCornerPossesionShutComparison(list6PerformanceData, "Corner_Home_3_5_Over", teamSide, x => x.HasCorner == true),
                    Corner_Home_4_5_Over = GenerateBySideCornerPossesionShutComparison(list6PerformanceData, "Corner_Home_4_5_Over", teamSide, x => x.HasCorner == true),
                    Corner_Home_5_5_Over = GenerateBySideCornerPossesionShutComparison(list6PerformanceData, "Corner_Home_5_5_Over", teamSide, x => x.HasCorner == true),

                    Corner_7_5_Over = GenerateBySideCornerPossesionShutComparison(list6PerformanceData, "Corner_7_5_Over", teamSide, x => x.HasCorner == true),
                    Corner_8_5_Over = GenerateBySideCornerPossesionShutComparison(list6PerformanceData, "Corner_8_5_Over", teamSide, x => x.HasCorner == true),
                    Corner_9_5_Over = GenerateBySideCornerPossesionShutComparison(list6PerformanceData, "Corner_9_5_Over", teamSide, x => x.HasCorner == true),

                    Away_FT_05_Over = GenerateBySideComparison(list6PerformanceData, "Away_FT_05_Over", teamSide),
                    Away_FT_15_Over = GenerateBySideComparison(list6PerformanceData, "Away_FT_15_Over", teamSide),
                    Away_HT_05_Over = GenerateBySideComparison(list6PerformanceData, "Away_HT_05_Over", teamSide),
                    Away_HT_15_Over = GenerateBySideComparison(list6PerformanceData, "Away_HT_15_Over", teamSide),
                    Away_SH_05_Over = GenerateBySideComparison(list6PerformanceData, "Away_SH_05_Over", teamSide),
                    Away_SH_15_Over = GenerateBySideComparison(list6PerformanceData, "Away_SH_15_Over", teamSide),
                    Away_Win_Any_Half = GenerateBySideComparison(list6PerformanceData, "Away_Win_Any_Half", teamSide),
                    Home_FT_05_Over = GenerateBySideComparison(list6PerformanceData, "Home_FT_05_Over", teamSide),
                    Home_FT_15_Over = GenerateBySideComparison(list6PerformanceData, "Home_FT_15_Over", teamSide),
                    Home_HT_05_Over = GenerateBySideComparison(list6PerformanceData, "Home_HT_05_Over", teamSide),
                    Home_HT_15_Over = GenerateBySideComparison(list6PerformanceData, "Home_HT_15_Over", teamSide),
                    Home_SH_05_Over = GenerateBySideComparison(list6PerformanceData, "Home_SH_05_Over", teamSide),
                    Home_SH_15_Over = GenerateBySideComparison(list6PerformanceData, "Home_SH_15_Over", teamSide),
                    Home_Win_Any_Half = GenerateBySideComparison(list6PerformanceData, "Home_Win_Any_Half", teamSide),
                    FT_15_Over = GenerateBySideComparison(list6PerformanceData, "FT_15_Over", teamSide),
                    FT_25_Over = GenerateBySideComparison(list6PerformanceData, "FT_25_Over", teamSide),
                    FT_35_Over = GenerateBySideComparison(list6PerformanceData, "FT_35_Over", teamSide),
                    FT_GG = GenerateBySideComparison(list6PerformanceData, "FT_GG", teamSide),
                    HT_GG = GenerateBySideComparison(list6PerformanceData, "HT_GG", teamSide),
                    SH_GG = GenerateBySideComparison(list6PerformanceData, "SH_GG", teamSide),
                    HT_05_Over = GenerateBySideComparison(list6PerformanceData, "HT_05_Over", teamSide),
                    HT_15_Over = GenerateBySideComparison(list6PerformanceData, "HT_15_Over", teamSide),
                    SH_05_Over = GenerateBySideComparison(list6PerformanceData, "SH_05_Over", teamSide),
                    SH_15_Over = GenerateBySideComparison(list6PerformanceData, "SH_15_Over", teamSide),
                    MoreGoalsBetweenTimes = GenerateBySideComparison(list6PerformanceData, "MoreGoalsBetweenTimes", teamSide),
                    Total_BetweenGoals = GenerateBySideComparison(list6PerformanceData, "Total_BetweenGoals", teamSide),
                    FT_Result = GenerateBySideComparison(list6PerformanceData, "FT_Result", teamSide),
                    HT_Result = GenerateBySideComparison(list6PerformanceData, "HT_Result", teamSide),
                    HT_FT_Result = GenerateBySideComparison(list6PerformanceData, "HT_FT_Result", teamSide),
                    SH_Result = GenerateBySideComparison(list6PerformanceData, "SH_Result", teamSide),
                    Is_FT_Win1 = GenerateBySideComparison(list6PerformanceData, "Is_FT_Win1", teamSide),
                    Is_FT_X = GenerateBySideComparison(list6PerformanceData, "Is_FT_X", teamSide),
                    Is_FT_Win2 = GenerateBySideComparison(list6PerformanceData, "Is_FT_Win2", teamSide),
                    Is_HT_Win1 = GenerateBySideComparison(list6PerformanceData, "Is_HT_Win1", teamSide),
                    Is_HT_X = GenerateBySideComparison(list6PerformanceData, "Is_HT_X", teamSide),
                    Is_HT_Win2 = GenerateBySideComparison(list6PerformanceData, "Is_HT_Win2", teamSide),
                    Is_SH_Win1 = GenerateBySideComparison(list6PerformanceData, "Is_SH_Win1", teamSide),
                    Is_SH_X = GenerateBySideComparison(list6PerformanceData, "Is_SH_X", teamSide),
                    Is_SH_Win2 = GenerateBySideComparison(list6PerformanceData, "Is_SH_Win2", teamSide),
                    Is_Corner_FT_Win1 = GenerateBySideCornerPossesionShutComparison(list6PerformanceData, "Is_Corner_FT_Win1", teamSide, x => x.HasCorner == true),
                    Is_Corner_FT_X = GenerateBySideCornerPossesionShutComparison(list6PerformanceData, "Is_Corner_FT_X", teamSide, x => x.HasCorner == true),
                    Is_Corner_FT_Win2 = GenerateBySideCornerPossesionShutComparison(list6PerformanceData, "Is_Corner_FT_Win2", teamSide, x => x.HasCorner == true)
                };

                ////////////////// General

                // Return Back
                var latest10GamesMatchBetGeneral = matchBetService.GetMatchBetQueryModels(countryName, unchangableTeam, 10).Data;
                var list10PerformanceDataGeneral = _proceeder.SelectListPerformanceDataContainers(latest10GamesMatchBetGeneral, teamSide, unchangableTeam);

                result.General = new GuessModel
                {
                    CountFound = GetGeneralCountFound(list10PerformanceDataGeneral, teamSide),
                    Average_FT_Goals_AwayTeam = GenerateGeneralGoalsAverage(list10PerformanceDataGeneral, "FT_Goals_AwayTeam", teamSide),
                    Average_FT_Goals_HomeTeam = GenerateGeneralGoalsAverage(list10PerformanceDataGeneral, "FT_Goals_HomeTeam", teamSide),
                    Average_HT_Goals_AwayTeam = GenerateGeneralGoalsAverage(list10PerformanceDataGeneral, "HT_Goals_AwayTeam", teamSide),
                    Average_HT_Goals_HomeTeam = GenerateGeneralGoalsAverage(list10PerformanceDataGeneral, "HT_Goals_HomeTeam", teamSide),
                    Average_SH_Goals_AwayTeam = GenerateGeneralGoalsAverage(list10PerformanceDataGeneral, "SH_Goals_AwayTeam", teamSide),
                    Average_SH_Goals_HomeTeam = GenerateGeneralGoalsAverage(list10PerformanceDataGeneral, "SH_Goals_HomeTeam", teamSide),
                    Average_FT_Conceded_Goals_AwayTeam = GenerateGeneralGoalsAverage(list10PerformanceDataGeneral, "FT_Conceded_Goals_AwayTeam", teamSide),
                    Average_FT_Conceded_Goals_HomeTeam = GenerateGeneralGoalsAverage(list10PerformanceDataGeneral, "FT_Conceded_Goals_HomeTeam", teamSide),
                    Average_HT_Conceded_Goals_AwayTeam = GenerateGeneralGoalsAverage(list10PerformanceDataGeneral, "HT_Conceded_Goals_AwayTeam", teamSide),
                    Average_HT_Conceded_Goals_HomeTeam = GenerateGeneralGoalsAverage(list10PerformanceDataGeneral, "HT_Conceded_Goals_HomeTeam", teamSide),
                    Average_SH_Conceded_Goals_AwayTeam = GenerateGeneralGoalsAverage(list10PerformanceDataGeneral, "SH_Conceded_Goals_AwayTeam", teamSide),
                    Average_SH_Conceded_Goals_HomeTeam = GenerateGeneralGoalsAverage(list10PerformanceDataGeneral, "SH_Conceded_Goals_HomeTeam", teamSide),
                    Average_FT_Corners_AwayTeam = GenerateGeneralCornersPossesionShutAverage(list10PerformanceDataGeneral, "AwayCornersCount", teamSide, x => x.HasCorner == true),
                    Average_FT_Corners_HomeTeam = GenerateGeneralCornersPossesionShutAverage(list10PerformanceDataGeneral, "HomeCornersCount", teamSide, x => x.HasCorner == true),

                    Average_FT_Possesion_HomeTeam = GenerateGeneralCornersPossesionShutAverage(list10PerformanceDataGeneral, "HomePossesionCount", teamSide, x => x.HasPossesion == true),
                    Average_FT_Possesion_AwayTeam = GenerateGeneralCornersPossesionShutAverage(list10PerformanceDataGeneral, "AwayPossesionCount", teamSide, x => x.HasPossesion == true),
                    Average_FT_Shot_AwayTeam = GenerateGeneralCornersPossesionShutAverage(list10PerformanceDataGeneral, "AwayShutCount", teamSide, x => x.HasShut == true),
                    Average_FT_Shot_HomeTeam = GenerateGeneralCornersPossesionShutAverage(list10PerformanceDataGeneral, "HomeShutCount", teamSide, x => x.HasShut == true),
                    Average_FT_ShotOnTarget_AwayTeam = GenerateGeneralCornersPossesionShutAverage(list10PerformanceDataGeneral, "AwayShutOnTargetCount", teamSide, x => x.HasShutOnTarget == true),
                    Average_FT_ShotOnTarget_HomeTeam = GenerateGeneralCornersPossesionShutAverage(list10PerformanceDataGeneral, "HomeShutOnTargetCount", teamSide, x => x.HasShutOnTarget == true),
                    Average_FT_GK_Saves_HomeTeam = GenerateGeneralCornersPossesionShutAverage(list10PerformanceDataGeneral, "FT_GK_Saves_HomeTeam", teamSide, x => x.HasGK_Saves),
                    Average_FT_GK_Saves_AwayTeam = GenerateGeneralCornersPossesionShutAverage(list10PerformanceDataGeneral, "FT_GK_Saves_AwayTeam", teamSide, x => x.HasGK_Saves),

                    Corner_Away_3_5_Over = GenerateGeneralCornerPossesionShutComparison(list10PerformanceDataGeneral, "Corner_Away_3_5_Over", teamSide, x => x.HasCorner == true),
                    Corner_Away_4_5_Over = GenerateGeneralCornerPossesionShutComparison(list10PerformanceDataGeneral, "Corner_Away_4_5_Over", teamSide, x => x.HasCorner == true),
                    Corner_Away_5_5_Over = GenerateGeneralCornerPossesionShutComparison(list10PerformanceDataGeneral, "Corner_Away_5_5_Over", teamSide, x => x.HasCorner == true),

                    Corner_Home_3_5_Over = GenerateGeneralCornerPossesionShutComparison(list10PerformanceDataGeneral, "Corner_Home_3_5_Over", teamSide, x => x.HasCorner == true),
                    Corner_Home_4_5_Over = GenerateGeneralCornerPossesionShutComparison(list10PerformanceDataGeneral, "Corner_Home_4_5_Over", teamSide, x => x.HasCorner == true),
                    Corner_Home_5_5_Over = GenerateGeneralCornerPossesionShutComparison(list10PerformanceDataGeneral, "Corner_Home_5_5_Over", teamSide, x => x.HasCorner == true),

                    Corner_7_5_Over = GenerateGeneralCornerPossesionShutComparison(list10PerformanceDataGeneral, "Corner_7_5_Over", teamSide, x => x.HasCorner == true),
                    Corner_8_5_Over = GenerateGeneralCornerPossesionShutComparison(list10PerformanceDataGeneral, "Corner_8_5_Over", teamSide, x => x.HasCorner == true),
                    Corner_9_5_Over = GenerateGeneralCornerPossesionShutComparison(list10PerformanceDataGeneral, "Corner_9_5_Over", teamSide, x => x.HasCorner == true),

                    Away_FT_05_Over = GenerateGeneralComparison(list10PerformanceDataGeneral, "Away_FT_05_Over", teamSide),
                    Away_FT_15_Over = GenerateGeneralComparison(list10PerformanceDataGeneral, "Away_FT_15_Over", teamSide),
                    Away_HT_05_Over = GenerateGeneralComparison(list10PerformanceDataGeneral, "Away_HT_05_Over", teamSide),
                    Away_HT_15_Over = GenerateGeneralComparison(list10PerformanceDataGeneral, "Away_HT_15_Over", teamSide),
                    Away_SH_05_Over = GenerateGeneralComparison(list10PerformanceDataGeneral, "Away_SH_05_Over", teamSide),
                    Away_SH_15_Over = GenerateGeneralComparison(list10PerformanceDataGeneral, "Away_SH_15_Over", teamSide),
                    Away_Win_Any_Half = GenerateGeneralComparison(list10PerformanceDataGeneral, "Away_Win_Any_Half", teamSide),
                    Home_FT_05_Over = GenerateGeneralComparison(list10PerformanceDataGeneral, "Home_FT_05_Over", teamSide),
                    Home_FT_15_Over = GenerateGeneralComparison(list10PerformanceDataGeneral, "Home_FT_15_Over", teamSide),
                    Home_HT_05_Over = GenerateGeneralComparison(list10PerformanceDataGeneral, "Home_HT_05_Over", teamSide),
                    Home_HT_15_Over = GenerateGeneralComparison(list10PerformanceDataGeneral, "Home_HT_15_Over", teamSide),
                    Home_SH_05_Over = GenerateGeneralComparison(list10PerformanceDataGeneral, "Home_SH_05_Over", teamSide),
                    Home_SH_15_Over = GenerateGeneralComparison(list10PerformanceDataGeneral, "Home_SH_15_Over", teamSide),
                    Home_Win_Any_Half = GenerateGeneralComparison(list10PerformanceDataGeneral, "Home_Win_Any_Half", teamSide),
                    FT_15_Over = GenerateGeneralComparison(list10PerformanceDataGeneral, "FT_15_Over", teamSide),
                    FT_25_Over = GenerateGeneralComparison(list10PerformanceDataGeneral, "FT_25_Over", teamSide),
                    FT_35_Over = GenerateGeneralComparison(list10PerformanceDataGeneral, "FT_35_Over", teamSide),
                    FT_GG = GenerateGeneralComparison(list10PerformanceDataGeneral, "FT_GG", teamSide),
                    HT_GG = GenerateGeneralComparison(list10PerformanceDataGeneral, "HT_GG", teamSide),
                    SH_GG = GenerateGeneralComparison(list10PerformanceDataGeneral, "SH_GG", teamSide),
                    HT_05_Over = GenerateGeneralComparison(list10PerformanceDataGeneral, "HT_05_Over", teamSide),
                    HT_15_Over = GenerateGeneralComparison(list10PerformanceDataGeneral, "HT_15_Over", teamSide),
                    SH_05_Over = GenerateGeneralComparison(list10PerformanceDataGeneral, "SH_05_Over", teamSide),
                    SH_15_Over = GenerateGeneralComparison(list10PerformanceDataGeneral, "SH_15_Over", teamSide),
                    MoreGoalsBetweenTimes = GenerateGeneralComparison(list10PerformanceDataGeneral, "MoreGoalsBetweenTimes", teamSide),
                    Total_BetweenGoals = GenerateGeneralComparison(list10PerformanceDataGeneral, "Total_BetweenGoals", teamSide),
                    FT_Result = GenerateGeneralComparison(list10PerformanceDataGeneral, "FT_Result", teamSide),
                    HT_Result = GenerateGeneralComparison(list10PerformanceDataGeneral, "HT_Result", teamSide),
                    HT_FT_Result = GenerateGeneralComparison(list10PerformanceDataGeneral, "HT_FT_Result", teamSide),
                    SH_Result = GenerateGeneralComparison(list10PerformanceDataGeneral, "SH_Result", teamSide),
                    Is_FT_Win1 = GenerateGeneralComparison(list10PerformanceDataGeneral, "Is_FT_Win1", teamSide),
                    Is_FT_X = GenerateGeneralComparison(list10PerformanceDataGeneral, "Is_FT_X", teamSide),
                    Is_FT_Win2 = GenerateGeneralComparison(list10PerformanceDataGeneral, "Is_FT_Win2", teamSide),
                    Is_HT_Win1 = GenerateGeneralComparison(list10PerformanceDataGeneral, "Is_HT_Win1", teamSide),
                    Is_HT_X = GenerateGeneralComparison(list10PerformanceDataGeneral, "Is_HT_X", teamSide),
                    Is_HT_Win2 = GenerateGeneralComparison(list10PerformanceDataGeneral, "Is_HT_Win2", teamSide),
                    Is_SH_Win1 = GenerateGeneralComparison(list10PerformanceDataGeneral, "Is_SH_Win1", teamSide),
                    Is_SH_X = GenerateGeneralComparison(list10PerformanceDataGeneral, "Is_SH_X", teamSide),
                    Is_SH_Win2 = GenerateGeneralComparison(list10PerformanceDataGeneral, "Is_SH_Win2", teamSide),
                    Is_Corner_FT_Win1 = GenerateGeneralCornerPossesionShutComparison(list10PerformanceDataGeneral, "Is_Corner_FT_Win1", teamSide, x => x.HasCorner == true),
                    Is_Corner_FT_X = GenerateGeneralCornerPossesionShutComparison(list10PerformanceDataGeneral, "Is_Corner_FT_X", teamSide, x => x.HasCorner == true),
                    Is_Corner_FT_Win2 = GenerateGeneralCornerPossesionShutComparison(list10PerformanceDataGeneral, "Is_Corner_FT_Win2", teamSide, x => x.HasCorner == true)
                };

                return result;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public static ComparisonGuessContainer GetComparisonProfilerResult(string serial, TeamSide teamSide)
        {
            var comparisonContainer = _proceeder.SelectListComparisonInfoBetweenTeams(serial);

            try
            {
                var result = comparisonContainer != null && comparisonContainer.Any() && comparisonContainer.Count >= 3 ? new ComparisonGuessContainer
                {
                    Away = comparisonContainer[0].UnchangableAwayTeam,
                    Home = comparisonContainer[0].UnchangableHomeTeam,
                    CountryName = GenerateBySideComparison(comparisonContainer, "CountryName", teamSide).FeatureName,
                    HomeAway = new GuessModel
                    {
                        CountFound = GetBeSideCountFound(comparisonContainer, teamSide),
                        Average_FT_Goals_AwayTeam = GenerateHomeAwayGoalsAverage(comparisonContainer, "FT_Goals_AwayTeam", teamSide),
                        Average_FT_Goals_HomeTeam = GenerateHomeAwayGoalsAverage(comparisonContainer, "FT_Goals_HomeTeam", teamSide),
                        Average_HT_Goals_AwayTeam = GenerateHomeAwayGoalsAverage(comparisonContainer, "HT_Goals_AwayTeam", teamSide),
                        Average_HT_Goals_HomeTeam = GenerateHomeAwayGoalsAverage(comparisonContainer, "HT_Goals_HomeTeam", teamSide),
                        Average_SH_Goals_AwayTeam = GenerateHomeAwayGoalsAverage(comparisonContainer, "SH_Goals_AwayTeam", teamSide),
                        Average_SH_Goals_HomeTeam = GenerateHomeAwayGoalsAverage(comparisonContainer, "SH_Goals_HomeTeam", teamSide),
                        Average_FT_Conceded_Goals_AwayTeam = GenerateHomeAwayGoalsAverage(comparisonContainer, "FT_Conceded_Goals_AwayTeam", teamSide),
                        Average_FT_Conceded_Goals_HomeTeam = GenerateHomeAwayGoalsAverage(comparisonContainer, "FT_Conceded_Goals_HomeTeam", teamSide),
                        Average_HT_Conceded_Goals_AwayTeam = GenerateHomeAwayGoalsAverage(comparisonContainer, "HT_Conceded_Goals_AwayTeam", teamSide),
                        Average_HT_Conceded_Goals_HomeTeam = GenerateHomeAwayGoalsAverage(comparisonContainer, "HT_Conceded_Goals_HomeTeam", teamSide),
                        Average_SH_Conceded_Goals_AwayTeam = GenerateHomeAwayGoalsAverage(comparisonContainer, "SH_Conceded_Goals_AwayTeam", teamSide),
                        Average_SH_Conceded_Goals_HomeTeam = GenerateHomeAwayGoalsAverage(comparisonContainer, "SH_Conceded_Goals_HomeTeam", teamSide),

                        Away_FT_05_Over = GenerateBySideComparison(comparisonContainer, "Away_FT_05_Over", teamSide),
                        Away_FT_15_Over = GenerateBySideComparison(comparisonContainer, "Away_FT_15_Over", teamSide),
                        Away_HT_05_Over = GenerateBySideComparison(comparisonContainer, "Away_HT_05_Over", teamSide),
                        Away_HT_15_Over = GenerateBySideComparison(comparisonContainer, "Away_HT_15_Over", teamSide), // *
                        Away_SH_05_Over = GenerateBySideComparison(comparisonContainer, "Away_SH_05_Over", teamSide),
                        Away_SH_15_Over = GenerateBySideComparison(comparisonContainer, "Away_SH_15_Over", teamSide),
                        Away_Win_Any_Half = GenerateBySideComparison(comparisonContainer, "Away_Win_Any_Half", teamSide),
                        Home_FT_05_Over = GenerateBySideComparison(comparisonContainer, "Home_FT_05_Over", teamSide),
                        Home_FT_15_Over = GenerateBySideComparison(comparisonContainer, "Home_FT_15_Over", teamSide),
                        Home_HT_05_Over = GenerateBySideComparison(comparisonContainer, "Home_HT_05_Over", teamSide),
                        Home_HT_15_Over = GenerateBySideComparison(comparisonContainer, "Home_HT_15_Over", teamSide),
                        Home_SH_05_Over = GenerateBySideComparison(comparisonContainer, "Home_SH_05_Over", teamSide),
                        Home_SH_15_Over = GenerateBySideComparison(comparisonContainer, "Home_SH_15_Over", teamSide),
                        Home_Win_Any_Half = GenerateBySideComparison(comparisonContainer, "Home_Win_Any_Half", teamSide),
                        Is_FT_Win1 = GenerateBySideComparison(comparisonContainer, "Is_FT_Win1", teamSide),
                        Is_FT_X = GenerateBySideComparison(comparisonContainer, "Is_FT_X", teamSide),
                        Is_FT_Win2 = GenerateBySideComparison(comparisonContainer, "Is_FT_Win2", teamSide), // *
                        Is_HT_Win1 = GenerateBySideComparison(comparisonContainer, "Is_HT_Win1", teamSide),
                        Is_HT_X = GenerateBySideComparison(comparisonContainer, "Is_HT_X", teamSide),
                        Is_HT_Win2 = GenerateBySideComparison(comparisonContainer, "Is_HT_Win2", teamSide),
                        Is_SH_Win1 = GenerateBySideComparison(comparisonContainer, "Is_SH_Win1", teamSide),
                        Is_SH_X = GenerateBySideComparison(comparisonContainer, "Is_SH_X", teamSide),
                        Is_SH_Win2 = GenerateBySideComparison(comparisonContainer, "Is_SH_Win2", teamSide),
                        FT_15_Over = GenerateBySideComparison(comparisonContainer, "FT_15_Over", teamSide),
                        FT_25_Over = GenerateBySideComparison(comparisonContainer, "FT_25_Over", teamSide),
                        FT_35_Over = GenerateBySideComparison(comparisonContainer, "FT_35_Over", teamSide),
                        FT_GG = GenerateBySideComparison(comparisonContainer, "FT_GG", teamSide),
                        HT_GG = GenerateBySideComparison(comparisonContainer, "HT_GG", teamSide), // *
                        SH_GG = GenerateBySideComparison(comparisonContainer, "SH_GG", teamSide),
                        HT_05_Over = GenerateBySideComparison(comparisonContainer, "HT_05_Over", teamSide),
                        HT_15_Over = GenerateBySideComparison(comparisonContainer, "HT_15_Over", teamSide),
                        SH_05_Over = GenerateBySideComparison(comparisonContainer, "SH_05_Over", teamSide),
                        SH_15_Over = GenerateBySideComparison(comparisonContainer, "SH_15_Over", teamSide),
                        MoreGoalsBetweenTimes = GenerateBySideComparison(comparisonContainer, "MoreGoalsBetweenTimes", teamSide),
                        Total_BetweenGoals = GenerateBySideComparison(comparisonContainer, "Total_BetweenGoals", teamSide),
                        FT_Result = GenerateBySideComparison(comparisonContainer, "FT_Result", teamSide),
                        HT_Result = GenerateBySideComparison(comparisonContainer, "HT_Result", teamSide),
                        HT_FT_Result = GenerateBySideComparison(comparisonContainer, "HT_FT_Result", teamSide),
                        SH_Result = GenerateBySideComparison(comparisonContainer, "SH_Result", teamSide)
                    },
                    General = new GuessModel
                    {
                        CountFound = GetGeneralCountFound(comparisonContainer, teamSide),
                        Average_FT_Goals_AwayTeam = GenerateGeneralGoalsAverage(comparisonContainer, "FT_Goals_AwayTeam", teamSide),
                        Average_FT_Goals_HomeTeam = GenerateGeneralGoalsAverage(comparisonContainer, "FT_Goals_HomeTeam", teamSide),
                        Average_HT_Goals_AwayTeam = GenerateGeneralGoalsAverage(comparisonContainer, "HT_Goals_AwayTeam", teamSide),
                        Average_HT_Goals_HomeTeam = GenerateGeneralGoalsAverage(comparisonContainer, "HT_Goals_HomeTeam", teamSide),
                        Average_SH_Goals_AwayTeam = GenerateGeneralGoalsAverage(comparisonContainer, "SH_Goals_AwayTeam", teamSide),
                        Average_SH_Goals_HomeTeam = GenerateGeneralGoalsAverage(comparisonContainer, "SH_Goals_HomeTeam", teamSide),
                        Average_FT_Conceded_Goals_AwayTeam = GenerateGeneralGoalsAverage(comparisonContainer, "FT_Conceded_Goals_AwayTeam", teamSide),
                        Average_FT_Conceded_Goals_HomeTeam = GenerateGeneralGoalsAverage(comparisonContainer, "FT_Conceded_Goals_HomeTeam", teamSide),
                        Average_HT_Conceded_Goals_AwayTeam = GenerateGeneralGoalsAverage(comparisonContainer, "HT_Conceded_Goals_AwayTeam", teamSide),
                        Average_HT_Conceded_Goals_HomeTeam = GenerateGeneralGoalsAverage(comparisonContainer, "HT_Conceded_Goals_HomeTeam", teamSide),
                        Average_SH_Conceded_Goals_AwayTeam = GenerateGeneralGoalsAverage(comparisonContainer, "SH_Conceded_Goals_AwayTeam", teamSide),
                        Average_SH_Conceded_Goals_HomeTeam = GenerateGeneralGoalsAverage(comparisonContainer, "SH_Conceded_Goals_HomeTeam", teamSide),

                        Away_FT_05_Over = GenerateGeneralComparison(comparisonContainer, "Away_FT_05_Over", teamSide),
                        Away_FT_15_Over = GenerateGeneralComparison(comparisonContainer, "Away_FT_15_Over", teamSide),
                        Away_HT_05_Over = GenerateGeneralComparison(comparisonContainer, "Away_HT_05_Over", teamSide),
                        Away_HT_15_Over = GenerateGeneralComparison(comparisonContainer, "Away_HT_15_Over", teamSide), // *
                        Away_SH_05_Over = GenerateGeneralComparison(comparisonContainer, "Away_SH_05_Over", teamSide),
                        Away_SH_15_Over = GenerateGeneralComparison(comparisonContainer, "Away_SH_15_Over", teamSide),
                        Away_Win_Any_Half = GenerateGeneralComparison(comparisonContainer, "Away_Win_Any_Half", teamSide),
                        Home_FT_05_Over = GenerateGeneralComparison(comparisonContainer, "Home_FT_05_Over", teamSide),
                        Home_FT_15_Over = GenerateGeneralComparison(comparisonContainer, "Home_FT_15_Over", teamSide),
                        Home_HT_05_Over = GenerateGeneralComparison(comparisonContainer, "Home_HT_05_Over", teamSide),
                        Home_HT_15_Over = GenerateGeneralComparison(comparisonContainer, "Home_HT_15_Over", teamSide),
                        Home_SH_05_Over = GenerateGeneralComparison(comparisonContainer, "Home_SH_05_Over", teamSide),
                        Home_SH_15_Over = GenerateGeneralComparison(comparisonContainer, "Home_SH_15_Over", teamSide),
                        Home_Win_Any_Half = GenerateGeneralComparison(comparisonContainer, "Home_Win_Any_Half", teamSide),
                        Is_FT_Win1 = GenerateGeneralComparison(comparisonContainer, "Is_FT_Win1", teamSide),
                        Is_FT_X = GenerateGeneralComparison(comparisonContainer, "Is_FT_X", teamSide),
                        Is_FT_Win2 = GenerateGeneralComparison(comparisonContainer, "Is_FT_Win2", teamSide),
                        Is_HT_Win1 = GenerateGeneralComparison(comparisonContainer, "Is_HT_Win1", teamSide),
                        Is_HT_X = GenerateGeneralComparison(comparisonContainer, "Is_HT_X", teamSide),
                        Is_HT_Win2 = GenerateGeneralComparison(comparisonContainer, "Is_HT_Win2", teamSide),
                        Is_SH_Win1 = GenerateGeneralComparison(comparisonContainer, "Is_SH_Win1", teamSide),
                        Is_SH_X = GenerateGeneralComparison(comparisonContainer, "Is_SH_X", teamSide),
                        Is_SH_Win2 = GenerateGeneralComparison(comparisonContainer, "Is_SH_Win2", teamSide),
                        FT_15_Over = GenerateGeneralComparison(comparisonContainer, "FT_15_Over", teamSide),
                        FT_25_Over = GenerateGeneralComparison(comparisonContainer, "FT_25_Over", teamSide),
                        FT_35_Over = GenerateGeneralComparison(comparisonContainer, "FT_35_Over", teamSide),
                        FT_GG = GenerateGeneralComparison(comparisonContainer, "FT_GG", teamSide),
                        HT_GG = GenerateGeneralComparison(comparisonContainer, "HT_GG", teamSide), // *
                        SH_GG = GenerateGeneralComparison(comparisonContainer, "SH_GG", teamSide),
                        HT_05_Over = GenerateGeneralComparison(comparisonContainer, "HT_05_Over", teamSide),
                        HT_15_Over = GenerateGeneralComparison(comparisonContainer, "HT_15_Over", teamSide),
                        SH_05_Over = GenerateGeneralComparison(comparisonContainer, "SH_05_Over", teamSide),
                        SH_15_Over = GenerateGeneralComparison(comparisonContainer, "SH_15_Over", teamSide),
                        MoreGoalsBetweenTimes = GenerateGeneralComparison(comparisonContainer, "MoreGoalsBetweenTimes", teamSide),
                        Total_BetweenGoals = GenerateGeneralComparison(comparisonContainer, "Total_BetweenGoals", teamSide),
                        FT_Result = GenerateGeneralComparison(comparisonContainer, "FT_Result", teamSide),
                        HT_Result = GenerateGeneralComparison(comparisonContainer, "HT_Result", teamSide),
                        HT_FT_Result = GenerateGeneralComparison(comparisonContainer, "HT_FT_Result", teamSide),
                        SH_Result = GenerateGeneralComparison(comparisonContainer, "SH_Result", teamSide)
                    },

                    Serial = serial
                } : null;

                return result;

            }
            catch (Exception ex)
            {
                return null;
            }

        }



        public static ComparisonGuessContainer GetComparisonOnlyDbProfilerResult(string serial, IMatchBetService matchBetService, IFilterResultService filterResultService, CountryContainerTemp countryContainerTemp, TeamSide teamSide)
        {
            var comparisonContainer = _proceeder.SelectListComparisonInfoFromDbBetweenTeams(serial, matchBetService, filterResultService, countryContainerTemp);

            try
            {
                var result = comparisonContainer != null && comparisonContainer.Any() && comparisonContainer.Count >= 4 ? new ComparisonGuessContainer
                {
                    Away = comparisonContainer[0].UnchangableAwayTeam,
                    Home = comparisonContainer[0].UnchangableHomeTeam,
                    CountryName = GenerateBySideComparison(comparisonContainer, "CountryName", teamSide).FeatureName,
                    HomeAway = new GuessModel
                    {
                        CountFound = GetBeSideCountFound(comparisonContainer, teamSide),
                        Average_FT_Goals_AwayTeam = GenerateHomeAwayGoalsAverage(comparisonContainer, "FT_Goals_AwayTeam", teamSide),
                        Average_FT_Goals_HomeTeam = GenerateHomeAwayGoalsAverage(comparisonContainer, "FT_Goals_HomeTeam", teamSide),
                        Average_HT_Goals_AwayTeam = GenerateHomeAwayGoalsAverage(comparisonContainer, "HT_Goals_AwayTeam", teamSide),
                        Average_HT_Goals_HomeTeam = GenerateHomeAwayGoalsAverage(comparisonContainer, "HT_Goals_HomeTeam", teamSide),
                        Average_SH_Goals_AwayTeam = GenerateHomeAwayGoalsAverage(comparisonContainer, "SH_Goals_AwayTeam", teamSide),
                        Average_SH_Goals_HomeTeam = GenerateHomeAwayGoalsAverage(comparisonContainer, "SH_Goals_HomeTeam", teamSide),
                        Average_FT_Conceded_Goals_AwayTeam = GenerateHomeAwayGoalsAverage(comparisonContainer, "FT_Conceded_Goals_AwayTeam", teamSide),
                        Average_FT_Conceded_Goals_HomeTeam = GenerateHomeAwayGoalsAverage(comparisonContainer, "FT_Conceded_Goals_HomeTeam", teamSide),
                        Average_HT_Conceded_Goals_AwayTeam = GenerateHomeAwayGoalsAverage(comparisonContainer, "HT_Conceded_Goals_AwayTeam", teamSide),
                        Average_HT_Conceded_Goals_HomeTeam = GenerateHomeAwayGoalsAverage(comparisonContainer, "HT_Conceded_Goals_HomeTeam", teamSide),
                        Average_SH_Conceded_Goals_AwayTeam = GenerateHomeAwayGoalsAverage(comparisonContainer, "SH_Conceded_Goals_AwayTeam", teamSide),
                        Average_SH_Conceded_Goals_HomeTeam = GenerateHomeAwayGoalsAverage(comparisonContainer, "SH_Conceded_Goals_HomeTeam", teamSide),
                        Average_FT_Corners_AwayTeam = GenerateHomeAwayCornersPossesionShutAverage(comparisonContainer, "AwayCornersCount", teamSide, x => x.HasCorner == true),
                        Average_FT_Corners_HomeTeam = GenerateHomeAwayCornersPossesionShutAverage(comparisonContainer, "HomeCornersCount", teamSide, x => x.HasCorner == true),


                        Average_FT_Possesion_HomeTeam = GenerateHomeAwayCornersPossesionShutAverage(comparisonContainer, "HomePossesionCount", teamSide, x => x.HasPossesion == true),
                        Average_FT_Possesion_AwayTeam = GenerateHomeAwayCornersPossesionShutAverage(comparisonContainer, "AwayPossesionCount", teamSide, x => x.HasPossesion == true),
                        Average_FT_Shot_HomeTeam = GenerateHomeAwayCornersPossesionShutAverage(comparisonContainer, "HomeShutCount", teamSide, x => x.HasShut == true),
                        Average_FT_Shot_AwayTeam = GenerateHomeAwayCornersPossesionShutAverage(comparisonContainer, "AwayShutCount", teamSide, x => x.HasShut == true),
                        Average_FT_ShotOnTarget_HomeTeam = GenerateHomeAwayCornersPossesionShutAverage(comparisonContainer, "HomeShutOnTargetCount", teamSide, x => x.HasShutOnTarget == true),
                        Average_FT_ShotOnTarget_AwayTeam = GenerateHomeAwayCornersPossesionShutAverage(comparisonContainer, "AwayShutOnTargetCount", teamSide, x => x.HasShutOnTarget == true),

                        Average_FT_GK_Saves_AwayTeam = GenerateHomeAwayCornersPossesionShutAverage(comparisonContainer, "FT_GK_Saves_AwayTeam", teamSide, x => x.HasGK_Saves == true),
                        Average_FT_GK_Saves_HomeTeam = GenerateHomeAwayCornersPossesionShutAverage(comparisonContainer, "FT_GK_Saves_HomeTeam", teamSide, x => x.HasGK_Saves == true),


                        Corner_Away_3_5_Over = GenerateBySideCornerPossesionShutComparison(comparisonContainer, "Corner_Away_3_5_Over", teamSide, x => x.HasCorner == true),
                        Corner_Away_4_5_Over = GenerateBySideCornerPossesionShutComparison(comparisonContainer, "Corner_Away_4_5_Over", teamSide, x => x.HasCorner == true),
                        Corner_Away_5_5_Over = GenerateBySideCornerPossesionShutComparison(comparisonContainer, "Corner_Away_5_5_Over", teamSide, x => x.HasCorner == true),

                        Corner_Home_3_5_Over = GenerateBySideCornerPossesionShutComparison(comparisonContainer, "Corner_Home_3_5_Over", teamSide, x => x.HasCorner == true),
                        Corner_Home_4_5_Over = GenerateBySideCornerPossesionShutComparison(comparisonContainer, "Corner_Home_4_5_Over", teamSide, x => x.HasCorner == true),
                        Corner_Home_5_5_Over = GenerateBySideCornerPossesionShutComparison(comparisonContainer, "Corner_Home_5_5_Over", teamSide, x => x.HasCorner == true),

                        Corner_7_5_Over = GenerateBySideCornerPossesionShutComparison(comparisonContainer, "Corner_7_5_Over", teamSide, x => x.HasCorner == true),
                        Corner_8_5_Over = GenerateBySideCornerPossesionShutComparison(comparisonContainer, "Corner_8_5_Over", teamSide, x => x.HasCorner == true),
                        Corner_9_5_Over = GenerateBySideCornerPossesionShutComparison(comparisonContainer, "Corner_9_5_Over", teamSide, x => x.HasCorner == true),

                        Away_FT_05_Over = GenerateBySideComparison(comparisonContainer, "Away_FT_05_Over", teamSide),
                        Away_FT_15_Over = GenerateBySideComparison(comparisonContainer, "Away_FT_15_Over", teamSide),
                        Away_HT_05_Over = GenerateBySideComparison(comparisonContainer, "Away_HT_05_Over", teamSide),
                        Away_HT_15_Over = GenerateBySideComparison(comparisonContainer, "Away_HT_15_Over", teamSide),
                        Away_SH_05_Over = GenerateBySideComparison(comparisonContainer, "Away_SH_05_Over", teamSide),
                        Away_SH_15_Over = GenerateBySideComparison(comparisonContainer, "Away_SH_15_Over", teamSide),
                        Away_Win_Any_Half = GenerateBySideComparison(comparisonContainer, "Away_Win_Any_Half", teamSide),
                        Home_FT_05_Over = GenerateBySideComparison(comparisonContainer, "Home_FT_05_Over", teamSide),
                        Home_FT_15_Over = GenerateBySideComparison(comparisonContainer, "Home_FT_15_Over", teamSide),
                        Home_HT_05_Over = GenerateBySideComparison(comparisonContainer, "Home_HT_05_Over", teamSide),
                        Home_HT_15_Over = GenerateBySideComparison(comparisonContainer, "Home_HT_15_Over", teamSide),
                        Home_SH_05_Over = GenerateBySideComparison(comparisonContainer, "Home_SH_05_Over", teamSide),
                        Home_SH_15_Over = GenerateBySideComparison(comparisonContainer, "Home_SH_15_Over", teamSide),
                        Home_Win_Any_Half = GenerateBySideComparison(comparisonContainer, "Home_Win_Any_Half", teamSide),
                        FT_15_Over = GenerateBySideComparison(comparisonContainer, "FT_15_Over", teamSide),
                        FT_25_Over = GenerateBySideComparison(comparisonContainer, "FT_25_Over", teamSide),
                        FT_35_Over = GenerateBySideComparison(comparisonContainer, "FT_35_Over", teamSide),
                        FT_GG = GenerateBySideComparison(comparisonContainer, "FT_GG", teamSide),
                        HT_GG = GenerateBySideComparison(comparisonContainer, "HT_GG", teamSide),
                        SH_GG = GenerateBySideComparison(comparisonContainer, "SH_GG", teamSide),
                        HT_05_Over = GenerateBySideComparison(comparisonContainer, "HT_05_Over", teamSide),
                        HT_15_Over = GenerateBySideComparison(comparisonContainer, "HT_15_Over", teamSide),
                        SH_05_Over = GenerateBySideComparison(comparisonContainer, "SH_05_Over", teamSide),
                        SH_15_Over = GenerateBySideComparison(comparisonContainer, "SH_15_Over", teamSide),
                        MoreGoalsBetweenTimes = GenerateBySideComparison(comparisonContainer, "MoreGoalsBetweenTimes", teamSide),
                        Total_BetweenGoals = GenerateBySideComparison(comparisonContainer, "Total_BetweenGoals", teamSide),
                        FT_Result = GenerateBySideComparison(comparisonContainer, "FT_Result", teamSide),
                        HT_Result = GenerateBySideComparison(comparisonContainer, "HT_Result", teamSide),
                        HT_FT_Result = GenerateBySideComparison(comparisonContainer, "HT_FT_Result", teamSide),
                        SH_Result = GenerateBySideComparison(comparisonContainer, "SH_Result", teamSide),
                        Is_FT_Win1 = GenerateBySideComparison(comparisonContainer, "Is_FT_Win1", teamSide),
                        Is_FT_X = GenerateBySideComparison(comparisonContainer, "Is_FT_X", teamSide),
                        Is_FT_Win2 = GenerateBySideComparison(comparisonContainer, "Is_FT_Win2", teamSide),
                        Is_HT_Win1 = GenerateBySideComparison(comparisonContainer, "Is_HT_Win1", teamSide),
                        Is_HT_X = GenerateBySideComparison(comparisonContainer, "Is_HT_X", teamSide),
                        Is_HT_Win2 = GenerateBySideComparison(comparisonContainer, "Is_HT_Win2", teamSide),
                        Is_SH_Win1 = GenerateBySideComparison(comparisonContainer, "Is_SH_Win1", teamSide),
                        Is_SH_X = GenerateBySideComparison(comparisonContainer, "Is_SH_X", teamSide),
                        Is_SH_Win2 = GenerateBySideComparison(comparisonContainer, "Is_SH_Win2", teamSide),
                        Is_Corner_FT_Win1 = GenerateBySideCornerPossesionShutComparison(comparisonContainer, "Is_Corner_FT_Win1", teamSide, x => x.HasCorner == true),
                        Is_Corner_FT_X = GenerateBySideCornerPossesionShutComparison(comparisonContainer, "Is_Corner_FT_X", teamSide, x => x.HasCorner == true),
                        Is_Corner_FT_Win2 = GenerateBySideCornerPossesionShutComparison(comparisonContainer, "Is_Corner_FT_Win2", teamSide, x => x.HasCorner == true)
                    },
                    General = new GuessModel
                    {
                        CountFound = GetGeneralCountFound(comparisonContainer, teamSide),
                        Average_FT_Goals_AwayTeam = GenerateGeneralGoalsAverage(comparisonContainer, "FT_Goals_AwayTeam", teamSide),
                        Average_FT_Goals_HomeTeam = GenerateGeneralGoalsAverage(comparisonContainer, "FT_Goals_HomeTeam", teamSide),
                        Average_HT_Goals_AwayTeam = GenerateGeneralGoalsAverage(comparisonContainer, "HT_Goals_AwayTeam", teamSide),
                        Average_HT_Goals_HomeTeam = GenerateGeneralGoalsAverage(comparisonContainer, "HT_Goals_HomeTeam", teamSide),
                        Average_SH_Goals_AwayTeam = GenerateGeneralGoalsAverage(comparisonContainer, "SH_Goals_AwayTeam", teamSide),
                        Average_SH_Goals_HomeTeam = GenerateGeneralGoalsAverage(comparisonContainer, "SH_Goals_HomeTeam", teamSide),
                        Average_FT_Conceded_Goals_AwayTeam = GenerateGeneralGoalsAverage(comparisonContainer, "FT_Conceded_Goals_AwayTeam", teamSide),
                        Average_FT_Conceded_Goals_HomeTeam = GenerateGeneralGoalsAverage(comparisonContainer, "FT_Conceded_Goals_HomeTeam", teamSide),
                        Average_HT_Conceded_Goals_AwayTeam = GenerateGeneralGoalsAverage(comparisonContainer, "HT_Conceded_Goals_AwayTeam", teamSide),
                        Average_HT_Conceded_Goals_HomeTeam = GenerateGeneralGoalsAverage(comparisonContainer, "HT_Conceded_Goals_HomeTeam", teamSide),
                        Average_SH_Conceded_Goals_AwayTeam = GenerateGeneralGoalsAverage(comparisonContainer, "SH_Conceded_Goals_AwayTeam", teamSide),
                        Average_SH_Conceded_Goals_HomeTeam = GenerateGeneralGoalsAverage(comparisonContainer, "SH_Conceded_Goals_HomeTeam", teamSide),
                        Average_FT_Corners_AwayTeam = GenerateGeneralCornersPossesionShutAverage(comparisonContainer, "AwayCornersCount", teamSide, x => x.HasCorner == true),
                        Average_FT_Corners_HomeTeam = GenerateGeneralCornersPossesionShutAverage(comparisonContainer, "HomeCornersCount", teamSide, x => x.HasCorner == true),

                        Average_FT_GK_Saves_AwayTeam = GenerateGeneralCornersPossesionShutAverage(comparisonContainer, "FT_GK_Saves_AwayTeam", teamSide, x => x.HasGK_Saves),
                        Average_FT_GK_Saves_HomeTeam = GenerateGeneralCornersPossesionShutAverage(comparisonContainer, "FT_GK_Saves_HomeTeam", teamSide, x => x.HasGK_Saves),

                        Average_FT_Possesion_HomeTeam = GenerateGeneralCornersPossesionShutAverage(comparisonContainer, "HomePossesionCount", teamSide, x => x.HasPossesion),
                        Average_FT_Possesion_AwayTeam = GenerateGeneralCornersPossesionShutAverage(comparisonContainer, "AwayPossesionCount", teamSide, x => x.HasPossesion),
                        Average_FT_Shot_HomeTeam = GenerateGeneralCornersPossesionShutAverage(comparisonContainer, "HomeShutCount", teamSide, x => x.HasShut),
                        Average_FT_Shot_AwayTeam = GenerateGeneralCornersPossesionShutAverage(comparisonContainer, "AwayShutCount", teamSide, x => x.HasShut),
                        Average_FT_ShotOnTarget_HomeTeam = GenerateGeneralCornersPossesionShutAverage(comparisonContainer, "HomeShutOnTargetCount", teamSide, x => x.HasShutOnTarget),
                        Average_FT_ShotOnTarget_AwayTeam = GenerateGeneralCornersPossesionShutAverage(comparisonContainer, "AwayShutOnTargetCount", teamSide, x => x.HasShutOnTarget),



                        Corner_Away_3_5_Over = GenerateGeneralCornerPossesionShutComparison(comparisonContainer, "Corner_Away_3_5_Over", teamSide, x => x.HasCorner),
                        Corner_Away_4_5_Over = GenerateGeneralCornerPossesionShutComparison(comparisonContainer, "Corner_Away_4_5_Over", teamSide, x => x.HasCorner),
                        Corner_Away_5_5_Over = GenerateGeneralCornerPossesionShutComparison(comparisonContainer, "Corner_Away_5_5_Over", teamSide, x => x.HasCorner),

                        Corner_Home_3_5_Over = GenerateGeneralCornerPossesionShutComparison(comparisonContainer, "Corner_Home_3_5_Over", teamSide, x => x.HasCorner),
                        Corner_Home_4_5_Over = GenerateGeneralCornerPossesionShutComparison(comparisonContainer, "Corner_Home_4_5_Over", teamSide, x => x.HasCorner),
                        Corner_Home_5_5_Over = GenerateGeneralCornerPossesionShutComparison(comparisonContainer, "Corner_Home_5_5_Over", teamSide, x => x.HasCorner),

                        Corner_7_5_Over = GenerateGeneralCornerPossesionShutComparison(comparisonContainer, "Corner_7_5_Over", teamSide, x => x.HasCorner),
                        Corner_8_5_Over = GenerateGeneralCornerPossesionShutComparison(comparisonContainer, "Corner_8_5_Over", teamSide, x => x.HasCorner),
                        Corner_9_5_Over = GenerateGeneralCornerPossesionShutComparison(comparisonContainer, "Corner_9_5_Over", teamSide, x => x.HasCorner),

                        Away_FT_05_Over = GenerateGeneralComparison(comparisonContainer, "Away_FT_05_Over", teamSide),
                        Away_FT_15_Over = GenerateGeneralComparison(comparisonContainer, "Away_FT_15_Over", teamSide),
                        Away_HT_05_Over = GenerateGeneralComparison(comparisonContainer, "Away_HT_05_Over", teamSide),
                        Away_HT_15_Over = GenerateGeneralComparison(comparisonContainer, "Away_HT_15_Over", teamSide),
                        Away_SH_05_Over = GenerateGeneralComparison(comparisonContainer, "Away_SH_05_Over", teamSide),
                        Away_SH_15_Over = GenerateGeneralComparison(comparisonContainer, "Away_SH_15_Over", teamSide),
                        Away_Win_Any_Half = GenerateGeneralComparison(comparisonContainer, "Away_Win_Any_Half", teamSide),
                        Home_FT_05_Over = GenerateGeneralComparison(comparisonContainer, "Home_FT_05_Over", teamSide),
                        Home_FT_15_Over = GenerateGeneralComparison(comparisonContainer, "Home_FT_15_Over", teamSide),
                        Home_HT_05_Over = GenerateGeneralComparison(comparisonContainer, "Home_HT_05_Over", teamSide),
                        Home_HT_15_Over = GenerateGeneralComparison(comparisonContainer, "Home_HT_15_Over", teamSide),
                        Home_SH_05_Over = GenerateGeneralComparison(comparisonContainer, "Home_SH_05_Over", teamSide),
                        Home_SH_15_Over = GenerateGeneralComparison(comparisonContainer, "Home_SH_15_Over", teamSide),
                        Home_Win_Any_Half = GenerateGeneralComparison(comparisonContainer, "Home_Win_Any_Half", teamSide),
                        FT_15_Over = GenerateGeneralComparison(comparisonContainer, "FT_15_Over", teamSide),
                        FT_25_Over = GenerateGeneralComparison(comparisonContainer, "FT_25_Over", teamSide),
                        FT_35_Over = GenerateGeneralComparison(comparisonContainer, "FT_35_Over", teamSide),
                        FT_GG = GenerateGeneralComparison(comparisonContainer, "FT_GG", teamSide),
                        HT_GG = GenerateGeneralComparison(comparisonContainer, "HT_GG", teamSide),
                        SH_GG = GenerateGeneralComparison(comparisonContainer, "SH_GG", teamSide),
                        HT_05_Over = GenerateGeneralComparison(comparisonContainer, "HT_05_Over", teamSide),
                        HT_15_Over = GenerateGeneralComparison(comparisonContainer, "HT_15_Over", teamSide),
                        SH_05_Over = GenerateGeneralComparison(comparisonContainer, "SH_05_Over", teamSide),
                        SH_15_Over = GenerateGeneralComparison(comparisonContainer, "SH_15_Over", teamSide),
                        MoreGoalsBetweenTimes = GenerateGeneralComparison(comparisonContainer, "MoreGoalsBetweenTimes", teamSide),
                        Total_BetweenGoals = GenerateGeneralComparison(comparisonContainer, "Total_BetweenGoals", teamSide),
                        FT_Result = GenerateGeneralComparison(comparisonContainer, "FT_Result", teamSide),
                        HT_Result = GenerateGeneralComparison(comparisonContainer, "HT_Result", teamSide),
                        HT_FT_Result = GenerateGeneralComparison(comparisonContainer, "HT_FT_Result", teamSide),
                        SH_Result = GenerateGeneralComparison(comparisonContainer, "SH_Result", teamSide),
                        Is_FT_Win1 = GenerateGeneralComparison(comparisonContainer, "Is_FT_Win1", teamSide),
                        Is_FT_X = GenerateGeneralComparison(comparisonContainer, "Is_FT_X", teamSide),
                        Is_FT_Win2 = GenerateGeneralComparison(comparisonContainer, "Is_FT_Win2", teamSide),
                        Is_HT_Win1 = GenerateGeneralComparison(comparisonContainer, "Is_HT_Win1", teamSide),
                        Is_HT_X = GenerateGeneralComparison(comparisonContainer, "Is_HT_X", teamSide),
                        Is_HT_Win2 = GenerateGeneralComparison(comparisonContainer, "Is_HT_Win2", teamSide),
                        Is_SH_Win1 = GenerateGeneralComparison(comparisonContainer, "Is_SH_Win1", teamSide),
                        Is_SH_X = GenerateGeneralComparison(comparisonContainer, "Is_SH_X", teamSide),
                        Is_SH_Win2 = GenerateGeneralComparison(comparisonContainer, "Is_SH_Win2", teamSide),
                        Is_Corner_FT_Win1 = GenerateGeneralCornerPossesionShutComparison(comparisonContainer, "Is_Corner_FT_Win1", teamSide, x => x.HasCorner),
                        Is_Corner_FT_X = GenerateGeneralCornerPossesionShutComparison(comparisonContainer, "Is_Corner_FT_X", teamSide, x => x.HasCorner),
                        Is_Corner_FT_Win2 = GenerateGeneralCornerPossesionShutComparison(comparisonContainer, "Is_Corner_FT_Win2", teamSide, x => x.HasCorner)
                    },

                    Serial = serial
                } : null;

                return result;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static FilterResult GenerateFilterResultCornersAndFT(FilterResult fResult)
        {
            var webOperation = new WebOperation();

            fResult.Is_FT_Win1 = fResult.FT_Result == 1;
            fResult.Is_FT_Win2 = fResult.FT_Result == 2;
            fResult.Is_FT_X = fResult.FT_Result == 9;

            fResult.Is_HT_Win1 = fResult.HT_Result == 1;
            fResult.Is_HT_Win2 = fResult.HT_Result == 2;
            fResult.Is_HT_X = fResult.HT_Result == 9;

            fResult.Is_SH_Win1 = fResult.SH_Result == 1;
            fResult.Is_SH_Win2 = fResult.SH_Result == 2;
            fResult.Is_SH_X = fResult.SH_Result == 9;

            var src = webOperation.GetMinifiedString($"http://arsiv.mackolik.com/Match/Default.aspx?id={fResult.SerialUniqueID}#mac-bilgisi");

            var cornerResult = ExtractFromStatistics(src, "Korner", "Köşe Vuruşu");
            var possesionResult = ExtractFromStatistics(src, "Topla Oynama");
            var shotResult = ExtractFromStatistics(src, "Toplam Şut");
            var shotOnTargetResult = ExtractFromStatistics(src, "İsabetli Şut");

            if (cornerResult[0] >= 0 && cornerResult[1] >= 0)
            {
                fResult.HomeCornerCount = cornerResult[0];
                fResult.AwayCornerCount = cornerResult[1];
                fResult.Corner_Home_3_5_Over = cornerResult[0] > 3;
                fResult.Corner_Home_4_5_Over = cornerResult[0] > 4;
                fResult.Corner_Home_5_5_Over = cornerResult[0] > 5;
                fResult.Corner_Away_3_5_Over = cornerResult[1] > 3;
                fResult.Corner_Away_4_5_Over = cornerResult[1] > 4;
                fResult.Corner_Away_5_5_Over = cornerResult[1] > 5;
                fResult.Is_Corner_FT_Win1 = cornerResult[0] > cornerResult[1];
                fResult.Is_Corner_FT_X = cornerResult[0] == cornerResult[1];
                fResult.Is_Corner_FT_Win2 = cornerResult[0] < cornerResult[1];
                fResult.Corner_7_5_Over = (cornerResult[0] + cornerResult[1]) > 7;
                fResult.Corner_8_5_Over = (cornerResult[0] + cornerResult[1]) > 8;
                fResult.Corner_9_5_Over = (cornerResult[0] + cornerResult[1]) > 9;
                fResult.IsCornerFound = true;
            }

            if (possesionResult[0] >= 0 && possesionResult[1] >= 0)
            {
                fResult.HomePossesion = possesionResult[0];
                fResult.AwayPossesion = possesionResult[1];
                fResult.IsPossesionFound = true;
            }

            if (shotResult[0] >= 0 && shotResult[1] >= 0)
            {
                fResult.HomeShotCount = shotResult[0];
                fResult.AwayShotCount = shotResult[1];
                fResult.IsShotFound = true;
            }

            if (shotOnTargetResult[0] >= 0 && shotOnTargetResult[1] >= 0)
            {
                fResult.HomeShotOnTargetCount = shotOnTargetResult[0];
                fResult.AwayShotOnTargetCount = shotOnTargetResult[1];
                fResult.IsShotOnTargetFound = true;
            }

            return fResult;
        }

        public static int[] ExtractFromStatistics(string src, params string[] statisticNames)
        {
            int[] result = new int[2];
            result[0] = -1;
            result[1] = -1;

            for (int i = 0; i < statisticNames.Length; i++)
            {
                var statisticName = statisticNames[i];

                var regexStat = new Regex(">" + statisticName + "<[\\s\\S]*?class=team-2-statistics-text[\\s\\S]*?>[\\s\\S]*?(.+?(?=<))");

                var firstSrcPart = src.Split($">{statisticName}<")[0];

                var belongTeam1 = "";
                var belongTeam2 = "";

                try
                {
                    belongTeam1 = firstSrcPart.Substring(firstSrcPart.Length - 40, 1).Trim().Trim('%');
                    if (belongTeam1.Trim() == ">")
                    {
                        belongTeam1 = firstSrcPart.Substring(firstSrcPart.Length - 39, 1).Trim().Trim('%');
                    }
                    else
                    {
                        belongTeam1 = firstSrcPart.Substring(firstSrcPart.Length - 40, 2).Trim().Trim('%');
                    }
                    belongTeam2 = regexStat.Matches(src)[0].Groups[1].Value.Trim().Trim('%');

                    result[0] = Convert.ToInt32(belongTeam1);
                    result[1] = Convert.ToInt32(belongTeam2);

                    break;
                }
                catch (Exception ex)
                {
                    result[0] = -1;
                    result[1] = -1;
                    continue;
                }
            }

            return result;
        }


        private static PercentageComplainer GenerateGeneralComparison<T>(List<T> containers, string propertyName, TeamSide teamSide)
    where T : BaseComparerContainerModel, new()
        {
            if (containers is null || containers.Count == 0)
            {
                return null;
            }

            var mixedData = _quickConvertService.MixRange(containers, teamSide);

            var sideContainers = (teamSide == TeamSide.Away)
                ? mixedData.Where(c => c.AwayTeam == c.UnchangableAwayTeam)
                : mixedData.Where(c => c.HomeTeam == c.UnchangableHomeTeam);

            if (!sideContainers.Any())
            {
                return null;
            }

            var groupedContainers = sideContainers
                .GroupBy(c => c.GetType().GetProperty(propertyName).GetValue(c, null))
                .Select(g => new PercentageComplainer
                {
                    Percentage = g.Count() * 100 / sideContainers.Count(),
                    CountFound = g.Count(),
                    CountAll = sideContainers.Count(),
                    FeatureName = g.Key.ToString(),
                    PropertyName = propertyName
                });

            return groupedContainers.OrderByDescending(gc => gc.Percentage).First();
        }


        private static PercentageComplainer GenerateGeneralCornerPossesionShutComparison<T>(List<T> containers, string propertyName, TeamSide teamSide, Func<T, bool> expression)
where T : BaseComparerContainerModel, new()
        {
            if (containers == null || !containers.Any())
            {
                return null;
            }

            var filteredListContainer = containers.Where(expression).ToList();

            if (filteredListContainer.Count == 0)
            {
                return new PercentageComplainer
                {
                    CountAll = -9999,
                    CountFound = -9999,
                    FeatureName = "NONE",
                    Percentage = -9999,
                    PropertyName = propertyName
                };
            }

            var mixedData = _quickConvertService.MixRange(filteredListContainer, teamSide);

            var sideContainers = (teamSide == TeamSide.Away)
                ? mixedData.Where(c => c.AwayTeam == c.UnchangableAwayTeam)
                : mixedData.Where(c => c.HomeTeam == c.UnchangableHomeTeam);

            if (!sideContainers.Any())
            {
                return null;
            }

            var groupedContainers = sideContainers
                .GroupBy(c => c.GetType().GetProperty(propertyName)?.GetValue(c))
                .Select(g => new PercentageComplainer
                {
                    Percentage = g.Count() * 100 / sideContainers.Count(),
                    CountFound = g.Count(),
                    CountAll = sideContainers.Count(),
                    FeatureName = g.Key?.ToString() ?? "UNKNOWN",
                    PropertyName = propertyName
                });

            return groupedContainers.OrderByDescending(gc => gc.Percentage).FirstOrDefault();
        }


        private static PercentageComplainer GenerateBySideComparison<T>(List<T> containers, string propertyName, TeamSide teamSide)
where T : BaseComparerContainerModel, new()
        {
            if (propertyName.ToLower().Contains("is_sh_"))
            {

            }

            if (containers is null || containers.Count == 0)
            {
                return null;
            }

            IEnumerable<T> sideContainers = (teamSide == TeamSide.Away)
                ? containers.Where(c => c.AwayTeam == c.UnchangableAwayTeam)
                : containers.Where(c => c.HomeTeam == c.UnchangableHomeTeam);

            if (!sideContainers.Any())
            {
                return null;
            }

            var groupedContainers = sideContainers
                .GroupBy(c => c.GetType().GetProperty(propertyName).GetValue(c, null))
                .Select(g => new PercentageComplainer
                {
                    Percentage = g.Count() * 100 / sideContainers.Count(),
                    CountFound = g.Count(),
                    CountAll = sideContainers.Count(),
                    FeatureName = g.Key.ToString(),
                    PropertyName = propertyName
                });

            return groupedContainers.OrderByDescending(gc => gc.Percentage).First();
        }


        private static PercentageComplainer GenerateBySideCornerPossesionShutComparison<T>(List<T> containers, string propertyName, TeamSide teamSide, Func<T, bool> filter)
    where T : BaseComparerContainerModel, new()
        {
            if (containers is null || containers.Count == 0)
            {
                return null;
            }

            var filteredList = containers.Where(filter).ToList();

            if (filteredList.Count == 0)
            {
                return new PercentageComplainer
                {
                    CountAll = -9999,
                    CountFound = -9999,
                    FeatureName = "NONE",
                    Percentage = -9999,
                    PropertyName = propertyName
                };
            }

            IEnumerable<T> sideContainers = (teamSide == TeamSide.Away)
                ? filteredList.Where(c => c.AwayTeam == c.UnchangableAwayTeam)
                : filteredList.Where(c => c.HomeTeam == c.UnchangableHomeTeam);

            if (!sideContainers.Any())
            {
                return null;
            }

            var groupedContainers = sideContainers
                .GroupBy(c => c.GetType().GetProperty(propertyName).GetValue(c, null))
                .Select(g => new PercentageComplainer
                {
                    Percentage = g.Count() * 100 / sideContainers.Count(),
                    CountFound = g.Count(),
                    CountAll = sideContainers.Count(),
                    FeatureName = g.Key.ToString(),
                    PropertyName = propertyName
                });

            return groupedContainers.OrderByDescending(gc => gc.Percentage).First();
        }


        private static decimal GenerateGeneralGoalsAverage<T>(List<T> listContainers, string propertyName, TeamSide teamSide)
    where T : BaseComparerContainerModel, new()
        {
            if (listContainers is null || listContainers.Count == 0)
            {
                return -1.00M;
            }

            var goalsProperty = typeof(T).GetProperty(propertyName);

            if (goalsProperty is null)
            {
                throw new ArgumentException($"The property '{propertyName}' does not exist on type '{typeof(T).Name}'.");
            }

            var mixedData = _quickConvertService.MixRange(listContainers, teamSide);

            var totalGoals = mixedData.Select(x => (int)goalsProperty.GetValue(x)).Sum();
            var averageGoals = totalGoals / (decimal)listContainers.Count;

            return averageGoals;
        }

        private static decimal GenerateGeneralCornersPossesionShutAverage<T>(List<T> listContainers, string propertyName, TeamSide teamSide, Func<T, bool> expression)
    where T : BaseComparerContainerModel, new()
        {
            if (listContainers is null || listContainers.Count == 0)
            {
                return -1.00M;
            }

            var cornersProperty = typeof(T).GetProperty(propertyName);

            if (cornersProperty is null)
            {
                throw new ArgumentException($"The property '{propertyName}' does not exist on type '{typeof(T).Name}'.");
            }

            var filteredListContainer = listContainers.Where(expression).ToList();

            if (filteredListContainer.Count == 0) return -9999;

            var mixedData = _quickConvertService.MixRange(filteredListContainer, teamSide);

            var totalCorners = mixedData.Where(expression).Select(x => (int)cornersProperty.GetValue(x)).Sum();
            var averageCorners = totalCorners / (decimal)filteredListContainer.Count;

            return averageCorners;
        }

        private static decimal GenerateHomeAwayGoalsAverage<T>(List<T> listContainers, string propertyName, TeamSide teamSide)
            where T : BaseComparerContainerModel, new()
        {
            decimal result = -1;
            if (listContainers == null || listContainers.Count == 0) return result;

            if (teamSide == TeamSide.Away)
            {
                var awaySideListContainers = listContainers.Where(x => x.AwayTeam == x.UnchangableAwayTeam);

                if (awaySideListContainers.Any())
                {
                    result = (decimal)awaySideListContainers
                        .Select(x => (int)x.GetType().GetProperty(propertyName).GetValue(x, null))
                        .Sum() / awaySideListContainers.Count();
                }
            }
            else
            {
                var homeSideListContainers = listContainers.Where(x => x.HomeTeam == x.UnchangableHomeTeam);

                if (homeSideListContainers.Any())
                {
                    result = (decimal)homeSideListContainers
                        .Select(x => (int)x.GetType().GetProperty(propertyName).GetValue(x, null))
                        .Sum() / homeSideListContainers.Count();
                }
            }

            return result;
        }

        private static decimal GenerateHomeAwayCornersPossesionShutAverage<T>(List<T> listContainers, string propertyName, TeamSide teamSide, Func<T, bool> filter)
    where T : BaseComparerContainerModel
        {
            var filteredList = listContainers.Where(filter).ToList();
            if (filteredList.Count == 0)
            {
                return -999M;
            }

            var cornersProperty = typeof(T).GetProperty(propertyName);

            if (cornersProperty is null)
            {
                throw new ArgumentException($"The property '{propertyName}' does not exist on type '{typeof(T).Name}'.");
            }

            var filteredContainers = (teamSide == TeamSide.Away)
                ? filteredList.Where(c => c.AwayTeam == c.UnchangableAwayTeam)
                : filteredList.Where(c => c.HomeTeam == c.UnchangableHomeTeam);

            if (!filteredContainers.Any())
            {
                return -999m;
            }

            var total = filteredContainers.Sum(c => (int)c.GetType().GetProperty(propertyName).GetValue(c));
            var count = filteredContainers.Count();
            return (decimal)total / count;
        }

        private static int GetGeneralCountFound<T>(List<T> listContainers, TeamSide teamSide)
            where T : BaseComparerContainerModel, new()
        {
            if (listContainers == null || listContainers.Count == 0) return -1;

            var result = _quickConvertService.MixRange(listContainers, teamSide).Count();

            return result;
        }

        private static int GetBeSideCountFound<T>(List<T> listContainers, TeamSide teamSide)
            where T : BaseComparerContainerModel, new()
        {
            if (listContainers == null || listContainers.Count == 0) return -1;

            var result = teamSide == TeamSide.Home
                ? listContainers.Where(x => x.HomeTeam == x.UnchangableHomeTeam).Count()
                : listContainers.Where(x => x.AwayTeam == x.UnchangableAwayTeam).Count();

            return result;
        }

        private static PercentageComplainer GenerateTeamPercentage(List<InTimeFilterResultProfilerContainer> container, string propertyName, int index)
        {
            var result = container[index].FilterResults
                .GroupBy(x => x.GetType().GetProperty(propertyName).GetValue(x, null))
                .Select(g =>
                          new PercentageComplainer
                          {
                              Percentage = g.Count() * 100 / container[index].FilterResults.Count(),
                              CountFound = g.Count(),
                              CountAll = container[index].FilterResults.Count(),
                              FeatureName = g.Key.ToString(),
                              PropertyName = propertyName
                          }).ToList().OrderByDescending(x => x.Percentage).ToList()[0];

            return result;
        }

        private static decimal GenerateOddTeamAverage(List<FilterResultMutateModel> container, string propertyName)
        {
            decimal result = -1;

            if (container.Any())
            {
                result = (decimal)container
                    .Select(x => (int)x.GetType().GetProperty(propertyName).GetValue(x, null))
                    .Sum() / container.Count();
            }

            return result;
        }

        private static decimal GenerateOddCornersPossesionShutAverage(List<FilterResultMutateModel> listContainers, string propertyName, Func<FilterResultMutateModel, bool> expression)
        {
            if (listContainers is null || listContainers.Count == 0)
            {
                return -1.00M;
            }

            var property = typeof(FilterResultMutateModel).GetProperty(propertyName);

            if (property is null)
            {
                throw new ArgumentException($"The property '{propertyName}' does not exist on type '{typeof(FilterResultMutateModel).Name}'.");
            }

            var filteredListContainer = listContainers.Where(expression).ToList();

            if (filteredListContainer.Count == 0) return -9999;

            var total = (decimal) filteredListContainer.Select(x => (int)property.GetValue(x)).Sum();
            var average = total / (decimal)filteredListContainer.Count;

            return average;
        }

        private static PercentageComplainer GenerateOddTeamPercentage(List<FilterResultMutateModel> container, string propertyName)
        {
            if (container is null || container.Count == 0)
            {
                return null;
            }

            var groupedContainers = container
                .GroupBy(c => c.GetType().GetProperty(propertyName).GetValue(c, null))
                .Select(g => new PercentageComplainer
                {
                    Percentage = g.Count() * 100 / container.Count(),
                    CountFound = g.Count(),
                    CountAll = container.Count(),
                    FeatureName = g.Key.ToString(),
                    PropertyName = propertyName
                });

            return groupedContainers.OrderByDescending(gc => gc.Percentage).First();
        }

        private static PercentageComplainer GenerateOddTeamPercentage(List<FilterResultMutateModel> container, string propertyName, Func<FilterResultMutateModel, bool> expression)
        {
            if (container is null || container.Count == 0)
            {
                return null;
            }

            var filteredContainer = container.Where(expression).ToList();

            var groupedContainers = filteredContainer
                .GroupBy(c => c.GetType().GetProperty(propertyName).GetValue(c, null))
                .Select(g => new PercentageComplainer
                {
                    Percentage = g.Count() * 100 / filteredContainer.Count(),
                    CountFound = g.Count(),
                    CountAll = filteredContainer.Count(),
                    FeatureName = g.Key.ToString(),
                    PropertyName = propertyName
                });

            return groupedContainers.OrderByDescending(gc => gc.Percentage).First();
        }

        private static PercentageComplainer GenerateTeamPercentage(List<List<FilterResult>> containers, string propertyName, int index)
        {
            var result = containers[index]
                .GroupBy(x => x.GetType().GetProperty(propertyName).GetValue(x, null))
                .Select(g =>
                          new PercentageComplainer
                          {
                              Percentage = g.Count() * 100 / containers[index].Count(),
                              CountFound = g.Count(),
                              CountAll = containers[index].Count(),
                              FeatureName = g.Key.ToString(),
                              PropertyName = propertyName
                          }).ToList().OrderByDescending(x => x.Percentage).ToList()[0];

            return result;
        }

        public static InitialiserPercentageContainer InitialiseWinningPercentage(List<TeamPercentageProfiler> profilers)
        {
            List<FilterResult> filterResults = new List<FilterResult>();
            if (profilers != null && profilers.Count > 0)
            {
                profilers.ForEach(x =>
                {
                    var filtered = ConvertToFilterResultFromTeamPercentageProfiler(x);
                    if (filtered != null)
                        filterResults.Add(filtered);
                });
            }

            List<PriorityCheckerInitialiserModel> priorityListCheckers = new List<PriorityCheckerInitialiserModel>();

            if (filterResults != null && filterResults.Count > 0)
            {
                for (int i = 0; i < filterResults.Count; i++)
                {
                    priorityListCheckers.Add(CheckInitialisedCheckedResponse(profilers.FirstOrDefault(x => x.Serial == filterResults[i].SerialUniqueID.ToString()), filterResults[i]));
                }
            }

            InitialiserPercentageContainer result = new InitialiserPercentageContainer() { MatchedCount = 0 };

            if (priorityListCheckers != null && priorityListCheckers.Count > 0)
            {
                result.MatchedCount = priorityListCheckers.Count;
                var divider = priorityListCheckers.Count;
                var priority1Count = priorityListCheckers.Where(x => x.Priority1).ToList().Count;
                var priority2Count = priorityListCheckers.Where(x => x.Priority2).ToList().Count;
                var priority3Count = priorityListCheckers.Where(x => x.Priority3).ToList().Count;

                result.CalculatedPercentage = new PriorityCheckerVisualiserModel
                {
                    Priority1_Percentage = priority1Count * 100 / divider,
                    Priority2_Percentage = priority2Count * 100 / divider,
                    Priority3_Percentage = priority3Count * 100 / divider,
                };
            }

            return result;
        }

        private static PriorityCheckerInitialiserModel CheckInitialisedCheckedResponse(TeamPercentageProfiler profiler, FilterResult filterResult)
        {
            List<PercentageComplainer> complainers = new List<PercentageComplainer>();

            foreach (var prop in profiler.GetType().GetProperties().ToList())
            {
                bool checker = prop.Name != "HomeTeam" &&
                               prop.Name != "AwayTeam" &&
                               prop.Name != "Serial" &&
                               prop.Name != "TargetURL" &&
                               prop.Name != "ZEND_HT_Result" &&
                               prop.Name != "ZEND_FT_Result";

                if (checker)
                {
                    var complainer = (PercentageComplainer)prop.GetValue(profiler, null);
                    complainers.Add(complainer);
                }
            }

            List<PercentageComplainer> filteredComplainers = new List<PercentageComplainer>();

            List<bool> priorityBoolList = new List<bool>();

            var filterResultPropList = filterResult.GetType().GetProperties().ToList();

            for (int i = 0; i < 3; i++)
            {
                filteredComplainers.Add(complainers.OrderByDescending(x => x.Percentage).ToList()[i]);

                string nameForCheck = complainers.OrderByDescending(x => x.Percentage).ToList()[i].PropertyName;

                string gettedValue = filterResultPropList.FirstOrDefault(x => x.Name == nameForCheck).GetValue(filterResult, null)?.ToString();

                if (!OfferanceAnalysers.Any(x => x.Name == nameForCheck))
                {
                    OfferanceAnalyserContainer offerance = new OfferanceAnalyserContainer
                    {
                        Name = nameForCheck
                    };
                    offerance.ResultList.Add(gettedValue);
                }
                else
                {
                    var offerance = OfferanceAnalysers.FirstOrDefault(x => x.Name == nameForCheck);
                    offerance.ResultList.Add(gettedValue);
                }

                priorityBoolList.Add(gettedValue == complainers.OrderByDescending(x => x.Percentage).ToList()[i].FeatureName);
            }

            PriorityCheckerInitialiserModel response = new PriorityCheckerInitialiserModel
            {
                Priority1 = priorityBoolList[0],
                Priority2 = priorityBoolList[1],
                Priority3 = priorityBoolList[2]
            };

            return response;
        }
    }
}