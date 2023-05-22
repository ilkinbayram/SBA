using Core.Entities.Concrete.ExternalDbEntities;
using Core.Utilities.Results;
using System.Linq.Expressions;

namespace SBA.Business.Abstract
{
    public interface IPossibleForecastService
    {
        IDataResult<List<PossibleForecast>> GetList(Expression<Func<PossibleForecast, bool>> filter = null);
        IDataResult<PossibleForecast> Get(Expression<Func<PossibleForecast, bool>> filter);

        IDataResult<int> Add(PossibleForecast entity);
        IDataResult<int> Update(PossibleForecast entity);
        IDataResult<int> Remove(long Id);
        IDataResult<int> AddRange(List<PossibleForecast> entities);
        IDataResult<int> UpdateRange(List<PossibleForecast> entities);
        IDataResult<int> RemoveRange(List<PossibleForecast> entities);
        IDataResult<IQueryable<PossibleForecast>> Query(Expression<Func<PossibleForecast, bool>> filter = null);

        Task<IDataResult<int>> RemoveRangeAsync(List<PossibleForecast> entities);
        Task<IDataResult<int>> UpdateRangeAsync(List<PossibleForecast> entities);
        Task<IDataResult<List<PossibleForecast>>> GetListAsync(Expression<Func<PossibleForecast, bool>> filter = null);
        Task<IDataResult<int>> AddRangeAsync(List<PossibleForecast> entities);
        Task<IDataResult<int>> UpdateAsync(PossibleForecast entity);
        Task<IDataResult<PossibleForecast>> GetAsync(Expression<Func<PossibleForecast, bool>> filter);
        Task<IDataResult<int>> RemoveAsync(long Id);
        Task<IDataResult<int>> AddAsync(PossibleForecast entity);
        Task<IDataResult<IQueryable<PossibleForecast>>> QueryAsync(Expression<Func<PossibleForecast, bool>> filter = null);
    }
}
