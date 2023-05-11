using Core.Entities.Concrete;
using Core.Extensions;
using Core.Resources.Enums;
using Core.Utilities.Helpers.Abstracts;
using Core.Utilities.UsableModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SBA.Business.Abstract;
using SBA.Business.BusinessHelper;
using SBA.Business.CoreAbilityServices.Job;
using SBA.Business.ExternalServices.Abstract;
using SBA.Business.FunctionalServices.Abstract;
using SBA.Business.FunctionalServices.Concrete;
using SBA.MvcUI.Models.SettingsModels;
using System.Diagnostics;

namespace SBA.MvcUI.Controllers
{
    public class SettingsController : BaseController
    {
        private readonly IDataMaintenanceService _dataMaintenanceService;
        private readonly IMatchBetService _matchBetService;
        private readonly IFilterResultService _filterResultService;
        private readonly ILeagueStatisticsHolderService _leagueStatisticsHolderService;
        private readonly ITeamPerformanceStatisticsHolderService _teamPerformanceStatisticsHolderService;
        private readonly IComparisonStatisticsHolderService _comparisonStatisticsHolderService;
        private readonly IAverageStatisticsHolderService _averageStatisticsHolderService;
        private readonly IMatchIdentifierService _matchIdentifierService;
        private readonly IStatisticInfoHolderService _statisticInfoHolderService;
        private readonly IAiDataHolderService _aiDataHolderService;
        private readonly IForecastService _forecastService;
        private readonly IConfigHelper _configHelper;
        private readonly ISocialBotMessagingService _telegramService;
        private readonly IConfiguration _configuration;

        public SettingsController(IDataMaintenanceService dataMaintenanceService,
                                  IMatchBetService matchBetService,
                                  IConfigHelper configHelper,
                                  IFilterResultService filterResultService,
                                  ISocialBotMessagingService telegramService,
                                  IConfiguration configuration,
                                  ILeagueStatisticsHolderService leagueStatisticsHolderService,
                                  ITeamPerformanceStatisticsHolderService teamPerformanceStatisticsHolderService,
                                  IComparisonStatisticsHolderService comparisonStatisticsHolderService,
                                  IMatchIdentifierService matchIdentifierService,
                                  IAverageStatisticsHolderService averageStatisticsHolderService,
                                  IStatisticInfoHolderService statisticInfoHolderService,
                                  IAiDataHolderService aiDataHolderService,
                                  IForecastService forecastService) : base(matchBetService, configuration)
        {
            _dataMaintenanceService = dataMaintenanceService;
            _matchBetService = matchBetService;
            _filterResultService = filterResultService;
            _configHelper = configHelper;
            _telegramService = telegramService;
            _configuration = configuration;

            _matchIdentifierService = matchIdentifierService;
            _leagueStatisticsHolderService = leagueStatisticsHolderService;
            _teamPerformanceStatisticsHolderService = teamPerformanceStatisticsHolderService;
            _comparisonStatisticsHolderService = comparisonStatisticsHolderService;
            _averageStatisticsHolderService = averageStatisticsHolderService;
            _statisticInfoHolderService = statisticInfoHolderService;
            _aiDataHolderService = aiDataHolderService;
            _forecastService = forecastService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("/Settings/AnalyseExternal")]
        public IActionResult AnalyseExternalResult(UserCheckerContainer container)
        {
            List<string> listSerials = OperationalProcessor.SplitSerials(container.Serials).ToList();

            if (listSerials.Count == 0)
            {
                return BadRequest(500);
            }

            JobOperation job = new JobOperation(_telegramService, new List<TimeSerialContainer>(), _matchBetService, _filterResultService, new SystemCheckerContainer(), DescriptionJobResultEnum.Standart, CountryContainer, _leagueStatisticsHolderService, _comparisonStatisticsHolderService, _averageStatisticsHolderService, _teamPerformanceStatisticsHolderService, _matchIdentifierService, _statisticInfoHolderService, _aiDataHolderService, _forecastService, _configuration);

            var pathes = new Dictionary<string, string>();

            pathes.Add("responseProfilerTemp", jsonPathFormat.GetJsonFileByFormat("responseProfilerTemp"));

            job.ExecuteTTT(listSerials, pathes, CountryContainer, LeagueContainer, container.CheckUser);

            return Ok(204);
        }

