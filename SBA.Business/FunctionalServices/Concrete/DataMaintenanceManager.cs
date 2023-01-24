using Business.Constants;
using Core.Entities.Concrete;
using Core.Resources.Constants;
using Core.Resources.Enums;
using Core.Utilities.Helpers;
using Core.Utilities.Results;
using Core.Utilities.UsableModel;
using Core.Utilities.UsableModel.TempTableModels.Country;
using SBA.Business.BusinessHelper;
using SBA.Business.FunctionalServices.Abstract;
using SBA.DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebMarkupMin.Core;

namespace SBA.Business.FunctionalServices.Concrete
{
    public class DataMaintenanceManager : IDataMaintenanceService
    {
        private readonly IMatchBetDal _matchBetDal;
        private readonly IFilterResultDal _filterResultDal;

        private readonly HtmlMinifier _minifier = new HtmlMinifier();
        private readonly HttpClient _client = new HttpClient();


        private readonly QuickConvert _quickConvert = new QuickConvert();

        private readonly string _defaultMatchUrl = ConfigHelper.GetSettingsDataStatic<string>(ParentKeySettings.UriTemplate.ToString(), ChildKeySettings.ConcatSerialDefaultMatch.ToString());

        private readonly string _plusMatchUrl = ConfigHelper.GetSettingsDataStatic<string>(ParentKeySettings.UriTemplate.ToString(), ChildKeySettings.ConcatCallbackAsResultMatch.ToString());

        private readonly string _comparisonMatchUrl = ConfigHelper.GetSettingsDataStatic<string>(ParentKeySettings.UriTemplate.ToString(), ChildKeySettings.ConcatComparisonMatch.ToString());

        public DataMaintenanceManager(IMatchBetDal matchBetDal,
                                          IFilterResultDal filterResultDal)
        {
            _matchBetDal = matchBetDal;
            _filterResultDal = filterResultDal;
        }

        public IDataResult<int> DeleteNonMatchedDataes()
        {
            try
            {
                IDataResult<int> dataResult;


                var allMatchBets = _matchBetDal.GetList();
                var allFilterResults = _filterResultDal.GetList();

                var listOfDeletableMatchbets = new List<MatchBet>();
                var listOfDeletableFilterResult = new List<FilterResult>();

                foreach (var matchBet in allMatchBets)
                {
                    if(!allFilterResults.Any(x=>x.SerialUniqueID == matchBet.SerialUniqueID))
                    {
                        listOfDeletableMatchbets.Add(matchBet);
                    }
                }

                foreach (var filterRes in allFilterResults)
                {
                    if (!allMatchBets.Any(x => x.SerialUniqueID == filterRes.SerialUniqueID))
                    {
                        listOfDeletableFilterResult.Add(filterRes);
                    }
                }

                int affectedRows1 = _filterResultDal.RemoveRange(listOfDeletableFilterResult);
                int affectedRows2 = _matchBetDal.RemoveRange(listOfDeletableMatchbets);

                if (affectedRows1 > 0 && affectedRows2 > 0)
                {
                    dataResult = new SuccessDataResult<int>(affectedRows1, Messages.BusinessDataUpdated);
                }
                else
                {
                    dataResult = new ErrorDataResult<int>(-1, false, Messages.BusinessDataWasNotUpdated);
                }

                return dataResult;
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<int>(-1, true, $"Exception Message: {ex.Message} \nInner Exception: {ex.InnerException}");
            }
        }

