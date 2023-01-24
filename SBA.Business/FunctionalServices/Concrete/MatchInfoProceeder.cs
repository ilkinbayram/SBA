using Core.Entities.Concrete.SqlEntities.QueryModels;
using Core.Extensions;
using Core.Resources.Constants;
using Core.Resources.Enums;
using Core.Utilities.Helpers;
using Core.Utilities.UsableModel;
using Core.Utilities.UsableModel.BaseModels;
using Core.Utilities.UsableModel.TempTableModels.Country;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace SBA.Business.FunctionalServices.Concrete
{
    public class MatchInfoProceeder
    {
        private readonly string _defaultMatchUrl = ConfigHelper.GetSettingsDataStatic<string>(ParentKeySettings.UriTemplate.ToString(), ChildKeySettings.ConcatSerialDefaultMatch.ToString());

        private readonly string _plusMatchUrl = ConfigHelper.GetSettingsDataStatic<string>(ParentKeySettings.UriTemplate.ToString(), ChildKeySettings.ConcatCallbackAsResultMatch.ToString());

        private readonly string _comparisonMatchUrl = ConfigHelper.GetSettingsDataStatic<string>(ParentKeySettings.UriTemplate.ToString(), ChildKeySettings.ConcatComparisonMatch.ToString());

        private readonly QuickConvert _quickConvert = new QuickConvert();

        private readonly WebOperation _webOperation = new WebOperation();

        public MatchInfoProceeder()
        {
        }

        private ComparisonInfoContainer GetComparisonInfoByPattern(string src)
        {
            return GenerateComparisonInfoByRegex(src);
        }

        private PerformanceDataContainer GetPerformanceData(MatchBetQM model, TeamSide teamSide, string unchangableTeam)
        {
            var result = new PerformanceDataContainer();

            switch (teamSide)
            {
                case TeamSide.Home:
                    result.UnchangableHomeTeam = unchangableTeam;
                    break;
                case TeamSide.Away:
                    result.UnchangableAwayTeam = unchangableTeam;
                    break;
            }

            result.HomeTeam = model.HomeTeam;
            result.AwayTeam = model.AwayTeam;
            result.CountryName = model.Country;

            bool isValid = ValidateScores(model.HT_Match_Result, model.FT_Match_Result);

            if (!isValid) return null;

            result.HT_Goals_AwayTeam = Convert.ToInt32(model.HT_Match_Result.Split('-')[1].Trim());
            result.HT_Goals_HomeTeam = Convert.ToInt32(model.HT_Match_Result.Split('-')[0].Trim());
            result.FT_Goals_AwayTeam = Convert.ToInt32(model.FT_Match_Result.Split('-')[1].Trim());
            result.FT_Goals_HomeTeam = Convert.ToInt32(model.FT_Match_Result.Split('-')[0].Trim());

            return result;
        }

        private bool ValidateScores(string resultHT, string resultFT)
        {
            bool notValid = string.IsNullOrEmpty(resultHT) ||
                            string.IsNullOrEmpty(resultFT) ||
                            resultHT.Split("-").Length != 2 ||
                            resultFT.Split("-").Length != 2;

            return !notValid;
        }

        public StandingInfoModel GetStandingInfoByPattern(string serial)
        {
            return GenerateStandingInfoByRegex(serial);
        }

        private MatchInfoContainer GetSingleMatchInformationByPattern(string serial, Dictionary<string, string> keyValuePair, CountryContainerTemp containerTemp)
        {
            string source;

            if (keyValuePair.TryGetValue(serial, out string _val))
            {
                source = keyValuePair[serial];
            }
            else
            {
                return null;
            }

            var matchInfo = GenerateStartedMatchInfoByRegex<MatchInfoContainer>(source, serial, containerTemp);

            if (string.IsNullOrEmpty(matchInfo.Away) || string.IsNullOrEmpty(matchInfo.Home) || string.IsNullOrEmpty(matchInfo.FT_Result) || string.IsNullOrEmpty(matchInfo.DateMatch))
            {
                return null;
            }

            return matchInfo;
        }

        private List<MatchInfoContainer> GetUnstartedMatchInformationsByPattern(List<string> serials, Dictionary<string, string> keyValuePairs, CountryContainerTemp containerTemp)
        {
            List<MatchInfoContainer> result = new List<MatchInfoContainer>();

            foreach (var serial in serials)
            {
                string source;

                if (keyValuePairs.TryGetValue(serial, out string _val))
                {
                    source = keyValuePairs[serial];
                }
                else
                {
                    return null;
                }

                var matchInfo = GenerateUnstartedMatchInfoByRegex<MatchInfoContainer>(source, serial, containerTemp);

                if (string.IsNullOrEmpty(matchInfo.Away) || string.IsNullOrEmpty(matchInfo.Home) || string.IsNullOrEmpty(matchInfo.DateMatch))
                {
                    continue;
                }

                result.Add(matchInfo);
            }

            return result;
        }

        private List<MatchOddResponseInTimeModel> GetInitializingCalculationInTimeInformationsByPattern(List<string> serials, Dictionary<string, string> keyValuePairs, CountryContainerTemp containerTemp)
        {
            var rgxTime = new Regex(PatternConstant.UnstartedMatchPattern.Time);
            var rgxFTRes = new Regex(PatternConstant.StartedMatchPattern.FTResultMatch);
            var rgxHTRes = new Regex(PatternConstant.StartedMatchPattern.HTResultMatch);

            List<MatchOddResponseInTimeModel> result = new List<MatchOddResponseInTimeModel>();

            foreach (var serial in serials)
            {
                string source;

                if (keyValuePairs.TryGetValue(serial, out string _val))
                {
                    source = keyValuePairs[serial];
                }
                else
                {
                    return null;
                }

                var time = source.ResolveTextByRegex(rgxTime);

                if (!CompareTimeIsUnstarted(time))
                    continue;

                var matchInfo = GenerateUnstartedMatchInfoByRegex<MatchOddResponseInTimeModel>(source, serial, containerTemp);

                matchInfo.ZEND_FT_Result = source.ResolveScoreByRegex(rgxFTRes);

                string specialGenerated = string.Format("{0}{1}/{2}-{3}", _plusMatchUrl, serial, matchInfo.Home.Replace(" ", "-"), matchInfo.Away.Replace(" ", "-"));

                var response = _webOperation.GetMinifiedStringAsync(specialGenerated).Result;

                if (string.IsNullOrEmpty(response)) continue;

                matchInfo.ZEND_HT_Result = response.ResolveScoreByRegex(rgxHTRes);

                if (string.IsNullOrEmpty(matchInfo.Away) || string.IsNullOrEmpty(matchInfo.Home) || string.IsNullOrEmpty(matchInfo.ZEND_FT_Result) || string.IsNullOrEmpty(matchInfo.ZEND_HT_Result))
                {
                    continue;
                }

                result.Add(matchInfo);
            }

            return result;
        }

        private List<MatchOddResponseInTimeModel> GetCalculationModelInTimeInformationsByPattern(List<SerialSourceContainer> containers, int addMinute, bool isAnalayseAnyTime, CountryContainerTemp containerTemp)
        {

            List<MatchOddResponseInTimeModel> result = new List<MatchOddResponseInTimeModel>();

            var rgxTime = new Regex(PatternConstant.UnstartedMatchPattern.Time);

            foreach (var cont in containers)
            {
                var time = cont.Source.ResolveTextByRegex(rgxTime);

                if (!CompareIsAnalysable(time, addMinute, isAnalayseAnyTime))
                    continue;

                var matchInfo = GenerateUnstartedMatchInfoByRegex<MatchOddResponseInTimeModel>(cont.Source, cont.Serial, containerTemp);

                if (matchInfo == null) continue;
                // TODO : Delete
                if (string.IsNullOrEmpty(matchInfo.FT_Result)) continue;

                var matchInfoLast = GenerateSingleHtResultMatchForInTimeModel(matchInfo);


                // TODO : Delete

                if (string.IsNullOrEmpty(matchInfoLast.Away) || string.IsNullOrEmpty(matchInfoLast.Home) || string.IsNullOrEmpty(matchInfoLast.HT_Result) || string.IsNullOrEmpty(matchInfoLast.FT_Result))
                {
                    continue;
                }

                result.Add(matchInfoLast);
            }

            return result;
        }

        private List<TimeSerialContainer> GenerateTimeSerialsByPattern(List<SerialSourceContainer> containers)
        {
            List<TimeSerialContainer> result = new List<TimeSerialContainer>();

            var rgxTime = new Regex(PatternConstant.UnstartedMatchPattern.Time);

            foreach (var cont in containers)
            {
                var timeCont = new TimeSerialContainer();

                timeCont.Serial = cont.Serial;

                timeCont.Time = cont.Source.ResolveTextByRegex(rgxTime);
                if (string.IsNullOrEmpty(timeCont.Time))
                {
                    continue;
                }

                result.Add(timeCont);
            }

            return result;
        }

        public MatchInfoContainer GenerateSingleBetInformation(string serial, CountryContainerTemp containerTemp)
        {
            var uri = string.Format("{0}{1}", _defaultMatchUrl, serial);

            var src = _webOperation.GetMinifiedStringAsync(uri).Result;

            if (string.IsNullOrEmpty(src)) return null;

            Dictionary<string, string> keyValueContainer = new Dictionary<string, string>();
            keyValueContainer.Add(serial, src);

            var result = GetSingleMatchInformationByPattern(serial, keyValueContainer, containerTemp);
            if (result == null)
                return null;
            var resultLast = GenerateSingleHtResultMatch(result);

            bool isValid = ValidateScores(resultLast.HT_Result, resultLast.FT_Result);

            if (!isValid) return null;

            return resultLast;

        }

        public List<ComparisonInfoContainer> SelectListComparisonInfoBetweenTeams(string serial)
        {
            var rgxTeamsSentence = new Regex(PatternConstant.ComparisonInfoPattern.TeamsNames);

            var rgxHomeTeam = new Regex(PatternConstant.StartedMatchPattern.HomeTeam);
            var rgxAwayTeam = new Regex(PatternConstant.StartedMatchPattern.AwayTeam);

            var uri = string.Format("{0}{1}", _comparisonMatchUrl, serial);
            var uriMatchInfo = string.Format("{0}{1}", _defaultMatchUrl, serial);

            var source = _webOperation.GetMinifiedString(uriMatchInfo);

            var src = _webOperation.GetMinifiedString(uri);

            if (string.IsNullOrEmpty(src)) return null;

            string unchangableAwayTeam = rgxAwayTeam.Matches(source)[0].Groups[1].Value;
            string unchangableHomeTeam = rgxHomeTeam.Matches(source)[0].Groups[1].Value;

            var splittedSources = src.Split("class=alt");

            if (splittedSources.Length > 1)
            {
                var result = new List<ComparisonInfoContainer>();

                for (int i = 1; i < splittedSources.Length; i++)
                {
                    var currentSrc = splittedSources[i];

                    var readyItem = GetComparisonInfoByPattern(currentSrc);

                    if (readyItem != null)
                    {
                        readyItem.Serial = serial;
                        readyItem.UnchangableHomeTeam = unchangableHomeTeam;
                        readyItem.UnchangableAwayTeam = unchangableAwayTeam;

                        result.Add(readyItem);
                    }
                };

                return result;
            }

            return null;
        }


        public List<ComparisonInfoContainer> SelectListComparisonInfoBetweenTeams_TEST(string serial)
        {
            var rgxTeamsSentence = new Regex(PatternConstant.ComparisonInfoPattern.TeamsNames);

            var uri = string.Format("{0}{1}", _comparisonMatchUrl, serial);
            var src = _webOperation.GetMinifiedStringAsync(uri).Result;

            if (string.IsNullOrEmpty(src)) return null;

            string unchangableAwayTeam = src.ResolveUnchangableTeamSentenceRegex(TeamSide.Away, rgxTeamsSentence);
            string unchangableHomeTeam = src.ResolveUnchangableTeamSentenceRegex(TeamSide.Home, rgxTeamsSentence);

            var splittedSources = src.Split("class=alt");

            if (splittedSources.Length > 2)
            {
                var result = new List<ComparisonInfoContainer>();

                for (int i = 2; i < splittedSources.Length; i++)
                {
                    var currentSrc = splittedSources[i];

                    var readyItem = GetComparisonInfoByPattern(currentSrc);

                    if (readyItem != null)
                    {
                        readyItem.Serial = serial;
                        readyItem.UnchangableHomeTeam = unchangableHomeTeam;
                        readyItem.UnchangableAwayTeam = unchangableAwayTeam;

                        result.Add(readyItem);
                    }
                };

                return result;
            }

            return null;
        }



        public List<PerformanceDataContainer> SelectListPerformanceDataContainers(List<MatchBetQM> qmModels, TeamSide teamSide, string unchangableAway)
        {
            var result = new List<PerformanceDataContainer>();

            foreach (var model in qmModels)
            {
                if (GetPerformanceData(model, teamSide, unchangableAway) != null)
                    result.Add(GetPerformanceData(model, teamSide, unchangableAway));
            }

            return result;
        }

        public List<MatchInfoContainer> GenerateUnstartedBetInformations(List<string> serialList, CountryContainerTemp containerTemp)
        {
            Dictionary<string, string> keyValueContainer = new Dictionary<string, string>();

            foreach (var serial in serialList)
            {
                var uri = string.Format("{0}{1}", _defaultMatchUrl, serial);
                var src = _webOperation.GetMinifiedStringAsync(uri).Result;
                if (string.IsNullOrEmpty(src)) continue;
                keyValueContainer.Add(serial, src);
            }

            var result = GetUnstartedMatchInformationsByPattern(serialList, keyValueContainer, containerTemp);

            return result;
        }


        public List<MatchOddResponseInTimeModel> GenerateCalculationModelInTimeInformations(List<string> serialList, int addMinute, bool isAnalyseAnyTime, CountryContainerTemp containerTemp)
        {
            string _pathJsonFiles = @"D:\Custom Projects Container\Publisher SBA\SB-Analyser\SBA.MvcUI\wwwroot\files\jsonFiles\SerialSourceContainer.json";

            List<SerialSourceContainer> sourceContainers = new List<SerialSourceContainer>();

            //foreach (var serial in serialList)
            //{
            //    var uri = string.Format("{0}{1}", _defaultMatchUrl, serial);
            //    string src = _webOperation.GetMinifiedStringAsync(uri).Result;
            //    if (string.IsNullOrEmpty(src)) continue;
            //    sourceContainers.Add(new SerialSourceContainer { Serial = serial, Source = src });
            //}

            try
            {
                using (var reader = new StreamReader(_pathJsonFiles))
                {
                    string jsonData = reader.ReadToEnd();
                    sourceContainers = JsonConvert.DeserializeObject<List<SerialSourceContainer>>(jsonData);
                }
            }
            catch (Exception ex)
            {
            }

            var result = GetCalculationModelInTimeInformationsByPattern(sourceContainers, addMinute, isAnalyseAnyTime, containerTemp);

            return result;
        }


        public List<MatchOddResponseInTimeModel> GenerateForInitialisingCalculationModelInTimeInformations(List<string> serialList, CountryContainerTemp containerTemp)
        {
            Dictionary<string, string> keyValueContainer = new Dictionary<string, string>();
            foreach (var serial in serialList)
            {
                var uri = string.Format("{0}{1}", _defaultMatchUrl, serial);
                var src = _webOperation.GetMinifiedStringAsync(uri).Result;
                if (string.IsNullOrEmpty(src)) continue;
                keyValueContainer.Add(serial, src);
            }

            var result = GetInitializingCalculationInTimeInformationsByPattern(serialList, keyValueContainer, containerTemp);

            return result;

        }

        public List<TimeSerialContainer> GetTimeSerialContainers(List<string> serials)
        {
            List<SerialSourceContainer> serialSources = new List<SerialSourceContainer>();
            foreach (var serial in serials)
            {
                var uri = string.Format("{0}{1}", _defaultMatchUrl, serial.Trim());
                var src = _webOperation.GetMinifiedStringAsync(uri).Result;
                if (!string.IsNullOrEmpty(src))
                {
                    serialSources.Add(new SerialSourceContainer { Serial = serial.Trim(), Source = src });
                }
            }

            var result = GenerateTimeSerialsByPattern(serialSources);
            return result;
        }

        private List<MatchInfoContainer> GenerateHtResultMatch(List<MatchInfoContainer> matchOdds)
        {
            var rgxHTRes = new Regex(PatternConstant.StartedMatchPattern.HTResultMatch);
            var rgxHTRes2ndCheck = new Regex(PatternConstant.StartedMatchPattern.HTResultMatch2ndCheck);

            foreach (var oddOne in matchOdds)
            {
                string specialGenerated = string.Format("{0}{1}/{2}-{3}", _plusMatchUrl, oddOne.Serial, oddOne.Home.Replace(" ", "-"), oddOne.Away.Replace(" ", "-"));

                var src = _webOperation.GetMinifiedStringAsync(specialGenerated).Result;

                if (!string.IsNullOrEmpty(src))
                {
                    oddOne.HT_Result = src.ResolveScoreByRegex(rgxHTRes, rgxHTRes2ndCheck);
                }

                if (oddOne.HT_Result.Split("-").Length == 2)
                {
                    var htGoalsArray = oddOne.HT_Result.Split('-');
                    var ftGoalsArray = oddOne.FT_Result.Split('-');

                    int homeHT_Goals = Convert.ToInt32(htGoalsArray[0].Trim());
                    int homeFT_Goals = Convert.ToInt32(ftGoalsArray[0].Trim());
                    int homeSH_Goals = homeFT_Goals - homeHT_Goals;

                    int awayHT_Goals = Convert.ToInt32(htGoalsArray[1].Trim());
                    int awayFT_Goals = Convert.ToInt32(ftGoalsArray[1].Trim());
                    int awaySH_Goals = awayFT_Goals - awayHT_Goals;

                    oddOne.Home_FT_Goals_Count = homeFT_Goals;
                    oddOne.Home_HT_Goals_Count = homeHT_Goals;
                    oddOne.Home_SH_Goals_Count = homeFT_Goals - homeHT_Goals;

                    oddOne.Away_SH_Goals_Count = awayFT_Goals - awayHT_Goals;
                    oddOne.Away_HT_Goals_Count = awayHT_Goals;
                    oddOne.Away_FT_Goals_Count = awayFT_Goals;

                    oddOne.HT_Goals_Count = homeHT_Goals + awayHT_Goals;
                    oddOne.FT_Goals_Count = homeFT_Goals + awayFT_Goals;
                    oddOne.SH_Goals_Count = homeSH_Goals + awaySH_Goals;
                }
            }
            return matchOdds.Where(x => !string.IsNullOrEmpty(x.HT_Result)).ToList();
        }

        private MatchInfoContainer GenerateSingleHtResultMatch(MatchInfoContainer matchInfo)
        {
            if (matchInfo != null)
            {
                var rgxHTRes = new Regex(PatternConstant.StartedMatchPattern.HTResultMatch);
                var rgxHTRes2ndCheck = new Regex(PatternConstant.StartedMatchPattern.HTResultMatch2ndCheck);

                string specialGenerated = string.Format("{0}{1}/{2}-{3}", _plusMatchUrl, matchInfo.Serial, matchInfo.Home.Replace(" ", "-"), matchInfo.Away.Replace(" ", "-"));
                string src = _webOperation.GetMinifiedStringAsync(specialGenerated).Result;

                if (!string.IsNullOrEmpty(src))
                    matchInfo.HT_Result = src.ResolveScoreByRegex(rgxHTRes, rgxHTRes2ndCheck);

                if (!string.IsNullOrEmpty(matchInfo.HT_Result) && matchInfo.HT_Result.Split("-").Length == 2)
                {
                    var htGoalsArray = matchInfo.HT_Result.Split('-');
                    var ftGoalsArray = matchInfo.FT_Result.Split('-');

                    int homeHT_Goals = Convert.ToInt32(htGoalsArray[0].Trim());
                    int homeFT_Goals = Convert.ToInt32(ftGoalsArray[0].Trim());
                    int homeSH_Goals = homeFT_Goals - homeHT_Goals;

                    int awayHT_Goals = Convert.ToInt32(htGoalsArray[1].Trim());
                    int awayFT_Goals = Convert.ToInt32(ftGoalsArray[1].Trim());
                    int awaySH_Goals = awayFT_Goals - awayHT_Goals;

                    matchInfo.Home_FT_Goals_Count = homeFT_Goals;
                    matchInfo.Home_HT_Goals_Count = homeHT_Goals;
                    matchInfo.Home_SH_Goals_Count = homeFT_Goals - homeHT_Goals;

                    matchInfo.Away_SH_Goals_Count = awayFT_Goals - awayHT_Goals;
                    matchInfo.Away_HT_Goals_Count = awayHT_Goals;
                    matchInfo.Away_FT_Goals_Count = awayFT_Goals;

                    matchInfo.HT_Goals_Count = homeHT_Goals + awayHT_Goals;
                    matchInfo.FT_Goals_Count = homeFT_Goals + awayFT_Goals;
                    matchInfo.SH_Goals_Count = homeSH_Goals + awaySH_Goals;
                }

                return matchInfo;
            }

            return null;
        }

        private MatchOddResponseInTimeModel GenerateSingleHtResultMatchForInTimeModel(MatchOddResponseInTimeModel matchInfo)
        {
            if (matchInfo != null)
            {
                var rgxHTRes = new Regex(PatternConstant.StartedMatchPattern.HTResultMatch);
                var rgxHTRes2ndCheck = new Regex(PatternConstant.StartedMatchPattern.HTResultMatch2ndCheck);

                string specialGenerated = string.Format("{0}{1}/{2}-{3}", _plusMatchUrl, matchInfo.Serial, matchInfo.Home.Replace(" ", "-"), matchInfo.Away.Replace(" ", "-"));
                string src = _webOperation.GetMinifiedStringAsync(specialGenerated).Result;

                if (!string.IsNullOrEmpty(src))
                    matchInfo.HT_Result = src.ResolveScoreByRegex(rgxHTRes, rgxHTRes2ndCheck);

                if (!string.IsNullOrEmpty(matchInfo.HT_Result) && matchInfo.HT_Result.Split("-").Length == 2)
                {
                    var htGoalsArray = matchInfo.HT_Result.Split('-');
                    var ftGoalsArray = matchInfo.FT_Result.Split('-');

                    int homeHT_Goals = Convert.ToInt32(htGoalsArray[0].Trim());
                    int homeFT_Goals = Convert.ToInt32(ftGoalsArray[0].Trim());
                    int homeSH_Goals = homeFT_Goals - homeHT_Goals;

                    int awayHT_Goals = Convert.ToInt32(htGoalsArray[1].Trim());
                    int awayFT_Goals = Convert.ToInt32(ftGoalsArray[1].Trim());
                    int awaySH_Goals = awayFT_Goals - awayHT_Goals;

                    matchInfo.Home_FT_Goals_Count = homeFT_Goals;
                    matchInfo.Home_HT_Goals_Count = homeHT_Goals;
                    matchInfo.Home_SH_Goals_Count = homeFT_Goals - homeHT_Goals;

                    matchInfo.Away_SH_Goals_Count = awayFT_Goals - awayHT_Goals;
                    matchInfo.Away_HT_Goals_Count = awayHT_Goals;
                    matchInfo.Away_FT_Goals_Count = awayFT_Goals;

                    matchInfo.HT_Goals_Count = homeHT_Goals + awayHT_Goals;
                    matchInfo.FT_Goals_Count = homeFT_Goals + awayFT_Goals;
                    matchInfo.SH_Goals_Count = homeSH_Goals + awaySH_Goals;
                }

                return matchInfo;
            }

            return null;
        }

        public bool CompareTimeIsUnstarted(string time)
        {
            time = time.Replace(" ", "");
            bool result;

            var info = TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time");
            DateTimeOffset localServerTime = DateTimeOffset.Now;
            DateTimeOffset localTime = TimeZoneInfo.ConvertTime(localServerTime, info);

            TimeSpan nowTime = localTime.TimeOfDay;
            TimeSpan futureTime = TimeSpan.Parse(time);

            result = futureTime.CompareTo(nowTime) > 0;

            return result;
        }


        public bool CompareIsAnalysable(string time, int addMinute, bool analyseAnyTime)
        {
            if (analyseAnyTime) return analyseAnyTime;

            if (string.IsNullOrEmpty(time)) return false;

            var info = TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time");
            DateTimeOffset localServerTime = DateTimeOffset.Now;
            DateTimeOffset localTime = TimeZoneInfo.ConvertTime(localServerTime, info);

            string timeNow = localTime.AddMinutes(addMinute).TimeOfDay.ToString().Substring(0, 5);

            int timeNowHour = Convert.ToInt32(timeNow.Split(":")[0]);
            int matchHour = Convert.ToInt32(time.Split(":")[0]);

            int timeNowMinute = Convert.ToInt32(timeNow.Split(":")[1]);
            int matchMinute = Convert.ToInt32(time.Split(":")[1]);

            TimeSpan compareMatchTime = new TimeSpan(matchHour, matchMinute, 0);
            TimeSpan compareTimeNow = new TimeSpan(timeNowHour, timeNowMinute, 0);

            return compareMatchTime <= compareTimeNow;
        }


        private T GenerateStartedMatchInfoByRegex<T>(string source, string serial, CountryContainerTemp containerTemp) where T : BaseMatchInfo, new()
        {
            var rgxH1 = new Regex(PatternConstant.StartedMatchPattern.HT_Win1);
            var rgxHX = new Regex(PatternConstant.StartedMatchPattern.HT_Draw);
            var rgxH2 = new Regex(PatternConstant.StartedMatchPattern.HT_Win2);
            var rgxF1 = new Regex(PatternConstant.StartedMatchPattern.FT_Win1);
            var rgxFX = new Regex(PatternConstant.StartedMatchPattern.FT_Draw);
            var rgxF2 = new Regex(PatternConstant.StartedMatchPattern.FT_Win2);

            var rgxSH1 = new Regex(PatternConstant.StartedMatchPattern.SH_Win1);
            var rgxSHX = new Regex(PatternConstant.StartedMatchPattern.SH_Draw);
            var rgxSH2 = new Regex(PatternConstant.StartedMatchPattern.SH_Win2);
            var rgxHT_1X = new Regex(PatternConstant.StartedMatchPattern.HT_Double_1_X);
            var rgxHT_12 = new Regex(PatternConstant.StartedMatchPattern.HT_Double_1_2);
            var rgxHT_X2 = new Regex(PatternConstant.StartedMatchPattern.HT_Double_X_2);

            var rgxHomeOv15 = new Regex(PatternConstant.StartedMatchPattern.Home_1_5_Over);
            var rgxHomeUn15 = new Regex(PatternConstant.StartedMatchPattern.Home_1_5_Under);
            var rgxAwayOv15 = new Regex(PatternConstant.StartedMatchPattern.Away_1_5_Over);
            var rgxAwayUn15 = new Regex(PatternConstant.StartedMatchPattern.Away_1_5_Under);

            var rgxHome = new Regex(PatternConstant.StartedMatchPattern.HomeTeam);
            var rgxAway = new Regex(PatternConstant.StartedMatchPattern.AwayTeam);
            var rgxDate = new Regex(PatternConstant.StartedMatchPattern.DateMatch);
            var rgxDate2nd = new Regex(PatternConstant.StartedMatchPattern.DateMatch2nd);
            var rgxLeague = new Regex(PatternConstant.StartedMatchPattern.League);
            var rgxCountry = new Regex(PatternConstant.StartedMatchPattern.Country);
            var rgxGoals01 = new Regex(PatternConstant.StartedMatchPattern.Goals01);
            var rgxGoals23 = new Regex(PatternConstant.StartedMatchPattern.Goals23);
            var rgxGoals45 = new Regex(PatternConstant.StartedMatchPattern.Goals45);
            var rgxGoals6 = new Regex(PatternConstant.StartedMatchPattern.Goals6);
            var rgxGG = new Regex(PatternConstant.StartedMatchPattern.GG);
            var rgxNG = new Regex(PatternConstant.StartedMatchPattern.NG);
            var rgxFT15A = new Regex(PatternConstant.StartedMatchPattern.FT_1_5_Under);
            var rgxFT15U = new Regex(PatternConstant.StartedMatchPattern.FT_1_5_Over);
            var rgxFT25A = new Regex(PatternConstant.StartedMatchPattern.FT_2_5_Under);
            var rgxFT25U = new Regex(PatternConstant.StartedMatchPattern.FT_2_5_Over);
            var rgxFT35A = new Regex(PatternConstant.StartedMatchPattern.FT_3_5_Under);
            var rgxFT35U = new Regex(PatternConstant.StartedMatchPattern.FT_3_5_Over);
            var rgxHT15A = new Regex(PatternConstant.StartedMatchPattern.HT_1_5_Under);
            var rgxHT15U = new Regex(PatternConstant.StartedMatchPattern.HT_1_5_Over);
            var rgxFTRes = new Regex(PatternConstant.StartedMatchPattern.FTResultMatch);
            var rgxFTRes2ndCheck = new Regex(PatternConstant.StartedMatchPattern.FTResultMatch2ndCheck);
            var rgxCountryLeagueMix = new Regex(PatternConstant.StartedMatchPattern.CountryAndLeague);


            var rgxHtFt11 = new Regex(PatternConstant.StartedMatchPattern.HT_FT_Home_Home);
            var rgxHtFt1X = new Regex(PatternConstant.StartedMatchPattern.HT_FT_Home_Draw);
            var rgxHtFt12 = new Regex(PatternConstant.StartedMatchPattern.HT_FT_Home_Away);
            var rgxHtFtX1 = new Regex(PatternConstant.StartedMatchPattern.HT_FT_Draw_Home);
            var rgxHtFtXX = new Regex(PatternConstant.StartedMatchPattern.HT_FT_Draw_Draw);
            var rgxHtFtX2 = new Regex(PatternConstant.StartedMatchPattern.HT_FT_Draw_Away);
            var rgxHtFt21 = new Regex(PatternConstant.StartedMatchPattern.HT_FT_Away_Home);
            var rgxHtFt2X = new Regex(PatternConstant.StartedMatchPattern.HT_FT_Away_Draw);
            var rgxHtFt22 = new Regex(PatternConstant.StartedMatchPattern.HT_FT_Away_Away);
            var rgxFtW1U15 = new Regex(PatternConstant.StartedMatchPattern.FT_Win1_Under_15);
            var rgxFtXU15 = new Regex(PatternConstant.StartedMatchPattern.FT_Draw_Under_15);
            var rgxFtW2U15 = new Regex(PatternConstant.StartedMatchPattern.FT_Win2_Under_15);
            var rgxFtW1O15 = new Regex(PatternConstant.StartedMatchPattern.FT_Win1_Over_15);
            var rgxFtXO15 = new Regex(PatternConstant.StartedMatchPattern.FT_Draw_Over_15);
            var rgxFtW2O15 = new Regex(PatternConstant.StartedMatchPattern.FT_Win2_Over_15);
            var rgxFtW1U25 = new Regex(PatternConstant.StartedMatchPattern.FT_Win1_Under_25);
            var rgxFtXU25 = new Regex(PatternConstant.StartedMatchPattern.FT_Draw_Under_25);
            var rgxFtW2U25 = new Regex(PatternConstant.StartedMatchPattern.FT_Win2_Under_25);
            var rgxFtW1O25 = new Regex(PatternConstant.StartedMatchPattern.FT_Win1_Over_25);
            var rgxFtXO25 = new Regex(PatternConstant.StartedMatchPattern.FT_Draw_Over_25);
            var rgxFtW2O25 = new Regex(PatternConstant.StartedMatchPattern.FT_Win2_Over_25);
            var rgxFtW1U35 = new Regex(PatternConstant.StartedMatchPattern.FT_Win1_Under_35);
            var rgxFtXU35 = new Regex(PatternConstant.StartedMatchPattern.FT_Draw_Under_35);
            var rgxFtW2U35 = new Regex(PatternConstant.StartedMatchPattern.FT_Win2_Under_35);
            var rgxFtW1O35 = new Regex(PatternConstant.StartedMatchPattern.FT_Win1_Over_35);
            var rgxFtXO35 = new Regex(PatternConstant.StartedMatchPattern.FT_Draw_Over_35);
            var rgxFtW2O35 = new Regex(PatternConstant.StartedMatchPattern.FT_Win2_Over_35);
            var rgxFtW1U45 = new Regex(PatternConstant.StartedMatchPattern.FT_Win1_Under_45);
            var rgxFtXU45 = new Regex(PatternConstant.StartedMatchPattern.FT_Draw_Under_45);
            var rgxFtW2U45 = new Regex(PatternConstant.StartedMatchPattern.FT_Win2_Under_45);
            var rgxFtW1O45 = new Regex(PatternConstant.StartedMatchPattern.FT_Win1_Over_45);
            var rgxFtXO45 = new Regex(PatternConstant.StartedMatchPattern.FT_Draw_Over_45);
            var rgxFtW2O45 = new Regex(PatternConstant.StartedMatchPattern.FT_Win2_Over_45);
            var rgxHand04W1 = new Regex(PatternConstant.StartedMatchPattern.Handicap_04_Win1);
            var rgxHand04X = new Regex(PatternConstant.StartedMatchPattern.Handicap_04_Draw);
            var rgxHand04W2 = new Regex(PatternConstant.StartedMatchPattern.Handicap_04_Win2);
            var rgxHand03W1 = new Regex(PatternConstant.StartedMatchPattern.Handicap_03_Win1);
            var rgxHand03X = new Regex(PatternConstant.StartedMatchPattern.Handicap_03_Draw);
            var rgxHand03W2 = new Regex(PatternConstant.StartedMatchPattern.Handicap_03_Win2);
            var rgxHand02W1 = new Regex(PatternConstant.StartedMatchPattern.Handicap_02_Win1);
            var rgxHand02X = new Regex(PatternConstant.StartedMatchPattern.Handicap_02_Draw);
            var rgxHand02W2 = new Regex(PatternConstant.StartedMatchPattern.Handicap_02_Win2);
            var rgxHand01W1 = new Regex(PatternConstant.StartedMatchPattern.Handicap_01_Win1);
            var rgxHand01X = new Regex(PatternConstant.StartedMatchPattern.Handicap_01_Draw);
            var rgxHand01W2 = new Regex(PatternConstant.StartedMatchPattern.Handicap_01_Win2);

            var rgxHand40W1 = new Regex(PatternConstant.StartedMatchPattern.Handicap_40_Win1);
            var rgxHand40X = new Regex(PatternConstant.StartedMatchPattern.Handicap_40_Draw);
            var rgxHand40W2 = new Regex(PatternConstant.StartedMatchPattern.Handicap_40_Win2);
            var rgxHand30W1 = new Regex(PatternConstant.StartedMatchPattern.Handicap_30_Win1);
            var rgxHand30X = new Regex(PatternConstant.StartedMatchPattern.Handicap_30_Draw);
            var rgxHand30W2 = new Regex(PatternConstant.StartedMatchPattern.Handicap_30_Win2);
            var rgxHand20W1 = new Regex(PatternConstant.StartedMatchPattern.Handicap_20_Win1);
            var rgxHand20X = new Regex(PatternConstant.StartedMatchPattern.Handicap_20_Draw);
            var rgxHand20W2 = new Regex(PatternConstant.StartedMatchPattern.Handicap_20_Win2);
            var rgxHand10W1 = new Regex(PatternConstant.StartedMatchPattern.Handicap_10_Win1);
            var rgxHand10X = new Regex(PatternConstant.StartedMatchPattern.Handicap_10_Draw);
            var rgxHand10W2 = new Regex(PatternConstant.StartedMatchPattern.Handicap_10_Win2);

            var rgxFtDouble1X = new Regex(PatternConstant.StartedMatchPattern.FT_Double_1_X);
            var rgxFtDouble12 = new Regex(PatternConstant.StartedMatchPattern.FT_Double_1_2);
            var rgxFtDoubleX2 = new Regex(PatternConstant.StartedMatchPattern.FT_Double_X_2);
            var rgx1stGoal1 = new Regex(PatternConstant.StartedMatchPattern.FirstGoal_Home);
            var rgx1stGoalNot = new Regex(PatternConstant.StartedMatchPattern.FirstGoal_None);
            var rgx1stGoal2 = new Regex(PatternConstant.StartedMatchPattern.FirstGoal_Away);
            var rgxFT45U = new Regex(PatternConstant.StartedMatchPattern.FT_4_5_Over);
            var rgxFT45A = new Regex(PatternConstant.StartedMatchPattern.FT_4_5_Under);
            var rgxFT55U = new Regex(PatternConstant.StartedMatchPattern.FT_5_5_Over);
            var rgxFT55A = new Regex(PatternConstant.StartedMatchPattern.FT_5_5_Under);
            var rgxHT05A = new Regex(PatternConstant.StartedMatchPattern.HT_0_5_Under);
            var rgxHT05U = new Regex(PatternConstant.StartedMatchPattern.HT_0_5_Over);
            var rgxHT25A = new Regex(PatternConstant.StartedMatchPattern.HT_2_5_Under);
            var rgxHT25U = new Regex(PatternConstant.StartedMatchPattern.HT_2_5_Over);

            var rgxHomeOv25 = new Regex(PatternConstant.StartedMatchPattern.Home_2_5_Over);
            var rgxHomeUn25 = new Regex(PatternConstant.StartedMatchPattern.Home_2_5_Under);
            var rgxAwayOv25 = new Regex(PatternConstant.StartedMatchPattern.Away_2_5_Over);
            var rgxAwayUn25 = new Regex(PatternConstant.StartedMatchPattern.Away_2_5_Under);
            var rgxHomeOv35 = new Regex(PatternConstant.StartedMatchPattern.Home_3_5_Over);
            var rgxHomeUn35 = new Regex(PatternConstant.StartedMatchPattern.Home_3_5_Under);
            var rgxAwayOv35 = new Regex(PatternConstant.StartedMatchPattern.Away_3_5_Over);
            var rgxAwayUn35 = new Regex(PatternConstant.StartedMatchPattern.Away_3_5_Under);
            var rgxHomeOv45 = new Regex(PatternConstant.StartedMatchPattern.Home_4_5_Over);
            var rgxHomeUn45 = new Regex(PatternConstant.StartedMatchPattern.Home_4_5_Under);
            var rgxAwayOv45 = new Regex(PatternConstant.StartedMatchPattern.Away_4_5_Over);
            var rgxAwayUn45 = new Regex(PatternConstant.StartedMatchPattern.Away_4_5_Under);

            var rgxEven = new Regex(PatternConstant.StartedMatchPattern.Even_Tek);
            var rgxOdd = new Regex(PatternConstant.StartedMatchPattern.Odd_Cut);

            var rgxScore_0_0 = new Regex(PatternConstant.StartedMatchPattern.Score_0_0);
            var rgxScore_1_0 = new Regex(PatternConstant.StartedMatchPattern.Score_1_0);
            var rgxScore_2_0 = new Regex(PatternConstant.StartedMatchPattern.Score_2_0);
            var rgxScore_3_0 = new Regex(PatternConstant.StartedMatchPattern.Score_3_0);
            var rgxScore_4_0 = new Regex(PatternConstant.StartedMatchPattern.Score_4_0);
            var rgxScore_5_0 = new Regex(PatternConstant.StartedMatchPattern.Score_5_0);
            var rgxScore_6_0 = new Regex(PatternConstant.StartedMatchPattern.Score_6_0);
            var rgxScore_0_1 = new Regex(PatternConstant.StartedMatchPattern.Score_0_1);
            var rgxScore_0_2 = new Regex(PatternConstant.StartedMatchPattern.Score_0_2);
            var rgxScore_0_3 = new Regex(PatternConstant.StartedMatchPattern.Score_0_3);
            var rgxScore_0_4 = new Regex(PatternConstant.StartedMatchPattern.Score_0_4);
            var rgxScore_0_5 = new Regex(PatternConstant.StartedMatchPattern.Score_0_5);
            var rgxScore_0_6 = new Regex(PatternConstant.StartedMatchPattern.Score_0_6);
            var rgxScore_1_1 = new Regex(PatternConstant.StartedMatchPattern.Score_1_1);
            var rgxScore_2_1 = new Regex(PatternConstant.StartedMatchPattern.Score_2_1);
            var rgxScore_3_1 = new Regex(PatternConstant.StartedMatchPattern.Score_3_1);
            var rgxScore_4_1 = new Regex(PatternConstant.StartedMatchPattern.Score_4_1);
            var rgxScore_5_1 = new Regex(PatternConstant.StartedMatchPattern.Score_5_1);
            var rgxScore_1_2 = new Regex(PatternConstant.StartedMatchPattern.Score_1_2);
            var rgxScore_1_3 = new Regex(PatternConstant.StartedMatchPattern.Score_1_3);
            var rgxScore_1_4 = new Regex(PatternConstant.StartedMatchPattern.Score_1_4);
            var rgxScore_1_5 = new Regex(PatternConstant.StartedMatchPattern.Score_1_5);
            var rgxScore_2_2 = new Regex(PatternConstant.StartedMatchPattern.Score_2_2);
            var rgxScore_3_2 = new Regex(PatternConstant.StartedMatchPattern.Score_3_2);
            var rgxScore_4_2 = new Regex(PatternConstant.StartedMatchPattern.Score_4_2);
            var rgxScore_2_3 = new Regex(PatternConstant.StartedMatchPattern.Score_2_3);
            var rgxScore_2_4 = new Regex(PatternConstant.StartedMatchPattern.Score_2_4);
            var rgxScore_3_3 = new Regex(PatternConstant.StartedMatchPattern.Score_3_3);
            var rgxScore_Other = new Regex(PatternConstant.StartedMatchPattern.Score_Other);

            var rgxMoreGoal1stHalf = new Regex(PatternConstant.StartedMatchPattern.MoreGoal_1st);
            var rgxMoreGoalEqual = new Regex(PatternConstant.StartedMatchPattern.MoreGoal_Equal);
            var rgxMoreGoal2ndHalf = new Regex(PatternConstant.StartedMatchPattern.MoreGoal_2nd);
            var rgxHtCorner45Ust = new Regex(PatternConstant.StartedMatchPattern.HT_Corners_4_5_Over);
            var rgxHtCorner45Alt = new Regex(PatternConstant.StartedMatchPattern.HT_Corners_4_5_Under);
            var rgxFtCorner85Ust = new Regex(PatternConstant.StartedMatchPattern.Corners_8_5_Over);
            var rgxFtCorner85Alt = new Regex(PatternConstant.StartedMatchPattern.Corners_8_5_Under);
            var rgxFtCorner95Ust = new Regex(PatternConstant.StartedMatchPattern.Corners_9_5_Over);
            var rgxFtCorner95Alt = new Regex(PatternConstant.StartedMatchPattern.Corners_9_5_Under);
            var rgxFtCorner105Ust = new Regex(PatternConstant.StartedMatchPattern.Corners_10_5_Over);
            var rgxFtCorner105Alt = new Regex(PatternConstant.StartedMatchPattern.Corners_10_5_Under);

            var rgxFtMoreCornerHome = new Regex(PatternConstant.StartedMatchPattern.FT_MoreCorner_Home);
            var rgxFtMoreCornerEqual = new Regex(PatternConstant.StartedMatchPattern.FT_MoreCorner_Equal);
            var rgxFtMoreCornerAway = new Regex(PatternConstant.StartedMatchPattern.FT_MoreCorner_Away);
            var rgxHtMoreCornerHome = new Regex(PatternConstant.StartedMatchPattern.HT_MoreCorner_Home);
            var rgxHtMoreCornerEqual = new Regex(PatternConstant.StartedMatchPattern.HT_MoreCorner_Equal);
            var rgxHtMoreCornerAway = new Regex(PatternConstant.StartedMatchPattern.HT_MoreCorner_Away);

            var rgxFirstCornerHome = new Regex(PatternConstant.StartedMatchPattern.FirstCorner_Home);
            var rgxFirstCornerNever = new Regex(PatternConstant.StartedMatchPattern.FirstCorner_Never);
            var rgxFirstCornerAway = new Regex(PatternConstant.StartedMatchPattern.FirstCorner_Away);

            var rgxFtCornerRange08 = new Regex(PatternConstant.StartedMatchPattern.FT_Corners_Range_0_8);
            var rgxFtCornerRange911 = new Regex(PatternConstant.StartedMatchPattern.FT_Corners_Range_9_11);
            var rgxFtCornerRange12 = new Regex(PatternConstant.StartedMatchPattern.FT_Corners_Range_12);
            var rgxHtCornerRange04 = new Regex(PatternConstant.StartedMatchPattern.HT_Corners_Range_0_4);
            var rgxHtCornerRange56 = new Regex(PatternConstant.StartedMatchPattern.HT_Corners_Range_5_6);
            var rgxHtCornerRange7 = new Regex(PatternConstant.StartedMatchPattern.HT_Corners_Range_7);

            var rgxCard35Ov = new Regex(PatternConstant.StartedMatchPattern.Cards_3_5_Over);
            var rgxCard35Un = new Regex(PatternConstant.StartedMatchPattern.Cards_3_5_Under);
            var rgxCard45Ov = new Regex(PatternConstant.StartedMatchPattern.Cards_4_5_Over);
            var rgxCard45Un = new Regex(PatternConstant.StartedMatchPattern.Cards_4_5_Under);
            var rgxCard55Ov = new Regex(PatternConstant.StartedMatchPattern.Cards_5_5_Over);
            var rgxCard55Un = new Regex(PatternConstant.StartedMatchPattern.Cards_5_5_Under);




            T result = new T
            {
                Serial = serial,
                HT_W1 = source.ResolveOddByRegex(rgxH1),
                HT_X = source.ResolveOddByRegex(rgxHX),
                HT_W2 = source.ResolveOddByRegex(rgxH2),
                HT_Double_1X = source.ResolveOddByRegex(rgxHT_1X),
                HT_Double_12 = source.ResolveOddByRegex(rgxHT_12),
                HT_Double_X2 = source.ResolveOddByRegex(rgxHT_X2),
                Home_15_Over = source.ResolveOddByRegex(rgxHomeOv15),
                Home_15_Under = source.ResolveOddByRegex(rgxHomeUn15),
                Away_15_Over = source.ResolveOddByRegex(rgxAwayOv15),
                Away_15_Under = source.ResolveOddByRegex(rgxAwayUn15),
                SH_W1 = source.ResolveOddByRegex(rgxSH1),
                SH_X = source.ResolveOddByRegex(rgxSHX),
                SH_W2 = source.ResolveOddByRegex(rgxSH2),
                FT_W1 = source.ResolveOddByRegex(rgxF1),
                FT_X = source.ResolveOddByRegex(rgxFX),
                FT_W2 = source.ResolveOddByRegex(rgxF2),
                GG = source.ResolveOddByRegex(rgxGG),
                NG = source.ResolveOddByRegex(rgxNG),
                Goals01 = source.ResolveOddByRegex(rgxGoals01),
                Goals23 = source.ResolveOddByRegex(rgxGoals23),
                Goals45 = source.ResolveOddByRegex(rgxGoals45),
                Goals6 = source.ResolveOddByRegex(rgxGoals6),
                HT_15_Over = source.ResolveOddByRegex(rgxHT15U),
                HT_15_Under = source.ResolveOddByRegex(rgxHT15A),
                FT_15_Over = source.ResolveOddByRegex(rgxFT15U),
                FT_15_Under = source.ResolveOddByRegex(rgxFT15A),
                FT_25_Over = source.ResolveOddByRegex(rgxFT25U),
                FT_25_Under = source.ResolveOddByRegex(rgxFT25A),
                FT_35_Over = source.ResolveOddByRegex(rgxFT35U),
                FT_35_Under = source.ResolveOddByRegex(rgxFT35A),
                Away = source.ResolveTextByRegex(rgxAway),
                Home = source.ResolveTextByRegex(rgxHome),
                DateMatch = source.ResolveDateByRegex(rgxDate, rgxDate2nd),
                Country = source.ResolveCountryByRegex(containerTemp, rgxCountryLeagueMix, rgxCountry),
                League = source.ResolveLeagueByRegex(containerTemp, rgxCountryLeagueMix, rgxLeague),
                FT_Result = source.ResolveScoreByRegex(rgxFTRes, rgxFTRes2ndCheck),

                HT_FT_Home_Home = source.ResolveOddByRegex(rgxHtFt11),
                HT_FT_Home_Draw = source.ResolveOddByRegex(rgxHtFt1X),
                HT_FT_Home_Away = source.ResolveOddByRegex(rgxHtFt12),
                HT_FT_Draw_Home = source.ResolveOddByRegex(rgxHtFtX1),
                HT_FT_Draw_Draw = source.ResolveOddByRegex(rgxHtFtXX),
                HT_FT_Draw_Away = source.ResolveOddByRegex(rgxHtFtX2),
                HT_FT_Away_Home = source.ResolveOddByRegex(rgxHtFt21),
                HT_FT_Away_Draw = source.ResolveOddByRegex(rgxHtFt2X),
                HT_FT_Away_Away = source.ResolveOddByRegex(rgxHtFt22),
                FT_Win1_Under_15 = source.ResolveOddByRegex(rgxFtW1U15),
                FT_Draw_Under_15 = source.ResolveOddByRegex(rgxFtXU15),
                FT_Win2_Under_15 = source.ResolveOddByRegex(rgxFtW2U15),
                FT_Win1_Over_15 = source.ResolveOddByRegex(rgxFtW1O15),
                FT_Draw_Over_15 = source.ResolveOddByRegex(rgxFtXO15),
                FT_Win2_Over_15 = source.ResolveOddByRegex(rgxFtW2O15),
                FT_Win1_Under_25 = source.ResolveOddByRegex(rgxFtW1U25),
                FT_Draw_Under_25 = source.ResolveOddByRegex(rgxFtXU25),
                FT_Win2_Under_25 = source.ResolveOddByRegex(rgxFtW2U25),
                FT_Win1_Over_25 = source.ResolveOddByRegex(rgxFtW1O25),
                FT_Draw_Over_25 = source.ResolveOddByRegex(rgxFtXO25),
                FT_Win2_Over_25 = source.ResolveOddByRegex(rgxFtW2O25),
                FT_Win1_Under_35 = source.ResolveOddByRegex(rgxFtW1U35),
                FT_Draw_Under_35 = source.ResolveOddByRegex(rgxFtXU35),
                FT_Win2_Under_35 = source.ResolveOddByRegex(rgxFtW2U35),
                FT_Win1_Over_35 = source.ResolveOddByRegex(rgxFtW1O35),
                FT_Draw_Over_35 = source.ResolveOddByRegex(rgxFtXO35),
                FT_Win2_Over_35 = source.ResolveOddByRegex(rgxFtW2O35),
                FT_Win1_Under_45 = source.ResolveOddByRegex(rgxFtW1U45),
                FT_Draw_Under_45 = source.ResolveOddByRegex(rgxFtXU45),
                FT_Win2_Under_45 = source.ResolveOddByRegex(rgxFtW2U45),
                FT_Win1_Over_45 = source.ResolveOddByRegex(rgxFtW1O45),
                FT_Draw_Over_45 = source.ResolveOddByRegex(rgxFtXO45),
                FT_Win2_Over_45 = source.ResolveOddByRegex(rgxFtW2O45),
                Handicap_04_Win1 = source.ResolveOddByRegex(rgxHand04W1),
                Handicap_04_Draw = source.ResolveOddByRegex(rgxHand04X),
                Handicap_04_Win2 = source.ResolveOddByRegex(rgxHand04W2),
                Handicap_03_Win1 = source.ResolveOddByRegex(rgxHand03W1),
                Handicap_03_Draw = source.ResolveOddByRegex(rgxHand03X),
                Handicap_03_Win2 = source.ResolveOddByRegex(rgxHand03W2),
                Handicap_02_Win1 = source.ResolveOddByRegex(rgxHand02W1),
                Handicap_02_Draw = source.ResolveOddByRegex(rgxHand02X),
                Handicap_02_Win2 = source.ResolveOddByRegex(rgxHand02W2),
                Handicap_01_Win1 = source.ResolveOddByRegex(rgxHand01W1),
                Handicap_01_Draw = source.ResolveOddByRegex(rgxHand01X),
                Handicap_01_Win2 = source.ResolveOddByRegex(rgxHand01W2),
                Handicap_40_Win1 = source.ResolveOddByRegex(rgxHand40W1),
                Handicap_40_Draw = source.ResolveOddByRegex(rgxHand40X),
                Handicap_40_Win2 = source.ResolveOddByRegex(rgxHand40W2),
                Handicap_30_Win1 = source.ResolveOddByRegex(rgxHand30W1),
                Handicap_30_Draw = source.ResolveOddByRegex(rgxHand30X),
                Handicap_30_Win2 = source.ResolveOddByRegex(rgxHand30W2),
                Handicap_20_Win1 = source.ResolveOddByRegex(rgxHand20W1),
                Handicap_20_Draw = source.ResolveOddByRegex(rgxHand20X),
                Handicap_20_Win2 = source.ResolveOddByRegex(rgxHand20W2),
                Handicap_10_Win1 = source.ResolveOddByRegex(rgxHand10W1),
                Handicap_10_Draw = source.ResolveOddByRegex(rgxHand10X),
                Handicap_10_Win2 = source.ResolveOddByRegex(rgxHand10W2),
                FT_Double_1_X = source.ResolveOddByRegex(rgxFtDouble1X),
                FT_Double_1_2 = source.ResolveOddByRegex(rgxFtDouble12),
                FT_Double_X_2 = source.ResolveOddByRegex(rgxFtDoubleX2),
                FirstGoal_Home = source.ResolveOddByRegex(rgx1stGoal1),
                FirstGoal_None = source.ResolveOddByRegex(rgx1stGoalNot),
                FirstGoal_Away = source.ResolveOddByRegex(rgx1stGoal2),
                FT_4_5_Under = source.ResolveOddByRegex(rgxFT45A),
                FT_4_5_Over = source.ResolveOddByRegex(rgxFT45U),
                FT_5_5_Under = source.ResolveOddByRegex(rgxFT55A),
                FT_5_5_Over = source.ResolveOddByRegex(rgxFT55U),
                HT_0_5_Under = source.ResolveOddByRegex(rgxHT05A),
                HT_0_5_Over = source.ResolveOddByRegex(rgxHT05U),
                HT_2_5_Under = source.ResolveOddByRegex(rgxHT25A),
                HT_2_5_Over = source.ResolveOddByRegex(rgxHT25U),
                Home_2_5_Under = source.ResolveOddByRegex(rgxHomeUn25),
                Home_2_5_Over = source.ResolveOddByRegex(rgxHomeOv25),
                Home_3_5_Under = source.ResolveOddByRegex(rgxHomeUn35),
                Home_3_5_Over = source.ResolveOddByRegex(rgxHomeOv35),
                Home_4_5_Under = source.ResolveOddByRegex(rgxHomeUn45),
                Home_4_5_Over = source.ResolveOddByRegex(rgxHomeOv45),
                Away_2_5_Under = source.ResolveOddByRegex(rgxAwayUn25),
                Away_2_5_Over = source.ResolveOddByRegex(rgxAwayOv25),
                Away_3_5_Under = source.ResolveOddByRegex(rgxAwayUn35),
                Away_3_5_Over = source.ResolveOddByRegex(rgxAwayOv35),
                Away_4_5_Under = source.ResolveOddByRegex(rgxAwayUn45),
                Away_4_5_Over = source.ResolveOddByRegex(rgxAwayOv45),
                Even_Tek = source.ResolveOddByRegex(rgxEven),
                Odd_Cut = source.ResolveOddByRegex(rgxOdd),
                Score_0_0 = source.ResolveOddByRegex(rgxScore_0_0),
                Score_1_0 = source.ResolveOddByRegex(rgxScore_1_0),
                Score_2_0 = source.ResolveOddByRegex(rgxScore_2_0),
                Score_3_0 = source.ResolveOddByRegex(rgxScore_3_0),
                Score_4_0 = source.ResolveOddByRegex(rgxScore_4_0),
                Score_5_0 = source.ResolveOddByRegex(rgxScore_5_0),
                Score_6_0 = source.ResolveOddByRegex(rgxScore_6_0),
                Score_0_1 = source.ResolveOddByRegex(rgxScore_0_1),
                Score_0_2 = source.ResolveOddByRegex(rgxScore_0_2),
                Score_0_3 = source.ResolveOddByRegex(rgxScore_0_3),
                Score_0_4 = source.ResolveOddByRegex(rgxScore_0_4),
                Score_0_5 = source.ResolveOddByRegex(rgxScore_0_5),
                Score_0_6 = source.ResolveOddByRegex(rgxScore_0_6),
                Score_1_1 = source.ResolveOddByRegex(rgxScore_1_1),
                Score_2_1 = source.ResolveOddByRegex(rgxScore_2_1),
                Score_3_1 = source.ResolveOddByRegex(rgxScore_3_1),
                Score_4_1 = source.ResolveOddByRegex(rgxScore_4_1),
                Score_5_1 = source.ResolveOddByRegex(rgxScore_5_1),
                Score_1_2 = source.ResolveOddByRegex(rgxScore_1_2),
                Score_1_3 = source.ResolveOddByRegex(rgxScore_1_3),
                Score_1_4 = source.ResolveOddByRegex(rgxScore_1_4),
                Score_1_5 = source.ResolveOddByRegex(rgxScore_1_5),
                Score_2_2 = source.ResolveOddByRegex(rgxScore_2_2),
                Score_3_2 = source.ResolveOddByRegex(rgxScore_3_2),
                Score_4_2 = source.ResolveOddByRegex(rgxScore_4_2),
                Score_2_3 = source.ResolveOddByRegex(rgxScore_2_3),
                Score_2_4 = source.ResolveOddByRegex(rgxScore_2_4),
                Score_3_3 = source.ResolveOddByRegex(rgxScore_3_3),
                Score_Other = source.ResolveOddByRegex(rgxScore_Other),
                MoreGoal_1st = source.ResolveOddByRegex(rgxMoreGoal1stHalf),
                MoreGoal_Equal = source.ResolveOddByRegex(rgxMoreGoalEqual),
                MoreGoal_2nd = source.ResolveOddByRegex(rgxMoreGoal2ndHalf),
                HT_Corners_4_5_Over = source.ResolveOddByRegex(rgxHtCorner45Ust),
                HT_Corners_4_5_Under = source.ResolveOddByRegex(rgxHtCorner45Alt),
                Corners_8_5_Over = source.ResolveOddByRegex(rgxFtCorner85Ust),
                Corners_8_5_Under = source.ResolveOddByRegex(rgxFtCorner85Alt),
                Corners_9_5_Over = source.ResolveOddByRegex(rgxFtCorner95Ust),
                Corners_9_5_Under = source.ResolveOddByRegex(rgxFtCorner95Alt),
                Corners_10_5_Over = source.ResolveOddByRegex(rgxFtCorner105Ust),
                Corners_10_5_Under = source.ResolveOddByRegex(rgxFtCorner105Alt),
                FT_MoreCorner_Home = source.ResolveOddByRegex(rgxFtMoreCornerHome),
                FT_MoreCorner_Equal = source.ResolveOddByRegex(rgxFtMoreCornerEqual),
                FT_MoreCorner_Away = source.ResolveOddByRegex(rgxFtMoreCornerAway),
                HT_MoreCorner_Home = source.ResolveOddByRegex(rgxHtMoreCornerHome),
                HT_MoreCorner_Equal = source.ResolveOddByRegex(rgxHtMoreCornerEqual),
                HT_MoreCorner_Away = source.ResolveOddByRegex(rgxHtMoreCornerAway),
                FirstCorner_Home = source.ResolveOddByRegex(rgxFirstCornerHome),
                FirstCorner_Never = source.ResolveOddByRegex(rgxFirstCornerNever),
                FirstCorner_Away = source.ResolveOddByRegex(rgxFirstCornerAway),
                FT_Corners_Range_0_8 = source.ResolveOddByRegex(rgxFtCornerRange08),
                FT_Corners_Range_9_11 = source.ResolveOddByRegex(rgxFtCornerRange911),
                FT_Corners_Range_12 = source.ResolveOddByRegex(rgxFtCornerRange12),
                HT_Corners_Range_0_4 = source.ResolveOddByRegex(rgxHtCornerRange04),
                HT_Corners_Range_5_6 = source.ResolveOddByRegex(rgxHtCornerRange56),
                HT_Corners_Range_7 = source.ResolveOddByRegex(rgxHtCornerRange7),
                Cards_3_5_Over = source.ResolveOddByRegex(rgxCard35Ov),
                Cards_3_5_Under = source.ResolveOddByRegex(rgxCard35Un),
                Cards_4_5_Over = source.ResolveOddByRegex(rgxCard45Ov),
                Cards_4_5_Under = source.ResolveOddByRegex(rgxCard45Un),
                Cards_5_5_Over = source.ResolveOddByRegex(rgxCard55Ov),
                Cards_5_5_Under = source.ResolveOddByRegex(rgxCard55Un)
            };

            return result;
        }

        private T GenerateUnstartedMatchInfoByRegex<T>(string source, string serial, CountryContainerTemp containerTemp)
            where T : BaseMatchInfo, new()
        {
            var rgxH1 = new Regex(PatternConstant.UnstartedMatchPattern.HT_Win1);
            var rgxHX = new Regex(PatternConstant.UnstartedMatchPattern.HT_Draw);
            var rgxH2 = new Regex(PatternConstant.UnstartedMatchPattern.HT_Win2);
            var rgxF1 = new Regex(PatternConstant.UnstartedMatchPattern.FT_Win1);
            var rgxFX = new Regex(PatternConstant.UnstartedMatchPattern.FT_Draw);
            var rgxF2 = new Regex(PatternConstant.UnstartedMatchPattern.FT_Win2);
            var rgxSH1 = new Regex(PatternConstant.UnstartedMatchPattern.SH_Win1);
            var rgxSHX = new Regex(PatternConstant.UnstartedMatchPattern.SH_Draw);
            var rgxSH2 = new Regex(PatternConstant.UnstartedMatchPattern.SH_Win2);
            var rgxHT_1X = new Regex(PatternConstant.UnstartedMatchPattern.HT_Double_1_X);
            var rgxHT_12 = new Regex(PatternConstant.UnstartedMatchPattern.HT_Double_1_2);
            var rgxHT_X2 = new Regex(PatternConstant.UnstartedMatchPattern.HT_Double_X_2);

            var rgxHomeOv15 = new Regex(PatternConstant.UnstartedMatchPattern.Home_1_5_Over);
            var rgxHomeUn15 = new Regex(PatternConstant.UnstartedMatchPattern.Home_1_5_Under);
            var rgxAwayOv15 = new Regex(PatternConstant.UnstartedMatchPattern.Away_1_5_Over);
            var rgxAwayUn15 = new Regex(PatternConstant.UnstartedMatchPattern.Away_1_5_Under);

            var rgxHome = new Regex(PatternConstant.UnstartedMatchPattern.HomeTeam);
            var rgxAway = new Regex(PatternConstant.UnstartedMatchPattern.AwayTeam);
            var rgxDate = new Regex(PatternConstant.UnstartedMatchPattern.DateMatch);
            var rgxLeague = new Regex(PatternConstant.UnstartedMatchPattern.League);
            var rgxCountry = new Regex(PatternConstant.UnstartedMatchPattern.Country);
            var rgxGoals01 = new Regex(PatternConstant.UnstartedMatchPattern.Goals01);
            var rgxGoals23 = new Regex(PatternConstant.UnstartedMatchPattern.Goals23);
            var rgxGoals45 = new Regex(PatternConstant.UnstartedMatchPattern.Goals45);
            var rgxGoals6 = new Regex(PatternConstant.UnstartedMatchPattern.Goals6);
            var rgxGG = new Regex(PatternConstant.UnstartedMatchPattern.GG);
            var rgxNG = new Regex(PatternConstant.UnstartedMatchPattern.NG);
            var rgxFT15A = new Regex(PatternConstant.UnstartedMatchPattern.FT_1_5_Under);
            var rgxFT15U = new Regex(PatternConstant.UnstartedMatchPattern.FT_1_5_Over);
            var rgxFT25A = new Regex(PatternConstant.UnstartedMatchPattern.FT_2_5_Under);
            var rgxFT25U = new Regex(PatternConstant.UnstartedMatchPattern.FT_2_5_Over);
            var rgxFT35A = new Regex(PatternConstant.UnstartedMatchPattern.FT_3_5_Under);
            var rgxFT35U = new Regex(PatternConstant.UnstartedMatchPattern.FT_3_5_Over);
            var rgxHT15A = new Regex(PatternConstant.UnstartedMatchPattern.HT_1_5_Under);
            var rgxHT15U = new Regex(PatternConstant.UnstartedMatchPattern.HT_1_5_Over);
            var rgxCountryLeagueMix = new Regex(PatternConstant.UnstartedMatchPattern.CountryAndLeague);

            var rgxHtFt11 = new Regex(PatternConstant.UnstartedMatchPattern.HT_FT_Home_Home);
            var rgxHtFt1X = new Regex(PatternConstant.UnstartedMatchPattern.HT_FT_Home_Draw);
            var rgxHtFt12 = new Regex(PatternConstant.UnstartedMatchPattern.HT_FT_Home_Away);
            var rgxHtFtX1 = new Regex(PatternConstant.UnstartedMatchPattern.HT_FT_Draw_Home);
            var rgxHtFtXX = new Regex(PatternConstant.UnstartedMatchPattern.HT_FT_Draw_Draw);
            var rgxHtFtX2 = new Regex(PatternConstant.UnstartedMatchPattern.HT_FT_Draw_Away);
            var rgxHtFt21 = new Regex(PatternConstant.UnstartedMatchPattern.HT_FT_Away_Home);
            var rgxHtFt2X = new Regex(PatternConstant.UnstartedMatchPattern.HT_FT_Away_Draw);
            var rgxHtFt22 = new Regex(PatternConstant.UnstartedMatchPattern.HT_FT_Away_Away);
            var rgxFtW1U15 = new Regex(PatternConstant.UnstartedMatchPattern.FT_Win1_Under_15);
            var rgxFtXU15 = new Regex(PatternConstant.UnstartedMatchPattern.FT_Draw_Under_15);
            var rgxFtW2U15 = new Regex(PatternConstant.UnstartedMatchPattern.FT_Win2_Under_15);
            var rgxFtW1O15 = new Regex(PatternConstant.UnstartedMatchPattern.FT_Win1_Over_15);
            var rgxFtXO15 = new Regex(PatternConstant.UnstartedMatchPattern.FT_Draw_Over_15);
            var rgxFtW2O15 = new Regex(PatternConstant.UnstartedMatchPattern.FT_Win2_Over_15);
            var rgxFtW1U25 = new Regex(PatternConstant.UnstartedMatchPattern.FT_Win1_Under_25);
            var rgxFtXU25 = new Regex(PatternConstant.UnstartedMatchPattern.FT_Draw_Under_25);
            var rgxFtW2U25 = new Regex(PatternConstant.UnstartedMatchPattern.FT_Win2_Under_25);
            var rgxFtW1O25 = new Regex(PatternConstant.UnstartedMatchPattern.FT_Win1_Over_25);
            var rgxFtXO25 = new Regex(PatternConstant.UnstartedMatchPattern.FT_Draw_Over_25);
            var rgxFtW2O25 = new Regex(PatternConstant.UnstartedMatchPattern.FT_Win2_Over_25);
            var rgxFtW1U35 = new Regex(PatternConstant.UnstartedMatchPattern.FT_Win1_Under_35);
            var rgxFtXU35 = new Regex(PatternConstant.UnstartedMatchPattern.FT_Draw_Under_35);
            var rgxFtW2U35 = new Regex(PatternConstant.UnstartedMatchPattern.FT_Win2_Under_35);
            var rgxFtW1O35 = new Regex(PatternConstant.UnstartedMatchPattern.FT_Win1_Over_35);
            var rgxFtXO35 = new Regex(PatternConstant.UnstartedMatchPattern.FT_Draw_Over_35);
            var rgxFtW2O35 = new Regex(PatternConstant.UnstartedMatchPattern.FT_Win2_Over_35);
            var rgxFtW1U45 = new Regex(PatternConstant.UnstartedMatchPattern.FT_Win1_Under_45);
            var rgxFtXU45 = new Regex(PatternConstant.UnstartedMatchPattern.FT_Draw_Under_45);
            var rgxFtW2U45 = new Regex(PatternConstant.UnstartedMatchPattern.FT_Win2_Under_45);
            var rgxFtW1O45 = new Regex(PatternConstant.UnstartedMatchPattern.FT_Win1_Over_45);
            var rgxFtXO45 = new Regex(PatternConstant.UnstartedMatchPattern.FT_Draw_Over_45);
            var rgxFtW2O45 = new Regex(PatternConstant.UnstartedMatchPattern.FT_Win2_Over_45);
            var rgxHand04W1 = new Regex(PatternConstant.UnstartedMatchPattern.Handicap_04_Win1);
            var rgxHand04X = new Regex(PatternConstant.UnstartedMatchPattern.Handicap_04_Draw);
            var rgxHand04W2 = new Regex(PatternConstant.UnstartedMatchPattern.Handicap_04_Win2);
            var rgxHand03W1 = new Regex(PatternConstant.UnstartedMatchPattern.Handicap_03_Win1);
            var rgxHand03X = new Regex(PatternConstant.UnstartedMatchPattern.Handicap_03_Draw);
            var rgxHand03W2 = new Regex(PatternConstant.UnstartedMatchPattern.Handicap_03_Win2);
            var rgxHand02W1 = new Regex(PatternConstant.UnstartedMatchPattern.Handicap_02_Win1);
            var rgxHand02X = new Regex(PatternConstant.UnstartedMatchPattern.Handicap_02_Draw);
            var rgxHand02W2 = new Regex(PatternConstant.UnstartedMatchPattern.Handicap_02_Win2);
            var rgxHand01W1 = new Regex(PatternConstant.UnstartedMatchPattern.Handicap_01_Win1);
            var rgxHand01X = new Regex(PatternConstant.UnstartedMatchPattern.Handicap_01_Draw);
            var rgxHand01W2 = new Regex(PatternConstant.UnstartedMatchPattern.Handicap_01_Win2);

            var rgxHand40W1 = new Regex(PatternConstant.UnstartedMatchPattern.Handicap_40_Win1);
            var rgxHand40X = new Regex(PatternConstant.UnstartedMatchPattern.Handicap_40_Draw);
            var rgxHand40W2 = new Regex(PatternConstant.UnstartedMatchPattern.Handicap_40_Win2);
            var rgxHand30W1 = new Regex(PatternConstant.UnstartedMatchPattern.Handicap_30_Win1);
            var rgxHand30X = new Regex(PatternConstant.UnstartedMatchPattern.Handicap_30_Draw);
            var rgxHand30W2 = new Regex(PatternConstant.UnstartedMatchPattern.Handicap_30_Win2);
            var rgxHand20W1 = new Regex(PatternConstant.UnstartedMatchPattern.Handicap_20_Win1);
            var rgxHand20X = new Regex(PatternConstant.UnstartedMatchPattern.Handicap_20_Draw);
            var rgxHand20W2 = new Regex(PatternConstant.UnstartedMatchPattern.Handicap_20_Win2);
            var rgxHand10W1 = new Regex(PatternConstant.UnstartedMatchPattern.Handicap_10_Win1);
            var rgxHand10X = new Regex(PatternConstant.UnstartedMatchPattern.Handicap_10_Draw);
            var rgxHand10W2 = new Regex(PatternConstant.UnstartedMatchPattern.Handicap_10_Win2);

            var rgxFtDouble1X = new Regex(PatternConstant.UnstartedMatchPattern.FT_Double_1_X);
            var rgxFtDouble12 = new Regex(PatternConstant.UnstartedMatchPattern.FT_Double_1_2);
            var rgxFtDoubleX2 = new Regex(PatternConstant.UnstartedMatchPattern.FT_Double_X_2);
            var rgx1stGoal1 = new Regex(PatternConstant.UnstartedMatchPattern.FirstGoal_Home);
            var rgx1stGoalNot = new Regex(PatternConstant.UnstartedMatchPattern.FirstGoal_None);
            var rgx1stGoal2 = new Regex(PatternConstant.UnstartedMatchPattern.FirstGoal_Away);
            var rgxFT45U = new Regex(PatternConstant.UnstartedMatchPattern.FT_4_5_Over);
            var rgxFT45A = new Regex(PatternConstant.UnstartedMatchPattern.FT_4_5_Under);
            var rgxFT55U = new Regex(PatternConstant.UnstartedMatchPattern.FT_5_5_Over);
            var rgxFT55A = new Regex(PatternConstant.UnstartedMatchPattern.FT_5_5_Under);
            var rgxHT05A = new Regex(PatternConstant.UnstartedMatchPattern.HT_0_5_Under);
            var rgxHT05U = new Regex(PatternConstant.UnstartedMatchPattern.HT_0_5_Over);
            var rgxHT25A = new Regex(PatternConstant.UnstartedMatchPattern.HT_2_5_Under);
            var rgxHT25U = new Regex(PatternConstant.UnstartedMatchPattern.HT_2_5_Over);

            var rgxHomeOv25 = new Regex(PatternConstant.UnstartedMatchPattern.Home_2_5_Over);
            var rgxHomeUn25 = new Regex(PatternConstant.UnstartedMatchPattern.Home_2_5_Under);
            var rgxAwayOv25 = new Regex(PatternConstant.UnstartedMatchPattern.Away_2_5_Over);
            var rgxAwayUn25 = new Regex(PatternConstant.UnstartedMatchPattern.Away_2_5_Under);
            var rgxHomeOv35 = new Regex(PatternConstant.UnstartedMatchPattern.Home_3_5_Over);
            var rgxHomeUn35 = new Regex(PatternConstant.UnstartedMatchPattern.Home_3_5_Under);
            var rgxAwayOv35 = new Regex(PatternConstant.UnstartedMatchPattern.Away_3_5_Over);
            var rgxAwayUn35 = new Regex(PatternConstant.UnstartedMatchPattern.Away_3_5_Under);
            var rgxHomeOv45 = new Regex(PatternConstant.UnstartedMatchPattern.Home_4_5_Over);
            var rgxHomeUn45 = new Regex(PatternConstant.UnstartedMatchPattern.Home_4_5_Under);
            var rgxAwayOv45 = new Regex(PatternConstant.UnstartedMatchPattern.Away_4_5_Over);
            var rgxAwayUn45 = new Regex(PatternConstant.UnstartedMatchPattern.Away_4_5_Under);

            var rgxEven = new Regex(PatternConstant.UnstartedMatchPattern.Even_Tek);
            var rgxOdd = new Regex(PatternConstant.UnstartedMatchPattern.Odd_Cut);

            var rgxScore_0_0 = new Regex(PatternConstant.UnstartedMatchPattern.Score_0_0);
            var rgxScore_1_0 = new Regex(PatternConstant.UnstartedMatchPattern.Score_1_0);
            var rgxScore_2_0 = new Regex(PatternConstant.UnstartedMatchPattern.Score_2_0);
            var rgxScore_3_0 = new Regex(PatternConstant.UnstartedMatchPattern.Score_3_0);
            var rgxScore_4_0 = new Regex(PatternConstant.UnstartedMatchPattern.Score_4_0);
            var rgxScore_5_0 = new Regex(PatternConstant.UnstartedMatchPattern.Score_5_0);
            var rgxScore_6_0 = new Regex(PatternConstant.UnstartedMatchPattern.Score_6_0);
            var rgxScore_0_1 = new Regex(PatternConstant.UnstartedMatchPattern.Score_0_1);
            var rgxScore_0_2 = new Regex(PatternConstant.UnstartedMatchPattern.Score_0_2);
            var rgxScore_0_3 = new Regex(PatternConstant.UnstartedMatchPattern.Score_0_3);
            var rgxScore_0_4 = new Regex(PatternConstant.UnstartedMatchPattern.Score_0_4);
            var rgxScore_0_5 = new Regex(PatternConstant.UnstartedMatchPattern.Score_0_5);
            var rgxScore_0_6 = new Regex(PatternConstant.UnstartedMatchPattern.Score_0_6);
            var rgxScore_1_1 = new Regex(PatternConstant.UnstartedMatchPattern.Score_1_1);
            var rgxScore_2_1 = new Regex(PatternConstant.UnstartedMatchPattern.Score_2_1);
            var rgxScore_3_1 = new Regex(PatternConstant.UnstartedMatchPattern.Score_3_1);
            var rgxScore_4_1 = new Regex(PatternConstant.UnstartedMatchPattern.Score_4_1);
            var rgxScore_5_1 = new Regex(PatternConstant.UnstartedMatchPattern.Score_5_1);
            var rgxScore_1_2 = new Regex(PatternConstant.UnstartedMatchPattern.Score_1_2);
            var rgxScore_1_3 = new Regex(PatternConstant.UnstartedMatchPattern.Score_1_3);
            var rgxScore_1_4 = new Regex(PatternConstant.UnstartedMatchPattern.Score_1_4);
            var rgxScore_1_5 = new Regex(PatternConstant.UnstartedMatchPattern.Score_1_5);
            var rgxScore_2_2 = new Regex(PatternConstant.UnstartedMatchPattern.Score_2_2);
            var rgxScore_3_2 = new Regex(PatternConstant.UnstartedMatchPattern.Score_3_2);
            var rgxScore_4_2 = new Regex(PatternConstant.UnstartedMatchPattern.Score_4_2);
            var rgxScore_2_3 = new Regex(PatternConstant.UnstartedMatchPattern.Score_2_3);
            var rgxScore_2_4 = new Regex(PatternConstant.UnstartedMatchPattern.Score_2_4);
            var rgxScore_3_3 = new Regex(PatternConstant.UnstartedMatchPattern.Score_3_3);
            var rgxScore_Other = new Regex(PatternConstant.UnstartedMatchPattern.Score_Other);

            var rgxMoreGoal1stHalf = new Regex(PatternConstant.UnstartedMatchPattern.MoreGoal_1st);
            var rgxMoreGoalEqual = new Regex(PatternConstant.UnstartedMatchPattern.MoreGoal_Equal);
            var rgxMoreGoal2ndHalf = new Regex(PatternConstant.UnstartedMatchPattern.MoreGoal_2nd);
            var rgxHtCorner45Ust = new Regex(PatternConstant.UnstartedMatchPattern.HT_Corners_4_5_Over); 
            var rgxHtCorner45Alt = new Regex(PatternConstant.UnstartedMatchPattern.HT_Corners_4_5_Under);
            var rgxFtCorner85Ust = new Regex(PatternConstant.UnstartedMatchPattern.Corners_8_5_Over);
            var rgxFtCorner85Alt = new Regex(PatternConstant.UnstartedMatchPattern.Corners_8_5_Under);
            var rgxFtCorner95Ust = new Regex(PatternConstant.UnstartedMatchPattern.Corners_9_5_Over);
            var rgxFtCorner95Alt = new Regex(PatternConstant.UnstartedMatchPattern.Corners_9_5_Under);
            var rgxFtCorner105Ust = new Regex(PatternConstant.UnstartedMatchPattern.Corners_10_5_Over);
            var rgxFtCorner105Alt = new Regex(PatternConstant.UnstartedMatchPattern.Corners_10_5_Under);

            var rgxFtMoreCornerHome = new Regex(PatternConstant.UnstartedMatchPattern.FT_MoreCorner_Home);
            var rgxFtMoreCornerEqual = new Regex(PatternConstant.UnstartedMatchPattern.FT_MoreCorner_Equal);
            var rgxFtMoreCornerAway = new Regex(PatternConstant.UnstartedMatchPattern.FT_MoreCorner_Away);
            var rgxHtMoreCornerHome = new Regex(PatternConstant.UnstartedMatchPattern.HT_MoreCorner_Home);
            var rgxHtMoreCornerEqual = new Regex(PatternConstant.UnstartedMatchPattern.HT_MoreCorner_Equal);
            var rgxHtMoreCornerAway = new Regex(PatternConstant.UnstartedMatchPattern.HT_MoreCorner_Away);

            var rgxFirstCornerHome = new Regex(PatternConstant.UnstartedMatchPattern.FirstCorner_Home);
            var rgxFirstCornerNever = new Regex(PatternConstant.UnstartedMatchPattern.FirstCorner_Never);
            var rgxFirstCornerAway = new Regex(PatternConstant.UnstartedMatchPattern.FirstCorner_Away);

            var rgxFtCornerRange08 = new Regex(PatternConstant.UnstartedMatchPattern.FT_Corners_Range_0_8);
            var rgxFtCornerRange911 = new Regex(PatternConstant.UnstartedMatchPattern.FT_Corners_Range_9_11);
            var rgxFtCornerRange12 = new Regex(PatternConstant.UnstartedMatchPattern.FT_Corners_Range_12);
            var rgxHtCornerRange04 = new Regex(PatternConstant.UnstartedMatchPattern.HT_Corners_Range_0_4);
            var rgxHtCornerRange56 = new Regex(PatternConstant.UnstartedMatchPattern.HT_Corners_Range_5_6);
            var rgxHtCornerRange7 = new Regex(PatternConstant.UnstartedMatchPattern.HT_Corners_Range_7);

            var rgxCard35Ov = new Regex(PatternConstant.UnstartedMatchPattern.Cards_3_5_Over);
            var rgxCard35Un = new Regex(PatternConstant.UnstartedMatchPattern.Cards_3_5_Under);
            var rgxCard45Ov = new Regex(PatternConstant.UnstartedMatchPattern.Cards_4_5_Over);
            var rgxCard45Un = new Regex(PatternConstant.UnstartedMatchPattern.Cards_4_5_Under);
            var rgxCard55Ov = new Regex(PatternConstant.UnstartedMatchPattern.Cards_5_5_Over);
            var rgxCard55Un = new Regex(PatternConstant.UnstartedMatchPattern.Cards_5_5_Under);



            // TODO : Delete
            var rgxFTRes = new Regex(PatternConstant.StartedMatchPattern.FTResultMatch);
            var rgxFTRes2ndCheck = new Regex(PatternConstant.StartedMatchPattern.FTResultMatch2ndCheck);

            T result = new T
            {
                Serial = serial,
                HT_W1 = source.ResolveOddByRegex(rgxH1),
                HT_X = source.ResolveOddByRegex(rgxHX),
                HT_W2 = source.ResolveOddByRegex(rgxH2),
                HT_Double_1X = source.ResolveOddByRegex(rgxHT_1X),
                HT_Double_12 = source.ResolveOddByRegex(rgxHT_12),
                HT_Double_X2 = source.ResolveOddByRegex(rgxHT_X2),
                Home_15_Over = source.ResolveOddByRegex(rgxHomeOv15),
                Home_15_Under = source.ResolveOddByRegex(rgxHomeUn15),
                Away_15_Over = source.ResolveOddByRegex(rgxAwayOv15),
                Away_15_Under = source.ResolveOddByRegex(rgxAwayUn15),
                SH_W1 = source.ResolveOddByRegex(rgxSH1),
                SH_X = source.ResolveOddByRegex(rgxSHX),
                SH_W2 = source.ResolveOddByRegex(rgxSH2),
                FT_W1 = source.ResolveOddByRegex(rgxF1),
                FT_X = source.ResolveOddByRegex(rgxFX),
                FT_W2 = source.ResolveOddByRegex(rgxF2),
                GG = source.ResolveOddByRegex(rgxGG),
                NG = source.ResolveOddByRegex(rgxNG),
                Goals01 = source.ResolveOddByRegex(rgxGoals01),
                Goals23 = source.ResolveOddByRegex(rgxGoals23),
                Goals45 = source.ResolveOddByRegex(rgxGoals45),
                Goals6 = source.ResolveOddByRegex(rgxGoals6),
                HT_15_Over = source.ResolveOddByRegex(rgxHT15U),
                HT_15_Under = source.ResolveOddByRegex(rgxHT15A),
                FT_15_Over = source.ResolveOddByRegex(rgxFT15U),
                FT_15_Under = source.ResolveOddByRegex(rgxFT15A),
                FT_25_Over = source.ResolveOddByRegex(rgxFT25U),
                FT_25_Under = source.ResolveOddByRegex(rgxFT25A),
                FT_35_Over = source.ResolveOddByRegex(rgxFT35U),
                FT_35_Under = source.ResolveOddByRegex(rgxFT35A),


                HT_FT_Home_Home = source.ResolveOddByRegex(rgxHtFt11),
                HT_FT_Home_Draw = source.ResolveOddByRegex(rgxHtFt1X),
                HT_FT_Home_Away = source.ResolveOddByRegex(rgxHtFt12),
                HT_FT_Draw_Home = source.ResolveOddByRegex(rgxHtFtX1),
                HT_FT_Draw_Draw = source.ResolveOddByRegex(rgxHtFtXX),
                HT_FT_Draw_Away = source.ResolveOddByRegex(rgxHtFtX2),
                HT_FT_Away_Home = source.ResolveOddByRegex(rgxHtFt21),
                HT_FT_Away_Draw = source.ResolveOddByRegex(rgxHtFt2X),
                HT_FT_Away_Away = source.ResolveOddByRegex(rgxHtFt22),
                FT_Win1_Under_15 = source.ResolveOddByRegex(rgxFtW1U15),
                FT_Draw_Under_15 = source.ResolveOddByRegex(rgxFtXU15),
                FT_Win2_Under_15 = source.ResolveOddByRegex(rgxFtW2U15),
                FT_Win1_Over_15 = source.ResolveOddByRegex(rgxFtW1O15),
                FT_Draw_Over_15 = source.ResolveOddByRegex(rgxFtXO15),
                FT_Win2_Over_15 = source.ResolveOddByRegex(rgxFtW2O15),
                FT_Win1_Under_25 = source.ResolveOddByRegex(rgxFtW1U25),
                FT_Draw_Under_25 = source.ResolveOddByRegex(rgxFtXU25),
                FT_Win2_Under_25 = source.ResolveOddByRegex(rgxFtW2U25),
                FT_Win1_Over_25 = source.ResolveOddByRegex(rgxFtW1O25),
                FT_Draw_Over_25 = source.ResolveOddByRegex(rgxFtXO25),
                FT_Win2_Over_25 = source.ResolveOddByRegex(rgxFtW2O25),
                FT_Win1_Under_35 = source.ResolveOddByRegex(rgxFtW1U35),
                FT_Draw_Under_35 = source.ResolveOddByRegex(rgxFtXU35),
                FT_Win2_Under_35 = source.ResolveOddByRegex(rgxFtW2U35),
                FT_Win1_Over_35 = source.ResolveOddByRegex(rgxFtW1O35),
                FT_Draw_Over_35 = source.ResolveOddByRegex(rgxFtXO35),
                FT_Win2_Over_35 = source.ResolveOddByRegex(rgxFtW2O35),
                FT_Win1_Under_45 = source.ResolveOddByRegex(rgxFtW1U45),
                FT_Draw_Under_45 = source.ResolveOddByRegex(rgxFtXU45),
                FT_Win2_Under_45 = source.ResolveOddByRegex(rgxFtW2U45),
                FT_Win1_Over_45 = source.ResolveOddByRegex(rgxFtW1O45),
                FT_Draw_Over_45 = source.ResolveOddByRegex(rgxFtXO45),
                FT_Win2_Over_45 = source.ResolveOddByRegex(rgxFtW2O45),
                Handicap_04_Win1 = source.ResolveOddByRegex(rgxHand04W1),
                Handicap_04_Draw = source.ResolveOddByRegex(rgxHand04X),
                Handicap_04_Win2 = source.ResolveOddByRegex(rgxHand04W2),
                Handicap_03_Win1 = source.ResolveOddByRegex(rgxHand03W1),
                Handicap_03_Draw = source.ResolveOddByRegex(rgxHand03X),
                Handicap_03_Win2 = source.ResolveOddByRegex(rgxHand03W2),
                Handicap_02_Win1 = source.ResolveOddByRegex(rgxHand02W1),
                Handicap_02_Draw = source.ResolveOddByRegex(rgxHand02X),
                Handicap_02_Win2 = source.ResolveOddByRegex(rgxHand02W2),
                Handicap_01_Win1 = source.ResolveOddByRegex(rgxHand01W1),
                Handicap_01_Draw = source.ResolveOddByRegex(rgxHand01X),
                Handicap_01_Win2 = source.ResolveOddByRegex(rgxHand01W2),
                Handicap_40_Win1 = source.ResolveOddByRegex(rgxHand40W1),
                Handicap_40_Draw = source.ResolveOddByRegex(rgxHand40X),
                Handicap_40_Win2 = source.ResolveOddByRegex(rgxHand40W2),
                Handicap_30_Win1 = source.ResolveOddByRegex(rgxHand30W1),
                Handicap_30_Draw = source.ResolveOddByRegex(rgxHand30X),
                Handicap_30_Win2 = source.ResolveOddByRegex(rgxHand30W2),
                Handicap_20_Win1 = source.ResolveOddByRegex(rgxHand20W1),
                Handicap_20_Draw = source.ResolveOddByRegex(rgxHand20X),
                Handicap_20_Win2 = source.ResolveOddByRegex(rgxHand20W2),
                Handicap_10_Win1 = source.ResolveOddByRegex(rgxHand10W1),
                Handicap_10_Draw = source.ResolveOddByRegex(rgxHand10X),
                Handicap_10_Win2 = source.ResolveOddByRegex(rgxHand10W2),
                FT_Double_1_X = source.ResolveOddByRegex(rgxFtDouble1X),
                FT_Double_1_2 = source.ResolveOddByRegex(rgxFtDouble12),
                FT_Double_X_2 = source.ResolveOddByRegex(rgxFtDoubleX2),
                FirstGoal_Home = source.ResolveOddByRegex(rgx1stGoal1),
                FirstGoal_None = source.ResolveOddByRegex(rgx1stGoalNot),
                FirstGoal_Away = source.ResolveOddByRegex(rgx1stGoal2),
                FT_4_5_Under = source.ResolveOddByRegex(rgxFT45A),
                FT_4_5_Over = source.ResolveOddByRegex(rgxFT45U),
                FT_5_5_Under = source.ResolveOddByRegex(rgxFT55A),
                FT_5_5_Over = source.ResolveOddByRegex(rgxFT55U),
                HT_0_5_Under = source.ResolveOddByRegex(rgxHT05A),
                HT_0_5_Over = source.ResolveOddByRegex(rgxHT05U),
                HT_2_5_Under = source.ResolveOddByRegex(rgxHT25A),
                HT_2_5_Over = source.ResolveOddByRegex(rgxHT25U),
                Home_2_5_Under = source.ResolveOddByRegex(rgxHomeUn25),
                Home_2_5_Over = source.ResolveOddByRegex(rgxHomeOv25),
                Home_3_5_Under = source.ResolveOddByRegex(rgxHomeUn35),
                Home_3_5_Over = source.ResolveOddByRegex(rgxHomeOv35),
                Home_4_5_Under = source.ResolveOddByRegex(rgxHomeUn45),
                Home_4_5_Over = source.ResolveOddByRegex(rgxHomeOv45),
                Away_2_5_Under = source.ResolveOddByRegex(rgxAwayUn25),
                Away_2_5_Over = source.ResolveOddByRegex(rgxAwayOv25),
                Away_3_5_Under = source.ResolveOddByRegex(rgxAwayUn35),
                Away_3_5_Over = source.ResolveOddByRegex(rgxAwayOv35),
                Away_4_5_Under = source.ResolveOddByRegex(rgxAwayUn45),
                Away_4_5_Over = source.ResolveOddByRegex(rgxAwayOv45),
                Even_Tek = source.ResolveOddByRegex(rgxEven),
                Odd_Cut = source.ResolveOddByRegex(rgxOdd),
                Score_0_0 = source.ResolveOddByRegex(rgxScore_0_0),
                Score_1_0 = source.ResolveOddByRegex(rgxScore_1_0),
                Score_2_0 = source.ResolveOddByRegex(rgxScore_2_0),
                Score_3_0 = source.ResolveOddByRegex(rgxScore_3_0),
                Score_4_0 = source.ResolveOddByRegex(rgxScore_4_0),
                Score_5_0 = source.ResolveOddByRegex(rgxScore_5_0),
                Score_6_0 = source.ResolveOddByRegex(rgxScore_6_0),
                Score_0_1 = source.ResolveOddByRegex(rgxScore_0_1),
                Score_0_2 = source.ResolveOddByRegex(rgxScore_0_2),
                Score_0_3 = source.ResolveOddByRegex(rgxScore_0_3),
                Score_0_4 = source.ResolveOddByRegex(rgxScore_0_4),
                Score_0_5 = source.ResolveOddByRegex(rgxScore_0_5),
                Score_0_6 = source.ResolveOddByRegex(rgxScore_0_6),
                Score_1_1 = source.ResolveOddByRegex(rgxScore_1_1),
                Score_2_1 = source.ResolveOddByRegex(rgxScore_2_1),
                Score_3_1 = source.ResolveOddByRegex(rgxScore_3_1),
                Score_4_1 = source.ResolveOddByRegex(rgxScore_4_1),
                Score_5_1 = source.ResolveOddByRegex(rgxScore_5_1),
                Score_1_2 = source.ResolveOddByRegex(rgxScore_1_2),
                Score_1_3 = source.ResolveOddByRegex(rgxScore_1_3),
                Score_1_4 = source.ResolveOddByRegex(rgxScore_1_4),
                Score_1_5 = source.ResolveOddByRegex(rgxScore_1_5),
                Score_2_2 = source.ResolveOddByRegex(rgxScore_2_2),
                Score_3_2 = source.ResolveOddByRegex(rgxScore_3_2),
                Score_4_2 = source.ResolveOddByRegex(rgxScore_4_2),
                Score_2_3 = source.ResolveOddByRegex(rgxScore_2_3),
                Score_2_4 = source.ResolveOddByRegex(rgxScore_2_4),
                Score_3_3 = source.ResolveOddByRegex(rgxScore_3_3),
                Score_Other = source.ResolveOddByRegex(rgxScore_Other),
                MoreGoal_1st = source.ResolveOddByRegex(rgxMoreGoal1stHalf),
                MoreGoal_Equal = source.ResolveOddByRegex(rgxMoreGoalEqual),
                MoreGoal_2nd = source.ResolveOddByRegex(rgxMoreGoal2ndHalf),
                HT_Corners_4_5_Over = source.ResolveOddByRegex(rgxHtCorner45Ust),
                HT_Corners_4_5_Under = source.ResolveOddByRegex(rgxHtCorner45Alt),
                Corners_8_5_Over = source.ResolveOddByRegex(rgxFtCorner85Ust),
                Corners_8_5_Under = source.ResolveOddByRegex(rgxFtCorner85Alt),
                Corners_9_5_Over = source.ResolveOddByRegex(rgxFtCorner95Ust),
                Corners_9_5_Under = source.ResolveOddByRegex(rgxFtCorner95Alt),
                Corners_10_5_Over = source.ResolveOddByRegex(rgxFtCorner105Ust),
                Corners_10_5_Under = source.ResolveOddByRegex(rgxFtCorner105Alt),
                FT_MoreCorner_Home = source.ResolveOddByRegex(rgxFtMoreCornerHome),
                FT_MoreCorner_Equal = source.ResolveOddByRegex(rgxFtMoreCornerEqual),
                FT_MoreCorner_Away = source.ResolveOddByRegex(rgxFtMoreCornerAway),
                HT_MoreCorner_Home = source.ResolveOddByRegex(rgxHtMoreCornerHome),
                HT_MoreCorner_Equal = source.ResolveOddByRegex(rgxHtMoreCornerEqual),
                HT_MoreCorner_Away = source.ResolveOddByRegex(rgxHtMoreCornerAway),
                FirstCorner_Home = source.ResolveOddByRegex(rgxFirstCornerHome),
                FirstCorner_Never = source.ResolveOddByRegex(rgxFirstCornerNever),
                FirstCorner_Away = source.ResolveOddByRegex(rgxFirstCornerAway),
                FT_Corners_Range_0_8 = source.ResolveOddByRegex(rgxFtCornerRange08),
                FT_Corners_Range_9_11 = source.ResolveOddByRegex(rgxFtCornerRange911),
                FT_Corners_Range_12 = source.ResolveOddByRegex(rgxFtCornerRange12),
                HT_Corners_Range_0_4 = source.ResolveOddByRegex(rgxHtCornerRange04),
                HT_Corners_Range_5_6 = source.ResolveOddByRegex(rgxHtCornerRange56),
                HT_Corners_Range_7 = source.ResolveOddByRegex(rgxHtCornerRange7),
                Cards_3_5_Over = source.ResolveOddByRegex(rgxCard35Ov),
                Cards_3_5_Under = source.ResolveOddByRegex(rgxCard35Un),
                Cards_4_5_Over = source.ResolveOddByRegex(rgxCard45Ov),
                Cards_4_5_Under = source.ResolveOddByRegex(rgxCard45Un),
                Cards_5_5_Over = source.ResolveOddByRegex(rgxCard55Ov),
                Cards_5_5_Under = source.ResolveOddByRegex(rgxCard55Un),

                Away = source.ResolveTextByRegex(rgxAway),
                Home = source.ResolveTextByRegex(rgxHome),
                DateMatch = source.ResolveTextByRegex(rgxDate),
                Country = source.ResolveCountryByRegex(containerTemp, rgxCountryLeagueMix, rgxCountry),
                League = source.ResolveLeagueByRegex(containerTemp, rgxCountryLeagueMix, rgxLeague),
                FT_Result = source.ResolveScoreByRegex(rgxFTRes, rgxFTRes2ndCheck)
            };

            return result;
        }

        private ComparisonInfoContainer GenerateComparisonInfoByRegex(string src)
        {
            var rgxHomeTeam = new Regex(PatternConstant.ComparisonInfoPattern.HomeTeam);
            var rgxAwayTeam = new Regex(PatternConstant.ComparisonInfoPattern.AwayTeam);
            var rgxHtRes = new Regex(PatternConstant.ComparisonInfoPattern.HT_Result);
            var rgxFtRes = new Regex(PatternConstant.ComparisonInfoPattern.FT_Result);
            var rgxCountry = new Regex(PatternConstant.ComparisonInfoPattern.CountryName);

            string htResult = src.ResolveScoreByRegex(rgxHtRes);
            string ftResult = src.ResolveScoreByRegex(rgxFtRes);

            if (string.IsNullOrEmpty(htResult) || string.IsNullOrEmpty(ftResult)) return null;

            var result = new ComparisonInfoContainer
            {
                HomeTeam = src.ResolveTextByRegex(rgxHomeTeam),
                AwayTeam = src.ResolveTextByRegex(rgxAwayTeam),
                CountryName = src.ResolveComparisonCountryByRegex(rgxCountry),
                HT_Goals_AwayTeam = Convert.ToInt32(htResult.Split('-')[1].Trim()),
                HT_Goals_HomeTeam = Convert.ToInt32(htResult.Split('-')[0].Trim()),
                FT_Goals_AwayTeam = Convert.ToInt32(ftResult.Split('-')[1].Trim()),
                FT_Goals_HomeTeam = Convert.ToInt32(ftResult.Split('-')[0].Trim())
            };

            return result;
        }

        private StandingInfoModel GenerateStandingInfoByRegex(string serial)
        {
            var uri = string.Format("{0}{1}", _defaultMatchUrl, serial);
            var src = _webOperation.GetMinifiedStringAsync(uri).Result;

            if (string.IsNullOrEmpty(src)) return null;

            var rgxUpTeamName = new Regex(PatternConstant.StandingInfoPattern.UpTeam.Team);
            var rgxUpOrder = new Regex(PatternConstant.StandingInfoPattern.UpTeam.Order);
            var rgxUpMatchesCount = new Regex(PatternConstant.StandingInfoPattern.UpTeam.PlayedMatchesCount);
            var rgxUpWinsCount = new Regex(PatternConstant.StandingInfoPattern.UpTeam.WinsCount);
            var rgxUpDrawsCount = new Regex(PatternConstant.StandingInfoPattern.UpTeam.DrawsCount);
            var rgxUpLostsCount = new Regex(PatternConstant.StandingInfoPattern.UpTeam.LostsCount);
            var rgxUpPoints = new Regex(PatternConstant.StandingInfoPattern.UpTeam.Ponints);

            var rgxDownTeamName = new Regex(PatternConstant.StandingInfoPattern.DownTeam.Team);
            var rgxDownOrder = new Regex(PatternConstant.StandingInfoPattern.DownTeam.Order);
            var rgxDownMatchesCount = new Regex(PatternConstant.StandingInfoPattern.DownTeam.PlayedMatchesCount);
            var rgxDownWinsCount = new Regex(PatternConstant.StandingInfoPattern.DownTeam.WinsCount);
            var rgxDownDrawsCount = new Regex(PatternConstant.StandingInfoPattern.DownTeam.DrawsCount);
            var rgxDownLostsCount = new Regex(PatternConstant.StandingInfoPattern.DownTeam.LostsCount);
            var rgxDownPoints = new Regex(PatternConstant.StandingInfoPattern.DownTeam.Ponints);

            try
            {
                var result = new StandingInfoModel
                {
                    UpTeam = new StandingDetail
                    {
                        TeamName = src.ResolveTextByRegex(rgxUpTeamName),
                        Order = Convert.ToInt32(src.ResolveTextByRegex(rgxUpOrder)),
                        MatchesCount = Convert.ToInt32(src.ResolveTextByRegex(rgxUpMatchesCount)),
                        WinsCount = Convert.ToInt32(src.ResolveTextByRegex(rgxUpWinsCount)),
                        DrawsCount = Convert.ToInt32(src.ResolveTextByRegex(rgxUpDrawsCount)),
                        LostsCount = Convert.ToInt32(src.ResolveTextByRegex(rgxUpLostsCount)),
                        Point = Convert.ToInt32(src.ResolveTextByRegex(rgxUpPoints))
                    },

                    DownTeam = new StandingDetail
                    {
                        TeamName = src.ResolveTextByRegex(rgxDownTeamName),
                        Order = Convert.ToInt32(src.ResolveTextByRegex(rgxDownOrder)),
                        MatchesCount = Convert.ToInt32(src.ResolveTextByRegex(rgxDownMatchesCount)),
                        WinsCount = Convert.ToInt32(src.ResolveTextByRegex(rgxDownWinsCount)),
                        DrawsCount = Convert.ToInt32(src.ResolveTextByRegex(rgxDownDrawsCount)),
                        LostsCount = Convert.ToInt32(src.ResolveTextByRegex(rgxDownLostsCount)),
                        Point = Convert.ToInt32(src.ResolveTextByRegex(rgxDownPoints))
                    }
                };

                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
