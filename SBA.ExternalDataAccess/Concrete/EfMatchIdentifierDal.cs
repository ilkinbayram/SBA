using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete.ComplexModels.Program;
using Core.Entities.Concrete.ExternalDbEntities;
using Microsoft.EntityFrameworkCore;
using SBA.ExternalDataAccess.Abstract;
using SBA.ExternalDataAccess.Concrete.EntityFramework.Contexts;

namespace SBA.ExternalDataAccess.Concrete
{
    public class EfMatchIdentifierDal : EfEntityRepositoryBase<MatchIdentifier, ExternalAppDbContext>, IMatchIdentifierDal
    {
        public EfMatchIdentifierDal(ExternalAppDbContext applicationContext) : base(applicationContext)
        {
        }

        public MatchProgramList GetGroupedMatchsProgram()
        {
            TimeZoneInfo azerbaycanZone = TimeZoneInfo.FindSystemTimeZoneById("Azerbaijan Standard Time");
            DateTime azerbaycanTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, azerbaycanZone);
            var result = (from mid in Context.MatchIdentifiers
                          join cmpHA in Context.ComparisonStatisticsHolders on mid.Id equals cmpHA.MatchIdentifierId
                          where cmpHA.BySideType == 1 && mid.MatchDateTime.Date == azerbaycanTime.Date
                          join lg in Context.LeagueStatisticsHolders on cmpHA.LeagueStaisticsHolderId equals lg.Id
                          group mid by new { lg.CountryName, lg.LeagueName } into matchGroup
                          select new MatchProgram
                          {
                              Country = matchGroup.Key.CountryName,
                              League = matchGroup.Key.LeagueName,
                              Matches = matchGroup.Select(m => new Match
                              {
                                  Serial = m.Serial,
                                  HomeTeam = m.HomeTeam,
                                  AwayTeam = m.AwayTeam,
                                  MatchTime = m.MatchDateTime.ToString("HH:mm")
                              }).ToList()
                          }).ToList();

            return new MatchProgramList { Matches = result };
        }

        public MatchDetailProgram GetAllMatchsProgram() 
        {
            TimeZoneInfo azerbaycanZone = TimeZoneInfo.FindSystemTimeZoneById("Azerbaijan Standard Time");
            DateTime azerbaycanTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, azerbaycanZone);
            var result = (from mid in Context.MatchIdentifiers
                          join cmpHA in Context.ComparisonStatisticsHolders on mid.Id equals cmpHA.MatchIdentifierId
                          where cmpHA.BySideType == 1 && mid.MatchDateTime.Date == azerbaycanTime.Date
                          join lg in Context.LeagueStatisticsHolders on cmpHA.LeagueStaisticsHolderId equals lg.Id
                          select new MatchDetail
                          {
                              Country = lg.CountryName,
                              League = lg.LeagueName,
                              HomeTeam = mid.HomeTeam,
                              AwayTeam = mid.AwayTeam,
                              MatchTime = mid.MatchDateTime.ToString("HH:mm"),
                              Serial = mid.Serial
                          }).ToList();

