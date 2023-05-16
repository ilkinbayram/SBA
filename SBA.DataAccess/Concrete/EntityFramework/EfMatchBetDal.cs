using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using Core.Entities.Concrete.ComplexModels.Sql;
using Core.Entities.Concrete.SqlEntities.QueryModels;
using Core.Utilities.Results;
using Core.Utilities.UsableModel;
using DataAccess.Concrete.EntityFramework.Contexts;
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

        public async Task<List<FilterResultMutateModel>> GetOddFilteredResultAsync(InTimeShortOddModel inTimeOdds, decimal range)
        {
            var abc = Convert.ToInt32("".Split("-")[0]);

            var queryResult = (from m in Context.MatchBets
                               join f in Context.FilterResults
                               on m.SerialUniqueID equals f.SerialUniqueID
                               where
                               m.FTWin1_Odd >= (inTimeOdds.FT_W1 - range) && m.FTWin1_Odd <= (inTimeOdds.FT_W1 + range) &&
                               m.FTDraw_Odd >= (inTimeOdds.FT_X - range) && m.FTDraw_Odd <= (inTimeOdds.FT_X + range) &&
                               m.FTWin2_Odd >= (inTimeOdds.FT_W2 - range) && m.FTWin2_Odd <= (inTimeOdds.FT_W2 + range) &&

                               m.HTWin1_Odd >= (inTimeOdds.HT_W1 - range) && m.HTWin1_Odd <= (inTimeOdds.HT_W1 + range) &&
                               m.HTDraw_Odd >= (inTimeOdds.HT_X - range) && m.HTDraw_Odd <= (inTimeOdds.HT_X + range) &&
                               m.HTWin2_Odd >= (inTimeOdds.HT_W2 - range) && m.HTWin2_Odd <= (inTimeOdds.HT_W2 + range) &&

                               m.FT_GG_Odd >= (inTimeOdds.GG - range) && m.FT_GG_Odd <= (inTimeOdds.GG + range) &&
                               m.FT_NG_Odd >= (inTimeOdds.NG - range) && m.FT_NG_Odd <= (inTimeOdds.NG + range) &&

                               m.FT_01_Odd >= (inTimeOdds.Goals01 - range) && m.FT_01_Odd <= (inTimeOdds.Goals01 + range) &&
                               m.FT_23_Odd >= (inTimeOdds.Goals23 - range) && m.FT_23_Odd <= (inTimeOdds.Goals23 + range) &&
                               m.FT_45_Odd >= (inTimeOdds.Goals45 - range) && m.FT_45_Odd <= (inTimeOdds.Goals45 + range) &&
                               m.FT_6_Odd >= (inTimeOdds.Goals6 - range) && m.FT_6_Odd <= (inTimeOdds.Goals6 + range)
                               select new FilterResultMutateModel
                               {
                                   HomeShotCount = f.HomeShotCount,
                                   HomeShotOnTargetCount = f.HomeShotOnTargetCount,
                                   HomePossesion = f.HomePossesion,
                                   HomeCornerCount = f.HomeCornerCount,
                                   Home_FT_0_5_Over = f.Home_FT_0_5_Over,
                                   Home_FT_1_5_Over = f.Home_FT_1_5_Over,
                                   Home_HT_0_5_Over = f.Home_HT_0_5_Over,
                                   Home_HT_1_5_Over = f.Home_HT_1_5_Over,
                                   Home_SH_0_5_Over = f.Home_SH_0_5_Over,
                                   Home_SH_1_5_Over = f.Home_SH_1_5_Over,
                                   Home_Win_Any_Half = f.Home_Win_Any_Half,
                                   Corner_Home_3_5_Over = f.Corner_Home_3_5_Over,
                                   Corner_Home_4_5_Over = f.Corner_Home_4_5_Over,
                                   Corner_Home_5_5_Over = f.Corner_Home_5_5_Over,

                                   AwayShotCount = f.AwayShotCount,
                                   AwayShotOnTargetCount = f.AwayShotOnTargetCount,
                                   AwayPossesion = f.AwayPossesion,
                                   AwayCornerCount = f.AwayCornerCount,
                                   Away_FT_0_5_Over = f.Away_FT_0_5_Over,
                                   Away_FT_1_5_Over = f.Away_FT_1_5_Over,
                                   Away_HT_0_5_Over = f.Away_HT_0_5_Over,
                                   Away_HT_1_5_Over = f.Away_HT_1_5_Over,
                                   Away_SH_0_5_Over = f.Away_SH_0_5_Over,
                                   Away_SH_1_5_Over = f.Away_SH_1_5_Over,
                                   Away_Win_Any_Half = f.Away_Win_Any_Half,
                                   Corner_Away_3_5_Over = f.Corner_Away_3_5_Over,
                                   Corner_Away_4_5_Over = f.Corner_Away_4_5_Over,
                                   Corner_Away_5_5_Over = f.Corner_Away_5_5_Over,

                                   Corner_7_5_Over = f.Corner_7_5_Over,
                                   Corner_8_5_Over = f.Corner_8_5_Over,
                                   Corner_9_5_Over = f.Corner_9_5_Over,

                                   HT_0_5_Over = f.HT_0_5_Over,
                                   HT_1_5_Over = f.HT_1_5_Over,
                                   SH_0_5_Over = f.SH_0_5_Over,
                                   SH_1_5_Over = f.SH_1_5_Over,
                                   FT_1_5_Over = f.FT_1_5_Over,
                                   FT_2_5_Over = f.FT_2_5_Over,
                                   FT_3_5_Over = f.FT_3_5_Over,
                                   FT_GG = f.FT_GG,
                                   Is_Corner_FT_Win1 = f.Is_Corner_FT_Win1,
                                   Is_Corner_FT_X = f.Is_Corner_FT_X,
                                   Is_Corner_FT_Win2 = f.Is_Corner_FT_Win2,
                                   HT_GG = f.HT_GG,
                                   SH_GG = f.SH_GG,
                                   IsCornerFound = f.IsCornerFound,
                                   IsPossesionFound = f.IsPossesionFound,
                                   IsShotFound = f.IsShotFound,
                                   IsShotOnTargetFound = f.IsShotOnTargetFound,
                                   Is_FT_Win1 = f.Is_FT_Win1,
                                   Is_FT_Win2 = f.Is_FT_Win2,
                                   Is_FT_X = f.Is_FT_X,
                                   Is_HT_Win1 = f.Is_HT_Win1,
                                   Is_HT_Win2 = f.Is_HT_Win2,
                                   Is_HT_X = f.Is_HT_X,
                                   Is_SH_Win1 = f.Is_SH_Win1,
                                   Is_SH_Win2 = f.Is_SH_Win2,
                                   Is_SH_X = f.Is_SH_X,

                                   Home_HT_GoalsCount = Convert.ToInt32(m.HT_Match_Result.Split(new char[] {'-'})[0].Trim()),
                                   Home_SH_GoalsCount = Convert.ToInt32(m.FT_Match_Result.Split(new char[] { '-' })[0].Trim()) - Convert.ToInt32(m.HT_Match_Result.Split(new char[] { '-' })[0].Trim()),
                                   Home_FT_GoalsCount = Convert.ToInt32(m.FT_Match_Result.Split(new char[] { '-' })[0].Trim()),
                                   Home_GK_SavesCount = f.HomeShotOnTargetCount - Convert.ToInt32(m.FT_Match_Result.Split(new char[] { '-' })[0].Trim()),
                                   Away_HT_GoalsCount = Convert.ToInt32(m.HT_Match_Result.Split(new char[] { '-' })[1].Trim()),
                                   Away_SH_GoalsCount = Convert.ToInt32(m.FT_Match_Result.Split(new char[] { '-' })[1].Trim()) - Convert.ToInt32(m.HT_Match_Result.Split(new char[] { '-' })[1].Trim()),
                                   Away_FT_GoalsCount = Convert.ToInt32(m.FT_Match_Result.Split(new char[] { '-' })[1].Trim()),
                                   Away_GK_SavesCount = f.AwayShotOnTargetCount - Convert.ToInt32(m.FT_Match_Result.Split(new char[] { '-' })[1].Trim())
                               }).ToListAsync();
            return await queryResult;
        }
    }
}
