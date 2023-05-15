using Core.Entities.Concrete;
using Core.Entities.Concrete.ComplexModels.ML;
using Core.Entities.Concrete.ExternalDbEntities;
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
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SBA.Business.Abstract;
using SBA.Business.BusinessHelper;
using SBA.Business.ExternalServices;
using SBA.Business.ExternalServices.Abstract;
using SBA.Business.ExternalServices.ChatGPT;
using SBA.Business.FunctionalServices.Concrete;
using SBA.MvcUI.Models.SettingsModels;
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
        public bool IsHT_Win1 { get; set; }
        public bool IsHT_Win2 { get; set; }
        public bool IsSH_Win1 { get; set; }
        public bool IsSH_Win2 { get; set; }
        public bool IsFT_35Under { get; set; }
        public bool IsHT_15Under { get; set; }
        public bool IsHomeWillGoal { get; set; }
        public bool IsAwayWillGoal { get; set; }

        public JobAnalyseModel AnalyseModel { get; set; }
        public JobAnalyseModelNisbi AnalyseModelNisbi { get; set; }
    }

    public class JobOperation : BaseJobOperation
    {
        private ISocialBotMessagingService _botService;
        private ILeagueStatisticsHolderService _leagueStatisticsHolderService;
        private IComparisonStatisticsHolderService _comparisonStatisticsHolderService;
        private IAverageStatisticsHolderService _averageStatisticsHolderService;
        private ITeamPerformanceStatisticsHolderService _teamPerformanceStatisticsHolderService;
        private IMatchIdentifierService _matchIdentifierService;
        private IStatisticInfoHolderService _statisticInfoHolderService;
        private IForecastService _forecastService;
        private IAiDataHolderService _aiDataHolderService;
        private List<TimeSerialContainer> _timeSerials;
        private List<TimeSerialContainer> _analysableSerials = new List<TimeSerialContainer>();
        private readonly IMatchBetService _matchBetService;
        private readonly IFilterResultService _filterResultService;
        private readonly SystemCheckerContainer _systemCheckerContainer;
        private readonly DescriptionJobResultEnum _descriptionJobResultEnum;
        private readonly IConfiguration _configuration;
        private int _addMinute;
        private readonly HttpClient _client;
        private readonly WebOperation _webHelper;
        private readonly ChatGPTService _chatGPTService;

        private CountryContainerTemp _containerTemp;

        public JobOperation(ISocialBotMessagingService botService,
                            List<TimeSerialContainer> timeSerials,
                            IMatchBetService matchBetService,
                            IFilterResultService filterResultService,
                            SystemCheckerContainer systemCheckerContainer,
                            DescriptionJobResultEnum descriptionJobResultEnum,
                            CountryContainerTemp containerTemp,
                            ILeagueStatisticsHolderService leagueStatisticsHolderService,
                            IComparisonStatisticsHolderService comparisonStatisticsHolderService,
                            IAverageStatisticsHolderService averageStatisticsHolderService,
                            ITeamPerformanceStatisticsHolderService teamPerformanceStatisticsHolderService,
                            IMatchIdentifierService matchIdentifierService,
                            IStatisticInfoHolderService statisticInfoHolderService,
                            IAiDataHolderService aiDataHolderService,
                            IForecastService forecastService,
                            IConfiguration configuration,
                            int addMinute = 3)
        {
            _matchBetService = matchBetService;
            _filterResultService = filterResultService;
            _leagueStatisticsHolderService = leagueStatisticsHolderService;
            _comparisonStatisticsHolderService = comparisonStatisticsHolderService;
            _averageStatisticsHolderService = averageStatisticsHolderService;
            _teamPerformanceStatisticsHolderService = teamPerformanceStatisticsHolderService;
            _matchIdentifierService = matchIdentifierService;
            _statisticInfoHolderService = statisticInfoHolderService;
            _aiDataHolderService = aiDataHolderService;
            _forecastService = forecastService;
            _configuration = configuration;
            _systemCheckerContainer = systemCheckerContainer;
            _addMinute = addMinute;
            _botService = botService;
            _timeSerials = timeSerials;
            _descriptionJobResultEnum = descriptionJobResultEnum;
            _containerTemp = containerTemp;
            _client = new HttpClient();
            _webHelper = new WebOperation();
            string apiKey = _configuration.GetValue<string>("OpenAI-SecretKey");
            _chatGPTService = new ChatGPTService(apiKey);
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

        public override void ExecuteNisbi(List<string> serials, Dictionary<string, string> path, CountryContainerTemp countryContainer, LeagueContainer league)
        {
            List<JobAnalyseModelNisbi> responseProfiler;

            string content;
            using (var reader = new StreamReader(path["responseProfilerTempNisbi"]))
            {
                content = reader.ReadToEnd();
                if (content.Length < 10)
                {
                    responseProfiler = OperationalProcessor.GetJobAnalyseModelResultNisbi(_matchBetService, _filterResultService, _containerTemp, league, serials).Where(x => x.AverageProfiler != null && x.AverageProfilerHomeAway != null).ToList();
                }
                else
                {
                    responseProfiler = JsonConvert.DeserializeObject<List<JobAnalyseModelNisbi>>(content);
                }
            }

            if (content.Length < 10)
            {
                using (var writer = new StreamWriter(path["responseProfilerTempNisbi"]))
                {
                    writer.Write(JsonConvert.SerializeObject(responseProfiler));
                }
            }

            if (responseProfiler == null) responseProfiler = new List<JobAnalyseModelNisbi>();

            var rgxOdd35U = new Regex(PatternConstant.StartedMatchPattern.FT_2_5_Over);
            var rgxLeague = new Regex(PatternConstant.StartedMatchPattern.League);
            var rgxCountry = new Regex(PatternConstant.StartedMatchPattern.Country);
            var rgxLeague2 = new Regex(PatternConstant.StartedMatchPattern.CountryAndLeague);

            var responsesBet = new List<ResultModel>();

            int iteration = 0;

            foreach (var item in responseProfiler)
            {
                iteration++;

                var contentString = _webHelper.GetMinifiedString($"http://arsiv.mackolik.com/Match/Default.aspx?id={item.HomeTeam_FormPerformanceGuessContainer.Serial}#karsilastirma");
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
                        //IsHT_Win1 = CheckHT_Win1(item, contentString, leagueHolder),
                        //IsHT_Win2 = CheckHT_Win2(item, contentString, leagueHolder),
                        //IsSH_Win1 = CheckSH_Win1(item, contentString, leagueHolder),
                        //IsSH_Win2 = CheckSH_Win2(item, contentString, leagueHolder),
                        IsFT_25Over = CheckFT25OverNisbi(item, contentString, leagueHolder),
                        IsFT_GG = CheckFT_GGNisbi(item, contentString, leagueHolder),
                        IsFT_15Over = CheckFT15OverNisbi(item, contentString, leagueHolder),
                        IsHT_05Over = CheckHT05OverNisbi(item, leagueHolder),
                        AnalyseModelNisbi = item
                    };

                    if (CheckIsOk(match))
                        responsesBet.Add(match);
                }
            }

            foreach (var bet in responsesBet)
            {
                try
                {
                    _botService = new TelegramMessagingManager();
                    var contentString = _webHelper.GetMinifiedString($"http://arsiv.mackolik.com/Match/Default.aspx?id={bet.Serial}#karsilastirma");
                    string leagueName = contentString.ResolveLeagueByRegex(countryContainer, rgxLeague, rgxLeague2);
                    string countryName = contentString.ResolveCountryByRegex(countryContainer, rgxCountry, rgxLeague2);
                    var leagueHolder = league.LeagueHolders.FirstOrDefault(x => x.Country.ToLower() == countryName.ToLower() && x.League.ToLower() == leagueName.ToLower());

                    if (leagueHolder == null)
                    {
                        continue;
                    }

                    var strBuilder = new StringBuilder();
                    strBuilder.Append("BET Statistic\n");
                    strBuilder.Append($"LINK: http://arsiv.mackolik.com/Match/Default.aspx?id={bet.Serial}\n");
                    strBuilder.Append($"__________________________\n");
                    strBuilder.Append($"COUNTRY: {bet.Country}\n");
                    strBuilder.Append($"LEAGUE: {bet.League}\n");
                    strBuilder.Append($"MATCH: {bet.Match}\n\n");
                    strBuilder.Append($"========================\n");
                    if (bet.IsFT_15Over) strBuilder.Append($"Forecast:  FT 1,5 Over\n");
                    if (bet.IsFT_25Over) strBuilder.Append($"Forecast:  FT 2,5 Over\n");
                    if (bet.IsFT_GG) strBuilder.Append($"Forecast:  FT GG\n");
                    if (bet.IsHT_05Over) strBuilder.Append($"Forecast:  HT 0,5 Over\n");
                    strBuilder.Append($"=================================");

                    _botService.SendNisbiMessage(strBuilder.ToString());
                }
                catch (Exception ex)
                {
                    var readyText = new StringBuilder();

                    using (var reader = new StreamReader(path["Report"]))
                    {
                        readyText.Append(reader.ReadToEnd());
                    }

                    using (var writer = new StreamWriter(path["Report"]))
                    {
                        readyText.Append("\n\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n////////////////////////////////////////\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n\n");
                        var text = $"OPERATION :  SENDING NISBI MESSAGE\n=============================================\n=============================================\nFAILED  SERIAL : {bet.Serial}";

                        readyText.Append(text);

                        writer.Write(readyText.ToString());
                    }

                    continue;
                }
            }

            base.ExecuteNisbi(serials, path, countryContainer, league);
        }


        public override void ExecuteTTT2(List<string> serials, Dictionary<string, string> path, CountryContainerTemp countryContainer, LeagueContainer league, UserCheck userCheck)
        {
            List<JobAnalyseModel> responseProfiler;

            string content;
            using (var reader = new StreamReader(path["responseProfilerTemp"]))
            {
                content = reader.ReadToEnd();
                if (content.Length < 10)
                {
                    responseProfiler = OperationalProcessor.GetJobAnalyseModelResultTest7777(_matchBetService, _filterResultService, _comparisonStatisticsHolderService, _averageStatisticsHolderService, _teamPerformanceStatisticsHolderService, _leagueStatisticsHolderService, _matchIdentifierService, _aiDataHolderService, _statisticInfoHolderService, _containerTemp, league, serials).Where(x => x.AverageProfiler != null && x.AverageProfilerHomeAway != null).ToList();
                }
                else
                {
                    responseProfiler = JsonConvert.DeserializeObject<List<JobAnalyseModel>>(content);
                }
            }

            if (content.Length < 10)
            {
                using (var writer = new StreamWriter(path["responseProfilerTemp"]))
                {
                    writer.Write(JsonConvert.SerializeObject(responseProfiler));
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

                var contentString = _webHelper.GetMinifiedString($"http://arsiv.mackolik.com/Match/Default.aspx?id={item.HomeTeam_FormPerformanceGuessContainer.Serial}#karsilastirma");
                string leagueName = contentString.ResolveLeagueByRegex(countryContainer, rgxLeague, rgxLeague2);
                string countryName = contentString.ResolveCountryByRegex(countryContainer, rgxCountry, rgxLeague2);
                var leagueHolder = league.LeagueHolders.FirstOrDefault(x => x.Country == countryName && x.League == leagueName);

                if (leagueHolder != null)
                {
                    bool ggResult = CheckFT_GG(item, item.ComparisonInfoContainer.Away, leagueHolder); // +
                    bool overResult = CheckFT25Over(item, contentString, leagueHolder, ggResult);

                    var match = new ResultModel
                    {
                        Serial = item.ComparisonInfoContainer.Serial,
                        Country = countryName,
                        League = leagueName,
                        Match = $"{item.ComparisonInfoContainer.Home} - {item.ComparisonInfoContainer.Away}",
                        IsHT_Win1 = CheckHT_Win1(item, contentString, leagueHolder, true),
                        IsHT_Win2 = CheckHT_Win2(item, contentString, leagueHolder, true),
                        IsSH_Win1 = CheckSH_Win1(item, contentString, leagueHolder, true),
                        IsSH_Win2 = CheckSH_Win2(item, contentString, leagueHolder, true),
                        IsFT_15Over = CheckFT15Over(item, contentString, leagueHolder, ggResult, overResult),
                        IsFT_25Over = overResult,
                        IsFT_GG = ggResult,
                        AnalyseModel = item
                    };

                    if (CheckIsOk(match))
                        responsesBet.Add(match);
                }
            }

            foreach (var bet in responsesBet)
            {
                int serial = Convert.ToInt32(bet.Serial);
                var match = _matchIdentifierService.Get(x => x.Serial == serial).Data;
                var aiStatisticsModel = _aiDataHolderService.Get(x => x.Serial == serial).Data;

                var aiAnalyseModel = JsonConvert.DeserializeObject<AiAnalyseModel>(aiStatisticsModel.JsonTextContent);

                if (aiAnalyseModel == null)
                    continue;

                aiAnalyseModel.AwayTeamPerformanceMatches = null;
                aiAnalyseModel.HomeTeamPerformanceMatches = null;
                aiAnalyseModel.ComparisonDataes = null;

                if (aiAnalyseModel.StatisticPercentageModel != null)
                {
                    aiAnalyseModel.StatisticPercentageModel.General_Form_Performance_Statistics_By_Last_10_Matches_Of_HomeTeam = null;
                    aiAnalyseModel.StatisticPercentageModel.General_Form_Performance_Statistics_By_Last_10_Matches_Of_AwayTeam = null;
                    aiAnalyseModel.StatisticPercentageModel.General_H2H_Comparison_Statistics_ByLast_10_Matches = null;
                }

                var serializerOptions = new JsonSerializerSettings
                {
                    Formatting = Formatting.None,
                    NullValueHandling = NullValueHandling.Ignore
                };

                string statisticsData = JsonConvert.SerializeObject(aiAnalyseModel, serializerOptions);

                var forecastCollection = new List<Forecast>();

                if (bet.IsHT_Win1)
                {
                    var aiResponse = Task.Run(() => _chatGPTService.CheckForecastAsync(statisticsData, "HT_Win_1_FC".TranslateResource(2))).Result;
                    if (aiResponse.ToUpper().Contains("TRUE200"))
                    {
                        forecastCollection.Add(new Forecast("HT_Win_1", match));
                    }
                }

                if (bet.IsHT_Win2)
                {
                    var aiResponse = Task.Run(() => _chatGPTService.CheckForecastAsync(statisticsData, "HT_Win_2_FC".TranslateResource(2))).Result;
                    if (aiResponse.ToUpper().Contains("TRUE200"))
                    {
                        forecastCollection.Add(new Forecast("HT_Win_2", match));
                    }
                }

                if (bet.IsSH_Win1)
                {
                    string aiResponse = Task.Run(() => _chatGPTService.CheckForecastAsync(statisticsData, "FT_15_O_FC".TranslateResource(2))).Result;
                    if (aiResponse.ToUpper().Contains("TRUE200"))
                    {
                        forecastCollection.Add(new Forecast("SH_Win_1", match));
                    }
                }

                if (bet.IsSH_Win2)
                {
                    var aiResponse = Task.Run(() => _chatGPTService.CheckForecastAsync(statisticsData, "SH_Win_2_FC".TranslateResource(2))).Result;
                    if (aiResponse.ToUpper().Contains("TRUE200"))
                    {
                        forecastCollection.Add(new Forecast("SH_Win_2", match));
                    }
                }

                if (bet.IsFT_15Over)
                {
                    var aiResponse = Task.Run(() => _chatGPTService.CheckForecastAsync(statisticsData, "FT_15_O_FC".TranslateResource(2))).Result;
                    if (aiResponse.ToUpper().Contains("TRUE200"))
                    {
                        forecastCollection.Add(new Forecast("FT_15_O", match));
                    }
                }

                if (bet.IsFT_25Over)
                {
                    var aiResponse = Task.Run(() => _chatGPTService.CheckForecastAsync(statisticsData, "FT_25_O_FC".TranslateResource(2))).Result;
                    if (aiResponse.ToUpper().Contains("TRUE200"))
                    {
                        forecastCollection.Add(new Forecast("FT_25_O", match));
                    }
                }

                if (bet.IsFT_GG)
                {
                    var aiResponse = Task.Run(() => _chatGPTService.CheckForecastAsync(statisticsData, "FT_GG_FC".TranslateResource(2))).Result;
                    if (aiResponse.ToUpper().Contains("TRUE200"))
                    {
                        forecastCollection.Add(new Forecast("FT_GG", match));
                    }
                }

                if (forecastCollection.Count > 0)
                {
                    _forecastService.AddRange(forecastCollection);
                }
            }

            //foreach (var bet in responsesBet)
            //{
            //    _botService = new TelegramMessagingManager();
            //    var contentString = _webHelper.GetMinifiedString($"http://arsiv.mackolik.com/Match/Default.aspx?id={bet.Serial}#karsilastirma");
            //    string leagueName = contentString.ResolveLeagueByRegex(countryContainer, rgxLeague, rgxLeague2);
            //    string countryName = contentString.ResolveCountryByRegex(countryContainer, rgxCountry, rgxLeague2);
            //    var leagueHolder = league.LeagueHolders.FirstOrDefault(x => x.Country.ToLower() == countryName.ToLower() && x.League.ToLower() == leagueName.ToLower());

            //    if (leagueHolder == null)
            //    {
            //        continue;
            //    }

            //    var strBuilder = new StringBuilder();
            //    strBuilder.Append("BET Statistic\n");
            //    strBuilder.Append($"LINK: http://arsiv.mackolik.com/Match/Default.aspx?id={bet.Serial}\n");
            //    strBuilder.Append($"__________________________\n");
            //    strBuilder.Append($"COUNTRY: {bet.Country}\n");
            //    strBuilder.Append($"LEAGUE: {bet.League}\n");
            //    strBuilder.Append($"MATCH: {bet.Match}\n\n");
            //    strBuilder.Append($"==========================\n");
            //    strBuilder.Append($"=== League Informations ===\n");
            //    strBuilder.Append($"\n");
            //    strBuilder.Append($"Found matchs count => {leagueHolder.CountFound}\n");
            //    strBuilder.Append($"FT Goals Average => {leagueHolder.GoalsAverage.ToString("0.00")}\n");
            //    strBuilder.Append($"HT Goals Average => {leagueHolder.HT_GoalsAverage.ToString("0.00")}\n");
            //    strBuilder.Append($"SH Goals Average => {leagueHolder.SH_GoalsAverage.ToString("0.00")}\n");
            //    strBuilder.Append($"\n");
            //    strBuilder.Append($"FT Both teams will score? => {leagueHolder.GG_Percentage.ToResponseBothGoalVisualise()}\n");
            //    strBuilder.Append($"FT 1,5 Under/Over => {leagueHolder.Over_1_5_Percentage.ToResponseOverVisualise()}\n");
            //    strBuilder.Append($"FT 2,5 Under/Over => {leagueHolder.Over_2_5_Percentage.ToResponseOverVisualise()}\n");
            //    strBuilder.Append($"\n");
            //    strBuilder.Append($"HT 0,5 Under/Over => {leagueHolder.HT_Over_0_5_Percentage.ToResponseOverVisualise()}\n");
            //    strBuilder.Append($"\n");
            //    strBuilder.Append($"SH 0,5 Under/Over => {leagueHolder.SH_Over_0_5_Percentage.ToResponseOverVisualise()}\n");
            //    strBuilder.Append($"========================\n");
            //    if (bet.IsHT_Win1) strBuilder.Append($"Forecast:  HT 1\n");
            //    if (bet.IsHT_Win2) strBuilder.Append($"Forecast:  HT 2\n");
            //    if (bet.IsSH_Win1) strBuilder.Append($"Forecast:  SH 1\n");
            //    if (bet.IsSH_Win2) strBuilder.Append($"Forecast:  SH 2\n");
            //    if (bet.IsFT_15Over) strBuilder.Append($"Forecast:  FT 1,5 Over\n");
            //    if (bet.IsFT_25Over) strBuilder.Append($"Forecast:  FT 2,5 Over\n");
            //    if (bet.IsFT_GG) strBuilder.Append($"Forecast:  FT G/G\n");
            //    strBuilder.Append($"\n");
            //    strBuilder.Append($"=================================\n");

            //    var strBuilder2 = new StringBuilder();
            //    var shortAverage = new AverageShort(bet.AnalyseModel.AverageProfiler);
            //    strBuilder2.Append($"=== General Statistic ===\n");
            //    strBuilder2.Append($"=================================\n");
            //    strBuilder2.Append($"\n");
            //    strBuilder2.Append($"FT Home Goals Average =>  {shortAverage.Average_FT_Goals_HomeTeam}\n");
            //    strBuilder2.Append($"FT Away Goals Average =>  {shortAverage.Average_FT_Goals_AwayTeam}\n");
            //    strBuilder2.Append($"\n");
            //    strBuilder2.Append($"HT Home Goals Average =>  {shortAverage.Average_HT_Goals_HomeTeam}\n");
            //    strBuilder2.Append($"HT Away Goals Average =>  {shortAverage.Average_HT_Goals_AwayTeam}\n");
            //    strBuilder2.Append($"\n");
            //    strBuilder2.Append($"SH Home Goals Average =>  {shortAverage.Average_SH_Goals_HomeTeam}\n");
            //    strBuilder2.Append($"SH Away Goals Average =>  {shortAverage.Average_SH_Goals_AwayTeam}\n");
            //    strBuilder2.Append($"\n");
            //    strBuilder2.Append($"FT Win1 =>  {shortAverage.Is_FT_Win1.ToResponseWinLoseVisualise()}\n");
            //    strBuilder2.Append($"FT X =>  {shortAverage.Is_FT_X.ToResponseWinLoseVisualise()}\n");
            //    strBuilder2.Append($"FT Win2 =>  {shortAverage.Is_FT_Win2.ToResponseWinLoseVisualise()}\n");
            //    strBuilder2.Append($"\n");
            //    strBuilder2.Append($"HT Win1 =>  {shortAverage.Is_HT_Win1.ToResponseWinLoseVisualise()}\n");
            //    strBuilder2.Append($"HT X =>  {shortAverage.Is_HT_X.ToResponseWinLoseVisualise()}\n");
            //    strBuilder2.Append($"HT Win2 =>  {shortAverage.Is_HT_Win2.ToResponseWinLoseVisualise()}\n");
            //    strBuilder2.Append($"\n");
            //    strBuilder2.Append($"SH Win1 =>  {shortAverage.Is_SH_Win1.ToResponseWinLoseVisualise()}\n");
            //    strBuilder2.Append($"SH X =>  {shortAverage.Is_SH_X.ToResponseWinLoseVisualise()}\n");
            //    strBuilder2.Append($"SH Win2 =>  {shortAverage.Is_SH_Win2.ToResponseWinLoseVisualise()}\n");
            //    strBuilder2.Append($"\n");
            //    strBuilder2.Append($"FT 1,5 Under/Over =>  {shortAverage.FT_15_Over.ToResponseOverVisualise()}\n");
            //    strBuilder2.Append($"FT 2,5 Under/Over =>  {shortAverage.FT_25_Over.ToResponseOverVisualise()}\n");
            //    strBuilder2.Append($"FT 3,5 Under/Over =>  {shortAverage.FT_35_Over.ToResponseOverVisualise()}\n");
            //    strBuilder2.Append($"\n");
            //    strBuilder2.Append($"HT 0,5 Under/Over =>  {shortAverage.HT_05_Over.ToResponseOverVisualise()}\n");
            //    strBuilder2.Append($"\n");
            //    strBuilder2.Append($"SH 0,5 Under/Over =>  {shortAverage.SH_05_Over.ToResponseOverVisualise()}\n");
            //    strBuilder2.Append($"\n");
            //    strBuilder2.Append($"FT Both teams will score? =>  {shortAverage.FT_GG.ToResponseBothGoalVisualise()}\n");
            //    strBuilder2.Append($"\n");
            //    strBuilder2.Append($"FT HOME 0,5 Over =>  {shortAverage.Home_FT_05_Over.ToResponseOverVisualise()}\n");
            //    strBuilder2.Append($"FT AWAY 0,5 Over =>  {shortAverage.Away_FT_05_Over.ToResponseOverVisualise()}\n");
            //    strBuilder2.Append($"\n");
            //    strBuilder2.Append($"HT HOME 0,5 Over =>  {shortAverage.Home_HT_05_Over.ToResponseOverVisualise()}\n");
            //    strBuilder2.Append($"HT AWAY 0,5 Over =>  {shortAverage.Away_HT_05_Over.ToResponseOverVisualise()}\n");
            //    strBuilder2.Append($"\n");
            //    strBuilder2.Append($"SH HOME 0,5 Over =>  {shortAverage.Home_SH_05_Over.ToResponseOverVisualise()}\n");
            //    strBuilder2.Append($"SH AWAY 0,5 Over =>  {shortAverage.Away_SH_05_Over.ToResponseOverVisualise()}\n");
            //    strBuilder2.Append($"\n");
            //    strBuilder2.Append($"\n===============================\n");

            //    var cntStr2 = strBuilder2.ToString();

            //    var strBuilder3 = new StringBuilder();
            //    var shortAverageHomeAway = new AverageShort(bet.AnalyseModel.AverageProfilerHomeAway);
            //    strBuilder3.Append($"=== Home at home / Away at away ===\n");
            //    strBuilder2.Append($"===============================\n");
            //    strBuilder3.Append($"\n");
            //    strBuilder3.Append($"FT Home Goals Average =>  {shortAverageHomeAway.Average_FT_Goals_HomeTeam}\n");
            //    strBuilder3.Append($"FT Away Goals Average =>  {shortAverageHomeAway.Average_FT_Goals_AwayTeam}\n");
            //    strBuilder3.Append($"\n");
            //    strBuilder3.Append($"HT Home Goals Average =>  {shortAverageHomeAway.Average_HT_Goals_HomeTeam}\n");
            //    strBuilder3.Append($"HT Away Goals Average =>  {shortAverageHomeAway.Average_HT_Goals_AwayTeam}\n");
            //    strBuilder3.Append($"\n");
            //    strBuilder3.Append($"SH Home Goals Average =>  {shortAverageHomeAway.Average_SH_Goals_HomeTeam}\n");
            //    strBuilder3.Append($"SH Away Goals Average =>  {shortAverageHomeAway.Average_SH_Goals_AwayTeam}\n");
            //    strBuilder3.Append($"\n");
            //    strBuilder3.Append($"FT Win1 =>  {shortAverageHomeAway.Is_FT_Win1.ToResponseWinLoseVisualise()}\n");
            //    strBuilder3.Append($"FT X =>  {shortAverageHomeAway.Is_FT_X.ToResponseWinLoseVisualise()}\n");
            //    strBuilder3.Append($"FT Win2 =>  {shortAverageHomeAway.Is_FT_Win2.ToResponseWinLoseVisualise()}\n");
            //    strBuilder3.Append($"\n");
            //    strBuilder3.Append($"HT Win1 =>  {shortAverageHomeAway.Is_HT_Win1.ToResponseWinLoseVisualise()}\n");
            //    strBuilder3.Append($"HT X =>  {shortAverageHomeAway.Is_HT_X.ToResponseWinLoseVisualise()}\n");
            //    strBuilder3.Append($"HT Win2 =>  {shortAverageHomeAway.Is_HT_Win2.ToResponseWinLoseVisualise()}\n");
            //    strBuilder3.Append($"\n");
            //    strBuilder3.Append($"SH Win1 =>  {shortAverageHomeAway.Is_SH_Win1.ToResponseWinLoseVisualise()}\n");
            //    strBuilder3.Append($"SH X =>  {shortAverageHomeAway.Is_SH_X.ToResponseWinLoseVisualise()}\n");
            //    strBuilder3.Append($"SH Win2 =>  {shortAverageHomeAway.Is_SH_Win2.ToResponseWinLoseVisualise()}\n");
            //    strBuilder3.Append($"\n");
            //    strBuilder3.Append($"FT 1,5 Under/Over =>  {shortAverageHomeAway.FT_15_Over.ToResponseOverVisualise()}\n");
            //    strBuilder3.Append($"FT 2,5 Under/Over =>  {shortAverageHomeAway.FT_25_Over.ToResponseOverVisualise()}\n");
            //    strBuilder3.Append($"FT 3,5 Under/Over =>  {shortAverageHomeAway.FT_35_Over.ToResponseOverVisualise()}\n");
            //    strBuilder3.Append($"\n");
            //    strBuilder3.Append($"HT 0,5 Under/Over =>  {shortAverageHomeAway.HT_05_Over.ToResponseOverVisualise()}\n");
            //    strBuilder3.Append($"\n");
            //    strBuilder3.Append($"SH 0,5 Under/Over =>  {shortAverageHomeAway.SH_05_Over.ToResponseOverVisualise()}\n");
            //    strBuilder3.Append($"\n");
            //    strBuilder3.Append($"FT Both teams will score? =>  {shortAverageHomeAway.FT_GG.ToResponseBothGoalVisualise()}\n");
            //    strBuilder3.Append($"\n");
            //    strBuilder3.Append($"FT HOME 0,5 Over =>  {shortAverageHomeAway.Home_FT_05_Over.ToResponseOverVisualise()}\n");
            //    strBuilder3.Append($"FT AWAY 0,5 Over =>  {shortAverageHomeAway.Away_FT_05_Over.ToResponseOverVisualise()}\n");
            //    strBuilder3.Append($"\n");
            //    strBuilder3.Append($"HT HOME 0,5 Over =>  {shortAverageHomeAway.Home_HT_05_Over.ToResponseOverVisualise()}\n");
            //    strBuilder3.Append($"HT AWAY 0,5 Over =>  {shortAverageHomeAway.Away_HT_05_Over.ToResponseOverVisualise()}\n");
            //    strBuilder3.Append($"\n");
            //    strBuilder3.Append($"SH HOME 0,5 Over =>  {shortAverageHomeAway.Home_SH_05_Over.ToResponseOverVisualise()}\n");
            //    strBuilder3.Append($"SH AWAY 0,5 Over =>  {shortAverageHomeAway.Away_SH_05_Over.ToResponseOverVisualise()}\n");
            //    strBuilder3.Append($"\n========================");

            //    var cntStr3 = strBuilder3.ToString();
            //    strBuilder.Append(cntStr2);
            //    strBuilder.Append(cntStr3);

            //    _botService.SendRiskerMessage(strBuilder.ToString());
            //}

            base.ExecuteTTT2(serials, path, countryContainer, league, userCheck);
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
                   betResult.IsSH_05Over ||
                   betResult.IsHT_Win1 ||
                   betResult.IsHT_Win2 ||
                   betResult.IsSH_Win1 ||
                   betResult.IsSH_Win2;
        }
        private bool CheckFT25Over(JobAnalyseModel item, string contentString, LeagueHolder leagueHolder, bool ggResult)
        {
            bool result = false;

            try
            {
                if (ggResult)
                {
                    bool avgGoalCount = item.AverageProfilerHomeAway.Average_FT_Goals_HomeTeam > (decimal)1.6 && item.AverageProfilerHomeAway.Average_FT_Goals_AwayTeam > (decimal)1.6;

                    bool avgGoalConcededHome = item.HomeTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_Goals_HomeTeam > (decimal)1.6 && item.HomeTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_Conceded_Goals_HomeTeam > (decimal)1.6;

                    bool avgGoalConcededAway = item.AwayTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_Goals_AwayTeam > (decimal)1.6 && item.AwayTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_Conceded_Goals_AwayTeam > (decimal)1.6;

                    bool fiftyPercentUp = item.AverageProfilerHomeAway.FT_25_Over.Percentage > 55 && item.AverageProfilerHomeAway.FT_GG.FeatureName.ToLower() == "true" && item.HomeTeam_FormPerformanceGuessContainer.HomeAway.FT_25_Over.Percentage > 55 && item.HomeTeam_FormPerformanceGuessContainer.HomeAway.FT_25_Over.FeatureName.ToLower() == "true" && item.AwayTeam_FormPerformanceGuessContainer.HomeAway.FT_25_Over.Percentage > 55 && item.AwayTeam_FormPerformanceGuessContainer.HomeAway.FT_25_Over.FeatureName.ToLower() == "true";

                    bool comparison = item.ComparisonInfoContainer.HomeAway.FT_25_Over.Percentage > 55 && item.ComparisonInfoContainer.HomeAway.FT_25_Over.FeatureName.ToLower() == "true";

                    bool leagueStat = leagueHolder.Over_2_5_Percentage > 54 && leagueHolder.GoalsAverage > (decimal)2.7;

                    result = avgGoalCount && avgGoalConcededHome && avgGoalConcededAway && fiftyPercentUp && comparison && leagueStat;
                }
                else
                {
                    bool avgGoalCount = item.AverageProfilerHomeAway.Average_FT_Goals_HomeTeam >= (decimal)2.75;
                    bool avgGoalCountAway = item.AverageProfilerHomeAway.Average_FT_Goals_AwayTeam >= (decimal)2.75;

                    bool ifContiditionResult = false;

                    if (avgGoalCount)
                    {
                        bool avgGoalConcededAway = item.AwayTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_Conceded_Goals_AwayTeam > (decimal)2.75;

                        bool fiftyPercentUp = item.AverageProfilerHomeAway.FT_25_Over.Percentage > 55 && item.AverageProfilerHomeAway.FT_GG.FeatureName.ToLower() == "true" && item.HomeTeam_FormPerformanceGuessContainer.HomeAway.FT_25_Over.Percentage > 55 && item.HomeTeam_FormPerformanceGuessContainer.HomeAway.FT_25_Over.FeatureName.ToLower() == "true" && item.AwayTeam_FormPerformanceGuessContainer.HomeAway.FT_25_Over.Percentage > 55 && item.AwayTeam_FormPerformanceGuessContainer.HomeAway.FT_25_Over.FeatureName.ToLower() == "true";

                        bool comparison = item.ComparisonInfoContainer.HomeAway.FT_25_Over.Percentage > 55 && item.ComparisonInfoContainer.HomeAway.FT_25_Over.FeatureName.ToLower() == "true";

                        ifContiditionResult = avgGoalConcededAway && fiftyPercentUp && comparison;
                    }
                    else if(avgGoalCountAway)
                    {
                        bool avgGoalConcededHome = item.HomeTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_Conceded_Goals_HomeTeam > (decimal)2.75;

                        bool fiftyPercentUp = item.AverageProfilerHomeAway.FT_25_Over.Percentage > 55 && item.AverageProfilerHomeAway.FT_GG.FeatureName.ToLower() == "true" && item.HomeTeam_FormPerformanceGuessContainer.HomeAway.FT_25_Over.Percentage > 55 && item.HomeTeam_FormPerformanceGuessContainer.HomeAway.FT_25_Over.FeatureName.ToLower() == "true" && item.AwayTeam_FormPerformanceGuessContainer.HomeAway.FT_25_Over.Percentage > 55 && item.AwayTeam_FormPerformanceGuessContainer.HomeAway.FT_25_Over.FeatureName.ToLower() == "true";

                        bool comparison = item.ComparisonInfoContainer.HomeAway.FT_25_Over.Percentage > 55 && item.ComparisonInfoContainer.HomeAway.FT_25_Over.FeatureName.ToLower() == "true";

                        ifContiditionResult = fiftyPercentUp && fiftyPercentUp && comparison;
                    }
                    else
                    {
                        return false;
                    }

                    bool leagueStat = leagueHolder.Over_2_5_Percentage > 54 && leagueHolder.GoalsAverage > (decimal)2.7;

                    result = ifContiditionResult && leagueStat;
                }

                return result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private bool CheckFT25Over_222(JobAnalyseModel item, string contentString, LeagueHolder leagueHolder, bool ggResult)
        {
            bool result = false;

            try
            {
                if (ggResult)
                {
                    bool homeForce = item.HomeTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_Goals_HomeTeam >= (decimal)1.65 &&
                                     item.AwayTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_Conceded_Goals_AwayTeam >= (decimal)1.65;
                    bool awayForce = item.AwayTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_Goals_AwayTeam >= (decimal)1.65 &&
                                     item.HomeTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_Conceded_Goals_HomeTeam >= (decimal)1.65;

                    if (homeForce || awayForce)
                    {
                        result = true;
                    }
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

        private bool CheckFT15Over(JobAnalyseModel item, string contentString, LeagueHolder leagueHolder, bool ggResult, bool overResult)
        {
            bool result = ggResult || overResult;
            if (result) return result;

            try
            {
                bool isValidImportant = leagueHolder.Over_1_5_Percentage >= 75 && leagueHolder.GoalsAverage >= (decimal)2.50;

                if (!isValidImportant) return isValidImportant;

                int indicatorOver = 85;

                bool isValid =
                item.AverageProfilerHomeAway.FT_15_Over.Percentage >= indicatorOver &&
                item.AverageProfilerHomeAway.FT_15_Over.FeatureName.ToLower() == "true";

                bool goalAndLeftGoal = item.HomeTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_Goals_HomeTeam >= (decimal)1.3 && item.HomeTeam_FormPerformanceGuessContainer.HomeAway.Average_HT_Conceded_Goals_AwayTeam >= (decimal)1.3 &&
                    item.AwayTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_Goals_AwayTeam >= (decimal)1.3 && item.AwayTeam_FormPerformanceGuessContainer.HomeAway.Average_HT_Conceded_Goals_HomeTeam >= (decimal)1.3;

                result = isValid && goalAndLeftGoal;

                return result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private bool CheckFT15Over_222(JobAnalyseModel item, string contentString, LeagueHolder leagueHolder, bool ggResult)
        {
            bool result = ggResult;
            if (result) return result;

            try
            {
                bool isValidImportant = leagueHolder.Over_1_5_Percentage >= 75 && leagueHolder.GoalsAverage >= (decimal)2.50;

                if (!isValidImportant) return isValidImportant;

                int indicatorOver = 80;

                bool isValid =
                item.AverageProfilerHomeAway.FT_15_Over.Percentage >= indicatorOver &&
                item.AverageProfilerHomeAway.FT_15_Over.FeatureName.ToLower() == "true" &&
                item.AverageProfiler.FT_15_Over.Percentage >= indicatorOver &&
                item.AverageProfiler.FT_15_Over.FeatureName.ToLower() == "true";

                bool goalAndLeftGoal = item.HomeTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_Goals_HomeTeam >= (decimal)1.3 && item.HomeTeam_FormPerformanceGuessContainer.HomeAway.Average_HT_Conceded_Goals_AwayTeam >= (decimal)1.3 &&
                    item.AwayTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_Goals_AwayTeam >= (decimal)1.3 && item.AwayTeam_FormPerformanceGuessContainer.HomeAway.Average_HT_Conceded_Goals_HomeTeam >= (decimal)1.3;

                result = isValid && goalAndLeftGoal;

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





        private bool CheckFT_Home_0_5_Over(JobAnalyseModel item, string contentString, LeagueHolder leagueHolder)
        {
            bool result = false;

            try
            {
                bool isValid =
                item.AverageProfilerHomeAway.Home_FT_05_Over.Percentage >= 90 &&
                item.AverageProfilerHomeAway.Home_FT_05_Over.FeatureName.ToLower() == "true" &&
                item.AverageProfiler.Home_FT_05_Over.Percentage >= 85 &&
                item.AverageProfiler.Home_FT_05_Over.FeatureName.ToLower() == "true";

                bool isValid2 =
                    item.AverageProfilerHomeAway.Average_FT_Goals_HomeTeam > (decimal)1.5 &&
                    item.AverageProfiler.Average_FT_Goals_HomeTeam >= (decimal)1.4;

                if (isValid && isValid2)
                {
                    result = true;
                    return result;
                }

                return result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private bool CheckFT_Away_0_5_Over(JobAnalyseModel item, string contentString, LeagueHolder leagueHolder)
        {
            bool result = false;

            try
            {
                bool isValid =
                item.AverageProfilerHomeAway.Away_FT_05_Over.Percentage >= 85 &&
                item.AverageProfilerHomeAway.Away_FT_05_Over.FeatureName.ToLower() == "true" &&
                item.AverageProfiler.Away_FT_05_Over.Percentage >= 90 &&
                item.AverageProfiler.Away_FT_05_Over.FeatureName.ToLower() == "true";

                bool isValid2 =
                    item.AverageProfilerHomeAway.Average_FT_Goals_AwayTeam >= (decimal)1.4 &&
                    item.AverageProfiler.Average_FT_Goals_AwayTeam > (decimal)1.5;

                if (isValid && isValid2)
                {
                    result = true;
                    return result;
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
                        leagueHolder.HT_Over_0_5_Percentage > 60 && leagueHolder.HT_GoalsAverage > 1;

                if (!isValidImportant) return isValidImportant;

                bool isValid =
                item.AverageProfilerHomeAway.HT_05_Over.Percentage >= 85 &&
                item.AverageProfilerHomeAway.HT_05_Over.FeatureName.ToLower() == "true" &&
                item.AverageProfiler.HT_05_Over.Percentage > 80 &&
                item.AverageProfiler.HT_05_Over.FeatureName.ToLower() == "true";

                return isValid;
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


        private bool CheckFT_GG(JobAnalyseModel item, string contentString, LeagueHolder leagueHolder)
        {
            try
            {
                //bool isValidImportant = false;

                //bool isHomeGoalPerf = false;
                //bool isAwayGoalPerf = false;

                //isValidImportant = leagueHolder.GoalsAverage >= (decimal)2.8 && leagueHolder.GG_Percentage >= 60;
                //isHomeGoalPerf = item.HomeTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_Goals_HomeTeam >= (decimal)1.8;
                //isHomeGoalPerf = item.AwayTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_Goals_AwayTeam >= (decimal)1.8;

                //if (!isValidImportant) return result;

                //bool isValid =
                //    item.AverageProfilerHomeAway.FT_GG.Percentage > leagueHolder.GG_Percentage &&
                //    item.AverageProfilerHomeAway.FT_GG.FeatureName.ToLower() == "true";

                //bool ggForceHome = item.AwayTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_Conceded_Goals_AwayTeam >= (decimal)1.7;
                //bool ggForceAway = item.HomeTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_Conceded_Goals_HomeTeam >= (decimal)1.7;

                //result = isValid && isHomeGoalPerf && isAwayGoalPerf && ggForceHome && ggForceAway;

                bool avgGoalCount = item.AverageProfilerHomeAway.Average_FT_Goals_HomeTeam > (decimal)1.2 && item.AverageProfilerHomeAway.Average_FT_Goals_AwayTeam > (decimal)1.2;

                bool avgGoalConcededHome = item.HomeTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_Goals_HomeTeam > (decimal)1.2 && item.HomeTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_Conceded_Goals_HomeTeam > (decimal)1.2;

                bool avgGoalConcededAway = item.AwayTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_Goals_AwayTeam > (decimal)1.2 && item.AwayTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_Conceded_Goals_AwayTeam > (decimal)1.2;

                bool fiftyPercentUp = item.AverageProfilerHomeAway.FT_GG.Percentage > 50 && item.AverageProfilerHomeAway.FT_GG.FeatureName.ToLower() == "true" && item.HomeTeam_FormPerformanceGuessContainer.HomeAway.FT_GG.Percentage > 50 && item.HomeTeam_FormPerformanceGuessContainer.HomeAway.FT_GG.FeatureName.ToLower() == "true" && item.AwayTeam_FormPerformanceGuessContainer.HomeAway.FT_GG.Percentage > 50 && item.AwayTeam_FormPerformanceGuessContainer.HomeAway.FT_GG.FeatureName.ToLower() == "true";

                bool firstHalfAverage = item.AverageProfilerHomeAway.Average_HT_Goals_HomeTeam > (decimal)0.5 && item.AverageProfilerHomeAway.Average_HT_Goals_AwayTeam > (decimal)0.5;

                bool firstHalfHome = item.HomeTeam_FormPerformanceGuessContainer.HomeAway.Average_HT_Goals_HomeTeam > (decimal)0.5 && item.HomeTeam_FormPerformanceGuessContainer.HomeAway.Average_HT_Conceded_Goals_HomeTeam > (decimal)0.5;

                bool firstHalfAway = item.AwayTeam_FormPerformanceGuessContainer.HomeAway.Average_HT_Goals_AwayTeam > (decimal)0.5 && item.AwayTeam_FormPerformanceGuessContainer.HomeAway.Average_HT_Conceded_Goals_AwayTeam > (decimal)0.5;

                bool secondHalfAverage = item.AverageProfilerHomeAway.Average_SH_Goals_HomeTeam > (decimal)0.5 && item.AverageProfilerHomeAway.Average_SH_Goals_AwayTeam > (decimal)0.5;

                bool secondHalfHome = item.HomeTeam_FormPerformanceGuessContainer.HomeAway.Average_SH_Goals_HomeTeam > (decimal)0.5 && item.HomeTeam_FormPerformanceGuessContainer.HomeAway.Average_SH_Conceded_Goals_HomeTeam > (decimal)0.5;

                bool secondHalfAway = item.AwayTeam_FormPerformanceGuessContainer.HomeAway.Average_SH_Goals_AwayTeam > (decimal)0.5 && item.AwayTeam_FormPerformanceGuessContainer.HomeAway.Average_SH_Conceded_Goals_AwayTeam > (decimal)0.5;

                bool comparison = item.ComparisonInfoContainer.HomeAway.FT_GG.Percentage > 55 && item.ComparisonInfoContainer.HomeAway.FT_GG.FeatureName.ToLower() == "true";

                bool leagueStat = leagueHolder.GG_Percentage > 54 && leagueHolder.GoalsAverage > (decimal)2.6;

                return avgGoalCount && avgGoalConcededHome && avgGoalConcededAway && fiftyPercentUp && firstHalfAverage && firstHalfHome && firstHalfAway && secondHalfAverage && secondHalfHome && secondHalfAway && comparison && leagueStat;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private bool CheckFT_GG_222(JobAnalyseModel item, string contentString, LeagueHolder leagueHolder)
        {
            bool result = false;

            try
            {
                bool isValidImportant = false;

                bool isHomeGoalPerf = false;
                bool isAwayGoalPerf = false;

                isValidImportant = leagueHolder.GoalsAverage >= (decimal)2.5 && leagueHolder.GG_Percentage >= 55;
                isHomeGoalPerf = item.HomeTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_Goals_HomeTeam >= (decimal)1.4;
                isHomeGoalPerf = item.AwayTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_Goals_AwayTeam >= (decimal)1.4;

                if (!isValidImportant) return result;

                bool isValid =
                    item.AverageProfilerHomeAway.FT_GG.Percentage > leagueHolder.GG_Percentage &&
                    item.AverageProfilerHomeAway.FT_GG.FeatureName.ToLower() == "true";

                bool ggForceHome = item.AwayTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_Conceded_Goals_AwayTeam >= (decimal)1.4;
                bool ggForceAway = item.HomeTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_Conceded_Goals_HomeTeam >= (decimal)1.4;

                result = isValid && isHomeGoalPerf && isAwayGoalPerf && ggForceHome && ggForceAway;

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

        #region HT_RESULT_CHECKER
        private bool CheckHT_Win1(JobAnalyseModel item, string contentString, LeagueHolder leagueHolder, bool hasMoreDetails)
        {
            bool result = false;

            try
            {
                bool isValidCompact =
                item.AverageProfilerHomeAway.Is_HT_Win1.Percentage > 55 &&
                item.AverageProfilerHomeAway.Is_HT_Win1.FeatureName.ToLower() == "true" &&
                item.HomeTeam_FormPerformanceGuessContainer.HomeAway.Is_HT_Win1.Percentage > 53 &&
                item.HomeTeam_FormPerformanceGuessContainer.HomeAway.Is_HT_Win1.FeatureName.ToLower() == "true" &&
                item.AwayTeam_FormPerformanceGuessContainer.HomeAway.Is_HT_Win1.Percentage > 53 &&
                item.AwayTeam_FormPerformanceGuessContainer.HomeAway.Is_HT_Win1.FeatureName.ToLower() == "true";

                bool isValidHome = item.HomeTeam_FormPerformanceGuessContainer.HomeAway.Average_HT_Goals_HomeTeam > (decimal)1;

                bool isValidAway = item.AwayTeam_FormPerformanceGuessContainer.HomeAway.Average_HT_Conceded_Goals_AwayTeam > (decimal)1;

                bool isHomeWinning = item.AverageProfilerHomeAway.Is_FT_Win1.Percentage > 60 && item.AverageProfilerHomeAway.Is_FT_Win1.FeatureName.ToLower() == "true";

                return isValidCompact && isValidHome && isValidAway && isHomeWinning;


                //bool isValidHomeAway =
                //item.AverageProfilerHomeAway.Is_HT_Win1.Percentage > 65 &&
                //item.AverageProfilerHomeAway.Is_HT_Win1.FeatureName.ToLower() == "true" &&
                //item.AverageProfilerHomeAway.Is_HT_X.Percentage > 65 &&
                //item.AverageProfilerHomeAway.Is_HT_X.FeatureName.ToLower() == "false" &&
                //item.AverageProfilerHomeAway.Is_HT_Win2.Percentage > 65 &&
                //item.AverageProfilerHomeAway.Is_HT_Win2.FeatureName.ToLower() == "false";

                //bool forceHtHome = ((item.AverageProfilerHomeAway.Average_HT_Goals_HomeTeam + (decimal)0.01) / (item.AverageProfilerHomeAway.Average_HT_Goals_AwayTeam + (decimal)0.01)) >= (decimal)2.00;

                //if (!isValidHomeAway || !forceHtHome)
                //    return result;

                //if (hasMoreDetails)
                //{
                //    forceHtHome = item.HomeTeam_FormPerformanceGuessContainer.HomeAway.Average_HT_Goals_HomeTeam >= (decimal)0.75 &&
                //                   item.AwayTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_ShotOnTarget_AwayTeam / (decimal)1.75 < item.HomeTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_GK_Saves_HomeTeam / 2 &&
                //                   item.HomeTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_ShotOnTarget_HomeTeam / (decimal)2 >
                //                   item.AwayTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_GK_Saves_AwayTeam / (decimal)1.75 &&
                //                   item.AwayTeam_FormPerformanceGuessContainer.HomeAway.Average_HT_Conceded_Goals_AwayTeam >= (decimal)0.75;
                //}

                //result = forceHtHome;

                //return result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private bool CheckHT_Win1_222(JobAnalyseModel item, string contentString, LeagueHolder leagueHolder, bool hasMoreDetails)
        {
            bool result = false;

            try
            {
                result =
                item.AverageProfilerHomeAway.Is_HT_Win1.Percentage > 60 &&
                item.AverageProfilerHomeAway.Is_HT_Win1.FeatureName.ToLower() == "true" &&
                item.AverageProfilerHomeAway.Is_HT_X.Percentage > 60 &&
                item.AverageProfilerHomeAway.Is_HT_X.FeatureName.ToLower() == "false" &&
                item.AverageProfilerHomeAway.Is_HT_Win2.Percentage > 60 &&
                item.AverageProfilerHomeAway.Is_HT_Win2.FeatureName.ToLower() == "false";

                return result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private bool CheckHT_Win2(JobAnalyseModel item, string contentString, LeagueHolder leagueHolder, bool hasMoreDetails)
        {
            bool result = false;

            try
            {
                bool isValidCompact =
                item.AverageProfilerHomeAway.Is_HT_Win2.Percentage > 55 &&
                item.AverageProfilerHomeAway.Is_HT_Win2.FeatureName.ToLower() == "true" &&
                item.HomeTeam_FormPerformanceGuessContainer.HomeAway.Is_HT_Win2.Percentage > 53 &&
                item.HomeTeam_FormPerformanceGuessContainer.HomeAway.Is_HT_Win2.FeatureName.ToLower() == "true" &&
                item.AwayTeam_FormPerformanceGuessContainer.HomeAway.Is_HT_Win2.Percentage > 53 &&
                item.AwayTeam_FormPerformanceGuessContainer.HomeAway.Is_HT_Win2.FeatureName.ToLower() == "true";

                bool isValidHome = item.AwayTeam_FormPerformanceGuessContainer.HomeAway.Average_HT_Goals_AwayTeam > (decimal)1;

                bool isValidAway = item.HomeTeam_FormPerformanceGuessContainer.HomeAway.Average_HT_Conceded_Goals_HomeTeam > (decimal)1;

                bool isHomeWinning = item.AverageProfilerHomeAway.Is_FT_Win2.Percentage > 60 && item.AverageProfilerHomeAway.Is_FT_Win2.FeatureName.ToLower() == "true";

                return isValidCompact && isValidHome && isValidAway && isHomeWinning;

                //bool isValidHomeAway =
                //item.AverageProfilerHomeAway.Is_HT_Win2.Percentage >= 66 &&
                //item.AverageProfilerHomeAway.Is_HT_Win2.FeatureName.ToLower() == "true" &&
                //item.AverageProfilerHomeAway.Is_HT_X.Percentage >= 66 &&
                //item.AverageProfilerHomeAway.Is_HT_X.FeatureName.ToLower() == "false" &&
                //item.AverageProfilerHomeAway.Is_HT_Win1.Percentage >= 66 &&
                //item.AverageProfilerHomeAway.Is_HT_Win1.FeatureName.ToLower() == "false";

                //bool forceHtAway = ((item.AverageProfilerHomeAway.Average_HT_Goals_AwayTeam + (decimal)0.01) / (item.AverageProfilerHomeAway.Average_HT_Goals_HomeTeam + (decimal)0.01)) >= (decimal)2.00;

                //if (!isValidHomeAway || !forceHtAway)
                //    return result;

                //if (hasMoreDetails)
                //{
                //    forceHtAway = item.AwayTeam_FormPerformanceGuessContainer.HomeAway.Average_HT_Goals_AwayTeam >= (decimal)0.75 &&
                //                   item.HomeTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_ShotOnTarget_HomeTeam / (decimal)1.75 < item.AwayTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_GK_Saves_AwayTeam / 2 &&
                //                   item.AwayTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_ShotOnTarget_AwayTeam / (decimal)2 >
                //                   item.HomeTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_GK_Saves_HomeTeam / (decimal)1.75 &&
                //                   item.HomeTeam_FormPerformanceGuessContainer.HomeAway.Average_HT_Conceded_Goals_HomeTeam >= (decimal)0.75;
                //}
                //result = forceHtAway;

                //return result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private bool CheckHT_Win2_222(JobAnalyseModel item, string contentString, LeagueHolder leagueHolder, bool hasMoreDetails)
        {
            bool result = false;

            try
            {
                result =
                item.AverageProfilerHomeAway.Is_HT_Win2.Percentage >= 60 &&
                item.AverageProfilerHomeAway.Is_HT_Win2.FeatureName.ToLower() == "true" &&
                item.AverageProfilerHomeAway.Is_HT_X.Percentage >= 60 &&
                item.AverageProfilerHomeAway.Is_HT_X.FeatureName.ToLower() == "false" &&
                item.AverageProfilerHomeAway.Is_HT_Win1.Percentage >= 60 &&
                item.AverageProfilerHomeAway.Is_HT_Win1.FeatureName.ToLower() == "false";

                return result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private bool CheckSH_Win1(JobAnalyseModel item, string contentString, LeagueHolder leagueHolder, bool hasMoreDetails)
        {
            bool result = false;

            try
            {
                bool isValidCompact =
                item.AverageProfilerHomeAway.Is_SH_Win1.Percentage > 55 &&
                item.AverageProfilerHomeAway.Is_SH_Win1.FeatureName.ToLower() == "true" &&
                item.HomeTeam_FormPerformanceGuessContainer.HomeAway.Is_SH_Win1.Percentage > 53 &&
                item.HomeTeam_FormPerformanceGuessContainer.HomeAway.Is_SH_Win1.FeatureName.ToLower() == "true" &&
                item.AwayTeam_FormPerformanceGuessContainer.HomeAway.Is_SH_Win1.Percentage > 53 &&
                item.AwayTeam_FormPerformanceGuessContainer.HomeAway.Is_SH_Win1.FeatureName.ToLower() == "true";

                bool isValidHome = item.HomeTeam_FormPerformanceGuessContainer.HomeAway.Average_SH_Goals_HomeTeam > (decimal)1;

                bool isValidAway = item.AwayTeam_FormPerformanceGuessContainer.HomeAway.Average_SH_Conceded_Goals_AwayTeam > (decimal)1;

                bool isHomeWinning = item.AverageProfilerHomeAway.Is_FT_Win1.Percentage > 60 && item.AverageProfilerHomeAway.Is_FT_Win1.FeatureName.ToLower() == "true";

                return isValidCompact && isValidHome && isValidAway && isHomeWinning;

                //bool isValidHomeAway =
                //item.AverageProfilerHomeAway.Is_SH_Win1.Percentage > 65 &&
                //item.AverageProfilerHomeAway.Is_SH_Win1.FeatureName.ToLower() == "true" &&
                //item.AverageProfilerHomeAway.Is_SH_X.Percentage > 65 &&
                //item.AverageProfilerHomeAway.Is_SH_X.FeatureName.ToLower() == "false" &&
                //item.AverageProfilerHomeAway.Is_SH_Win2.Percentage > 65 &&
                //item.AverageProfilerHomeAway.Is_SH_Win2.FeatureName.ToLower() == "false";

                //bool forceShHome = ((item.AverageProfilerHomeAway.Average_SH_Goals_HomeTeam + (decimal)0.01) / (item.AverageProfilerHomeAway.Average_SH_Goals_AwayTeam + (decimal)0.01)) >= (decimal)2.00;

                //if (!isValidHomeAway || !forceShHome)
                //    return result;

                //if (hasMoreDetails)
                //{
                //    forceShHome = item.HomeTeam_FormPerformanceGuessContainer.HomeAway.Average_SH_Goals_HomeTeam >= (decimal)1 &&
                //                   item.AwayTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_ShotOnTarget_AwayTeam / (decimal)1.65 < item.HomeTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_GK_Saves_HomeTeam / 2 &&
                //                   item.HomeTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_ShotOnTarget_HomeTeam / (decimal)2 >
                //                   item.AwayTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_GK_Saves_AwayTeam / (decimal)1.75 &&
                //                   item.AwayTeam_FormPerformanceGuessContainer.HomeAway.Average_SH_Conceded_Goals_AwayTeam >= (decimal)1;
                //}

                //result = forceShHome;

                //return result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private bool CheckSH_Win1_222(JobAnalyseModel item, string contentString, LeagueHolder leagueHolder, bool hasMoreDetails)
        {
            bool result = false;

            try
            {
                result =
                item.AverageProfilerHomeAway.Is_SH_Win1.Percentage > 60 &&
                item.AverageProfilerHomeAway.Is_SH_Win1.FeatureName.ToLower() == "true" &&
                item.AverageProfilerHomeAway.Is_SH_X.Percentage > 60 &&
                item.AverageProfilerHomeAway.Is_SH_X.FeatureName.ToLower() == "false" &&
                item.AverageProfilerHomeAway.Is_SH_Win2.Percentage > 60 &&
                item.AverageProfilerHomeAway.Is_SH_Win2.FeatureName.ToLower() == "false";

                return result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private bool CheckSH_Win2(JobAnalyseModel item, string contentString, LeagueHolder leagueHolder, bool hasMoreDetails)
        {
            bool result = false;

            try
            {
                bool isValidCompact =
                item.AverageProfilerHomeAway.Is_SH_Win2.Percentage > 55 &&
                item.AverageProfilerHomeAway.Is_SH_Win2.FeatureName.ToLower() == "true" &&
                item.HomeTeam_FormPerformanceGuessContainer.HomeAway.Is_SH_Win2.Percentage > 53 &&
                item.HomeTeam_FormPerformanceGuessContainer.HomeAway.Is_SH_Win2.FeatureName.ToLower() == "true" &&
                item.AwayTeam_FormPerformanceGuessContainer.HomeAway.Is_SH_Win2.Percentage > 53 &&
                item.AwayTeam_FormPerformanceGuessContainer.HomeAway.Is_SH_Win2.FeatureName.ToLower() == "true";

                bool isValidHome = item.AwayTeam_FormPerformanceGuessContainer.HomeAway.Average_SH_Goals_AwayTeam > (decimal)1;

                bool isValidAway = item.HomeTeam_FormPerformanceGuessContainer.HomeAway.Average_SH_Conceded_Goals_HomeTeam > (decimal)1;

                bool isHomeWinning = item.AverageProfilerHomeAway.Is_FT_Win2.Percentage > 60 && item.AverageProfilerHomeAway.Is_FT_Win2.FeatureName.ToLower() == "true";

                return isValidCompact && isValidHome && isValidAway && isHomeWinning;

                //bool isValidHomeAway =
                //item.AverageProfilerHomeAway.Is_SH_Win2.Percentage >= 66 &&
                //item.AverageProfilerHomeAway.Is_SH_Win2.FeatureName.ToLower() == "true" &&
                //item.AverageProfilerHomeAway.Is_SH_X.Percentage >= 66 &&
                //item.AverageProfilerHomeAway.Is_SH_X.FeatureName.ToLower() == "false" &&
                //item.AverageProfilerHomeAway.Is_SH_Win1.Percentage >= 66 &&
                //item.AverageProfilerHomeAway.Is_SH_Win1.FeatureName.ToLower() == "false";

                //bool forceShAway = ((item.AverageProfilerHomeAway.Average_SH_Goals_AwayTeam + (decimal)0.01) / (item.AverageProfilerHomeAway.Average_SH_Goals_HomeTeam + (decimal)0.01)) >= (decimal)2.00;

                //if (!isValidHomeAway || !forceShAway)
                //    return result;

                //if (hasMoreDetails)
                //{
                //    forceShAway = item.AwayTeam_FormPerformanceGuessContainer.HomeAway.Average_SH_Goals_AwayTeam >= (decimal)0.75 &&
                //                   item.HomeTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_ShotOnTarget_HomeTeam / (decimal)1.75 < item.AwayTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_GK_Saves_AwayTeam / 2 &&
                //                   item.AwayTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_ShotOnTarget_AwayTeam / (decimal)2 >
                //                   item.HomeTeam_FormPerformanceGuessContainer.HomeAway.Average_FT_GK_Saves_HomeTeam / (decimal)1.75 &&
                //                   item.HomeTeam_FormPerformanceGuessContainer.HomeAway.Average_SH_Conceded_Goals_HomeTeam >= (decimal)0.75;
                //}
                //result = forceShAway;

                //return result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private bool CheckSH_Win2_222(JobAnalyseModel item, string contentString, LeagueHolder leagueHolder, bool hasMoreDetails)
        {
            bool result = false;

            try
            {
                result =
                item.AverageProfilerHomeAway.Is_SH_Win2.Percentage >= 60 &&
                item.AverageProfilerHomeAway.Is_SH_Win2.FeatureName.ToLower() == "true" &&
                item.AverageProfilerHomeAway.Is_SH_X.Percentage >= 60 &&
                item.AverageProfilerHomeAway.Is_SH_X.FeatureName.ToLower() == "false" &&
                item.AverageProfilerHomeAway.Is_SH_Win1.Percentage >= 60 &&
                item.AverageProfilerHomeAway.Is_SH_Win1.FeatureName.ToLower() == "false";

                return result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #endregion



        public override void ExecuteTTT(List<string> serials, Dictionary<string, string> path, CountryContainerTemp countryContainer, LeagueContainer leagueContainer, UserCheck userCheck)
        {
            List<JobAnalyseModel> responseProfiler;
            string content;
            using (var reader = new StreamReader(path["responseProfilerTemp"]))
            {
                content = reader.ReadToEnd();
                if (content.Length < 10)
                {
                    responseProfiler = OperationalProcessor.GetJobAnalyseModelResultTest7777(_matchBetService, _filterResultService, _comparisonStatisticsHolderService, _averageStatisticsHolderService, _teamPerformanceStatisticsHolderService, _leagueStatisticsHolderService, _matchIdentifierService, _aiDataHolderService, _statisticInfoHolderService, _containerTemp, leagueContainer, serials).Where(x => x.AverageProfiler != null && x.AverageProfilerHomeAway != null).ToList();
                }
                else
                {
                    responseProfiler = JsonConvert.DeserializeObject<List<JobAnalyseModel>>(content);
                }
            }

            if (content.Length < 10)
            {
                using (var writer = new StreamWriter(path["responseProfilerTemp"]))
                {
                    writer.Write(JsonConvert.SerializeObject(responseProfiler, Formatting.Indented));
                }
            }

            if (responseProfiler == null) responseProfiler = new List<JobAnalyseModel>();

            int failed = 0;
            var rgxLeague = new Regex(PatternConstant.StartedMatchPattern.League);
            var rgxCountry = new Regex(PatternConstant.StartedMatchPattern.Country);
            var rgxLeague2 = new Regex(PatternConstant.StartedMatchPattern.CountryAndLeague);
            int iteration = 0;

            var removeList = new List<JobAnalyseModel>();

            for (int i = 0; i < responseProfiler.Count; i++)
            {
                var item = responseProfiler[i];

                iteration++;
                TelegramMessagingManager botServiceManager = new TelegramMessagingManager();
                try
                {
                    var contentString = _webHelper.GetMinifiedString($"http://arsiv.mackolik.com/Match/Default.aspx?id={item.HomeTeam_FormPerformanceGuessContainer.Serial}#karsilastirma");
                    string leagueName = contentString.ResolveLeagueByRegex(countryContainer, rgxLeague2, rgxLeague);
                    string countryName = contentString.ResolveCountryByRegex(countryContainer, rgxLeague2, rgxCountry);
                    var leagueHolder = leagueContainer.LeagueHolders.FirstOrDefault(x => x.Country.ToLower() == countryName.ToLower() && x.League.ToLower() == leagueName.ToLower());

                    if (leagueHolder == null)
                    {
                        continue;
                    }

                    var strBuilder1 = new StringBuilder();
                    strBuilder1.Append("BET Statistic\n");
                    strBuilder1.Append($"LINK:  http://arsiv.mackolik.com/Match/Default.aspx?id={item.ComparisonInfoContainer.Serial}\n");
                    strBuilder1.Append($"COUNTRY:  {countryName}\n");
                    strBuilder1.Append($"LEAGUE:  {leagueName} \n");
                    strBuilder1.Append($"MATCH:  {item.ComparisonInfoContainer.Home} - {item.ComparisonInfoContainer.Away}\n");
                    strBuilder1.Append($"=== League Informations ===\n");
                    strBuilder1.Append($"\n");
                    strBuilder1.Append($"Found matchs count => {leagueHolder.CountFound}\n");
                    strBuilder1.Append($"FT Goals Average => {leagueHolder.GoalsAverage.ToString("0.00")}\n");
                    strBuilder1.Append($"HT Goals Average => {leagueHolder.HT_GoalsAverage.ToString("0.00")}\n");
                    strBuilder1.Append($"SH Goals Average => {leagueHolder.SH_GoalsAverage.ToString("0.00")}\n");
                    strBuilder1.Append($"\n");
                    strBuilder1.Append($"FT Both teams will score? => {leagueHolder.GG_Percentage.ToResponseBothGoalVisualise()}\n");
                    strBuilder1.Append($"FT 1,5 Under/Over => {leagueHolder.Over_1_5_Percentage.ToResponseOverVisualise()}\n");
                    strBuilder1.Append($"FT 2,5 Under/Over => {leagueHolder.Over_2_5_Percentage.ToResponseOverVisualise()}\n");
                    strBuilder1.Append($"\n");
                    strBuilder1.Append($"HT 0,5 Under/Over => {leagueHolder.HT_Over_0_5_Percentage.ToResponseOverVisualise()}\n");
                    strBuilder1.Append($"\n");
                    strBuilder1.Append($"SH 0,5 Under/Over => {leagueHolder.SH_Over_0_5_Percentage.ToResponseOverVisualise()}\n");
                    strBuilder1.Append($"\n");
                    strBuilder1.Append($"============================\n");

                    var strBuilder2 = new StringBuilder();
                    var shortAverage = new AverageShort(item.AverageProfiler);
                    strBuilder2.Append($"=== General Statistic ===\n");
                    strBuilder2.Append($"============================\n");
                    strBuilder2.Append($"\n");
                    strBuilder2.Append($"FT Home Goals Average =>  {shortAverage.Average_FT_Goals_HomeTeam}\n");
                    strBuilder2.Append($"FT Away Goals Average =>  {shortAverage.Average_FT_Goals_AwayTeam}\n");
                    strBuilder2.Append($"\n");
                    strBuilder2.Append($"HT Home Goals Average =>  {shortAverage.Average_HT_Goals_HomeTeam}\n");
                    strBuilder2.Append($"HT Away Goals Average =>  {shortAverage.Average_HT_Goals_AwayTeam}\n");
                    strBuilder2.Append($"\n");
                    strBuilder2.Append($"SH Home Goals Average =>  {shortAverage.Average_SH_Goals_HomeTeam}\n");
                    strBuilder2.Append($"SH Away Goals Average =>  {shortAverage.Average_SH_Goals_AwayTeam}\n");
                    strBuilder2.Append($"\n");
                    strBuilder2.Append($"FT Win1 =>  {shortAverage.Is_FT_Win1.ToResponseWinLoseVisualise()}\n");
                    strBuilder2.Append($"FT X =>  {shortAverage.Is_FT_X.ToResponseWinLoseVisualise()}\n");
                    strBuilder2.Append($"FT Win2 =>  {shortAverage.Is_FT_Win2.ToResponseWinLoseVisualise()}\n");
                    strBuilder2.Append($"\n");
                    strBuilder2.Append($"HT Win1 =>  {shortAverage.Is_HT_Win1.ToResponseWinLoseVisualise()}\n");
                    strBuilder2.Append($"HT X =>  {shortAverage.Is_HT_X.ToResponseWinLoseVisualise()}\n");
                    strBuilder2.Append($"HT Win2 =>  {shortAverage.Is_HT_Win2.ToResponseWinLoseVisualise()}\n");
                    strBuilder2.Append($"\n");
                    strBuilder2.Append($"SH Win1 =>  {shortAverage.Is_SH_Win1.ToResponseWinLoseVisualise()}\n");
                    strBuilder2.Append($"SH X =>  {shortAverage.Is_SH_X.ToResponseWinLoseVisualise()}\n");
                    strBuilder2.Append($"SH Win2 =>  {shortAverage.Is_SH_Win2.ToResponseWinLoseVisualise()}\n");
                    strBuilder2.Append($"\n");
                    strBuilder2.Append($"FT 1,5 Under/Over =>  {shortAverage.FT_15_Over.ToResponseOverVisualise()}\n");
                    strBuilder2.Append($"FT 2,5 Under/Over =>  {shortAverage.FT_25_Over.ToResponseOverVisualise()}\n");
                    strBuilder2.Append($"\n");
                    strBuilder2.Append($"HT 0,5 Under/Over =>  {shortAverage.HT_05_Over.ToResponseOverVisualise()}\n");
                    strBuilder2.Append($"\n");
                    strBuilder2.Append($"SH 0,5 Under/Over =>  {shortAverage.SH_05_Over.ToResponseOverVisualise()}\n");
                    strBuilder2.Append($"\n");
                    strBuilder2.Append($"FT Both teams will score? =>  {shortAverage.FT_GG.ToResponseBothGoalVisualise()}\n");
                    strBuilder2.Append($"\n");
                    strBuilder2.Append($"FT HOME 0,5 Over =>  {shortAverage.Home_FT_05_Over.ToResponseOverVisualise()}\n");
                    strBuilder2.Append($"FT AWAY 0,5 Over =>  {shortAverage.Away_FT_05_Over.ToResponseOverVisualise()}\n");
                    strBuilder2.Append($"\n");
                    strBuilder2.Append($"HT HOME 0,5 Over =>  {shortAverage.Home_HT_05_Over.ToResponseOverVisualise()}\n");
                    strBuilder2.Append($"HT AWAY 0,5 Over =>  {shortAverage.Away_HT_05_Over.ToResponseOverVisualise()}\n");
                    strBuilder2.Append($"\n");
                    strBuilder2.Append($"SH HOME 0,5 Over =>  {shortAverage.Home_SH_05_Over.ToResponseOverVisualise()}\n");
                    strBuilder2.Append($"SH AWAY 0,5 Over =>  {shortAverage.Away_SH_05_Over.ToResponseOverVisualise()}\n");
                    strBuilder2.Append($"\n===============================\n");

                    var cntStr2 = strBuilder2.ToString();

                    var strBuilder3 = new StringBuilder();
                    var shortAverageHomeAway = new AverageShort(item.AverageProfilerHomeAway);
                    strBuilder3.Append($"=== Home at home / Away at away ===\n");
                    strBuilder3.Append($"===============================\n");
                    strBuilder3.Append($"\n");
                    strBuilder3.Append($"FT Home Goals Average =>  {shortAverageHomeAway.Average_FT_Goals_HomeTeam}\n");
                    strBuilder3.Append($"FT Away Goals Average =>  {shortAverageHomeAway.Average_FT_Goals_AwayTeam}\n");
                    strBuilder3.Append($"\n");
                    strBuilder3.Append($"HT Home Goals Average =>  {shortAverageHomeAway.Average_HT_Goals_HomeTeam}\n");
                    strBuilder3.Append($"HT Away Goals Average =>  {shortAverageHomeAway.Average_HT_Goals_AwayTeam}\n");
                    strBuilder3.Append($"\n");
                    strBuilder3.Append($"SH Home Goals Average =>  {shortAverageHomeAway.Average_SH_Goals_HomeTeam}\n");
                    strBuilder3.Append($"SH Away Goals Average =>  {shortAverageHomeAway.Average_SH_Goals_AwayTeam}\n");
                    strBuilder3.Append($"\n");
                    strBuilder3.Append($"FT Win1 =>  {shortAverageHomeAway.Is_FT_Win1.ToResponseWinLoseVisualise()}\n");
                    strBuilder3.Append($"FT X =>  {shortAverageHomeAway.Is_FT_X.ToResponseWinLoseVisualise()}\n");
                    strBuilder3.Append($"FT Win2 =>  {shortAverageHomeAway.Is_FT_Win2.ToResponseWinLoseVisualise()}\n");
                    strBuilder3.Append($"\n");
                    strBuilder3.Append($"HT Win1 =>  {shortAverageHomeAway.Is_HT_Win1.ToResponseWinLoseVisualise()}\n");
                    strBuilder3.Append($"HT X =>  {shortAverageHomeAway.Is_HT_X.ToResponseWinLoseVisualise()}\n");
                    strBuilder3.Append($"HT Win2 =>  {shortAverageHomeAway.Is_HT_Win2.ToResponseWinLoseVisualise()}\n");
                    strBuilder3.Append($"\n");
                    strBuilder3.Append($"SH Win1 =>  {shortAverageHomeAway.Is_SH_Win1.ToResponseWinLoseVisualise()}\n");
                    strBuilder3.Append($"SH X =>  {shortAverageHomeAway.Is_SH_X.ToResponseWinLoseVisualise()}\n");
                    strBuilder3.Append($"SH Win2 =>  {shortAverageHomeAway.Is_SH_Win2.ToResponseWinLoseVisualise()}\n");
                    strBuilder3.Append($"\n");
                    strBuilder3.Append($"FT 1,5 Under/Over =>  {shortAverageHomeAway.FT_15_Over.ToResponseOverVisualise()}\n");
                    strBuilder3.Append($"FT 2,5 Under/Over =>  {shortAverageHomeAway.FT_25_Over.ToResponseOverVisualise()}\n");
                    strBuilder3.Append($"\n");
                    strBuilder3.Append($"HT 0,5 Under/Over =>  {shortAverageHomeAway.HT_05_Over.ToResponseOverVisualise()}\n");
                    strBuilder3.Append($"\n");
                    strBuilder3.Append($"SH 0,5 Under/Over =>  {shortAverageHomeAway.SH_05_Over.ToResponseOverVisualise()}\n");
                    strBuilder3.Append($"\n");
                    strBuilder3.Append($"FT Both teams will score? =>  {shortAverageHomeAway.FT_GG.ToResponseBothGoalVisualise()}\n");
                    strBuilder3.Append($"\n");
                    strBuilder3.Append($"FT HOME 0,5 Over =>  {shortAverageHomeAway.Home_FT_05_Over.ToResponseOverVisualise()}\n");
                    strBuilder3.Append($"FT AWAY 0,5 Over =>  {shortAverageHomeAway.Away_FT_05_Over.ToResponseOverVisualise()}\n");
                    strBuilder3.Append($"\n");
                    strBuilder3.Append($"HT HOME 0,5 Over =>  {shortAverageHomeAway.Home_HT_05_Over.ToResponseOverVisualise()}\n");
                    strBuilder3.Append($"HT AWAY 0,5 Over =>  {shortAverageHomeAway.Away_HT_05_Over.ToResponseOverVisualise()}\n");
                    strBuilder3.Append($"\n");
                    strBuilder3.Append($"SH HOME 0,5 Over =>  {shortAverageHomeAway.Home_SH_05_Over.ToResponseOverVisualise()}\n");
                    strBuilder3.Append($"SH AWAY 0,5 Over =>  {shortAverageHomeAway.Away_SH_05_Over.ToResponseOverVisualise()}\n");
                    strBuilder3.Append($"\n========================");

                    var cntStr3 = strBuilder3.ToString();

                    strBuilder1.Append(cntStr2);
                    strBuilder1.Append(cntStr3);


                    var strBuilderCorner = new StringBuilder();

                    strBuilderCorner.Append($"=== CORNER Informations ===\n");
                    strBuilderCorner.Append($"---- LIGA Corner Informations ----\n");
                    //strBuilderCorner.Append($"LINK:  http://arsiv.mackolik.com/Match/Default.aspx?id={item.ComparisonInfoContainer.Serial}\n");
                    //strBuilderCorner.Append($"MATCH:  {item.ComparisonInfoContainer.Home} - {item.ComparisonInfoContainer.Away}\n");
                    //strBuilderCorner.Append($"COUNTRY:  {countryName}\n");
                    //strBuilderCorner.Append($"LEAGUE:  {leagueName} \n");
                    strBuilderCorner.Append($"===========================\n");
                    strBuilderCorner.Append($"FT Corner Average => {leagueHolder.CornerAverage.ToString("0.00")}\n");
                    strBuilderCorner.Append($"Home Corner Average => {leagueHolder.HomeCornerAverage.ToString("0.00")}\n");
                    strBuilderCorner.Append($"Away Corner Average => {leagueHolder.AwayCornerAverage.ToString("0.00")}\n");
                    strBuilderCorner.Append($"Corner 7,5 Under/Over => {leagueHolder.Corner_Over_7_5_Percentage.ToResponseOverVisualise()}\n");
                    strBuilderCorner.Append($"Corner 8,5 Under/Over => {leagueHolder.Corner_Over_8_5_Percentage.ToResponseOverVisualise()}\n");
                    strBuilderCorner.Append($"Corner 9,5 Under/Over => {leagueHolder.Corner_Over_9_5_Percentage.ToResponseOverVisualise()}\n");
                    strBuilderCorner.Append($"Home Corner 3,5 Under/Over => {leagueHolder.Home_Corner_35_Over_Percentage.ToResponseOverVisualise()}\n");
                    strBuilderCorner.Append($"Home Corner 4,5 Under/Over => {leagueHolder.Home_Corner_45_Over_Percentage.ToResponseOverVisualise()}\n");
                    strBuilderCorner.Append($"Home Corner 5,5 Under/Over => {leagueHolder.Home_Corner_55_Over_Percentage.ToResponseOverVisualise()}\n");
                    strBuilderCorner.Append($"Away Corner 3,5 Under/Over => {leagueHolder.Away_Corner_35_Over_Percentage.ToResponseOverVisualise()}\n");
                    strBuilderCorner.Append($"Away Corner 4,5 Under/Over => {leagueHolder.Away_Corner_45_Over_Percentage.ToResponseOverVisualise()}\n");
                    strBuilderCorner.Append($"Away Corner 5,5 Under/Over => {leagueHolder.Away_Corner_55_Over_Percentage.ToResponseOverVisualise()}\n");
                    strBuilderCorner.Append($"Corner FT Win1 => {leagueHolder.Corner_FT_Win1_Percentage.ToResponseWinLoseVisualise()}\n");
                    strBuilderCorner.Append($"Corner FT X => {leagueHolder.Corner_FT_X_Percentage.ToResponseWinLoseVisualise()}\n");
                    strBuilderCorner.Append($"Corner FT Win2 => {leagueHolder.Corner_FT_Win2_Percentage.ToResponseWinLoseVisualise()}\n");
                    strBuilderCorner.Append($"========================\n");

                    var strBuild11 = new StringBuilder();
                    strBuild11.Append($"=== General ===\n");
                    strBuild11.Append($"-------------\n");
                    strBuild11.Append($"FT Home Corner Average =>  {shortAverage.Average_FT_Corners_HomeTeam}\n");
                    strBuild11.Append($"FT Away Corner Average =>  {shortAverage.Average_FT_Corners_AwayTeam}\n");
                    strBuild11.Append($"FT Corner 7,5 Under/Over =>  {shortAverage.Corner_7_5_Over.ToResponseOverVisualise("shortAverage.Corner_7_5_Over")}\n");
                    strBuild11.Append($"FT Corner 8,5 Under/Over =>  {shortAverage.Corner_8_5_Over.ToResponseOverVisualise("shortAverage.Corner_8_5_Over")}\n");
                    strBuild11.Append($"FT Corner 9,5 Under/Over =>  {shortAverage.Corner_9_5_Over.ToResponseOverVisualise("shortAverage.Corner_9_5_Over")}\n");
                    strBuild11.Append($"------------------------\n");
                    strBuild11.Append($"FT HOME Corner 3,5 Over =>  {shortAverage.Corner_Home_3_5_Over.ToResponseOverVisualise("shortAverage.Corner_Home_3_5_Over")}\n");
                    strBuild11.Append($"FT Away Corner 3,5 Over =>  {shortAverage.Corner_Away_3_5_Over.ToResponseOverVisualise("shortAverage.Corner_Away_3_5_Over")}\n");
                    strBuild11.Append($"------------------------\n");
                    strBuild11.Append($"FT HOME Corner 4,5 Over =>  {shortAverage.Corner_Home_4_5_Over.ToResponseOverVisualise("shortAverage.Corner_Home_4_5_Over")}\n");
                    strBuild11.Append($"FT Away Corner 4,5 Over =>  {shortAverage.Corner_Away_4_5_Over.ToResponseOverVisualise("shortAverage.Corner_Away_4_5_Over")}\n");
                    strBuild11.Append($"------------------------\n");
                    strBuild11.Append($"FT HOME Corner 5,5 Over =>  {shortAverage.Corner_Home_5_5_Over.ToResponseOverVisualise("shortAverage.Corner_Home_5_5_Over")}\n");
                    strBuild11.Append($"FT Away Corner 5,5 Over =>  {shortAverage.Corner_Away_5_5_Over.ToResponseOverVisualise("shortAverage.Corner_Away_5_5_Over")}\n");
                    strBuild11.Append($"Corner FT Win 1 =>  {shortAverage.Is_Corner_FT_Win1.ToResponseWinLoseVisualise("shortAverage.Is_Corner_FT_Win1")}\n");
                    strBuild11.Append($"Corner FT X =>  {shortAverage.Is_Corner_FT_X.ToResponseWinLoseVisualise("shortAverage.Is_Corner_FT_X")}\n");
                    strBuild11.Append($"Corner FT Win 2 =>  {shortAverage.Is_Corner_FT_Win2.ToResponseWinLoseVisualise("shortAverage.Is_Corner_FT_Win2")}\n");
                    strBuild11.Append($"\n========================\n");
                    strBuilderCorner.Append(strBuild11.ToString());
                    var strBuild22 = new StringBuilder();
                    strBuild22.Append($"=== Home at home / Away at away ===\n");
                    strBuild22.Append($"-------------\n");
                    strBuild22.Append($"FT Home Corner Average =>  {shortAverageHomeAway.Average_FT_Corners_HomeTeam}\n");
                    strBuild22.Append($"FT Away Corner Average =>  {shortAverageHomeAway.Average_FT_Corners_AwayTeam}\n");
                    strBuild22.Append($"FT Corner 7,5 Under/Over =>  {shortAverageHomeAway.Corner_7_5_Over.ToResponseOverVisualise("shortAverageHomeAway.Corner_7_5_Over")}\n");
                    strBuild22.Append($"FT Corner 8,5 Under/Over =>  {shortAverageHomeAway.Corner_8_5_Over.ToResponseOverVisualise("shortAverageHomeAway.Corner_8_5_Over")}\n");
                    strBuild22.Append($"FT Corner 9,5 Under/Over =>  {shortAverageHomeAway.Corner_9_5_Over.ToResponseOverVisualise("shortAverageHomeAway.Corner_9_5_Over")}\n");
                    strBuild22.Append($"------------------------\n");
                    strBuild22.Append($"FT HOME Corner 3,5 Over =>  {shortAverageHomeAway.Corner_Home_3_5_Over.ToResponseOverVisualise("shortAverageHomeAway.Corner_Home_3_5_Over")}\n");
                    strBuild22.Append($"FT Away Corner 3,5 Over =>  {shortAverageHomeAway.Corner_Away_3_5_Over.ToResponseOverVisualise("shortAverageHomeAway.Corner_Away_3_5_Over")}\n");
                    strBuild22.Append($"------------------------\n");
                    strBuild22.Append($"FT HOME Corner 4,5 Over =>  {shortAverageHomeAway.Corner_Home_4_5_Over.ToResponseOverVisualise("shortAverageHomeAway.Corner_Home_4_5_Over")}\n");
                    strBuild22.Append($"FT Away Corner 4,5 Over =>  {shortAverageHomeAway.Corner_Away_4_5_Over.ToResponseOverVisualise("shortAverageHomeAway.Corner_Away_4_5_Over")}\n");
                    strBuild22.Append($"------------------------\n");
                    strBuild22.Append($"FT HOME Corner 5,5 Over =>  {shortAverageHomeAway.Corner_Home_5_5_Over.ToResponseOverVisualise("shortAverageHomeAway.Corner_Home_5_5_Over")}\n");
                    strBuild22.Append($"FT Away Corner 5,5 Over =>  {shortAverageHomeAway.Corner_Away_5_5_Over.ToResponseOverVisualise("shortAverageHomeAway.Corner_Away_5_5_Over")}\n");
                    strBuild22.Append($"Corner FT Win 1 =>  {shortAverageHomeAway.Is_Corner_FT_Win1.ToResponseWinLoseVisualise("shortAverageHomeAway.Is_Corner_FT_Win1")}\n");
                    strBuild22.Append($"Corner FT X =>  {shortAverageHomeAway.Is_Corner_FT_X.ToResponseWinLoseVisualise("shortAverageHomeAway.Is_Corner_FT_X")}\n");
                    strBuild22.Append($"Corner FT Win 2 =>  {shortAverageHomeAway.Is_Corner_FT_Win2.ToResponseWinLoseVisualise("shortAverageHomeAway.Is_Corner_FT_Win2")}\n");
                    strBuild22.Append($"\n========================\n");
                    strBuilderCorner.Append(strBuild22.ToString());

                    if (userCheck.IsMeChecked)
                        botServiceManager.SendMessage(userCheck.MyID, strBuilder1.ToString());
                    if (userCheck.IsEldarChecked)
                        botServiceManager.SendMessage(userCheck.EldarID, strBuilder1.ToString());
                    if (userCheck.IsOnurChecked)
                        botServiceManager.SendMessage(userCheck.OnurID, strBuilder1.ToString());

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

                    if (userCheck.IsMeChecked)
                        botServiceManager.SendMessage(userCheck.MyID, strBuilderCorner.ToString());
                    if (userCheck.IsEldarChecked)
                        botServiceManager.SendMessage(userCheck.EldarID, strBuilderCorner.ToString());
                    if (userCheck.IsOnurChecked)
                        botServiceManager.SendMessage(userCheck.OnurID, strBuilderCorner.ToString());

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
