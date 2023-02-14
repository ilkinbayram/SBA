using Core.CrossCuttingConcerns.Caching;
using Core.Extensions;
using Core.Resources.Enums;
using Core.Utilities.Helpers.Abstracts;
using Core.Utilities.UsableModel;
using Core.Utilities.UsableModel.Visualisers;
using Microsoft.AspNetCore.Mvc;
using SBA.Business.Abstract;
using SBA.Business.BusinessHelper;
using SBA.Business.FunctionalServices.Concrete;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace SBA.MvcUI.Controllers
{
    public class SystemController : BaseController
    {
        private readonly MatchInfoProceeder _proceeder;

        private readonly IMatchBetService _matchBetService;
        private readonly IFilterResultService _filterResultService;
        private readonly IConfigHelper _configHelper;
        private readonly ICacheManager _cacheManager;
        private readonly IConfiguration _configuration;

        public SystemController(IMatchBetService matchBetService,
                                IFilterResultService filterResultService,
                                IConfigHelper configHelper,
                                ICacheManager cacheManager,
                                IConfiguration configuration) : base(matchBetService, configuration)
        {
            _proceeder = new MatchInfoProceeder();

            _matchBetService = matchBetService;
            _filterResultService = filterResultService;
            _configHelper = configHelper;
            _cacheManager = cacheManager;

            _configuration = configuration;
        }

        public IActionResult Index()
        {
            SystemCheckerContainer model = new SystemCheckerContainer();
            return View(model);
        }

        [HttpPost("/System/GuestCurrentMatchResult")]
        public PartialViewResult GuestCurrentMatchResult(SystemCheckerContainer viewModel)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var responseProfiler = OperationalProcessor.GetJobAnalyseModelResult(viewModel, _matchBetService, _filterResultService, CountryContainer);

            if (_cacheManager.IsAdd(CachingKeysEnum.GuestSytemKeyStandartFirst.ToString()))
                _cacheManager.Remove(CachingKeysEnum.GuestSytemKeyStandartFirst.ToString());

            _cacheManager.Add(CachingKeysEnum.GuestSytemKeyStandartFirst.ToString(), responseProfiler, 60);

            List<AnalyseResultVisualiser> visualiserModelList;

            if (responseProfiler != null && responseProfiler.Count>0)
            {
                visualiserModelList = OperationalProcessor.GetDataVisualisers(responseProfiler, viewModel.MinimumPercentage);

                stopwatch.Stop();
                var timeInline = stopwatch.ElapsedMilliseconds / 1000;

                return PartialView("PartialViews/_PartialGuestTable", visualiserModelList);
            }

            stopwatch.Stop();
            var time = stopwatch.ElapsedMilliseconds / 1000;

            return PartialView("PartialViews/_PartialNotFound");
        }

        [HttpPost("/System/GuestAlsoExtraResult")]
        public PartialViewResult GuestAlsoExtraResult(ExtraCheckerContainer extraChecker)
        {
            // TODO:GetList Also Which is Less Odd

            var cachedProfiler = _cacheManager.Get<List<TeamPercentageProfiler>>(CachingKeysEnum.GuestSytemKeyStandartFirst.ToString());

            SystemCheckerContainer viewModel = new SystemCheckerContainer
            {
                MinimumFoundMatch = extraChecker.MinimumFoundMatch,
                MinimumPercentage = extraChecker.MinimumPercentage,
                IsFT_25_OU_Checked = extraChecker.IsFT_25_OU_Checked,
                IsGG_NG_Checked = extraChecker.IsGG_NG_Checked,
                IsCountry_Checked = extraChecker.IsCountry_Checked,
                SerialsBeforeGenerated = cachedProfiler.Select(x => x.Serial).ToList()
            };

            var responseProfiler = OperationalProcessor.GetJobAnalyseModelResult(viewModel, _matchBetService, _filterResultService, CountryContainer);


            List<AnalyseResultVisualiser> visualiserModelList;

            if (responseProfiler != null && responseProfiler.Count > 0)
            {
                visualiserModelList = OperationalProcessor.GetDataVisualisers(responseProfiler, viewModel.MinimumPercentage);

                return PartialView("PartialViews/_PartialGuestTable", visualiserModelList);
            }

            return PartialView("PartialViews/_PartialNotFound");
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

        [HttpPost]
        public IActionResult CalculateInitialisedAllVariationsPercentage(SystemCheckerContainer viewModel)
        {
            SystemCheckerContainer Vm_FT_UST = viewModel;
            SystemCheckerContainer Vm_FT_GG = viewModel;
            SystemCheckerContainer Vm_FT_UST_GG = viewModel;
            SystemCheckerContainer Vm_FT_HT = viewModel;
            SystemCheckerContainer Vm_FT_HT_UST = viewModel;
            SystemCheckerContainer Vm_FT_HT_GG = viewModel;
            SystemCheckerContainer Vm_FT_HT_GG_UST = viewModel;

            Vm_FT_UST.IsFT_25_OU_Checked = true;
            Vm_FT_UST.MinimumFoundMatch = 4;

            Vm_FT_GG.IsGG_NG_Checked = true;
            Vm_FT_GG.MinimumFoundMatch = 4;

            Vm_FT_UST_GG.IsFT_25_OU_Checked = true;
            Vm_FT_UST_GG.IsGG_NG_Checked = true;
            Vm_FT_UST_GG.MinimumFoundMatch = 3;

            Vm_FT_HT.IsHT_ResultChecked = true;
            Vm_FT_HT.MinimumFoundMatch = 3;

            Vm_FT_HT_UST.IsHT_ResultChecked = true;
            Vm_FT_HT_UST.IsFT_25_OU_Checked = true;
            Vm_FT_HT_UST.MinimumFoundMatch = 3;

            Vm_FT_HT_GG.IsHT_ResultChecked = true;
            Vm_FT_HT_GG.IsGG_NG_Checked = true;
            Vm_FT_HT_GG.MinimumFoundMatch = 3;

            Vm_FT_HT_GG_UST.IsGG_NG_Checked = true;
            Vm_FT_HT_GG_UST.IsHT_ResultChecked = true;
            Vm_FT_HT_GG_UST.IsFT_25_OU_Checked = true;
            Vm_FT_HT_GG_UST.MinimumFoundMatch = 3;


            var responseProfiler = OperationalProcessor.GetInitializingTeamProfileResult(viewModel, _matchBetService, _filterResultService, CountryContainer);

            var responseProfilerFT_UST = OperationalProcessor.GetInitializingTeamProfileResult(Vm_FT_UST, _matchBetService, _filterResultService, CountryContainer);

            var responseProfilerFT_GG = OperationalProcessor.GetInitializingTeamProfileResult(Vm_FT_GG, _matchBetService, _filterResultService, CountryContainer);

            var responseProfilerFT_UST_GG = OperationalProcessor.GetInitializingTeamProfileResult(Vm_FT_UST_GG, _matchBetService, _filterResultService, CountryContainer);

            var responseProfilerFT_HT = OperationalProcessor.GetInitializingTeamProfileResult(Vm_FT_HT, _matchBetService, _filterResultService, CountryContainer);

            var responseProfilerFT_HT_UST = OperationalProcessor.GetInitializingTeamProfileResult(Vm_FT_HT_UST, _matchBetService, _filterResultService, CountryContainer);

            var responseProfilerFT_HT_GG = OperationalProcessor.GetInitializingTeamProfileResult(Vm_FT_HT_GG, _matchBetService, _filterResultService, CountryContainer);

            var responseProfilerFT_HT_GG_UST = OperationalProcessor.GetInitializingTeamProfileResult(Vm_FT_HT_GG_UST, _matchBetService, _filterResultService, CountryContainer);

            List<InitialiserPercentageContainer> resultPercentageVisualiser = new List<InitialiserPercentageContainer>();

            // JUST Full Time Result => around
            var result = OperationalProcessor.InitialiseWinningPercentage(responseProfiler);
            result.WhichCheckersAround = "JUST Full Time Result => Min Found: 5";
            resultPercentageVisualiser.Add(result);

            // FT _ UST => around
            var resultFT_UST = OperationalProcessor.InitialiseWinningPercentage(responseProfilerFT_UST);
            resultFT_UST.WhichCheckersAround = "FT _._ UST => Min Found: 4";
            resultPercentageVisualiser.Add(resultFT_UST);

            // FT _ GG => around
            var resultFT_GG = OperationalProcessor.InitialiseWinningPercentage(responseProfilerFT_GG);
            resultFT_GG.WhichCheckersAround = "FT _._ GG => Min Found: 4";
            resultPercentageVisualiser.Add(resultFT_GG);

            // FT _ UST _ GG => around
            var resultFT_GG_UST = OperationalProcessor.InitialiseWinningPercentage(responseProfilerFT_UST_GG);
            resultFT_GG_UST.WhichCheckersAround = "FT _._ UST _._ GG => Min Found: 3";
            resultPercentageVisualiser.Add(resultFT_GG_UST);

            // FT _ HT => around
            var resultFT_HT = OperationalProcessor.InitialiseWinningPercentage(responseProfilerFT_HT);
            resultFT_HT.WhichCheckersAround = "FT _._ HT => Min Found: 3";
            resultPercentageVisualiser.Add(resultFT_HT);

            // FT _ HT _ UST => around
            var resultFT_HT_UST = OperationalProcessor.InitialiseWinningPercentage(responseProfilerFT_HT_UST);
            resultFT_HT_UST.WhichCheckersAround = "FT _._ HT _._ UST => Min Found: 3";
            resultPercentageVisualiser.Add(resultFT_HT_UST);

            // FT _ HT _ GG => around
            var resultFT_HT_GG = OperationalProcessor.InitialiseWinningPercentage(responseProfilerFT_HT_GG);
            resultFT_HT_GG.WhichCheckersAround = "FT _._ HT _._ GG => Min Found: 3";
            resultPercentageVisualiser.Add(resultFT_HT_GG);

            // FT _ HT _ GG _ UST => around
            var resultFT_HT_GG_UST = OperationalProcessor.InitialiseWinningPercentage(responseProfilerFT_HT_GG_UST);
            resultFT_HT_GG_UST.WhichCheckersAround = "FT _._ HT _._ GG _._ UST => Min Found: 3";
            resultPercentageVisualiser.Add(resultFT_HT_GG_UST);

            using (StreamWriter sw = new StreamWriter(txtPathFormat.GetTextFileByFormat("UnstartedStagingContainerSerials")))
            {
                string jsonVisualiser = Newtonsoft.Json.JsonConvert.SerializeObject(resultPercentageVisualiser, Newtonsoft.Json.Formatting.Indented);
                sw.WriteLine(jsonVisualiser);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