        [HttpPost("/Settings/RelativeAnalysing")]
        public IActionResult AnalyseRelativeResult(string serials)
        {
            List<string> listSerials = OperationalProcessor.SplitSerials(serials).ToList();

            if (listSerials.Count == 0)
            {
                return BadRequest(500);
            }

            JobOperation job = new JobOperation(_telegramService, new List<TimeSerialContainer>(), _matchBetService, _filterResultService, new SystemCheckerContainer(), DescriptionJobResultEnum.Standart, CountryContainer, _leagueStatisticsHolderService, _comparisonStatisticsHolderService, _averageStatisticsHolderService, _teamPerformanceStatisticsHolderService, _matchIdentifierService, _statisticInfoHolderService, _aiDataHolderService, _forecastService, _configuration);

            var pathes = new Dictionary<string, string>();
            pathes.Add("responseProfilerTempNisbi", jsonPathFormat.GetJsonFileByFormat("responseProfilerTempNisbi"));
            pathes.Add("Report", txtPathFormat.GetTextFileByFormat("Report"));

            job.ExecuteNisbi(listSerials, pathes, CountryContainer, LeagueContainer);

            return Ok(204);
        }

        [HttpPost("/Settings/RestoreDatabase")]
        public IActionResult RestoreDb()
        {
            List<MatchBet> matchBetsChk = new List<MatchBet>();
            List<FilterResult> filterResultsChk = new List<FilterResult>();

            using (var writerMatchBet = new StreamReader(jsonPathFormat.GetJsonFileByFormat("backupMatchBet")))
            {
                string mtbs = writerMatchBet.ReadToEnd();
                matchBetsChk = JsonConvert.DeserializeObject<List<MatchBet>>(mtbs);
                matchBetsChk.ForEach(x => x.Id = 0);
            }

            using (var writerFilterResult = new StreamReader(jsonPathFormat.GetJsonFileByFormat("backupFilterResult")))
            {
                string ftrs = writerFilterResult.ReadToEnd();
                filterResultsChk = JsonConvert.DeserializeObject<List<FilterResult>>(ftrs);
                filterResultsChk.ForEach(x => x.Id = 0);
            }

            _matchBetService.AddRange(matchBetsChk);
            _filterResultService.AddRange(filterResultsChk);
            return Ok();
        }

        [HttpPost("/Settings/BackupDatabase")]
        public IActionResult BackupDb()
        {
            List<MatchBet> allMatchBets = _matchBetService.GetList().Data;
            List<FilterResult> allFilterResults = _filterResultService.GetList().Data;

            using (var writerMatchBet = new StreamWriter(jsonPathFormat.GetJsonFileByFormat("backupMatchBet")))
            {
                writerMatchBet.Write(JsonConvert.SerializeObject(allMatchBets, Formatting.Indented));
            }

            using (var writerFilterResult = new StreamWriter(jsonPathFormat.GetJsonFileByFormat("backupFilterResult")))
            {
                writerFilterResult.Write(JsonConvert.SerializeObject(allFilterResults, Formatting.Indented));
            }

            return Ok();
        }

        [HttpPost("/Settings/RealDataAnalysing")]
        public IActionResult AnalyseRealCustomAnalysing(UserCheckerContainer container)
        {
            List<string> listSerials = OperationalProcessor.SplitSerials(container.Serials).ToList();

            if (listSerials.Count == 0)
            {
                return BadRequest(500);
            }

            JobOperation job = new JobOperation(_telegramService, new List<TimeSerialContainer>(), _matchBetService, _filterResultService, new SystemCheckerContainer(), DescriptionJobResultEnum.Standart, CountryContainer, _leagueStatisticsHolderService, _comparisonStatisticsHolderService, _averageStatisticsHolderService, _teamPerformanceStatisticsHolderService, _matchIdentifierService, _statisticInfoHolderService, _aiDataHolderService, _forecastService, _configuration);

            var pathes = new Dictionary<string, string>();
            pathes.Add("responseProfilerTemp", jsonPathFormat.GetJsonFileByFormat("responseProfilerTemp"));

            job.ExecuteTTT2(listSerials, pathes, CountryContainer, LeagueContainer, container.CheckUser);

            return Ok(204);
        }

