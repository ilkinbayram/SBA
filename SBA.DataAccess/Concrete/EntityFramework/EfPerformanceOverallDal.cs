using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using Core.Entities.Concrete.ComplexModels.Sql;
using Core.Utilities.UsableModel;
using DataAccess.Concrete.EntityFramework.Contexts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SBA.DataAccess.Abstract;
using System.Data;

namespace SBA.DataAccess.Concrete.EntityFramework
{
    public class EfPerformanceOverallDal : EfEntityRepositoryBase<PerformanceOverall, ApplicationDbContext>, IPerformanceOverallDal
    {
        public EfPerformanceOverallDal(ApplicationDbContext applicationContext) : base(applicationContext)
        {
        }

        public MatchStatisticOverallResultModel GetSpMatchAnalyzeResult(MatchPerformanceOverallParameterModel parameters)
        {
            var paramHomeTeam = new SqlParameter("@HomeTeam", parameters.HomeTeam);
            var paramAwayTeam = new SqlParameter("@AwayTeam", parameters.AwayTeam);
            var paramCountry = new SqlParameter("@CountryName", parameters.CountryName);
            var paramLeague = new SqlParameter("@LeagueName", parameters.LeagueName);
            var paramMatchDate = new SqlParameter("@MatchDate", parameters.MatchDate);

            var connection = this.Context.Database.GetDbConnection();

            try
            {
                connection.Open(); // Explicitly open the connection if it's not already open

                // Create a command
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SP_GetMatchAnalyseResult";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange((new List<SqlParameter>{paramHomeTeam, paramAwayTeam, paramLeague, paramCountry, paramMatchDate}).ToArray());

                    using (var reader = command.ExecuteReader())
                    {
                        var result = new MatchStatisticOverallResultModel();
                        if (reader.Read())  // If there is at least one row
                        {
                            // Map the columns of the result row to the properties of the MatchStatisticOverallResultModel
                            result.Average_Home_FT_Goals = reader.GetDecimal(reader.GetOrdinal("Average_Home_FT_Goals"));
                            result.Average_Away_FT_Goals = reader.GetDecimal(reader.GetOrdinal("Average_Away_FT_Goals"));
                            result.Average_Home_HT_Goals = reader.GetDecimal(reader.GetOrdinal("Average_Home_HT_Goals"));
                            result.Average_Away_HT_Goals = reader.GetDecimal(reader.GetOrdinal("Average_Away_HT_Goals"));
                            result.Average_Home_SH_Goals = reader.GetDecimal(reader.GetOrdinal("Average_Home_SH_Goals"));
                            result.Average_Away_SH_Goals = reader.GetDecimal(reader.GetOrdinal("Average_Away_SH_Goals"));
                            result.Average_Home_FT_Conceded_Goals = reader.GetDecimal(reader.GetOrdinal("Average_Home_FT_Conceded_Goals"));
                            result.Average_Away_FT_Conceded_Goals = reader.GetDecimal(reader.GetOrdinal("Average_Away_FT_Conceded_Goals"));
                            result.Average_Home_HT_Conceded_Goals = reader.GetDecimal(reader.GetOrdinal("Average_Home_HT_Conceded_Goals"));
                            result.Average_Away_HT_Conceded_Goals = reader.GetDecimal(reader.GetOrdinal("Average_Away_HT_Conceded_Goals"));
                            result.Average_Home_SH_Conceded_Goals = reader.GetDecimal(reader.GetOrdinal("Average_Home_SH_Conceded_Goals"));
                            result.Average_Away_SH_Conceded_Goals = reader.GetDecimal(reader.GetOrdinal("Average_Away_SH_Conceded_Goals"));
                            result.Average_Home_FT_GK_Saves = reader.GetDecimal(reader.GetOrdinal("Average_Home_FT_GK_Saves"));
                            result.Average_Away_FT_GK_Saves = reader.GetDecimal(reader.GetOrdinal("Average_Away_FT_GK_Saves"));
                            result.Average_Home_FT_Shuts = reader.GetDecimal(reader.GetOrdinal("Average_Home_FT_Shuts"));
                            result.Average_Away_FT_Shuts = reader.GetDecimal(reader.GetOrdinal("Average_Away_FT_Shuts"));
                            result.Average_Home_FT_Shuts_ON_Target = reader.GetDecimal(reader.GetOrdinal("Average_Home_FT_Shuts_ON_Target"));
                            result.Average_Away_FT_Shuts_ON_Target = reader.GetDecimal(reader.GetOrdinal("Average_Away_FT_Shuts_ON_Target"));
                            result.Average_Home_TeamPossesionPercent = reader.GetInt32(reader.GetOrdinal("Average_Home_TeamPossesionPercent"));
                            result.Average_Away_TeamPossesionPercent = reader.GetInt32(reader.GetOrdinal("Average_Away_TeamPossesionPercent"));
                            result.Average_Home_FT_WinPercent = reader.GetInt32(reader.GetOrdinal("Average_Home_FT_WinPercent"));
                            result.Average_Home_HT_WinPercent = reader.GetInt32(reader.GetOrdinal("Average_Home_HT_WinPercent"));
                            result.Average_Home_SH_WinPercent = reader.GetInt32(reader.GetOrdinal("Average_Home_SH_WinPercent"));
                            result.Average_FT_DrawPercent = reader.GetInt32(reader.GetOrdinal("Average_FT_DrawPercent"));
                            result.Average_HT_DrawPercent = reader.GetInt32(reader.GetOrdinal("Average_HT_DrawPercent"));
                            result.Average_SH_DrawPercent = reader.GetInt32(reader.GetOrdinal("Average_SH_DrawPercent"));
                            result.Average_Away_FT_WinPercent = reader.GetInt32(reader.GetOrdinal("Average_Away_FT_WinPercent"));
                            result.Average_Away_HT_WinPercent = reader.GetInt32(reader.GetOrdinal("Average_Away_HT_WinPercent"));
                            result.Average_Away_SH_WinPercent = reader.GetInt32(reader.GetOrdinal("Average_Away_SH_WinPercent"));
                            result.Average_GG_Percent = reader.GetInt32(reader.GetOrdinal("Average_GG_Percent"));
                            result.Average_FT_1_5_Over_Percent = reader.GetInt32(reader.GetOrdinal("Average_FT_1_5_Over_Percent"));
                            result.Average_FT_2_5_Over_Percent = reader.GetInt32(reader.GetOrdinal("Average_FT_2_5_Over_Percent"));
                            result.Average_FT_3_5_Over_Percent = reader.GetInt32(reader.GetOrdinal("Average_FT_3_5_Over_Percent"));
                            result.Average_HT_0_5_Over_Percent = reader.GetInt32(reader.GetOrdinal("Average_HT_0_5_Over_Percent"));
                            result.Average_SH_0_5_Over_Percent = reader.GetInt32(reader.GetOrdinal("Average_SH_0_5_Over_Percent"));
                            result.Average_Home_IND_FT_0_5_Over_Percent = reader.GetInt32(reader.GetOrdinal("Average_Home_IND_FT_0_5_Over_Percent"));
                            result.Average_Home_IND_FT_1_5_Over_Percent = reader.GetInt32(reader.GetOrdinal("Average_Home_IND_FT_1_5_Over_Percent"));
                            result.Average_Home_IND_HT_0_5_Over_Percent = reader.GetInt32(reader.GetOrdinal("Average_Home_IND_HT_0_5_Over_Percent"));
                            result.Average_Home_IND_SH_0_5_Over_Percent = reader.GetInt32(reader.GetOrdinal("Average_Home_IND_SH_0_5_Over_Percent"));
                            result.Average_Home_WinAnyHalf_Percent = reader.GetInt32(reader.GetOrdinal("Average_Home_WinAnyHalf_Percent"));
                            result.Average_Away_IND_FT_0_5_Over_Percent = reader.GetInt32(reader.GetOrdinal("Average_Away_IND_FT_0_5_Over_Percent"));
                            result.Average_Away_IND_FT_1_5_Over_Percent = reader.GetInt32(reader.GetOrdinal("Average_Away_IND_FT_1_5_Over_Percent"));
                            result.Average_Away_IND_HT_0_5_Over_Percent = reader.GetInt32(reader.GetOrdinal("Average_Away_IND_HT_0_5_Over_Percent"));
                            result.Average_Away_IND_SH_0_5_Over_Percent = reader.GetInt32(reader.GetOrdinal("Average_Away_IND_SH_0_5_Over_Percent"));
                            result.Average_Away_WinAnyHalf_Percent = reader.GetInt32(reader.GetOrdinal("Average_Away_WinAnyHalf_Percent"));
                            result.FoundMatchCount = reader.GetInt32(reader.GetOrdinal("FoundMatchCount"));
                        }

                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                return new MatchStatisticOverallResultModel();
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
