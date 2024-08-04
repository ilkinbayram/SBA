using Core.Entities.Concrete.System;
using Core.Utilities.Results;
using System.Linq.Expressions;

namespace SBA.Business.Abstract
{
    public interface ISavedStepService
    {
        IDataResult<List<SavedStep>> GetList(Expression<Func<SavedStep, bool>> filter = null);
        IDataResult<SavedStep> Get(Expression<Func<SavedStep, bool>> filter);

        IDataResult<int> Add(SavedStep entity);
        IDataResult<int> Update(SavedStep entity);

        IDataResult<int> Remove(long Id);
        IDataResult<int> AddRange(List<SavedStep> entities);
        IDataResult<int> UpdateRange(List<SavedStep> entities);
        IDataResult<int> RemoveRange(List<SavedStep> entities);
        IDataResult<IQueryable<SavedStep>> Query(Expression<Func<SavedStep, bool>> filter = null);

        Task<IDataResult<int>> RemoveRangeAsync(List<SavedStep> entities);
        Task<IDataResult<int>> UpdateRangeAsync(List<SavedStep> entities);
        Task<IDataResult<List<SavedStep>>> GetListAsync(Expression<Func<SavedStep, bool>> filter = null);
        Task<IDataResult<int>> AddRangeAsync(List<SavedStep> entities);
        Task<IDataResult<int>> UpdateAsync(SavedStep entity);
        Task<IDataResult<SavedStep>> GetAsync(Expression<Func<SavedStep, bool>> filter);
        Task<IDataResult<int>> RemoveAsync(long Id);
        Task<IDataResult<int>> AddAsync(SavedStep entity);
        Task<IDataResult<IQueryable<SavedStep>>> QueryAsync(Expression<Func<SavedStep, bool>> filter = null);
    }
}
