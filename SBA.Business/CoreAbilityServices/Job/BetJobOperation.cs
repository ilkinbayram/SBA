using Core.Entities.Concrete;
using Core.Extensions;
using Core.Resources.Constants;
using Core.Resources.Enums;
using Core.Utilities.Maintenance.Abstract;
using Core.Utilities.Results;
using Core.Utilities.UsableModel;
using Core.Utilities.UsableModel.TempTableModels.Country;
using Core.Utilities.UsableModel.TempTableModels.Initialization;
using Core.Utilities.UsableModel.Test;
using Core.Utilities.UsableModel.Visualisers;
using Core.Utilities.UsableModel.Visualisers.SeparatedMessager;
using Newtonsoft.Json;
using SBA.Business.Abstract;
using SBA.Business.BusinessHelper;
using SBA.Business.ExternalServices.Abstract;
using SBA.Business.FunctionalServices.Concrete;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Timers;

namespace SBA.Business.CoreAbilityServices.Job
{
    public class AwayHomeSideBet
    {
        public string TeamAt { get; set; }
        public string Score { get; set; }
        public string AwayTeam { get; set; }
        public string HomeTeam { get; set; }
        public bool Is25Over { get; set; }
        public bool Is15Over { get; set; }
        public bool Is35Over { get; set; }
        public bool IsGG { get; set; }
        public bool IsWin { get; set; }
        public bool IsLost { get; set; }
    }

    public class BetResultModel
    {
        public string Serial { get; set; }
        public string Match { get; set; }
        public string Country { get; set; }
        public string League { get; set; }
        public bool IsFT_25Over { get; set; }
        public bool IsFT_15Over { get; set; }
        public bool IsFT_GG { get; set; }
        public bool IsHT_05Over { get; set; }
        public bool IsSH_05Over { get; set; }
        public bool IsFT_X2 { get; set; }
        public bool IsFT_X1 { get; set; }
        public bool IsHT_X2 { get; set; }
        public bool IsHT_X1 { get; set; }
        public bool IsFT_35Under { get; set; }
        public bool IsHT_15Under { get; set; }
    }

    public class BetJobOperation : BaseJobOperation
    {
        private readonly ISocialBotMessagingService _botService;
        private List<TimeSerialContainer> _timeSerials;
        private List<TimeSerialContainer> _analysableSerials = new List<TimeSerialContainer>();
        private readonly IMatchBetService _matchBetService;
        private readonly IFilterResultService _filterResultService;
        private readonly SystemCheckerContainer _systemCheckerContainer;
        private readonly DescriptionJobResultEnum _descriptionJobResultEnum;
        private int _addMinute;
        private readonly HttpClient _client;
        private readonly WebOperation _webHelper;

        private CountryContainerTemp _containerTemp;

        public BetJobOperation(ISocialBotMessagingService botService,
                            List<TimeSerialContainer> timeSerials,
                            IMatchBetService matchBetService,
                            IFilterResultService filterResultService,
                            SystemCheckerContainer systemCheckerContainer,
                            DescriptionJobResultEnum descriptionJobResultEnum,
                            CountryContainerTemp containerTemp,
                            int addMinute = 3)
        {
            _matchBetService = matchBetService;
            _filterResultService = filterResultService;
            _systemCheckerContainer = systemCheckerContainer;
            _addMinute = addMinute;
            _botService = botService;
            _timeSerials = timeSerials;
            _descriptionJobResultEnum = descriptionJobResultEnum;
            _containerTemp = containerTemp;
            _client = new HttpClient();
            _webHelper = new WebOperation();
        }

        private void InitialiseTimeContainer()
        {
            _analysableSerials = _timeSerials.MapToNewListTimeSerials(_systemCheckerContainer.IsAnalyseAnyTime);

            _analysableSerials.ForEach(x =>
            {
                bool checkedResult = CompareIsAnalysable(x.Time, _addMinute, _systemCheckerContainer.IsAnalyseAnyTime);
                if (checkedResult) x.IsSelected = true;
            });

            _analysableSerials = _analysableSerials.Where(x => x.IsSelected).ToList();
        }

        public override void Execute()
        {
            InitialiseTimeContainer();

            _analysableSerials.ForEach(x =>
            {
                var serialItemServer = _timeSerials.FirstOrDefault(p => p.Serial == x.Serial);
                serialItemServer.IsAnalysed = true;
            });

            _systemCheckerContainer.SerialsBeforeGenerated = _analysableSerials.Select(x => x.Serial).ToList();

            var responseProfiler = OperationalProcessor.GetJobAnalyseModelResult(_systemCheckerContainer, _matchBetService, _filterResultService, _containerTemp);

            List<AnalyseResultVisualiser> visualiserModelList = new List<AnalyseResultVisualiser>();

            if (responseProfiler != null && responseProfiler.Count > 0)
            {
                visualiserModelList = OperationalProcessor.GetDataVisualisers(responseProfiler, 1);
            }

            var jsonNullIgnorer = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            visualiserModelList.ForEach(x =>
            {
                x.ExtraDetail = _descriptionJobResultEnum.ToString();

                var separatedOddVisualiser = new SeparatedOddVisualiser
                {
                    ExtraDetail = x.ExtraDetail,
                    HomeTeamVsAwayTeam = x.HomeTeamVsAwayTeam,
                    TargetURL = x.TargetURL,
                    ODD_PERCENTAGE_Visualiser = x.ODD_PERCENTAGE_Visualiser,
                    AVERAGE_Visualiser = x.AVERAGE_Visualiser
                };

                var separatedTableVisualiser = new SeparatedTableVisualiser
                {
                    TABLE_Visualiser = x.TABLE_Visualiser
                };


                string analyseOddMessage = JsonConvert.SerializeObject(separatedOddVisualiser, Formatting.Indented, jsonNullIgnorer);
                string analyseTableMessage = JsonConvert.SerializeObject(separatedTableVisualiser, Formatting.Indented, jsonNullIgnorer);

                _botService.SendMessage(analyseOddMessage);
                _botService.SendMessage(analyseTableMessage);
            });

            _analysableSerials.Clear();

            base.Execute();
        }


        public override void ExecuteTTT2(List<string> serials, string path, LeagueContainer league, CountryContainerTemp countryContainer)
        {
            using (var sr = new StreamReader(path))
            {
                string res = sr.ReadToEnd();
                serials = res.Split("\n").Select(x => x.Trim()).ToList();
            }

            var obj = new List<object>();

            List<JobAnalyseModel> responseProfiler = OperationalProcessor.GetJobAnalyseModelResultTest2222(_matchBetService, _containerTemp, serials).Where(x => x.AverageProfiler != null && x.AverageProfilerHomeAway != null).ToList();

            List<object> listsRes = new List<object>();

            List<object> odds = new List<object>();
            var rgxOdd35U = new Regex(PatternConstant.StartedMatchPattern.FT_2_5_Over);
            var rgxLeague = new Regex(PatternConstant.StartedMatchPattern.League);
            var rgxCountry = new Regex(PatternConstant.StartedMatchPattern.Country);
            var rgxLeague2 = new Regex(PatternConstant.StartedMatchPattern.CountryAndLeague);

            var responsesBet = new List<BetResultModel>();

            int iteration = 0;

            foreach (var item in responseProfiler)
            {
                iteration++;

                var contentString = _webHelper.GetMinifiedString($"https://arsiv.mackolik.com/Match/Default.aspx?id={item.HomeTeam_FormPerformanceGuessContainer.Serial}#karsilastirma");
                string leagueName = contentString.ResolveLeagueByRegex(countryContainer, rgxLeague, rgxLeague2);
                string countryName = contentString.ResolveCountryByRegex(countryContainer, rgxCountry, rgxLeague2);
                var leagueHolder = league.LeagueHolders.FirstOrDefault(x => x.Country == countryName && x.League == leagueName);

                if (leagueHolder != null)
                {
                    var match = new BetResultModel
                    {
                        Serial = item.ComparisonInfoContainer.Serial,
                        Country = countryName,
                        League = leagueName,
                        Match = $"{item.ComparisonInfoContainer.Home} - {item.ComparisonInfoContainer.Away}",
                        IsFT_25Over = CheckFT25Over(item, contentString, leagueHolder),
                        IsFT_15Over = CheckFT15Over(item, contentString, leagueHolder),
                        IsFT_35Under = CheckFT35Under(item, contentString, leagueHolder),
                        IsHT_15Under = CheckHT15Under(item, leagueHolder),
                        IsHT_05Over = CheckHT05Over(item, leagueHolder),
                        IsSH_05Over = CheckSH05Over(item, leagueHolder),
                        IsFT_GG = CheckFT_GG(item, contentString, leagueHolder),
                        IsFT_X1 = CheckFT_X1(item, contentString, leagueHolder),
                        IsFT_X2 = CheckFT_X2(item, contentString, leagueHolder),
                        IsHT_X1 = CheckHT_X1(item, contentString, leagueHolder),
                        IsHT_X2 = CheckHT_X2(item, contentString, leagueHolder)
                    };

                    if (CheckIsOk(match))
                        responsesBet.Add(match);
                }

                obj.Add(new { League = league.LeagueHolders.FirstOrDefault(x => x.Country == item.ComparisonInfoContainer.CountryName && x.League == leagueName), Response = item });
            }

            foreach (var bet in responsesBet)
            {
                var strBuilder = new StringBuilder();
                strBuilder.Append("BET Nəticəsi\n");
                strBuilder.Append($"Link: https://arsiv.mackolik.com/Match/Default.aspx?id={bet.Serial}\n");
                strBuilder.Append($"___________________________________\n");
                strBuilder.Append($"Ölkə: {bet.Country}\n");
                strBuilder.Append($"Liqa: {bet.League}\n");
                strBuilder.Append($"Matç: {bet.Match}\n\n");
                strBuilder.Append($"=================================\n");
                if (bet.IsHT_15Under) strBuilder.Append($"Təxmin:  HT 1,5 ALT\n");
                if (bet.IsFT_25Over) strBuilder.Append($"Təxmin:  FT 2,5 ÜST\n");
                if (bet.IsFT_35Under) strBuilder.Append($"Təxmin:  FT 3,5 ALT\n");
                if (bet.IsFT_GG) strBuilder.Append($"Təxmin:  FT QOL-QOL\n");
                if (bet.IsHT_05Over) strBuilder.Append($"Təxmin:  HT 0,5 ÜST\n");
                if (bet.IsSH_05Over) strBuilder.Append($"Təxmin:  İH 0,5 ÜST\n");
                if (bet.IsFT_15Over) strBuilder.Append($"Təxmin:  FT 1,5 ÜST\n");
                if (bet.IsFT_X1) strBuilder.Append($"Təxmin:  FT CŞ 1-X\n");
                if (bet.IsFT_X2) strBuilder.Append($"Təxmin:  FT CŞ X-2\n");
                if (bet.IsHT_X1) strBuilder.Append($"Təxmin:  HT CŞ 1-X\n");
                if (bet.IsHT_X2) strBuilder.Append($"Təxmin:  HT CŞ X-2\n");
                strBuilder.Append($"=================================\n");

                _botService.SendMessage(strBuilder.ToString());
            }

            base.ExecuteTTT2(serials, path, league, countryContainer);
        }


