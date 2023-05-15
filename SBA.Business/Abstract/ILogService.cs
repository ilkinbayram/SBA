using Core.Entities.Concrete;
using Core.Utilities.Results;
using System.Linq.Expressions;

namespace SBA.Business.Abstract
{
    public interface ILogService
    {
        IDataResult<List<Log>> GetList(Expression<Func<Log, bool>> filter = null);
        IDataResult<Log> Get(Expression<Func<Log, bool>> filter);

        IDataResult<int> Add(Log entity);
        IDataResult<int> Update(Log entity);
        IDataResult<int> Remove(long Id);
        IDataResult<int> AddRange(List<Log> entities);
        IDataResult<int> UpdateRange(List<Log> entities);
        IDataResult<int> RemoveRange(List<Log> entities);
        IDataResult<IQueryable<Log>> Query(Expression<Func<Log, bool>> filter = null);

        Task<IDataResult<int>> RemoveRangeAsync(List<Log> entities);
        Task<IDataResult<int>> UpdateRangeAsync(List<Log> entities);
        Task<IDataResult<List<Log>>> GetListAsync(Expression<Func<Log, bool>> filter = null);
        Task<IDataResult<int>> AddRangeAsync(List<Log> entities);
        Task<IDataResult<int>> UpdateAsync(Log entity);
        Task<IDataResult<Log>> GetAsync(Expression<Func<Log, bool>> filter);
        Task<IDataResult<int>> RemoveAsync(long Id);
        Task<IDataResult<int>> AddAsync(Log entity);
        Task<IDataResult<IQueryable<Log>>> QueryAsync(Expression<Func<Log, bool>> filter = null);
    }
}
