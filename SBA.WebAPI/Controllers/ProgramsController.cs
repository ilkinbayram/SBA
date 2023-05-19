using Core.Entities.Concrete.ExternalDbEntities;
using Microsoft.AspNetCore.Mvc;
using SBA.Business.Abstract;
using SBA.WebAPI.Utilities.Extensions;

namespace SBA.WebAPI.Controllers
{
    [Route("api/programs")]
    [ApiController]
    [BasicAuthentication]
    public class ProgramsController : ControllerBase
    {
        private readonly IComparisonStatisticsHolderService _comparisonStatisticsHolderService;
        private readonly IAverageStatisticsHolderService _averageStatisticsHolderService;
        private readonly ITeamPerformanceStatisticsHolderService _teamPerformanceStatisticsHolderService;
        private readonly IMatchIdentifierService _matchIdentifierService;
        private readonly IForecastService _forecastService;

        public ProgramsController(IComparisonStatisticsHolderService comparisonStatisticsHolderService,
                                    ITeamPerformanceStatisticsHolderService teamPerformanceStatisticsHolderService,
                                    IAverageStatisticsHolderService averageStatisticsHolderService,
                                    IMatchIdentifierService matchIdentifierService,
                                    IForecastService forecastService)
        {
            _comparisonStatisticsHolderService = comparisonStatisticsHolderService;
            _teamPerformanceStatisticsHolderService = teamPerformanceStatisticsHolderService;
            _averageStatisticsHolderService = averageStatisticsHolderService;
            _matchIdentifierService = matchIdentifierService;
            _forecastService = forecastService;
        }


        [HttpGet("get-grouped-matches/today")]
        public async Task<IActionResult> GetTodayGroupedMatchesAsync()
        {
            var result = await _matchIdentifierService.GetGroupedMatchsProgramAsync();

            return Ok(result);
        }

        [HttpGet("get-grouped-filtered-forecast-matches/today")]
        public async Task<IActionResult> GetFilteredForecastGroupedMatchesAsync()
        {
            var result = await _matchIdentifierService.GetGroupedMatchsProgramAsync();

            return Ok(result);
        }

        [HttpGet("get-matches/today")]
        public async Task<IActionResult> GetTodayMatchesAsync()
        {
            var result = await _matchIdentifierService.GetAllMatchsProgramAsync();

            return Ok(result);
        }

        [HttpGet("get-filtered-forecast-matches/today")]
        public async Task<IActionResult> GetPossibleForecastsProgramTodayMatchesAsync()
        {
            var result = await _matchIdentifierService.GetPossibleForecastMatchsProgramAsync();

            return Ok(result);
        }

        [HttpGet("get-timer-matches/today")]
        public async Task<IActionResult> GetTodayMatchesForTimerAsync()
        {
            var resultAsync = await _matchIdentifierService.GetListAsync();
            var result = resultAsync.Data.Group();

            return Ok(result);
        }


        [HttpPost("add-possible-forecasts/today")]
        public async Task<IActionResult> AddPossibleForecastsAsync(List<PossibleForecast> possibleForecasts)
        {
            var result = await _forecastService.AddPossibleForecastsAsync(possibleForecasts);
            return Ok(result);
        }
    }
}
