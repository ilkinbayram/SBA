using Core.Entities.Concrete;
using Core.Extensions;
using Core.Resources.Enums;
using Core.Utilities.Helpers.Abstracts;
using Core.Utilities.UsableModel;
using Microsoft.AspNetCore.Mvc;
using SBA.Business.Abstract;
using SBA.Business.BusinessHelper;
using SBA.Business.CoreAbilityServices.Job;
using SBA.Business.ExternalServices.Abstract;
using SBA.Business.FunctionalServices.Concrete;
using SBA.Business.Mapping;
using SBA.MvcUI.Models;
using SBA.MvcUI.Models.CustomViewModels.Home;
using System.Diagnostics;
using System.Text;
using ST = System.Timers;

namespace SBA.MvcUI.Controllers
{
    public class HomeController : BaseController
    {
        private readonly MatchInfoProceeder _proceeder;

        private readonly IMatchBetService _matchBetService;
        private readonly IFilterResultService _filterResultService;
        private readonly ILeagueStatisticsHolderService _leagueStatisticsHolderService;
        private readonly ITeamPerformanceStatisticsHolderService _teamPerformanceStatisticsHolderService;
        private readonly IComparisonStatisticsHolderService _comparisonStatisticsHolderService;
        private readonly IAverageStatisticsHolderService _averageStatisticsHolderService;
        private readonly IMatchIdentifierService _matchIdentifierService;
        private readonly IConfigHelper _configHelper;
        private readonly ISocialBotMessagingService _telegramService;
        private readonly IConfiguration _configuration;

        public HomeController(IMatchBetService matchBetService,
                              IFilterResultService filterResultService,
                              IConfigHelper configHelper,
                              ISocialBotMessagingService telegramService,
                              IConfiguration configuration,
                              ILeagueStatisticsHolderService leagueStatisticsHolderService,
                              ITeamPerformanceStatisticsHolderService teamPerformanceStatisticsHolderService,
                              IComparisonStatisticsHolderService comparisonStatisticsHolderService,
                              IMatchIdentifierService matchIdentifierService,
                              IAverageStatisticsHolderService averageStatisticsHolderService) : base(matchBetService, configuration)
        {
            _proceeder = new MatchInfoProceeder();

            _matchBetService = matchBetService;
            _telegramService = telegramService;
            _filterResultService = filterResultService;
            _configHelper = configHelper;

            _matchIdentifierService = matchIdentifierService;
            _leagueStatisticsHolderService = leagueStatisticsHolderService;
            _teamPerformanceStatisticsHolderService = teamPerformanceStatisticsHolderService;
            _comparisonStatisticsHolderService = comparisonStatisticsHolderService;
            _averageStatisticsHolderService = averageStatisticsHolderService;

            _configuration = configuration;
        }

        public IActionResult Index()
        {
            var lgCont = LeagueContainer;
            FunctionalProcessTriggerViewModel functionalViewModel = new FunctionalProcessTriggerViewModel();
            return View(functionalViewModel);
        }

        [HttpPost("/Home/StartStandartAnalyser")]
        public string StartStandartAnalyser(FunctionalProcessTriggerViewModel viewModel)
        {
            ExecuteSynchronousTest();

            // ExecuteStandartJob(viewModel.BeforeDateCount);
            return "Standart Analysing Process Started...";
        }


        [HttpPost("/Home/StartHighAnalyser")]
        public string StartHighAnalyser(FunctionalProcessTriggerViewModel viewModel)
        {
            ExecuteJob(true, viewModel.BeforeDateCount, DescriptionJobResultEnum.HighReceiver);
            return "High-Acceptor Analysing Started...";
        }