            return new MatchDetailProgram { Matches = result };
        }

        public async Task<MatchProgramList> GetGroupedMatchsProgramAsync()
        {
            TimeZoneInfo azerbaycanZone = TimeZoneInfo.FindSystemTimeZoneById("Azerbaijan Standard Time");
            DateTime azerbaycanTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, azerbaycanZone);

            var result = await (from mid in Context.MatchIdentifiers
                          join cmpHA in Context.ComparisonStatisticsHolders on mid.Id equals cmpHA.MatchIdentifierId
                          where cmpHA.BySideType == 1 && mid.MatchDateTime.Date == azerbaycanTime.Date
                                join lg in Context.LeagueStatisticsHolders on cmpHA.LeagueStaisticsHolderId equals lg.Id
                          group mid by new { lg.CountryName, lg.LeagueName } into matchGroup
                          select new MatchProgram
                          {
                              Country = matchGroup.Key.CountryName,
                              League = matchGroup.Key.LeagueName,
                              Matches = matchGroup.Select(m => new Match
                              {
                                  Serial = m.Serial,
                                  HomeTeam = m.HomeTeam,
                                  AwayTeam = m.AwayTeam,
                                  MatchTime = m.MatchDateTime.ToString("HH:mm")
                              }).ToList()
                          }).ToListAsync();

            return new MatchProgramList { Matches = result };
        }

        public async Task<MatchProgramList> GetGroupedFilteredForecastMatchsProgramAsync()
        {
            TimeZoneInfo azerbaycanZone = TimeZoneInfo.FindSystemTimeZoneById("Azerbaijan Standard Time");
            DateTime azerbaycanTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, azerbaycanZone);

            var result = await (from mid in Context.MatchIdentifiers
                                join cmpHA in Context.ComparisonStatisticsHolders on mid.Id equals cmpHA.MatchIdentifierId
                                where cmpHA.BySideType == 1 && mid.MatchDateTime.Date == azerbaycanTime.Date
                                join lg in Context.LeagueStatisticsHolders on cmpHA.LeagueStaisticsHolderId equals lg.Id
                                group mid by new { lg.CountryName, lg.LeagueName } into matchGroup
                                select new MatchProgram
                                {
                                    Country = matchGroup.Key.CountryName,
                                    League = matchGroup.Key.LeagueName,
                                    Matches = matchGroup.Select(m => new Match
                                    {
                                        Serial = m.Serial,
                                        HomeTeam = m.HomeTeam,
                                        AwayTeam = m.AwayTeam,
                                        MatchTime = m.MatchDateTime.ToString("HH:mm")
                                    }).ToList()
                                }).ToListAsync();

            var filteredResult = new List<MatchProgram>();

            var possibleForecasts = await Context.PossibleForecasts.ToListAsync();
            var serialsFilter = possibleForecasts.Select(x => x.Serial).ToList();

            for (int i = 0; i < result.Count; i++)
            {
                var match = result[i];
                var newMatchProgram = new MatchProgram
                {
                    Country = match.Country,
                    League = match.League,
                    Matches = new List<Match>()
                };

                var filteredMatches = match.Matches.Where(x => serialsFilter.Contains(x.Serial)).ToList();

                if (filteredMatches.Any() || filteredMatches.Count > 0)
                {
                    newMatchProgram.Matches = filteredMatches;
                    filteredResult.Add(newMatchProgram);
                }
            }
            
            return new MatchProgramList { Matches = filteredResult };
        }

        public async Task<MatchDetailProgram> GetAllMatchsProgramAsync()
        {
            TimeZoneInfo azerbaycanZone = TimeZoneInfo.FindSystemTimeZoneById("Azerbaijan Standard Time");
            DateTime azerbaycanTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, azerbaycanZone);
            var result = await (from mid in Context.MatchIdentifiers
                          join cmpHA in Context.ComparisonStatisticsHolders on mid.Id equals cmpHA.MatchIdentifierId
                          where cmpHA.BySideType == 1 && mid.MatchDateTime.Date == azerbaycanTime.Date
                                join lg in Context.LeagueStatisticsHolders on cmpHA.LeagueStaisticsHolderId equals lg.Id
                          select new MatchDetail
                          {
                              Country = lg.CountryName,
                              League = lg.LeagueName,
                              HomeTeam = mid.HomeTeam,
                              AwayTeam = mid.AwayTeam,
                              MatchTime = mid.MatchDateTime.ToString("HH:mm"),
                              Serial = mid.Serial
                          }).ToListAsync();

            return new MatchDetailProgram { Matches = result };
        }

        public async Task<MatchDetailProgram> GetPossibleForecastMatchsProgramAsync()
        {
            TimeZoneInfo azerbaycanZone = TimeZoneInfo.FindSystemTimeZoneById("Azerbaijan Standard Time");
            DateTime azerbaycanTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, azerbaycanZone);
            var result = await (from mid in Context.MatchIdentifiers
                                join cmpHA in Context.ComparisonStatisticsHolders on mid.Id equals cmpHA.MatchIdentifierId
                                join pf in Context.PossibleForecasts on mid.Serial equals pf.Serial
                                where cmpHA.BySideType == 1 && pf.CreatedDate.Date == azerbaycanTime.Date && mid.MatchDateTime.Date == azerbaycanTime.Date
                                join lg in Context.LeagueStatisticsHolders on cmpHA.LeagueStaisticsHolderId equals lg.Id
                                select new MatchDetail
                                {
                                    Country = lg.CountryName,
                                    League = lg.LeagueName,
                                    HomeTeam = mid.HomeTeam,
                                    AwayTeam = mid.AwayTeam,
                                    MatchTime = mid.MatchDateTime.ToString("HH:mm"),
                                    Serial = pf.Serial
                                }).ToListAsync();

            return new MatchDetailProgram { Matches = result };
        }
    }
}
