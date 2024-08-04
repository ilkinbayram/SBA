using Core.Entities.Concrete.System;
using Core.Utilities.Results;
using System.Linq.Expressions;

namespace SBA.Business.Abstract
{
    public interface IStepService
    {
        IDataResult<List<Step>> GetList(Expression<Func<Step, bool>> filter = null);
        IDataResult<Step> Get(Expression<Func<Step, bool>> filter);

        IDataResult<int> Add(Step entity);
        IDataResult<int> Update(Step entity);

        IDataResult<int> Remove(long Id);
        IDataResult<int> AddRange(List<Step> entities);
        IDataResult<int> UpdateRange(List<Step> entities);
        IDataResult<int> RemoveRange(List<Step> entities);
        IDataResult<IQueryable<Step>> Query(Expression<Func<Step, bool>> filter = null);

        Task<IDataResult<int>> RemoveRangeAsync(List<Step> entities);
        Task<IDataResult<int>> UpdateRangeAsync(List<Step> entities);
        Task<IDataResult<List<Step>>> GetListAsync(Expression<Func<Step, bool>> filter = null);
        Task<IDataResult<int>> AddRangeAsync(List<Step> entities);
        Task<IDataResult<int>> UpdateAsync(Step entity);
        Task<IDataResult<Step>> GetAsync(Expression<Func<Step, bool>> filter);
        Task<IDataResult<int>> RemoveAsync(long Id);
        Task<IDataResult<int>> AddAsync(Step entity);
        Task<IDataResult<IQueryable<Step>>> QueryAsync(Expression<Func<Step, bool>> filter = null);
    }
}
