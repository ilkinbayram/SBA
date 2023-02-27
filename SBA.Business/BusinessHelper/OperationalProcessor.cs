using Core.Entities.Concrete;
using Core.Extensions;
using Core.Resources.Constants;
using Core.Resources.Enums;
using Core.Utilities.Helpers;
using Core.Utilities.UsableModel;
using Core.Utilities.UsableModel.BaseModels;
using Core.Utilities.UsableModel.TempTableModels.Country;
using Core.Utilities.UsableModel.TempTableModels.Initialization;
using Core.Utilities.UsableModel.Visualisers;
using Newtonsoft.Json;
using SBA.Business.Abstract;
using SBA.Business.CoreAbilityServices.Job;
using SBA.Business.FunctionalServices.Concrete;
using SBA.Business.Mapping;
using System.Text.RegularExpressions;

namespace SBA.Business.BusinessHelper
{
    public static class OperationalProcessor
    {
        private static string _defaultMatchUrl = ConfigHelper.GetSettingsDataStatic<string>(ParentKeySettings.UriTemplate.ToString(), ChildKeySettings.ConcatSerialDefaultMatch.ToString());

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

            using (StreamReader srCacheReader = new StreamReader("wwwroot\\files\\jsonFiles\\TimeSerialContainer.json"))
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
                var serials = sr.ReadToEnd().Split(new string[] { "\r\n", "\t\n", "\n" }, StringSplitOptions.None).ToList();

                var result = _proceeder.GetTimeSerialContainers(serials);
                cachedTimeSerials = result;
                TimeSerialListContainer = result;
            }

