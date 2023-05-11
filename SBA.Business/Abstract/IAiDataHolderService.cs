using Core.Entities.Concrete.ExternalDbEntities;
using Core.Utilities.Results;
using System.Linq.Expressions;

namespace SBA.Business.Abstract
{
    public interface IAiDataHolderService
    {
        IDataResult<List<AiDataHolder>> GetList(Expression<Func<AiDataHolder, bool>> filter = null);
        IDataResult<AiDataHolder> Get(Expression<Func<AiDataHolder, bool>> filter);

        IDataResult<int> Add(AiDataHolder entity);
        IDataResult<int> Update(AiDataHolder entity);
        IDataResult<int> Remove(long Id);
        IDataResult<int> AddRange(List<AiDataHolder> entities);
        IDataResult<int> UpdateRange(List<AiDataHolder> entities);
        IDataResult<int> RemoveRange(List<AiDataHolder> entities);
        IDataResult<IQueryable<AiDataHolder>> Query(Expression<Func<AiDataHolder, bool>> filter = null);

        Task<IDataResult<int>> RemoveRangeAsync(List<AiDataHolder> entities);
        Task<IDataResult<int>> UpdateRangeAsync(List<AiDataHolder> entities);
        Task<IDataResult<List<AiDataHolder>>> GetListAsync(Expression<Func<AiDataHolder, bool>> filter = null);
        Task<IDataResult<int>> AddRangeAsync(List<AiDataHolder> entities);
        Task<IDataResult<int>> UpdateAsync(AiDataHolder entity);
        Task<IDataResult<AiDataHolder>> GetAsync(Expression<Func<AiDataHolder, bool>> filter);
        Task<IDataResult<int>> RemoveAsync(long Id);
        Task<IDataResult<int>> AddAsync(AiDataHolder entity);
        Task<IDataResult<IQueryable<AiDataHolder>>> QueryAsync(Expression<Func<AiDataHolder, bool>> filter = null);
    }
}
