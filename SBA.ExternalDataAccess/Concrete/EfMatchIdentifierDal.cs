using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete.ComplexModels.Program;
using Core.Entities.Concrete.ExternalDbEntities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SBA.ExternalDataAccess.Abstract;
using SBA.ExternalDataAccess.Concrete.EntityFramework.Contexts;
using sqlParamModel = Core.Utilities.UsableModel;
using System.Data;
using Core.Entities.Concrete.ComplexModels.Sql;
using TLMixedCore = Core.Entities.Concrete.ComplexModels.Sql;

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

        public async Task<MatchDetailProgram> GetAllMatchsProgramAsync(int month, int day)
        {
            //TimeZoneInfo azerbaycanZone = TimeZoneInfo.FindSystemTimeZoneById("Azerbaijan Standard Time");
            //DateTime azerbaycanTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, azerbaycanZone);

            var result = await (from mid in Context.MatchIdentifiers
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


        public TLMixedCore.TeamLeagueMixedStat SP_GetTeamLeagueMixedStatResult(int serial)
        {
            var paramSerial = new SqlParameter("@serial", serial);

            using (var context = new ExternalAppDbContext())
            {
                using (var connection = context.Database.GetDbConnection())
                {

                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open(); // Bağlantıyı aç
                    }

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "SP_GetTeamLeagueMixedStatisics";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddRange((new List<SqlParameter>{ paramSerial }).ToArray());

                        try
                        {
                            using (var result = command.ExecuteReader())
                            {
                                var columnNames = Enumerable.Range(0, result.FieldCount).Select(result.GetName).ToList();
                                var resultModel = new TeamLeagueMixedStat();

                                if (result.Read())  // If there is at least one row
                                {
                                    // Map the columns of the result row to the properties of the MatchStatisticOverallResultModel
                                    resultModel.Average_FT_Goals_HomeTeam = ReadDecimal(result, columnNames, "Average_FT_Goals_HomeTeam");
                                    resultModel.Average_FT_Goals_AwayTeam = ReadDecimal(result, columnNames, "Average_FT_Goals_AwayTeam");
                                    resultModel.Average_FT_Conceeded_Goals_HomeTeam = ReadDecimal(result, columnNames, "Average_FT_Conceeded_Goals_HomeTeam");
                                    resultModel.Average_FT_Conceeded_Goals_AwayTeam = ReadDecimal(result, columnNames, "Average_FT_Conceeded_Goals_AwayTeam");
                                    resultModel.Average_HT_Goals_HomeTeam = ReadDecimal(result, columnNames, "Average_HT_Goals_HomeTeam");
                                    resultModel.Average_HT_Goals_AwayTeam = ReadDecimal(result, columnNames, "Average_HT_Goals_AwayTeam");
                                    resultModel.Average_HT_Conceeded_Goals_HomeTeam = ReadDecimal(result, columnNames, "Average_HT_Conceeded_Goals_HomeTeam");
                                    resultModel.Average_HT_Conceeded_Goals_AwayTeam = ReadDecimal(result, columnNames, "Average_HT_Conceeded_Goals_AwayTeam");
                                    resultModel.Average_SH_Goals_HomeTeam = ReadDecimal(result, columnNames, "Average_SH_Goals_HomeTeam");
                                    resultModel.Average_SH_Goals_AwayTeam = ReadDecimal(result, columnNames, "Average_SH_Goals_AwayTeam");
                                    resultModel.Average_SH_Conceeded_Goals_HomeTeam = ReadDecimal(result, columnNames, "Average_SH_Conceeded_Goals_HomeTeam");
                                    resultModel.Average_SH_Conceeded_Goals_AwayTeam = ReadDecimal(result, columnNames, "Average_SH_Conceeded_Goals_AwayTeam");
                                    resultModel.ShutSaveHomeTeam = ReadDecimal(result, columnNames, "ShutSaveHomeTeam");
                                    resultModel.ShutSaveAwayTeam = ReadDecimal(result, columnNames, "ShutSaveAwayTeam");
                                    resultModel.HomeInd_FT_05_Over = ReadInt32(result, columnNames, "HomeInd_FT_05_Over");
                                    resultModel.AwayInd_FT_05_Over = ReadInt32(result, columnNames, "AwayInd_FT_05_Over");
                                    resultModel.HomeInd_FT_15_Over = ReadInt32(result, columnNames, "HomeInd_FT_15_Over");
                                    resultModel.AwayInd_FT_15_Over = ReadInt32(result, columnNames, "AwayInd_FT_15_Over");
                                    resultModel.HomeInd_HT_05_Over = ReadInt32(result, columnNames, "HomeInd_HT_05_Over");
                                    resultModel.AwayInd_HT_05_Over = ReadInt32(result, columnNames, "AwayInd_HT_05_Over");
                                    resultModel.HomeInd_SH_05_Over = ReadInt32(result, columnNames, "HomeInd_SH_05_Over");
                                    resultModel.AwayInd_SH_05_Over = ReadInt32(result, columnNames, "AwayInd_SH_05_Over");
                                    resultModel.FT_GG_Home = ReadInt32(result, columnNames, "FT_GG_Home");
                                    resultModel.FT_GG_Away = ReadInt32(result, columnNames, "FT_GG_Away");
                                    resultModel.FT_15_Over_Home = ReadInt32(result, columnNames, "FT_15_Over_Home");
                                    resultModel.FT_15_Over_Away = ReadInt32(result, columnNames, "FT_15_Over_Away");
                                    resultModel.FT_25_Over_Home = ReadInt32(result, columnNames, "FT_25_Over_Home");
                                    resultModel.FT_25_Over_Away = ReadInt32(result, columnNames, "FT_25_Over_Away");
                                    resultModel.FT_35_Over_Home = ReadInt32(result, columnNames, "FT_35_Over_Home");
                                    resultModel.FT_35_Over_Away = ReadInt32(result, columnNames, "FT_35_Over_Away");
                                    resultModel.HT_05_Over_Home = ReadInt32(result, columnNames, "HT_05_Over_Home");
                                    resultModel.HT_05_Over_Away = ReadInt32(result, columnNames, "HT_05_Over_Away");
                                    resultModel.HT_15_Over_Home = ReadInt32(result, columnNames, "HT_15_Over_Home");
                                    resultModel.HT_15_Over_Away = ReadInt32(result, columnNames, "HT_15_Over_Away");
                                    resultModel.SH_05_Over_Home = ReadInt32(result, columnNames, "SH_05_Over_Home");
                                    resultModel.SH_05_Over_Away = ReadInt32(result, columnNames, "SH_05_Over_Away");
                                    resultModel.SH_15_Over_Home = ReadInt32(result, columnNames, "SH_15_Over_Home");
                                    resultModel.SH_15_Over_Away = ReadInt32(result, columnNames, "SH_15_Over_Away");
                                    resultModel.League_FT_GoalsAverage = ReadDecimal(result, columnNames, "League_FT_GoalsAverage");
                                    resultModel.League_HT_GoalsAverage = ReadDecimal(result, columnNames, "League_HT_GoalsAverage");
                                    resultModel.League_SH_GoalsAverage = ReadDecimal(result, columnNames, "League_SH_GoalsAverage");
                                    resultModel.League_GG_Percentage = ReadInt32(result, columnNames, "League_GG_Percentage");
                                    resultModel.League_FT_Over15_Percentage = ReadInt32(result, columnNames, "League_FT_Over15_Percentage");
                                    resultModel.League_FT_Over25_Percentage = ReadInt32(result, columnNames, "League_FT_Over25_Percentage");
                                    resultModel.League_FT_Over35_Percentage = ReadInt32(result, columnNames, "League_FT_Over35_Percentage");
                                    resultModel.League_HT_Over05_Percentage = ReadInt32(result, columnNames, "League_HT_Over05_Percentage");
                                    resultModel.League_HT_Over15_Percentage = ReadInt32(result, columnNames, "League_HT_Over15_Percentage");
                                    resultModel.League_SH_Over05_Percentage = ReadInt32(result, columnNames, "League_SH_Over05_Percentage");
                                    resultModel.League_SH_Over15_Percentage = ReadInt32(result, columnNames, "League_SH_Over15_Percentage");
                                }

                                return resultModel;
                            }
                        }
                        catch (Exception ex)
                        {
                            return null;
                        }
                    }
                }
            }
        }



        public async Task<List<int>> GetAllProgramSerialsAsync()
        {
            var result = await Context.MatchIdentifiers.Select(x => x.Serial).ToListAsync();
            var uniqueList = new HashSet<int>(result);
            return uniqueList.ToList();
        }


        private decimal ReadDecimal(System.Data.Common.DbDataReader reader, List<string> columnNames, string columnName)
        {
            int index = columnNames.IndexOf(columnName);

            if (index != -1 && !reader.IsDBNull(index))
            {
                return reader.GetDecimal(index);
            }

            return 0;
        }

        private int ReadInt32(System.Data.Common.DbDataReader reader, List<string> columnNames, string columnName)
        {
            int index = columnNames.IndexOf(columnName);

            if (index != -1 && !reader.IsDBNull(index))
            {
                return reader.GetInt32(index);
            }

            return 0;
        }
    }
}