        public IActionResult GenerateFromSerials(string serials)
        {
            var watch = new Stopwatch();
            watch.Start();
            int success = 0;
            int fails = 0;
            int emptyResults = 0;

            var failSerials = new StringBuilder();
            var emptySerials = new StringBuilder();

            List<int> listSerials = new List<int>();

            if (serials != "Burani Sil ve Serialari yapistir...")
            {
                listSerials = OperationalProcessor.SplitSerials(serials).Select(x => Convert.ToInt32(x)).ToList();
            }
            else
            {
                listSerials = OperationalProcessor.ReadTextObject("StagingContainerSerials").Split("|").Select(x => x.Trim()).Where(x => x != string.Empty).ToList().Select(x => Convert.ToInt32(x)).ToList();
            }


            if (listSerials != null && listSerials.Count > 0)
            {
                var hashSet = new HashSet<int>(listSerials);
                var uniqueSerials = hashSet.ToList();

                var filterResultList = new List<FilterResult>();
                var matchBetList = new List<MatchBet>();

                var uniqueSerialsMb = uniqueSerials.Where(x => !_matchBetService.Query().Data.Select(x => x.SerialUniqueID).Contains(x)).ToList();
                var uniqueSerialsFr = uniqueSerials.Where(x => !_filterResultService.Query().Data.Select(x => x.SerialUniqueID).Contains(x)).ToList();

                foreach (var serial in uniqueSerialsMb)
                {
                    var result = _proceeder.GenerateSingleBetInformation(serial.ToString(), CountryContainer);
                    if (result == null)
                    {
                        emptySerials.Append(serial);
                        emptySerials.Append(" | ");
                        emptyResults++;
                        continue;
                    }

                    var filterResult = OperationalProcessor.GetSingleFilterResultFromMatchInfoContainer(result);

                    var matchBet = result.MapToMatchBetFromMatchInfo();

                    if (matchBet != null)
                    {
                        matchBetList.Add(matchBet);
                    }
                    else
                    {
                        failSerials.Append(serial);
                        failSerials.Append(" | ");
                        fails++;
                        continue;
                    }

                    if (filterResult != null)
                    {
                        filterResult = OperationalProcessor.GenerateFilterResultCornersAndFT(filterResult);
                        filterResultList.Add(filterResult);
                    }
                    else
                    {
                        failSerials.Append(serial);
                        failSerials.Append(" | ");
                        matchBetList.Remove(matchBet);
                        fails++;
                        continue;
                    }

                    success++;
                }

                // OperationalProcessor.WriteObject(matchBetList, "tempMatchBet", FileType.Json);
                _matchBetService.AddRange(matchBetList);
                _filterResultService.AddRange(filterResultList);
            }

            var seconds = watch.ElapsedMilliseconds / 1000;
            var minutes = seconds / 60;
            var hours = minutes / 60;

            var oneOprSeconds = (decimal)seconds / listSerials.Count;

            var readyText = new StringBuilder();

            using (var reader = new StreamReader(txtPathFormat.GetTextFileByFormat("Report")))
            {
                readyText.Append(reader.ReadToEnd());
            }

            using (var writer = new StreamWriter(txtPathFormat.GetTextFileByFormat("Report")))
            {
                readyText.Append("\n\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n////////////////////////////////////////\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n\n");
                var text = $"_____Success :  {success}\n_______Fails :  {fails}\nNull Results :  {emptyResults}\n=============================================\n______________________TOTAL TIME :  {hours} hours, {minutes} minutes, {seconds} seconds\n=============================================\nHOW MANY SECONDS FOR 1 OPERATION :  {oneOprSeconds.ToString("0.00")}\n=============================================\nEMPTY  SERIALS :  {emptySerials.ToString()}\n=============================================\nFAILED  SERIALS :  {failSerials.ToString()}";

                readyText.Append(text);

                writer.Write(readyText.ToString());
            }

            return RedirectToAction("Index", "Home");
        }


