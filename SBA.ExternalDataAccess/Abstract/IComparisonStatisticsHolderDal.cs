using Core.DataAccess;
using Core.Entities.Concrete.ExternalDbEntities;
using Core.Entities.Dtos.ComplexDataes;

namespace SBA.ExternalDataAccess.Abstract
{
    public interface IComparisonStatisticsHolderDal : IEntityRepository<ComparisonStatisticsHolder>, IEntityQueryableRepository<ComparisonStatisticsHolder>
    {
        ComparisonStatisticsMatchResult GetComparisonMatchResultById(int serial, int bySideType);
    }
}
