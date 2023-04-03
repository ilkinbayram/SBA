using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using Core.Entities.Concrete.SqlEntities.QueryModels;
using DataAccess.Concrete.EntityFramework.Contexts;
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
    }
}