        public IActionResult GenerateUnstartedAndSerializeDB()
        {
            var listSerials = new List<string>();
            using (StreamReader sr = new StreamReader(txtPathFormat.GetTextFileByFormat("UnstartedStagingContainerSerials")))
            {
                sr.ReadToEnd().Split(new string[] { "\r\n", "\t\n", "\n" }, StringSplitOptions.None).ToList().ForEach(x =>
                {
                    listSerials.Add(x);
                });
            }
            var result = _proceeder.GenerateUnstartedBetInformations(listSerials, CountryContainer);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private void ExecuteStandartJob(int beforeMonthsCount = 0)
        {
            ExecuteJob(false, beforeMonthsCount, DescriptionJobResultEnum.Standart);
        }

        private void ExecuteJob(bool high = false, int beforeMonths = 0, DescriptionJobResultEnum descriptionJobResultEnum = DescriptionJobResultEnum.Standart)
        {
            OperationalProcessor.InitialiseTimeSerials(txtPathFormat.GetTextFileByFormat("UnstartedStagingContainerSerials"));
            List<TimeSerialContainer> timeSerialContainers = OperationalProcessor.TimeSerialListContainer;

            SystemCheckerContainer _systemCheckerContainer = new SystemCheckerContainer
            {
                MinimumFoundMatch = 4,
                MinimumPercentage = 74,
                IsFT_ResultChecked = true
            };

            if (high)
            {
                _systemCheckerContainer.MinimumPercentage = 99;
            }

            if (beforeMonths > 0)
            {
                int handledMonthForBefore = -1 * beforeMonths;
                _systemCheckerContainer.FilterFromDate = DateTime.Now.AddMonths(handledMonthForBefore);
            };


            JobOperation job = new JobOperation(_telegramService, timeSerialContainers, _matchBetService, _filterResultService, _systemCheckerContainer, descriptionJobResultEnum, CountryContainer, _leagueStatisticsHolderService, _comparisonStatisticsHolderService, _averageStatisticsHolderService, _teamPerformanceStatisticsHolderService, _matchIdentifierService);
            ST.Timer aTimer = new ST.Timer();
            aTimer.Elapsed += new ST.ElapsedEventHandler(job.InTimeReflected);
            aTimer.Interval = 10000;
            aTimer.Enabled = true;
        }

        private void ExecuteJobTest()
        {
            OperationalProcessor.InitialiseTimeSerials(txtPathFormat.GetTextFileByFormat("UnstartedStagingContainerSerials"));
            List<TimeSerialContainer> timeSerialContainers = OperationalProcessor.TimeSerialListContainer;
            SystemCheckerContainer _systemCheckerContainer = new SystemCheckerContainer
            {
                MinimumFoundMatch = 4,
                MinimumPercentage = 74,
                IsFT_ResultChecked = true,
                IsAnalyseAnyTime = true
            };

            JobOperation job = new JobOperation(_telegramService, timeSerialContainers, _matchBetService, _filterResultService, _systemCheckerContainer, DescriptionJobResultEnum.Standart, CountryContainer, _leagueStatisticsHolderService, _comparisonStatisticsHolderService, _averageStatisticsHolderService, _teamPerformanceStatisticsHolderService, _matchIdentifierService);
            ST.Timer aTimer = new ST.Timer();
            aTimer.Elapsed += new ST.ElapsedEventHandler(job.TestInTimeReflected);
            aTimer.Interval = 20000;
            aTimer.Enabled = true;
        }

        private void ExecuteSynchronousTest()
        {
            // OperationalProcessor.InitialiseTimeSerials(txtPathFormat.GetTextFileByFormat("UnstartedStagingContainerSerials"));
            List<TimeSerialContainer> timeSerialContainers = OperationalProcessor.TimeSerialListContainer;
            string serialText = string.Empty;

            using (var reader = new StreamReader(txtPathFormat.GetTextFileByFormat("UnstartedStagingContainerSerials")))
            {
                serialText = reader.ReadToEnd();
            }

            SystemCheckerContainer _systemCheckerContainer = new SystemCheckerContainer
            {
                MinimumFoundMatch = 4,
                MinimumPercentage = 74,
                IsFT_ResultChecked = true,
                IsAnalyseAnyTime = true,
                SerialsText = serialText
            };

            JobOperation job = new JobOperation(_telegramService, timeSerialContainers, _matchBetService, _filterResultService, _systemCheckerContainer, DescriptionJobResultEnum.Standart, CountryContainer, _leagueStatisticsHolderService, _comparisonStatisticsHolderService, _averageStatisticsHolderService, _teamPerformanceStatisticsHolderService, _matchIdentifierService);

            job.Execute_TEST();
        }
    }
}
