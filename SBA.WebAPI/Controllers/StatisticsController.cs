using Core.Entities.Concrete.ComplexModels.ML;
using Core.Entities.Concrete.ExternalDbEntities;
using Core.Entities.Dtos.ComplexDataes.UIData;
using Core.Extensions;
using Core.Resources.Enums;
using Core.Utilities.Helpers.Serialization;
using Core.Utilities.UsableModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SBA.Business.Abstract;
using SBA.Business.BusinessHelper;
using SBA.Business.ExternalServices.Abstract;
using SBA.Business.ExternalServices.ChatGPT;
using SBA.Business.ExternalServices.ChatGPT.Models;
using SBA.WebAPI.Utilities.Extensions;
using SBA.WebAPI.Utilities.Helpers;
using SharpCompress.Common;
using System.Configuration;
using Telegram.Bot.Types;

namespace SBA.WebAPI.Controllers
{
    [Route("api/statistics")]
    [ApiController]
    [BasicAuthentication]
    public class StatisticsController : ControllerBase
    {
        private readonly IComparisonStatisticsHolderService _comparisonStatisticsHolderService;
        private readonly IAverageStatisticsHolderService _averageStatisticsHolderService;
        private readonly ITeamPerformanceStatisticsHolderService _teamPerformanceStatisticsHolderService;
        private readonly ILeagueStatisticsHolderService _leagueStatisticsHolderService;
        private readonly IStatisticInfoHolderService _statisticInfoHolderService;
        private readonly IAiDataHolderService _aiDataHolderService;
        private readonly ITranslationService _translationService;
        private readonly IMatchBetService _matchBetService;
        private readonly IMatchIdentifierService _matchIdentifierService;
        private readonly IFilterResultService _filterResultService;
        private readonly IForecastService _forecastService;
        private readonly IConfiguration _configuration;
        private readonly IExtLogService _logService;
        private readonly ChatGPTService _aiService;
        private readonly FileFormatBinder _formatBinder;

        private readonly string _jsonPathFormat;
        private readonly string _txtPathFormat;


        public StatisticsController(IComparisonStatisticsHolderService comparisonStatisticsHolderService,
                                    ITeamPerformanceStatisticsHolderService teamPerformanceStatisticsHolderService,
                                    IAverageStatisticsHolderService averageStatisticsHolderService,
                                    ILeagueStatisticsHolderService leagueStatisticsHolderService,
                                    ITranslationService translationService,
                                    IConfiguration configuration,
                                    IStatisticInfoHolderService statisticInfoHolderService,
                                    IAiDataHolderService aiDataHolderService,
                                    IMatchBetService matchBetService,
                                    IMatchIdentifierService matchIdentifierService,
                                    IForecastService forecastService,
                                    IFilterResultService filterResultService,
                                    IExtLogService logService)
        {
            _comparisonStatisticsHolderService = comparisonStatisticsHolderService;
            _teamPerformanceStatisticsHolderService = teamPerformanceStatisticsHolderService;
            _averageStatisticsHolderService = averageStatisticsHolderService;
            _leagueStatisticsHolderService = leagueStatisticsHolderService;
            _translationService = translationService;
            _configuration = configuration;
            string apiKey = _configuration.GetValue<string>("OpenAI-SecretKey");
            _jsonPathFormat = _configuration.GetSection("PathConstant").GetValue<string>("JsonPathFormat");
            _txtPathFormat = _configuration.GetSection("PathConstant").GetValue<string>("TextFormat");
            _aiService = new ChatGPTService(apiKey);
            _formatBinder = new FileFormatBinder();
            _statisticInfoHolderService = statisticInfoHolderService;
            _aiDataHolderService = aiDataHolderService;
            _matchBetService = matchBetService;
            _matchIdentifierService = matchIdentifierService;
            _forecastService = forecastService;
            _filterResultService = filterResultService;
            _logService = logService;
        }

        [HttpGet("getaverage/home-away/{serial}")]
        public IActionResult GetAverageOnlyHomeAwayStatistics(int serial)
        {
            int bySideType = (int)BySideType.HomeAway;
            var result = _averageStatisticsHolderService.GetAverageMatchResultById(serial, bySideType);

            return Ok(result);
        }

        #region MobileServices
        [HttpGet("getstatistics/mobile/{serial}/{language}")]
        public IActionResult GetAllMobileStatistics(int serial, int language)
        {
            var result = _statisticInfoHolderService.GetAllStatisticResultById(serial, language);

            return Ok(result);
        }

        [HttpGet("getaverage/mobile/home-away/{serial}/{language}")]
        public IActionResult GetAverageOnlyHomeAwayMobileStatistics(int serial, int language)
        {
            int bySideType = (int)BySideType.HomeAway;
            var result = _statisticInfoHolderService.GetAverageStatisticResultById(serial, bySideType, language);

            return Ok(result);
        }

        [HttpGet("getaverage/mobile/general/{serial}/{language}")]
        public IActionResult GetAverageOnlyGeneralMobileStatistics(int serial, int language)
        {
            int bySideType = (int)BySideType.General;
            var result = _statisticInfoHolderService.GetAverageStatisticResultById(serial, bySideType, language);

            return Ok(result);
        }

        [HttpGet("getperformance/mobile/home-away/{serial}/{language}")]
        public IActionResult GetPerformanceOnlyHomeAwayMobileStatistics(int serial, int language)
        {
            int bySideType = (int)BySideType.HomeAway;
            var result = _statisticInfoHolderService.GetPerformanceStatisticResultById(serial, bySideType, language);

            return Ok(result);
        }

        [HttpGet("getperformance/mobile/general/{serial}/{language}")]
        public IActionResult GetPerformanceOnlyGeneralMobileStatistics(int serial, int language)
        {
            int bySideType = (int)BySideType.General;
            var result = _statisticInfoHolderService.GetPerformanceStatisticResultById(serial, bySideType, language);

            return Ok(result);
        }

        [HttpGet("getcomparison/mobile/home-away/{serial}/{language}")]
        public IActionResult GetComparisonOnlyHomeAwayMobileStatistics(int serial, int language)
        {
            int bySideType = (int)BySideType.HomeAway;
            var result = _statisticInfoHolderService.GetComparisonStatisticResultById(serial, bySideType, language);

            return Ok(result);
        }

        [HttpGet("getcomparison/mobile/general/{serial}/{language}")]
        public IActionResult GetComparisonOnlyGeneralMobileStatistics(int serial, int language)
        {
            int bySideType = (int)BySideType.General;
            var result = _statisticInfoHolderService.GetComparisonStatisticResultById(serial, bySideType, language);

            return Ok(result);
        }
        #endregion




        [HttpGet("getaverage/general/{serial}")]
        public IActionResult GetAverageOnlyGeneralStatistics(int serial)
        {
            int bySideType = (int)BySideType.General;
            var result = _averageStatisticsHolderService.GetAverageMatchResultById(serial, bySideType);

            return Ok(result);
        }

        [HttpGet("gethomeperformance/home-away/{serial}")]
        public IActionResult GetHomePerformanceOnlyHomeAwayStatistics(int serial)
        {
            int bySideType = (int)BySideType.HomeAway;
            int homeOrAway = (int)HomeOrAway.Home;
            var result = _teamPerformanceStatisticsHolderService.GetPerformanceMatchResultById(serial, bySideType, homeOrAway);

            return Ok(result);
        }

        [HttpGet("gethomeperformance/general/{serial}")]
        public IActionResult GetHomePerformanceOnlyGeneralStatistics(int serial)
        {
            int bySideType = (int)BySideType.General;
            int homeOrAway = (int)HomeOrAway.Home;
            var result = _teamPerformanceStatisticsHolderService.GetPerformanceMatchResultById(serial, bySideType, homeOrAway);

            return Ok(result);
        }

        [HttpGet("getawayperformance/home-away/{serial}")]
        public IActionResult GetAwayPerformanceOnlyHomeAwayStatistics(int serial)
        {
            int bySideType = (int)BySideType.HomeAway;
            int homeOrAway = (int)HomeOrAway.Away;
            var result = _teamPerformanceStatisticsHolderService.GetPerformanceMatchResultById(serial, bySideType, homeOrAway);

            return Ok(result);
        }

        [HttpGet("getawayperformance/general/{serial}")]
        public IActionResult GetAwayPerformanceOnlyGeneralStatistics(int serial)
        {
            int bySideType = (int)BySideType.General;
            int homeOrAway = (int)HomeOrAway.Away;
            var result = _teamPerformanceStatisticsHolderService.GetPerformanceMatchResultById(serial, bySideType, homeOrAway);

            return Ok(result);
        }

        [HttpGet("getcomparison/home-away/{serial}")]
        public IActionResult GetComparisonOnlyDbStatistics(int serial)
        {
            int bySideType = (int)BySideType.HomeAway;
            var result = _comparisonStatisticsHolderService.GetComparisonMatchResultById(serial, bySideType);

            return Ok(result);
        }

        [HttpGet("getcomparison/general/{serial}")]
        public IActionResult GetComparisonAutomatedStatistics(int serial)
        {
            int bySideType = (int)BySideType.General;
            var result = _comparisonStatisticsHolderService.GetComparisonMatchResultById(serial, bySideType);

            return Ok(result);
        }


        [HttpGet("getcomparison-ui-response/{byside}/{serial}")]
        public IActionResult GetComparisonResponseModel(int serial, int byside)
        {
            var result = _leagueStatisticsHolderService.GetComparisonStatistics(serial, byside);

            return Ok(result);
        }

        [HttpGet("getperformance-ui-response/{bySide}/{homeOrAway}/{serial}")]
        public IActionResult GetPerformanceResponseModel(int serial, int bySide, int homeOrAway)
        {
            var result = _leagueStatisticsHolderService.GetPerformanceStatistics(serial, bySide, homeOrAway);

            return Ok(result);
        }

        [HttpGet("getleaguestatistics-ui-response/{serial}")]
        public IActionResult GetLeagueStatisticsResponseModel(int serial)
        {
            var result = _leagueStatisticsHolderService.GetLeagueStatistics(serial);

            return Ok(result);
        }


        [HttpGet("get-ai-guess/{serial}")]
        public async Task<string> GetAIGuessAsync(int serial)
        {
            var aiModel = await _aiDataHolderService.GetAsync(x => x.Serial == serial);

            if (aiModel.Data == null)
                return "Data Has Not Been Found...";

            var statisticsModel = JsonConvert.DeserializeObject<AiAnalyseModel>(aiModel.Data.JsonTextContent);

            if (statisticsModel == null)
                return "Data Has Not Been Found...";

            statisticsModel.AwayTeamPerformanceMatches = null;
            statisticsModel.HomeTeamPerformanceMatches = null;
            statisticsModel.ComparisonDataes = null;

            var serializerOptions = new JsonSerializerSettings
            {
                Formatting = Formatting.None,
                NullValueHandling = NullValueHandling.Ignore
            };
            string statisticsData = JsonConvert.SerializeObject(statisticsModel, serializerOptions);

            var messages = new List<RequestMessageGPT>();

            messages.Add(new RequestMessageGPT
            {
                Role = OpenAI_API.Chat.ChatMessageRole.System,
                Content = "Analyze the provided soccer match statistics and predict the most likely outcome. Return the prediction using one of these specific keys: 'FT_Both_Team_Score', 'FT_1_5_Over', 'FT_2_5_Over', 'FT_3_5_Under', 'HT_0_5_Over', 'SH_0_5_Over', 'HT_1_5_Under'. If a high-probability prediction is not possible or none of the keys are suitable, return 'NOT'"
            });

            messages.Add(new RequestMessageGPT
            {
                Role = OpenAI_API.Chat.ChatMessageRole.User,
                Content = statisticsData
            });

            var requestConfig = new RequestConfigGPT(500, (float)0.0);

            var resultGuess = await _aiService.CallOpenAIAsync(messages, requestConfig, GptEngineType.Default_GPT_4);

            return resultGuess;
        }


        [HttpGet("get-complex-response/{serial}")]
        public async Task<IActionResult> GetComplexResponseModelAsync(int serial)
        {
            var resultComplexData = _leagueStatisticsHolderService.GetAiComplexStatistics(serial);

            return Ok(resultComplexData);
        }



        [HttpGet("get-readable-model/{serial}")]
        public async Task<IActionResult> GetFormatModelAsync(int serial)
        {
            string statPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", "readable", "readstatFormat.txt");
            string statPosShutPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", "readable", "readstatTeamPosShutFormat.txt");
            string statTeamCornerPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", "readable", "readstatTeamCornerFormat.txt");
            string statAllCornerPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", "readable", "readstatAllTeamsCornerFormat.txt");

            string statFormat;
            string statPosShutFormat;
            string statTeamCornerFormat;
            string statAllCornerFormat;

            using (var sr = new StreamReader(statPath))
            {
                statFormat = sr.ReadToEnd();
            }

            using (var sr = new StreamReader(statPosShutPath))
            {
                statPosShutFormat = sr.ReadToEnd();
            }

            using (var sr = new StreamReader(statTeamCornerPath))
            {
                statTeamCornerFormat = sr.ReadToEnd();
            }

            using (var sr = new StreamReader(statAllCornerPath))
            {
                statAllCornerFormat = sr.ReadToEnd();
            }

            var resultComplexData = _leagueStatisticsHolderService.GetAiComplexStatistics(serial);

            if (resultComplexData is null)
                return NotFound("AI Model Could not be found!");

            var resultAiModel = _formatBinder.BindComplexStats(statFormat, statPosShutFormat, statTeamCornerFormat, statAllCornerFormat, resultComplexData);

            return Ok(resultAiModel);
        }


        [HttpGet("get-intime-oddstatistics/{serial}/{range}")]
        public IActionResult GetInTimeOddStatistics(int serial, decimal range)
        {
            var model = _matchBetService.GetOddFilteredResult(serial, range).OrderBy(x => x.Order).ToList();

            return Ok(model);
        }


        [HttpGet("get-overall-performance-statistics/{serial}")]
        public IActionResult GetPerformanceOverallStatistics(int serial)
        {
            var model = _matchBetService.GetPerformanceOverallResult(serial).OrderBy(x => x.Order).ToList();

            return Ok(model);
        }


        [HttpGet("get-forecast-productivity/{day}/{month}/{year}/{is99Percent}")]
        public async Task<IActionResult> GetForecastProductivity(int day, int month, int year, bool is99Percent)
        {
            ForecastDataContainer model = null;
            var filterDate = new DateTime(year, month, day);
            if (filterDate.Date == DateTime.UtcNow.ToAzeDate())
                model = await _forecastService.SelectForecastContainerInfoAsync(true, is99Percent);
            else
                model = await _forecastService.SelectForecastContainerInfoAsync(true, is99Percent, x => x.MatchDateTime.Date == filterDate.Date);

            return Ok(model);
        }


        [HttpGet("get-forecast-by-serial/{serial}")]
        public async Task<IActionResult> GetForecastBySerialAsync(int serial)
        {
            var model = await _forecastService.SelectForecastsBySerialAsync(serial);

            return Ok(model);
        }






