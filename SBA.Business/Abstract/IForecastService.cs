using Core.Entities.Concrete.ExternalDbEntities;
using Core.Entities.Concrete.SqlEntities.FunctionViewProcModels;
using Core.Entities.Dtos.ComplexDataes.UIData;
using Core.Utilities.Results;
using System.Linq.Expressions;

namespace SBA.Business.Abstract
{
    public interface IForecastService
    {
        IDataResult<List<Forecast>> GetList(Expression<Func<Forecast, bool>> filter = null);
        IDataResult<Forecast> Get(Expression<Func<Forecast, bool>> filter);

        IDataResult<int> Add(Forecast entity);
        IDataResult<int> Update(Forecast entity);
        IDataResult<int> Remove(long Id);
        IDataResult<int> AddRange(List<Forecast> entities);
        IDataResult<int> UpdateRange(List<Forecast> entities);
        IDataResult<int> RemoveRange(List<Forecast> entities);
        IDataResult<IQueryable<Forecast>> Query(Expression<Func<Forecast, bool>> filter = null);

        Task<int> AddPossibleForecastsAsync(List<PossibleForecast> possibleForecasts);
        Task<ForecastDataContainer> SelectForecastContainerInfoAsync(bool isCheckedItems, Func<MatchForecastFM, bool> filter = null);

        Task<List<string>> SelectForecastsBySerialAsync(int serial);

        Task<IDataResult<int>> RemoveRangeAsync(List<Forecast> entities);
        Task<IDataResult<int>> UpdateRangeAsync(List<Forecast> entities);
        Task<IDataResult<List<Forecast>>> GetListAsync(Expression<Func<Forecast, bool>> filter = null);
        Task<IDataResult<int>> AddRangeAsync(List<Forecast> entities);
        Task<IDataResult<int>> UpdateAsync(Forecast entity);
        Task<IDataResult<Forecast>> GetAsync(Expression<Func<Forecast, bool>> filter);
        Task<IDataResult<int>> RemoveAsync(long Id);
        Task<IDataResult<int>> AddAsync(Forecast entity);
        Task<IDataResult<IQueryable<Forecast>>> QueryAsync(Expression<Func<Forecast, bool>> filter = null);
    }
}
