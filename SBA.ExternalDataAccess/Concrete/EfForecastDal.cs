using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete.ExternalDbEntities;
using SBA.ExternalDataAccess.Abstract;
using SBA.ExternalDataAccess.Concrete.EntityFramework.Contexts;

namespace SBA.ExternalDataAccess.Concrete
{
    public class EfForecastDal : EfEntityRepositoryBase<Forecast, ExternalAppDbContext>, IForecastDal
    {
        public EfForecastDal(ExternalAppDbContext applicationContext) : base(applicationContext)
        {
        }
    }
}
