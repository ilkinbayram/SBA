using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete.ExternalDbEntities;
using SBA.ExternalDataAccess.Abstract;
using SBA.ExternalDataAccess.Concrete.EntityFramework.Contexts;

namespace SBA.ExternalDataAccess.Concrete
{
    public class EfPossibleForecastDal : EfEntityRepositoryBase<PossibleForecast, ExternalAppDbContext>, IPossibleForecastDal
    {
        public EfPossibleForecastDal(ExternalAppDbContext applicationContext) : base(applicationContext)
        {
        }
    }
}
