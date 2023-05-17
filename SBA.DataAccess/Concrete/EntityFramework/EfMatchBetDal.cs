using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using Core.Entities.Concrete.ComplexModels.Sql;
using Core.Entities.Concrete.SqlEntities.QueryModels;
using Core.Utilities.Results;
using Core.Utilities.UsableModel;
using DataAccess.Concrete.EntityFramework.Contexts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SBA.DataAccess.Abstract;
using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace SBA.DataAccess.Concrete.EntityFramework
{
    public class EfMatchBetDal : EfEntityRepositoryBase<MatchBet, ApplicationDbContext>, IMatchBetDal
    {
        public EfMatchBetDal(ApplicationDbContext context) : base(context)
        {
        }

        public IQueryable<MatchBetQM> GetMatchBetQueryModels(string countryName, 
                                                             string teamName, 
                                                             int takeCount,
                                                             Expression<Func<MatchBetQM, bool>> filter = null)
        {

            var query =   from mb in Context.MatchBets
                          join fr in Context.FilterResults
                          on mb.SerialUniqueID equals fr.SerialUniqueID
                          where
                          (mb.Country == "AFC Kupası" ||
                          mb.Country == "AFC Şampiyonlar Ligi" ||
                          mb.Country == "AFC U23 Şampiyonası" ||
                          mb.Country == "AFF Şampiyonası" ||
                          mb.Country == "Afrika U20 Uluslar Kupası" ||
                          mb.Country == "Afrika U23 Uluslar Kupası" ||
                          mb.Country == "Afrika Uluslar Kupası" ||
                          mb.Country == "Afrika Uluslar Şampiyonası" ||
                          mb.Country == "Avrupa Kadınlar Şampiyonası" ||
                          mb.Country == "Avrupa U17 Şampiyonası" ||
                          mb.Country == "Avrupa U19 Şampiyonası" ||
                          mb.Country == "Avrupa U21 Şampiyonası" ||
                          mb.Country == "Baltık Kupası" ||
                          mb.Country == "CAF Konfederasyon Kupası" ||
                          mb.Country == "Dünya Kupası" ||
                          mb.Country == "Dünya Kupası U17" ||
                          mb.Country == "Euro" ||
                          mb.Country == "Kadınlar Dünya Kupası" ||
                          mb.Country == "Körfez Kupası" ||
                          mb.Country == "UEFA" ||
                          mb.Country == "UEFA Avrupa Konferans Ligi" ||
                          mb.Country == "UEFA Avrupa Ligi" ||
                          mb.Country == "UEFA Gençlik Ligi" ||
                          mb.Country == "UEFA Şampiyonlar Ligi" ||
                          mb.Country == "UEFA Uluslar Ligi" ||
                          mb.Country == countryName) &&
                          
                          (mb.HomeTeam == teamName || mb.AwayTeam == teamName)

                          orderby mb.MatchDate descending

                          select new MatchBetQM
                          {
                              Country = mb.Country,
                              MatchDate = mb.MatchDate,
                              SerialUniqueID = mb.SerialUniqueID,
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
                              HomeShotOnTargetCount = fr.HomeShotOnTargetCount
                          };

            query = filter == null
                    ? query.Take(takeCount)
                    : query.Where(filter).Take(takeCount);

            return query;

        }

        public IQueryable<MatchBetQM> GetMatchBetQueryModelsForPerformanceResult(string countryName,
                                                             string teamName,
                                                             int takeCount,
                                                             Expression<Func<MatchBetQM, bool>> filter = null)
        {

            var query = from mb in Context.MatchBets
                        join fr in Context.FilterResults
                        on mb.SerialUniqueID equals fr.SerialUniqueID
                        where
                        mb.Country == countryName &&

                        (mb.HomeTeam == teamName || mb.AwayTeam == teamName)

                        orderby mb.MatchDate descending

                        select new MatchBetQM
                        {
                            Country = mb.Country,
                            MatchDate = mb.MatchDate,
                            SerialUniqueID = mb.SerialUniqueID,
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

        public List<FilterResultMutateModel> GetOddFilteredResult(InTimeShortOddModel inTimeOdds, decimal range)
        {
            var paramFT_W1 = new SqlParameter("@FT_W1", inTimeOdds.FT_W1);
            var paramFT_X = new SqlParameter("@FT_X", inTimeOdds.FT_X);
            var paramFT_W2 = new SqlParameter("@FT_W2", inTimeOdds.FT_W2);
            var paramHT_W1 = new SqlParameter("@HT_W1", inTimeOdds.HT_W1);
            var paramHT_X = new SqlParameter("@HT_X", inTimeOdds.HT_X);
            var paramHT_W2 = new SqlParameter("@HT_W2", inTimeOdds.HT_W2);
            var paramGG = new SqlParameter("@GG", inTimeOdds.GG);
            var paramNG = new SqlParameter("@NG", inTimeOdds.NG);
            var paramFT_15_Over = new SqlParameter("@FT_15_Over", inTimeOdds.FT_15_Over);
            var paramFT_15_Under = new SqlParameter("@FT_15_Under", inTimeOdds.FT_15_Under);
            var paramFT_25_Over = new SqlParameter("@FT_25_Over", inTimeOdds.FT_25_Over);
            var paramFT_25_Under = new SqlParameter("@FT_25_Under", inTimeOdds.FT_25_Under);
            var paramFT_35_Over = new SqlParameter("@FT_35_Over", inTimeOdds.FT_35_Over);
            var paramFT_35_Under = new SqlParameter("@FT_35_Under", inTimeOdds.FT_35_Under);
            //var paramGoals01 = new SqlParameter("@Goals01", inTimeOdds.Goals01);
            //var paramGoals23 = new SqlParameter("@Goals23", inTimeOdds.Goals23);
            //var paramGoals45 = new SqlParameter("@Goals45", inTimeOdds.Goals45);
            //var paramGoals6 = new SqlParameter("@Goals6", inTimeOdds.Goals6);
            var paramRange = new SqlParameter("@Range", range);

            // Create a command
            using var command = this.Context.Database.GetDbConnection().CreateCommand();
            command.CommandText = "SP_GetOddFilteredResult";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddRange((new List<SqlParameter>{ paramFT_W1, paramFT_X, paramFT_W2, paramHT_W1, paramHT_X, paramHT_W2, paramGG, paramNG, paramFT_15_Over, paramFT_15_Under, paramFT_25_Over, paramFT_25_Under, paramFT_35_Over, paramFT_35_Under, 
                //paramGoals01, 
                //paramGoals23, 
                //paramGoals45, 
                //paramGoals6, 
                paramRange }).ToArray());

            // Open the connection and execute the command
            this.Context.Database.OpenConnection();
            using var result = command.ExecuteReader();

            // Create a list to hold the result objects
            var filterResultMutateModels = new List<FilterResultMutateModel>();

            // Read the results
            while (result.Read())
            {
                var filterResultMutateModel = new FilterResultMutateModel
                {
                    Home_FT_GoalsCount = result.GetInt32(result.GetOrdinal("Home_FT_GoalsCount")),
                    Home_HT_GoalsCount = result.GetInt32(result.GetOrdinal("Home_HT_GoalsCount")),
                    Home_SH_GoalsCount = result.GetInt32(result.GetOrdinal("Home_SH_GoalsCount")),
                    HomeCornerCount = result.GetInt32(result.GetOrdinal("HomeCornerCount")),
                    HomeShotCount = result.GetInt32(result.GetOrdinal("HomeShotCount")),
                    HomeShotOnTargetCount = result.GetInt32(result.GetOrdinal("HomeShotOnTargetCount")),
                    HomePossesion = result.GetInt32(result.GetOrdinal("HomePossesion")),
                    Home_GK_SavesCount = result.GetInt32(result.GetOrdinal("Home_GK_SavesCount")),
                    Home_Win_Any_Half = result.GetBoolean(result.GetOrdinal("Home_Win_Any_Half")),
                    Home_HT_0_5_Over = result.GetBoolean(result.GetOrdinal("Home_HT_0_5_Over")),
                    Home_HT_1_5_Over = result.GetBoolean(result.GetOrdinal("Home_HT_1_5_Over")),
                    Home_SH_0_5_Over = result.GetBoolean(result.GetOrdinal("Home_SH_0_5_Over")),
                    Home_SH_1_5_Over = result.GetBoolean(result.GetOrdinal("Home_SH_1_5_Over")),
                    Home_FT_0_5_Over = result.GetBoolean(result.GetOrdinal("Home_FT_0_5_Over")),
                    Home_FT_1_5_Over = result.GetBoolean(result.GetOrdinal("Home_FT_1_5_Over")),
                    Corner_Home_3_5_Over = result.GetBoolean(result.GetOrdinal("Corner_Home_3_5_Over")),
                    Corner_Home_4_5_Over = result.GetBoolean(result.GetOrdinal("Corner_Home_4_5_Over")),
                    Corner_Home_5_5_Over = result.GetBoolean(result.GetOrdinal("Corner_Home_5_5_Over")),
                    Away_FT_GoalsCount = result.GetInt32(result.GetOrdinal("Away_FT_GoalsCount")),
                    Away_HT_GoalsCount = result.GetInt32(result.GetOrdinal("Away_HT_GoalsCount")),
                    Away_SH_GoalsCount = result.GetInt32(result.GetOrdinal("Away_SH_GoalsCount")),
                    AwayCornerCount = result.GetInt32(result.GetOrdinal("AwayCornerCount")),
                    AwayShotCount = result.GetInt32(result.GetOrdinal("AwayShotCount")),
                    AwayShotOnTargetCount = result.GetInt32(result.GetOrdinal("AwayShotOnTargetCount")),
                    AwayPossesion = result.GetInt32(result.GetOrdinal("AwayPossesion")),
                    Away_GK_SavesCount = result.GetInt32(result.GetOrdinal("Away_GK_SavesCount")),
                    Away_Win_Any_Half = result.GetBoolean(result.GetOrdinal("Away_Win_Any_Half")),
                    Away_HT_0_5_Over = result.GetBoolean(result.GetOrdinal("Away_HT_0_5_Over")),
                    Away_HT_1_5_Over = result.GetBoolean(result.GetOrdinal("Away_HT_1_5_Over")),
                    Away_SH_0_5_Over = result.GetBoolean(result.GetOrdinal("Away_SH_0_5_Over")),
                    Away_SH_1_5_Over = result.GetBoolean(result.GetOrdinal("Away_SH_1_5_Over")),
                    Away_FT_0_5_Over = result.GetBoolean(result.GetOrdinal("Away_FT_0_5_Over")),
                    Away_FT_1_5_Over = result.GetBoolean(result.GetOrdinal("Away_FT_1_5_Over")),
                    Corner_Away_3_5_Over = result.GetBoolean(result.GetOrdinal("Corner_Away_3_5_Over")),
                    Corner_Away_4_5_Over = result.GetBoolean(result.GetOrdinal("Corner_Away_4_5_Over")),
                    Corner_Away_5_5_Over = result.GetBoolean(result.GetOrdinal("Corner_Away_5_5_Over")),
                    Corner_7_5_Over = result.GetBoolean(result.GetOrdinal("Corner_7_5_Over")),
                    Corner_8_5_Over = result.GetBoolean(result.GetOrdinal("Corner_8_5_Over")),
                    Corner_9_5_Over = result.GetBoolean(result.GetOrdinal("Corner_9_5_Over")),
                    HT_0_5_Over = result.GetBoolean(result.GetOrdinal("HT_0_5_Over")),
                    HT_1_5_Over = result.GetBoolean(result.GetOrdinal("HT_1_5_Over")),
                    SH_0_5_Over = result.GetBoolean(result.GetOrdinal("SH_0_5_Over")),
                    SH_1_5_Over = result.GetBoolean(result.GetOrdinal("SH_1_5_Over")),
                    FT_1_5_Over = result.GetBoolean(result.GetOrdinal("FT_1_5_Over")),
                    FT_2_5_Over = result.GetBoolean(result.GetOrdinal("FT_2_5_Over")),
                    FT_3_5_Over = result.GetBoolean(result.GetOrdinal("FT_3_5_Over")),
                    HT_GG = result.GetBoolean(result.GetOrdinal("HT_GG")),
                    SH_GG = result.GetBoolean(result.GetOrdinal("SH_GG")),
                    FT_GG = result.GetBoolean(result.GetOrdinal("FT_GG")),
                    IsCornerFound = result.GetBoolean(result.GetOrdinal("IsCornerFound")),
                    IsPossesionFound = result.GetBoolean(result.GetOrdinal("IsPossesionFound")),
                    IsShotFound = result.GetBoolean(result.GetOrdinal("IsShotFound")),
                    IsShotOnTargetFound = result.GetBoolean(result.GetOrdinal("IsShotOnTargetFound")),
                    Is_Corner_FT_Win1 = result.GetBoolean(result.GetOrdinal("Is_Corner_FT_Win1")),
                    Is_Corner_FT_X = result.GetBoolean(result.GetOrdinal("Is_Corner_FT_X")),
                    Is_Corner_FT_Win2 = result.GetBoolean(result.GetOrdinal("Is_Corner_FT_Win2")),
                    Is_HT_Win1 = result.GetBoolean(result.GetOrdinal("Is_HT_Win1")),
                    Is_HT_X = result.GetBoolean(result.GetOrdinal("Is_HT_X")),
                    Is_HT_Win2 = result.GetBoolean(result.GetOrdinal("Is_HT_Win2")),
                    Is_SH_Win1 = result.GetBoolean(result.GetOrdinal("Is_SH_Win1")),
                    Is_SH_X = result.GetBoolean(result.GetOrdinal("Is_SH_X")),
                    Is_SH_Win2 = result.GetBoolean(result.GetOrdinal("Is_SH_Win2")),
                    Is_FT_Win1 = result.GetBoolean(result.GetOrdinal("Is_FT_Win1")),
                    Is_FT_X = result.GetBoolean(result.GetOrdinal("Is_FT_X")),
                    Is_FT_Win2 = result.GetBoolean(result.GetOrdinal("Is_FT_Win2"))
                };

                filterResultMutateModels.Add(filterResultMutateModel);
            }

            return filterResultMutateModels;
        }
    }
}
