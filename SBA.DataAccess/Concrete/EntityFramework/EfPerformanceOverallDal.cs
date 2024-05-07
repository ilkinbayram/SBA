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

            using (var context = new ApplicationDbContext())
            {
                using (var connection = context.Database.GetDbConnection())
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open(); // Bağlantıyı aç
                    }

                    // Create a command
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "SP_GetMatchAnalyseResult";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddRange((new List<SqlParameter> { paramHomeTeam, paramAwayTeam, paramLeague, paramCountry, paramMatchDate }).ToArray());

                        try
                        {
                            using (var reader = command.ExecuteReader())
                            {
                                var result = new MatchStatisticOverallResultModel();
                                if (reader.Read())  // If there is at least one row
                                {
                                    var columnNames = Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).ToList();

                                    result.Average_Home_FT_Goals = ReadDecimal(reader, columnNames, "Average_Home_FT_Goals");
                                    result.Average_Away_FT_Goals = ReadDecimal(reader, columnNames, "Average_Away_FT_Goals");
                                    result.Average_Home_HT_Goals = ReadDecimal(reader, columnNames, "Average_Home_HT_Goals");
                                    result.Average_Away_HT_Goals = ReadDecimal(reader, columnNames, "Average_Away_HT_Goals");
                                    result.Average_Home_SH_Goals = ReadDecimal(reader, columnNames, "Average_Home_SH_Goals");
                                    result.Average_Away_SH_Goals = ReadDecimal(reader, columnNames, "Average_Away_SH_Goals");
                                    result.Average_Home_FT_Conceded_Goals = ReadDecimal(reader, columnNames, "Average_Home_FT_Conceded_Goals");
                                    result.Average_Away_FT_Conceded_Goals = ReadDecimal(reader, columnNames, "Average_Away_FT_Conceded_Goals");
                                    result.Average_Home_HT_Conceded_Goals = ReadDecimal(reader, columnNames, "Average_Home_HT_Conceded_Goals");
                                    result.Average_Away_HT_Conceded_Goals = ReadDecimal(reader, columnNames, "Average_Away_HT_Conceded_Goals");
                                    result.Average_Home_SH_Conceded_Goals = ReadDecimal(reader, columnNames, "Average_Home_SH_Conceded_Goals");
                                    result.Average_Away_SH_Conceded_Goals = ReadDecimal(reader, columnNames, "Average_Away_SH_Conceded_Goals");
                                    result.Average_Home_FT_GK_Saves = ReadDecimal(reader, columnNames, "Average_Home_FT_GK_Saves");
                                    result.Average_Away_FT_GK_Saves = ReadDecimal(reader, columnNames, "Average_Away_FT_GK_Saves");
                                    result.Average_Home_FT_Shuts = ReadDecimal(reader, columnNames, "Average_Home_FT_Shuts");
                                    result.Average_Away_FT_Shuts = ReadDecimal(reader, columnNames, "Average_Away_FT_Shuts");
                                    result.Average_Home_FT_Shuts_ON_Target = ReadDecimal(reader, columnNames, "Average_Home_FT_Shuts_ON_Target");
                                    result.Average_Away_FT_Shuts_ON_Target = ReadDecimal(reader, columnNames, "Average_Away_FT_Shuts_ON_Target");
                                    result.Average_Home_TeamPossesionPercent = ReadInt32(reader, columnNames, "Average_Home_TeamPossesionPercent");
                                    result.Average_Away_TeamPossesionPercent = ReadInt32(reader, columnNames, "Average_Away_TeamPossesionPercent");
                                    result.Average_Home_FT_WinPercent = ReadInt32(reader, columnNames, "Average_Home_FT_WinPercent");
                                    result.Average_Home_HT_WinPercent = ReadInt32(reader, columnNames, "Average_Home_HT_WinPercent");
                                    result.Average_Home_SH_WinPercent = ReadInt32(reader, columnNames, "Average_Home_SH_WinPercent");
                                    result.Average_FT_DrawPercent = ReadInt32(reader, columnNames, "Average_FT_DrawPercent");
                                    result.Average_HT_DrawPercent = ReadInt32(reader, columnNames, "Average_HT_DrawPercent");
                                    result.Average_SH_DrawPercent = ReadInt32(reader, columnNames, "Average_SH_DrawPercent");
                                    result.Average_Away_FT_WinPercent = ReadInt32(reader, columnNames, "Average_Away_FT_WinPercent");
                                    result.Average_Away_HT_WinPercent = ReadInt32(reader, columnNames, "Average_Away_HT_WinPercent");
                                    result.Average_Away_SH_WinPercent = ReadInt32(reader, columnNames, "Average_Away_SH_WinPercent");
                                    result.Average_GG_Percent = ReadInt32(reader, columnNames, "Average_GG_Percent");
                                    result.Average_FT_1_5_Over_Percent = ReadInt32(reader, columnNames, "Average_FT_1_5_Over_Percent");
                                    result.Average_FT_2_5_Over_Percent = ReadInt32(reader, columnNames, "Average_FT_2_5_Over_Percent");
                                    result.Average_FT_3_5_Over_Percent = ReadInt32(reader, columnNames, "Average_FT_3_5_Over_Percent");
                                    result.Average_HT_0_5_Over_Percent = ReadInt32(reader, columnNames, "Average_HT_0_5_Over_Percent");
                                    result.Average_SH_0_5_Over_Percent = ReadInt32(reader, columnNames, "Average_SH_0_5_Over_Percent");
                                    result.Average_Home_IND_FT_0_5_Over_Percent = ReadInt32(reader, columnNames, "Average_Home_IND_FT_0_5_Over_Percent");
                                    result.Average_Home_IND_FT_1_5_Over_Percent = ReadInt32(reader, columnNames, "Average_Home_IND_FT_1_5_Over_Percent");
                                    result.Average_Home_IND_HT_0_5_Over_Percent = ReadInt32(reader, columnNames, "Average_Home_IND_HT_0_5_Over_Percent");
                                    result.Average_Home_IND_SH_0_5_Over_Percent = ReadInt32(reader, columnNames, "Average_Home_IND_SH_0_5_Over_Percent");
                                    result.Average_Home_WinAnyHalf_Percent = ReadInt32(reader, columnNames, "Average_Home_WinAnyHalf_Percent");
                                    result.Average_Away_IND_FT_0_5_Over_Percent = ReadInt32(reader, columnNames, "Average_Away_IND_FT_0_5_Over_Percent");
                                    result.Average_Away_IND_FT_1_5_Over_Percent = ReadInt32(reader, columnNames, "Average_Away_IND_FT_1_5_Over_Percent");
                                    result.Average_Away_IND_HT_0_5_Over_Percent = ReadInt32(reader, columnNames, "Average_Away_IND_HT_0_5_Over_Percent");
                                    result.Average_Away_IND_SH_0_5_Over_Percent = ReadInt32(reader, columnNames, "Average_Away_IND_SH_0_5_Over_Percent");
                                    result.Average_Away_WinAnyHalf_Percent = ReadInt32(reader, columnNames, "Average_Away_WinAnyHalf_Percent");
                                    result.FoundMatchCount = ReadInt32(reader, columnNames, "FoundMatchCount");
                                }

                                return result;
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
