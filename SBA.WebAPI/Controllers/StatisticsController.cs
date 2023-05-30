using Core.Entities.Concrete.ComplexModels.ML;
using Core.Entities.Dtos.ComplexDataes.UIData;
using Core.Extensions;
using Core.Resources.Enums;
using Core.Utilities.Helpers.Serialization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SBA.Business.Abstract;
using SBA.Business.BusinessHelper;
using SBA.Business.ExternalServices.Abstract;
using SBA.Business.ExternalServices.ChatGPT;
using SBA.WebAPI.Utilities.Extensions;
using SBA.WebAPI.Utilities.Helpers;

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
        private readonly IFilterResultService _filterResultService;
        private readonly IForecastService _forecastService;
        private readonly IConfiguration _configuration;
        private readonly IExtLogService _logService;
        private readonly ChatGPTService _aiService;
        private readonly FileFormatBinder _formatBinder;

        public StatisticsController(IComparisonStatisticsHolderService comparisonStatisticsHolderService,
                                    ITeamPerformanceStatisticsHolderService teamPerformanceStatisticsHolderService,
                                    IAverageStatisticsHolderService averageStatisticsHolderService,
                                    ILeagueStatisticsHolderService leagueStatisticsHolderService,
                                    ITranslationService translationService,
                                    IConfiguration configuration,
                                    IStatisticInfoHolderService statisticInfoHolderService,
                                    IAiDataHolderService aiDataHolderService,
                                    IMatchBetService matchBetService,
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
            _aiService = new ChatGPTService(apiKey);
            _formatBinder = new FileFormatBinder();
            _statisticInfoHolderService = statisticInfoHolderService;
            _aiDataHolderService = aiDataHolderService;
            _matchBetService = matchBetService;
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
            var aiModel = await _aiDataHolderService.GetAsync(x=>x.Serial == serial);

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

            var resultGuess = await _aiService.CallOpenAIAsync(statisticsData, statisticsModel.MatchInformation.HomeTeam, statisticsModel.MatchInformation.AwayTeam);

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
            var model = _matchBetService.GetOddFilteredResult(serial, range).OrderBy(x=>x.Order).ToList();

            return Ok(model);
        }


        [HttpGet("get-forecast-productivity/{day}/{month}/{year}")]
        public async Task<IActionResult> GetForecastProductivity(int day, int month, int year)
        {
            ForecastDataContainer model = null;
            var filterDate = new DateTime(year, month, day);
            if (filterDate.Date == DateTime.UtcNow.ToAzeDate())
                model = await _forecastService.SelectForecastContainerInfoAsync(true);
            else
                model = await _forecastService.SelectForecastContainerInfoAsync(true, x=>x.MatchDateTime.Date == filterDate.Date);

            return Ok(model);
        }


        [HttpGet("get-forecast-by-serial/{serial}")]
        public async Task<IActionResult> GetForecastBySerialAsync(int serial)
        {
            var model = await _forecastService.SelectForecastsBySerialAsync(serial);

            return Ok(model);
        }
    }
}
