using Core.Entities.Concrete.System;
using Core.Utilities.Results;
using System.Linq.Expressions;

namespace SBA.Business.Abstract
{
    public interface IComboBetService
    {
        IDataResult<List<ComboBet>> GetList(Expression<Func<ComboBet, bool>> filter = null);
        IDataResult<ComboBet> Get(Expression<Func<ComboBet, bool>> filter);

        IDataResult<int> Add(ComboBet entity);
        IDataResult<int> Update(ComboBet entity);

        IDataResult<int> Remove(long Id);
        IDataResult<int> AddRange(List<ComboBet> entities);
        IDataResult<int> UpdateRange(List<ComboBet> entities);
        IDataResult<int> RemoveRange(List<ComboBet> entities);
        IDataResult<IQueryable<ComboBet>> Query(Expression<Func<ComboBet, bool>> filter = null);

        Task<IDataResult<int>> RemoveRangeAsync(List<ComboBet> entities);
        Task<IDataResult<int>> UpdateRangeAsync(List<ComboBet> entities);
        Task<IDataResult<List<ComboBet>>> GetListAsync(Expression<Func<ComboBet, bool>> filter = null);
        Task<IDataResult<int>> AddRangeAsync(List<ComboBet> entities);
        Task<IDataResult<int>> UpdateAsync(ComboBet entity);
        Task<IDataResult<ComboBet>> GetAsync(Expression<Func<ComboBet, bool>> filter);
        Task<IDataResult<int>> RemoveAsync(long Id);
        Task<IDataResult<int>> AddAsync(ComboBet entity);
        Task<IDataResult<IQueryable<ComboBet>>> QueryAsync(Expression<Func<ComboBet, bool>> filter = null);
    }
}
