using Core.Extensions;
using Core.Utilities.UsableModel;
using Core.Utilities.UsableModel.TempTableModels.Country;
using Core.Utilities.UsableModel.TempTableModels.Initialization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SBA.Business.Abstract;

namespace SBA.MvcUI.Controllers
{
    public class BaseController : Controller
    {
        private readonly IMatchBetService _matchBetService;
        protected readonly IConfiguration Configuration;

        private CountryContainerTemp _containerTemp;
        private LeagueContainer _leagueContainer;

        protected readonly string txtPathFormat;
        protected readonly string jsonPathFormat;

        public BaseController(IMatchBetService matchBetService, IConfiguration configuration)
        {
            _matchBetService = matchBetService;
            Configuration = configuration;
            txtPathFormat = Configuration.GetSection("PathConstant").GetValue<string>("TextPathFormat");
            jsonPathFormat = Configuration.GetSection("PathConstant").GetValue<string>("JsonPathFormat");
        }

        protected CountryContainerTemp CountryContainer
        {
            get
            {
                if (this._containerTemp == null)
                {
                    string contentCountries = string.Empty;
                    using (var sr = new StreamReader(jsonPathFormat.GetJsonFileByFormat("Countries")))
                    {
                        contentCountries = sr.ReadToEnd();
                    }

                    if (contentCountries.Length > 50)
                    {
                        _containerTemp = JsonConvert.DeserializeObject<CountryContainerTemp>(contentCountries);

                        if (_containerTemp != null && _containerTemp.Countries != null && _containerTemp.Countries.Count > 10)
                        {
                            return _containerTemp;
                        }
                    }

                    var countries = _matchBetService
                                    .Query().Data
                                    .Select(x => x.Country)
                                    .Distinct()
                                    .Select(country => new CountryOrdership
                                    {
                                        Country = country,
                                        Count = country.Length
                                    })
                                    .OrderByDescending(x => x.Count)
                                    .ThenBy(x => x.Country)
                                    .ToList();

                    _containerTemp = new CountryContainerTemp
                    {
                        Countries = countries.Select(x => new CountryTemp { Name = x.Country }).ToList()
                    };


                    using (var writer = new StreamWriter(jsonPathFormat.GetJsonFileByFormat("Countries")))
                    {
                        writer.Write(JsonConvert.SerializeObject(_containerTemp, Formatting.Indented));
                    }
                }

                return _containerTemp;
            }
        }

