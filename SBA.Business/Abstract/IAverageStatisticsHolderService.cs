using Core.Entities.Concrete.ExternalDbEntities;
using Core.Entities.Dtos.ComplexDataes;
using Core.Utilities.Results;
using System.Linq.Expressions;

namespace SBA.Business.Abstract
{
    public interface IAverageStatisticsHolderService
    {
        IDataResult<List<AverageStatisticsHolder>> GetList(Expression<Func<AverageStatisticsHolder, bool>> filter = null);
        IDataResult<AverageStatisticsHolder> Get(Expression<Func<AverageStatisticsHolder, bool>> filter);

        AverageStatisticsMatchResult GetAverageMatchResultById(int serial, int bySideType);

        IDataResult<int> Add(AverageStatisticsHolder entity);
        IDataResult<int> Update(AverageStatisticsHolder entity);
        IDataResult<int> Remove(long Id);
        IDataResult<int> AddRange(List<AverageStatisticsHolder> entities);
        IDataResult<int> UpdateRange(List<AverageStatisticsHolder> entities);
        IDataResult<int> RemoveRange(List<AverageStatisticsHolder> entities);
        IDataResult<IQueryable<AverageStatisticsHolder>> Query(Expression<Func<AverageStatisticsHolder, bool>> filter = null);

        Task<IDataResult<int>> RemoveRangeAsync(List<AverageStatisticsHolder> entities);
        Task<IDataResult<int>> UpdateRangeAsync(List<AverageStatisticsHolder> entities);
        Task<IDataResult<List<AverageStatisticsHolder>>> GetListAsync(Expression<Func<AverageStatisticsHolder, bool>> filter = null);
        Task<IDataResult<int>> AddRangeAsync(List<AverageStatisticsHolder> entities);
        Task<IDataResult<int>> UpdateAsync(AverageStatisticsHolder entity);
        Task<IDataResult<AverageStatisticsHolder>> GetAsync(Expression<Func<AverageStatisticsHolder, bool>> filter);
        Task<IDataResult<int>> RemoveAsync(long Id);
        Task<IDataResult<int>> AddAsync(AverageStatisticsHolder entity);
        Task<IDataResult<IQueryable<AverageStatisticsHolder>>> QueryAsync(Expression<Func<AverageStatisticsHolder, bool>> filter = null);
    }
}
