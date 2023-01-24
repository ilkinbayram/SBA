using Core.Entities.Concrete;
using Core.Extensions;
using Core.Resources.Enums;
using Core.Utilities.Helpers.Abstracts;
using Core.Utilities.UsableModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SBA.Business.Abstract;
using SBA.Business.CoreAbilityServices.Job;
using SBA.Business.ExternalServices.Abstract;
using SBA.Business.FunctionalServices.Abstract;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace SBA.MvcUI.Controllers
{
    public class SettingsController : BaseController
    {
        private readonly IDataMaintenanceService _dataMaintenanceService;
        private readonly IMatchBetService _matchBetService;
        private readonly IFilterResultService _filterResultService;
        private readonly IConfigHelper _configHelper;
        private readonly ISocialBotMessagingService _telegramService;

        private readonly string txtPathFormat;
        private readonly string jsonPathFormat;

        public SettingsController(IDataMaintenanceService dataMaintenanceService,
                                  IMatchBetService matchBetService,
                                  IConfigHelper configHelper,
                                  IFilterResultService filterResultService,
                                  ISocialBotMessagingService telegramService) : base(matchBetService)
        {
            _dataMaintenanceService = dataMaintenanceService;
            _matchBetService = matchBetService;
            _filterResultService = filterResultService;
            _configHelper = configHelper;
            _telegramService = telegramService;

            txtPathFormat = _configHelper.GetSettingsData<string>(ParentKeySettings.PathConstant.ToString(), ChildKeySettings.TextPathFormat.ToString());
            jsonPathFormat = _configHelper.GetSettingsData<string>(ParentKeySettings.PathConstant.ToString(), ChildKeySettings.JsonPathFormat.ToString());
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("/Settings/SyncFilters")]
        public IActionResult SynchroniseFilters()
        {
            JobOperation job = new JobOperation(_telegramService, new List<TimeSerialContainer>(), _matchBetService, _filterResultService, new SystemCheckerContainer(), DescriptionJobResultEnum.Standart, CountryContainer);

            job.ExecuteTTT(new List<string>(), txtPathFormat.GetTextFileByFormat("SerialsForDetailsResponse"), CountryContainer);

            return Ok(204);
        }

        [HttpPost("/Settings/DeleteNonMatcheds")]
        public IActionResult DeleteNonMathcedDataes()
        {

            var result = _dataMaintenanceService.DeleteNonMatchedDataes();

            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return Ok(JsonConvert.SerializeObject(result.Responses));
            }
        }

        [HttpPost("/Settings/UpdateEmptyCountries")]
        public IActionResult UpdateEmptyCountries(string serials)
        {
            var stp = new Stopwatch();
            stp.Start();

            //var serialList = OperationalProcessor.SplitSerials(serials);

            //List<MatchBet> matchBetList = new List<MatchBet>(); 
            //List<FilterResult> filterResultList = new List<FilterResult>();

            //foreach (var serial in serialList)
            //{
            //    var delMB = _matchBetService.Get(x => x.SerialUniqueID == Convert.ToInt32(serial)).Data;
            //    var delFR = _filterResultService.Get(x => x.SerialUniqueID == Convert.ToInt32(serial)).Data;

            //    matchBetList.Add(delMB);
            //    filterResultList.Add(delFR);
            //}

            //int result = _matchBetService.DeletePermanentlyList(matchBetList).Data;
            //result += _filterResultService.DeletePermanentlyList(filterResultList).Data;




            //var matchBetList = OperationalProcessor.ReadObject<List<MatchBet>>("backupMatchBetList", FileType.Json);
            //var filterResultList = OperationalProcessor.ReadObject<List<FilterResult>>("backupFilterResultList", FileType.Json);

            //var matchBetList = OperationalProcessor.ReadObject<List<MatchBet>>("tempMatchBet", FileType.Json);

            //var res1 = _matchBetService.AddRange(matchBetList).Data;
            //var res2 = _filterResultService.AddRange(filterResultList).Data;


            //var listSerials = OperationalProcessor.ReadTextObject("StagingContainerSerials").Split("|").Select(x => x.Trim()).Where(x => x != string.Empty).ToList().Select(x => Convert.ToInt32(x)).ToList();
            //var hashSet = new HashSet<int>(listSerials);
            //var uniqueSerials = hashSet.ToList();

            //var uniqueSerialsMb = uniqueSerials.Where(x => !_matchBetService.Query().Data.Select(x => x.SerialUniqueID).Contains(x)).ToList();
            //var uniqueSerialsFr = uniqueSerials.Where(x => !_filterResultService.Query().Data.Select(x => x.SerialUniqueID).Contains(x)).ToList();

            //uniqueSerialsMb.AddRange(uniqueSerialsFr);

            //foreach (var serAllOne in uniqueSerialsMb)
            //{
            //    _matchBetService.Remove(serAllOne);
            //    _filterResultService.Remove(serAllOne);
            //}




            //List<MatchBet> allMatchBets = _matchBetService.GetList().Data.OrderBy(x=>x.MatchDate).ToList();
            //List<FilterResult> allFilterResults = _filterResultService.GetList().Data.OrderBy(x => x.Id).ToList();

            //List<MatchBet> allMatchBets = _matchBetService.GetList().Data;
            //List<FilterResult> allFilterResults = _filterResultService.GetList().Data;

            //_matchBetService.RemoveRange(allMatchBets);
            //_filterResultService.RemoveRange(allFilterResults);

            //var lastCalcGames = _matchBetService.GetList(x => x.MatchDate.Date >= DateTime.Now.AddDays(-5).Date).Data;

            //var lastCalcResults = _filterResultService.GetList(x => lastCalcGames.Select(x=>x.SerialUniqueID).Contains(x.SerialUniqueID)).Data;

            #region JobOperation BET
            ////
            //////
            ////
            ////

            JobOperation job = new JobOperation(_telegramService, new List<TimeSerialContainer>(), _matchBetService, _filterResultService, new SystemCheckerContainer(), DescriptionJobResultEnum.Standart, CountryContainer);

            job.ExecuteTTT2(new List<string>(), txtPathFormat.GetTextFileByFormat("LogFile"), LeagueContainer, CountryContainer);

            ////
            //////
            //////
            //////
            /////
            ////
            #endregion


            #region Update Leagues
            //var allMatches = _matchBetService.GetList(x=>x.MatchDate > DateTime.Now.AddDays(-10)).Data;

            //for (int i = 0; i < allMatches.Count; i++)
            //{
            //    var match = allMatches[i];

            //    match.LeagueName = match.LeagueName
            //                      .Replace("2018/2019", "").Trim()
            //                      .Replace("2019/2020", "").Trim()
            //                      .Replace("2020/2021", "").Trim()
            //                      .Replace("2021/2022", "").Trim()
            //                      .Replace("2022/2023", "").Trim()
            //                      .Replace("2023/2024", "").Trim()
            //                      .Replace("Grup A", "").Trim()
            //                      .Replace("Grup B", "").Trim()
            //                      .Replace("Grup C", "").Trim()
            //                      .Replace("Grup D", "").Trim()
            //                      .Replace("Grup E", "").Trim()
            //                      .Replace("Grup F", "").Trim()
            //                      .Replace("Grup G", "").Trim()
            //                      .Replace("Grup H", "").Trim()
            //                      .Replace("Grup I", "").Trim()
            //                      .Replace("Grup J", "").Trim()
            //                      .Replace("Grup K", "").Trim()
            //                      .Replace("Grup L", "").Trim()
            //                      .Replace("Grup M", "").Trim()
            //                      .Replace("Grup N", "").Trim()
            //                      .Replace("Group A", "").Trim()
            //                      .Replace("Group B", "").Trim()
            //                      .Replace("Group C", "").Trim()
            //                      .Replace("Group D", "").Trim()
            //                      .Replace("Group E", "").Trim()
            //                      .Replace("Group F", "").Trim()
            //                      .Replace("Group G", "").Trim()
            //                      .Replace("Group H", "").Trim()
            //                      .Replace("Group I", "").Trim()
            //                      .Replace("Group J", "").Trim()
            //                      .Replace("Group K", "").Trim()
            //                      .Replace("Group L", "").Trim()
            //                      .Replace("Group M", "").Trim()
            //                      .Replace("Group N", "").Trim()
            //                      .Replace("Grup 1", "").Trim()
            //                      .Replace("Grup 2", "").Trim()
            //                      .Replace("Grup 3", "").Trim()
            //                      .Replace("Grup 4", "").Trim()
            //                      .Replace("Grup 5", "").Trim()
            //                      .Replace("Grup 6", "").Trim()
            //                      .Replace("Grup 7", "").Trim()
            //                      .Replace("Grup 8", "").Trim()
            //                      .Replace("Grup 9", "").Trim()
            //                      .Replace("Grup 10", "").Trim()
            //                      .Replace("Grup 11", "").Trim()
            //                      .Replace("Grup 12", "").Trim()
            //                      .Replace("Grup 13", "").Trim()
            //                      .Replace("Grup 14", "").Trim()
            //                      .Replace("Grup 15", "").Trim()
            //                      .Replace("Grup 16", "").Trim()
            //                      .Replace("Group 1", "").Trim()
            //                      .Replace("Group 2", "").Trim()
            //                      .Replace("Group 3", "").Trim()
            //                      .Replace("Group 4", "").Trim()
            //                      .Replace("Group 5", "").Trim()
            //                      .Replace("Group 6", "").Trim()
            //                      .Replace("Group 7", "").Trim()
            //                      .Replace("Group 8", "").Trim()
            //                      .Replace("Group 9", "").Trim()
            //                      .Replace("Group 10", "").Trim()
            //                      .Replace("Group 11", "").Trim()
            //                      .Replace("Group 12", "").Trim()
            //                      .Replace("Group 13", "").Trim()
            //                      .Replace("Group 14", "").Trim()
            //                      .Replace("Group 15", "").Trim()
            //                      .Replace("Group 16", "").Trim()
            //                      .Replace("2018", "").Trim()
            //                      .Replace("2019", "").Trim()
            //                      .Replace("2020", "").Trim()
            //                      .Replace("2021", "").Trim()
            //                      .Replace("2022", "").Trim()
            //                      .Replace("2023", "").Trim()
            //                      .Replace("2024", "").Trim()
            //                      .Replace("Son 64 Turu", "").Trim()
            //                      .Replace("Son 32 Turu", "").Trim()
            //                      .Replace("Son 16 Turu", "").Trim()
            //                      .Replace("Son 8 Turu", "").Trim()
            //                      .Replace("Turu", "").Trim()
            //                      .Replace("1. Tur", "").Trim()
            //                      .Replace("2. Tur", "").Trim()
            //                      .Replace("3. Tur", "").Trim()
            //                      .Replace("4. Tur", "").Trim()
            //                      .Replace("5. Tur", "").Trim()
            //                      .Replace("6. Tur", "").Trim()
            //                      .Replace("7. Tur", "").Trim()
            //                      .Replace("8. Tur", "").Trim()
            //                      .Replace("9. Tur", "").Trim()
            //                      .Replace("10. Tur", "").Trim()
            //                      .Replace("11. Tur", "").Trim()
            //                      .Replace("12. Tur", "").Trim()
            //                      .Replace("13. Tur", "").Trim()
            //                      .Replace("14. Tur", "").Trim()
            //                      .Replace("15. Tur", "").Trim()
            //                      .Replace("16. Tur", "").Trim()
            //                      .Replace("1.Tur", "").Trim()
            //                      .Replace("2.Tur", "").Trim()
            //                      .Replace("3.Tur", "").Trim()
            //                      .Replace("4.Tur", "").Trim()
            //                      .Replace("5.Tur", "").Trim()
            //                      .Replace("6.Tur", "").Trim()
            //                      .Replace("7.Tur", "").Trim()
            //                      .Replace("8.Tur", "").Trim()
            //                      .Replace("9.Tur", "").Trim()
            //                      .Replace("10.Tur", "").Trim()
            //                      .Replace("11.Tur", "").Trim()
            //                      .Replace("12.Tur", "").Trim()
            //                      .Replace("13.Tur", "").Trim()
            //                      .Replace("14.Tur", "").Trim()
            //                      .Replace("15.Tur", "").Trim()
            //                      .Replace("16.Tur", "").Trim()
            //                      .Replace("La Liga", "LaLiga").Trim()
            //                      .Replace("Canlı", "").Trim();
            //}

            //var res = _matchBetService.UpdateRange(allMatches).Data;
            #endregion


            #region Delete EMPTY LEAGUES And Non Matched FILTERS

            //var matchBetsDelete = _matchBetService.GetList(x => string.IsNullOrEmpty(x.LeagueName.Trim())).Data;

            //var result = _matchBetService.RemoveRange(matchBetsDelete).Data;

            //var allMatchBetsSerials = _matchBetService.GetList().Data.Select(x=>x.SerialUniqueID).ToList();

            //var allFilters = _filterResultService.GetList().Data;

            //var selectFilters = allFilters.Where(x=>!allMatchBetsSerials.Contains(x.SerialUniqueID)).ToList();

            //var dels = _filterResultService.RemoveRange(selectFilters).Data;
            #endregion


            #region BACKUP DB to JSON
            //List<MatchBet> allMatchBets = _matchBetService.GetList().Data;
            //List<FilterResult> allFilterResults = _filterResultService.GetList().Data;

            //using (var writerMatchBet = new StreamWriter(jsonPathFormat.GetJsonFileByFormat("backupMatchBet")))
            //{
            //    writerMatchBet.Write(JsonConvert.SerializeObject(allMatchBets, Formatting.Indented));
            //}

            //using (var writerFilterResult = new StreamWriter(jsonPathFormat.GetJsonFileByFormat("backupFilterResult")))
            //{
            //    writerFilterResult.Write(JsonConvert.SerializeObject(allFilterResults, Formatting.Indented));
            //}
            #endregion


            #region JSON Checker

            //List<MatchBet> matchBetsChk = new List<MatchBet>();
            //List<FilterResult> filterResultsChk = new List<FilterResult>();

            //using (var writerMatchBet = new StreamReader(jsonPathFormat.GetJsonFileByFormat("backupMatchBet")))
            //{
            //    string mtbs = writerMatchBet.ReadToEnd();
            //    matchBetsChk = JsonConvert.DeserializeObject<List<MatchBet>>(mtbs);
            //}

            //using (var writerFilterResult = new StreamReader(jsonPathFormat.GetJsonFileByFormat("backupFilterResult")))
            //{
            //    string ftrs = writerFilterResult.ReadToEnd();
            //    filterResultsChk = JsonConvert.DeserializeObject<List<FilterResult>>(ftrs);
            //}
            #endregion


            #region Restore DB

            //List<MatchBet> matchBetsChk = new List<MatchBet>();
            //List<FilterResult> filterResultsChk = new List<FilterResult>();

            //using (var writerMatchBet = new StreamReader(jsonPathFormat.GetJsonFileByFormat("backupMatchBet")))
            //{
            //    string mtbs = writerMatchBet.ReadToEnd();
            //    matchBetsChk = JsonConvert.DeserializeObject<List<MatchBet>>(mtbs);
            //    matchBetsChk.ForEach(x => x.Id = 0);
            //}

            //using (var writerFilterResult = new StreamReader(jsonPathFormat.GetJsonFileByFormat("backupFilterResult")))
            //{
            //    string ftrs = writerFilterResult.ReadToEnd();
            //    filterResultsChk = JsonConvert.DeserializeObject<List<FilterResult>>(ftrs);
            //    filterResultsChk.ForEach(x=>x.Id = 0);
            //}

            //_matchBetService.AddRange(matchBetsChk);
            //_filterResultService.AddRange(filterResultsChk);

            #endregion
            //var mbRes = _matchBetService.AddRange(allMatchBets.ToList());
            //var frRes = _filterResultService.AddRange(allFilterResults.ToList());


            //var deletAbleMatchBets = _matchBetService.GetList(x => string.IsNullOrEmpty(x.HT_Match_Result) || x.FT_Match_Result.Contains("P")).Data;

            //var deletableFilterResults = _filterResultService.GetList(x => deletAbleMatchBets.Select(p => p.SerialUniqueID).Contains(x.SerialUniqueID)).Data;

            //var res1 = _matchBetService.RemoveRange(deletAbleMatchBets).Data;
            //var res2 = _filterResultService.RemoveRange(deletableFilterResults).Data;


            stp.Stop();

            var mSec = stp.ElapsedMilliseconds / 1000;

            return Ok(204);

            //var result = await _dataMaintenanceService.UpdateEmptyCountriesAsync();

            //if (result.Success)
            //{
            //    return Ok(result.Data);
            //}
            //else
            //{
            //    return Ok(JsonConvert.SerializeObject(result.Responses));
            //}
        }
    }
}
