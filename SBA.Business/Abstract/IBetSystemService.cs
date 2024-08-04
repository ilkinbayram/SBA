using Core.Entities.Concrete.System;
using Core.Entities.Dtos.SystemModels;
using Core.Utilities.Results;
using System.Linq.Expressions;

namespace SBA.Business.Abstract
{
    public interface IBetSystemService
    {
        IDataResult<List<BetSystem>> GetList(Expression<Func<BetSystem, bool>> filter = null);
        IDataResult<BetSystem> Get(Expression<Func<BetSystem, bool>> filter);

        IDataResult<int> Add(CreateSystemDto createDto);
        IDataResult<int> Update(BetSystem entity);

        IDataResult<int> Remove(long Id);
        IDataResult<int> AddRange(List<BetSystem> entities);
        IDataResult<int> UpdateRange(List<BetSystem> entities);
        IDataResult<int> RemoveRange(List<BetSystem> entities);
        IDataResult<IQueryable<BetSystem>> Query(Expression<Func<BetSystem, bool>> filter = null);


        Task<IDataResult<int>> RemoveRangeAsync(List<BetSystem> entities);
        Task<IDataResult<int>> UpdateRangeAsync(List<BetSystem> entities);
        Task<IDataResult<List<BetSystem>>> GetListAsync(Expression<Func<BetSystem, bool>> filter = null);
        Task<IDataResult<int>> AddRangeAsync(List<BetSystem> entities);
        Task<IDataResult<int>> UpdateAsync(BetSystem entity);
        Task<IDataResult<BetSystem>> GetAsync(Expression<Func<BetSystem, bool>> filter);
        Task<IDataResult<int>> RemoveAsync(long Id);
        Task<IDataResult<int>> AddAsync(BetSystem entity);
        Task<IDataResult<IQueryable<BetSystem>>> QueryAsync(Expression<Func<BetSystem, bool>> filter = null);
    }
}