            using (StreamWriter sw = new StreamWriter("wwwroot\\files\\jsonFiles\\TimeSerialContainer.json"))
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
                serialsText.Split(new string[] { "\r\n", "\t\n", "\n" }, StringSplitOptions.None).ToList().ForEach(x =>
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

        public static List<JobAnalyseModel> GetJobAnalyseModelResultTest7777(IMatchBetService matchBetService, IFilterResultService filterResultService, CountryContainerTemp containerTemp, LeagueContainer leagueContainer, List<string> serials)
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
                    ComparisonOnlyDB = GetComparisonOnlyDbProfilerResult(serials[i], matchBetService, filterResultService, containerTemp, TeamSide.Home),
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
            var result = new InTimeModelResultContainer() { CheckingType = "2.5 Over", Forecast = false, RealityResult = false};

            if(inTimeModel != null)
            {
                if(inTimeModel.FT_W1 >= (decimal)2.20 && inTimeModel.FT_W1 <= (decimal)2.27)
                {
                    if(inTimeModel.FT_W2 >= (decimal) 2.40 && inTimeModel.FT_W2 <= (decimal)2.47)
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
                        if(inTimeModel.Score_1_1 < (decimal)6.20)
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




        public static StandingInfoModel GetStandingInfoModel(string serial)
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
                    Average_FT_Corners_AwayTeam = GenerateHomeAwayCornersAverage(list6PerformanceData, "AwayCornersCount", teamSide),
                    Average_FT_Corners_HomeTeam = GenerateHomeAwayCornersAverage(list6PerformanceData, "HomeCornersCount", teamSide),

                    Corner_Away_3_5_Over = GenerateBySideCornerComparison(list6PerformanceData, "Corner_Away_3_5_Over", teamSide),
                    Corner_Away_4_5_Over = GenerateBySideCornerComparison(list6PerformanceData, "Corner_Away_4_5_Over", teamSide),
                    Corner_Away_5_5_Over = GenerateBySideCornerComparison(list6PerformanceData, "Corner_Away_5_5_Over", teamSide),

                    Corner_Home_3_5_Over = GenerateBySideCornerComparison(list6PerformanceData, "Corner_Home_3_5_Over", teamSide),
                    Corner_Home_4_5_Over = GenerateBySideCornerComparison(list6PerformanceData, "Corner_Home_4_5_Over", teamSide),
                    Corner_Home_5_5_Over = GenerateBySideCornerComparison(list6PerformanceData, "Corner_Home_5_5_Over", teamSide),

                    Corner_7_5_Over = GenerateBySideCornerComparison(list6PerformanceData, "Corner_7_5_Over", teamSide),
                    Corner_8_5_Over = GenerateBySideCornerComparison(list6PerformanceData, "Corner_8_5_Over", teamSide),
                    Corner_9_5_Over = GenerateBySideCornerComparison(list6PerformanceData, "Corner_9_5_Over", teamSide),

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
                    Is_Corner_FT_Win1 = GenerateBySideCornerComparison(list6PerformanceData, "Is_Corner_FT_Win1", teamSide),
                    Is_Corner_FT_X = GenerateBySideCornerComparison(list6PerformanceData, "Is_Corner_FT_X", teamSide),
                    Is_Corner_FT_Win2 = GenerateBySideCornerComparison(list6PerformanceData, "Is_Corner_FT_Win2", teamSide)
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
                    Average_FT_Corners_AwayTeam = GenerateGeneralCornersAverage(list10PerformanceDataGeneral, "AwayCornersCount", teamSide),
                    Average_FT_Corners_HomeTeam = GenerateGeneralCornersAverage(list10PerformanceDataGeneral, "HomeCornersCount", teamSide),

                    Corner_Away_3_5_Over = GenerateGeneralCornerComparison(list10PerformanceDataGeneral, "Corner_Away_3_5_Over", teamSide),
                    Corner_Away_4_5_Over = GenerateGeneralCornerComparison(list10PerformanceDataGeneral, "Corner_Away_4_5_Over", teamSide),
                    Corner_Away_5_5_Over = GenerateGeneralCornerComparison(list10PerformanceDataGeneral, "Corner_Away_5_5_Over", teamSide),

                    Corner_Home_3_5_Over = GenerateGeneralCornerComparison(list10PerformanceDataGeneral, "Corner_Home_3_5_Over", teamSide),
                    Corner_Home_4_5_Over = GenerateGeneralCornerComparison(list10PerformanceDataGeneral, "Corner_Home_4_5_Over", teamSide),
                    Corner_Home_5_5_Over = GenerateGeneralCornerComparison(list10PerformanceDataGeneral, "Corner_Home_5_5_Over", teamSide),

                    Corner_7_5_Over = GenerateGeneralCornerComparison(list10PerformanceDataGeneral, "Corner_7_5_Over", teamSide),
                    Corner_8_5_Over = GenerateGeneralCornerComparison(list10PerformanceDataGeneral, "Corner_8_5_Over", teamSide),
                    Corner_9_5_Over = GenerateGeneralCornerComparison(list10PerformanceDataGeneral, "Corner_9_5_Over", teamSide),

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
                    Is_Corner_FT_Win1 = GenerateGeneralCornerComparison(list10PerformanceDataGeneral, "Is_Corner_FT_Win1", teamSide),
                    Is_Corner_FT_X = GenerateGeneralCornerComparison(list10PerformanceDataGeneral, "Is_Corner_FT_X", teamSide),
                    Is_Corner_FT_Win2 = GenerateGeneralCornerComparison(list10PerformanceDataGeneral, "Is_Corner_FT_Win2", teamSide)
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
                        Average_FT_Goals_AwayTeam = GenerateHomeAwayGoalsAverage(comparisonContainer, "FT_Goals_AwayTeam", TeamSide.Away),
                        Average_FT_Goals_HomeTeam = GenerateHomeAwayGoalsAverage(comparisonContainer, "FT_Goals_HomeTeam", TeamSide.Home),
                        Average_HT_Goals_AwayTeam = GenerateHomeAwayGoalsAverage(comparisonContainer, "HT_Goals_AwayTeam", TeamSide.Away),
                        Average_HT_Goals_HomeTeam = GenerateHomeAwayGoalsAverage(comparisonContainer, "HT_Goals_HomeTeam", TeamSide.Home),
                        Average_SH_Goals_AwayTeam = GenerateHomeAwayGoalsAverage(comparisonContainer, "SH_Goals_AwayTeam", TeamSide.Away),
                        Average_SH_Goals_HomeTeam = GenerateHomeAwayGoalsAverage(comparisonContainer, "SH_Goals_HomeTeam", TeamSide.Home),
                        Average_FT_Corners_AwayTeam = GenerateHomeAwayCornersAverage(comparisonContainer, "AwayCornersCount", TeamSide.Away),
                        Average_FT_Corners_HomeTeam = GenerateHomeAwayCornersAverage(comparisonContainer, "HomeCornersCount", TeamSide.Home),

                        Corner_Away_3_5_Over = GenerateBySideCornerComparison(comparisonContainer, "Corner_Away_3_5_Over", teamSide),
                        Corner_Away_4_5_Over = GenerateBySideCornerComparison(comparisonContainer, "Corner_Away_4_5_Over", teamSide),
                        Corner_Away_5_5_Over = GenerateBySideCornerComparison(comparisonContainer, "Corner_Away_5_5_Over", teamSide),

                        Corner_Home_3_5_Over = GenerateBySideCornerComparison(comparisonContainer, "Corner_Home_3_5_Over", teamSide),
                        Corner_Home_4_5_Over = GenerateBySideCornerComparison(comparisonContainer, "Corner_Home_4_5_Over", teamSide),
                        Corner_Home_5_5_Over = GenerateBySideCornerComparison(comparisonContainer, "Corner_Home_5_5_Over", teamSide),
                        
                        Corner_7_5_Over = GenerateBySideCornerComparison(comparisonContainer, "Corner_7_5_Over", teamSide),
                        Corner_8_5_Over = GenerateBySideCornerComparison(comparisonContainer, "Corner_8_5_Over", teamSide),
                        Corner_9_5_Over = GenerateBySideCornerComparison(comparisonContainer, "Corner_9_5_Over", teamSide),

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
                        Is_Corner_FT_Win1 = GenerateBySideCornerComparison(comparisonContainer, "Is_Corner_FT_Win1", teamSide),
                        Is_Corner_FT_X = GenerateBySideCornerComparison(comparisonContainer, "Is_Corner_FT_X", teamSide),
                        Is_Corner_FT_Win2 = GenerateBySideCornerComparison(comparisonContainer, "Is_Corner_FT_Win2", teamSide)
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
                        Average_FT_Corners_AwayTeam = GenerateGeneralCornersAverage(comparisonContainer, "AwayCornersCount", teamSide),
                        Average_FT_Corners_HomeTeam = GenerateGeneralCornersAverage(comparisonContainer, "HomeCornersCount", teamSide),

                        Corner_Away_3_5_Over = GenerateGeneralCornerComparison(comparisonContainer, "Corner_Away_3_5_Over", teamSide),
                        Corner_Away_4_5_Over = GenerateGeneralCornerComparison(comparisonContainer, "Corner_Away_4_5_Over", teamSide),
                        Corner_Away_5_5_Over = GenerateGeneralCornerComparison(comparisonContainer, "Corner_Away_5_5_Over", teamSide),

                        Corner_Home_3_5_Over = GenerateGeneralCornerComparison(comparisonContainer, "Corner_Home_3_5_Over", teamSide),
                        Corner_Home_4_5_Over = GenerateGeneralCornerComparison(comparisonContainer, "Corner_Home_4_5_Over", teamSide),
                        Corner_Home_5_5_Over = GenerateGeneralCornerComparison(comparisonContainer, "Corner_Home_5_5_Over", teamSide),

                        Corner_7_5_Over = GenerateGeneralCornerComparison(comparisonContainer, "Corner_7_5_Over", teamSide),
                        Corner_8_5_Over = GenerateGeneralCornerComparison(comparisonContainer, "Corner_8_5_Over", teamSide),
                        Corner_9_5_Over = GenerateGeneralCornerComparison(comparisonContainer, "Corner_9_5_Over", teamSide),

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
                        Is_Corner_FT_Win1 = GenerateGeneralCornerComparison(comparisonContainer, "Is_Corner_FT_Win1", teamSide),
                        Is_Corner_FT_X = GenerateGeneralCornerComparison(comparisonContainer, "Is_Corner_FT_X", teamSide),
                        Is_Corner_FT_Win2 = GenerateGeneralCornerComparison(comparisonContainer, "Is_Corner_FT_Win2", teamSide)
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

            var src = webOperation.GetMinifiedString($"https://arsiv.mackolik.com/Match/Default.aspx?id={fResult.SerialUniqueID}#mac-bilgisi");
            var regexKorner2 = new Regex(">Korner<[\\s\\S]*?class=team-2-statistics-text[\\s\\S]*?>[\\s\\S]*?(.+?(?=<))");
            var firstSrcPart = src.Split(">Korner<")[0];

            var korner1 = "";
            var korner2 = "";

            try
            {
                korner1 = firstSrcPart.Substring(firstSrcPart.Length - 40, 1);
                if (korner1.Trim() == ">")
                {
                    korner1 = firstSrcPart.Substring(firstSrcPart.Length - 39, 1);
                }
                else
                {
                    korner1 = firstSrcPart.Substring(firstSrcPart.Length - 40, 2);
                }
                korner2 = regexKorner2.Matches(src)[0].Groups[1].Value;// 5
            }
            catch (Exception)
            {
                try
                {
                    regexKorner2 = new Regex(">Köşe Vuruşu<[\\s\\S]*?class=team-2-statistics-text[\\s\\S]*?>[\\s\\S]*?(.+?(?=<))");
                    firstSrcPart = src.Split(">Köşe Vuruşu<")[0];
                    korner1 = firstSrcPart.Substring(firstSrcPart.Length - 40, 1);
                    if (korner1.Trim() == ">")
                    {
                        korner1 = firstSrcPart.Substring(firstSrcPart.Length - 39, 1);
                    }
                    else
                    {
                        korner1 = firstSrcPart.Substring(firstSrcPart.Length - 40, 2);
                    }
                    korner2 = regexKorner2.Matches(src)[0].Groups[1].Value;
                }
                catch (Exception)
                {
                    return fResult;
                }
            }

            int crn1 = 0;
            int crn2 = 0;

            try
            {
                crn1 = Convert.ToInt32(korner1);
                crn2 = Convert.ToInt32(korner2);
            }
            catch (Exception)
            {
                return fResult;
            }

            fResult.HomeCornerCount = crn1;
            fResult.AwayCornerCount = crn2;
            fResult.Corner_Home_3_5_Over = crn1 > 3;
            fResult.Corner_Home_4_5_Over = crn1 > 4;
            fResult.Corner_Home_5_5_Over = crn1 > 5;
            fResult.Corner_Away_3_5_Over = crn2 > 3;
            fResult.Corner_Away_4_5_Over = crn2 > 4;
            fResult.Corner_Away_5_5_Over = crn2 > 5;
            fResult.Is_Corner_FT_Win1 = crn1 > crn2;
            fResult.Is_Corner_FT_X = crn1 == crn2;
            fResult.Is_Corner_FT_Win2 = crn1 < crn2;
            fResult.Corner_7_5_Over = (crn1 + crn2) > 7;
            fResult.Corner_8_5_Over = (crn1 + crn2) > 8;
            fResult.Corner_9_5_Over = (crn1 + crn2) > 9;
            fResult.IsCornerFound = true;

            return fResult;
        }


        private static PercentageComplainer GenerateGeneralComparison<T>(List<T> listContainers, string propertyName, TeamSide teamSide)
            where T : BaseComparerContainerModel, new()
        {
            PercentageComplainer result = null;

            if (listContainers == null || listContainers.Count == 0) return result;

            var mixedData = _quickConvertService.MixRange(listContainers, teamSide);

            try
            {
                if (teamSide == TeamSide.Away)
                {
                    var awaySideListContainers = mixedData.Where(x => x.AwayTeam == x.UnchangableAwayTeam);

                    if (awaySideListContainers.Any())
                    {
                        result = awaySideListContainers
                .GroupBy(x => x.GetType().GetProperty(propertyName).GetValue(x, null))
                .Select(g =>
                          new PercentageComplainer
                          {
                              Percentage = g.Count() * 100 / awaySideListContainers.Count(),
                              CountFound = g.Count(),
                              CountAll = awaySideListContainers.Count(),
                              FeatureName = g.Key.ToString(),
                              PropertyName = propertyName
                          }).ToList().OrderByDescending(x => x.Percentage).ToList()[0];
                    }
                }
                else
                {
                    var homeSideListContainers = mixedData.Where(x => x.HomeTeam == x.UnchangableHomeTeam);
                    if (homeSideListContainers.Any())
                    {
                        result = homeSideListContainers
                .GroupBy(x => x.GetType().GetProperty(propertyName).GetValue(x, null))
                .Select(g =>
                          new PercentageComplainer
                          {
                              Percentage = g.Count() * 100 / homeSideListContainers.Count(),
                              CountFound = g.Count(),
                              CountAll = homeSideListContainers.Count(),
                              FeatureName = g.Key.ToString(),
                              PropertyName = propertyName
                          }).ToList().OrderByDescending(x => x.Percentage).ToList()[0];
                    }
                }

                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }


        private static PercentageComplainer GenerateGeneralCornerComparison<T>(List<T> listContainers, string propertyName, TeamSide teamSide)
            where T : BaseComparerContainerModel, new()
        {
            PercentageComplainer result = null;

            if (listContainers.Count != listContainers.Count(x=>x.HasCorner))
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

            if (listContainers == null || listContainers.Count == 0) return result;

            var mixedData = _quickConvertService.MixRange(listContainers, teamSide);

            try
            {
                if (teamSide == TeamSide.Away)
                {
                    var awaySideListContainers = mixedData.Where(x => x.AwayTeam == x.UnchangableAwayTeam);

                    if (awaySideListContainers.Any())
                    {
                        result = awaySideListContainers
                .GroupBy(x => x.GetType().GetProperty(propertyName).GetValue(x, null))
                .Select(g =>
                          new PercentageComplainer
                          {
                              Percentage = g.Count() * 100 / awaySideListContainers.Count(),
                              CountFound = g.Count(),
                              CountAll = awaySideListContainers.Count(),
                              FeatureName = g.Key.ToString(),
                              PropertyName = propertyName
                          }).ToList().OrderByDescending(x => x.Percentage).ToList()[0];
                    }
                }
                else
                {
                    var homeSideListContainers = mixedData.Where(x => x.HomeTeam == x.UnchangableHomeTeam);
                    if (homeSideListContainers.Any())
                    {
                        result = homeSideListContainers
                .GroupBy(x => x.GetType().GetProperty(propertyName).GetValue(x, null))
                .Select(g =>
                          new PercentageComplainer
                          {
                              Percentage = g.Count() * 100 / homeSideListContainers.Count(),
                              CountFound = g.Count(),
                              CountAll = homeSideListContainers.Count(),
                              FeatureName = g.Key.ToString(),
                              PropertyName = propertyName
                          }).ToList().OrderByDescending(x => x.Percentage).ToList()[0];
                    }
                }

                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }


        private static PercentageComplainer GenerateBySideComparison<T>(List<T> listContainers, string propertyName, TeamSide teamSide)
            where T : BaseComparerContainerModel, new()
        {
            PercentageComplainer result = null;

            if (listContainers == null || listContainers.Count == 0) return result;

            try
            {
                if (teamSide == TeamSide.Away)
                {
                    var awaySideListContainers = listContainers.Where(x => x.AwayTeam == x.UnchangableAwayTeam);

                    if (awaySideListContainers.Any())
                    {
                        result = awaySideListContainers
                .GroupBy(x => x.GetType().GetProperty(propertyName).GetValue(x, null))
                .Select(g =>
                          new PercentageComplainer
                          {
                              Percentage = g.Count() * 100 / awaySideListContainers.Count(),
                              CountFound = g.Count(),
                              CountAll = awaySideListContainers.Count(),
                              FeatureName = g.Key.ToString(),
                              PropertyName = propertyName
                          }).ToList().OrderByDescending(x => x.Percentage).ToList()[0];
                    }
                }
                else
                {
                    var homeSideListContainers = listContainers.Where(x => x.HomeTeam == x.UnchangableHomeTeam);
                    if (homeSideListContainers.Any())
                    {
                        result = homeSideListContainers
                .GroupBy(x => x.GetType().GetProperty(propertyName).GetValue(x, null))
                .Select(g =>
                          new PercentageComplainer
                          {
                              Percentage = g.Count() * 100 / homeSideListContainers.Count(),
                              CountFound = g.Count(),
                              CountAll = homeSideListContainers.Count(),
                              FeatureName = g.Key.ToString(),
                              PropertyName = propertyName
                          }).ToList().OrderByDescending(x => x.Percentage).ToList()[0];
                    }
                }

                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }


        private static PercentageComplainer GenerateBySideCornerComparison<T>(List<T> listContainers, string propertyName, TeamSide teamSide)
            where T : BaseComparerContainerModel, new()
        {
            PercentageComplainer result = null;

            if (listContainers.Count != listContainers.Count(x => x.HasCorner))
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

            if (listContainers == null || listContainers.Count == 0) return result;

            try
            {
                if (teamSide == TeamSide.Away)
                {
                    var awaySideListContainers = listContainers.Where(x => x.AwayTeam == x.UnchangableAwayTeam);

                    if (awaySideListContainers.Any())
                    {
                        result = awaySideListContainers
                .GroupBy(x => x.GetType().GetProperty(propertyName).GetValue(x, null))
                .Select(g =>
                          new PercentageComplainer
                          {
                              Percentage = g.Count() * 100 / awaySideListContainers.Count(),
                              CountFound = g.Count(),
                              CountAll = awaySideListContainers.Count(),
                              FeatureName = g.Key.ToString(),
                              PropertyName = propertyName
                          }).ToList().OrderByDescending(x => x.Percentage).ToList()[0];
                    }
                }
                else
                {
                    var homeSideListContainers = listContainers.Where(x => x.HomeTeam == x.UnchangableHomeTeam);
                    if (homeSideListContainers.Any())
                    {
                        result = homeSideListContainers
                .GroupBy(x => x.GetType().GetProperty(propertyName).GetValue(x, null))
                .Select(g =>
                          new PercentageComplainer
                          {
                              Percentage = g.Count() * 100 / homeSideListContainers.Count(),
                              CountFound = g.Count(),
                              CountAll = homeSideListContainers.Count(),
                              FeatureName = g.Key.ToString(),
                              PropertyName = propertyName
                          }).ToList().OrderByDescending(x => x.Percentage).ToList()[0];
                    }
                }

                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }


        private static decimal GenerateGeneralGoalsAverage<T>(List<T> listContainers, string propertyName, TeamSide teamSide)
            where T : BaseComparerContainerModel, new()
        {
            if (listContainers == null || listContainers.Count == 0) return (decimal)-1.00;

            var result = (decimal)_quickConvertService.MixRange(listContainers, teamSide)
                        .Select(x => (int)x.GetType().GetProperty(propertyName).GetValue(x, null))
                        .Sum() / listContainers.Count();

            return result;
        }

        private static decimal GenerateGeneralCornersAverage<T>(List<T> listContainers, string propertyName, TeamSide teamSide)
            where T : BaseComparerContainerModel, new()
        {
            if (listContainers.Count != listContainers.Count(x=>x.HasCorner))
            {
                return (decimal)-99999.99;
            }
            if (listContainers == null || listContainers.Count == 0) return (decimal)-1.00;

            var result = (decimal)_quickConvertService.MixRange(listContainers, teamSide)
                        .Select(x => (int)x.GetType().GetProperty(propertyName).GetValue(x, null))
                        .Sum() / listContainers.Count();

            return result;
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


        private static decimal GenerateHomeAwayCornersAverage<T>(List<T> listContainers, string propertyName, TeamSide teamSide)
            where T : BaseComparerContainerModel, new()
        {
            decimal result = -1;

            if(listContainers.Count != listContainers.Count(x => x.HasCorner))
            {
                return (decimal)-99999.99;
            }

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

        private static BetOfferance CheckInitialisedBetOfferance(TeamPercentageProfiler profiler, FilterResult filterResult)
        {
            var any = profiler.GetType().GetProperties().ToList();

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
                    var complainer = (PercentageComplainer)prop.GetValue(any, null);
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

                string gettedValue = filterResultPropList.FirstOrDefault(x => x.Name == nameForCheck).GetValue(filterResultPropList, null)?.ToString();

                priorityBoolList.Add(gettedValue == complainers.OrderByDescending(x => x.Percentage).ToList()[i].FeatureName);
            }

            BetOfferance betOfferance = new BetOfferance
            {
                TeamsDefiner = string.Format("{0} vs {1}", profiler.HomeTeam, profiler.AwayTeam),
                CountFound = profiler.FT_Result.CountAll,
                ChoicePriority1 = string.Format("{0}: [{1}%] | {2} => {3}", filteredComplainers[0].PropertyName,
                                                                            filteredComplainers[0].Percentage,
                                                                            filteredComplainers[0].FeatureName,
                                                                            priorityBoolList[0]),
                ChoicePriority2 = string.Format("{0}: [{1}%] | {2} => {3}", filteredComplainers[1].PropertyName,
                                                                            filteredComplainers[1].Percentage,
                                                                            filteredComplainers[1].FeatureName,
                                                                            priorityBoolList[1]),
                ChoicePriority3 = string.Format("{0}: [{1}%] | {2} => {3}", filteredComplainers[2].PropertyName,
                                                                            filteredComplainers[2].Percentage,
                                                                            filteredComplainers[2].FeatureName,
                                                                            priorityBoolList[2])
            };

            return betOfferance;
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