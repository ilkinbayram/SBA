using Core.Entities.Concrete.System;
using Core.Utilities.Results;
using System.Linq.Expressions;

namespace SBA.Business.Abstract
{
    public interface IBundleService
    {
        IDataResult<List<Bundle>> GetList(Expression<Func<Bundle, bool>> filter = null);
        IDataResult<Bundle> Get(Expression<Func<Bundle, bool>> filter);

        IDataResult<int> Add(Bundle entity);
        IDataResult<int> Update(Bundle entity);

        IDataResult<int> Remove(long Id);
        IDataResult<int> AddRange(List<Bundle> entities);
        IDataResult<int> UpdateRange(List<Bundle> entities);
        IDataResult<int> RemoveRange(List<Bundle> entities);
        IDataResult<IQueryable<Bundle>> Query(Expression<Func<Bundle, bool>> filter = null);

        Task<IDataResult<int>> RemoveRangeAsync(List<Bundle> entities);
        Task<IDataResult<int>> UpdateRangeAsync(List<Bundle> entities);
        Task<IDataResult<List<Bundle>>> GetListAsync(Expression<Func<Bundle, bool>> filter = null);
        Task<IDataResult<int>> AddRangeAsync(List<Bundle> entities);
        Task<IDataResult<int>> UpdateAsync(Bundle entity);
        Task<IDataResult<Bundle>> GetAsync(Expression<Func<Bundle, bool>> filter);
        Task<IDataResult<int>> RemoveAsync(long Id);
        Task<IDataResult<int>> AddAsync(Bundle entity);
        Task<IDataResult<IQueryable<Bundle>>> QueryAsync(Expression<Func<Bundle, bool>> filter = null);
    }
}
