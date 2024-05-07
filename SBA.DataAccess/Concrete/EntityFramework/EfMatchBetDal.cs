using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using Core.Entities.Concrete.ComplexModels.Sql;
using Core.Entities.Concrete.SqlEntities.QueryModels;
using Core.Utilities.UsableModel;
using DataAccess.Concrete.EntityFramework.Contexts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SBA.DataAccess.Abstract;
using System.Data;
using System.Linq.Expressions;

namespace SBA.DataAccess.Concrete.EntityFramework
{
    public class EfMatchBetDal : EfEntityRepositoryBase<MatchBet, ApplicationDbContext>, IMatchBetDal
    {
        public EfMatchBetDal(ApplicationDbContext context) : base(context)
        {
        }

        public IQueryable<MatchBetQM> GetMatchBetQueryModelsForPerformanceResult(List<int> leagueIds, string countryName,
                                                             string teamName,
                                                             int takeCount,
                                                             Expression<Func<MatchBetQM, bool>> filter = null)
        {

            var query = from mb in Context.MatchBets
                        join fr in Context.FilterResults
                        on mb.SerialUniqueID equals fr.SerialUniqueID
                        where
                        leagueIds.Contains(mb.LeagueId) &&

                        (mb.HomeTeam == teamName || mb.AwayTeam == teamName)

                        orderby mb.MatchDate descending

                        select new MatchBetQM
                        {
                            Country = mb.Country,
                            MatchDate = mb.MatchDate,
                            SerialUniqueID = mb.SerialUniqueID,
                            LeagueID = mb.LeagueId,
                            HomeTeam = mb.HomeTeam,
                            AwayTeam = mb.AwayTeam,
                            FT_Match_Result = mb.FT_Match_Result,
                            HT_Match_Result = mb.HT_Match_Result,
                            AwayCornersCount = fr.AwayCornerCount,
                            HomeCornersCount = fr.HomeCornerCount,
                            AwayPossesion = fr.AwayPossesion,
                            HomePossesion = fr.HomePossesion,
                            AwayShotCount = fr.AwayShotCount,
                            HomeShotCount = fr.HomeShotCount,
                            AwayShotOnTargetCount = fr.AwayShotOnTargetCount,
                            HomeShotOnTargetCount = fr.HomeShotOnTargetCount,
                            HasCorner = fr.IsCornerFound,
                            HasPossesion = fr.IsPossesionFound,
                            HasShot = fr.IsShotFound,
                            HasShotOnTarget = fr.IsShotOnTargetFound
                        };

            query = filter == null
                    ? query.Take(takeCount)
                    : query.Where(filter).Take(takeCount);

            return query;

        }

        public IQueryable<MatchBetQM> GetMatchBetFilterResultQueryModels(Expression<Func<MatchBetQM, bool>> filter = null)
        {

            var query = from mb in Context.MatchBets
                        join fr in Context.FilterResults
                        on mb.SerialUniqueID equals fr.SerialUniqueID

                        orderby mb.MatchDate descending

                        select new MatchBetQM
                        {
                            Country = mb.Country,
                            MatchDate = mb.MatchDate,
                            SerialUniqueID = mb.SerialUniqueID,
                            LeagueID = mb.LeagueId,
                            HomeTeam = mb.HomeTeam,
                            AwayTeam = mb.AwayTeam,
                            FT_Match_Result = mb.FT_Match_Result,
                            HT_Match_Result = mb.HT_Match_Result,
                            AwayCornersCount = fr.AwayCornerCount,
                            HomeCornersCount = fr.HomeCornerCount,
                            AwayPossesion = fr.AwayPossesion,
                            HomePossesion = fr.HomePossesion,
                            AwayShotCount = fr.AwayShotCount,
                            HomeShotCount = fr.HomeShotCount,
                            AwayShotOnTargetCount = fr.AwayShotOnTargetCount,
                            HomeShotOnTargetCount = fr.HomeShotOnTargetCount,
                            League = mb.LeagueName,
                            HasCorner = fr.IsCornerFound,
                            HasShotOnTarget = fr.IsShotOnTargetFound,
                            HasShot = fr.IsShotFound,
                            HasPossesion = fr.IsPossesionFound
                        };

            return filter == null
                   ? query
                   : query.Where(filter);

        }

