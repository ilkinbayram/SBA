using Core.DataAccess;
using Core.Entities.Concrete.ExternalDbEntities;

namespace SBA.ExternalDataAccess.Abstract
{
    internal interface IPossibleForecastDal : IEntityRepository<PossibleForecast>, IEntityQueryableRepository<PossibleForecast>
    {
    }
}
