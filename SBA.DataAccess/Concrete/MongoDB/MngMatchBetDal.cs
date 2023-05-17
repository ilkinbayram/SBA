using Core.DataAccess.Mongo;
using Core.Entities.Concrete;
using Core.Entities.Concrete.ComplexModels.Sql;
using Core.Entities.Concrete.SqlEntities.QueryModels;
using Core.Utilities.UsableModel;
using MongoDB.Driver;
using SBA.DataAccess.Abstract;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SBA.DataAccess.Concrete.MongoDB
{
    public class MngMatchBetDal : MongoBaseRepository<MatchBet>, IMatchBetDal
    {
        public MngMatchBetDal() : base("MatchBets")
        {
        }

        public IQueryable<MatchBetQM> GetMatchBetFilterResultQueryModels(Expression<Func<MatchBetQM, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public IQueryable<MatchBetQM> GetMatchBetQueryModels(string countryName, string teamName, int takeCount, Expression<Func<MatchBetQM, bool>> filter = null)
        {
            var queryableMatchBets = Database.GetCollection<MatchBet>("MatchBets").AsQueryable();
            var query = from mb in queryableMatchBets
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
                            HT_Match_Result = mb.HT_Match_Result
                        };

            query = filter == null
                    ? query.Take(takeCount)
                    : query.Where(filter).Take(takeCount);

            return query;
        }

        public IQueryable<MatchBetQM> GetMatchBetQueryModelsForPerformanceResult(string countryName, string teamName, int takeCount, Expression<Func<MatchBetQM, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<FilterResultMutateModel> GetOddFilteredResult(InTimeShortOddModel inTimeOdds, decimal range)
        {
            throw new NotImplementedException();
        }

        public Task<List<FilterResultMutateModel>> GetOddFilteredResultAsync(InTimeShortOddModel inTimeOdds, decimal range)
        {
            throw new NotImplementedException();
        }
    }
}
