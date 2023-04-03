using Core.Entities.Concrete.ExternalDbEntities;
using Core.Entities.Dtos.ComplexDataes;
using Core.Utilities.Results;
using System.Linq.Expressions;

namespace SBA.Business.Abstract
{
    public interface IComparisonStatisticsHolderService
    {
        IDataResult<List<ComparisonStatisticsHolder>> GetList(Expression<Func<ComparisonStatisticsHolder, bool>> filter = null);
        IDataResult<ComparisonStatisticsHolder> Get(Expression<Func<ComparisonStatisticsHolder, bool>> filter);
        ComparisonStatisticsMatchResult GetComparisonMatchResultById(int serial, int bySideType);

        IDataResult<int> Add(ComparisonStatisticsHolder entity);
        IDataResult<int> Update(ComparisonStatisticsHolder entity);
        IDataResult<int> Remove(long Id);
        IDataResult<int> AddRange(List<ComparisonStatisticsHolder> entities);
        IDataResult<int> UpdateRange(List<ComparisonStatisticsHolder> entities);
        IDataResult<int> RemoveRange(List<ComparisonStatisticsHolder> entities);
        IDataResult<IQueryable<ComparisonStatisticsHolder>> Query(Expression<Func<ComparisonStatisticsHolder, bool>> filter = null);

        Task<IDataResult<int>> RemoveRangeAsync(List<ComparisonStatisticsHolder> entities);
        Task<IDataResult<int>> UpdateRangeAsync(List<ComparisonStatisticsHolder> entities);
        Task<IDataResult<List<ComparisonStatisticsHolder>>> GetListAsync(Expression<Func<ComparisonStatisticsHolder, bool>> filter = null);
        Task<IDataResult<int>> AddRangeAsync(List<ComparisonStatisticsHolder> entities);
        Task<IDataResult<int>> UpdateAsync(ComparisonStatisticsHolder entity);
        Task<IDataResult<ComparisonStatisticsHolder>> GetAsync(Expression<Func<ComparisonStatisticsHolder, bool>> filter);
        Task<IDataResult<int>> RemoveAsync(long Id);
        Task<IDataResult<int>> AddAsync(ComparisonStatisticsHolder entity);
        Task<IDataResult<IQueryable<ComparisonStatisticsHolder>>> QueryAsync(Expression<Func<ComparisonStatisticsHolder, bool>> filter = null);
    }
}