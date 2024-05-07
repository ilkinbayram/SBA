using Core.Resources.Enums;
using Core.Utilities.Helpers.Abstracts;
using Core.Utilities.UsableModel;
using Microsoft.AspNetCore.Mvc;
using SBA.Business.Abstract;
using SBA.Business.BusinessHelper;
using SBA.Business.FunctionalServices.Concrete;
using System;
using System.Diagnostics;

namespace SBA.MvcUI.Controllers
{
    public class DedectiveController : BaseController
    {

        private readonly MatchInfoProceeder _proceeder;

        private readonly IMatchBetService _matchBetService;
        private readonly IFilterResultService _filterResultService;
        private readonly IConfigHelper _configHelper;
        private readonly IConfiguration _configuration;

        public DedectiveController(IMatchBetService matchBetService,
                              IFilterResultService filterResultService,
                              IConfigHelper configHelper,
                              IConfiguration configuration) : base(matchBetService, configuration)
        {
            _proceeder = new MatchInfoProceeder();

            _matchBetService = matchBetService;
            _filterResultService = filterResultService;
            _configHelper = configHelper;
            _configuration = configuration;
        }


        public IActionResult Index()
        {
            SystemCheckerContainer model = new SystemCheckerContainer
            {
                FilterFromDate = DateTime.ParseExact("01.09.2019", "dd.MM.yyyy", null),
                FilterToDate = DateTime.Now,
            };
            return View(model);
        }


        [HttpPost]
        public IActionResult GuestCurrentMatchResult(SystemCheckerContainer viewModel)
        {
            var responseProfiler = OperationalProcessor.GetJobAnalyseModelResult(viewModel, _matchBetService, _filterResultService, LeagueContainer, CountryContainer);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult CalculateInitialisedPercentage(SystemCheckerContainer viewModel)
        {
            var stp = new Stopwatch();
            stp.Start();

            var responseProfiler = OperationalProcessor.GetInitializingTeamProfileResult(viewModel, _matchBetService, _filterResultService, CountryContainer);

            var result = OperationalProcessor.InitialiseWinningPercentage(responseProfiler);

            stp.Stop();
            var time = stp.ElapsedMilliseconds / 1000;
            return RedirectToAction("Index", "Home");
        }
    }
}
