using Core.DataAccess;
using Core.Entities.Concrete.ExternalDbEntities;
using Core.Entities.Dtos.ComplexDataes.UIData;

namespace SBA.ExternalDataAccess.Abstract
{
    public interface IForecastDal : IEntityRepository<Forecast>, IEntityQueryableRepository<Forecast>
    {
        Task<int> AddPossibleForecastsAsync(List<PossibleForecast> possibleForecasts);

        Task<ForecastDataContainer> SelectForecastContainerInfoAsync(bool isCheckedItems);
    }
}
