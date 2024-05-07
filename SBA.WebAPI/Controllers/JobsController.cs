using Core.Extensions;
using Core.Utilities.Helpers;
using Core.Utilities.UsableModel;
using DataAccess.Concrete.EntityFramework.Contexts;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using SBA.Business.Abstract;
using SBA.Business.ExternalServices.Abstract;
using SBA.Business.FunctionalServices.Concrete;
using SBA.ExternalDataAccess.Concrete.EntityFramework.Contexts;
using SBA.WebAPI.Utilities.Helpers;

namespace SBA.WebAPI.Controllers
{
    [Route("api/jobs")]
    [ApiController]
    [BasicAuthentication]
    public class JobsController : BaseWebApiController
    {
        private readonly IMatchBetService _matchBetService;
        private readonly IForecastService _forecastService;
        private readonly IMatchIdentifierService _matchIdentifierService;
        private readonly IBackgroundJobClient _backgroundJobs;
        private readonly ISocialBotMessagingService _telegramService;
        private readonly IConfiguration _configuration;
        private readonly string _txtPathFormat;

        public JobsController(IBackgroundJobClient backgroundJobs,
                              IMatchIdentifierService matchIdentifierService,
                              ISocialBotMessagingService socialBotMessagingService,
                              IMatchBetService matchBetService,
                              IConfiguration configuration,
                              IForecastService forecastService) : base(configuration)
        {
            _backgroundJobs = backgroundJobs;
            _matchIdentifierService = matchIdentifierService;
            _telegramService = socialBotMessagingService;
            _matchBetService = matchBetService;
            _configuration = configuration;

            _txtPathFormat = _configuration.GetSection("PathConstant").GetValue<string>("TextFormat");
            _forecastService = forecastService;
        }

        [HttpGet("write-job-forecast")]
        public IActionResult WriteJob()
        {
            List<int> serials = new List<int>();

            using (StreamReader srCacheReader = new StreamReader(_txtPathFormat.GetTextFileByFormat("FullOutOfScopeSerials")))
            {
                string line;
                while ((line = srCacheReader.ReadLine()) != null)
                {
                    serials.Add(Convert.ToInt32(line.Trim()));
                }
            }

            var shortInfoes = new List<ShortMatchInfo>();

            foreach (var serial in serials)
            {
                var shortMatchInfo = MatchInfoProceeder.ExtractShortMatchInfo(serial);

                shortInfoes.Add(shortMatchInfo);
            }

            return Ok("Job is already configured.");
        }


        [HttpGet("update-leagues")]
        public async Task<IActionResult> UpdateLeaguesAsync()
        {
            using (var context = new ApplicationDbContext())
            {
                var allMatches = context.MatchBets.Where(x => x.IsCountryLeagueUpdated == false).ToList();

                for (int i = 0; i < allMatches.Count; i++)
                {
                    var existingMatch = allMatches[i];
                    var updateModel = MatchInfoProceeder.ExtractLeagueUpdateModel(existingMatch.SerialUniqueID, Countries);

                    if (updateModel == null) continue;

                    existingMatch.LeagueId = updateModel.LeagueId;

                    if (!string.IsNullOrEmpty(updateModel.Country) && !string.IsNullOrEmpty(updateModel.CombinedLeague))
                    {
                        existingMatch.Country = updateModel.Country;
                        existingMatch.LeagueName = updateModel.CombinedLeague;
                        existingMatch.IsCountryLeagueUpdated = true;
                    }

                    context.MatchBets.Update(existingMatch);
                    context.SaveChanges();
                }
            }

            return Ok("Updated List 6.");
        }