        public List<FilterResultMutateModel> GetOddFilteredResult(InTimeShortOddModel inTimeOdds, decimal range, DateTime? matchDate = null)
        {
            var paramFT_W1 = new SqlParameter("@FT_W1", (double)inTimeOdds.FT_W1);
            var paramFT_X = new SqlParameter("@FT_X", (double)inTimeOdds.FT_X);
            var paramFT_W2 = new SqlParameter("@FT_W2", (double)inTimeOdds.FT_W2);
            //var paramHT_W1 = new SqlParameter("@HT_W1", (double)inTimeOdds.HT_W1);
            //var paramHT_X = new SqlParameter("@HT_X", (double)inTimeOdds.HT_X);
            //var paramHT_W2 = new SqlParameter("@HT_W2", (double)inTimeOdds.HT_W2);
            var paramGG = new SqlParameter("@GG", (double)inTimeOdds.GG);
            var paramNG = new SqlParameter("@NG", (double)inTimeOdds.NG);
            //var paramFT_15_Over = new SqlParameter("@FT_15_Over", (double)inTimeOdds.FT_15_Over);
            //var paramFT_15_Under = new SqlParameter("@FT_15_Under", (double)inTimeOdds.FT_15_Under);
            var paramFT_25_Over = new SqlParameter("@FT_25_Over", (double)inTimeOdds.FT_25_Over);
            var paramFT_25_Under = new SqlParameter("@FT_25_Under", (double)inTimeOdds.FT_25_Under);
            //var paramFT_35_Over = new SqlParameter("@FT_35_Over", (double)inTimeOdds.FT_35_Over);
            //var paramFT_35_Under = new SqlParameter("@FT_35_Under", (double)inTimeOdds.FT_35_Under);
            //var paramGoals01 = new SqlParameter("@Goals01", (double)inTimeOdds.Goals01);
            //var paramGoals23 = new SqlParameter("@Goals23", (double)inTimeOdds.Goals23);
            //var paramGoals45 = new SqlParameter("@Goals45", (double)inTimeOdds.Goals45);
            //var paramGoals6 = new SqlParameter("@Goals6", (double)inTimeOdds.Goals6);
            var paramRange = new SqlParameter("@Range", (double)range);

            var paramMatchDate = new SqlParameter("@MatchDate", SqlDbType.DateTime);
            if (matchDate.HasValue)
                paramMatchDate.Value = matchDate.Value;
            else
                paramMatchDate.Value = DBNull.Value;

            using (var context = new ApplicationDbContext())
            {

                using (var connection = context.Database.GetDbConnection())
                {

                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open(); // Bağlantıyı aç
                    }

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "SP_GetOddFilteredResult";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddRange((new List<SqlParameter>{
                paramFT_W1, paramFT_X, paramFT_W2,
                //paramHT_W1, paramHT_X, paramHT_W2,
                paramGG, paramNG,
                //paramFT_15_Over, paramFT_15_Under,
                paramFT_25_Over, paramFT_25_Under,
                //paramFT_35_Over, paramFT_35_Under,
                //paramGoals01, paramGoals23, paramGoals45, paramGoals6,
                paramRange, paramMatchDate }).ToArray());

                        try
                        {
                            using (var result = command.ExecuteReader())
                            {
                                var columnNames = Enumerable.Range(0, result.FieldCount).Select(result.GetName).ToList();
                                var filterResultMutateModels = new List<FilterResultMutateModel>();

                                while (result.Read())
                                {
                                    var filterResultMutateModel = new FilterResultMutateModel
                                    {
                                        Home_FT_GoalsCount = ReadInt32(result, columnNames, "Home_FT_GoalsCount"),
                                        Home_HT_GoalsCount = ReadInt32(result, columnNames, "Home_HT_GoalsCount"),
                                        Home_SH_GoalsCount = ReadInt32(result, columnNames, "Home_SH_GoalsCount"),
                                        HomeCornerCount = ReadInt32(result, columnNames, "HomeCornerCount"),
                                        HomeShotCount = ReadInt32(result, columnNames, "HomeShotCount"),
                                        HomeShotOnTargetCount = ReadInt32(result, columnNames, "HomeShotOnTargetCount"),
                                        HomePossesion = ReadInt32(result, columnNames, "HomePossesion"),
                                        Home_GK_SavesCount = ReadInt32(result, columnNames, "Home_GK_SavesCount"),
                                        Home_Win_Any_Half = ReadBoolean(result, columnNames, "Home_Win_Any_Half"),
                                        Home_HT_0_5_Over = ReadBoolean(result, columnNames, "Home_HT_0_5_Over"),
                                        Home_HT_1_5_Over = ReadBoolean(result, columnNames, "Home_HT_1_5_Over"),
                                        Home_SH_0_5_Over = ReadBoolean(result, columnNames, "Home_SH_0_5_Over"),
                                        Home_SH_1_5_Over = ReadBoolean(result, columnNames, "Home_SH_1_5_Over"),
                                        Home_FT_0_5_Over = ReadBoolean(result, columnNames, "Home_FT_0_5_Over"),
                                        Home_FT_1_5_Over = ReadBoolean(result, columnNames, "Home_FT_1_5_Over"),
                                        Corner_Home_3_5_Over = ReadBoolean(result, columnNames, "Corner_Home_3_5_Over"),
                                        Corner_Home_4_5_Over = ReadBoolean(result, columnNames, "Corner_Home_4_5_Over"),
                                        Corner_Home_5_5_Over = ReadBoolean(result, columnNames, "Corner_Home_5_5_Over"),
                                        Away_FT_GoalsCount = ReadInt32(result, columnNames, "Away_FT_GoalsCount"),
                                        Away_HT_GoalsCount = ReadInt32(result, columnNames, "Away_HT_GoalsCount"),
                                        Away_SH_GoalsCount = ReadInt32(result, columnNames, "Away_SH_GoalsCount"),
                                        AwayCornerCount = ReadInt32(result, columnNames, "AwayCornerCount"),
                                        AwayShotCount = ReadInt32(result, columnNames, "AwayShotCount"),
                                        AwayShotOnTargetCount = ReadInt32(result, columnNames, "AwayShotOnTargetCount"),
                                        AwayPossesion = ReadInt32(result, columnNames, "AwayPossesion"),
                                        Away_GK_SavesCount = ReadInt32(result, columnNames, "Away_GK_SavesCount"),
                                        Away_Win_Any_Half = ReadBoolean(result, columnNames, "Away_Win_Any_Half"),
                                        Away_HT_0_5_Over = ReadBoolean(result, columnNames, "Away_HT_0_5_Over"),
                                        Away_HT_1_5_Over = ReadBoolean(result, columnNames, "Away_HT_1_5_Over"),
                                        Away_SH_0_5_Over = ReadBoolean(result, columnNames, "Away_SH_0_5_Over"),
                                        Away_SH_1_5_Over = ReadBoolean(result, columnNames, "Away_SH_1_5_Over"),
                                        Away_FT_0_5_Over = ReadBoolean(result, columnNames, "Away_FT_0_5_Over"),
                                        Away_FT_1_5_Over = ReadBoolean(result, columnNames, "Away_FT_1_5_Over"),
                                        Corner_Away_3_5_Over = ReadBoolean(result, columnNames, "Corner_Away_3_5_Over"),
                                        Corner_Away_4_5_Over = ReadBoolean(result, columnNames, "Corner_Away_4_5_Over"),
                                        Corner_Away_5_5_Over = ReadBoolean(result, columnNames, "Corner_Away_5_5_Over"),
                                        Corner_7_5_Over = ReadBoolean(result, columnNames, "Corner_7_5_Over"),
                                        Corner_8_5_Over = ReadBoolean(result, columnNames, "Corner_8_5_Over"),
                                        Corner_9_5_Over = ReadBoolean(result, columnNames, "Corner_9_5_Over"),
                                        HT_0_5_Over = ReadBoolean(result, columnNames, "HT_0_5_Over"),
                                        HT_1_5_Over = ReadBoolean(result, columnNames, "HT_1_5_Over"),
                                        SH_0_5_Over = ReadBoolean(result, columnNames, "SH_0_5_Over"),
                                        SH_1_5_Over = ReadBoolean(result, columnNames, "SH_1_5_Over"),
                                        FT_1_5_Over = ReadBoolean(result, columnNames, "FT_1_5_Over"),
                                        FT_2_5_Over = ReadBoolean(result, columnNames, "FT_2_5_Over"),
                                        FT_3_5_Over = ReadBoolean(result, columnNames, "FT_3_5_Over"),
                                        HT_GG = ReadBoolean(result, columnNames, "HT_GG"),
                                        SH_GG = ReadBoolean(result, columnNames, "SH_GG"),
                                        FT_GG = ReadBoolean(result, columnNames, "FT_GG"),
                                        IsCornerFound = ReadBoolean(result, columnNames, "IsCornerFound"),
                                        IsPossesionFound = ReadBoolean(result, columnNames, "IsPossesionFound"),
                                        IsShotFound = ReadBoolean(result, columnNames, "IsShotFound"),
                                        IsShotOnTargetFound = ReadBoolean(result, columnNames, "IsShotOnTargetFound"),
                                        Is_Corner_FT_Win1 = ReadBoolean(result, columnNames, "Is_Corner_FT_Win1"),
                                        Is_Corner_FT_X = ReadBoolean(result, columnNames, "Is_Corner_FT_X"),
                                        Is_Corner_FT_Win2 = ReadBoolean(result, columnNames, "Is_Corner_FT_Win2"),
                                        Is_HT_Win1 = ReadBoolean(result, columnNames, "Is_HT_Win1"),
                                        Is_HT_X = ReadBoolean(result, columnNames, "Is_HT_X"),
                                        Is_HT_Win2 = ReadBoolean(result, columnNames, "Is_HT_Win2"),
                                        Is_SH_Win1 = ReadBoolean(result, columnNames, "Is_SH_Win1"),
                                        Is_SH_X = ReadBoolean(result, columnNames, "Is_SH_X"),
                                        Is_SH_Win2 = ReadBoolean(result, columnNames, "Is_SH_Win2"),
                                        Is_FT_Win1 = ReadBoolean(result, columnNames, "Is_FT_Win1"),
                                        Is_FT_X = ReadBoolean(result, columnNames, "Is_FT_X"),
                                        Is_FT_Win2 = ReadBoolean(result, columnNames, "Is_FT_Win2")
                                    };

                                    filterResultMutateModels.Add(filterResultMutateModel);
                                }

                                return filterResultMutateModels;
                            }
                        }
                        catch (Exception ex)
                        {
                            return new List<FilterResultMutateModel>();
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

        private bool ReadBoolean(System.Data.Common.DbDataReader reader, List<string> columnNames, string columnName)
        {
            int index = columnNames.IndexOf(columnName);
            if (index != -1 && !reader.IsDBNull(index))
            {
                return reader.GetBoolean(index);
            }
            return false;
        }
    }
}
