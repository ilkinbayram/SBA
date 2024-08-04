using Core.Entities.Concrete.System;
using Core.Utilities.Results;
using System.Linq.Expressions;

namespace SBA.Business.Abstract
{
    public interface IPredictionService
    {
        IDataResult<List<Prediction>> GetList(Expression<Func<Prediction, bool>> filter = null);
        IDataResult<Prediction> Get(Expression<Func<Prediction, bool>> filter);

        IDataResult<int> Add(Prediction entity);
        IDataResult<int> Update(Prediction entity);

        IDataResult<int> Remove(long Id);
        IDataResult<int> AddRange(List<Prediction> entities);
        IDataResult<int> UpdateRange(List<Prediction> entities);
        IDataResult<int> RemoveRange(List<Prediction> entities);
        IDataResult<IQueryable<Prediction>> Query(Expression<Func<Prediction, bool>> filter = null);

        Task<IDataResult<int>> RemoveRangeAsync(List<Prediction> entities);
        Task<IDataResult<int>> UpdateRangeAsync(List<Prediction> entities);
        Task<IDataResult<List<Prediction>>> GetListAsync(Expression<Func<Prediction, bool>> filter = null);
        Task<IDataResult<int>> AddRangeAsync(List<Prediction> entities);
        Task<IDataResult<int>> UpdateAsync(Prediction entity);
        Task<IDataResult<Prediction>> GetAsync(Expression<Func<Prediction, bool>> filter);
        Task<IDataResult<int>> RemoveAsync(long Id);
        Task<IDataResult<int>> AddAsync(Prediction entity);
        Task<IDataResult<IQueryable<Prediction>>> QueryAsync(Expression<Func<Prediction, bool>> filter = null);
    }
}
