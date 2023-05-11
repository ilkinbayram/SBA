using Core.Entities.Concrete.ExternalDbEntities;
using Core.Entities.Dtos.ComplexDataes.MobileUI;
using Core.Utilities.Results;
using System.Linq.Expressions;

namespace SBA.Business.Abstract
{
    public interface IStatisticInfoHolderService
    {
        IDataResult<List<StatisticInfoHolder>> GetList(Expression<Func<StatisticInfoHolder, bool>> filter = null);
        IDataResult<StatisticInfoHolder> Get(Expression<Func<StatisticInfoHolder, bool>> filter);

        StatisticInfoContainer GetAverageStatisticResultById(int serial, int bySideType, int lang);
        StatisticInfoContainer GetComparisonStatisticResultById(int serial, int bySideType, int lang);
        StatisticInfoContainer GetPerformanceStatisticResultById(int serial, int bySideType, int lang);
        StatisticInfoContainer GetAllStatisticResultById(int serial, int lang);

        IDataResult<int> Add(StatisticInfoHolder entity);
        IDataResult<int> Update(StatisticInfoHolder entity);
        IDataResult<int> Remove(long Id);
        IDataResult<int> AddRange(List<StatisticInfoHolder> entities);
        IDataResult<int> UpdateRange(List<StatisticInfoHolder> entities);
        IDataResult<int> RemoveRange(List<StatisticInfoHolder> entities);
        IDataResult<IQueryable<StatisticInfoHolder>> Query(Expression<Func<StatisticInfoHolder, bool>> filter = null);

        Task<IDataResult<int>> RemoveRangeAsync(List<StatisticInfoHolder> entities);
        Task<IDataResult<int>> UpdateRangeAsync(List<StatisticInfoHolder> entities);
        Task<IDataResult<List<StatisticInfoHolder>>> GetListAsync(Expression<Func<StatisticInfoHolder, bool>> filter = null);
        Task<IDataResult<int>> AddRangeAsync(List<StatisticInfoHolder> entities);
        Task<IDataResult<int>> UpdateAsync(StatisticInfoHolder entity);
        Task<IDataResult<StatisticInfoHolder>> GetAsync(Expression<Func<StatisticInfoHolder, bool>> filter);
        Task<IDataResult<int>> RemoveAsync(long Id);
        Task<IDataResult<int>> AddAsync(StatisticInfoHolder entity);
        Task<IDataResult<IQueryable<StatisticInfoHolder>>> QueryAsync(Expression<Func<StatisticInfoHolder, bool>> filter = null);
    }
}