        [HttpPost("/Settings/UpdateEmptyDataes")]
        public IActionResult UpdateEmptyDataes(string serials)
        {
            var allMatches = _matchBetService.GetList(x => x.MatchDate > DateTime.Now.AddDays(-10)).Data;

            for (int i = 0; i < allMatches.Count; i++)
            {
                var match = allMatches[i];
                #region LeagueStringCorrector
                match.LeagueName = match.LeagueName
                                  .Replace("2018/2019", "").Trim()
                                  .Replace("2019/2020", "").Trim()
                                  .Replace("2020/2021", "").Trim()
                                  .Replace("2021/2022", "").Trim()
                                  .Replace("2022/2023", "").Trim()
                                  .Replace("2023/2024", "").Trim()
                                  .Replace("Grup A", "").Trim()
                                  .Replace("Grup B", "").Trim()
                                  .Replace("Grup C", "").Trim()
                                  .Replace("Grup D", "").Trim()
                                  .Replace("Grup E", "").Trim()
                                  .Replace("Grup F", "").Trim()
                                  .Replace("Grup G", "").Trim()
                                  .Replace("Grup H", "").Trim()
                                  .Replace("Grup I", "").Trim()
                                  .Replace("Grup J", "").Trim()
                                  .Replace("Grup K", "").Trim()
                                  .Replace("Grup L", "").Trim()
                                  .Replace("Grup M", "").Trim()
                                  .Replace("Grup N", "").Trim()
                                  .Replace("Group A", "").Trim()
                                  .Replace("Group B", "").Trim()
                                  .Replace("Group C", "").Trim()
                                  .Replace("Group D", "").Trim()
                                  .Replace("Group E", "").Trim()
                                  .Replace("Group F", "").Trim()
                                  .Replace("Group G", "").Trim()
                                  .Replace("Group H", "").Trim()
                                  .Replace("Group I", "").Trim()
                                  .Replace("Group J", "").Trim()
                                  .Replace("Group K", "").Trim()
                                  .Replace("Group L", "").Trim()
                                  .Replace("Group M", "").Trim()
                                  .Replace("Group N", "").Trim()
                                  .Replace("Grup 1", "").Trim()
                                  .Replace("Grup 2", "").Trim()
                                  .Replace("Grup 3", "").Trim()
                                  .Replace("Grup 4", "").Trim()
                                  .Replace("Grup 5", "").Trim()
                                  .Replace("Grup 6", "").Trim()
                                  .Replace("Grup 7", "").Trim()
                                  .Replace("Grup 8", "").Trim()
                                  .Replace("Grup 9", "").Trim()
                                  .Replace("Grup 10", "").Trim()
                                  .Replace("Grup 11", "").Trim()
                                  .Replace("Grup 12", "").Trim()
                                  .Replace("Grup 13", "").Trim()
                                  .Replace("Grup 14", "").Trim()
                                  .Replace("Grup 15", "").Trim()
                                  .Replace("Grup 16", "").Trim()
                                  .Replace("Group 1", "").Trim()
                                  .Replace("Group 2", "").Trim()
                                  .Replace("Group 3", "").Trim()
                                  .Replace("Group 4", "").Trim()
                                  .Replace("Group 5", "").Trim()
                                  .Replace("Group 6", "").Trim()
                                  .Replace("Group 7", "").Trim()
                                  .Replace("Group 8", "").Trim()
                                  .Replace("Group 9", "").Trim()
                                  .Replace("Group 10", "").Trim()
                                  .Replace("Group 11", "").Trim()
                                  .Replace("Group 12", "").Trim()
                                  .Replace("Group 13", "").Trim()
                                  .Replace("Group 14", "").Trim()
                                  .Replace("Group 15", "").Trim()
                                  .Replace("Group 16", "").Trim()
                                  .Replace("2018", "").Trim()
                                  .Replace("2019", "").Trim()
                                  .Replace("2020", "").Trim()
                                  .Replace("2021", "").Trim()
                                  .Replace("2022", "").Trim()
                                  .Replace("2023", "").Trim()
                                  .Replace("2024", "").Trim()
                                  .Replace("Son 64 Turu", "").Trim()
                                  .Replace("Son 32 Turu", "").Trim()
                                  .Replace("Son 16 Turu", "").Trim()
                                  .Replace("Son 8 Turu", "").Trim()
                                  .Replace("Turu", "").Trim()
                                  .Replace("1. Tur", "").Trim()
                                  .Replace("2. Tur", "").Trim()
                                  .Replace("3. Tur", "").Trim()
                                  .Replace("4. Tur", "").Trim()
                                  .Replace("5. Tur", "").Trim()
                                  .Replace("6. Tur", "").Trim()
                                  .Replace("7. Tur", "").Trim()
                                  .Replace("8. Tur", "").Trim()
                                  .Replace("9. Tur", "").Trim()
                                  .Replace("10. Tur", "").Trim()
                                  .Replace("11. Tur", "").Trim()
                                  .Replace("12. Tur", "").Trim()
                                  .Replace("13. Tur", "").Trim()
                                  .Replace("14. Tur", "").Trim()
                                  .Replace("15. Tur", "").Trim()
                                  .Replace("16. Tur", "").Trim()
                                  .Replace("1.Tur", "").Trim()
                                  .Replace("2.Tur", "").Trim()
                                  .Replace("3.Tur", "").Trim()
                                  .Replace("4.Tur", "").Trim()
                                  .Replace("5.Tur", "").Trim()
                                  .Replace("6.Tur", "").Trim()
                                  .Replace("7.Tur", "").Trim()
                                  .Replace("8.Tur", "").Trim()
                                  .Replace("9.Tur", "").Trim()
                                  .Replace("10.Tur", "").Trim()
                                  .Replace("11.Tur", "").Trim()
                                  .Replace("12.Tur", "").Trim()
                                  .Replace("13.Tur", "").Trim()
                                  .Replace("14.Tur", "").Trim()
                                  .Replace("15.Tur", "").Trim()
                                  .Replace("16.Tur", "").Trim()
                                  .Replace("La Liga", "LaLiga").Trim()
                                  .Replace("Canlı", "").Trim();
                #endregion

            }

            var res = _matchBetService.UpdateRange(allMatches).Data;


            #region Delete EMPTY LEAGUES And Non Matched FILTERS

            var matchBetsDelete = _matchBetService.GetList(x => string.IsNullOrEmpty(x.LeagueName.Trim()) || string.IsNullOrEmpty(x.Country.Trim())).Data;

            var result = _matchBetService.RemoveRange(matchBetsDelete).Data;

            var allMatchBetsSerials = _matchBetService.GetList().Data.Select(x => x.SerialUniqueID).ToList();

            var allFilters = _filterResultService.GetList().Data;

            var selectFilters = allFilters.Where(x => !allMatchBetsSerials.Contains(x.SerialUniqueID)).ToList();

            var dels = _filterResultService.RemoveRange(selectFilters).Data;
            #endregion

            return Ok(204);
        }

