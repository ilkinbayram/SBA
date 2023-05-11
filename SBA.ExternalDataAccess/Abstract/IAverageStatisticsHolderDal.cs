using Core.DataAccess;
using Core.Entities.Concrete.ExternalDbEntities;
using Core.Entities.Dtos.ComplexDataes;
using Core.Entities.Dtos.ComplexDataes.MobileUI;

namespace SBA.ExternalDataAccess.Abstract
{
    public interface IAverageStatisticsHolderDal : IEntityRepository<AverageStatisticsHolder>, IEntityQueryableRepository<AverageStatisticsHolder>
    {
        AverageStatisticsMatchResult GetAverageMatchResultById(int serial, int bySideType);
    }
}
