using Core.DataAccess;
using Core.Entities.Concrete;
using Core.Entities.Concrete.ComplexModels.Sql;
using Core.Entities.Concrete.SqlEntities.QueryModels;
using Core.Utilities.UsableModel;
using System.Linq.Expressions;

namespace SBA.DataAccess.Abstract
{
    public interface IMatchBetDal : IEntityRepository<MatchBet>, IEntityQueryableRepository<MatchBet>
    {
        IQueryable<MatchBetQM> GetMatchBetQueryModels(string countryName, string teamName, int takeCount, Expression<Func<MatchBetQM, bool>> filter = null);

        IQueryable<MatchBetQM> GetMatchBetQueryModelsForPerformanceResult(string countryName, string teamName, int takeCount, Expression<Func<MatchBetQM, bool>> filter = null);

        IQueryable<MatchBetQM> GetMatchBetFilterResultQueryModels(Expression<Func<MatchBetQM, bool>> filter = null);
        Task<List<FilterResultMutateModel>> GetOddFilteredResultAsync(InTimeShortOddModel inTimeOdds, decimal range);
    }
}