        //
        [HttpGet("calculate-report/{minCount}")]
        public async Task<IActionResult> CalculateReports(int minCount, int homeOrAway = 1)
        {
            List<int> serials = new List<int>();

            using (StreamReader srCacheReader = new StreamReader(_txtPathFormat.GetTextFileByFormat("reportingOddSerials")))
            {
                string line;
                while ((line = srCacheReader.ReadLine()) != null)
                {
                    serials.Add(Convert.ToInt32(line));
                }
            }

            var reportAllCount = 0;
            var reportCorrectCount = 0;

            var ggFtOdd60Percent = new CalculationReportingModel("ODD: FT_GG", 60, serials.Count);
            var ggFtOdd65Percent = new CalculationReportingModel("ODD: FT_GG", 65, serials.Count);
            var ggFtOdd70Percent = new CalculationReportingModel("ODD: FT_GG", 70, serials.Count);
            var ggFtOdd75Percent = new CalculationReportingModel("ODD: FT_GG", 75, serials.Count);
            var ggFtOdd80Percent = new CalculationReportingModel("ODD: FT_GG", 80, serials.Count);

            var ngFtOdd60Percent = new CalculationReportingModel("ODD: FT_NG", 40, serials.Count);
            var ngFtOdd65Percent = new CalculationReportingModel("ODD: FT_NG", 35, serials.Count);
            var ngFtOdd70Percent = new CalculationReportingModel("ODD: FT_NG", 30, serials.Count);
            var ngFtOdd75Percent = new CalculationReportingModel("ODD: FT_NG", 25, serials.Count);
            var ngFtOdd80Percent = new CalculationReportingModel("ODD: FT_NG", 20, serials.Count);

            var overFt15Odd75Percent = new CalculationReportingModel("ODD: FT_15_Over", 75, serials.Count);
            var overFt15Odd80Percent = new CalculationReportingModel("ODD: FT_15_Over", 80, serials.Count);
            var overFt15Odd85Percent = new CalculationReportingModel("ODD: FT_15_Over", 85, serials.Count);
            var overFt15Odd90Percent = new CalculationReportingModel("ODD: FT_15_Over", 90, serials.Count);
            var overFt15Odd95Percent = new CalculationReportingModel("ODD: FT_15_Over", 95, serials.Count);

            var overHt05Odd75Percent = new CalculationReportingModel("ODD: HT_05_Over", 75, serials.Count);
            var overHt05Odd80Percent = new CalculationReportingModel("ODD: HT_05_Over", 80, serials.Count);
            var overHt05Odd85Percent = new CalculationReportingModel("ODD: HT_05_Over", 85, serials.Count);
            var overHt05Odd90Percent = new CalculationReportingModel("ODD: HT_05_Over", 90, serials.Count);
            var overHt05Odd95Percent = new CalculationReportingModel("ODD: HT_05_Over", 95, serials.Count);

            var overHt15UnderOdd75Percent = new CalculationReportingModel("ODD: HT_15_Under", 25, serials.Count);
            var overHt15UnderOdd80Percent = new CalculationReportingModel("ODD: HT_15_Under", 20, serials.Count);
            var overHt15UnderOdd85Percent = new CalculationReportingModel("ODD: HT_15_Under", 15, serials.Count);
            var overHt15UnderOdd90Percent = new CalculationReportingModel("ODD: HT_15_Under", 10, serials.Count);
            var overHt15UnderOdd95Percent = new CalculationReportingModel("ODD: HT_15_Under", 5, serials.Count);

            var overSh15UnderOdd75Percent = new CalculationReportingModel("ODD: SH_15_Under", 25, serials.Count);
            var overSh15UnderOdd80Percent = new CalculationReportingModel("ODD: SH_15_Under", 20, serials.Count);
            var overSh15UnderOdd85Percent = new CalculationReportingModel("ODD: SH_15_Under", 15, serials.Count);
            var overSh15UnderOdd90Percent = new CalculationReportingModel("ODD: SH_15_Under", 10, serials.Count);
            var overSh15UnderOdd95Percent = new CalculationReportingModel("ODD: SH_15_Under", 5, serials.Count);

            var overFt35UnderOdd75Percent = new CalculationReportingModel("ODD: FT_35_Under", 25, serials.Count);
            var overFt35UnderOdd80Percent = new CalculationReportingModel("ODD: FT_35_Under", 20, serials.Count);
            var overFt35UnderOdd85Percent = new CalculationReportingModel("ODD: FT_35_Under", 15, serials.Count);
            var overFt35UnderOdd90Percent = new CalculationReportingModel("ODD: FT_35_Under", 10, serials.Count);
            var overFt35UnderOdd95Percent = new CalculationReportingModel("ODD: FT_35_Under", 5, serials.Count);

            var overSh05Odd75Percent = new CalculationReportingModel("ODD: SH_05_Over", 75, serials.Count);
            var overSh05Odd80Percent = new CalculationReportingModel("ODD: SH_05_Over", 80, serials.Count);
            var overSh05Odd85Percent = new CalculationReportingModel("ODD: SH_05_Over", 85, serials.Count);
            var overSh05Odd90Percent = new CalculationReportingModel("ODD: SH_05_Over", 90, serials.Count);
            var overSh05Odd95Percent = new CalculationReportingModel("ODD: SH_05_Over", 95, serials.Count);

            var overFt25Odd60Percent = new CalculationReportingModel("ODD: FT_25_Over", 60, serials.Count);
            var overFt25Odd65Percent = new CalculationReportingModel("ODD: FT_25_Over", 65, serials.Count);
            var overFt25Odd70Percent = new CalculationReportingModel("ODD: FT_25_Over", 70, serials.Count);
            var overFt25Odd75Percent = new CalculationReportingModel("ODD: FT_25_Over", 75, serials.Count);
            var overFt25Odd80Percent = new CalculationReportingModel("ODD: FT_25_Over", 80, serials.Count);





            var overFtHome05Odd70Percent = new CalculationReportingModel("ODD: FT_Home_05_Over", 70, serials.Count);
            var overFtHome05Odd75Percent = new CalculationReportingModel("ODD: FT_Home_05_Over", 75, serials.Count);
            var overFtHome05Odd80Percent = new CalculationReportingModel("ODD: FT_Home_05_Over", 80, serials.Count);
            var overFtHome05Odd85Percent = new CalculationReportingModel("ODD: FT_Home_05_Over", 85, serials.Count);
            var overFtHome05Odd90Percent = new CalculationReportingModel("ODD: FT_Home_05_Over", 90, serials.Count);
            var overFtHome05Odd95Percent = new CalculationReportingModel("ODD: FT_Home_05_Over", 95, serials.Count);

            var overFtAway05Odd70Percent = new CalculationReportingModel("ODD: FT_Away_05_Over", 70, serials.Count);
            var overFtAway05Odd75Percent = new CalculationReportingModel("ODD: FT_Away_05_Over", 75, serials.Count);
            var overFtAway05Odd80Percent = new CalculationReportingModel("ODD: FT_Away_05_Over", 80, serials.Count);
            var overFtAway05Odd85Percent = new CalculationReportingModel("ODD: FT_Away_05_Over", 85, serials.Count);
            var overFtAway05Odd90Percent = new CalculationReportingModel("ODD: FT_Away_05_Over", 90, serials.Count);
            var overFtAway05Odd95Percent = new CalculationReportingModel("ODD: FT_Away_05_Over", 95, serials.Count);

            var overHtHome05Odd60Percent = new CalculationReportingModel("ODD: HT_Home_05_Over", 60, serials.Count);
            var overHtHome05Odd65Percent = new CalculationReportingModel("ODD: HT_Home_05_Over", 65, serials.Count);
            var overHtHome05Odd70Percent = new CalculationReportingModel("ODD: HT_Home_05_Over", 70, serials.Count);
            var overHtHome05Odd75Percent = new CalculationReportingModel("ODD: HT_Home_05_Over", 75, serials.Count);
            var overHtHome05Odd80Percent = new CalculationReportingModel("ODD: HT_Home_05_Over", 80, serials.Count);
            var overHtHome05Odd85Percent = new CalculationReportingModel("ODD: HT_Home_05_Over", 85, serials.Count);
            var overHtHome05Odd90Percent = new CalculationReportingModel("ODD: HT_Home_05_Over", 90, serials.Count);

            var overHtAway05Odd60Percent = new CalculationReportingModel("ODD: HT_Away_05_Over", 60, serials.Count);
            var overHtAway05Odd65Percent = new CalculationReportingModel("ODD: HT_Away_05_Over", 65, serials.Count);
            var overHtAway05Odd70Percent = new CalculationReportingModel("ODD: HT_Away_05_Over", 70, serials.Count);
            var overHtAway05Odd75Percent = new CalculationReportingModel("ODD: HT_Away_05_Over", 75, serials.Count);
            var overHtAway05Odd80Percent = new CalculationReportingModel("ODD: HT_Away_05_Over", 80, serials.Count);
            var overHtAway05Odd85Percent = new CalculationReportingModel("ODD: HT_Away_05_Over", 85, serials.Count);
            var overHtAway05Odd90Percent = new CalculationReportingModel("ODD: HT_Away_05_Over", 90, serials.Count);

            var overShHome05Odd60Percent = new CalculationReportingModel("ODD: SH_Home_05_Over", 60, serials.Count);
            var overShHome05Odd65Percent = new CalculationReportingModel("ODD: SH_Home_05_Over", 65, serials.Count);
            var overShHome05Odd70Percent = new CalculationReportingModel("ODD: SH_Home_05_Over", 70, serials.Count);
            var overShHome05Odd75Percent = new CalculationReportingModel("ODD: SH_Home_05_Over", 75, serials.Count);
            var overShHome05Odd80Percent = new CalculationReportingModel("ODD: SH_Home_05_Over", 80, serials.Count);
            var overShHome05Odd85Percent = new CalculationReportingModel("ODD: SH_Home_05_Over", 85, serials.Count);
            var overShHome05Odd90Percent = new CalculationReportingModel("ODD: SH_Home_05_Over", 90, serials.Count);

            var overShAway05Odd60Percent = new CalculationReportingModel("ODD: SH_Away_05_Over", 60, serials.Count);
            var overShAway05Odd65Percent = new CalculationReportingModel("ODD: SH_Away_05_Over", 65, serials.Count);
            var overShAway05Odd70Percent = new CalculationReportingModel("ODD: SH_Away_05_Over", 70, serials.Count);
            var overShAway05Odd75Percent = new CalculationReportingModel("ODD: SH_Away_05_Over", 75, serials.Count);
            var overShAway05Odd80Percent = new CalculationReportingModel("ODD: SH_Away_05_Over", 80, serials.Count);
            var overShAway05Odd85Percent = new CalculationReportingModel("ODD: SH_Away_05_Over", 85, serials.Count);
            var overShAway05Odd90Percent = new CalculationReportingModel("ODD: SH_Away_05_Over", 90, serials.Count);


            var overFtHome15Odd60Percent = new CalculationReportingModel("ODD: FT_Home_15_Over", 60, serials.Count);
            var overFtHome15Odd65Percent = new CalculationReportingModel("ODD: FT_Home_15_Over", 65, serials.Count);
            var overFtHome15Odd70Percent = new CalculationReportingModel("ODD: FT_Home_15_Over", 70, serials.Count);
            var overFtHome15Odd75Percent = new CalculationReportingModel("ODD: FT_Home_15_Over", 75, serials.Count);
            var overFtHome15Odd80Percent = new CalculationReportingModel("ODD: FT_Home_15_Over", 80, serials.Count);
            var overFtHome15Odd85Percent = new CalculationReportingModel("ODD: FT_Home_15_Over", 85, serials.Count);
            var overFtHome15Odd90Percent = new CalculationReportingModel("ODD: FT_Home_15_Over", 90, serials.Count);
            var overFtHome15Odd95Percent = new CalculationReportingModel("ODD: FT_Home_15_Over", 95, serials.Count);

            var overFtAway15Odd60Percent = new CalculationReportingModel("ODD: FT_Away_15_Over", 60, serials.Count);
            var overFtAway15Odd65Percent = new CalculationReportingModel("ODD: FT_Away_15_Over", 65, serials.Count);
            var overFtAway15Odd70Percent = new CalculationReportingModel("ODD: FT_Away_15_Over", 70, serials.Count);
            var overFtAway15Odd75Percent = new CalculationReportingModel("ODD: FT_Away_15_Over", 75, serials.Count);
            var overFtAway15Odd80Percent = new CalculationReportingModel("ODD: FT_Away_15_Over", 80, serials.Count);
            var overFtAway15Odd85Percent = new CalculationReportingModel("ODD: FT_Away_15_Over", 85, serials.Count);
            var overFtAway15Odd90Percent = new CalculationReportingModel("ODD: FT_Away_15_Over", 90, serials.Count);
            var overFtAway15Odd95Percent = new CalculationReportingModel("ODD: FT_Away_15_Over", 95, serials.Count);




            var homeFtWinOdd45Percent = new CalculationReportingModel("ODD: FT_Home_Win", 45, serials.Count);
            var homeFtWinOdd50Percent = new CalculationReportingModel("ODD: FT_Home_Win", 50, serials.Count);
            var homeFtWinOdd55Percent = new CalculationReportingModel("ODD: FT_Home_Win", 55, serials.Count);
            var homeFtWinOdd60Percent = new CalculationReportingModel("ODD: FT_Home_Win", 60, serials.Count);
            var homeFtWinOdd65Percent = new CalculationReportingModel("ODD: FT_Home_Win", 65, serials.Count);
            var homeFtWinOdd70Percent = new CalculationReportingModel("ODD: FT_Home_Win", 70, serials.Count);
            var homeFtWinOdd75Percent = new CalculationReportingModel("ODD: FT_Home_Win", 75, serials.Count);
            var homeFtWinOdd80Percent = new CalculationReportingModel("ODD: FT_Home_Win", 80, serials.Count);
            var homeFtWinOdd85Percent = new CalculationReportingModel("ODD: FT_Home_Win", 85, serials.Count);
            var homeFtWinOdd90Percent = new CalculationReportingModel("ODD: FT_Home_Win", 90, serials.Count);

            var homeHtWinOdd45Percent = new CalculationReportingModel("ODD: HT_Home_Win", 45, serials.Count);
            var homeHtWinOdd50Percent = new CalculationReportingModel("ODD: HT_Home_Win", 50, serials.Count);
            var homeHtWinOdd55Percent = new CalculationReportingModel("ODD: HT_Home_Win", 55, serials.Count);
            var homeHtWinOdd60Percent = new CalculationReportingModel("ODD: HT_Home_Win", 60, serials.Count);
            var homeHtWinOdd65Percent = new CalculationReportingModel("ODD: HT_Home_Win", 65, serials.Count);
            var homeHtWinOdd70Percent = new CalculationReportingModel("ODD: HT_Home_Win", 70, serials.Count);
            var homeHtWinOdd75Percent = new CalculationReportingModel("ODD: HT_Home_Win", 75, serials.Count);
            var homeHtWinOdd80Percent = new CalculationReportingModel("ODD: HT_Home_Win", 80, serials.Count);
            var homeHtWinOdd85Percent = new CalculationReportingModel("ODD: HT_Home_Win", 85, serials.Count);
            var homeHtWinOdd90Percent = new CalculationReportingModel("ODD: HT_Home_Win", 90, serials.Count);

            var homeShWinOdd45Percent = new CalculationReportingModel("ODD: SH_Home_Win", 45, serials.Count);
            var homeShWinOdd50Percent = new CalculationReportingModel("ODD: SH_Home_Win", 50, serials.Count);
            var homeShWinOdd55Percent = new CalculationReportingModel("ODD: SH_Home_Win", 55, serials.Count);
            var homeShWinOdd60Percent = new CalculationReportingModel("ODD: SH_Home_Win", 60, serials.Count);
            var homeShWinOdd65Percent = new CalculationReportingModel("ODD: SH_Home_Win", 65, serials.Count);
            var homeShWinOdd70Percent = new CalculationReportingModel("ODD: SH_Home_Win", 70, serials.Count);
            var homeShWinOdd75Percent = new CalculationReportingModel("ODD: SH_Home_Win", 75, serials.Count);
            var homeShWinOdd80Percent = new CalculationReportingModel("ODD: SH_Home_Win", 80, serials.Count);
            var homeShWinOdd85Percent = new CalculationReportingModel("ODD: SH_Home_Win", 85, serials.Count);
            var homeShWinOdd90Percent = new CalculationReportingModel("ODD: SH_Home_Win", 90, serials.Count);



            var awayFtWinOdd45Percent = new CalculationReportingModel("ODD: FT_Away_Win", 45, serials.Count);
            var awayFtWinOdd50Percent = new CalculationReportingModel("ODD: FT_Away_Win", 50, serials.Count);
            var awayFtWinOdd55Percent = new CalculationReportingModel("ODD: FT_Away_Win", 55, serials.Count);
            var awayFtWinOdd60Percent = new CalculationReportingModel("ODD: FT_Away_Win", 60, serials.Count);
            var awayFtWinOdd65Percent = new CalculationReportingModel("ODD: FT_Away_Win", 65, serials.Count);
            var awayFtWinOdd70Percent = new CalculationReportingModel("ODD: FT_Away_Win", 70, serials.Count);
            var awayFtWinOdd75Percent = new CalculationReportingModel("ODD: FT_Away_Win", 75, serials.Count);
            var awayFtWinOdd80Percent = new CalculationReportingModel("ODD: FT_Away_Win", 80, serials.Count);
            var awayFtWinOdd85Percent = new CalculationReportingModel("ODD: FT_Away_Win", 85, serials.Count);
            var awayFtWinOdd90Percent = new CalculationReportingModel("ODD: FT_Away_Win", 90, serials.Count);

            var awayHtWinOdd45Percent = new CalculationReportingModel("ODD: HT_Away_Win", 45, serials.Count);
            var awayHtWinOdd50Percent = new CalculationReportingModel("ODD: HT_Away_Win", 50, serials.Count);
            var awayHtWinOdd55Percent = new CalculationReportingModel("ODD: HT_Away_Win", 55, serials.Count);
            var awayHtWinOdd60Percent = new CalculationReportingModel("ODD: HT_Away_Win", 60, serials.Count);
            var awayHtWinOdd65Percent = new CalculationReportingModel("ODD: HT_Away_Win", 65, serials.Count);
            var awayHtWinOdd70Percent = new CalculationReportingModel("ODD: HT_Away_Win", 70, serials.Count);
            var awayHtWinOdd75Percent = new CalculationReportingModel("ODD: HT_Away_Win", 75, serials.Count);
            var awayHtWinOdd80Percent = new CalculationReportingModel("ODD: HT_Away_Win", 80, serials.Count);
            var awayHtWinOdd85Percent = new CalculationReportingModel("ODD: HT_Away_Win", 85, serials.Count);
            var awayHtWinOdd90Percent = new CalculationReportingModel("ODD: HT_Away_Win", 90, serials.Count);

            var awayShWinOdd45Percent = new CalculationReportingModel("ODD: SH_Away_Win", 45, serials.Count);
            var awayShWinOdd50Percent = new CalculationReportingModel("ODD: SH_Away_Win", 50, serials.Count);
            var awayShWinOdd55Percent = new CalculationReportingModel("ODD: SH_Away_Win", 55, serials.Count);
            var awayShWinOdd60Percent = new CalculationReportingModel("ODD: SH_Away_Win", 60, serials.Count);
            var awayShWinOdd65Percent = new CalculationReportingModel("ODD: SH_Away_Win", 65, serials.Count);
            var awayShWinOdd70Percent = new CalculationReportingModel("ODD: SH_Away_Win", 70, serials.Count);
            var awayShWinOdd75Percent = new CalculationReportingModel("ODD: SH_Away_Win", 75, serials.Count);
            var awayShWinOdd80Percent = new CalculationReportingModel("ODD: SH_Away_Win", 80, serials.Count);
            var awayShWinOdd85Percent = new CalculationReportingModel("ODD: SH_Away_Win", 85, serials.Count);
            var awayShWinOdd90Percent = new CalculationReportingModel("ODD: SH_Away_Win", 90, serials.Count);




            var FtDrawOdd45Percent = new CalculationReportingModel("ODD: FT_Draw", 45, serials.Count);
            var FtDrawOdd50Percent = new CalculationReportingModel("ODD: FT_Draw", 50, serials.Count);
            var FtDrawOdd55Percent = new CalculationReportingModel("ODD: FT_Draw", 55, serials.Count);
            var FtDrawOdd60Percent = new CalculationReportingModel("ODD: FT_Draw", 60, serials.Count);
            var FtDrawOdd65Percent = new CalculationReportingModel("ODD: FT_Draw", 65, serials.Count);
            var FtDrawOdd70Percent = new CalculationReportingModel("ODD: FT_Draw", 70, serials.Count);
            var FtDrawOdd75Percent = new CalculationReportingModel("ODD: FT_Draw", 75, serials.Count);
            var FtDrawOdd80Percent = new CalculationReportingModel("ODD: FT_Draw", 80, serials.Count);
            var FtDrawOdd85Percent = new CalculationReportingModel("ODD: FT_Draw", 85, serials.Count);
            var FtDrawOdd90Percent = new CalculationReportingModel("ODD: FT_Draw", 90, serials.Count);

            var HtDrawOdd45Percent = new CalculationReportingModel("ODD: HT_Draw", 45, serials.Count);
            var HtDrawOdd50Percent = new CalculationReportingModel("ODD: HT_Draw", 50, serials.Count);
            var HtDrawOdd55Percent = new CalculationReportingModel("ODD: HT_Draw", 55, serials.Count);
            var HtDrawOdd60Percent = new CalculationReportingModel("ODD: HT_Draw", 60, serials.Count);
            var HtDrawOdd65Percent = new CalculationReportingModel("ODD: HT_Draw", 65, serials.Count);
            var HtDrawOdd70Percent = new CalculationReportingModel("ODD: HT_Draw", 70, serials.Count);
            var HtDrawOdd75Percent = new CalculationReportingModel("ODD: HT_Draw", 75, serials.Count);
            var HtDrawOdd80Percent = new CalculationReportingModel("ODD: HT_Draw", 80, serials.Count);
            var HtDrawOdd85Percent = new CalculationReportingModel("ODD: HT_Draw", 85, serials.Count);
            var HtDrawOdd90Percent = new CalculationReportingModel("ODD: HT_Draw", 90, serials.Count);

            var ShDrawOdd45Percent = new CalculationReportingModel("ODD: SH_Draw", 45, serials.Count);
            var ShDrawOdd50Percent = new CalculationReportingModel("ODD: SH_Draw", 50, serials.Count);
            var ShDrawOdd55Percent = new CalculationReportingModel("ODD: SH_Draw", 55, serials.Count);
            var ShDrawOdd60Percent = new CalculationReportingModel("ODD: SH_Draw", 60, serials.Count);
            var ShDrawOdd65Percent = new CalculationReportingModel("ODD: SH_Draw", 65, serials.Count);
            var ShDrawOdd70Percent = new CalculationReportingModel("ODD: SH_Draw", 70, serials.Count);
            var ShDrawOdd75Percent = new CalculationReportingModel("ODD: SH_Draw", 75, serials.Count);
            var ShDrawOdd80Percent = new CalculationReportingModel("ODD: SH_Draw", 80, serials.Count);
            var ShDrawOdd85Percent = new CalculationReportingModel("ODD: SH_Draw", 85, serials.Count);
            var ShDrawOdd90Percent = new CalculationReportingModel("ODD: SH_Draw", 90, serials.Count);



            var ftWin1DrawOdd45Percent = new CalculationReportingModel("ODD: FT_Win1_X", 45, serials.Count);
            var ftWin1DrawOdd50Percent = new CalculationReportingModel("ODD: FT_Win1_X", 50, serials.Count);
            var ftWin1DrawOdd55Percent = new CalculationReportingModel("ODD: FT_Win1_X", 55, serials.Count);
            var ftWin1DrawOdd60Percent = new CalculationReportingModel("ODD: FT_Win1_X", 60, serials.Count);
            var ftWin1DrawOdd65Percent = new CalculationReportingModel("ODD: FT_Win1_X", 65, serials.Count);
            var ftWin1DrawOdd70Percent = new CalculationReportingModel("ODD: FT_Win1_X", 70, serials.Count);
            var ftWin1DrawOdd75Percent = new CalculationReportingModel("ODD: FT_Win1_X", 75, serials.Count);
            var ftWin1DrawOdd80Percent = new CalculationReportingModel("ODD: FT_Win1_X", 80, serials.Count);
            var ftWin1DrawOdd85Percent = new CalculationReportingModel("ODD: FT_Win1_X", 85, serials.Count);
            var ftWin1DrawOdd90Percent = new CalculationReportingModel("ODD: FT_Win1_X", 90, serials.Count);

            var htWin1DrawOdd45Percent = new CalculationReportingModel("ODD: HT_Win1_X", 45, serials.Count);
            var htWin1DrawOdd50Percent = new CalculationReportingModel("ODD: HT_Win1_X", 50, serials.Count);
            var htWin1DrawOdd55Percent = new CalculationReportingModel("ODD: HT_Win1_X", 55, serials.Count);
            var htWin1DrawOdd60Percent = new CalculationReportingModel("ODD: HT_Win1_X", 60, serials.Count);
            var htWin1DrawOdd65Percent = new CalculationReportingModel("ODD: HT_Win1_X", 65, serials.Count);
            var htWin1DrawOdd70Percent = new CalculationReportingModel("ODD: HT_Win1_X", 70, serials.Count);
            var htWin1DrawOdd75Percent = new CalculationReportingModel("ODD: HT_Win1_X", 75, serials.Count);
            var htWin1DrawOdd80Percent = new CalculationReportingModel("ODD: HT_Win1_X", 80, serials.Count);
            var htWin1DrawOdd85Percent = new CalculationReportingModel("ODD: HT_Win1_X", 85, serials.Count);
            var htWin1DrawOdd90Percent = new CalculationReportingModel("ODD: HT_Win1_X", 90, serials.Count);

            var shWin1DrawOdd45Percent = new CalculationReportingModel("ODD: SH_Win1_X", 45, serials.Count);
            var shWin1DrawOdd50Percent = new CalculationReportingModel("ODD: SH_Win1_X", 50, serials.Count);
            var shWin1DrawOdd55Percent = new CalculationReportingModel("ODD: SH_Win1_X", 55, serials.Count);
            var shWin1DrawOdd60Percent = new CalculationReportingModel("ODD: SH_Win1_X", 60, serials.Count);
            var shWin1DrawOdd65Percent = new CalculationReportingModel("ODD: SH_Win1_X", 65, serials.Count);
            var shWin1DrawOdd70Percent = new CalculationReportingModel("ODD: SH_Win1_X", 70, serials.Count);
            var shWin1DrawOdd75Percent = new CalculationReportingModel("ODD: SH_Win1_X", 75, serials.Count);
            var shWin1DrawOdd80Percent = new CalculationReportingModel("ODD: SH_Win1_X", 80, serials.Count);
            var shWin1DrawOdd85Percent = new CalculationReportingModel("ODD: SH_Win1_X", 85, serials.Count);
            var shWin1DrawOdd90Percent = new CalculationReportingModel("ODD: SH_Win1_X", 90, serials.Count);


            var ftWin2DrawOdd45Percent = new CalculationReportingModel("ODD: FT_X_Win2", 45, serials.Count);
            var ftWin2DrawOdd50Percent = new CalculationReportingModel("ODD: FT_X_Win2", 50, serials.Count);
            var ftWin2DrawOdd55Percent = new CalculationReportingModel("ODD: FT_X_Win2", 55, serials.Count);
            var ftWin2DrawOdd60Percent = new CalculationReportingModel("ODD: FT_X_Win2", 60, serials.Count);
            var ftWin2DrawOdd65Percent = new CalculationReportingModel("ODD: FT_X_Win2", 65, serials.Count);
            var ftWin2DrawOdd70Percent = new CalculationReportingModel("ODD: FT_X_Win2", 70, serials.Count);
            var ftWin2DrawOdd75Percent = new CalculationReportingModel("ODD: FT_X_Win2", 75, serials.Count);
            var ftWin2DrawOdd80Percent = new CalculationReportingModel("ODD: FT_X_Win2", 80, serials.Count);
            var ftWin2DrawOdd85Percent = new CalculationReportingModel("ODD: FT_X_Win2", 85, serials.Count);
            var ftWin2DrawOdd90Percent = new CalculationReportingModel("ODD: FT_X_Win2", 90, serials.Count);

            var htWin2DrawOdd45Percent = new CalculationReportingModel("ODD: HT_X_Win2", 45, serials.Count);
            var htWin2DrawOdd50Percent = new CalculationReportingModel("ODD: HT_X_Win2", 50, serials.Count);
            var htWin2DrawOdd55Percent = new CalculationReportingModel("ODD: HT_X_Win2", 55, serials.Count);
            var htWin2DrawOdd60Percent = new CalculationReportingModel("ODD: HT_X_Win2", 60, serials.Count);
            var htWin2DrawOdd65Percent = new CalculationReportingModel("ODD: HT_X_Win2", 65, serials.Count);
            var htWin2DrawOdd70Percent = new CalculationReportingModel("ODD: HT_X_Win2", 70, serials.Count);
            var htWin2DrawOdd75Percent = new CalculationReportingModel("ODD: HT_X_Win2", 75, serials.Count);
            var htWin2DrawOdd80Percent = new CalculationReportingModel("ODD: HT_X_Win2", 80, serials.Count);
            var htWin2DrawOdd85Percent = new CalculationReportingModel("ODD: HT_X_Win2", 85, serials.Count);
            var htWin2DrawOdd90Percent = new CalculationReportingModel("ODD: HT_X_Win2", 90, serials.Count);

            var shWin2DrawOdd45Percent = new CalculationReportingModel("ODD: SH_X_Win2", 45, serials.Count);
            var shWin2DrawOdd50Percent = new CalculationReportingModel("ODD: SH_X_Win2", 50, serials.Count);
            var shWin2DrawOdd55Percent = new CalculationReportingModel("ODD: SH_X_Win2", 55, serials.Count);
            var shWin2DrawOdd60Percent = new CalculationReportingModel("ODD: SH_X_Win2", 60, serials.Count);
            var shWin2DrawOdd65Percent = new CalculationReportingModel("ODD: SH_X_Win2", 65, serials.Count);
            var shWin2DrawOdd70Percent = new CalculationReportingModel("ODD: SH_X_Win2", 70, serials.Count);
            var shWin2DrawOdd75Percent = new CalculationReportingModel("ODD: SH_X_Win2", 75, serials.Count);
            var shWin2DrawOdd80Percent = new CalculationReportingModel("ODD: SH_X_Win2", 80, serials.Count);
            var shWin2DrawOdd85Percent = new CalculationReportingModel("ODD: SH_X_Win2", 85, serials.Count);
            var shWin2DrawOdd90Percent = new CalculationReportingModel("ODD: SH_X_Win2", 90, serials.Count);


            var ftWin1Win2Odd45Percent = new CalculationReportingModel("ODD: FT_Win1_Win2", 45, serials.Count);
            var ftWin1Win2Odd50Percent = new CalculationReportingModel("ODD: FT_Win1_Win2", 50, serials.Count);
            var ftWin1Win2Odd55Percent = new CalculationReportingModel("ODD: FT_Win1_Win2", 55, serials.Count);
            var ftWin1Win2Odd60Percent = new CalculationReportingModel("ODD: FT_Win1_Win2", 60, serials.Count);
            var ftWin1Win2Odd65Percent = new CalculationReportingModel("ODD: FT_Win1_Win2", 65, serials.Count);
            var ftWin1Win2Odd70Percent = new CalculationReportingModel("ODD: FT_Win1_Win2", 70, serials.Count);
            var ftWin1Win2Odd75Percent = new CalculationReportingModel("ODD: FT_Win1_Win2", 75, serials.Count);
            var ftWin1Win2Odd80Percent = new CalculationReportingModel("ODD: FT_Win1_Win2", 80, serials.Count);
            var ftWin1Win2Odd85Percent = new CalculationReportingModel("ODD: FT_Win1_Win2", 85, serials.Count);
            var ftWin1Win2Odd90Percent = new CalculationReportingModel("ODD: FT_Win1_Win2", 90, serials.Count);

            var htWin1Win2Odd45Percent = new CalculationReportingModel("ODD: HT_Win1_Win2", 45, serials.Count);
            var htWin1Win2Odd50Percent = new CalculationReportingModel("ODD: HT_Win1_Win2", 50, serials.Count);
            var htWin1Win2Odd55Percent = new CalculationReportingModel("ODD: HT_Win1_Win2", 55, serials.Count);
            var htWin1Win2Odd60Percent = new CalculationReportingModel("ODD: HT_Win1_Win2", 60, serials.Count);
            var htWin1Win2Odd65Percent = new CalculationReportingModel("ODD: HT_Win1_Win2", 65, serials.Count);
            var htWin1Win2Odd70Percent = new CalculationReportingModel("ODD: HT_Win1_Win2", 70, serials.Count);
            var htWin1Win2Odd75Percent = new CalculationReportingModel("ODD: HT_Win1_Win2", 75, serials.Count);
            var htWin1Win2Odd80Percent = new CalculationReportingModel("ODD: HT_Win1_Win2", 80, serials.Count);
            var htWin1Win2Odd85Percent = new CalculationReportingModel("ODD: HT_Win1_Win2", 85, serials.Count);
            var htWin1Win2Odd90Percent = new CalculationReportingModel("ODD: HT_Win1_Win2", 90, serials.Count);

            var shWin1Win2Odd45Percent = new CalculationReportingModel("ODD: SH_Win1_Win2", 45, serials.Count);
            var shWin1Win2Odd50Percent = new CalculationReportingModel("ODD: SH_Win1_Win2", 50, serials.Count);
            var shWin1Win2Odd55Percent = new CalculationReportingModel("ODD: SH_Win1_Win2", 55, serials.Count);
            var shWin1Win2Odd60Percent = new CalculationReportingModel("ODD: SH_Win1_Win2", 60, serials.Count);
            var shWin1Win2Odd65Percent = new CalculationReportingModel("ODD: SH_Win1_Win2", 65, serials.Count);
            var shWin1Win2Odd70Percent = new CalculationReportingModel("ODD: SH_Win1_Win2", 70, serials.Count);
            var shWin1Win2Odd75Percent = new CalculationReportingModel("ODD: SH_Win1_Win2", 75, serials.Count);
            var shWin1Win2Odd80Percent = new CalculationReportingModel("ODD: SH_Win1_Win2", 80, serials.Count);
            var shWin1Win2Odd85Percent = new CalculationReportingModel("ODD: SH_Win1_Win2", 85, serials.Count);
            var shWin1Win2Odd90Percent = new CalculationReportingModel("ODD: SH_Win1_Win2", 90, serials.Count);



            //var ggFtOverall60Percent = new CalculationReportingModel("OVERALL: FT_GG", 60, serials.Count);
            //var ggFtOverall65Percent = new CalculationReportingModel("OVERALL: FT_GG", 65, serials.Count);
            //var ggFtOverall70Percent = new CalculationReportingModel("OVERALL: FT_GG", 70, serials.Count);
            //var ggFtOverall75Percent = new CalculationReportingModel("OVERALL: FT_GG", 75, serials.Count);
            //var ggFtOverall80Percent = new CalculationReportingModel("OVERALL: FT_GG", 80, serials.Count);

            //var ngFtOverall60Percent = new CalculationReportingModel("OVERALL: FT_NG", 40, serials.Count);
            //var ngFtOverall65Percent = new CalculationReportingModel("OVERALL: FT_NG", 35, serials.Count);
            //var ngFtOverall70Percent = new CalculationReportingModel("OVERALL: FT_NG", 30, serials.Count);
            //var ngFtOverall75Percent = new CalculationReportingModel("OVERALL: FT_NG", 25, serials.Count);
            //var ngFtOverall80Percent = new CalculationReportingModel("OVERALL: FT_NG", 20, serials.Count);

            //var overFt15Overall75Percent = new CalculationReportingModel("OVERALL: FT_15_Over", 75, serials.Count);
            //var overFt15Overall80Percent = new CalculationReportingModel("OVERALL: FT_15_Over", 80, serials.Count);
            //var overFt15Overall85Percent = new CalculationReportingModel("OVERALL: FT_15_Over", 85, serials.Count);
            //var overFt15Overall90Percent = new CalculationReportingModel("OVERALL: FT_15_Over", 90, serials.Count);
            //var overFt15Overall95Percent = new CalculationReportingModel("OVERALL: FT_15_Over", 95, serials.Count);

            //var overHt05Overall75Percent = new CalculationReportingModel("OVERALL: HT_05_Over", 75, serials.Count);
            //var overHt05Overall80Percent = new CalculationReportingModel("OVERALL: HT_05_Over", 80, serials.Count);
            //var overHt05Overall85Percent = new CalculationReportingModel("OVERALL: HT_05_Over", 85, serials.Count);
            //var overHt05Overall90Percent = new CalculationReportingModel("OVERALL: HT_05_Over", 90, serials.Count);
            //var overHt05Overall95Percent = new CalculationReportingModel("OVERALL: HT_05_Over", 95, serials.Count);

            ////var overHt15UnderOverall75Percent = new CalculationReportingModel("OVERALL: HT_15_Under", 25, serials.Count);
            ////var overHt15UnderOverall80Percent = new CalculationReportingModel("OVERALL: HT_15_Under", 20, serials.Count);
            ////var overHt15UnderOverall85Percent = new CalculationReportingModel("OVERALL: HT_15_Under", 15, serials.Count);
            ////var overHt15UnderOverall90Percent = new CalculationReportingModel("OVERALL: HT_15_Under", 10, serials.Count);
            ////var overHt15UnderOverall95Percent = new CalculationReportingModel("OVERALL: HT_15_Under", 5, serials.Count);

            ////var overSh15UnderOverall75Percent = new CalculationReportingModel("OVERALL: SH_15_Under", 25, serials.Count);
            ////var overSh15UnderOverall80Percent = new CalculationReportingModel("OVERALL: SH_15_Under", 20, serials.Count);
            ////var overSh15UnderOverall85Percent = new CalculationReportingModel("OVERALL: SH_15_Under", 15, serials.Count);
            ////var overSh15UnderOverall90Percent = new CalculationReportingModel("OVERALL: SH_15_Under", 10, serials.Count);
            ////var overSh15UnderOverall95Percent = new CalculationReportingModel("OVERALL: SH_15_Under", 5, serials.Count);

            //var overFt35UnderOverall75Percent = new CalculationReportingModel("OVERALL: FT_35_Under", 25, serials.Count);
            //var overFt35UnderOverall80Percent = new CalculationReportingModel("OVERALL: FT_35_Under", 20, serials.Count);
            //var overFt35UnderOverall85Percent = new CalculationReportingModel("OVERALL: FT_35_Under", 15, serials.Count);
            //var overFt35UnderOverall90Percent = new CalculationReportingModel("OVERALL: FT_35_Under", 10, serials.Count);
            //var overFt35UnderOverall95Percent = new CalculationReportingModel("OVERALL: FT_35_Under", 5, serials.Count);

            //var overSh05Overall75Percent = new CalculationReportingModel("OVERALL: SH_05_Over", 75, serials.Count);
            //var overSh05Overall80Percent = new CalculationReportingModel("OVERALL: SH_05_Over", 80, serials.Count);
            //var overSh05Overall85Percent = new CalculationReportingModel("OVERALL: SH_05_Over", 85, serials.Count);
            //var overSh05Overall90Percent = new CalculationReportingModel("OVERALL: SH_05_Over", 90, serials.Count);
            //var overSh05Overall95Percent = new CalculationReportingModel("OVERALL: SH_05_Over", 95, serials.Count);

            //var overFt25Overall60Percent = new CalculationReportingModel("OVERALL: FT_25_Over", 60, serials.Count);
            //var overFt25Overall65Percent = new CalculationReportingModel("OVERALL: FT_25_Over", 65, serials.Count);
            //var overFt25Overall70Percent = new CalculationReportingModel("OVERALL: FT_25_Over", 70, serials.Count);
            //var overFt25Overall75Percent = new CalculationReportingModel("OVERALL: FT_25_Over", 75, serials.Count);
            //var overFt25Overall80Percent = new CalculationReportingModel("OVERALL: FT_25_Over", 80, serials.Count);

            for (int i = 0; i < serials.Count; i++)
            {
                int serial = serials[i];

                var statInfoHolders = _matchBetService.GetOddFilteredResult(serial, (decimal)0.05).OrderBy(x => x.Order).ToList();
                // var statOverallInfoHolders = _matchBetService.GetPerformanceOverallResult(serial).OrderBy(x => x.Order).ToList();

                if (statInfoHolders != null && statInfoHolders.Count > 0)
                {
                    ggFtOdd60Percent = Calculate_GG_Yes(statInfoHolders, ggFtOdd60Percent, serial);
                    ggFtOdd65Percent = Calculate_GG_Yes(statInfoHolders, ggFtOdd65Percent, serial);
                    ggFtOdd70Percent = Calculate_GG_Yes(statInfoHolders, ggFtOdd70Percent, serial);
                    ggFtOdd75Percent = Calculate_GG_Yes(statInfoHolders, ggFtOdd75Percent, serial);
                    ggFtOdd80Percent = Calculate_GG_Yes(statInfoHolders, ggFtOdd80Percent, serial);

                    ngFtOdd60Percent = Calculate_GG_NO(statInfoHolders, ngFtOdd60Percent, serial);
                    ngFtOdd65Percent = Calculate_GG_NO(statInfoHolders, ngFtOdd65Percent, serial);
                    ngFtOdd70Percent = Calculate_GG_NO(statInfoHolders, ngFtOdd70Percent, serial);
                    ngFtOdd75Percent = Calculate_GG_NO(statInfoHolders, ngFtOdd75Percent, serial);
                    ngFtOdd80Percent = Calculate_GG_NO(statInfoHolders, ngFtOdd80Percent, serial);

                    overFt15Odd75Percent = Calculate_15_Over(statInfoHolders, overFt15Odd75Percent, serial);
                    overFt15Odd80Percent = Calculate_15_Over(statInfoHolders, overFt15Odd80Percent, serial);
                    overFt15Odd85Percent = Calculate_15_Over(statInfoHolders, overFt15Odd85Percent, serial);
                    overFt15Odd90Percent = Calculate_15_Over(statInfoHolders, overFt15Odd90Percent, serial);
                    overFt15Odd95Percent = Calculate_15_Over(statInfoHolders, overFt15Odd95Percent, serial);

                    overFt25Odd60Percent = Calculate_25_Over(statInfoHolders, overFt25Odd60Percent, serial);
                    overFt25Odd65Percent = Calculate_25_Over(statInfoHolders, overFt25Odd65Percent, serial);
                    overFt25Odd70Percent = Calculate_25_Over(statInfoHolders, overFt25Odd70Percent, serial);
                    overFt25Odd75Percent = Calculate_25_Over(statInfoHolders, overFt25Odd75Percent, serial);
                    overFt25Odd80Percent = Calculate_25_Over(statInfoHolders, overFt25Odd80Percent, serial);

                    overFt35UnderOdd75Percent = Calculate_35_Under(statInfoHolders, overFt35UnderOdd75Percent, serial);
                    overFt35UnderOdd80Percent = Calculate_35_Under(statInfoHolders, overFt35UnderOdd80Percent, serial);
                    overFt35UnderOdd85Percent = Calculate_35_Under(statInfoHolders, overFt35UnderOdd85Percent, serial);
                    overFt35UnderOdd90Percent = Calculate_35_Under(statInfoHolders, overFt35UnderOdd90Percent, serial);
                    overFt35UnderOdd95Percent = Calculate_35_Under(statInfoHolders, overFt35UnderOdd95Percent, serial);

                    overHt05Odd75Percent = Calculate_HT_05_Over(statInfoHolders, overHt05Odd75Percent, serial);
                    overHt05Odd80Percent = Calculate_HT_05_Over(statInfoHolders, overHt05Odd80Percent, serial);
                    overHt05Odd85Percent = Calculate_HT_05_Over(statInfoHolders, overHt05Odd85Percent, serial);
                    overHt05Odd90Percent = Calculate_HT_05_Over(statInfoHolders, overHt05Odd90Percent, serial);
                    overHt05Odd95Percent = Calculate_HT_05_Over(statInfoHolders, overHt05Odd95Percent, serial);

                    overHt15UnderOdd75Percent = Calculate_HT_15_Under(statInfoHolders, overHt15UnderOdd75Percent, serial);
                    overHt15UnderOdd80Percent = Calculate_HT_15_Under(statInfoHolders, overHt15UnderOdd80Percent, serial);
                    overHt15UnderOdd85Percent = Calculate_HT_15_Under(statInfoHolders, overHt15UnderOdd85Percent, serial);
                    overHt15UnderOdd90Percent = Calculate_HT_15_Under(statInfoHolders, overHt15UnderOdd90Percent, serial);
                    overHt15UnderOdd95Percent = Calculate_HT_15_Under(statInfoHolders, overHt15UnderOdd95Percent, serial);

                    overSh05Odd75Percent = Calculate_SH_05_Over(statInfoHolders, overSh05Odd75Percent, serial);
                    overSh05Odd80Percent = Calculate_SH_05_Over(statInfoHolders, overSh05Odd80Percent, serial);
                    overSh05Odd85Percent = Calculate_SH_05_Over(statInfoHolders, overSh05Odd85Percent, serial);
                    overSh05Odd90Percent = Calculate_SH_05_Over(statInfoHolders, overSh05Odd90Percent, serial);
                    overSh05Odd95Percent = Calculate_SH_05_Over(statInfoHolders, overSh05Odd95Percent, serial);

                    overSh15UnderOdd75Percent = Calculate_SH_15_Under(statInfoHolders, overSh15UnderOdd75Percent, serial);
                    overSh15UnderOdd80Percent = Calculate_SH_15_Under(statInfoHolders, overSh15UnderOdd80Percent, serial);
                    overSh15UnderOdd85Percent = Calculate_SH_15_Under(statInfoHolders, overSh15UnderOdd85Percent, serial);
                    overSh15UnderOdd90Percent = Calculate_SH_15_Under(statInfoHolders, overSh15UnderOdd90Percent, serial);
                    overSh15UnderOdd95Percent = Calculate_SH_15_Under(statInfoHolders, overSh15UnderOdd95Percent, serial);

                    overFtHome05Odd70Percent = Calculate_Home_FT_05_Over(statInfoHolders, overFtHome05Odd70Percent, serial);
                    overFtHome05Odd75Percent = Calculate_Home_FT_05_Over(statInfoHolders, overFtHome05Odd75Percent, serial);
                    overFtHome05Odd80Percent = Calculate_Home_FT_05_Over(statInfoHolders, overFtHome05Odd80Percent, serial);
                    overFtHome05Odd85Percent = Calculate_Home_FT_05_Over(statInfoHolders, overFtHome05Odd85Percent, serial);
                    overFtHome05Odd90Percent = Calculate_Home_FT_05_Over(statInfoHolders, overFtHome05Odd90Percent, serial);
                    overFtHome05Odd95Percent = Calculate_Home_FT_05_Over(statInfoHolders, overFtHome05Odd95Percent, serial);


                    overFtAway05Odd70Percent = Calculate_Away_FT_05_Over(statInfoHolders, overFtAway05Odd70Percent, serial);
                    overFtAway05Odd75Percent = Calculate_Away_FT_05_Over(statInfoHolders, overFtAway05Odd75Percent, serial);
                    overFtAway05Odd80Percent = Calculate_Away_FT_05_Over(statInfoHolders, overFtAway05Odd80Percent, serial);
                    overFtAway05Odd85Percent = Calculate_Away_FT_05_Over(statInfoHolders, overFtAway05Odd85Percent, serial);
                    overFtAway05Odd90Percent = Calculate_Away_FT_05_Over(statInfoHolders, overFtAway05Odd90Percent, serial);
                    overFtAway05Odd95Percent = Calculate_Away_FT_05_Over(statInfoHolders, overFtAway05Odd95Percent, serial);

                    overHtHome05Odd60Percent = Calculate_Home_HT_05_Over(statInfoHolders, overHtHome05Odd60Percent, serial);
                    overHtHome05Odd65Percent = Calculate_Home_HT_05_Over(statInfoHolders, overHtHome05Odd65Percent, serial);
                    overHtHome05Odd70Percent = Calculate_Home_HT_05_Over(statInfoHolders, overHtHome05Odd70Percent, serial);
                    overHtHome05Odd75Percent = Calculate_Home_HT_05_Over(statInfoHolders, overHtHome05Odd75Percent, serial);
                    overHtHome05Odd80Percent = Calculate_Home_HT_05_Over(statInfoHolders, overHtHome05Odd80Percent, serial);
                    overHtHome05Odd85Percent = Calculate_Home_HT_05_Over(statInfoHolders, overHtHome05Odd85Percent, serial);
                    overHtHome05Odd90Percent = Calculate_Home_HT_05_Over(statInfoHolders, overHtHome05Odd90Percent, serial);

                    overHtAway05Odd60Percent = Calculate_Away_HT_05_Over(statInfoHolders, overHtAway05Odd60Percent, serial);
                    overHtAway05Odd65Percent = Calculate_Away_HT_05_Over(statInfoHolders, overHtAway05Odd65Percent, serial);
                    overHtAway05Odd70Percent = Calculate_Away_HT_05_Over(statInfoHolders, overHtAway05Odd70Percent, serial);
                    overHtAway05Odd75Percent = Calculate_Away_HT_05_Over(statInfoHolders, overHtAway05Odd75Percent, serial);
                    overHtAway05Odd80Percent = Calculate_Away_HT_05_Over(statInfoHolders, overHtAway05Odd80Percent, serial);
                    overHtAway05Odd85Percent = Calculate_Away_HT_05_Over(statInfoHolders, overHtAway05Odd85Percent, serial);
                    overHtAway05Odd90Percent = Calculate_Away_HT_05_Over(statInfoHolders, overHtAway05Odd90Percent, serial);


                    overShHome05Odd60Percent = Calculate_Home_SH_05_Over(statInfoHolders, overShHome05Odd60Percent, serial);
                    overShHome05Odd65Percent = Calculate_Home_SH_05_Over(statInfoHolders, overShHome05Odd65Percent, serial);
                    overShHome05Odd70Percent = Calculate_Home_SH_05_Over(statInfoHolders, overShHome05Odd70Percent, serial);
                    overShHome05Odd75Percent = Calculate_Home_SH_05_Over(statInfoHolders, overShHome05Odd75Percent, serial);
                    overShHome05Odd80Percent = Calculate_Home_SH_05_Over(statInfoHolders, overShHome05Odd80Percent, serial);
                    overShHome05Odd85Percent = Calculate_Home_SH_05_Over(statInfoHolders, overShHome05Odd85Percent, serial);
                    overShHome05Odd90Percent = Calculate_Home_SH_05_Over(statInfoHolders, overShHome05Odd90Percent, serial);

                    overShAway05Odd60Percent = Calculate_Away_SH_05_Over(statInfoHolders, overShAway05Odd60Percent, serial);
                    overShAway05Odd65Percent = Calculate_Away_SH_05_Over(statInfoHolders, overShAway05Odd65Percent, serial);
                    overShAway05Odd70Percent = Calculate_Away_SH_05_Over(statInfoHolders, overShAway05Odd70Percent, serial);
                    overShAway05Odd75Percent = Calculate_Away_SH_05_Over(statInfoHolders, overShAway05Odd75Percent, serial);
                    overShAway05Odd80Percent = Calculate_Away_SH_05_Over(statInfoHolders, overShAway05Odd80Percent, serial);
                    overShAway05Odd85Percent = Calculate_Away_SH_05_Over(statInfoHolders, overShAway05Odd85Percent, serial);
                    overShAway05Odd90Percent = Calculate_Away_SH_05_Over(statInfoHolders, overShAway05Odd90Percent, serial);


                    overFtHome15Odd60Percent = Calculate_Home_FT_15_Over(statInfoHolders, overFtHome15Odd60Percent, serial);
                    overFtHome15Odd65Percent = Calculate_Home_FT_15_Over(statInfoHolders, overFtHome15Odd65Percent, serial);
                    overFtHome15Odd70Percent = Calculate_Home_FT_15_Over(statInfoHolders, overFtHome15Odd70Percent, serial);
                    overFtHome15Odd75Percent = Calculate_Home_FT_15_Over(statInfoHolders, overFtHome15Odd75Percent, serial);
                    overFtHome15Odd80Percent = Calculate_Home_FT_15_Over(statInfoHolders, overFtHome15Odd80Percent, serial);
                    overFtHome15Odd85Percent = Calculate_Home_FT_15_Over(statInfoHolders, overFtHome15Odd85Percent, serial);
                    overFtHome15Odd90Percent = Calculate_Home_FT_15_Over(statInfoHolders, overFtHome15Odd90Percent, serial);
                    overFtHome15Odd95Percent = Calculate_Home_FT_15_Over(statInfoHolders, overFtHome15Odd95Percent, serial);


                    overFtAway15Odd60Percent = Calculate_Away_FT_15_Over(statInfoHolders, overFtAway15Odd60Percent, serial);
                    overFtAway15Odd65Percent = Calculate_Away_FT_15_Over(statInfoHolders, overFtAway15Odd65Percent, serial);
                    overFtAway15Odd70Percent = Calculate_Away_FT_15_Over(statInfoHolders, overFtAway15Odd70Percent, serial);
                    overFtAway15Odd75Percent = Calculate_Away_FT_15_Over(statInfoHolders, overFtAway15Odd75Percent, serial);
                    overFtAway15Odd80Percent = Calculate_Away_FT_15_Over(statInfoHolders, overFtAway15Odd80Percent, serial);
                    overFtAway15Odd85Percent = Calculate_Away_FT_15_Over(statInfoHolders, overFtAway15Odd85Percent, serial);
                    overFtAway15Odd90Percent = Calculate_Away_FT_15_Over(statInfoHolders, overFtAway15Odd90Percent, serial);
                    overFtAway15Odd95Percent = Calculate_Away_FT_15_Over(statInfoHolders, overFtAway15Odd95Percent, serial);


                    homeFtWinOdd45Percent = Calculate_Home_FT_Win(statInfoHolders, homeFtWinOdd45Percent, serial);
                    homeFtWinOdd50Percent = Calculate_Home_FT_Win(statInfoHolders, homeFtWinOdd50Percent, serial);
                    homeFtWinOdd55Percent = Calculate_Home_FT_Win(statInfoHolders, homeFtWinOdd55Percent, serial);
                    homeFtWinOdd60Percent = Calculate_Home_FT_Win(statInfoHolders, homeFtWinOdd60Percent, serial);
                    homeFtWinOdd65Percent = Calculate_Home_FT_Win(statInfoHolders, homeFtWinOdd65Percent, serial);
                    homeFtWinOdd70Percent = Calculate_Home_FT_Win(statInfoHolders, homeFtWinOdd70Percent, serial);
                    homeFtWinOdd75Percent = Calculate_Home_FT_Win(statInfoHolders, homeFtWinOdd75Percent, serial);
                    homeFtWinOdd80Percent = Calculate_Home_FT_Win(statInfoHolders, homeFtWinOdd80Percent, serial);
                    homeFtWinOdd85Percent = Calculate_Home_FT_Win(statInfoHolders, homeFtWinOdd85Percent, serial);
                    homeFtWinOdd90Percent = Calculate_Home_FT_Win(statInfoHolders, homeFtWinOdd90Percent, serial);


                    homeHtWinOdd45Percent = Calculate_Home_HT_Win(statInfoHolders, homeHtWinOdd45Percent, serial);
                    homeHtWinOdd50Percent = Calculate_Home_HT_Win(statInfoHolders, homeHtWinOdd50Percent, serial);
                    homeHtWinOdd55Percent = Calculate_Home_HT_Win(statInfoHolders, homeHtWinOdd55Percent, serial);
                    homeHtWinOdd60Percent = Calculate_Home_HT_Win(statInfoHolders, homeHtWinOdd60Percent, serial);
                    homeHtWinOdd65Percent = Calculate_Home_HT_Win(statInfoHolders, homeHtWinOdd65Percent, serial);
                    homeHtWinOdd70Percent = Calculate_Home_HT_Win(statInfoHolders, homeHtWinOdd70Percent, serial);
                    homeHtWinOdd75Percent = Calculate_Home_HT_Win(statInfoHolders, homeHtWinOdd75Percent, serial);
                    homeHtWinOdd80Percent = Calculate_Home_HT_Win(statInfoHolders, homeHtWinOdd80Percent, serial);
                    homeHtWinOdd85Percent = Calculate_Home_HT_Win(statInfoHolders, homeHtWinOdd85Percent, serial);
                    homeHtWinOdd90Percent = Calculate_Home_HT_Win(statInfoHolders, homeHtWinOdd90Percent, serial);


                    homeShWinOdd45Percent = Calculate_Home_SH_Win(statInfoHolders, homeShWinOdd45Percent, serial);
                    homeShWinOdd50Percent = Calculate_Home_SH_Win(statInfoHolders, homeShWinOdd50Percent, serial);
                    homeShWinOdd55Percent = Calculate_Home_SH_Win(statInfoHolders, homeShWinOdd55Percent, serial);
                    homeShWinOdd60Percent = Calculate_Home_SH_Win(statInfoHolders, homeShWinOdd60Percent, serial);
                    homeShWinOdd65Percent = Calculate_Home_SH_Win(statInfoHolders, homeShWinOdd65Percent, serial);
                    homeShWinOdd70Percent = Calculate_Home_SH_Win(statInfoHolders, homeShWinOdd70Percent, serial);
                    homeShWinOdd75Percent = Calculate_Home_SH_Win(statInfoHolders, homeShWinOdd75Percent, serial);
                    homeShWinOdd80Percent = Calculate_Home_SH_Win(statInfoHolders, homeShWinOdd80Percent, serial);
                    homeShWinOdd85Percent = Calculate_Home_SH_Win(statInfoHolders, homeShWinOdd85Percent, serial);
                    homeShWinOdd90Percent = Calculate_Home_SH_Win(statInfoHolders, homeShWinOdd90Percent, serial);




                    awayFtWinOdd45Percent = Calculate_Away_FT_Win(statInfoHolders, awayFtWinOdd45Percent, serial);
                    awayFtWinOdd50Percent = Calculate_Away_FT_Win(statInfoHolders, awayFtWinOdd50Percent, serial);
                    awayFtWinOdd55Percent = Calculate_Away_FT_Win(statInfoHolders, awayFtWinOdd55Percent, serial);
                    awayFtWinOdd60Percent = Calculate_Away_FT_Win(statInfoHolders, awayFtWinOdd60Percent, serial);
                    awayFtWinOdd65Percent = Calculate_Away_FT_Win(statInfoHolders, awayFtWinOdd65Percent, serial);
                    awayFtWinOdd70Percent = Calculate_Away_FT_Win(statInfoHolders, awayFtWinOdd70Percent, serial);
                    awayFtWinOdd75Percent = Calculate_Away_FT_Win(statInfoHolders, awayFtWinOdd75Percent, serial);
                    awayFtWinOdd80Percent = Calculate_Away_FT_Win(statInfoHolders, awayFtWinOdd80Percent, serial);
                    awayFtWinOdd85Percent = Calculate_Away_FT_Win(statInfoHolders, awayFtWinOdd85Percent, serial);
                    awayFtWinOdd90Percent = Calculate_Away_FT_Win(statInfoHolders, awayFtWinOdd90Percent, serial);


                    awayHtWinOdd45Percent = Calculate_Away_HT_Win(statInfoHolders, awayHtWinOdd45Percent, serial);
                    awayHtWinOdd50Percent = Calculate_Away_HT_Win(statInfoHolders, awayHtWinOdd50Percent, serial);
                    awayHtWinOdd55Percent = Calculate_Away_HT_Win(statInfoHolders, awayHtWinOdd55Percent, serial);
                    awayHtWinOdd60Percent = Calculate_Away_HT_Win(statInfoHolders, awayHtWinOdd60Percent, serial);
                    awayHtWinOdd65Percent = Calculate_Away_HT_Win(statInfoHolders, awayHtWinOdd65Percent, serial);
                    awayHtWinOdd70Percent = Calculate_Away_HT_Win(statInfoHolders, awayHtWinOdd70Percent, serial);
                    awayHtWinOdd75Percent = Calculate_Away_HT_Win(statInfoHolders, awayHtWinOdd75Percent, serial);
                    awayHtWinOdd80Percent = Calculate_Away_HT_Win(statInfoHolders, awayHtWinOdd80Percent, serial);
                    awayHtWinOdd85Percent = Calculate_Away_HT_Win(statInfoHolders, awayHtWinOdd85Percent, serial);
                    awayHtWinOdd90Percent = Calculate_Away_HT_Win(statInfoHolders, awayHtWinOdd90Percent, serial);


                    awayShWinOdd45Percent = Calculate_Away_SH_Win(statInfoHolders, awayShWinOdd45Percent, serial);
                    awayShWinOdd50Percent = Calculate_Away_SH_Win(statInfoHolders, awayShWinOdd50Percent, serial);
                    awayShWinOdd55Percent = Calculate_Away_SH_Win(statInfoHolders, awayShWinOdd55Percent, serial);
                    awayShWinOdd60Percent = Calculate_Away_SH_Win(statInfoHolders, awayShWinOdd60Percent, serial);
                    awayShWinOdd65Percent = Calculate_Away_SH_Win(statInfoHolders, awayShWinOdd65Percent, serial);
                    awayShWinOdd70Percent = Calculate_Away_SH_Win(statInfoHolders, awayShWinOdd70Percent, serial);
                    awayShWinOdd75Percent = Calculate_Away_SH_Win(statInfoHolders, awayShWinOdd75Percent, serial);
                    awayShWinOdd80Percent = Calculate_Away_SH_Win(statInfoHolders, awayShWinOdd80Percent, serial);
                    awayShWinOdd85Percent = Calculate_Away_SH_Win(statInfoHolders, awayShWinOdd85Percent, serial);
                    awayShWinOdd90Percent = Calculate_Away_SH_Win(statInfoHolders, awayShWinOdd90Percent, serial);


                    FtDrawOdd45Percent = Calculate_FT_Draw(statInfoHolders, FtDrawOdd45Percent, serial);
                    FtDrawOdd50Percent = Calculate_FT_Draw(statInfoHolders, FtDrawOdd50Percent, serial);
                    FtDrawOdd55Percent = Calculate_FT_Draw(statInfoHolders, FtDrawOdd55Percent, serial);
                    FtDrawOdd60Percent = Calculate_FT_Draw(statInfoHolders, FtDrawOdd60Percent, serial);
                    FtDrawOdd65Percent = Calculate_FT_Draw(statInfoHolders, FtDrawOdd65Percent, serial);
                    FtDrawOdd70Percent = Calculate_FT_Draw(statInfoHolders, FtDrawOdd70Percent, serial);
                    FtDrawOdd75Percent = Calculate_FT_Draw(statInfoHolders, FtDrawOdd75Percent, serial);
                    FtDrawOdd80Percent = Calculate_FT_Draw(statInfoHolders, FtDrawOdd80Percent, serial);
                    FtDrawOdd85Percent = Calculate_FT_Draw(statInfoHolders, FtDrawOdd85Percent, serial);
                    FtDrawOdd90Percent = Calculate_FT_Draw(statInfoHolders, FtDrawOdd90Percent, serial);


                    HtDrawOdd45Percent = Calculate_HT_Draw(statInfoHolders, HtDrawOdd45Percent, serial);
                    HtDrawOdd50Percent = Calculate_HT_Draw(statInfoHolders, HtDrawOdd50Percent, serial);
                    HtDrawOdd55Percent = Calculate_HT_Draw(statInfoHolders, HtDrawOdd55Percent, serial);
                    HtDrawOdd60Percent = Calculate_HT_Draw(statInfoHolders, HtDrawOdd60Percent, serial);
                    HtDrawOdd65Percent = Calculate_HT_Draw(statInfoHolders, HtDrawOdd65Percent, serial);
                    HtDrawOdd70Percent = Calculate_HT_Draw(statInfoHolders, HtDrawOdd70Percent, serial);
                    HtDrawOdd75Percent = Calculate_HT_Draw(statInfoHolders, HtDrawOdd75Percent, serial);
                    HtDrawOdd80Percent = Calculate_HT_Draw(statInfoHolders, HtDrawOdd80Percent, serial);
                    HtDrawOdd85Percent = Calculate_HT_Draw(statInfoHolders, HtDrawOdd85Percent, serial);
                    HtDrawOdd90Percent = Calculate_HT_Draw(statInfoHolders, HtDrawOdd90Percent, serial);


                    ShDrawOdd45Percent = Calculate_SH_Draw(statInfoHolders, ShDrawOdd45Percent, serial);
                    ShDrawOdd50Percent = Calculate_SH_Draw(statInfoHolders, ShDrawOdd50Percent, serial);
                    ShDrawOdd55Percent = Calculate_SH_Draw(statInfoHolders, ShDrawOdd55Percent, serial);
                    ShDrawOdd60Percent = Calculate_SH_Draw(statInfoHolders, ShDrawOdd60Percent, serial);
                    ShDrawOdd65Percent = Calculate_SH_Draw(statInfoHolders, ShDrawOdd65Percent, serial);
                    ShDrawOdd70Percent = Calculate_SH_Draw(statInfoHolders, ShDrawOdd70Percent, serial);
                    ShDrawOdd75Percent = Calculate_SH_Draw(statInfoHolders, ShDrawOdd75Percent, serial);
                    ShDrawOdd80Percent = Calculate_SH_Draw(statInfoHolders, ShDrawOdd80Percent, serial);
                    ShDrawOdd85Percent = Calculate_SH_Draw(statInfoHolders, ShDrawOdd85Percent, serial);
                    ShDrawOdd90Percent = Calculate_SH_Draw(statInfoHolders, ShDrawOdd90Percent, serial);


                    ftWin1DrawOdd45Percent = Calculate_FT_Win1_Or_Draw(statInfoHolders, ftWin1DrawOdd45Percent, serial);
                    ftWin1DrawOdd50Percent = Calculate_FT_Win1_Or_Draw(statInfoHolders, ftWin1DrawOdd50Percent, serial);
                    ftWin1DrawOdd55Percent = Calculate_FT_Win1_Or_Draw(statInfoHolders, ftWin1DrawOdd55Percent, serial);
                    ftWin1DrawOdd60Percent = Calculate_FT_Win1_Or_Draw(statInfoHolders, ftWin1DrawOdd60Percent, serial);
                    ftWin1DrawOdd65Percent = Calculate_FT_Win1_Or_Draw(statInfoHolders, ftWin1DrawOdd65Percent, serial);
                    ftWin1DrawOdd70Percent = Calculate_FT_Win1_Or_Draw(statInfoHolders, ftWin1DrawOdd70Percent, serial);
                    ftWin1DrawOdd75Percent = Calculate_FT_Win1_Or_Draw(statInfoHolders, ftWin1DrawOdd75Percent, serial);
                    ftWin1DrawOdd80Percent = Calculate_FT_Win1_Or_Draw(statInfoHolders, ftWin1DrawOdd80Percent, serial);
                    ftWin1DrawOdd85Percent = Calculate_FT_Win1_Or_Draw(statInfoHolders, ftWin1DrawOdd85Percent, serial);
                    ftWin1DrawOdd90Percent = Calculate_FT_Win1_Or_Draw(statInfoHolders, ftWin1DrawOdd90Percent, serial);


                    htWin1DrawOdd45Percent = Calculate_HT_Win1_Or_Draw(statInfoHolders, htWin1DrawOdd45Percent, serial);
                    htWin1DrawOdd50Percent = Calculate_HT_Win1_Or_Draw(statInfoHolders, htWin1DrawOdd50Percent, serial);
                    htWin1DrawOdd55Percent = Calculate_HT_Win1_Or_Draw(statInfoHolders, htWin1DrawOdd55Percent, serial);
                    htWin1DrawOdd60Percent = Calculate_HT_Win1_Or_Draw(statInfoHolders, htWin1DrawOdd60Percent, serial);
                    htWin1DrawOdd65Percent = Calculate_HT_Win1_Or_Draw(statInfoHolders, htWin1DrawOdd65Percent, serial);
                    htWin1DrawOdd70Percent = Calculate_HT_Win1_Or_Draw(statInfoHolders, htWin1DrawOdd70Percent, serial);
                    htWin1DrawOdd75Percent = Calculate_HT_Win1_Or_Draw(statInfoHolders, htWin1DrawOdd75Percent, serial);
                    htWin1DrawOdd80Percent = Calculate_HT_Win1_Or_Draw(statInfoHolders, htWin1DrawOdd80Percent, serial);
                    htWin1DrawOdd85Percent = Calculate_HT_Win1_Or_Draw(statInfoHolders, htWin1DrawOdd85Percent, serial);
                    htWin1DrawOdd90Percent = Calculate_HT_Win1_Or_Draw(statInfoHolders, htWin1DrawOdd90Percent, serial);


                    shWin1DrawOdd45Percent = Calculate_SH_Win1_Or_Draw(statInfoHolders, shWin1DrawOdd45Percent, serial);
                    shWin1DrawOdd50Percent = Calculate_SH_Win1_Or_Draw(statInfoHolders, shWin1DrawOdd50Percent, serial);
                    shWin1DrawOdd55Percent = Calculate_SH_Win1_Or_Draw(statInfoHolders, shWin1DrawOdd55Percent, serial);
                    shWin1DrawOdd60Percent = Calculate_SH_Win1_Or_Draw(statInfoHolders, shWin1DrawOdd60Percent, serial);
                    shWin1DrawOdd65Percent = Calculate_SH_Win1_Or_Draw(statInfoHolders, shWin1DrawOdd65Percent, serial);
                    shWin1DrawOdd70Percent = Calculate_SH_Win1_Or_Draw(statInfoHolders, shWin1DrawOdd70Percent, serial);
                    shWin1DrawOdd75Percent = Calculate_SH_Win1_Or_Draw(statInfoHolders, shWin1DrawOdd75Percent, serial);
                    shWin1DrawOdd80Percent = Calculate_SH_Win1_Or_Draw(statInfoHolders, shWin1DrawOdd80Percent, serial);
                    shWin1DrawOdd85Percent = Calculate_SH_Win1_Or_Draw(statInfoHolders, shWin1DrawOdd85Percent, serial);
                    shWin1DrawOdd90Percent = Calculate_SH_Win1_Or_Draw(statInfoHolders, shWin1DrawOdd90Percent, serial);




                    ftWin2DrawOdd45Percent = Calculate_FT_Draw_Or_Win2(statInfoHolders, ftWin2DrawOdd45Percent, serial);
                    ftWin2DrawOdd50Percent = Calculate_FT_Draw_Or_Win2(statInfoHolders, ftWin2DrawOdd50Percent, serial);
                    ftWin2DrawOdd55Percent = Calculate_FT_Draw_Or_Win2(statInfoHolders, ftWin2DrawOdd55Percent, serial);
                    ftWin2DrawOdd60Percent = Calculate_FT_Draw_Or_Win2(statInfoHolders, ftWin2DrawOdd60Percent, serial);
                    ftWin2DrawOdd65Percent = Calculate_FT_Draw_Or_Win2(statInfoHolders, ftWin2DrawOdd65Percent, serial);
                    ftWin2DrawOdd70Percent = Calculate_FT_Draw_Or_Win2(statInfoHolders, ftWin2DrawOdd70Percent, serial);
                    ftWin2DrawOdd75Percent = Calculate_FT_Draw_Or_Win2(statInfoHolders, ftWin2DrawOdd75Percent, serial);
                    ftWin2DrawOdd80Percent = Calculate_FT_Draw_Or_Win2(statInfoHolders, ftWin2DrawOdd80Percent, serial);
                    ftWin2DrawOdd85Percent = Calculate_FT_Draw_Or_Win2(statInfoHolders, ftWin2DrawOdd85Percent, serial);
                    ftWin2DrawOdd90Percent = Calculate_FT_Draw_Or_Win2(statInfoHolders, ftWin2DrawOdd90Percent, serial);


                    htWin2DrawOdd45Percent = Calculate_HT_Draw_Or_Win2(statInfoHolders, htWin2DrawOdd45Percent, serial);
                    htWin2DrawOdd50Percent = Calculate_HT_Draw_Or_Win2(statInfoHolders, htWin2DrawOdd50Percent, serial);
                    htWin2DrawOdd55Percent = Calculate_HT_Draw_Or_Win2(statInfoHolders, htWin2DrawOdd55Percent, serial);
                    htWin2DrawOdd60Percent = Calculate_HT_Draw_Or_Win2(statInfoHolders, htWin2DrawOdd60Percent, serial);
                    htWin2DrawOdd65Percent = Calculate_HT_Draw_Or_Win2(statInfoHolders, htWin2DrawOdd65Percent, serial);
                    htWin2DrawOdd70Percent = Calculate_HT_Draw_Or_Win2(statInfoHolders, htWin2DrawOdd70Percent, serial);
                    htWin2DrawOdd75Percent = Calculate_HT_Draw_Or_Win2(statInfoHolders, htWin2DrawOdd75Percent, serial);
                    htWin2DrawOdd80Percent = Calculate_HT_Draw_Or_Win2(statInfoHolders, htWin2DrawOdd80Percent, serial);
                    htWin2DrawOdd85Percent = Calculate_HT_Draw_Or_Win2(statInfoHolders, htWin2DrawOdd85Percent, serial);
                    htWin2DrawOdd90Percent = Calculate_HT_Draw_Or_Win2(statInfoHolders, htWin2DrawOdd90Percent, serial);


                    shWin2DrawOdd45Percent = Calculate_SH_Draw_Or_Win2(statInfoHolders, shWin2DrawOdd45Percent, serial);
                    shWin2DrawOdd50Percent = Calculate_SH_Draw_Or_Win2(statInfoHolders, shWin2DrawOdd50Percent, serial);
                    shWin2DrawOdd55Percent = Calculate_SH_Draw_Or_Win2(statInfoHolders, shWin2DrawOdd55Percent, serial);
                    shWin2DrawOdd60Percent = Calculate_SH_Draw_Or_Win2(statInfoHolders, shWin2DrawOdd60Percent, serial);
                    shWin2DrawOdd65Percent = Calculate_SH_Draw_Or_Win2(statInfoHolders, shWin2DrawOdd65Percent, serial);
                    shWin2DrawOdd70Percent = Calculate_SH_Draw_Or_Win2(statInfoHolders, shWin2DrawOdd70Percent, serial);
                    shWin2DrawOdd75Percent = Calculate_SH_Draw_Or_Win2(statInfoHolders, shWin2DrawOdd75Percent, serial);
                    shWin2DrawOdd80Percent = Calculate_SH_Draw_Or_Win2(statInfoHolders, shWin2DrawOdd80Percent, serial);
                    shWin2DrawOdd85Percent = Calculate_SH_Draw_Or_Win2(statInfoHolders, shWin2DrawOdd85Percent, serial);
                    shWin2DrawOdd90Percent = Calculate_SH_Draw_Or_Win2(statInfoHolders, shWin2DrawOdd90Percent, serial);



                    ftWin1Win2Odd45Percent = Calculate_FT_Win1_Or_Win2(statInfoHolders, ftWin1Win2Odd45Percent, serial);
                    ftWin1Win2Odd50Percent = Calculate_FT_Win1_Or_Win2(statInfoHolders, ftWin1Win2Odd50Percent, serial);
                    ftWin1Win2Odd55Percent = Calculate_FT_Win1_Or_Win2(statInfoHolders, ftWin1Win2Odd55Percent, serial);
                    ftWin1Win2Odd60Percent = Calculate_FT_Win1_Or_Win2(statInfoHolders, ftWin1Win2Odd60Percent, serial);
                    ftWin1Win2Odd65Percent = Calculate_FT_Win1_Or_Win2(statInfoHolders, ftWin1Win2Odd65Percent, serial);
                    ftWin1Win2Odd70Percent = Calculate_FT_Win1_Or_Win2(statInfoHolders, ftWin1Win2Odd70Percent, serial);
                    ftWin1Win2Odd75Percent = Calculate_FT_Win1_Or_Win2(statInfoHolders, ftWin1Win2Odd75Percent, serial);
                    ftWin1Win2Odd80Percent = Calculate_FT_Win1_Or_Win2(statInfoHolders, ftWin1Win2Odd80Percent, serial);
                    ftWin1Win2Odd85Percent = Calculate_FT_Win1_Or_Win2(statInfoHolders, ftWin1Win2Odd85Percent, serial);
                    ftWin1Win2Odd90Percent = Calculate_FT_Win1_Or_Win2(statInfoHolders, ftWin1Win2Odd90Percent, serial);


                    htWin1Win2Odd45Percent = Calculate_HT_Win1_Or_Win2(statInfoHolders, htWin1Win2Odd45Percent, serial);
                    htWin1Win2Odd50Percent = Calculate_HT_Win1_Or_Win2(statInfoHolders, htWin1Win2Odd50Percent, serial);
                    htWin1Win2Odd55Percent = Calculate_HT_Win1_Or_Win2(statInfoHolders, htWin1Win2Odd55Percent, serial);
                    htWin1Win2Odd60Percent = Calculate_HT_Win1_Or_Win2(statInfoHolders, htWin1Win2Odd60Percent, serial);
                    htWin1Win2Odd65Percent = Calculate_HT_Win1_Or_Win2(statInfoHolders, htWin1Win2Odd65Percent, serial);
                    htWin1Win2Odd70Percent = Calculate_HT_Win1_Or_Win2(statInfoHolders, htWin1Win2Odd70Percent, serial);
                    htWin1Win2Odd75Percent = Calculate_HT_Win1_Or_Win2(statInfoHolders, htWin1Win2Odd75Percent, serial);
                    htWin1Win2Odd80Percent = Calculate_HT_Win1_Or_Win2(statInfoHolders, htWin1Win2Odd80Percent, serial);
                    htWin1Win2Odd85Percent = Calculate_HT_Win1_Or_Win2(statInfoHolders, htWin1Win2Odd85Percent, serial);
                    htWin1Win2Odd90Percent = Calculate_HT_Win1_Or_Win2(statInfoHolders, htWin1Win2Odd90Percent, serial);


                    shWin1Win2Odd45Percent = Calculate_SH_Win1_Or_Win2(statInfoHolders, shWin1Win2Odd45Percent, serial);
                    shWin1Win2Odd50Percent = Calculate_SH_Win1_Or_Win2(statInfoHolders, shWin1Win2Odd50Percent, serial);
                    shWin1Win2Odd55Percent = Calculate_SH_Win1_Or_Win2(statInfoHolders, shWin1Win2Odd55Percent, serial);
                    shWin1Win2Odd60Percent = Calculate_SH_Win1_Or_Win2(statInfoHolders, shWin1Win2Odd60Percent, serial);
                    shWin1Win2Odd65Percent = Calculate_SH_Win1_Or_Win2(statInfoHolders, shWin1Win2Odd65Percent, serial);
                    shWin1Win2Odd70Percent = Calculate_SH_Win1_Or_Win2(statInfoHolders, shWin1Win2Odd70Percent, serial);
                    shWin1Win2Odd75Percent = Calculate_SH_Win1_Or_Win2(statInfoHolders, shWin1Win2Odd75Percent, serial);
                    shWin1Win2Odd80Percent = Calculate_SH_Win1_Or_Win2(statInfoHolders, shWin1Win2Odd80Percent, serial);
                    shWin1Win2Odd85Percent = Calculate_SH_Win1_Or_Win2(statInfoHolders, shWin1Win2Odd85Percent, serial);
                    shWin1Win2Odd90Percent = Calculate_SH_Win1_Or_Win2(statInfoHolders, shWin1Win2Odd90Percent, serial);
                };

                //if (statOverallInfoHolders != null && statOverallInfoHolders.Count > 0)
                //{
                //    ggFtOverall60Percent = Calculate_GG_Yes(statOverallInfoHolders, ggFtOverall60Percent, serial);
                //    ggFtOverall65Percent = Calculate_GG_Yes(statOverallInfoHolders, ggFtOverall65Percent, serial);
                //    ggFtOverall70Percent = Calculate_GG_Yes(statOverallInfoHolders, ggFtOverall70Percent, serial);
                //    ggFtOverall75Percent = Calculate_GG_Yes(statOverallInfoHolders, ggFtOverall75Percent, serial);
                //    ggFtOverall80Percent = Calculate_GG_Yes(statOverallInfoHolders, ggFtOverall80Percent, serial);

                //    ngFtOverall60Percent = Calculate_GG_NO(statOverallInfoHolders, ngFtOverall60Percent, serial);
                //    ngFtOverall65Percent = Calculate_GG_NO(statOverallInfoHolders, ngFtOverall65Percent, serial);
                //    ngFtOverall70Percent = Calculate_GG_NO(statOverallInfoHolders, ngFtOverall70Percent, serial);
                //    ngFtOverall75Percent = Calculate_GG_NO(statOverallInfoHolders, ngFtOverall75Percent, serial);
                //    ngFtOverall80Percent = Calculate_GG_NO(statOverallInfoHolders, ngFtOverall80Percent, serial);

                //    overFt15Overall75Percent = Calculate_15_Over(statOverallInfoHolders, overFt15Overall75Percent, serial);
                //    overFt15Overall80Percent = Calculate_15_Over(statOverallInfoHolders, overFt15Overall80Percent, serial);
                //    overFt15Overall85Percent = Calculate_15_Over(statOverallInfoHolders, overFt15Overall85Percent, serial);
                //    overFt15Overall90Percent = Calculate_15_Over(statOverallInfoHolders, overFt15Overall90Percent, serial);
                //    overFt15Overall95Percent = Calculate_15_Over(statOverallInfoHolders, overFt15Overall95Percent, serial);

                //    overFt25Overall60Percent = Calculate_25_Over(statOverallInfoHolders, overFt25Overall60Percent, serial);
                //    overFt25Overall65Percent = Calculate_25_Over(statOverallInfoHolders, overFt25Overall65Percent, serial);
                //    overFt25Overall70Percent = Calculate_25_Over(statOverallInfoHolders, overFt25Overall70Percent, serial);
                //    overFt25Overall75Percent = Calculate_25_Over(statOverallInfoHolders, overFt25Overall75Percent, serial);
                //    overFt25Overall80Percent = Calculate_25_Over(statOverallInfoHolders, overFt25Overall80Percent, serial);

                //    overFt35UnderOverall75Percent = Calculate_35_Under(statOverallInfoHolders, overFt35UnderOverall75Percent, serial);
                //    overFt35UnderOverall80Percent = Calculate_35_Under(statOverallInfoHolders, overFt35UnderOverall80Percent, serial);
                //    overFt35UnderOverall85Percent = Calculate_35_Under(statOverallInfoHolders, overFt35UnderOverall85Percent, serial);
                //    overFt35UnderOverall90Percent = Calculate_35_Under(statOverallInfoHolders, overFt35UnderOverall90Percent, serial);
                //    overFt35UnderOverall95Percent = Calculate_35_Under(statOverallInfoHolders, overFt35UnderOverall95Percent, serial);

                //    overHt05Overall75Percent = Calculate_HT_05_Over(statOverallInfoHolders, overHt05Overall75Percent, serial);
                //    overHt05Overall80Percent = Calculate_HT_05_Over(statOverallInfoHolders, overHt05Overall80Percent, serial);
                //    overHt05Overall85Percent = Calculate_HT_05_Over(statOverallInfoHolders, overHt05Overall85Percent, serial);
                //    overHt05Overall90Percent = Calculate_HT_05_Over(statOverallInfoHolders, overHt05Overall90Percent, serial);
                //    overHt05Overall95Percent = Calculate_HT_05_Over(statOverallInfoHolders, overHt05Overall95Percent, serial);

                //    //overHt15UnderOverall75Percent = Calculate_HT_15_Under(statOverallInfoHolders, overHt15UnderOverall75Percent, serial);
                //    //overHt15UnderOverall80Percent = Calculate_HT_15_Under(statOverallInfoHolders, overHt15UnderOverall80Percent, serial);
                //    //overHt15UnderOverall85Percent = Calculate_HT_15_Under(statOverallInfoHolders, overHt15UnderOverall85Percent, serial);
                //    //overHt15UnderOverall90Percent = Calculate_HT_15_Under(statOverallInfoHolders, overHt15UnderOverall90Percent, serial);
                //    //overHt15UnderOverall95Percent = Calculate_HT_15_Under(statOverallInfoHolders, overHt15UnderOverall95Percent, serial);

                //    overSh05Overall75Percent = Calculate_SH_05_Over(statOverallInfoHolders, overSh05Overall75Percent, serial);
                //    overSh05Overall80Percent = Calculate_SH_05_Over(statOverallInfoHolders, overSh05Overall80Percent, serial);
                //    overSh05Overall85Percent = Calculate_SH_05_Over(statOverallInfoHolders, overSh05Overall85Percent, serial);
                //    overSh05Overall90Percent = Calculate_SH_05_Over(statOverallInfoHolders, overSh05Overall90Percent, serial);
                //    overSh05Overall95Percent = Calculate_SH_05_Over(statOverallInfoHolders, overSh05Overall95Percent, serial);

                //    //overSh15UnderOverall75Percent = Calculate_SH_15_Under(statOverallInfoHolders, overSh15UnderOverall75Percent, serial);
                //    //overSh15UnderOverall80Percent = Calculate_SH_15_Under(statOverallInfoHolders, overSh15UnderOverall80Percent, serial);
                //    //overSh15UnderOverall85Percent = Calculate_SH_15_Under(statOverallInfoHolders, overSh15UnderOverall85Percent, serial);
                //    //overSh15UnderOverall90Percent = Calculate_SH_15_Under(statOverallInfoHolders, overSh15UnderOverall90Percent, serial);
                //    //overSh15UnderOverall95Percent = Calculate_SH_15_Under(statOverallInfoHolders, overSh15UnderOverall95Percent, serial);
                //};
            }

            var listReports = new List<CalculationReportingModel>
                {
                    ggFtOdd60Percent,
                    ggFtOdd65Percent,
                    ggFtOdd70Percent,
                    ggFtOdd75Percent,
                    ggFtOdd80Percent,
                    ngFtOdd60Percent,
                    ngFtOdd65Percent,
                    ngFtOdd70Percent,
                    ngFtOdd75Percent,
                    ngFtOdd80Percent,
                    overFt15Odd75Percent,
                    overFt15Odd80Percent,
                    overFt15Odd85Percent,
                    overFt15Odd90Percent,
                    overFt15Odd95Percent,
                    overFt25Odd60Percent,
                    overFt25Odd65Percent,
                    overFt25Odd70Percent,
                    overFt25Odd75Percent,
                    overFt25Odd80Percent,
                    overFt35UnderOdd75Percent,
                    overFt35UnderOdd80Percent,
                    overFt35UnderOdd85Percent,
                    overFt35UnderOdd90Percent,
                    overFt35UnderOdd95Percent,
                    overHt05Odd75Percent,
                    overHt05Odd80Percent,
                    overHt05Odd85Percent,
                    overHt05Odd90Percent,
                    overHt05Odd95Percent,
                    overHt15UnderOdd75Percent,
                    overHt15UnderOdd80Percent,
                    overHt15UnderOdd85Percent,
                    overHt15UnderOdd90Percent,
                    overHt15UnderOdd95Percent,
                    overSh05Odd75Percent,
                    overSh05Odd80Percent,
                    overSh05Odd85Percent,
                    overSh05Odd90Percent,
                    overSh05Odd95Percent,
                    overSh15UnderOdd75Percent,
                    overSh15UnderOdd80Percent,
                    overSh15UnderOdd85Percent,
                    overSh15UnderOdd90Percent,
                    overSh15UnderOdd95Percent,


                    overFtHome05Odd70Percent,
                    overFtHome05Odd75Percent,
                    overFtHome05Odd80Percent,
                    overFtHome05Odd85Percent,
                    overFtHome05Odd90Percent,
                    overFtHome05Odd95Percent,

                    overFtHome15Odd60Percent,
                    overFtHome15Odd65Percent,
                    overFtHome15Odd70Percent,
                    overFtHome15Odd75Percent,
                    overFtHome15Odd80Percent,
                    overFtHome15Odd85Percent,
                    overFtHome15Odd90Percent,
                    overFtHome15Odd95Percent,

                    overFtAway05Odd70Percent,
                    overFtAway05Odd75Percent,
                    overFtAway05Odd80Percent,
                    overFtAway05Odd85Percent,
                    overFtAway05Odd90Percent,
                    overFtAway05Odd95Percent,

                    overFtAway15Odd60Percent,
                    overFtAway15Odd65Percent,
                    overFtAway15Odd70Percent,
                    overFtAway15Odd75Percent,
                    overFtAway15Odd80Percent,
                    overFtAway15Odd85Percent,
                    overFtAway15Odd90Percent,
                    overFtAway15Odd95Percent,

                    overHtHome05Odd60Percent,
                    overHtHome05Odd65Percent,
                    overHtHome05Odd70Percent,
                    overHtHome05Odd75Percent,
                    overHtHome05Odd80Percent,
                    overHtHome05Odd85Percent,
                    overHtHome05Odd90Percent,
                                            
                                            
                    overHtAway05Odd60Percent,
                    overHtAway05Odd65Percent,
                    overHtAway05Odd70Percent,
                    overHtAway05Odd75Percent,
                    overHtAway05Odd80Percent,
                    overHtAway05Odd85Percent,
                    overHtAway05Odd90Percent,
                                            
                                            
                                            
                    overShHome05Odd60Percent,
                    overShHome05Odd65Percent,
                    overShHome05Odd70Percent,
                    overShHome05Odd75Percent,
                    overShHome05Odd80Percent,
                    overShHome05Odd85Percent,
                    overShHome05Odd90Percent,
                                            
                                            
                    overShAway05Odd60Percent,
                    overShAway05Odd65Percent,
                    overShAway05Odd70Percent,
                    overShAway05Odd75Percent,
                    overShAway05Odd80Percent,
                    overShAway05Odd85Percent,
                    overShAway05Odd90Percent,

                    homeFtWinOdd45Percent,
                    homeFtWinOdd50Percent,
                    homeFtWinOdd55Percent,
                    homeFtWinOdd60Percent,
                    homeFtWinOdd65Percent,
                    homeFtWinOdd70Percent,
                    homeFtWinOdd75Percent,
                    homeFtWinOdd80Percent,
                    homeFtWinOdd85Percent,
                    homeFtWinOdd90Percent,
                                         
                                         
                                         
                    homeHtWinOdd45Percent,
                    homeHtWinOdd50Percent,
                    homeHtWinOdd55Percent,
                    homeHtWinOdd60Percent,
                    homeHtWinOdd65Percent,
                    homeHtWinOdd70Percent,
                    homeHtWinOdd75Percent,
                    homeHtWinOdd80Percent,
                    homeHtWinOdd85Percent,
                    homeHtWinOdd90Percent,
                                         
                                         
                                         
                    homeShWinOdd45Percent,
                    homeShWinOdd50Percent,
                    homeShWinOdd55Percent,
                    homeShWinOdd60Percent,
                    homeShWinOdd65Percent,
                    homeShWinOdd70Percent,
                    homeShWinOdd75Percent,
                    homeShWinOdd80Percent,
                    homeShWinOdd85Percent,
                    homeShWinOdd90Percent,
                                         
                                         
                                         
                                         
                                         
                    awayFtWinOdd45Percent,
                    awayFtWinOdd50Percent,
                    awayFtWinOdd55Percent,
                    awayFtWinOdd60Percent,
                    awayFtWinOdd65Percent,
                    awayFtWinOdd70Percent,
                    awayFtWinOdd75Percent,
                    awayFtWinOdd80Percent,
                    awayFtWinOdd85Percent,
                    awayFtWinOdd90Percent,
                                         
                                         
                                         
                    awayHtWinOdd45Percent,
                    awayHtWinOdd50Percent,
                    awayHtWinOdd55Percent,
                    awayHtWinOdd60Percent,
                    awayHtWinOdd65Percent,
                    awayHtWinOdd70Percent,
                    awayHtWinOdd75Percent,
                    awayHtWinOdd80Percent,
                    awayHtWinOdd85Percent,
                    awayHtWinOdd90Percent,
                                         
                                         
                                         
                    awayShWinOdd45Percent,
                    awayShWinOdd50Percent,
                    awayShWinOdd55Percent,
                    awayShWinOdd60Percent,
                    awayShWinOdd65Percent,
                    awayShWinOdd70Percent,
                    awayShWinOdd75Percent,
                    awayShWinOdd80Percent,
                    awayShWinOdd85Percent,
                    awayShWinOdd90Percent,

                    FtDrawOdd45Percent,
                    FtDrawOdd50Percent,
                    FtDrawOdd55Percent,
                    FtDrawOdd60Percent,
                    FtDrawOdd65Percent,
                    FtDrawOdd70Percent,
                    FtDrawOdd75Percent,
                    FtDrawOdd80Percent,
                    FtDrawOdd85Percent,
                    FtDrawOdd90Percent,
                                      
                                      
                                      
                    HtDrawOdd45Percent,
                    HtDrawOdd50Percent,
                    HtDrawOdd55Percent,
                    HtDrawOdd60Percent,
                    HtDrawOdd65Percent,
                    HtDrawOdd70Percent,
                    HtDrawOdd75Percent,
                    HtDrawOdd80Percent,
                    HtDrawOdd85Percent,
                    HtDrawOdd90Percent,
                                      
                                     
                                      
                    ShDrawOdd45Percent,
                    ShDrawOdd50Percent,
                    ShDrawOdd55Percent,
                    ShDrawOdd60Percent,
                    ShDrawOdd65Percent,
                    ShDrawOdd70Percent,
                    ShDrawOdd75Percent,
                    ShDrawOdd80Percent,
                    ShDrawOdd85Percent,
                    ShDrawOdd90Percent,


                    ftWin1DrawOdd45Percent,
                    ftWin1DrawOdd50Percent,
                    ftWin1DrawOdd55Percent,
                    ftWin1DrawOdd60Percent,
                    ftWin1DrawOdd65Percent,
                    ftWin1DrawOdd70Percent,
                    ftWin1DrawOdd75Percent,
                    ftWin1DrawOdd80Percent,
                    ftWin1DrawOdd85Percent,
                    ftWin1DrawOdd90Percent,
                                          
                                          
                                          
                    htWin1DrawOdd45Percent,
                    htWin1DrawOdd50Percent,
                    htWin1DrawOdd55Percent,
                    htWin1DrawOdd60Percent,
                    htWin1DrawOdd65Percent,
                    htWin1DrawOdd70Percent,
                    htWin1DrawOdd75Percent,
                    htWin1DrawOdd80Percent,
                    htWin1DrawOdd85Percent,
                    htWin1DrawOdd90Percent,
                                          
                                          
                                          
                    shWin1DrawOdd45Percent,
                    shWin1DrawOdd50Percent,
                    shWin1DrawOdd55Percent,
                    shWin1DrawOdd60Percent,
                    shWin1DrawOdd65Percent,
                    shWin1DrawOdd70Percent,
                    shWin1DrawOdd75Percent,
                    shWin1DrawOdd80Percent,
                    shWin1DrawOdd85Percent,
                    shWin1DrawOdd90Percent,
                                          
                                          
                                          
                                          
                                          
                    ftWin2DrawOdd45Percent,
                    ftWin2DrawOdd50Percent,
                    ftWin2DrawOdd55Percent,
                    ftWin2DrawOdd60Percent,
                    ftWin2DrawOdd65Percent,
                    ftWin2DrawOdd70Percent,
                    ftWin2DrawOdd75Percent,
                    ftWin2DrawOdd80Percent,
                    ftWin2DrawOdd85Percent,
                    ftWin2DrawOdd90Percent,
                                          
                                          
                                          
                    htWin2DrawOdd45Percent,
                    htWin2DrawOdd50Percent,
                    htWin2DrawOdd55Percent,
                    htWin2DrawOdd60Percent,
                    htWin2DrawOdd65Percent,
                    htWin2DrawOdd70Percent,
                    htWin2DrawOdd75Percent,
                    htWin2DrawOdd80Percent,
                    htWin2DrawOdd85Percent,
                    htWin2DrawOdd90Percent,
                                          
                                          
                                          
                    shWin2DrawOdd45Percent,
                    shWin2DrawOdd50Percent,
                    shWin2DrawOdd55Percent,
                    shWin2DrawOdd60Percent,
                    shWin2DrawOdd65Percent,
                    shWin2DrawOdd70Percent,
                    shWin2DrawOdd75Percent,
                    shWin2DrawOdd80Percent,
                    shWin2DrawOdd85Percent,
                    shWin2DrawOdd90Percent,
                                          
                                          
                                          
                                          
                    ftWin1Win2Odd45Percent,
                    ftWin1Win2Odd50Percent,
                    ftWin1Win2Odd55Percent,
                    ftWin1Win2Odd60Percent,
                    ftWin1Win2Odd65Percent,
                    ftWin1Win2Odd70Percent,
                    ftWin1Win2Odd75Percent,
                    ftWin1Win2Odd80Percent,
                    ftWin1Win2Odd85Percent,
                    ftWin1Win2Odd90Percent,
                                          
                                          
                                          
                    htWin1Win2Odd45Percent,
                    htWin1Win2Odd50Percent,
                    htWin1Win2Odd55Percent,
                    htWin1Win2Odd60Percent,
                    htWin1Win2Odd65Percent,
                    htWin1Win2Odd70Percent,
                    htWin1Win2Odd75Percent,
                    htWin1Win2Odd80Percent,
                    htWin1Win2Odd85Percent,
                    htWin1Win2Odd90Percent,
                                          
                                          
                                          
                    shWin1Win2Odd45Percent,
                    shWin1Win2Odd50Percent,
                    shWin1Win2Odd55Percent,
                    shWin1Win2Odd60Percent,
                    shWin1Win2Odd65Percent,
                    shWin1Win2Odd70Percent,
                    shWin1Win2Odd75Percent,
                    shWin1Win2Odd80Percent,
                    shWin1Win2Odd85Percent,
                    shWin1Win2Odd90Percent


                    //ggFtOverall60Percent,
                    //ggFtOverall65Percent,
                    //ggFtOverall70Percent,
                    //ggFtOverall75Percent,
                    //ggFtOverall80Percent,
                    //ngFtOverall60Percent,
                    //ngFtOverall65Percent,
                    //ngFtOverall70Percent,
                    //ngFtOverall75Percent,
                    //ngFtOverall80Percent,
                    //overFt15Overall75Percent,
                    //overFt15Overall80Percent,
                    //overFt15Overall85Percent,
                    //overFt15Overall90Percent,
                    //overFt15Overall95Percent,
                    //overFt25Overall60Percent,
                    //overFt25Overall65Percent,
                    //overFt25Overall70Percent,
                    //overFt25Overall75Percent,
                    //overFt25Overall80Percent,
                    //overFt35UnderOverall75Percent,
                    //overFt35UnderOverall80Percent,
                    //overFt35UnderOverall85Percent,
                    //overFt35UnderOverall90Percent,
                    //overFt35UnderOverall95Percent,
                    //overHt05Overall75Percent,
                    //overHt05Overall80Percent,
                    //overHt05Overall85Percent,
                    //overHt05Overall90Percent,
                    //overHt05Overall95Percent,
                    ////overHt15UnderOverall75Percent,
                    ////overHt15UnderOverall80Percent,
                    ////overHt15UnderOverall85Percent,
                    ////overHt15UnderOverall90Percent,
                    ////overHt15UnderOverall95Percent,
                    //overSh05Overall75Percent,
                    //overSh05Overall80Percent,
                    //overSh05Overall85Percent,
                    //overSh05Overall90Percent,
                    //overSh05Overall95Percent,
                    ////overSh15UnderOverall75Percent,
                    ////overSh15UnderOverall80Percent,
                    ////overSh15UnderOverall85Percent,
                    ////overSh15UnderOverall90Percent,
                    ////overSh15UnderOverall95Percent
                };

            using (var str = new StreamWriter(_jsonPathFormat.GetJsonFileByFormat("reportPossibility")))
            {
                var serializedData = JsonConvert.SerializeObject(listReports, Formatting.Indented);
                str.Write(serializedData);
            }

            return Ok(listReports);
        }


