using Core.DataAccess;
using Core.Entities.Concrete.ExternalDbEntities;
using Core.Entities.Dtos.ComplexDataes;

namespace SBA.ExternalDataAccess.Abstract
{
    public interface ITeamPerformanceStatisticsHolderDal : IEntityRepository<TeamPerformanceStatisticsHolder>, IEntityQueryableRepository<TeamPerformanceStatisticsHolder>
    {
        PerformanceStatisticsMatchResult GetPerformanceMatchResultById(int serial, int bySideType, int homeOrAway);
    }
}
