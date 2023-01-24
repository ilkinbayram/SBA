using Core.DataAccess;
using Core.Entities.Concrete;
using Core.Entities.Concrete.SqlEntities.QueryModels;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SBA.DataAccess.Abstract
{
    public interface IMatchBetDal : IEntityRepository<MatchBet>, IEntityQueryableRepository<MatchBet>
    {
        IQueryable<MatchBetQM> GetMatchBetQueryModels(string countryName, string teamName, int takeCount, Expression<Func<MatchBetQM, bool>> filter = null);
    }
}