        private CalculationReportingModel Calculate_GG_Yes(List<StatisticInfoHolder> statInfoHolders, CalculationReportingModel reference, int serial)
        {
            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "FT_GG");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.HomePercent) >= reference.MinimumCount && Convert.ToInt32(neededStat.HomePercent * 100) >= reference.MinimumPercent)
            {
                var filterResult = _filterResultService.Get(x => x.SerialUniqueID == serial).Data;

                if (filterResult == null) return reference;

                if (filterResult.FT_GG)
                {
                    reference.CorrectCount++;
                }

                reference.FoundCount++;
            }

            return reference;
        }

        private CalculationReportingModel Calculate_GG_NO(List<StatisticInfoHolder> statInfoHolders, CalculationReportingModel reference, int serial)
        {
            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "FT_GG");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.HomePercent) >= reference.MinimumCount && Convert.ToInt32(neededStat.HomePercent * 100) <= reference.MinimumPercent)
            {
                var filterResult = _filterResultService.Get(x => x.SerialUniqueID == serial).Data;

                if (filterResult == null) return reference;

                if (!filterResult.FT_GG)
                {
                    reference.CorrectCount++;
                }

                reference.FoundCount++;
            }

            return reference;
        }

        private CalculationReportingModel Calculate_25_Over(List<StatisticInfoHolder> statInfoHolders, CalculationReportingModel reference, int serial)
        {
            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "FT_25");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.HomePercent) >= reference.MinimumCount && Convert.ToInt32(neededStat.HomePercent * 100) >= reference.MinimumPercent)
            {
                var filterResult = _filterResultService.Get(x => x.SerialUniqueID == serial).Data;

                if (filterResult == null) return reference;

                if (filterResult.FT_2_5_Over)
                {
                    reference.CorrectCount++;
                }

                reference.FoundCount++;
            }

            return reference;
        }

        private CalculationReportingModel Calculate_35_Under(List<StatisticInfoHolder> statInfoHolders, CalculationReportingModel reference, int serial)
        {
            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "FT_25");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.HomePercent) >= reference.MinimumCount && Convert.ToInt32(neededStat.HomePercent * 100) <= reference.MinimumPercent)
            {
                var filterResult = _filterResultService.Get(x => x.SerialUniqueID == serial).Data;

                if (filterResult == null) return reference;

                if (!filterResult.FT_3_5_Over)
                {
                    reference.CorrectCount++;
                }

                reference.FoundCount++;
            }

            return reference;
        }

        private CalculationReportingModel Calculate_15_Over(List<StatisticInfoHolder> statInfoHolders, CalculationReportingModel reference, int serial)
        {
            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "FT_15");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.HomePercent) >= reference.MinimumCount && Convert.ToInt32(neededStat.HomePercent * 100) >= reference.MinimumPercent)
            {
                var filterResult = _filterResultService.Get(x => x.SerialUniqueID == serial).Data;

                if (filterResult == null) return reference;

                if (filterResult.FT_1_5_Over)
                {
                    reference.CorrectCount++;
                }

                reference.FoundCount++;
            }

            return reference;
        }

        private CalculationReportingModel Calculate_HT_05_Over(List<StatisticInfoHolder> statInfoHolders, CalculationReportingModel reference, int serial)
        {
            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "HT_05");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.HomePercent) >= reference.MinimumCount && Convert.ToInt32(neededStat.HomePercent * 100) >= reference.MinimumPercent)
            {
                var filterResult = _filterResultService.Get(x => x.SerialUniqueID == serial).Data;

                if (filterResult == null) return reference;

                if (filterResult.HT_0_5_Over)
                {
                    reference.CorrectCount++;
                }

                reference.FoundCount++;
            }

            return reference;
        }

        private CalculationReportingModel Calculate_Home_FT_05_Over(List<StatisticInfoHolder> statInfoHolders, CalculationReportingModel reference, int serial)
        {
            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "Ind_FT_05");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.HomePercent) >= reference.MinimumCount && Convert.ToInt32(neededStat.HomePercent * 100) >= reference.MinimumPercent)
            {
                var filterResult = _filterResultService.Get(x => x.SerialUniqueID == serial).Data;

                if (filterResult == null) return reference;

                if (filterResult.Home_FT_0_5_Over)
                {
                    reference.CorrectCount++;
                }

                reference.FoundCount++;
            }

            return reference;
        }

        private CalculationReportingModel Calculate_Home_FT_15_Over(List<StatisticInfoHolder> statInfoHolders, CalculationReportingModel reference, int serial)
        {
            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "Ind_FT_15");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.HomePercent) >= reference.MinimumCount && Convert.ToInt32(neededStat.HomePercent * 100) >= reference.MinimumPercent)
            {
                var filterResult = _filterResultService.Get(x => x.SerialUniqueID == serial).Data;

                if (filterResult == null) return reference;

                if (filterResult.Home_FT_1_5_Over)
                {
                    reference.CorrectCount++;
                }

                reference.FoundCount++;
            }

            return reference;
        }

        private CalculationReportingModel Calculate_Home_HT_05_Over(List<StatisticInfoHolder> statInfoHolders, CalculationReportingModel reference, int serial)
        {
            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "Ind_HT_05");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.HomePercent) >= reference.MinimumCount && Convert.ToInt32(neededStat.HomePercent * 100) >= reference.MinimumPercent)
            {
                var filterResult = _filterResultService.Get(x => x.SerialUniqueID == serial).Data;

                if (filterResult == null) return reference;

                if (filterResult.Home_HT_0_5_Over)
                {
                    reference.CorrectCount++;
                }

                reference.FoundCount++;
            }

            return reference;
        }

        private CalculationReportingModel Calculate_Home_SH_05_Over(List<StatisticInfoHolder> statInfoHolders, CalculationReportingModel reference, int serial)
        {
            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "Ind_SH_05");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.HomePercent) >= reference.MinimumCount && Convert.ToInt32(neededStat.HomePercent * 100) >= reference.MinimumPercent)
            {
                var filterResult = _filterResultService.Get(x => x.SerialUniqueID == serial).Data;

                if (filterResult == null) return reference;

                if (filterResult.Home_SH_0_5_Over)
                {
                    reference.CorrectCount++;
                }

                reference.FoundCount++;
            }

            return reference;
        }


        private CalculationReportingModel Calculate_Away_FT_05_Over(List<StatisticInfoHolder> statInfoHolders, CalculationReportingModel reference, int serial)
        {
            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "Ind_FT_05");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.AwayPercent) >= reference.MinimumCount && Convert.ToInt32(neededStat.AwayPercent * 100) >= reference.MinimumPercent)
            {
                var filterResult = _filterResultService.Get(x => x.SerialUniqueID == serial).Data;

                if (filterResult == null) return reference;

                if (filterResult.Away_FT_0_5_Over)
                {
                    reference.CorrectCount++;
                }

                reference.FoundCount++;
            }

            return reference;
        }

        private CalculationReportingModel Calculate_Away_FT_15_Over(List<StatisticInfoHolder> statInfoHolders, CalculationReportingModel reference, int serial)
        {
            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "Ind_FT_15");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.AwayPercent) >= reference.MinimumCount && Convert.ToInt32(neededStat.AwayPercent * 100) >= reference.MinimumPercent)
            {
                var filterResult = _filterResultService.Get(x => x.SerialUniqueID == serial).Data;

                if (filterResult == null) return reference;

                if (filterResult.Away_FT_1_5_Over)
                {
                    reference.CorrectCount++;
                }

                reference.FoundCount++;
            }

            return reference;
        }

        private CalculationReportingModel Calculate_Away_HT_05_Over(List<StatisticInfoHolder> statInfoHolders, CalculationReportingModel reference, int serial)
        {
            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "Ind_HT_05");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.AwayPercent) >= reference.MinimumCount && Convert.ToInt32(neededStat.AwayPercent * 100) >= reference.MinimumPercent)
            {
                var filterResult = _filterResultService.Get(x => x.SerialUniqueID == serial).Data;

                if (filterResult == null) return reference;

                if (filterResult.Away_HT_0_5_Over)
                {
                    reference.CorrectCount++;
                }

                reference.FoundCount++;
            }

            return reference;
        }


        private CalculationReportingModel Calculate_Away_SH_05_Over(List<StatisticInfoHolder> statInfoHolders, CalculationReportingModel reference, int serial)
        {
            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "Ind_SH_05");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.AwayPercent) >= reference.MinimumCount && Convert.ToInt32(neededStat.AwayPercent * 100) >= reference.MinimumPercent)
            {
                var filterResult = _filterResultService.Get(x => x.SerialUniqueID == serial).Data;

                if (filterResult == null) return reference;

                if (filterResult.Away_SH_0_5_Over)
                {
                    reference.CorrectCount++;
                }

                reference.FoundCount++;
            }

            return reference;
        }


        private CalculationReportingModel Calculate_Home_FT_Win(List<StatisticInfoHolder> statInfoHolders, CalculationReportingModel reference, int serial)
        {
            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "Ind_FT_Win");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.HomePercent) >= reference.MinimumCount && Convert.ToInt32(neededStat.HomePercent * 100) >= reference.MinimumPercent)
            {
                var filterResult = _filterResultService.Get(x => x.SerialUniqueID == serial).Data;

                if (filterResult == null) return reference;

                if (filterResult.Is_FT_Win1)
                {
                    reference.CorrectCount++;
                }

                reference.FoundCount++;
            }

            return reference;
        }

        private CalculationReportingModel Calculate_Home_HT_Win(List<StatisticInfoHolder> statInfoHolders, CalculationReportingModel reference, int serial)
        {
            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "Ind_HT_Win");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.HomePercent) >= reference.MinimumCount && Convert.ToInt32(neededStat.HomePercent * 100) >= reference.MinimumPercent)
            {
                var filterResult = _filterResultService.Get(x => x.SerialUniqueID == serial).Data;

                if (filterResult == null) return reference;

                if (filterResult.Is_HT_Win1)
                {
                    reference.CorrectCount++;
                }

                reference.FoundCount++;
            }

            return reference;
        }

        private CalculationReportingModel Calculate_Home_SH_Win(List<StatisticInfoHolder> statInfoHolders, CalculationReportingModel reference, int serial)
        {
            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "Ind_SH_Win");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.HomePercent) >= reference.MinimumCount && Convert.ToInt32(neededStat.HomePercent * 100) >= reference.MinimumPercent)
            {
                var filterResult = _filterResultService.Get(x => x.SerialUniqueID == serial).Data;

                if (filterResult == null) return reference;

                if (filterResult.Is_SH_Win1)
                {
                    reference.CorrectCount++;
                }

                reference.FoundCount++;
            }

            return reference;
        }




        private CalculationReportingModel Calculate_Away_FT_Win(List<StatisticInfoHolder> statInfoHolders, CalculationReportingModel reference, int serial)
        {
            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "Ind_FT_Win");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.AwayPercent) >= reference.MinimumCount && Convert.ToInt32(neededStat.AwayPercent * 100) >= reference.MinimumPercent)
            {
                var filterResult = _filterResultService.Get(x => x.SerialUniqueID == serial).Data;

                if (filterResult == null) return reference;

                if (filterResult.Is_FT_Win2)
                {
                    reference.CorrectCount++;
                }

                reference.FoundCount++;
            }

            return reference;
        }

        private CalculationReportingModel Calculate_Away_HT_Win(List<StatisticInfoHolder> statInfoHolders, CalculationReportingModel reference, int serial)
        {
            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "Ind_HT_Win");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.AwayPercent) >= reference.MinimumCount && Convert.ToInt32(neededStat.AwayPercent * 100) >= reference.MinimumPercent)
            {
                var filterResult = _filterResultService.Get(x => x.SerialUniqueID == serial).Data;

                if (filterResult == null) return reference;

                if (filterResult.Is_HT_Win2)
                {
                    reference.CorrectCount++;
                }

                reference.FoundCount++;
            }

            return reference;
        }

        private CalculationReportingModel Calculate_Away_SH_Win(List<StatisticInfoHolder> statInfoHolders, CalculationReportingModel reference, int serial)
        {
            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "Ind_SH_Win");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.AwayPercent) >= reference.MinimumCount && Convert.ToInt32(neededStat.AwayPercent * 100) >= reference.MinimumPercent)
            {
                var filterResult = _filterResultService.Get(x => x.SerialUniqueID == serial).Data;

                if (filterResult == null) return reference;

                if (filterResult.Is_SH_Win2)
                {
                    reference.CorrectCount++;
                }

                reference.FoundCount++;
            }

            return reference;
        }


        private CalculationReportingModel Calculate_FT_Draw(List<StatisticInfoHolder> statInfoHolders, CalculationReportingModel reference, int serial)
        {
            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "FT_X");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.HomePercent) >= reference.MinimumCount && Convert.ToInt32(neededStat.HomePercent * 100) >= reference.MinimumPercent)
            {
                var filterResult = _filterResultService.Get(x => x.SerialUniqueID == serial).Data;

                if (filterResult == null) return reference;

                if (filterResult.Is_FT_X)
                {
                    reference.CorrectCount++;
                }

                reference.FoundCount++;
            }

            return reference;
        }

        private CalculationReportingModel Calculate_HT_Draw(List<StatisticInfoHolder> statInfoHolders, CalculationReportingModel reference, int serial)
        {
            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "HT_X");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.HomePercent) >= reference.MinimumCount && Convert.ToInt32(neededStat.HomePercent * 100) >= reference.MinimumPercent)
            {
                var filterResult = _filterResultService.Get(x => x.SerialUniqueID == serial).Data;

                if (filterResult == null) return reference;

                if (filterResult.Is_HT_X)
                {
                    reference.CorrectCount++;
                }

                reference.FoundCount++;
            }

            return reference;
        }

        private CalculationReportingModel Calculate_SH_Draw(List<StatisticInfoHolder> statInfoHolders, CalculationReportingModel reference, int serial)
        {
            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "SH_X");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.HomePercent) >= reference.MinimumCount && Convert.ToInt32(neededStat.HomePercent * 100) >= reference.MinimumPercent)
            {
                var filterResult = _filterResultService.Get(x => x.SerialUniqueID == serial).Data;

                if (filterResult == null) return reference;

                if (filterResult.Is_SH_X)
                {
                    reference.CorrectCount++;
                }

                reference.FoundCount++;
            }

            return reference;
        }



        private CalculationReportingModel Calculate_SH_05_Over(List<StatisticInfoHolder> statInfoHolders, CalculationReportingModel reference, int serial)
        {
            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "SH_05");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.HomePercent) >= reference.MinimumCount && Convert.ToInt32(neededStat.HomePercent * 100) >= reference.MinimumPercent)
            {
                var filterResult = _filterResultService.Get(x => x.SerialUniqueID == serial).Data;

                if (filterResult == null) return reference;

                if (filterResult.SH_0_5_Over)
                {
                    reference.CorrectCount++;
                }

                reference.FoundCount++;
            }

            return reference;
        }

        private CalculationReportingModel Calculate_HT_15_Under(List<StatisticInfoHolder> statInfoHolders, CalculationReportingModel reference, int serial)
        {
            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "HT_15");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.HomePercent) >= reference.MinimumCount && Convert.ToInt32(neededStat.HomePercent * 100) <= reference.MinimumPercent)
            {
                var filterResult = _filterResultService.Get(x => x.SerialUniqueID == serial).Data;

                if (filterResult == null) return reference;

                if (!filterResult.HT_1_5_Over)
                {
                    reference.CorrectCount++;
                }

                reference.FoundCount++;
            }

            return reference;
        }

        private CalculationReportingModel Calculate_SH_15_Under(List<StatisticInfoHolder> statInfoHolders, CalculationReportingModel reference, int serial)
        {
            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "SH_15");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.HomePercent) >= reference.MinimumCount && Convert.ToInt32(neededStat.HomePercent * 100) <= reference.MinimumPercent)
            {
                var filterResult = _filterResultService.Get(x => x.SerialUniqueID == serial).Data;

                if (filterResult == null) return reference;

                if (!filterResult.SH_1_5_Over)
                {
                    reference.CorrectCount++;
                }

                reference.FoundCount++;
            }

            return reference;
        }




        private CalculationReportingModel Calculate_FT_Win1_Or_Draw(List<StatisticInfoHolder> statInfoHolders, CalculationReportingModel reference, int serial)
        {
            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "Ind_FT_Win");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.HomePercent) >= reference.MinimumCount && Convert.ToInt32(100 - (neededStat.AwayPercent * 100)) >= reference.MinimumPercent)
            {
                var filterResult = _filterResultService.Get(x => x.SerialUniqueID == serial).Data;

                if (filterResult == null) return reference;

                if (!filterResult.Is_FT_Win2)
                {
                    reference.CorrectCount++;
                }

                reference.FoundCount++;
            }

            return reference;
        }

        private CalculationReportingModel Calculate_HT_Win1_Or_Draw(List<StatisticInfoHolder> statInfoHolders, CalculationReportingModel reference, int serial)
        {
            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "Ind_HT_Win");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.HomePercent) >= reference.MinimumCount && Convert.ToInt32(100 - (neededStat.AwayPercent * 100)) >= reference.MinimumPercent)
            {
                var filterResult = _filterResultService.Get(x => x.SerialUniqueID == serial).Data;

                if (filterResult == null) return reference;

                if (!filterResult.Is_HT_Win2)
                {
                    reference.CorrectCount++;
                }

                reference.FoundCount++;
            }

            return reference;
        }

        private CalculationReportingModel Calculate_SH_Win1_Or_Draw(List<StatisticInfoHolder> statInfoHolders, CalculationReportingModel reference, int serial)
        {
            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "Ind_SH_Win");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.HomePercent) >= reference.MinimumCount && Convert.ToInt32(100 - (neededStat.AwayPercent * 100)) >= reference.MinimumPercent)
            {
                var filterResult = _filterResultService.Get(x => x.SerialUniqueID == serial).Data;

                if (filterResult == null) return reference;

                if (!filterResult.Is_SH_Win2)
                {
                    reference.CorrectCount++;
                }

                reference.FoundCount++;
            }

            return reference;
        }






        private CalculationReportingModel Calculate_FT_Draw_Or_Win2(List<StatisticInfoHolder> statInfoHolders, CalculationReportingModel reference, int serial)
        {
            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "Ind_FT_Win");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.HomePercent) >= reference.MinimumCount && Convert.ToInt32(100 - (neededStat.HomePercent * 100)) >= reference.MinimumPercent)
            {
                var filterResult = _filterResultService.Get(x => x.SerialUniqueID == serial).Data;

                if (filterResult == null) return reference;

                if (!filterResult.Is_FT_Win1)
                {
                    reference.CorrectCount++;
                }

                reference.FoundCount++;
            }

            return reference;
        }

        private CalculationReportingModel Calculate_HT_Draw_Or_Win2(List<StatisticInfoHolder> statInfoHolders, CalculationReportingModel reference, int serial)
        {
            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "Ind_HT_Win");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.HomePercent) >= reference.MinimumCount && Convert.ToInt32(100 - (neededStat.HomePercent * 100)) >= reference.MinimumPercent)
            {
                var filterResult = _filterResultService.Get(x => x.SerialUniqueID == serial).Data;

                if (filterResult == null) return reference;

                if (!filterResult.Is_HT_Win1)
                {
                    reference.CorrectCount++;
                }

                reference.FoundCount++;
            }

            return reference;
        }

        private CalculationReportingModel Calculate_SH_Draw_Or_Win2(List<StatisticInfoHolder> statInfoHolders, CalculationReportingModel reference, int serial)
        {
            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "Ind_SH_Win");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.HomePercent) >= reference.MinimumCount && Convert.ToInt32(100 - (neededStat.HomePercent * 100)) >= reference.MinimumPercent)
            {
                var filterResult = _filterResultService.Get(x => x.SerialUniqueID == serial).Data;

                if (filterResult == null) return reference;

                if (!filterResult.Is_SH_Win1)
                {
                    reference.CorrectCount++;
                }

                reference.FoundCount++;
            }

            return reference;
        }






        private CalculationReportingModel Calculate_FT_Win1_Or_Win2(List<StatisticInfoHolder> statInfoHolders, CalculationReportingModel reference, int serial)
        {
            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "FT_X");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.HomePercent) >= reference.MinimumCount && Convert.ToInt32(100 - (neededStat.HomePercent * 100)) >= reference.MinimumPercent)
            {
                var filterResult = _filterResultService.Get(x => x.SerialUniqueID == serial).Data;

                if (filterResult == null) return reference;

                if (!filterResult.Is_FT_X)
                {
                    reference.CorrectCount++;
                }

                reference.FoundCount++;
            }

            return reference;
        }

        private CalculationReportingModel Calculate_HT_Win1_Or_Win2(List<StatisticInfoHolder> statInfoHolders, CalculationReportingModel reference, int serial)
        {
            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "HT_X");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.HomePercent) >= reference.MinimumCount && Convert.ToInt32(100 - (neededStat.HomePercent * 100)) >= reference.MinimumPercent)
            {
                var filterResult = _filterResultService.Get(x => x.SerialUniqueID == serial).Data;

                if (filterResult == null) return reference;

                if (!filterResult.Is_HT_X)
                {
                    reference.CorrectCount++;
                }

                reference.FoundCount++;
            }

            return reference;
        }

        private CalculationReportingModel Calculate_SH_Win1_Or_Win2(List<StatisticInfoHolder> statInfoHolders, CalculationReportingModel reference, int serial)
        {
            var neededStat = statInfoHolders.FirstOrDefault(x => x.Title == "SH_X");
            var foundCountStat = statInfoHolders.FirstOrDefault(x => x.Title == "Count_Percent_Found");

            if (Convert.ToInt32(foundCountStat.HomePercent) >= reference.MinimumCount && Convert.ToInt32(100 - (neededStat.HomePercent * 100)) >= reference.MinimumPercent)
            {
                var filterResult = _filterResultService.Get(x => x.SerialUniqueID == serial).Data;

                if (filterResult == null) return reference;

                if (!filterResult.Is_SH_X)
                {
                    reference.CorrectCount++;
                }

                reference.FoundCount++;
            }

            return reference;
        }
    }
}