        [HttpPost("/Settings/ClearCookie")]
        public IActionResult ClearCookie()
        {
            string dateDefinitionKey = Configuration.GetValue<string>("date_league_key");
            Response.Cookies.Delete(dateDefinitionKey);
            using (var writer = new StreamWriter(jsonPathFormat.GetJsonFileByFormat("Leagues")))
            {
                writer.Write(string.Empty);
            }
            using (var writer = new StreamWriter(jsonPathFormat.GetJsonFileByFormat("responseProfilerTemp")))
            {
                writer.Write(string.Empty);
            }
            using (var writer = new StreamWriter(jsonPathFormat.GetJsonFileByFormat("responseProfilerTempNisbi")))
            {
                writer.Write(string.Empty);
            }
            using (var writer = new StreamWriter(jsonPathFormat.GetJsonFileByFormat("responseProfilerTempReal")))
            {
                writer.Write(string.Empty);
            }
            return Ok(204);
        }

        [HttpPost("/Settings/UpdateCorners")]
        public IActionResult UpdateCorners()
        {
            var stp = new Stopwatch();
            stp.Start();

            var webOperation = new WebOperation();
            var allFilterResults = _filterResultService.GetList(x => !x.Is_FT_Win1 && !x.Is_FT_Win2 && !x.Is_FT_X).Data.ToList();

            for (int i = 0; i < allFilterResults.Count; i++)
            {
                FilterResult fResult = allFilterResults[i];

                fResult = OperationalProcessor.GenerateFilterResultCornersAndFT(fResult);
            }

            _filterResultService.UpdateRange(allFilterResults);

            stp.Stop();
            var minute = (decimal) stp.ElapsedMilliseconds / (1000 * 60);

            _telegramService.SendMessage($"{allFilterResults.Count} DATA.\nFinished at {minute} minutes.");

            return Ok(204);
        }
    }
}
