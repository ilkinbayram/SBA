using Core.DataAccess;
using Core.Entities.Concrete.ComplexModels.RequestModelHelpers;
using Core.Entities.Concrete.ComplexModels.Sql;
using Core.Entities.Concrete.ExternalDbEntities;
using Core.Resources.Enums;

namespace SBA.ExternalDataAccess.Abstract
{
    public interface ILeagueStatisticsHolderDal : IEntityRepository<LeagueStatisticsHolder>, IEntityQueryableRepository<LeagueStatisticsHolder>
    {
        MatchLeagueComplexDto GetAiComplexStatistics(int serial);
        ComparisonResponseModel GetComparisonStatistics(int serial, int bySide);
        PerformanceResponseModel GetPerformanceStatistics(int serial, int bySide, int homeOrAway);
        LeagueStatisticsResponseModel GetLeagueStatistics(int serial);
    }
}
