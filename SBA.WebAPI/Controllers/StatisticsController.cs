using Core.Resources.Enums;
using Core.Utilities.Helpers.Serialization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SBA.Business.Abstract;
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
        private readonly ITranslationService _translationService;
        private readonly IConfiguration _configuration;
        private readonly ChatGPTService _aiService;
        private readonly FileFormatBinder _formatBinder;

        public StatisticsController(IComparisonStatisticsHolderService comparisonStatisticsHolderService,
                                    ITeamPerformanceStatisticsHolderService teamPerformanceStatisticsHolderService,
                                    IAverageStatisticsHolderService averageStatisticsHolderService,
                                    ILeagueStatisticsHolderService leagueStatisticsHolderService,
                                    ITranslationService translationService,
                                    IConfiguration configuration)
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
        }

        [HttpGet("getaverage/home-away/{serial}")]
        public IActionResult GetAverageOnlyHomeAwayStatistics(int serial)
        {
            int bySideType = (int)BySideType.HomeAway;
            var result = _averageStatisticsHolderService.GetAverageMatchResultById(serial, bySideType);

            return Ok(result);
        }

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
            //string statPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot","files","statFormat.txt");
            //string statPosShutPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot","files", "statTeamPosShutFormat.txt");
            //string statTeamCornerPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot","files", "statTeamCornerFormat.txt");
            //string statAllCornerPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot","files", "statAllTeamsCornerFormat.txt");

            //string statFormat;
            //string statPosShutFormat;
            //string statTeamCornerFormat;
            //string statAllCornerFormat;

            //using (var sr = new StreamReader(statPath))
            //{
            //    statFormat = sr.ReadToEnd();
            //}

            //using (var sr = new StreamReader(statPosShutPath))
            //{
            //    statPosShutFormat = sr.ReadToEnd();
            //}

            //using (var sr = new StreamReader(statTeamCornerPath))
            //{
            //    statTeamCornerFormat = sr.ReadToEnd();
            //}

            //using (var sr = new StreamReader(statAllCornerPath))
            //{
            //    statAllCornerFormat = sr.ReadToEnd();
            //}

            var resultComplexData = _leagueStatisticsHolderService.GetAiComplexStatistics(serial);

            if (resultComplexData is null)
                return "AI Model Could not be found!";

            //var resultAiModel = _formatBinder.BindComplexStats(statFormat, statPosShutFormat, statTeamCornerFormat, statAllCornerFormat, resultComplexData);

            var aiModel = resultComplexData.MapToAI();

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                Converters = new List<JsonConverter> { new IgnoreNegativeValuesConverter() },
                NullValueHandling = NullValueHandling.Ignore
            };

            var jsonAiModel = JsonConvert.SerializeObject(aiModel, settings);

            var resultGuess = await _aiService.CallOpenAIAsync(jsonAiModel);

            //var messages = new Messages.AdvisorGuessMessages();

            //if (!resultGuess.ToLower().Contains("error"))
            //    resultGuess = string.Format("{0}\n{1}", resultGuess, messages.Message);

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
    }
}
