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
using System.Text;
using System.Text.RegularExpressions;
using System.Timers;

namespace SBA.Business.CoreAbilityServices.Job
{
    public class AwayHomeSide
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
        public bool IsTeamHasGoal { get; set; }
    }

    public class ResultModel
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

        public JobAnalyseModel AnalyseModel { get; set; }
        public JobAnalyseModelNisbi AnalyseModelNisbi { get; set; }
    }

    public class JobOperation : BaseJobOperation
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

        public JobOperation(ISocialBotMessagingService botService,
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
            List<JobAnalyseModel> responseProfiler;

            using (var reader = new StreamReader("wwwroot\\files\\jsonFiles\\responseProfilerTemp.json"))
            {
                string content = reader.ReadToEnd();
                if (content.Length < 10)
                {
                    responseProfiler = OperationalProcessor.GetJobAnalyseModelResultTest2222(_matchBetService, _containerTemp, serials).Where(x => x.AverageProfiler != null && x.AverageProfilerHomeAway != null).ToList();
                    using (var writer = new StreamWriter("wwwroot\\files\\jsonFiles\\responseProfilerTemp.json"))
                    {
                        writer.Write(JsonConvert.SerializeObject(responseProfiler));
                    }
                }
                else
                {
                    responseProfiler = JsonConvert.DeserializeObject<List<JobAnalyseModel>>(content);
                }
            }

            if (responseProfiler == null) responseProfiler = new List<JobAnalyseModel>();

            var rgxOdd35U = new Regex(PatternConstant.StartedMatchPattern.FT_2_5_Over);
            var rgxLeague = new Regex(PatternConstant.StartedMatchPattern.League);
            var rgxCountry = new Regex(PatternConstant.StartedMatchPattern.Country);
            var rgxLeague2 = new Regex(PatternConstant.StartedMatchPattern.CountryAndLeague);

            var responsesBet = new List<ResultModel>();

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
                    var match = new ResultModel
                    {
                        Serial = item.ComparisonInfoContainer.Serial,
                        Country = countryName,
                        League = leagueName,
                        Match = $"{item.ComparisonInfoContainer.Home} - {item.ComparisonInfoContainer.Away}",
                        IsFT_25Over = CheckFT25Over(item, contentString, leagueHolder),
                        IsFT_15Over = CheckFT15Over(item, contentString, leagueHolder),
                        IsHT_05Over = CheckHT05Over(item, leagueHolder),
                        IsFT_GG = CheckFT_GG(item, contentString, leagueHolder),
                        AnalyseModel = item
                    };

                    if (CheckIsOk(match))
                        responsesBet.Add(match);
                }
            }

            foreach (var bet in responsesBet)
            {
                var contentString = _webHelper.GetMinifiedString($"https://arsiv.mackolik.com/Match/Default.aspx?id={bet.Serial}#karsilastirma");
                string leagueName = contentString.ResolveLeagueByRegex(countryContainer, rgxLeague, rgxLeague2);
                string countryName = contentString.ResolveCountryByRegex(countryContainer, rgxCountry, rgxLeague2);
                var leagueHolder = league.LeagueHolders.FirstOrDefault(x => x.Country.ToLower() == countryName.ToLower() && x.League.ToLower() == leagueName.ToLower());

                if (leagueHolder == null)
                {
                    continue;
                }

                var strBuilder = new StringBuilder();
                strBuilder.Append("BET Nəticəsi\n");
                strBuilder.Append($"Link: https://arsiv.mackolik.com/Match/Default.aspx?id={bet.Serial}\n");
                strBuilder.Append($"___________________________________\n");
                strBuilder.Append($"Ölkə: {bet.Country}\n");
                strBuilder.Append($"Liqa: {bet.League}\n");
                strBuilder.Append($"Matç: {bet.Match}\n\n");
                strBuilder.Append($"=================================\n");
                strBuilder.Append($"=== LİG Məlumatları ===\n");
                strBuilder.Append($"-----------------------\n");
                strBuilder.Append($"Tapılan Oyun Sayı => {leagueHolder.CountFound}\n");
                strBuilder.Append($"FT Qol-Ort. => {leagueHolder.GoalsAverage.ToString("0.00")}\n");
                strBuilder.Append($"HT Qol-Ort. => {leagueHolder.HT_GoalsAverage.ToString("0.00")}\n");
                strBuilder.Append($"SH Qol-Ort. => {leagueHolder.SH_GoalsAverage.ToString("0.00")}\n");
                strBuilder.Append($"FT Qol/Qol Faizi => {leagueHolder.GG_Percentage.ToResponseBothGoalVisualise()}\n");
                strBuilder.Append($"FT 1,5 Alt/Üst => {leagueHolder.Over_1_5_Percentage.ToResponseOverVisualise()}\n");
                strBuilder.Append($"FT 2,5 Alt/Üst => {leagueHolder.Over_2_5_Percentage.ToResponseOverVisualise()}\n");
                strBuilder.Append($"FT 3,5 Alt/Üst => {leagueHolder.Over_3_5_Percentage.ToResponseOverVisualise()}\n");
                strBuilder.Append($"HT 0,5 Alt/Üst => {leagueHolder.HT_Over_0_5_Percentage.ToResponseOverVisualise()}\n");
                strBuilder.Append($"HT 1,5 Alt/Üst => {leagueHolder.HT_Over_1_5_Percentage.ToResponseOverVisualise()}\n");
                strBuilder.Append($"SH 0,5 Alt/Üst => {leagueHolder.SH_Over_0_5_Percentage.ToResponseOverVisualise()}\n");
                strBuilder.Append($"SH 1,5 Alt/Üst => {leagueHolder.SH_Over_1_5_Percentage.ToResponseOverVisualise()}\n");
                strBuilder.Append($"========================\n");
                if (bet.IsFT_25Over) strBuilder.Append($"Təxmin:  FT 2,5 ÜST\n");
                if (bet.IsFT_GG) strBuilder.Append($"Təxmin:  FT QOL-QOL\n");
                if (bet.IsHT_05Over) strBuilder.Append($"Təxmin:  HT 0,5 ÜST\n");
                if (bet.IsFT_15Over) strBuilder.Append($"Təxmin:  FT 1,5 ÜST\n");
                strBuilder.Append($"=================================\n");

                var strBuilder2 = new StringBuilder();
                var shortAverage = new AverageShort(bet.AnalyseModel.AverageProfiler);
                strBuilder2.Append($"=== ÜMUMİ ===\n");
                strBuilder2.Append($"-------------\n");
                strBuilder2.Append($"FT Home Qol-Ort. =>  {shortAverage.Average_FT_Goals_HomeTeam}\n");
                strBuilder2.Append($"FT Away Qol-Ort. =>  {shortAverage.Average_FT_Goals_AwayTeam}\n");
                strBuilder2.Append($"HT Home Qol-Ort. =>  {shortAverage.Average_HT_Goals_HomeTeam}\n");
                strBuilder2.Append($"HT Away Qol-Ort. =>  {shortAverage.Average_HT_Goals_AwayTeam}\n");
                strBuilder2.Append($"SH Home Qol-Ort. =>  {shortAverage.Average_SH_Goals_HomeTeam}\n");
                strBuilder2.Append($"SH Away Qol-Ort. =>  {shortAverage.Average_SH_Goals_AwayTeam}\n");
                strBuilder2.Append($"FT 1,5 Alt/Üst =>  {shortAverage.FT_15_Over.ToResponseOverVisualise()}\n");
                strBuilder2.Append($"FT 2,5 Alt/Üst =>  {shortAverage.FT_25_Over.ToResponseOverVisualise()}\n");
                strBuilder2.Append($"FT 3,5 Alt/Üst =>  {shortAverage.FT_35_Over.ToResponseOverVisualise()}\n");
                strBuilder2.Append($"HT 0,5 Alt/Üst =>  {shortAverage.HT_05_Over.ToResponseOverVisualise()}\n");
                strBuilder2.Append($"SH 0,5 Alt/Üst =>  {shortAverage.SH_05_Over.ToResponseOverVisualise()}\n");
                strBuilder2.Append($"FT Qol/Qol Faizi =>  {shortAverage.FT_GG.ToResponseBothGoalVisualise()}\n");
                strBuilder2.Append($"FT HOME 0,5 Üst =>  {shortAverage.Home_FT_05_Over.ToResponseOverVisualise()}\n");
                strBuilder2.Append($"FT AWAY 0,5 Üst =>  {shortAverage.Away_FT_05_Over.ToResponseOverVisualise()}\n");
                strBuilder2.Append($"HT HOME 0,5 Üst =>  {shortAverage.Home_HT_05_Over.ToResponseOverVisualise()}\n");
                strBuilder2.Append($"HT AWAY 0,5 Üst =>  {shortAverage.Away_HT_05_Over.ToResponseOverVisualise()}\n");
                strBuilder2.Append($"SH HOME 0,5 Üst =>  {shortAverage.Home_SH_05_Over.ToResponseOverVisualise()}\n");
                strBuilder2.Append($"SH AWAY 0,5 Üst =>  {shortAverage.Away_SH_05_Over.ToResponseOverVisualise()}\n");
                strBuilder2.Append($"\n========================\n");

                var cntStr2 = strBuilder2.ToString();

                var strBuilder3 = new StringBuilder();
                var shortAverageHomeAway = new AverageShort(bet.AnalyseModel.AverageProfilerHomeAway);
                strBuilder3.Append($"=== EV - SƏFƏR ===\n");
                strBuilder3.Append($"-------------\n");
                strBuilder3.Append($"FT Home Qol-Ort. =>  {shortAverageHomeAway.Average_FT_Goals_HomeTeam}\n");
                strBuilder3.Append($"FT Away Qol-Ort. =>  {shortAverageHomeAway.Average_FT_Goals_AwayTeam}\n");
                strBuilder3.Append($"HT Home Qol-Ort. =>  {shortAverageHomeAway.Average_HT_Goals_HomeTeam}\n");
                strBuilder3.Append($"HT Away Qol-Ort. =>  {shortAverageHomeAway.Average_HT_Goals_AwayTeam}\n");
                strBuilder3.Append($"SH Home Qol-Ort. =>  {shortAverageHomeAway.Average_SH_Goals_HomeTeam}\n");
                strBuilder3.Append($"SH Away Qol-Ort. =>  {shortAverageHomeAway.Average_SH_Goals_AwayTeam}\n");
                strBuilder3.Append($"FT 1,5 Alt/Üst =>  {shortAverageHomeAway.FT_15_Over.ToResponseOverVisualise()}\n");
                strBuilder3.Append($"FT 2,5 Alt/Üst =>  {shortAverageHomeAway.FT_25_Over.ToResponseOverVisualise()}\n");
                strBuilder3.Append($"FT 3,5 Alt/Üst =>  {shortAverageHomeAway.FT_35_Over.ToResponseOverVisualise()}\n");
                strBuilder3.Append($"HT 0,5 Alt/Üst =>  {shortAverageHomeAway.HT_05_Over.ToResponseOverVisualise()}\n");
                strBuilder3.Append($"SH 0,5 Alt/Üst =>  {shortAverageHomeAway.SH_05_Over.ToResponseOverVisualise()}\n");
                strBuilder3.Append($"FT Qol/Qol Faizi =>  {shortAverageHomeAway.FT_GG.ToResponseBothGoalVisualise()}\n");
                strBuilder3.Append($"FT HOME 0,5 Üst =>  {shortAverageHomeAway.Home_FT_05_Over.ToResponseOverVisualise()}\n");
                strBuilder3.Append($"FT AWAY 0,5 Üst =>  {shortAverageHomeAway.Away_FT_05_Over.ToResponseOverVisualise()}\n");
                strBuilder3.Append($"HT HOME 0,5 Üst =>  {shortAverageHomeAway.Home_HT_05_Over.ToResponseOverVisualise()}\n");
                strBuilder3.Append($"HT AWAY 0,5 Üst =>  {shortAverageHomeAway.Away_HT_05_Over.ToResponseOverVisualise()}\n");
                strBuilder3.Append($"SH HOME 0,5 Üst =>  {shortAverageHomeAway.Home_SH_05_Over.ToResponseOverVisualise()}\n");
                strBuilder3.Append($"SH AWAY 0,5 Üst =>  {shortAverageHomeAway.Away_SH_05_Over.ToResponseOverVisualise()}\n");
                strBuilder3.Append($"\n========================");

                var cntStr3 = strBuilder3.ToString();
                strBuilder.Append(cntStr2);
                strBuilder.Append(cntStr3);
                _botService.SendMessage(strBuilder.ToString());
            }

            base.ExecuteTTT2(serials, path, league, countryContainer);
        }

        #region BetResult Checker Area
        private bool CheckIsOk(ResultModel betResult)
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
                        awaySide.Add(new AwayHomeSide
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
                    else if (awaySide[awaySide.Count - 1].IsLost && homeSide[homeSide.Count - 1].IsWin)
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
        private bool CheckFT25OverNisbi(JobAnalyseModelNisbi item, string contentString, LeagueHolder leagueHolder)
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
                        awaySide.Add(new AwayHomeSide
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
                    else if (awaySide[awaySide.Count - 1].IsLost && homeSide[homeSide.Count - 1].IsWin)
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
        private bool CheckFT15OverNisbi(JobAnalyseModelNisbi item, string contentString, LeagueHolder leagueHolder)
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
                            Is35Over = Convert.ToInt32(resScore.Split("-")[0].Trim()) + Convert.ToInt32(resScore.Split("-")[1].Trim()) > 3,

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
        private bool CheckFT35UnderNisbi(JobAnalyseModelNisbi item, string contentString, LeagueHolder leagueHolder)
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
                            Is35Over = Convert.ToInt32(resScore.Split("-")[0].Trim()) + Convert.ToInt32(resScore.Split("-")[1].Trim()) > 3,

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
        private bool CheckHT15UnderNisbi(JobAnalyseModelNisbi item, LeagueHolder leagueHolder)
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
        private bool CheckHT05OverNisbi(JobAnalyseModelNisbi item, LeagueHolder leagueHolder)
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
        private bool CheckSH05OverNisbi(JobAnalyseModelNisbi item, LeagueHolder leagueHolder)
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
                bool isValidImportant = leagueHolder.GG_Percentage < item.AverageProfilerHomeAway.FT_GG.Percentage && item.AverageProfilerHomeAway.FT_GG.FeatureName.ToLower() == "true" &&
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
        private bool CheckFT_GGNisbi(JobAnalyseModelNisbi item, string contentString, LeagueHolder leagueHolder)
        {
            bool result = false;

            try
            {
                bool isValidImportant = leagueHolder.GG_Percentage < item.AverageProfilerHomeAway.FT_GG.Percentage && item.AverageProfilerHomeAway.FT_GG.FeatureName.ToLower() == "true" &&
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
                        awaySide.Add(new AwayHomeSide
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
        private bool CheckFT_X1Nisbi(JobAnalyseModelNisbi item, string contentString, LeagueHolder leagueHolder)
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
                        awaySide.Add(new AwayHomeSide
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

                // TODO : LOOK

                bool isValid =
                item.AverageProfilerHomeAway.FT_Result.Percentage >= 50 &&
                item.AverageProfilerHomeAway.FT_Result.FeatureName.ToLower() != "1" &&
                item.AverageProfiler.FT_Result.Percentage >= 50 &&
                item.AverageProfiler.FT_Result.FeatureName.ToLower() != "1";

                if (isValid)
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
                        awaySide.Add(new AwayHomeSide
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
        private bool CheckFT_X2Nisbi(JobAnalyseModelNisbi item, string contentString, LeagueHolder leagueHolder)
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

                // TODO : LOOK

                bool isValid =
                item.AverageProfilerHomeAway.FT_Result.Percentage >= 50 &&
                item.AverageProfilerHomeAway.FT_Result.FeatureName.ToLower() != "1" &&
                item.AverageProfiler.FT_Result.Percentage >= 50 &&
                item.AverageProfiler.FT_Result.FeatureName.ToLower() != "1";

                if (isValid)
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
                        awaySide.Add(new AwayHomeSide
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
        private bool CheckHT_X1Nisbi(JobAnalyseModelNisbi item, string contentString, LeagueHolder leagueHolder)
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
        private bool CheckHT_X2Nisbi(JobAnalyseModelNisbi item, string contentString, LeagueHolder leagueHolder)
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



        public override void ExecuteTTT(List<string> serials, Dictionary<string, string> path, CountryContainerTemp countryContainer, LeagueContainer leagueContainer)
        {
            List<JobAnalyseModel> responseProfiler;
            string content;
            using (var reader = new StreamReader(path["responseProfilerTemp"]))
            {
                content = reader.ReadToEnd();
                if (content.Length < 10)
                {
                    responseProfiler = OperationalProcessor.GetJobAnalyseModelResultTest7777(_matchBetService, _filterResultService, _containerTemp, leagueContainer, serials).Where(x => x.AverageProfiler != null && x.AverageProfilerHomeAway != null).ToList();
                }
                else
                {
                    responseProfiler = JsonConvert.DeserializeObject<List<JobAnalyseModel>>(content);
                }
            }

            if(content.Length < 10)
            {
                using (var writer = new StreamWriter(path["responseProfilerTemp"]))
                {
                    writer.Write(JsonConvert.SerializeObject(responseProfiler));
                }
            }

            if (responseProfiler == null) responseProfiler = new List<JobAnalyseModel>();

            int failed = 0;
            var rgxLeague = new Regex(PatternConstant.StartedMatchPattern.League);
            var rgxCountry = new Regex(PatternConstant.StartedMatchPattern.Country);
            var rgxLeague2 = new Regex(PatternConstant.StartedMatchPattern.CountryAndLeague);
            int iteration = 0;

            foreach (var item in responseProfiler)
            {
                iteration++;
                try
                {
                    var contentString = _webHelper.GetMinifiedString($"https://arsiv.mackolik.com/Match/Default.aspx?id={item.HomeTeam_FormPerformanceGuessContainer.Serial}#karsilastirma");
                    string leagueName = contentString.ResolveLeagueByRegex(countryContainer, rgxLeague2, rgxLeague);
                    string countryName = contentString.ResolveCountryByRegex(countryContainer, rgxLeague2, rgxCountry);
                    var leagueHolder = leagueContainer.LeagueHolders.FirstOrDefault(x => x.Country.ToLower() == countryName.ToLower() && x.League.ToLower() == leagueName.ToLower());

                    if (leagueHolder == null)
                    {
                        continue;
                    }

                    var strBuilder1 = new StringBuilder();
                    strBuilder1.Append("BET DETAILS\n");
                    strBuilder1.Append($"LINK:  https://arsiv.mackolik.com/Match/Default.aspx?id={item.ComparisonInfoContainer.Serial}\n");
                    strBuilder1.Append($"MATCH:  {item.ComparisonInfoContainer.Home} - {item.ComparisonInfoContainer.Away}\n");
                    strBuilder1.Append($"COUNTRY:  {countryName}\n");
                    strBuilder1.Append($"LEAGUE:  {leagueName} \n");
                    strBuilder1.Append($"=== LİG Məlumatları ===\n");
                    strBuilder1.Append($"-----------------------\n");
                    strBuilder1.Append($"Tapılan Oyun Sayı => {leagueHolder.CountFound}\n");
                    strBuilder1.Append($"FT Qol-Ort. => {leagueHolder.GoalsAverage.ToString("0.00")}\n");
                    strBuilder1.Append($"HT Qol-Ort. => {leagueHolder.HT_GoalsAverage.ToString("0.00")}\n");
                    strBuilder1.Append($"SH Qol-Ort. => {leagueHolder.SH_GoalsAverage.ToString("0.00")}\n");
                    strBuilder1.Append($"FT Qol/Qol Faizi => {leagueHolder.GG_Percentage.ToResponseBothGoalVisualise()}\n");
                    strBuilder1.Append($"FT 1,5 Alt/Üst => {leagueHolder.Over_1_5_Percentage.ToResponseOverVisualise()}\n");
                    strBuilder1.Append($"FT 2,5 Alt/Üst => {leagueHolder.Over_2_5_Percentage.ToResponseOverVisualise()}\n");
                    strBuilder1.Append($"HT 0,5 Alt/Üst => {leagueHolder.HT_Over_0_5_Percentage.ToResponseOverVisualise()}\n");
                    strBuilder1.Append($"SH 0,5 Alt/Üst => {leagueHolder.SH_Over_0_5_Percentage.ToResponseOverVisualise()}\n");
                    strBuilder1.Append($"========================\n");

                    var strBuilder2 = new StringBuilder();
                    var shortAverage = new AverageShort(item.AverageProfiler);
                    strBuilder2.Append($"=== ÜMUMİ ===\n");
                    strBuilder2.Append($"-------------\n");
                    strBuilder2.Append($"FT Home Qol-Ort. =>  {shortAverage.Average_FT_Goals_HomeTeam}\n");
                    strBuilder2.Append($"FT Away Qol-Ort. =>  {shortAverage.Average_FT_Goals_AwayTeam}\n");
                    strBuilder2.Append($"HT Home Qol-Ort. =>  {shortAverage.Average_HT_Goals_HomeTeam}\n");
                    strBuilder2.Append($"HT Away Qol-Ort. =>  {shortAverage.Average_HT_Goals_AwayTeam}\n");
                    strBuilder2.Append($"SH Home Qol-Ort. =>  {shortAverage.Average_SH_Goals_HomeTeam}\n");
                    strBuilder2.Append($"SH Away Qol-Ort. =>  {shortAverage.Average_SH_Goals_AwayTeam}\n");
                    strBuilder2.Append($"FT Win1 =>  {shortAverage.Is_FT_Win1.ToResponseWinLoseVisualise()}\n");
                    strBuilder2.Append($"FT X =>  {shortAverage.Is_FT_X.ToResponseWinLoseVisualise()}\n");
                    strBuilder2.Append($"FT Win2 =>  {shortAverage.Is_FT_Win2.ToResponseWinLoseVisualise()}\n");
                    strBuilder2.Append($"HT Win1 =>  {shortAverage.Is_HT_Win1.ToResponseWinLoseVisualise()}\n");
                    strBuilder2.Append($"HT X =>  {shortAverage.Is_HT_X.ToResponseWinLoseVisualise()}\n");
                    strBuilder2.Append($"HT Win2 =>  {shortAverage.Is_HT_Win2.ToResponseWinLoseVisualise()}\n");
                    strBuilder2.Append($"FT 1,5 Alt/Üst =>  {shortAverage.FT_15_Over.ToResponseOverVisualise()}\n");
                    strBuilder2.Append($"FT 2,5 Alt/Üst =>  {shortAverage.FT_25_Over.ToResponseOverVisualise()}\n");
                    strBuilder2.Append($"HT 0,5 Alt/Üst =>  {shortAverage.HT_05_Over.ToResponseOverVisualise()}\n");
                    strBuilder2.Append($"SH 0,5 Alt/Üst =>  {shortAverage.SH_05_Over.ToResponseOverVisualise()}\n");
                    strBuilder2.Append($"FT Qol/Qol Faizi =>  {shortAverage.FT_GG.ToResponseBothGoalVisualise()}\n");
                    strBuilder2.Append($"FT HOME 0,5 Üst =>  {shortAverage.Home_FT_05_Over.ToResponseOverVisualise()}\n");
                    strBuilder2.Append($"FT AWAY 0,5 Üst =>  {shortAverage.Away_FT_05_Over.ToResponseOverVisualise()}\n");
                    strBuilder2.Append($"HT HOME 0,5 Üst =>  {shortAverage.Home_HT_05_Over.ToResponseOverVisualise()}\n");
                    strBuilder2.Append($"HT AWAY 0,5 Üst =>  {shortAverage.Away_HT_05_Over.ToResponseOverVisualise()}\n");
                    strBuilder2.Append($"SH HOME 0,5 Üst =>  {shortAverage.Home_SH_05_Over.ToResponseOverVisualise()}\n");
                    strBuilder2.Append($"SH AWAY 0,5 Üst =>  {shortAverage.Away_SH_05_Over.ToResponseOverVisualise()}\n");
                    strBuilder2.Append($"\n========================\n");

                    var cntStr2 = strBuilder2.ToString();

                    var strBuilder3 = new StringBuilder();
                    var shortAverageHomeAway = new AverageShort(item.AverageProfilerHomeAway);
                    strBuilder3.Append($"=== EV - SƏFƏR ===\n");
                    strBuilder3.Append($"-------------\n");
                    strBuilder3.Append($"FT Home Qol-Ort. =>  {shortAverageHomeAway.Average_FT_Goals_HomeTeam}\n");
                    strBuilder3.Append($"FT Away Qol-Ort. =>  {shortAverageHomeAway.Average_FT_Goals_AwayTeam}\n");
                    strBuilder3.Append($"HT Home Qol-Ort. =>  {shortAverageHomeAway.Average_HT_Goals_HomeTeam}\n");
                    strBuilder3.Append($"HT Away Qol-Ort. =>  {shortAverageHomeAway.Average_HT_Goals_AwayTeam}\n");
                    strBuilder3.Append($"SH Home Qol-Ort. =>  {shortAverageHomeAway.Average_SH_Goals_HomeTeam}\n");
                    strBuilder3.Append($"SH Away Qol-Ort. =>  {shortAverageHomeAway.Average_SH_Goals_AwayTeam}\n");
                    strBuilder3.Append($"FT Win1 =>  {shortAverageHomeAway.Is_FT_Win1.ToResponseWinLoseVisualise()}\n");
                    strBuilder3.Append($"FT X =>  {shortAverageHomeAway.Is_FT_X.ToResponseWinLoseVisualise()}\n");
                    strBuilder3.Append($"FT Win2 =>  {shortAverageHomeAway.Is_FT_Win2.ToResponseWinLoseVisualise()}\n");
                    strBuilder3.Append($"HT Win1 =>  {shortAverageHomeAway.Is_HT_Win1.ToResponseWinLoseVisualise()}\n");
                    strBuilder3.Append($"HT X =>  {shortAverageHomeAway.Is_HT_X.ToResponseWinLoseVisualise()}\n");
                    strBuilder3.Append($"HT Win2 =>  {shortAverageHomeAway.Is_HT_Win2.ToResponseWinLoseVisualise()}\n");
                    strBuilder3.Append($"FT 1,5 Alt/Üst =>  {shortAverageHomeAway.FT_15_Over.ToResponseOverVisualise()}\n");
                    strBuilder3.Append($"FT 2,5 Alt/Üst =>  {shortAverageHomeAway.FT_25_Over.ToResponseOverVisualise()}\n");
                    strBuilder3.Append($"HT 0,5 Alt/Üst =>  {shortAverageHomeAway.HT_05_Over.ToResponseOverVisualise()}\n");
                    strBuilder3.Append($"SH 0,5 Alt/Üst =>  {shortAverageHomeAway.SH_05_Over.ToResponseOverVisualise()}\n");
                    strBuilder3.Append($"FT Qol/Qol Faizi =>  {shortAverageHomeAway.FT_GG.ToResponseBothGoalVisualise()}\n");
                    strBuilder3.Append($"FT HOME 0,5 Üst =>  {shortAverageHomeAway.Home_FT_05_Over.ToResponseOverVisualise()}\n");
                    strBuilder3.Append($"FT AWAY 0,5 Üst =>  {shortAverageHomeAway.Away_FT_05_Over.ToResponseOverVisualise()}\n");
                    strBuilder3.Append($"HT HOME 0,5 Üst =>  {shortAverageHomeAway.Home_HT_05_Over.ToResponseOverVisualise()}\n");
                    strBuilder3.Append($"HT AWAY 0,5 Üst =>  {shortAverageHomeAway.Away_HT_05_Over.ToResponseOverVisualise()}\n");
                    strBuilder3.Append($"SH HOME 0,5 Üst =>  {shortAverageHomeAway.Home_SH_05_Over.ToResponseOverVisualise()}\n");
                    strBuilder3.Append($"SH AWAY 0,5 Üst =>  {shortAverageHomeAway.Away_SH_05_Over.ToResponseOverVisualise()}\n");
                    strBuilder3.Append($"\n========================");

                    var cntStr3 = strBuilder3.ToString();

                    strBuilder1.Append(cntStr2);
                    strBuilder1.Append(cntStr3);


                    var strBuilderCorner = new StringBuilder();

                    strBuilderCorner.Append($"=== CORNER Məlumatları ===\n");
                    strBuilderCorner.Append($"---- LIGA Məlumatları ----\n");
                    //strBuilderCorner.Append($"LINK:  https://arsiv.mackolik.com/Match/Default.aspx?id={item.ComparisonInfoContainer.Serial}\n");
                    //strBuilderCorner.Append($"MATCH:  {item.ComparisonInfoContainer.Home} - {item.ComparisonInfoContainer.Away}\n");
                    //strBuilderCorner.Append($"COUNTRY:  {countryName}\n");
                    //strBuilderCorner.Append($"LEAGUE:  {leagueName} \n");
                    strBuilderCorner.Append($"===========================\n");
                    strBuilderCorner.Append($"FT Corner-Ort. => {leagueHolder.CornerAverage.ToString("0.00")}\n");
                    strBuilderCorner.Append($"Home Corner-Ort. => {leagueHolder.HomeCornerAverage.ToString("0.00")}\n");
                    strBuilderCorner.Append($"Away Corner-Ort. => {leagueHolder.AwayCornerAverage.ToString("0.00")}\n");
                    strBuilderCorner.Append($"Corner 7,5 Alt/Üst => {leagueHolder.Corner_Over_7_5_Percentage.ToResponseOverVisualise()}\n");
                    strBuilderCorner.Append($"Corner 8,5 Alt/Üst => {leagueHolder.Corner_Over_8_5_Percentage.ToResponseOverVisualise()}\n");
                    strBuilderCorner.Append($"Corner 9,5 Alt/Üst => {leagueHolder.Corner_Over_9_5_Percentage.ToResponseOverVisualise()}\n");
                    strBuilderCorner.Append($"Home Corner 3,5 Alt/Üst => {leagueHolder.Home_Corner_35_Over_Percentage.ToResponseOverVisualise()}\n");
                    strBuilderCorner.Append($"Home Corner 4,5 Alt/Üst => {leagueHolder.Home_Corner_45_Over_Percentage.ToResponseOverVisualise()}\n");
                    strBuilderCorner.Append($"Home Corner 5,5 Alt/Üst => {leagueHolder.Home_Corner_55_Over_Percentage.ToResponseOverVisualise()}\n");
                    strBuilderCorner.Append($"Away Corner 3,5 Alt/Üst => {leagueHolder.Away_Corner_35_Over_Percentage.ToResponseOverVisualise()}\n");
                    strBuilderCorner.Append($"Away Corner 4,5 Alt/Üst => {leagueHolder.Away_Corner_45_Over_Percentage.ToResponseOverVisualise()}\n");
                    strBuilderCorner.Append($"Away Corner 5,5 Alt/Üst => {leagueHolder.Away_Corner_55_Over_Percentage.ToResponseOverVisualise()}\n");
                    strBuilderCorner.Append($"Corner FT Win1 => {leagueHolder.Corner_FT_Win1_Percentage.ToResponseWinLoseVisualise()}\n");
                    strBuilderCorner.Append($"Corner FT X => {leagueHolder.Corner_FT_X_Percentage.ToResponseWinLoseVisualise()}\n");
                    strBuilderCorner.Append($"Corner FT Win2 => {leagueHolder.Corner_FT_Win2_Percentage.ToResponseWinLoseVisualise()}\n");
                    strBuilderCorner.Append($"========================\n");

                    var strBuild11 = new StringBuilder();
                    strBuild11.Append($"=== ÜMUMİ ===\n");
                    strBuild11.Append($"-------------\n");
                    strBuild11.Append($"FT Home Corner-Ort. =>  {shortAverage.Average_FT_Corners_HomeTeam}\n");
                    strBuild11.Append($"FT Away Corner-Ort. =>  {shortAverage.Average_FT_Corners_AwayTeam}\n");
                    strBuild11.Append($"FT Corner 7,5 Alt/Üst =>  {shortAverage.Corner_7_5_Over.ToResponseOverVisualise("shortAverage.Corner_7_5_Over")}\n");
                    strBuild11.Append($"FT Corner 8,5 Alt/Üst =>  {shortAverage.Corner_8_5_Over.ToResponseOverVisualise("shortAverage.Corner_8_5_Over")}\n");
                    strBuild11.Append($"FT Corner 9,5 Alt/Üst =>  {shortAverage.Corner_9_5_Over.ToResponseOverVisualise("shortAverage.Corner_9_5_Over")}\n");
                    strBuild11.Append($"------------------------\n");
                    strBuild11.Append($"FT HOME Corner 3,5 Üst =>  {shortAverage.Corner_Home_3_5_Over.ToResponseOverVisualise("shortAverage.Corner_Home_3_5_Over")}\n");
                    strBuild11.Append($"FT Away Corner 3,5 Üst =>  {shortAverage.Corner_Away_3_5_Over.ToResponseOverVisualise("shortAverage.Corner_Away_3_5_Over")}\n");
                    strBuild11.Append($"------------------------\n");
                    strBuild11.Append($"FT HOME Corner 4,5 Üst =>  {shortAverage.Corner_Home_4_5_Over.ToResponseOverVisualise("shortAverage.Corner_Home_4_5_Over")}\n");
                    strBuild11.Append($"FT Away Corner 4,5 Üst =>  {shortAverage.Corner_Away_4_5_Over.ToResponseOverVisualise("shortAverage.Corner_Away_4_5_Over")}\n");
                    strBuild11.Append($"------------------------\n");
                    strBuild11.Append($"FT HOME Corner 5,5 Üst =>  {shortAverage.Corner_Home_5_5_Over.ToResponseOverVisualise("shortAverage.Corner_Home_5_5_Over")}\n");
                    strBuild11.Append($"FT Away Corner 5,5 Üst =>  {shortAverage.Corner_Away_5_5_Over.ToResponseOverVisualise("shortAverage.Corner_Away_5_5_Over")}\n");
                    strBuild11.Append($"Corner FT Win 1 =>  {shortAverage.Is_Corner_FT_Win1.ToResponseWinLoseVisualise("shortAverage.Is_Corner_FT_Win1")}\n");
                    strBuild11.Append($"Corner FT X =>  {shortAverage.Is_Corner_FT_X.ToResponseWinLoseVisualise("shortAverage.Is_Corner_FT_X")}\n");
                    strBuild11.Append($"Corner FT Win 2 =>  {shortAverage.Is_Corner_FT_Win2.ToResponseWinLoseVisualise("shortAverage.Is_Corner_FT_Win2")}\n");
                    strBuild11.Append($"\n========================\n");
                    strBuilderCorner.Append(strBuild11.ToString());
                    var strBuild22 = new StringBuilder();
                    strBuild22.Append($"=== EV - SƏFƏR ===\n");
                    strBuild22.Append($"-------------\n");
                    strBuild22.Append($"FT Home Corner-Ort. =>  {shortAverageHomeAway.Average_FT_Corners_HomeTeam}\n");
                    strBuild22.Append($"FT Away Corner-Ort. =>  {shortAverageHomeAway.Average_FT_Corners_AwayTeam}\n");
                    strBuild22.Append($"FT Corner 7,5 Alt/Üst =>  {shortAverageHomeAway.Corner_7_5_Over.ToResponseOverVisualise("shortAverageHomeAway.Corner_7_5_Over")}\n");
                    strBuild22.Append($"FT Corner 8,5 Alt/Üst =>  {shortAverageHomeAway.Corner_8_5_Over.ToResponseOverVisualise("shortAverageHomeAway.Corner_8_5_Over")}\n");
                    strBuild22.Append($"FT Corner 9,5 Alt/Üst =>  {shortAverageHomeAway.Corner_9_5_Over.ToResponseOverVisualise("shortAverageHomeAway.Corner_9_5_Over")}\n");
                    strBuild22.Append($"------------------------\n");
                    strBuild22.Append($"FT HOME Corner 3,5 Üst =>  {shortAverageHomeAway.Corner_Home_3_5_Over.ToResponseOverVisualise("shortAverageHomeAway.Corner_Home_3_5_Over")}\n");
                    strBuild22.Append($"FT Away Corner 3,5 Üst =>  {shortAverageHomeAway.Corner_Away_3_5_Over.ToResponseOverVisualise("shortAverageHomeAway.Corner_Away_3_5_Over")}\n");
                    strBuild22.Append($"------------------------\n");
                    strBuild22.Append($"FT HOME Corner 4,5 Üst =>  {shortAverageHomeAway.Corner_Home_4_5_Over.ToResponseOverVisualise("shortAverageHomeAway.Corner_Home_4_5_Over")}\n");
                    strBuild22.Append($"FT Away Corner 4,5 Üst =>  {shortAverageHomeAway.Corner_Away_4_5_Over.ToResponseOverVisualise("shortAverageHomeAway.Corner_Away_4_5_Over")}\n");
                    strBuild22.Append($"------------------------\n");
                    strBuild22.Append($"FT HOME Corner 5,5 Üst =>  {shortAverageHomeAway.Corner_Home_5_5_Over.ToResponseOverVisualise("shortAverageHomeAway.Corner_Home_5_5_Over")}\n");
                    strBuild22.Append($"FT Away Corner 5,5 Üst =>  {shortAverageHomeAway.Corner_Away_5_5_Over.ToResponseOverVisualise("shortAverageHomeAway.Corner_Away_5_5_Over")}\n");
                    strBuild22.Append($"Corner FT Win 1 =>  {shortAverageHomeAway.Is_Corner_FT_Win1.ToResponseWinLoseVisualise("shortAverageHomeAway.Is_Corner_FT_Win1")}\n");
                    strBuild22.Append($"Corner FT X =>  {shortAverageHomeAway.Is_Corner_FT_X.ToResponseWinLoseVisualise("shortAverageHomeAway.Is_Corner_FT_X")}\n");
                    strBuild22.Append($"Corner FT Win 2 =>  {shortAverageHomeAway.Is_Corner_FT_Win2.ToResponseWinLoseVisualise("shortAverageHomeAway.Is_Corner_FT_Win2")}\n");
                    strBuild22.Append($"\n========================\n");
                    strBuilderCorner.Append(strBuild22.ToString());

                    //_botService.SendMessage(1093967965, strBuilder1.ToString());
                    //_botService.SendMessage(5532339449, strBuilder1.ToString());
                    _botService.SendMessage(5065439386, strBuilder1.ToString());

                    if (shortAverage.Corner_8_5_Over == null || shortAverage.Corner_8_5_Over == null ||
                        shortAverageHomeAway.Corner_8_5_Over == null || shortAverageHomeAway.Corner_8_5_Over == null)
                    {
                        continue;
                    }

                    if (shortAverage.Corner_8_5_Over.Percentage < -100 || shortAverage.Corner_8_5_Over.Percentage > 200 ||
                        shortAverageHomeAway.Corner_8_5_Over.Percentage < -100 || shortAverageHomeAway.Corner_8_5_Over.Percentage > 200)
                    {
                        continue;
                    }

                    try
                    {
                        decimal ftHomeAverage = Convert.ToDecimal(shortAverage.Average_FT_Corners_HomeTeam);
                        decimal ftAwayAverage = Convert.ToDecimal(shortAverage.Average_FT_Corners_AwayTeam);
                        decimal ftBeSideHomeAverage = Convert.ToDecimal(shortAverageHomeAway.Average_FT_Corners_HomeTeam);
                        decimal ftBeSideAwayAverage = Convert.ToDecimal(shortAverageHomeAway.Average_FT_Corners_AwayTeam);
                        if (ftHomeAverage < 0 || ftAwayAverage < 0 || ftBeSideHomeAverage < 0 || ftBeSideAwayAverage < 0)
                        {
                            continue;
                        }
                    }
                    catch (Exception)
                    {
                        continue;
                    }

                    //_botService.SendMessage(5532339449, strBuilderCorner.ToString());
                    //_botService.SendMessage(1093967965, strBuilderCorner.ToString());
                    _botService.SendMessage(5065439386, strBuilderCorner.ToString());
                }
                catch (Exception ex)
                {
                    failed++;
                    continue;
                }
            }
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
