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

        public ProgramsController(IComparisonStatisticsHolderService comparisonStatisticsHolderService,
                                    ITeamPerformanceStatisticsHolderService teamPerformanceStatisticsHolderService,
                                    IAverageStatisticsHolderService averageStatisticsHolderService,
                                    IMatchIdentifierService matchIdentifierService)
        {
            _comparisonStatisticsHolderService = comparisonStatisticsHolderService;
            _teamPerformanceStatisticsHolderService = teamPerformanceStatisticsHolderService;
            _averageStatisticsHolderService = averageStatisticsHolderService;
            _matchIdentifierService = matchIdentifierService;
        }


        [HttpGet("get-matches/today")]
        public async Task<IActionResult> GetTodayMatchesAsync()
        {
            var result = _matchIdentifierService.GetMatchsProgram();

            return Ok(result);
        }

        [HttpGet("get-timer-matches/today")]
        public async Task<IActionResult> GetTodayMatchesForTimerAsync()
        {
            var resultAsync = await _matchIdentifierService.GetListAsync();
            var result = resultAsync.Data.Group();

            return Ok(result);
        }
    }
}
