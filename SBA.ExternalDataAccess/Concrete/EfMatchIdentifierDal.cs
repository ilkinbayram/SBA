using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete.ComplexModels.Program;
using Core.Entities.Concrete.ExternalDbEntities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SBA.ExternalDataAccess.Abstract;
using SBA.ExternalDataAccess.Concrete.EntityFramework.Contexts;
using sqlParamModel = Core.Utilities.UsableModel;
using System.Data;

namespace SBA.ExternalDataAccess.Concrete
{
    public class EfMatchIdentifierDal : EfEntityRepositoryBase<MatchIdentifier, ExternalAppDbContext>, IMatchIdentifierDal
    {
        public EfMatchIdentifierDal(ExternalAppDbContext applicationContext) : base(applicationContext)
        {
        }

        public MatchProgramList GetGroupedMatchsProgram(int month, int day)
        {
            var result = (from mid in Context.MatchIdentifiers
                          join cmpHA in Context.ComparisonStatisticsHolders on mid.Id equals cmpHA.MatchIdentifierId
                          where cmpHA.BySideType == 1 && mid.MatchDateTime.Month == month && mid.MatchDateTime.Day == day
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

        public MatchDetailProgram GetAllMatchsProgram(int month, int day) 
        {
            var result = (from mid in Context.MatchIdentifiers
                          join cmpHA in Context.ComparisonStatisticsHolders on mid.Id equals cmpHA.MatchIdentifierId
                          where cmpHA.BySideType == 1 && mid.MatchDateTime.Month == month && mid.MatchDateTime.Day == day
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

        public async Task<MatchProgramList> GetGroupedMatchsProgramAsync(int month, int day)
        {
            var result = await (from mid in Context.MatchIdentifiers
                          join cmpHA in Context.ComparisonStatisticsHolders on mid.Id equals cmpHA.MatchIdentifierId
                          where cmpHA.BySideType == 1 && mid.MatchDateTime.Month == month && mid.MatchDateTime.Day == day
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
                                // TODO : Turn Back
                                where cmpHA.BySideType == 1 // && mid.MatchDateTime.Date == azerbaycanTime.Date
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

        public async Task<MatchDetailProgram> GetAllMatchsProgramAsync(int month, int day)
        {
            //TimeZoneInfo azerbaycanZone = TimeZoneInfo.FindSystemTimeZoneById("Azerbaijan Standard Time");
            //DateTime azerbaycanTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, azerbaycanZone);

            var result = await (from mid in Context.MatchIdentifiers
                          join cmpHA in Context.ComparisonStatisticsHolders on mid.Id equals cmpHA.MatchIdentifierId
                          where cmpHA.BySideType == 1 
                          // TODO : Turn Back
                          //&& mid.MatchDateTime.Month == month && mid.MatchDateTime.Day == day
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


        public sqlParamModel.MatchPerformanceOverallParameterModel SpGetMatchInformation(int serial)
        {
            var paramSerial = new SqlParameter("@SerialUniqueId", serial);

            // Assuming Context is an instance of your database context
            var connection = this.Context.Database.GetDbConnection();

            try
            {
                connection.Open(); // Explicitly open the connection if it's not already open

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SP_GetMatchInformation";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(paramSerial);

                    using (var reader = command.ExecuteReader())
                    {
                        var result = new sqlParamModel.MatchPerformanceOverallParameterModel();
                        if (reader.Read())  // If there is at least one row
                        {
                            // Map the columns of the result row to the properties of the MatchStatisticOverallResultModel
                            result.HomeTeam = reader.GetString(reader.GetOrdinal("HomeTeam"));
                            result.AwayTeam = reader.GetString(reader.GetOrdinal("AwayTeam"));
                            result.LeagueName = reader.GetString(reader.GetOrdinal("LeagueName"));
                            result.CountryName = reader.GetString(reader.GetOrdinal("CountryName"));
                            result.MatchDate = reader.GetDateTime(reader.GetOrdinal("MatchDate"));
                        }

                        return result;
                    }
                }
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close(); // Explicitly close the connection
                }
            }
        }
    }
}