        protected LeagueContainer LeagueContainer
        {
            get
            {
                string dateDefinitionKey = Configuration.GetValue<string>("date_league_key");
                string dateValueNow = DateTime.Now.ToString("dd.MM.yyyy");
                string cookieExistValue = Request.Cookies[dateDefinitionKey];
                if (cookieExistValue == dateValueNow)
                {
                    string contentLeagues;
                    using (var sr = new StreamReader(jsonPathFormat.GetJsonFileByFormat("Leagues")))
                    {
                        contentLeagues = sr.ReadToEnd();
                    }

                    if (contentLeagues.Length > 50)
                    {
                        var leagues = JsonConvert.DeserializeObject<LeagueContainer>(contentLeagues);
                        return leagues;
                    }
                }

                Response.Cookies.Delete(dateDefinitionKey);

                if (this._leagueContainer == null)
                {
                    var allMatches = _matchBetService
            .Query().Data.ToList();

                    var allHolders = allMatches.GroupBy(y => new { y.Country, y.LeagueName })
                                      .Select(x => new LeagueHolder
                                      {
                                          Country = x.Key.Country,
                                          League = x.Key.LeagueName,
                                          LeagueCountryIds = x.Select(m => m.LeagueId).Distinct().ToList()
                                      }).ToList();

                    _leagueContainer = new LeagueContainer();

                    for (int i = 0; i < allHolders.Count; i++)
                    {
                        var holderOne = allHolders[i];

                        var holders = _matchBetService.GetMatchBetFilterResultQueryModels(p => p.Country == holderOne.Country && p.League == holderOne.League && p.MatchDate.Month == DateTime.Now.Month).Data;

                        if (holders.Count < 20) continue;

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
                            Away_SHGoalSum = Convert.ToInt32(x.FT_Match_Result.Split("-")[1].Trim()) - Convert.ToInt32(x.HT_Match_Result.Split("-")[1].Trim()),

                            Corner_Over_7_5 = (x.HomeCornersCount + x.AwayCornersCount) > 7,
                            Corner_Over_8_5 = (x.HomeCornersCount + x.AwayCornersCount) > 8,
                            Corner_Over_9_5 = (x.HomeCornersCount + x.AwayCornersCount) > 9,
                            CornerSum = x.HomeCornersCount + x.AwayCornersCount,
                            HomeCornerSum = x.HomeCornersCount,
                            AwayCornerSum = x.AwayCornersCount,
                            Home_Corner_35_Over = x.HomeCornersCount > 3,
                            Home_Corner_45_Over = x.HomeCornersCount > 4,
                            Home_Corner_55_Over = x.HomeCornersCount > 5,
                            Away_Corner_35_Over = x.AwayCornersCount > 3,
                            Away_Corner_45_Over = x.AwayCornersCount > 4,
                            Away_Corner_55_Over = x.AwayCornersCount > 5,
                            Corner_FT_Win1 = x.HomeCornersCount > x.AwayCornersCount,
                            Corner_FT_X = x.HomeCornersCount == x.AwayCornersCount,
                            Corner_FT_Win2 = x.HomeCornersCount < x.AwayCornersCount
                        });

                        holderOne.GG_Percentage = holderAnalyse.Count(x => x.IsGG) * 100 / holderAnalyse.Count();
                        holderOne.SH_Over_1_5_Percentage = holderAnalyse.Count(x => x.IsSH15Over) * 100 / holderAnalyse.Count();
                        holderOne.HT_Over_1_5_Percentage = holderAnalyse.Count(x => x.IsHT15Over) * 100 / holderAnalyse.Count();
                        holderOne.HT_Over_0_5_Percentage = holderAnalyse.Count(x => x.IsHT05Over) * 100 / holderAnalyse.Count();
                        holderOne.SH_Over_0_5_Percentage = holderAnalyse.Count(x => x.IsSH05Over) * 100 / holderAnalyse.Count();
                        holderOne.Over_2_5_Percentage = holderAnalyse.Count(x => x.Is25Over) * 100 / holderAnalyse.Count();
                        holderOne.Over_3_5_Percentage = holderAnalyse.Count(x => x.Is35Over) * 100 / holderAnalyse.Count();
                        holderOne.GoalsAverage = (decimal)holderAnalyse.Sum(x => x.GoalSum) / holderAnalyse.Count();
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
                        holderOne.Corner_Over_7_5_Percentage = holderAnalyse.Count(x=>x.Corner_Over_7_5) * 100 / holderAnalyse.Count();
                        holderOne.Corner_Over_8_5_Percentage = holderAnalyse.Count(x => x.Corner_Over_8_5) * 100 / holderAnalyse.Count();
                        holderOne.Corner_Over_9_5_Percentage = holderAnalyse.Count(x => x.Corner_Over_9_5) * 100 / holderAnalyse.Count();
                        holderOne.CornerAverage = (decimal)holderAnalyse.Sum(x => x.CornerSum) / holderAnalyse.Count();
                        holderOne.HomeCornerAverage = (decimal)holderAnalyse.Sum(x => x.HomeCornerSum) / holderAnalyse.Count();
                        holderOne.AwayCornerAverage = (decimal)holderAnalyse.Sum(x => x.AwayCornerSum) / holderAnalyse.Count();
                        holderOne.Home_Corner_35_Over_Percentage = holderAnalyse.Count(x => x.Home_Corner_35_Over) * 100 / holderAnalyse.Count();
                        holderOne.Home_Corner_45_Over_Percentage = holderAnalyse.Count(x => x.Home_Corner_45_Over) * 100 / holderAnalyse.Count();
                        holderOne.Home_Corner_55_Over_Percentage = holderAnalyse.Count(x => x.Home_Corner_55_Over) * 100 / holderAnalyse.Count();
                        holderOne.Away_Corner_35_Over_Percentage = holderAnalyse.Count(x => x.Away_Corner_35_Over) * 100 / holderAnalyse.Count();
                        holderOne.Away_Corner_45_Over_Percentage = holderAnalyse.Count(x => x.Away_Corner_45_Over) * 100 / holderAnalyse.Count();
                        holderOne.Away_Corner_55_Over_Percentage = holderAnalyse.Count(x => x.Away_Corner_55_Over) * 100 / holderAnalyse.Count();
                        holderOne.Corner_FT_Win1_Percentage = holderAnalyse.Count(x => x.Corner_FT_Win1) * 100 / holderAnalyse.Count();
                        holderOne.Corner_FT_X_Percentage = holderAnalyse.Count(x => x.Corner_FT_X) * 100 / holderAnalyse.Count();
                        holderOne.Corner_FT_Win2_Percentage = holderAnalyse.Count(x => x.Corner_FT_Win2) * 100 / holderAnalyse.Count();

                        _leagueContainer.LeagueHolders.Add(holderOne);
                    }
                }

                using (var writer = new StreamWriter(jsonPathFormat.GetJsonFileByFormat("Leagues")))
                {
                    writer.Write(JsonConvert.SerializeObject(_leagueContainer, Formatting.Indented));
                }

                var options = new CookieOptions();
                options.Expires = DateTime.Now.AddDays(7);
                Response.Cookies.Append(dateDefinitionKey, dateValueNow, options);

                return _leagueContainer;
            }
        }
    }
}
