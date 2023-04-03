using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete.ExternalDbEntities;
using Core.Entities.Dtos.ComplexDataes;
using SBA.ExternalDataAccess.Abstract;
using SBA.ExternalDataAccess.Concrete.EntityFramework.Contexts;

namespace SBA.ExternalDataAccess.Concrete
{
    public class EfComparisonStatisticsHolderDal : EfEntityRepositoryBase<ComparisonStatisticsHolder, ExternalAppDbContext>, IComparisonStatisticsHolderDal
    {
        public EfComparisonStatisticsHolderDal(ExternalAppDbContext applicationContext) : base(applicationContext)
        {
        }

        public ComparisonStatisticsMatchResult GetComparisonMatchResultById(int serial, int bySideType)
        {
            var joinedData = (from matchId in this.Context.MatchIdentifiers
                              join statistics in this.Context.ComparisonStatisticsHolders on matchId.Id equals statistics.MatchIdentifierId
                              join leagueSt in this.Context.LeagueStatisticsHolders on statistics.LeagueStaisticsHolderId equals leagueSt.Id
                              where matchId.Serial == serial &&
                                    statistics.BySideType == bySideType
                              select new ComparisonStatisticsMatchResult
                              {
                                  LeagueCountryStatistics = leagueSt,
                                  ComparisonStatistics = statistics,
                                  MatchIdentity = matchId,
                              }).FirstOrDefault();
            return joinedData;
        }
    }
}
