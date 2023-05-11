using Core.DataAccess;
using Core.Entities.Concrete.ExternalDbEntities;
using Core.Entities.Dtos.ComplexDataes.MobileUI;

namespace SBA.ExternalDataAccess.Abstract
{
    public interface IStatisticInfoHolderDal : IEntityRepository<StatisticInfoHolder>, IEntityQueryableRepository<StatisticInfoHolder>
    {
        public StatisticInfoContainer GetAverageStatisticResultById(int serial, int bySideType, int lang);
        public StatisticInfoContainer GetComparisonStatisticResultById(int serial, int bySideType, int lang);
        public StatisticInfoContainer GetPerformanceStatisticResultById(int serial, int bySideType, int lang);
        StatisticInfoContainer GetAllStatisticResultById(int serial, int lang);
    }
}
