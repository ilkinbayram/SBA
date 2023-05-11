using Core.DataAccess;
using Core.Entities.Concrete.ExternalDbEntities;

namespace SBA.ExternalDataAccess.Abstract
{
    public interface IForecastDal : IEntityRepository<Forecast>, IEntityQueryableRepository<Forecast>
    {
    }
}