        #region BetResult Checker Area
        private bool CheckIsOk(BetResultModel betResult)
        {
            return betResult.IsFT_15Over ||
                   betResult.IsFT_25Over ||
                   betResult.IsFT_GG ||
                   betResult.IsFT_35Under ||
                   betResult.IsFT_X1 ||
                   betResult.IsFT_X2 ||
                   betResult.IsHT_05Over ||
                   betResult.IsHT_15Under ||
                   betResult.IsHT_X1 ||
                   betResult.IsHT_X2 ||
                   betResult.IsSH_05Over;
        }
        private bool CheckFT25Over(JobAnalyseModel item, string contentString, LeagueHolder leagueHolder)
        {
            bool result = false;

            try
            {
                bool isValidImportant = leagueHolder.Over_2_5_Percentage < item.AverageProfilerHomeAway.FT_25_Over.Percentage &&
                                        item.AverageProfilerHomeAway.FT_25_Over.FeatureName.ToLower() == "true" &&
                                        leagueHolder.Over_2_5_Percentage < item.AverageProfiler.FT_25_Over.Percentage &&
                                        item.AverageProfiler.FT_25_Over.FeatureName.ToLower() == "true" &&
                                        leagueHolder.GoalsAverage > (decimal)2.5;

                if (!isValidImportant) return isValidImportant;

                bool isValid =
                item.AverageProfilerHomeAway.FT_25_Over.Percentage > 60 &&
                item.AverageProfilerHomeAway.FT_25_Over.FeatureName.ToLower() == "true" &&
                item.AverageProfiler.FT_25_Over.Percentage > 60 &&
                item.AverageProfiler.FT_25_Over.FeatureName.ToLower() == "true";

                bool isValid2 =
                    item.AverageProfiler.Average_FT_Goals_HomeTeam >= (decimal)1.55 &&
                    item.AverageProfiler.Average_FT_Goals_AwayTeam >= (decimal)1.55 &&
                    item.AverageProfilerHomeAway.Average_FT_Goals_HomeTeam >= (decimal)1.55 &&
                    item.AverageProfilerHomeAway.Average_FT_Goals_AwayTeam >= (decimal)1.55;

                bool isValid3 =
                    item.AverageProfiler.Average_FT_Goals_HomeTeam >= (decimal)2.75 &&
                    item.AverageProfiler.Average_FT_Goals_AwayTeam >= (decimal)2.75 &&
                    item.AverageProfilerHomeAway.Average_FT_Goals_HomeTeam > (decimal)1.00 &&
                    item.AverageProfilerHomeAway.Average_FT_Goals_AwayTeam > (decimal)1.00;

                bool isValid4 =
                    item.AverageProfiler.Average_FT_Goals_HomeTeam > (decimal)1.00 &&
                    item.AverageProfiler.Average_FT_Goals_AwayTeam > (decimal)1.00 &&
                    item.AverageProfilerHomeAway.Average_FT_Goals_HomeTeam >= (decimal)2.75 &&
                    item.AverageProfilerHomeAway.Average_FT_Goals_AwayTeam >= (decimal)2.75;

                if (isValid || isValid2 || isValid3 || isValid4)
                {
                    var listContent = contentString.Split("last-games").Where(x => x.Trim().StartsWith("title")).ToList();

                    List<AwayHomeSideBet> homeSide = new List<AwayHomeSideBet>();
                    List<AwayHomeSideBet> awaySide = new List<AwayHomeSideBet>();
                    var rgx = new Regex(PatternConstant.RegSrcMix.ScoreFromPerformance);
                    var rgxTeams = new Regex(PatternConstant.RegSrcMix.TeamsCollector);

                    var firstlst = new List<string>();
                    var secndlst = new List<string>();

                    for (var i = 0; i < 5; i++)
                    {
                        var resScore = rgx.Matches(listContent[i])[0].Groups[0].Value;
                        var resTeams = rgxTeams.Matches(listContent[i])[0].Groups[1].Value;
                        foreach (var team in resTeams.Split(resScore))
                        {
                            firstlst.Add(team.Trim());
                        }
                    }

                    for (var i = 5; i < 10; i++)
                    {
                        var resScore = rgx.Matches(listContent[i])[0].Groups[0].Value;
                        var resTeams = rgxTeams.Matches(listContent[i])[0].Groups[1].Value;
                        foreach (var team in resTeams.Split(resScore))
                        {
                            secndlst.Add(team.Trim());
                        }
                    }

                    var homeName = firstlst.GroupBy(x => x).Select(p => new { Count = p.Count(), Name = p.Key }).OrderByDescending(x => x.Count).ToList()[0].Name;
                    var awayName = secndlst.GroupBy(x => x).Select(p => new { Count = p.Count(), Name = p.Key }).OrderByDescending(x => x.Count).ToList()[0].Name;

                    for (var i = 0; i < 5; i++)
                    {
                        var resScore = rgx.Matches(listContent[i])[0].Groups[0].Value;
                        var resTeams = rgxTeams.Matches(listContent[i])[0].Groups[1].Value;
                        homeSide.Add(new AwayHomeSideBet
                        {
                            TeamAt = resTeams.Split(resScore)[0].Trim() == homeName.Trim() ? "Home" : "Away",
                            Score = resScore,
                            HomeTeam = resTeams.Split(resScore)[0].Trim(),
                            AwayTeam = resTeams.Split(resScore)[1].Trim(),
                            Is25Over = Convert.ToInt32(resScore.Split("-")[0].Trim()) + Convert.ToInt32(resScore.Split("-")[1].Trim()) > 2,
                            IsWin = resTeams.Split(resScore)[0].Trim() == homeName.Trim()
                                  ? Convert.ToInt32(resScore.Split("-")[0].Trim()) > Convert.ToInt32(resScore.Split("-")[1].Trim())
                                  : Convert.ToInt32(resScore.Split("-")[0].Trim()) < Convert.ToInt32(resScore.Split("-")[1].Trim()),
                            IsLost = resTeams.Split(resScore)[0].Trim() == homeName.Trim()
                                  ? Convert.ToInt32(resScore.Split("-")[0].Trim()) < Convert.ToInt32(resScore.Split("-")[1].Trim())
                                  : Convert.ToInt32(resScore.Split("-")[0].Trim()) > Convert.ToInt32(resScore.Split("-")[1].Trim()),
                        });
                    }

                    for (var i = 5; i < 10; i++)
                    {
                        var resScore = rgx.Matches(listContent[i])[0].Groups[0].Value;
                        var resTeams = rgxTeams.Matches(listContent[i])[0].Groups[1].Value;
                        awaySide.Add(new AwayHomeSideBet
                        {
                            TeamAt = resTeams.Split(resScore)[0].Trim() == awayName.Trim() ? "Home" : "Away",
                            Score = resScore,
                            HomeTeam = resTeams.Split(resScore)[0].Trim(),
                            AwayTeam = resTeams.Split(resScore)[1].Trim(),
                            Is25Over = Convert.ToInt32(resScore.Split("-")[0].Trim()) + Convert.ToInt32(resScore.Split("-")[1].Trim()) > 2,
                            IsWin = resTeams.Split(resScore)[0].Trim() == homeName.Trim()
                                  ? Convert.ToInt32(resScore.Split("-")[0].Trim()) > Convert.ToInt32(resScore.Split("-")[1].Trim())
                                  : Convert.ToInt32(resScore.Split("-")[0].Trim()) < Convert.ToInt32(resScore.Split("-")[1].Trim()),
                            IsLost = resTeams.Split(resScore)[0].Trim() == homeName.Trim()
                                  ? Convert.ToInt32(resScore.Split("-")[0].Trim()) < Convert.ToInt32(resScore.Split("-")[1].Trim())
                                  : Convert.ToInt32(resScore.Split("-")[0].Trim()) > Convert.ToInt32(resScore.Split("-")[1].Trim()),
                        });
                    }

                    bool condTicketMix1 = awaySide[awaySide.Count - 1].Is25Over != homeSide[homeSide.Count - 1].Is25Over;

                    bool condTicketMix2 = false;
                    if (awaySide[awaySide.Count - 1].IsWin && homeSide[homeSide.Count - 1].IsLost)
                    {
                        condTicketMix2 = true;
                    }
                    else if(awaySide[awaySide.Count - 1].IsLost && homeSide[homeSide.Count - 1].IsWin)
                    {
                        condTicketMix2 = true;
                    }

                    bool mixResult = condTicketMix1 && condTicketMix2;

                    bool condTicket2 = !awaySide[awaySide.Count - 1].Is25Over && !awaySide[awaySide.Count - 2].Is25Over && !homeSide[homeSide.Count - 1].Is25Over && !homeSide[homeSide.Count - 2].Is25Over;

                    result = mixResult || condTicket2;
                }

                return result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private bool CheckFT15Over(JobAnalyseModel item, string contentString, LeagueHolder leagueHolder)
        {
            bool result = false;

            try
            {
                bool isValidImportant = leagueHolder.Over_1_5_Percentage < item.AverageProfilerHomeAway.FT_15_Over.Percentage &&
                                        item.AverageProfilerHomeAway.FT_15_Over.FeatureName.ToLower() == "true" &&
                                        leagueHolder.Over_1_5_Percentage < item.AverageProfiler.FT_15_Over.Percentage &&
                                        item.AverageProfiler.FT_15_Over.FeatureName.ToLower() == "true" &&
                                        leagueHolder.GoalsAverage > (decimal)2.3;

                if (!isValidImportant) return isValidImportant;

                bool isValid =
                item.AverageProfilerHomeAway.FT_15_Over.Percentage > 80 &&
                item.AverageProfilerHomeAway.FT_15_Over.FeatureName.ToLower() == "true" &&
                item.AverageProfiler.FT_15_Over.Percentage > 80 &&
                item.AverageProfiler.FT_15_Over.FeatureName.ToLower() == "true";

                bool isValid2 =
                    item.AverageProfiler.Average_FT_Goals_HomeTeam > (decimal)1.25 &&
                    item.AverageProfiler.Average_FT_Goals_AwayTeam > (decimal)1.25 &&
                    item.AverageProfilerHomeAway.Average_FT_Goals_HomeTeam > (decimal)1.25 &&
                    item.AverageProfilerHomeAway.Average_FT_Goals_AwayTeam > (decimal)1.25;

                bool isValid3 =
                    item.AverageProfiler.Average_FT_Goals_HomeTeam > (decimal)1.00 &&
                    item.AverageProfiler.Average_FT_Goals_AwayTeam > (decimal)1.55 &&
                    item.AverageProfilerHomeAway.Average_FT_Goals_HomeTeam > (decimal)1.00 &&
                    item.AverageProfilerHomeAway.Average_FT_Goals_AwayTeam > (decimal)1.55;

                bool isValid4 =
                    item.AverageProfiler.Average_FT_Goals_HomeTeam > (decimal)1.55 &&
                    item.AverageProfiler.Average_FT_Goals_AwayTeam > (decimal)1.00 &&
                    item.AverageProfilerHomeAway.Average_FT_Goals_HomeTeam > (decimal)1.55 &&
                    item.AverageProfilerHomeAway.Average_FT_Goals_AwayTeam > (decimal)1.00;

                if (isValid || isValid2 || isValid3 || isValid4)
                {
                    var listContent = contentString.Split("last-games").Where(x => x.Trim().StartsWith("title")).ToList();

                    List<AwayHomeSide> homeSide = new List<AwayHomeSide>();
                    List<AwayHomeSide> awaySide = new List<AwayHomeSide>();
                    var rgx = new Regex(PatternConstant.RegSrcMix.ScoreFromPerformance);
                    var rgxTeams = new Regex(PatternConstant.RegSrcMix.TeamsCollector);

                    var firstlst = new List<string>();
                    var secndlst = new List<string>();

                    for (var i = 0; i < 5; i++)
                    {
                        var resScore = rgx.Matches(listContent[i])[0].Groups[0].Value;
                        var resTeams = rgxTeams.Matches(listContent[i])[0].Groups[1].Value;
                        foreach (var team in resTeams.Split(resScore))
                        {
                            firstlst.Add(team.Trim());
                        }
                    }

                    for (var i = 5; i < 10; i++)
                    {
                        var resScore = rgx.Matches(listContent[i])[0].Groups[0].Value;
                        var resTeams = rgxTeams.Matches(listContent[i])[0].Groups[1].Value;
                        foreach (var team in resTeams.Split(resScore))
                        {
                            secndlst.Add(team.Trim());
                        }
                    }

                    var homeName = firstlst.GroupBy(x => x).Select(p => new { Count = p.Count(), Name = p.Key }).OrderByDescending(x => x.Count).ToList()[0].Name;
                    var awayName = secndlst.GroupBy(x => x).Select(p => new { Count = p.Count(), Name = p.Key }).OrderByDescending(x => x.Count).ToList()[0].Name;

                    for (var i = 0; i < 5; i++)
                    {
                        var resScore = rgx.Matches(listContent[i])[0].Groups[0].Value;
                        var resTeams = rgxTeams.Matches(listContent[i])[0].Groups[1].Value;
                        homeSide.Add(new AwayHomeSide
                        {
                            TeamAt = resTeams.Split(resScore)[0].Trim() == homeName.Trim() ? "Home" : "Away",
                            Score = resScore,
                            HomeTeam = resTeams.Split(resScore)[0].Trim(),
                            AwayTeam = resTeams.Split(resScore)[1].Trim(),
                            Is15Over = Convert.ToInt32(resScore.Split("-")[0].Trim()) + Convert.ToInt32(resScore.Split("-")[1].Trim()) > 1,
                        });
                    }

                    for (var i = 5; i < 10; i++)
                    {
                        var resScore = rgx.Matches(listContent[i])[0].Groups[0].Value;
                        var resTeams = rgxTeams.Matches(listContent[i])[0].Groups[1].Value;
                        awaySide.Add(new AwayHomeSide
                        {
                            TeamAt = resTeams.Split(resScore)[0].Trim() == awayName.Trim() ? "Home" : "Away",
                            Score = resScore,
                            HomeTeam = resTeams.Split(resScore)[0].Trim(),
                            AwayTeam = resTeams.Split(resScore)[1].Trim(),
                            Is15Over = Convert.ToInt32(resScore.Split("-")[0].Trim()) + Convert.ToInt32(resScore.Split("-")[1].Trim()) > 1,
                        });
                    }

                    bool condTicket1 = !awaySide[awaySide.Count - 1].Is15Over && !homeSide[homeSide.Count - 1].Is15Over;

                    result = condTicket1;
                }

                return result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private bool CheckFT35Under(JobAnalyseModel item, string contentString, LeagueHolder leagueHolder)
        {
            bool result = false;

            try
            {
                bool isValidImportant =
                        leagueHolder.Over_3_5_Percentage > item.AverageProfiler.FT_35_Over.Percentage &&
                        item.AverageProfiler.FT_35_Over.FeatureName.ToLower() == "true" &&
                        leagueHolder.Over_3_5_Percentage > item.AverageProfilerHomeAway.FT_35_Over.Percentage &&
                        item.AverageProfilerHomeAway.FT_35_Over.FeatureName.ToLower() == "true" &&
                        leagueHolder.GoalsAverage < (decimal)2.2;


                if (!isValidImportant) return isValidImportant;

                bool isValid =
                item.AverageProfilerHomeAway.FT_35_Over.Percentage > 60 &&
                item.AverageProfilerHomeAway.FT_35_Over.FeatureName.ToLower() == "false" &&
                item.AverageProfiler.FT_35_Over.Percentage > 60 &&
                item.AverageProfiler.FT_35_Over.FeatureName.ToLower() == "false";

                bool isValid2 =
                    item.AverageProfiler.Average_FT_Goals_HomeTeam < (decimal)1.4 &&
                    item.AverageProfiler.Average_FT_Goals_AwayTeam < (decimal)1.4 &&
                    item.AverageProfilerHomeAway.Average_FT_Goals_HomeTeam < (decimal)1.4 &&
                    item.AverageProfilerHomeAway.Average_FT_Goals_AwayTeam < (decimal)1.4;

                if (isValid || isValid2)
                {
                    var listContent = contentString.Split("last-games").Where(x => x.Trim().StartsWith("title")).ToList();

                    List<AwayHomeSideBet> homeSide = new List<AwayHomeSideBet>();
                    List<AwayHomeSideBet> awaySide = new List<AwayHomeSideBet>();
                    var rgx = new Regex(PatternConstant.RegSrcMix.ScoreFromPerformance);
                    var rgxTeams = new Regex(PatternConstant.RegSrcMix.TeamsCollector);

                    var firstlst = new List<string>();
                    var secndlst = new List<string>();

                    for (var i = 0; i < 5; i++)
                    {
                        var resScore = rgx.Matches(listContent[i])[0].Groups[0].Value;
                        var resTeams = rgxTeams.Matches(listContent[i])[0].Groups[1].Value;
                        foreach (var team in resTeams.Split(resScore))
                        {
                            firstlst.Add(team.Trim());
                        }
                    }

                    for (var i = 5; i < 10; i++)
                    {
                        var resScore = rgx.Matches(listContent[i])[0].Groups[0].Value;
                        var resTeams = rgxTeams.Matches(listContent[i])[0].Groups[1].Value;
                        foreach (var team in resTeams.Split(resScore))
                        {
                            secndlst.Add(team.Trim());
                        }
                    }

                    var homeName = firstlst.GroupBy(x => x).Select(p => new { Count = p.Count(), Name = p.Key }).OrderByDescending(x => x.Count).ToList()[0].Name;
                    var awayName = secndlst.GroupBy(x => x).Select(p => new { Count = p.Count(), Name = p.Key }).OrderByDescending(x => x.Count).ToList()[0].Name;

                    for (var i = 0; i < 5; i++)
                    {
                        var resScore = rgx.Matches(listContent[i])[0].Groups[0].Value;
                        var resTeams = rgxTeams.Matches(listContent[i])[0].Groups[1].Value;
                        homeSide.Add(new AwayHomeSideBet
                        {
                            TeamAt = resTeams.Split(resScore)[0].Trim() == homeName.Trim() ? "Home" : "Away",
                            Score = resScore,
                            HomeTeam = resTeams.Split(resScore)[0].Trim(),
                            AwayTeam = resTeams.Split(resScore)[1].Trim(),
                            Is35Over = Convert.ToInt32(resScore.Split("-")[0].Trim()) + Convert.ToInt32(resScore.Split("-")[1].Trim()) > 3,
                            
                        });
                    }

                    for (var i = 5; i < 10; i++)
                    {
                        var resScore = rgx.Matches(listContent[i])[0].Groups[0].Value;
                        var resTeams = rgxTeams.Matches(listContent[i])[0].Groups[1].Value;
                        awaySide.Add(new AwayHomeSideBet
                        {
                            TeamAt = resTeams.Split(resScore)[0].Trim() == awayName.Trim() ? "Home" : "Away",
                            Score = resScore,
                            HomeTeam = resTeams.Split(resScore)[0].Trim(),
                            AwayTeam = resTeams.Split(resScore)[1].Trim(),
                            Is35Over = Convert.ToInt32(resScore.Split("-")[0].Trim()) + Convert.ToInt32(resScore.Split("-")[1].Trim()) > 3
                        });
                    }

                    bool condTicket1 = awaySide[awaySide.Count - 1].Is35Over && 
                                       homeSide[homeSide.Count - 1].Is35Over && 
                                       homeSide[homeSide.Count - 2].Is35Over;
                    bool condTicket2 = homeSide[homeSide.Count - 1].Is35Over &&
                                       awaySide[awaySide.Count - 1].Is35Over &&
                                       awaySide[awaySide.Count - 2].Is35Over;

                    result = condTicket1 || condTicket2;
                }

                return result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private bool CheckHT15Under(JobAnalyseModel item, LeagueHolder leagueHolder)
        {
            try
            {
                bool isValidImportant =
                        leagueHolder.HT_Over_1_5_Percentage > item.AverageProfiler.HT_15_Over.Percentage &&
                        item.AverageProfiler.HT_15_Over.FeatureName.ToLower() == "true" &&
                        leagueHolder.HT_Over_1_5_Percentage > item.AverageProfilerHomeAway.HT_15_Over.Percentage &&
                        item.AverageProfilerHomeAway.HT_15_Over.FeatureName.ToLower() == "true" &&
                        leagueHolder.HT_GoalsAverage < (decimal)0.85;

                if (!isValidImportant) return isValidImportant;

                bool isValid =
                item.AverageProfilerHomeAway.HT_15_Over.Percentage > 60 &&
                item.AverageProfilerHomeAway.HT_15_Over.FeatureName.ToLower() == "false" &&
                item.AverageProfiler.HT_15_Over.Percentage > 60 &&
                item.AverageProfiler.HT_15_Over.FeatureName.ToLower() == "false";

                bool isValid2 =
                    item.AverageProfiler.Average_HT_Goals_HomeTeam + item.AverageProfiler.Average_HT_Goals_AwayTeam < (decimal)0.50 &&
                    item.AverageProfilerHomeAway.Average_HT_Goals_AwayTeam + item.AverageProfilerHomeAway.Average_HT_Goals_HomeTeam < (decimal)0.50;

                return isValid || isValid2;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private bool CheckHT05Over(JobAnalyseModel item, LeagueHolder leagueHolder)
        {
            try
            {
                bool isValidImportant =
                        leagueHolder.HT_Over_0_5_Percentage < item.AverageProfiler.HT_05_Over.Percentage &&
                        item.AverageProfiler.HT_05_Over.FeatureName.ToLower() == "true" &&
                        leagueHolder.HT_Over_0_5_Percentage < item.AverageProfilerHomeAway.HT_05_Over.Percentage &&
                        item.AverageProfilerHomeAway.HT_05_Over.FeatureName.ToLower() == "true" &&
                        leagueHolder.HT_GoalsAverage > (decimal)1.20;

                if (!isValidImportant) return isValidImportant;

                bool isValid =
                item.AverageProfilerHomeAway.HT_05_Over.Percentage > 80 &&
                item.AverageProfilerHomeAway.HT_05_Over.FeatureName.ToLower() == "true" &&
                item.AverageProfiler.HT_05_Over.Percentage > 80 &&
                item.AverageProfiler.HT_05_Over.FeatureName.ToLower() == "true";

                bool isValid2 =
                    item.AverageProfiler.Average_HT_Goals_HomeTeam + item.AverageProfiler.Average_HT_Goals_AwayTeam > (decimal)1.20 &&
                    item.AverageProfilerHomeAway.Average_HT_Goals_AwayTeam + item.AverageProfilerHomeAway.Average_HT_Goals_HomeTeam > (decimal)1.20;

                return isValid || isValid2;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private bool CheckSH05Over(JobAnalyseModel item, LeagueHolder leagueHolder)
        {
            try
            {
                bool isValidImportant =
                        leagueHolder.SH_Over_0_5_Percentage < item.AverageProfiler.SH_05_Over.Percentage &&
                        item.AverageProfiler.SH_05_Over.FeatureName.ToLower() == "true" &&
                        leagueHolder.SH_Over_0_5_Percentage < item.AverageProfilerHomeAway.SH_05_Over.Percentage &&
                        item.AverageProfilerHomeAway.SH_05_Over.FeatureName.ToLower() == "true" &&
                        leagueHolder.SH_GoalsAverage >= (decimal)1.62;

                if (!isValidImportant) return isValidImportant;

                bool isValid =
                item.AverageProfilerHomeAway.SH_05_Over.Percentage >= 90 &&
                item.AverageProfilerHomeAway.SH_05_Over.FeatureName.ToLower() == "true" &&
                item.AverageProfiler.SH_05_Over.Percentage >= 90 &&
                item.AverageProfiler.SH_05_Over.FeatureName.ToLower() == "true";

                bool isValid2 =
                    item.AverageProfiler.Average_SH_Goals_HomeTeam + item.AverageProfiler.Average_SH_Goals_AwayTeam > (decimal)1.62 &&
                    item.AverageProfilerHomeAway.Average_SH_Goals_AwayTeam + item.AverageProfilerHomeAway.Average_SH_Goals_HomeTeam > (decimal)1.62;

                return isValid || isValid2;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private bool CheckFT_GG(JobAnalyseModel item, string contentString, LeagueHolder leagueHolder)
        {
            bool result = false;

            try
            {
                bool isValidImportant = leagueHolder.GG_Percentage < item.AverageProfilerHomeAway.FT_GG.Percentage &&                                  item.AverageProfilerHomeAway.FT_GG.FeatureName.ToLower() == "true" && 
                                        leagueHolder.GG_Percentage < item.AverageProfiler.FT_GG.Percentage &&
                                        item.AverageProfiler.FT_GG.FeatureName.ToLower() == "true" &&
                                        leagueHolder.HomeFT_GoalsAverage > (decimal)1.3 &&
                                        leagueHolder.AwayFT_GoalsAverage > (decimal)1.3;

                if (!isValidImportant) return isValidImportant;

                bool isValid =
                item.AverageProfilerHomeAway.FT_GG.Percentage > 60 &&
                item.AverageProfilerHomeAway.FT_GG.FeatureName.ToLower() == "true" &&
                item.AverageProfiler.FT_GG.Percentage > 60 &&
                item.AverageProfiler.FT_GG.FeatureName.ToLower() == "true";

                bool isValid2 =
                    item.AverageProfiler.Average_FT_Goals_HomeTeam >= (decimal)1.55 &&
                    item.AverageProfiler.Average_FT_Goals_AwayTeam >= (decimal)1.55 &&
                    item.AverageProfilerHomeAway.Average_FT_Goals_HomeTeam >= (decimal)1.55 &&
                    item.AverageProfilerHomeAway.Average_FT_Goals_AwayTeam >= (decimal)1.55;

                if (isValid || isValid2)
                {
                    var listContent = contentString.Split("last-games").Where(x => x.Trim().StartsWith("title")).ToList();

                    List<AwayHomeSide> homeSide = new List<AwayHomeSide>();
                    List<AwayHomeSide> awaySide = new List<AwayHomeSide>();
                    var rgx = new Regex(PatternConstant.RegSrcMix.ScoreFromPerformance);
                    var rgxTeams = new Regex(PatternConstant.RegSrcMix.TeamsCollector);

                    var firstlst = new List<string>();
                    var secndlst = new List<string>();

                    for (var i = 0; i < 5; i++)
                    {
                        var resScore = rgx.Matches(listContent[i])[0].Groups[0].Value;
                        var resTeams = rgxTeams.Matches(listContent[i])[0].Groups[1].Value;
                        foreach (var team in resTeams.Split(resScore))
                        {
                            firstlst.Add(team.Trim());
                        }
                    }

                    for (var i = 5; i < 10; i++)
                    {
                        var resScore = rgx.Matches(listContent[i])[0].Groups[0].Value;
                        var resTeams = rgxTeams.Matches(listContent[i])[0].Groups[1].Value;
                        foreach (var team in resTeams.Split(resScore))
                        {
                            secndlst.Add(team.Trim());
                        }
                    }

                    var homeName = firstlst.GroupBy(x => x).Select(p => new { Count = p.Count(), Name = p.Key }).OrderByDescending(x => x.Count).ToList()[0].Name;
                    var awayName = secndlst.GroupBy(x => x).Select(p => new { Count = p.Count(), Name = p.Key }).OrderByDescending(x => x.Count).ToList()[0].Name;

                    for (var i = 0; i < 5; i++)
                    {
                        var resScore = rgx.Matches(listContent[i])[0].Groups[0].Value;
                        var resTeams = rgxTeams.Matches(listContent[i])[0].Groups[1].Value;
                        homeSide.Add(new AwayHomeSide
                        {
                            TeamAt = resTeams.Split(resScore)[0].Trim() == homeName.Trim() ? "Home" : "Away",
                            Score = resScore,
                            HomeTeam = resTeams.Split(resScore)[0].Trim(),
                            AwayTeam = resTeams.Split(resScore)[1].Trim(),
                            IsGG = Convert.ToInt32(resScore.Split("-")[0].Trim()) > 0 && Convert.ToInt32(resScore.Split("-")[1].Trim()) > 0,
                            IsTeamHasGoal = resTeams.Split(resScore)[0].Trim() == homeName.Trim()
                                          ? Convert.ToInt32(resScore.Split("-")[0].Trim()) > 0
                                          : Convert.ToInt32(resScore.Split("-")[1].Trim()) > 0
                        });
                    }

                    for (var i = 5; i < 10; i++)
                    {
                        var resScore = rgx.Matches(listContent[i])[0].Groups[0].Value;
                        var resTeams = rgxTeams.Matches(listContent[i])[0].Groups[1].Value;
                        awaySide.Add(new AwayHomeSide
                        {
                            TeamAt = resTeams.Split(resScore)[0].Trim() == awayName.Trim() ? "Home" : "Away",
                            Score = resScore,
                            HomeTeam = resTeams.Split(resScore)[0].Trim(),
                            AwayTeam = resTeams.Split(resScore)[1].Trim(),
                            IsGG = Convert.ToInt32(resScore.Split("-")[0].Trim()) > 0 && Convert.ToInt32(resScore.Split("-")[1].Trim()) > 0,
                            IsTeamHasGoal = resTeams.Split(resScore)[0].Trim() == homeName.Trim()
                                          ? Convert.ToInt32(resScore.Split("-")[0].Trim()) > 0
                                          : Convert.ToInt32(resScore.Split("-")[1].Trim()) > 0
                        });
                    }

                    bool awayNotGG1 = !awaySide[awaySide.Count - 1].IsGG && !awaySide[awaySide.Count - 2].IsGG;

                    bool homeNotGG1 = !homeSide[homeSide.Count - 1].IsGG;

                    bool awayNotGG2 = !awaySide[awaySide.Count - 1].IsGG;

                    bool homeNotGG2 = !homeSide[homeSide.Count - 1].IsGG && !homeSide[homeSide.Count - 2].IsGG;

                    bool mix1 = awayNotGG1 && homeNotGG1;
                    bool mix2 = awayNotGG2 && homeNotGG2;

                    result = mix1 || mix2;

                    if (!result)
                    {
                        bool isValid3 =
                        item.AverageProfiler.Home_FT_05_Over.Percentage >= 90 &&
                        item.AverageProfiler.Home_FT_05_Over.FeatureName.ToLower() == "true" &&
                        item.AverageProfilerHomeAway.Home_FT_05_Over.Percentage >= 90 &&
                        item.AverageProfilerHomeAway.Home_FT_05_Over.FeatureName.ToLower() == "true" &&
                        item.AverageProfiler.Away_FT_05_Over.Percentage >= 90 &&
                        item.AverageProfiler.Away_FT_05_Over.FeatureName.ToLower() == "true" &&
                        item.AverageProfilerHomeAway.Away_FT_05_Over.Percentage >= 90 &&
                        item.AverageProfilerHomeAway.Away_FT_05_Over.FeatureName.ToLower() == "true";

                        bool awayNotGoal1 = !awaySide[awaySide.Count - 1].IsTeamHasGoal;

                        bool homeNotGoal1 = !homeSide[homeSide.Count - 1].IsTeamHasGoal && !homeSide[homeSide.Count - 2].IsTeamHasGoal;

                        bool awayNotGoal2 = !awaySide[awaySide.Count - 1].IsTeamHasGoal && !awaySide[awaySide.Count - 2].IsTeamHasGoal;

                        bool homeNotGoal2 = !homeSide[homeSide.Count - 1].IsTeamHasGoal;

                        bool mixNotGoal1 = awayNotGoal1 && homeNotGoal1;
                        bool mixNotGoal2 = awayNotGoal2 && homeNotGoal2;

                        result = mixNotGoal1 || mixNotGoal2;
                    }
                }
                                
                return result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private bool CheckFT_X1(JobAnalyseModel item, string contentString, LeagueHolder leagueHolder)
        {
            bool result = false;

            try
            {
                bool isValidImportant =
                    leagueHolder.AwayFT_GoalsAverage > item.AverageProfilerHomeAway.Average_FT_Goals_AwayTeam &&
                    leagueHolder.AwayFT_GoalsAverage > item.AverageProfiler.Average_FT_Goals_AwayTeam &&

                    leagueHolder.HomeFT_GoalsAverage < item.AverageProfilerHomeAway.Average_FT_Goals_HomeTeam &&
                    leagueHolder.HomeFT_GoalsAverage < item.AverageProfiler.Average_FT_Goals_HomeTeam;

                if (!isValidImportant) return isValidImportant;

                bool isValid =
                item.AverageProfilerHomeAway.FT_Result.Percentage >= 50 &&
                item.AverageProfilerHomeAway.FT_Result.FeatureName.ToLower() != "2" &&
                item.AverageProfiler.FT_Result.Percentage >= 50 &&
                item.AverageProfiler.FT_Result.FeatureName.ToLower() != "2";

                if (isValid)
                {
                    var listContent = contentString.Split("last-games").Where(x => x.Trim().StartsWith("title")).ToList();

                    List<AwayHomeSideBet> homeSide = new List<AwayHomeSideBet>();
                    List<AwayHomeSideBet> awaySide = new List<AwayHomeSideBet>();
                    var rgx = new Regex(PatternConstant.RegSrcMix.ScoreFromPerformance);
                    var rgxTeams = new Regex(PatternConstant.RegSrcMix.TeamsCollector);

                    var firstlst = new List<string>();
                    var secndlst = new List<string>();

                    for (var i = 0; i < 5; i++)
                    {
                        var resScore = rgx.Matches(listContent[i])[0].Groups[0].Value;
                        var resTeams = rgxTeams.Matches(listContent[i])[0].Groups[1].Value;
                        foreach (var team in resTeams.Split(resScore))
                        {
                            firstlst.Add(team.Trim());
                        }
                    }

                    for (var i = 5; i < 10; i++)
                    {
                        var resScore = rgx.Matches(listContent[i])[0].Groups[0].Value;
                        var resTeams = rgxTeams.Matches(listContent[i])[0].Groups[1].Value;
                        foreach (var team in resTeams.Split(resScore))
                        {
                            secndlst.Add(team.Trim());
                        }
                    }

                    var homeName = firstlst.GroupBy(x => x).Select(p => new { Count = p.Count(), Name = p.Key }).OrderByDescending(x => x.Count).ToList()[0].Name;
                    var awayName = secndlst.GroupBy(x => x).Select(p => new { Count = p.Count(), Name = p.Key }).OrderByDescending(x => x.Count).ToList()[0].Name;

                    for (var i = 0; i < 5; i++)
                    {
                        var resScore = rgx.Matches(listContent[i])[0].Groups[0].Value;
                        var resTeams = rgxTeams.Matches(listContent[i])[0].Groups[1].Value;
                        homeSide.Add(new AwayHomeSideBet
                        {
                            TeamAt = resTeams.Split(resScore)[0].Trim() == homeName.Trim() ? "Home" : "Away",
                            Score = resScore,
                            HomeTeam = resTeams.Split(resScore)[0].Trim(),
                            AwayTeam = resTeams.Split(resScore)[1].Trim(),
                            IsWin = resTeams.Split(resScore)[0].Trim() == homeName.Trim()
                                  ? Convert.ToInt32(resScore.Split("-")[0].Trim()) > Convert.ToInt32(resScore.Split("-")[1].Trim())
                                  : Convert.ToInt32(resScore.Split("-")[0].Trim()) < Convert.ToInt32(resScore.Split("-")[1].Trim()),
                            IsLost = resTeams.Split(resScore)[0].Trim() == homeName.Trim()
                                  ? Convert.ToInt32(resScore.Split("-")[0].Trim()) < Convert.ToInt32(resScore.Split("-")[1].Trim())
                                  : Convert.ToInt32(resScore.Split("-")[0].Trim()) > Convert.ToInt32(resScore.Split("-")[1].Trim()),
                        });
                    }

                    for (var i = 5; i < 10; i++)
                    {
                        var resScore = rgx.Matches(listContent[i])[0].Groups[0].Value;
                        var resTeams = rgxTeams.Matches(listContent[i])[0].Groups[1].Value;
                        awaySide.Add(new AwayHomeSideBet
                        {
                            TeamAt = resTeams.Split(resScore)[0].Trim() == awayName.Trim() ? "Home" : "Away",
                            Score = resScore,
                            HomeTeam = resTeams.Split(resScore)[0].Trim(),
                            AwayTeam = resTeams.Split(resScore)[1].Trim(),
                            IsWin = resTeams.Split(resScore)[0].Trim() == homeName.Trim()
                                  ? Convert.ToInt32(resScore.Split("-")[0].Trim()) > Convert.ToInt32(resScore.Split("-")[1].Trim())
                                  : Convert.ToInt32(resScore.Split("-")[0].Trim()) < Convert.ToInt32(resScore.Split("-")[1].Trim()),
                            IsLost = resTeams.Split(resScore)[0].Trim() == homeName.Trim()
                                  ? Convert.ToInt32(resScore.Split("-")[0].Trim()) < Convert.ToInt32(resScore.Split("-")[1].Trim())
                                  : Convert.ToInt32(resScore.Split("-")[0].Trim()) > Convert.ToInt32(resScore.Split("-")[1].Trim()),
                        });
                    }

                    bool homeIsLost = homeSide[homeSide.Count - 1].IsLost;

                    bool awayIsWinOrX = awaySide[awaySide.Count - 1].IsWin;

                    result = homeIsLost && awayIsWinOrX;
                }

                return result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private bool CheckFT_X2(JobAnalyseModel item, string contentString, LeagueHolder leagueHolder)
        {
            bool result = false;

            try
            {
                bool isValidImportant =
                    leagueHolder.AwayFT_GoalsAverage < item.AverageProfilerHomeAway.Average_FT_Goals_AwayTeam &&
                    leagueHolder.AwayFT_GoalsAverage < item.AverageProfiler.Average_FT_Goals_AwayTeam &&

                    leagueHolder.HomeFT_GoalsAverage > item.AverageProfilerHomeAway.Average_FT_Goals_HomeTeam &&
                    leagueHolder.HomeFT_GoalsAverage > item.AverageProfiler.Average_FT_Goals_HomeTeam;

                if (!isValidImportant) return isValidImportant;

                bool isValid =
                item.AverageProfilerHomeAway.FT_Result.Percentage >= 50 &&
                item.AverageProfilerHomeAway.FT_Result.FeatureName.ToLower() != "1" &&
                item.AverageProfiler.FT_Result.Percentage >= 50 &&
                item.AverageProfiler.FT_Result.FeatureName.ToLower() != "1";

                if (isValid)
                {
                    var listContent = contentString.Split("last-games").Where(x => x.Trim().StartsWith("title")).ToList();

                    List<AwayHomeSideBet> homeSide = new List<AwayHomeSideBet>();
                    List<AwayHomeSideBet> awaySide = new List<AwayHomeSideBet>();
                    var rgx = new Regex(PatternConstant.RegSrcMix.ScoreFromPerformance);
                    var rgxTeams = new Regex(PatternConstant.RegSrcMix.TeamsCollector);

                    var firstlst = new List<string>();
                    var secndlst = new List<string>();

                    for (var i = 0; i < 5; i++)
                    {
                        var resScore = rgx.Matches(listContent[i])[0].Groups[0].Value;
                        var resTeams = rgxTeams.Matches(listContent[i])[0].Groups[1].Value;
                        foreach (var team in resTeams.Split(resScore))
                        {
                            firstlst.Add(team.Trim());
                        }
                    }

                    for (var i = 5; i < 10; i++)
                    {
                        var resScore = rgx.Matches(listContent[i])[0].Groups[0].Value;
                        var resTeams = rgxTeams.Matches(listContent[i])[0].Groups[1].Value;
                        foreach (var team in resTeams.Split(resScore))
                        {
                            secndlst.Add(team.Trim());
                        }
                    }

                    var homeName = firstlst.GroupBy(x => x).Select(p => new { Count = p.Count(), Name = p.Key }).OrderByDescending(x => x.Count).ToList()[0].Name;
                    var awayName = secndlst.GroupBy(x => x).Select(p => new { Count = p.Count(), Name = p.Key }).OrderByDescending(x => x.Count).ToList()[0].Name;

                    for (var i = 0; i < 5; i++)
                    {
                        var resScore = rgx.Matches(listContent[i])[0].Groups[0].Value;
                        var resTeams = rgxTeams.Matches(listContent[i])[0].Groups[1].Value;
                        homeSide.Add(new AwayHomeSideBet
                        {
                            TeamAt = resTeams.Split(resScore)[0].Trim() == homeName.Trim() ? "Home" : "Away",
                            Score = resScore,
                            HomeTeam = resTeams.Split(resScore)[0].Trim(),
                            AwayTeam = resTeams.Split(resScore)[1].Trim(),
                            IsWin = resTeams.Split(resScore)[0].Trim() == homeName.Trim()
                                  ? Convert.ToInt32(resScore.Split("-")[0].Trim()) > Convert.ToInt32(resScore.Split("-")[1].Trim())
                                  : Convert.ToInt32(resScore.Split("-")[0].Trim()) < Convert.ToInt32(resScore.Split("-")[1].Trim()),
                            IsLost = resTeams.Split(resScore)[0].Trim() == homeName.Trim()
                                  ? Convert.ToInt32(resScore.Split("-")[0].Trim()) < Convert.ToInt32(resScore.Split("-")[1].Trim())
                                  : Convert.ToInt32(resScore.Split("-")[0].Trim()) > Convert.ToInt32(resScore.Split("-")[1].Trim()),
                        });
                    }

                    for (var i = 5; i < 10; i++)
                    {
                        var resScore = rgx.Matches(listContent[i])[0].Groups[0].Value;
                        var resTeams = rgxTeams.Matches(listContent[i])[0].Groups[1].Value;
                        awaySide.Add(new AwayHomeSideBet
                        {
                            TeamAt = resTeams.Split(resScore)[0].Trim() == awayName.Trim() ? "Home" : "Away",
                            Score = resScore,
                            HomeTeam = resTeams.Split(resScore)[0].Trim(),
                            AwayTeam = resTeams.Split(resScore)[1].Trim(),
                            IsWin = resTeams.Split(resScore)[0].Trim() == homeName.Trim()
                                  ? Convert.ToInt32(resScore.Split("-")[0].Trim()) > Convert.ToInt32(resScore.Split("-")[1].Trim())
                                  : Convert.ToInt32(resScore.Split("-")[0].Trim()) < Convert.ToInt32(resScore.Split("-")[1].Trim()),
                            IsLost = resTeams.Split(resScore)[0].Trim() == homeName.Trim()
                                  ? Convert.ToInt32(resScore.Split("-")[0].Trim()) < Convert.ToInt32(resScore.Split("-")[1].Trim())
                                  : Convert.ToInt32(resScore.Split("-")[0].Trim()) > Convert.ToInt32(resScore.Split("-")[1].Trim()),
                        });
                    }

                    bool homeIsLost = homeSide[homeSide.Count - 1].IsWin;

                    bool awayIsWinOrX = awaySide[awaySide.Count - 1].IsLost;

                    result = homeIsLost && awayIsWinOrX;
                }

                return result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        private bool CheckHT_X1(JobAnalyseModel item, string contentString, LeagueHolder leagueHolder)
        {
            bool result = false;

            try
            {
                bool isValidImportant =
                    leagueHolder.AwayHT_GoalsAverage > item.AverageProfilerHomeAway.Average_HT_Goals_AwayTeam &&
                    leagueHolder.AwayHT_GoalsAverage > item.AverageProfiler.Average_HT_Goals_AwayTeam &&

                    leagueHolder.HomeHT_GoalsAverage < item.AverageProfilerHomeAway.Average_HT_Goals_HomeTeam &&
                    leagueHolder.HomeHT_GoalsAverage < item.AverageProfiler.Average_HT_Goals_HomeTeam;

                if (!isValidImportant) return isValidImportant;

                bool isValid =
                item.AverageProfilerHomeAway.Away_HT_05_Over.Percentage > 70 &&
                item.AverageProfilerHomeAway.Away_HT_05_Over.FeatureName.ToLower() == "false" &&
                item.AverageProfiler.Away_HT_05_Over.Percentage > 70 &&
                item.AverageProfiler.Away_HT_05_Over.FeatureName.ToLower() == "false" &&
                item.AverageProfilerHomeAway.Home_HT_05_Over.Percentage >= 60 &&
                item.AverageProfilerHomeAway.Home_HT_05_Over.FeatureName.ToLower() == "true" &&
                item.AverageProfiler.Home_HT_05_Over.Percentage >= 60 &&
                item.AverageProfiler.Home_HT_05_Over.FeatureName.ToLower() == "true";

                bool isValid2 =
                    item.AverageProfilerHomeAway.Average_HT_Goals_AwayTeam < (decimal)0.34 &&
                item.AverageProfiler.Average_HT_Goals_AwayTeam < (decimal)0.34 &&
                item.AverageProfilerHomeAway.Average_HT_Goals_HomeTeam > (decimal)0.66 &&
                item.AverageProfiler.Average_HT_Goals_HomeTeam > (decimal)0.66;

                if (isValid || isValid2)
                    result = true;

                return result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private bool CheckHT_X2(JobAnalyseModel item, string contentString, LeagueHolder leagueHolder)
        {
            bool result = false;

            try
            {
                bool isValidImportant =
                    leagueHolder.AwayHT_GoalsAverage < item.AverageProfilerHomeAway.Average_HT_Goals_AwayTeam &&
                    leagueHolder.AwayHT_GoalsAverage < item.AverageProfiler.Average_HT_Goals_AwayTeam &&

                    leagueHolder.HomeHT_GoalsAverage > item.AverageProfilerHomeAway.Average_HT_Goals_HomeTeam &&
                    leagueHolder.HomeHT_GoalsAverage > item.AverageProfiler.Average_HT_Goals_HomeTeam;

                if (!isValidImportant) return isValidImportant;

                bool isValid =
                item.AverageProfilerHomeAway.Home_HT_05_Over.Percentage > 70 &&
                item.AverageProfilerHomeAway.Home_HT_05_Over.FeatureName.ToLower() == "false" &&
                item.AverageProfiler.Home_HT_05_Over.Percentage > 70 &&
                item.AverageProfiler.Home_HT_05_Over.FeatureName.ToLower() == "false" &&

                item.AverageProfilerHomeAway.Away_HT_05_Over.Percentage >= 60 &&
                item.AverageProfilerHomeAway.Away_HT_05_Over.FeatureName.ToLower() == "true" &&
                item.AverageProfiler.Away_HT_05_Over.Percentage >= 60 &&
                item.AverageProfiler.Away_HT_05_Over.FeatureName.ToLower() == "true";

                bool isValid2 =
                    item.AverageProfilerHomeAway.Average_HT_Goals_HomeTeam < (decimal)0.34 &&
                item.AverageProfiler.Average_HT_Goals_HomeTeam < (decimal)0.34 &&
                item.AverageProfilerHomeAway.Average_HT_Goals_AwayTeam > (decimal)0.66 &&
                item.AverageProfiler.Average_HT_Goals_AwayTeam > (decimal)0.66;

                if (isValid || isValid2)
                    result = true;

                return result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion



        public override void ExecuteTTT(List<FilterResult> filterResults, string path)
        {
            List<JobAnalyseModel> responseProfiler = new List<JobAnalyseModel>();
            bool check = false;

            using (var sr = new StreamReader(path))
            {
                responseProfiler = JsonConvert.DeserializeObject<List<JobAnalyseModel>>(sr.ReadToEnd());
                check = responseProfiler != null && responseProfiler.Any();
            }

            if (!check)
            {
                responseProfiler = OperationalProcessor.GetJobAnalyseModelResultTest(_matchBetService, _containerTemp, filterResults).Where(x => x.AverageProfiler != null).ToList();

                using (var sw = new StreamWriter(path))
                {
                    sw.Write(JsonConvert.SerializeObject(responseProfiler, Formatting.Indented));
                }
            }

            // responseProfiler = responseProfiler.Take(30).ToList();

            List<object> listsRes = new List<object>();

            List<object> odds = new List<object>();
            var rgxOdd35U = new Regex(PatternConstant.StartedMatchPattern.FT_2_5_Over);

            int correctRes = 0;
            int allRes = 0;
            int correctResUnder = 0;
            int allResUnder = 0;
            int correctResOver = 0;
            int allResOver = 0;
            int failed = 0;

            int iteration = 0;

            foreach (var item in responseProfiler)
            {
                iteration++;
                try
                {
                    if (item.HomeTeam_FormPerformanceGuessContainer.HomeAway.FT_25_Over.Percentage >= 60 && item.HomeTeam_FormPerformanceGuessContainer.HomeAway.FT_25_Over.FeatureName.ToLower() == "true" &&
                        item.AwayTeam_FormPerformanceGuessContainer.HomeAway.FT_25_Over.Percentage >= 60 && item.AwayTeam_FormPerformanceGuessContainer.HomeAway.FT_25_Over.FeatureName.ToLower() == "true" &&
                        item.ComparisonInfoContainer.HomeAway.FT_25_Over.Percentage >= 60 && item.ComparisonInfoContainer.HomeAway.FT_25_Over.FeatureName.ToLower() == "true")
                    {
                        var client1 = new HttpClient();
                        var client = new WebOperation();
                        var cnt1 = client1.GetStringAsync($"https://arsiv.mackolik.com/Match/Default.aspx?id={item.HomeTeam_FormPerformanceGuessContainer.Serial}#karsilastirma").Result;
                        var cnt = client.GetMinifiedString($"https://arsiv.mackolik.com/Match/Default.aspx?id={item.HomeTeam_FormPerformanceGuessContainer.Serial}#karsilastirma");

                        //var cntArr = cnt.Split("left-block-team-name"); "last-games"
                        var cntArr = cnt1.Split("last-games").Where(x => x.StartsWith("\" title")).ToList();

                        List<AwayHomeSide> homeSide = new List<AwayHomeSide>();
                        List<AwayHomeSide> awaySide = new List<AwayHomeSide>();
                        var rgx = new Regex("\\b(0|[1-9]\\d*)-(0|[1-9]\\d*)\\b");
                        var rgxTeams = new Regex("title=\"[\\s\\S]*?([^\\W][^\\(]+)");

                        var firstlst = new List<string>();
                        var secndlst = new List<string>();

                        for (var i = 0; i < 5; i++)
                        {
                            var resScore = rgx.Matches(cntArr[i])[0].Groups[0].Value;
                            var resTeams = rgxTeams.Matches(cntArr[i])[0].Groups[1].Value;
                            foreach (var team in resTeams.Split(resScore))
                            {
                                firstlst.Add(team.Trim());
                            }
                        }

                        for (var i = 5; i < 10; i++)
                        {
                            var resScore = rgx.Matches(cntArr[i])[0].Groups[0].Value;
                            var resTeams = rgxTeams.Matches(cntArr[i])[0].Groups[1].Value;
                            foreach (var team in resTeams.Split(resScore))
                            {
                                secndlst.Add(team.Trim());
                            }
                        }

                        var homeName = firstlst.GroupBy(x => x).Select(p => new { Count = p.Count(), Name = p.Key }).OrderByDescending(x => x.Count).ToList()[0].Name;
                        var awayName = secndlst.GroupBy(x => x).Select(p => new { Count = p.Count(), Name = p.Key }).OrderByDescending(x => x.Count).ToList()[0].Name;

                        for (var i = 0; i < 4; i++)
                        {
                            var resScore = rgx.Matches(cntArr[i])[0].Groups[0].Value;
                            var resTeams = rgxTeams.Matches(cntArr[i])[0].Groups[1].Value;
                            homeSide.Add(new AwayHomeSide
                            {
                                TeamAt = resTeams.Split(resScore)[0].Trim() == homeName.Trim() ? "Home" : "Away",
                                Score = resScore,
                                HomeTeam = resTeams.Split(resScore)[0].Trim(),
                                AwayTeam = resTeams.Split(resScore)[1].Trim(),
                                Is25Over = Convert.ToInt32(resScore.Split("-")[0].Trim()) + Convert.ToInt32(resScore.Split("-")[1].Trim()) > 2
                            });
                        }

                        for (var i = 5; i < 9; i++)
                        {
                            var resScore = rgx.Matches(cntArr[i])[0].Groups[0].Value;
                            var resTeams = rgxTeams.Matches(cntArr[i])[0].Groups[1].Value;
                            awaySide.Add(new AwayHomeSide
                            {
                                TeamAt = resTeams.Split(resScore)[0].Trim() == awayName.Trim() ? "Home" : "Away",
                                Score = resScore,
                                HomeTeam = resTeams.Split(resScore)[0].Trim(),
                                AwayTeam = resTeams.Split(resScore)[1].Trim(),
                                Is25Over = Convert.ToInt32(resScore.Split("-")[0].Trim()) + Convert.ToInt32(resScore.Split("-")[1].Trim()) > 2
                            });
                        }

                        if (awaySide[awaySide.Count - 1].Is25Over != homeSide[homeSide.Count - 1].Is25Over)
                        {
                            var regexScore = new Regex(PatternConstant.StartedMatchPattern.FTResultMatch);
                            var score = regexScore.Matches(cnt)[0].Groups[1].Value.Trim();
                            int frst = Convert.ToInt32(score.Split("-")[0].Trim());
                            int scnd = Convert.ToInt32(score.Split("-")[1].Trim());
                            bool resss = frst + scnd > 2;

                            if (resss)
                            {
                                odds.Add(new { Serial = item.HomeTeam_FormPerformanceGuessContainer.Serial, Odd = rgxOdd35U.Matches(cnt)[0].Groups[1].Value });
                                correctRes++;
                            }

                            allRes++;
                        }
                    }
                }
                catch (Exception ex)
                {
                    failed++;
                    continue;
                }
            }

            decimal percent = correctRes * 100 / allRes;

            base.ExecuteTTT(filterResults, path);
        }



        public override void Execute_TEST()
        {
            InitialiseTimeContainer();
            var jsonNullIgnorer = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            _analysableSerials.ForEach(x =>
            {
                var serialItemServer = _timeSerials.FirstOrDefault(p => p.Serial == x.Serial);
                serialItemServer.IsAnalysed = true;
            });

            _systemCheckerContainer.SerialsBeforeGenerated = _analysableSerials.Select(x => x.Serial).ToList();

            var responseProfiler = OperationalProcessor.GetJobAnalyseModelResult_TEST(_systemCheckerContainer, _matchBetService, _filterResultService, _containerTemp);

            var resultResponse = new PossiblitiyContainer
            {
                FT_25_Over_Testing = new PossiblitiyTestingModel
                {
                    CountFound = responseProfiler.Where(x => x.Is_25_Over.Forecast).Count(),
                    ResultPercentage = Convert.ToInt32(responseProfiler.Where(x => x.Is_25_Over.RealityResult).Count() * 100 / responseProfiler.Where(x => x.Is_25_Over.Forecast).Count())
                },
                FT_GG_Testing = new PossiblitiyTestingModel
                {
                    CountFound = responseProfiler.Where(x => x.Is_GG.Forecast).Count(),
                    ResultPercentage = Convert.ToInt32(responseProfiler.Where(x => x.Is_GG.RealityResult).Count() * 100 / responseProfiler.Where(x => x.Is_GG.Forecast).Count())
                },
                HT_15_Over_Testing = new PossiblitiyTestingModel
                {
                    CountFound = responseProfiler.Where(x => x.Is_HT_15_Over.Forecast).Count(),
                    ResultPercentage = Convert.ToInt32(responseProfiler.Where(x => x.Is_HT_15_Over.RealityResult).Count() * 100 / responseProfiler.Where(x => x.Is_HT_15_Over.Forecast).Count())
                },
                FT_35_Over_Testing = new PossiblitiyTestingModel
                {
                    CountFound = responseProfiler.Where(x => x.Is_35_Over.Forecast).Count(),
                    ResultPercentage = Convert.ToInt32(responseProfiler.Where(x => x.Is_35_Over.RealityResult).Count() * 100 / responseProfiler.Where(x => x.Is_35_Over.Forecast).Count())
                },
                FT_Goals_23_Testing = new PossiblitiyTestingModel
                {
                    CountFound = responseProfiler.Where(x => x.Is_Goals_23.Forecast).Count(),
                    ResultPercentage = Convert.ToInt32(responseProfiler.Where(x => x.Is_Goals_23.RealityResult).Count() * 100 / responseProfiler.Where(x => x.Is_Goals_23.Forecast).Count())
                },
                FT_Score_1_1_Testing = new PossiblitiyTestingModel
                {
                    CountFound = responseProfiler.Where(x => x.Is_Score_1_1.Forecast).Count(),
                    ResultPercentage = Convert.ToInt32(responseProfiler.Where(x => x.Is_Score_1_1.RealityResult).Count() * 100 / responseProfiler.Where(x => x.Is_Score_1_1.Forecast).Count())
                }
            };

            _botService.SendMessage(JsonConvert.SerializeObject(resultResponse, Formatting.Indented));

            //responseProfiler.ForEach(x =>
            //{
            //    string serializedExData = JsonConvert.SerializeObject(x, Formatting.Indented, jsonNullIgnorer);

            //    _botService.SendMessage(serializedExData);
            //});

            _analysableSerials.Clear();

            base.Execute_TEST();
        }

        public void InTimeReflected(object source, ElapsedEventArgs e)
        {
            Execute();
        }

        public void TestInTimeReflected(object source, ElapsedEventArgs e)
        {
            _botService.SendMessage("Testing");
        }
    }
}
