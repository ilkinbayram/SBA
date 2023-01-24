using Core.Utilities.UsableModel.TempTableModels.Country;
using Core.Utilities.UsableModel.TempTableModels.Initialization;
using Microsoft.AspNetCore.Mvc;
using SBA.Business.Abstract;
using SBA.Business.BusinessHelper;
using System;
using System.Linq;

namespace SBA.MvcUI.Controllers
{
    public class BaseController : Controller
    {
        private readonly IMatchBetService _matchBetService;

        private CountryContainerTemp _containerTemp;
        private LeagueContainer _leagueContainer;

        public BaseController(IMatchBetService matchBetService)
        {
            _matchBetService = matchBetService;
        }

        protected CountryContainerTemp CountryContainer
        {
            get
            {
                if (this._containerTemp == null)
                {
                    var countries = _matchBetService
            .Query().Data
            .Where(x => x.Country != "NONE")
            .Select(x => x.Country)
            .Distinct()
            .OrderBy(x => x).ToList();

                    _containerTemp = new CountryContainerTemp
                    {
                        Countries = countries.Select(x => new CountryTemp { Name = x }).ToList()
                    };
                }

                return _containerTemp;
            }
        }

        protected LeagueContainer LeagueContainer
        {
            get
            {
                if (this._leagueContainer == null)
                {
                    var allMatches = _matchBetService
            .Query().Data
            .Where(x => x.Country != "NONE" && !x.LeagueName.Contains("kupa")).ToList();

                    var allHolders = allMatches.GroupBy(y => new { y.Country, y.LeagueName })
                                      .Select(x => new LeagueHolder
                                      {
                                          Country = x.Key.Country,
                                          League = x.Key.LeagueName
                                      }).ToList();

                    _leagueContainer = new LeagueContainer();

                    for (int i = 0; i < allHolders.Count; i++)
                    {
                        var holderOne = allHolders[i];

                        var holders = _matchBetService.GetList(p => p.Country == holderOne.Country && p.LeagueName == holderOne.League && p.MatchDate.Month == DateTime.Now.Month).Data;

                        if (holders.Count < 6) continue;

                        var holderAnalyse = holders.Select(x => new
                        {
                            IsGG = Convert.ToInt32(x.FT_Match_Result.Split("-")[0].Trim()) > 0 && Convert.ToInt32(x.FT_Match_Result.Split("-")[1].Trim()) > 0,
                            IsHomeFT05Over = Convert.ToInt32(x.FT_Match_Result.Split("-")[0].Trim()) > 0,
                            IsHomeHT05Over = Convert.ToInt32(x.HT_Match_Result.Split("-")[0].Trim()) > 0,
                            IsHomeSH05Over = (Convert.ToInt32(x.FT_Match_Result.Split("-")[0].Trim()) - Convert.ToInt32(x.HT_Match_Result.Split("-")[0].Trim())) > 0,
                            IsHomeFT15Over = Convert.ToInt32(x.FT_Match_Result.Split("-")[0].Trim()) > 1,
                            IsAwayFT05Over = Convert.ToInt32(x.FT_Match_Result.Split("-")[1].Trim()) > 0,
                            IsAwayHT05Over = Convert.ToInt32(x.HT_Match_Result.Split("-")[1].Trim()) > 0,
                            IsAwaySH05Over = (Convert.ToInt32(x.FT_Match_Result.Split("-")[1].Trim()) - Convert.ToInt32(x.HT_Match_Result.Split("-")[1].Trim())) > 0,
                            IsAwayFT15Over = Convert.ToInt32(x.FT_Match_Result.Split("-")[1].Trim()) > 1,
                            Is15Over = (Convert.ToInt32(x.FT_Match_Result.Split("-")[0].Trim()) + Convert.ToInt32(x.FT_Match_Result.Split("-")[1].Trim())) > 1,
                            Is25Over = (Convert.ToInt32(x.FT_Match_Result.Split("-")[0].Trim()) + Convert.ToInt32(x.FT_Match_Result.Split("-")[1].Trim())) > 2,
                            Is35Over = (Convert.ToInt32(x.FT_Match_Result.Split("-")[0].Trim()) + Convert.ToInt32(x.FT_Match_Result.Split("-")[1].Trim())) > 3,
                            IsHT15Over = (Convert.ToInt32(x.HT_Match_Result.Split("-")[0].Trim()) + Convert.ToInt32(x.HT_Match_Result.Split("-")[1].Trim())) > 1,
                            IsHT05Over = (Convert.ToInt32(x.HT_Match_Result.Split("-")[0].Trim()) + Convert.ToInt32(x.HT_Match_Result.Split("-")[1].Trim())) > 0,
                            IsSH05Over = (Convert.ToInt32(x.FT_Match_Result.Split("-")[0].Trim()) + Convert.ToInt32(x.FT_Match_Result.Split("-")[1].Trim())) - (Convert.ToInt32(x.HT_Match_Result.Split("-")[0].Trim()) + Convert.ToInt32(x.HT_Match_Result.Split("-")[1].Trim())) > 0,
                            IsSH15Over = ((Convert.ToInt32(x.FT_Match_Result.Split("-")[0].Trim()) + Convert.ToInt32(x.FT_Match_Result.Split("-")[1].Trim())) - (Convert.ToInt32(x.HT_Match_Result.Split("-")[0].Trim()) + Convert.ToInt32(x.HT_Match_Result.Split("-")[1].Trim()))) > 1,
                            GoalSum = Convert.ToInt32(x.FT_Match_Result.Split("-")[0].Trim()) + Convert.ToInt32(x.FT_Match_Result.Split("-")[1].Trim()),
                            HTGoalSum = Convert.ToInt32(x.HT_Match_Result.Split("-")[0].Trim()) + Convert.ToInt32(x.HT_Match_Result.Split("-")[1].Trim()),
                            SHGoalSum = (Convert.ToInt32(x.FT_Match_Result.Split("-")[0].Trim()) + Convert.ToInt32(x.FT_Match_Result.Split("-")[1].Trim())) - (Convert.ToInt32(x.HT_Match_Result.Split("-")[0].Trim()) + Convert.ToInt32(x.HT_Match_Result.Split("-")[1].Trim())),
                            Home_GoalSum = Convert.ToInt32(x.FT_Match_Result.Split("-")[0].Trim()),
                            Home_HTGoalSum = Convert.ToInt32(x.HT_Match_Result.Split("-")[0].Trim()),
                            Home_SHGoalSum = Convert.ToInt32(x.FT_Match_Result.Split("-")[0].Trim()) - Convert.ToInt32(x.HT_Match_Result.Split("-")[0].Trim()),
                            Away_GoalSum = Convert.ToInt32(x.FT_Match_Result.Split("-")[1].Trim()),
                            Away_HTGoalSum = Convert.ToInt32(x.HT_Match_Result.Split("-")[1].Trim()),
                            Away_SHGoalSum = Convert.ToInt32(x.FT_Match_Result.Split("-")[1].Trim()) - Convert.ToInt32(x.HT_Match_Result.Split("-")[1].Trim())
                        });

                        holderOne.GG_Percentage = holderAnalyse.Count(x => x.IsGG) * 100 / holderAnalyse.Count();
                        holderOne.SH_Over_1_5_Percentage = holderAnalyse.Count(x => x.IsSH15Over) * 100 / holderAnalyse.Count();
                        holderOne.HT_Over_1_5_Percentage = holderAnalyse.Count(x => x.IsHT15Over) * 100 / holderAnalyse.Count();
                        holderOne.HT_Over_0_5_Percentage = holderAnalyse.Count(x => x.IsHT05Over) * 100 / holderAnalyse.Count();
                        holderOne.SH_Over_0_5_Percentage = holderAnalyse.Count(x => x.IsSH05Over) * 100 / holderAnalyse.Count();
                        holderOne.Over_2_5_Percentage = holderAnalyse.Count(x => x.Is25Over) * 100 / holderAnalyse.Count();
                        holderOne.Over_3_5_Percentage = holderAnalyse.Count(x => x.Is35Over) * 100 / holderAnalyse.Count();
                        holderOne.GoalsAverage = (decimal) holderAnalyse.Sum(x=>x.GoalSum) / holderAnalyse.Count();
                        holderOne.CountFound = holderAnalyse.Count();
                        holderOne.Over_1_5_Percentage = holderAnalyse.Count(x => x.Is15Over) * 100 / holderAnalyse.Count();
                        holderOne.HT_GoalsAverage = (decimal)holderAnalyse.Sum(x => x.HTGoalSum) / holderAnalyse.Count();
                        holderOne.SH_GoalsAverage = (decimal)holderAnalyse.Sum(x => x.SHGoalSum) / holderAnalyse.Count();
                        holderOne.HomeFT_GoalsAverage = (decimal)holderAnalyse.Sum(x => x.Home_GoalSum) / holderAnalyse.Count();
                        holderOne.HomeFT_05_Over_Percentage = holderAnalyse.Count(x => x.IsHomeFT05Over) * 100 / holderAnalyse.Count();
                        holderOne.AwayFT_GoalsAverage = (decimal)holderAnalyse.Sum(x => x.Away_GoalSum) / holderAnalyse.Count();
                        holderOne.AwayFT_05_Over_Percentage = holderAnalyse.Count(x => x.IsAwayFT05Over) * 100 / holderAnalyse.Count();

                        holderOne.HomeHT_GoalsAverage = (decimal)holderAnalyse.Sum(x => x.Home_HTGoalSum) / holderAnalyse.Count();
                        holderOne.HomeHT_05_Over_Percentage = holderAnalyse.Count(x => x.IsHomeHT05Over) * 100 / holderAnalyse.Count();

                        holderOne.AwayHT_GoalsAverage = (decimal)holderAnalyse.Sum(x => x.Away_HTGoalSum) / holderAnalyse.Count();
                        holderOne.AwayHT_05_Over_Percentage = holderAnalyse.Count(x => x.IsAwayHT05Over) * 100 / holderAnalyse.Count();

                        _leagueContainer.LeagueHolders.Add(holderOne);
                    }
                }

                return _leagueContainer;
            }
        }
    }
}