        [HttpGet("start-job")]
        public IActionResult StartJob()
        {
            if (!HangfireBootstrap.IsJobConfigured)
            {
                HangfireBootstrap.IsJobConfigured = true;
                _backgroundJobs.Enqueue(() => TriggeredJob());
                return Ok("Job is configured and will run.");
            }
            return Ok("Job is already configured.");
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public void TriggeredJob()
        {
            try
            {
                TimeZoneInfo azerbaycanZone = TimeZoneInfo.FindSystemTimeZoneById("Azerbaijan Standard Time");
                DateTime azerbaycanTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, azerbaycanZone);

                var listIdentifiers = _matchIdentifierService.GetList(x => x.MatchDateTime >= azerbaycanTime).Data;

                foreach (var match in listIdentifiers)
                {
                    var timeUntilMatch = match.MatchDateTime.AddMinutes(-5) - azerbaycanTime;
                    if (timeUntilMatch.TotalSeconds > 0)
                    {
                        var info = _matchIdentifierService.SpGetMatchInformation(match.Serial);
                        var extraDetails = _matchIdentifierService.SP_GetTeamLeagueMixedStatResult(match.Serial);

                        var performanceOverall = _matchBetService.GetPerformanceOverallResult(match.Serial);

                        // string extraDetailsJoinContent = TemplateMessageHelper.PrepareStatistics(performanceOverall);


                        var jobForecast = new JobForecast
                        {
                            Country = info.CountryName,
                            League = info.LeagueName,
                            Match = $"{info.HomeTeam}  ~vs~  {info.AwayTeam}",
                            ExtraDetails = string.Empty,
                            MatchIdentifier = match,
                            Link = $"https://arsiv.mackolik.com/Match/Default.aspx?id={match.Serial}"
                        };

                        _backgroundJobs.Schedule(() => SendMatchNotification(match.Serial, jobForecast), timeUntilMatch);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public void SendMatchNotification(int serial, JobForecast jobForecast)
        {
            var statInfoHolders = _matchBetService.GetOddFilteredResult(serial, (decimal)0.05).OrderBy(x => x.Order).ToList();

            jobForecast.IsFT_15Over = ForecastChecker.Check_FT_15_Over(statInfoHolders, 10, 90); //=>    94 %
            jobForecast.IsFT_25Over = ForecastChecker.Check_FT_25_Over(statInfoHolders, 10, 80); //=>    83 %
            jobForecast.IsFT_GG = ForecastChecker.Check_GG(statInfoHolders, 10, 80); //=>                83 %
            jobForecast.IsFT_NG = ForecastChecker.Check_NG(statInfoHolders, 10, 20); //=>                80 %
            jobForecast.IsFT_35Under = ForecastChecker.Check_FT_35_Under(statInfoHolders, 10, 20); //=>  90 %
            jobForecast.IsHT_05Over = ForecastChecker.Check_HT_05_Over(statInfoHolders, 10, 85); //=>    91 %
            jobForecast.IsSH_05Over = ForecastChecker.Check_SH_05_Over(statInfoHolders, 10, 90); //=>    92 %
            jobForecast.IsHT_15Under = ForecastChecker.Check_HT_15_Under(statInfoHolders, 10, 15); //=>  93 %
            jobForecast.IsSH_15Under = ForecastChecker.Check_SH_15_Under(statInfoHolders, 10, 10); //=>  90 %

            jobForecast.IsHome_FT_05_Over = ForecastChecker.Check_Home_FT_05_Over(statInfoHolders, 10, 90); //=>  94 %
            jobForecast.IsHome_FT_15_Over = ForecastChecker.Check_Home_FT_15_Over(statInfoHolders, 10, 80); //=>  88 %
            jobForecast.IsHome_HT_05_Over = ForecastChecker.Check_Home_HT_05_Over(statInfoHolders, 10, 85); //=>  89 %
            jobForecast.IsHome_SH_05_Over = ForecastChecker.Check_Home_SH_05_Over(statInfoHolders, 10, 85); //=>  93 %
            jobForecast.IsAway_FT_05_Over = ForecastChecker.Check_Away_FT_05_Over(statInfoHolders, 10, 90); //=>  92 %
            jobForecast.IsAway_FT_15_Over = ForecastChecker.Check_Away_FT_15_Over(statInfoHolders, 10, 85); //=>  87 %
            jobForecast.IsAway_HT_05_Over = ForecastChecker.Check_Away_HT_05_Over(statInfoHolders, 10, 85); //=>  89 %
            jobForecast.IsAway_SH_05_Over = ForecastChecker.Check_Away_SH_05_Over(statInfoHolders, 10, 85); //=>  95 %

            jobForecast.IsHome_FT_Win = ForecastChecker.Check_Home_FT_Win(statInfoHolders, 10, 85); //=>  91 %
            jobForecast.IsAway_FT_Win = ForecastChecker.Check_Away_FT_Win(statInfoHolders, 10, 85); //=>  91 %
            jobForecast.Is_HT_Draw = ForecastChecker.Check_HT_Draw(statInfoHolders, 10, 80); //=>  78 %
            jobForecast.Is_SH_Draw = ForecastChecker.Check_SH_Draw(statInfoHolders, 10, 75); //=>  85 %

            jobForecast.Is_FT_Win1_Or_X = ForecastChecker.Check_FT_Win1_Or_Draw(statInfoHolders, 10, 92); //=>  93 %
            jobForecast.Is_HT_Win1_Or_X = ForecastChecker.Check_HT_Win1_Or_Draw(statInfoHolders, 10, 92); //=>  93 %
            jobForecast.Is_SH_Win1_Or_X = ForecastChecker.Check_SH_Win1_Or_Draw(statInfoHolders, 10, 92); //=>  93 %

            jobForecast.Is_FT_X_Or_Win2 = ForecastChecker.Check_FT_Draw_Or_Win2(statInfoHolders, 10, 90); //=>  91 %
            jobForecast.Is_HT_X_Or_Win2 = ForecastChecker.Check_HT_Draw_Or_Win2(statInfoHolders, 10, 90); //=>  91 %
            jobForecast.Is_SH_X_Or_Win2 = ForecastChecker.Check_SH_Draw_Or_Win2(statInfoHolders, 10, 90); //=>  91 %

            jobForecast.Is_FT_Win1_Or_Win2 = ForecastChecker.Check_FT_Win1_Or_Win2(statInfoHolders, 10, 90); //=>  92 %
            jobForecast.Is_HT_Win1_Or_Win2 = ForecastChecker.Check_HT_Win1_Or_Win2(statInfoHolders, 10, 85); //=>  92 %
            jobForecast.Is_SH_Win1_Or_Win2 = ForecastChecker.Check_SH_Win1_Or_Win2(statInfoHolders, 10, 90); //=>  85 %



            if (jobForecast.IsFT_15Over.IsAcceptable || jobForecast.IsFT_25Over.IsAcceptable || jobForecast.IsFT_GG.IsAcceptable || jobForecast.IsFT_NG.IsAcceptable || jobForecast.IsFT_35Under.IsAcceptable || jobForecast.IsHT_05Over.IsAcceptable || jobForecast.IsSH_05Over.IsAcceptable || jobForecast.IsHT_15Under.IsAcceptable || jobForecast.IsSH_15Under.IsAcceptable || jobForecast.IsHome_FT_05_Over.IsAcceptable || jobForecast.IsHome_FT_15_Over.IsAcceptable || jobForecast.IsHome_HT_05_Over.IsAcceptable || jobForecast.IsHome_SH_05_Over.IsAcceptable || jobForecast.IsHome_FT_Win.IsAcceptable || jobForecast.IsHome_HT_Win.IsAcceptable || jobForecast.IsHome_SH_Win.IsAcceptable || jobForecast.IsAway_FT_05_Over.IsAcceptable || jobForecast.IsAway_FT_15_Over.IsAcceptable || jobForecast.IsAway_HT_05_Over.IsAcceptable || jobForecast.IsAway_SH_05_Over.IsAcceptable || jobForecast.IsAway_FT_Win.IsAcceptable || jobForecast.IsAway_HT_Win.IsAcceptable || jobForecast.IsAway_SH_Win.IsAcceptable || jobForecast.Is_FT_Draw.IsAcceptable || jobForecast.Is_HT_Draw.IsAcceptable || jobForecast.Is_SH_Draw.IsAcceptable || jobForecast.Is_FT_Win1_Or_X.IsAcceptable || jobForecast.Is_HT_Win1_Or_X.IsAcceptable || jobForecast.Is_SH_Win1_Or_X.IsAcceptable || jobForecast.Is_FT_X_Or_Win2.IsAcceptable || jobForecast.Is_HT_X_Or_Win2.IsAcceptable || jobForecast.Is_SH_X_Or_Win2.IsAcceptable || jobForecast.Is_FT_Win1_Or_Win2.IsAcceptable || jobForecast.Is_HT_Win1_Or_Win2.IsAcceptable || jobForecast.Is_SH_Win1_Or_Win2.IsAcceptable)
            {
                var dbForecastList = ForecastChecker.PrepareForecastList(jobForecast, serial);

                if (dbForecastList.Count > 0)
                {
                    using (var context = new ExternalAppDbContext())
                    {
                        var keys = dbForecastList.Select(x=>x.Key.ToLower()).ToList();

                        var matchedForecasts = context.Forecasts.Where(x=> keys.Contains(x.Key.ToLower()) && x.Serial == serial).ToList();

                        matchedForecasts.ForEach(x => x.Is99Percent = true);

                        if (matchedForecasts.Count > 0)
                        {
                            var forecastContent = ForecastChecker.JoinForecast(jobForecast, matchedForecasts);

                            var messageContent = $"Maç:  {jobForecast.Match}\r\nLig: {jobForecast.Country}/{jobForecast.League}\r\nLink: {jobForecast.Link}\r\n\r\n------------------PROQNOZLAR-------------------\r\n\r\n{forecastContent}\r\n{jobForecast.ExtraDetails}";
                            _telegramService.SendMessage(messageContent);

                            context.SaveChanges();
                        }
                    }
                }
            }

            Console.WriteLine($"Notification sent for match {serial} at: {DateTime.Now}");
        }


        #region FullOutOfScope
        [HttpGet("full-out-of-scope-start-job")]
        public IActionResult StartFullOutOfScopeJob()
        {
            if (!HangfireBootstrap.IsFullOutOfScopeJobConfigured)
            {
                HangfireBootstrap.IsFullOutOfScopeJobConfigured = true;

                TimeZoneInfo azerbaycanZone = TimeZoneInfo.FindSystemTimeZoneById("Azerbaijan Standard Time");
                DateTime azerbaycanTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, azerbaycanZone);

                List<int> serials = new List<int>();

                using (StreamReader srCacheReader = new StreamReader(_txtPathFormat.GetTextFileByFormat("FullOutOfScopeSerials")))
                {
                    string line;
                    while ((line = srCacheReader.ReadLine()) != null)
                    {
                        serials.Add(Convert.ToInt32(line.Trim()));
                    }
                }

                var jbls = new List<JobForecast>();

                foreach (var serial in serials)
                {
                    var shortMatchInfo = MatchInfoProceeder.ExtractShortMatchInfo(serial);

                    var jobForecast = new JobForecast
                    {
                        Country = shortMatchInfo.Country,
                        League = shortMatchInfo.League,
                        Match = shortMatchInfo.Match,
                        Link = shortMatchInfo.Link,
                    };

                    jbls.Add(jobForecast);
                }

                // _backgroundJobs.Enqueue(() => TriggeredFullOutOfScopeJob());
                return Ok("Full-Out-Of-Scope-Job is configured and will run.");
            }
            return Ok("Full-Out-Of-Scope-Job is already configured.");
        }


        [ApiExplorerSettings(IgnoreApi = true)]
        public void TriggeredFullOutOfScopeJob()
        {
            try
            {
                TimeZoneInfo azerbaycanZone = TimeZoneInfo.FindSystemTimeZoneById("Azerbaijan Standard Time");
                DateTime azerbaycanTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, azerbaycanZone);

                List<int> serials = new List<int>();

                using (StreamReader srCacheReader = new StreamReader(_txtPathFormat.GetTextFileByFormat("FullOutOfScopeSerials")))
                {
                    string line;
                    while ((line = srCacheReader.ReadLine()) != null)
                    {
                        serials.Add(Convert.ToInt32(line));
                    }
                }

                foreach (var serial in serials)
                {
                    var shortMatchInfo = MatchInfoProceeder.ExtractShortMatchInfo(serial);

                    TimeZoneInfo turkeyZone = TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time");
                    TimeZoneInfo azerbaijanZone = TimeZoneInfo.FindSystemTimeZoneById("Azerbaijan Standard Time");

                    DateTime utcTime = TimeZoneInfo.ConvertTimeToUtc(shortMatchInfo.MatchDateTime, turkeyZone);

                    DateTime convertedMatchTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, azerbaijanZone);

                    var timeUntilMatch = convertedMatchTime.AddMinutes(-5) - azerbaycanTime;
                    if (timeUntilMatch.TotalSeconds > 0)
                    {
                        var jobForecast = new JobForecast
                        {
                            Country = shortMatchInfo.Country,
                            League = shortMatchInfo.League,
                            Match = shortMatchInfo.Match,
                            Link = shortMatchInfo.Link
                        };
                        _backgroundJobs.Schedule(() => SendMatchNotificationForFullOutOfScope(shortMatchInfo.Serial, jobForecast), timeUntilMatch);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public void SendMatchNotificationForFullOutOfScope(int serial, JobForecast jobForecast)
        {
            var statInfoHolders = _matchBetService.GetOddFilteredResult(serial, (decimal)0.05).OrderBy(x => x.Order).ToList();

            jobForecast.IsFT_15Over = ForecastChecker.Check_FT_15_Over(statInfoHolders, 10, 85); //=>    92 %
            jobForecast.IsFT_25Over = ForecastChecker.Check_FT_25_Over(statInfoHolders, 10, 80); //=>    83 %
            jobForecast.IsFT_GG = ForecastChecker.Check_GG(statInfoHolders, 10, 80); //=>                83 %
            jobForecast.IsFT_NG = ForecastChecker.Check_NG(statInfoHolders, 10, 20); //=>                80 %
            jobForecast.IsFT_35Under = ForecastChecker.Check_FT_35_Under(statInfoHolders, 10, 20); //=>  89 %
            jobForecast.IsHT_05Over = ForecastChecker.Check_HT_05_Over(statInfoHolders, 10, 85); //=>    91 %
            jobForecast.IsSH_05Over = ForecastChecker.Check_SH_05_Over(statInfoHolders, 10, 90); //=>    92 %
            jobForecast.IsHT_15Under = ForecastChecker.Check_HT_15_Under(statInfoHolders, 10, 15); //=>  93 %
            jobForecast.IsSH_15Under = ForecastChecker.Check_SH_15_Under(statInfoHolders, 10, 15); //=>  90 %

            if (jobForecast.IsFT_15Over.IsAcceptable || jobForecast.IsFT_25Over.IsAcceptable || jobForecast.IsFT_GG.IsAcceptable || jobForecast.IsFT_NG.IsAcceptable || jobForecast.IsFT_35Under.IsAcceptable || jobForecast.IsHT_05Over.IsAcceptable || jobForecast.IsSH_05Over.IsAcceptable || jobForecast.IsHT_15Under.IsAcceptable || jobForecast.IsSH_15Under.IsAcceptable)
            {
                var forecastContent = ForecastChecker.JoinForecast(jobForecast);
                var messageContent = $"Match:  {jobForecast.Match}\nLig: {jobForecast.Country}/{jobForecast.League}\nLink: {jobForecast.Link}\n--------------------------------\n{forecastContent}";
                _telegramService.SendRiskerMessage(messageContent);
            }

            Console.WriteLine($"Notification sent for match {serial} at: {DateTime.Now}");
        }
        #endregion
    }
}