        public IDataResult<int> InitializeNewColumnsOnFilterResult()
        {
            try
            {
                IDataResult<int> dataResult;


                var allMatchBetSerials = _matchBetDal.GetList().Select(x=>new TempMatchBetHandler
                {
                    SerialUniqueID = x.SerialUniqueID,
                    FT_AwayTeam_Goals = Convert.ToInt32(x.FT_Match_Result.Split('-')[1].Trim()),
                    FT_HomeTeam_Goals = Convert.ToInt32(x.FT_Match_Result.Split('-')[0].Trim()),
                    HT_AwayTeam_Goals = Convert.ToInt32(x.HT_Match_Result.Split('-')[1].Trim()),
                    HT_HomeTeam_Goals = Convert.ToInt32(x.HT_Match_Result.Split('-')[0].Trim())
                });

                var allFilterResults = _filterResultDal.GetList();

                foreach (var filterRes in allFilterResults)
                {
                    var foundMatch = allMatchBetSerials.FirstOrDefault(x => x.SerialUniqueID == filterRes.SerialUniqueID);

                    filterRes.Away_HT_0_5_Over = foundMatch.HT_AwayTeam_Goals > 0;
                    filterRes.Away_HT_1_5_Over = foundMatch.HT_AwayTeam_Goals > 1;
                    filterRes.Home_HT_0_5_Over = foundMatch.HT_HomeTeam_Goals > 0;
                    filterRes.Home_HT_1_5_Over = foundMatch.HT_HomeTeam_Goals > 1;

                    filterRes.Away_SH_0_5_Over = (foundMatch.FT_AwayTeam_Goals - foundMatch.HT_AwayTeam_Goals) > 0;
                    filterRes.Away_SH_1_5_Over = (foundMatch.FT_AwayTeam_Goals - foundMatch.HT_AwayTeam_Goals) > 1;
                    filterRes.Home_SH_0_5_Over = (foundMatch.FT_HomeTeam_Goals - foundMatch.HT_HomeTeam_Goals) > 0;
                    filterRes.Home_SH_1_5_Over = (foundMatch.FT_HomeTeam_Goals - foundMatch.HT_HomeTeam_Goals) > 1;

                    filterRes.Home_Win_Any_Half = foundMatch.HT_HomeTeam_Goals > foundMatch.HT_AwayTeam_Goals || (foundMatch.FT_HomeTeam_Goals - foundMatch.HT_HomeTeam_Goals) > (foundMatch.FT_AwayTeam_Goals - foundMatch.HT_AwayTeam_Goals);

                    filterRes.Away_Win_Any_Half = foundMatch.HT_AwayTeam_Goals > foundMatch.HT_HomeTeam_Goals || (foundMatch.FT_AwayTeam_Goals - foundMatch.HT_AwayTeam_Goals) > (foundMatch.FT_HomeTeam_Goals - foundMatch.HT_HomeTeam_Goals);
                }

                int affectedRows = _filterResultDal.UpdateRange(allFilterResults);

                if (affectedRows > 0)
                {
                    dataResult = new SuccessDataResult<int>(affectedRows, Messages.BusinessDataUpdated);
                }
                else
                {
                    dataResult = new ErrorDataResult<int>(-1, false, Messages.BusinessDataWasNotUpdated);
                }

                return dataResult;
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<int>(-1, true, $"Exception Message: {ex.Message} \nInner Exception: {ex.InnerException}");
            }
        }

        public async Task<IDataResult<int>> UpdateEmptyCountriesAsync(CountryContainerTemp containerTemp)
        {
            IDataResult<int> dataResult;
            try
            {
                var allNoneMatchBets = _matchBetDal
                    .Query()
                    .Where(x => x.Country == "NONE" || x.LeagueName == "NONE").ToList();

                var regexCombine = new Regex(PatternConstant.StartedMatchPattern.CountryAndLeague);

                foreach (var bet in allNoneMatchBets)
                {
                    try
                    {
                        object obj = new object();
                        await Task.Delay(300);

                        lock (obj)
                        {
                            string concatUri = String.Format("{0}{1}", _defaultMatchUrl, bet.SerialUniqueID);
                            var response = _client.GetStringAsync(concatUri).Result;
                            var minifiedResult = _minifier.Minify(response).MinifiedContent;

                            var countryName = string.Empty;
                            var leagueName = string.Empty;

                            if (regexCombine.IsMatch(minifiedResult))
                            {
                                countryName = OperationalProcessor.GetCountryName(regexCombine.Matches(minifiedResult)[0].Groups[1].Value.Trim(), containerTemp);
                                leagueName = OperationalProcessor.GetLeagueName(regexCombine.Matches(minifiedResult)[0].Groups[1].Value.Trim(), containerTemp);
                            }

                            if (string.IsNullOrEmpty(countryName) || string.IsNullOrEmpty(leagueName))
                            {
                                continue;
                            }

                            bet.Country = countryName;
                            bet.LeagueName = leagueName;
                        }
                    }
                    catch (Exception)
                    {
                        continue;
                    }

                }

                var affectedRows = _matchBetDal.UpdateRange(allNoneMatchBets);

                if (affectedRows > 0)
                {
                    dataResult = new SuccessDataResult<int>(affectedRows, Messages.BusinessDataUpdated);
                }
                else
                {
                    dataResult = new ErrorDataResult<int>(-1, false, Messages.BusinessDataWasNotUpdated);
                }


                return dataResult;
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<int>(-1, true, $"Exception Message: {ex.Message} \nInner Exception: {ex.InnerException}");
            }
        }
    }
}
