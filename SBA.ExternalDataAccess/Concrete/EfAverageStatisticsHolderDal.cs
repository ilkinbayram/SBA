using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete.ExternalDbEntities;
using Core.Entities.Dtos.ComplexDataes;
using SBA.ExternalDataAccess.Abstract;
using SBA.ExternalDataAccess.Concrete.EntityFramework.Contexts;

namespace SBA.ExternalDataAccess.Concrete
{
    public class EfAverageStatisticsHolderDal : EfEntityRepositoryBase<AverageStatisticsHolder, ExternalAppDbContext>, IAverageStatisticsHolderDal
    {
        public EfAverageStatisticsHolderDal(ExternalAppDbContext applicationContext) : base(applicationContext)
        {
        }

        public AverageStatisticsMatchResult GetAverageMatchResultById(int serial, int bySideType)
        {
            var joinedData = (from matchId in this.Context.MatchIdentifiers
                              join statistics in this.Context.AverageStatisticsHolders on matchId.Id equals statistics.MatchIdentifierId
                              join leagueSt in this.Context.LeagueStatisticsHolders on statistics.LeagueStaisticsHolderId equals leagueSt.Id
                              where matchId.Serial == serial &&
                                    statistics.BySideType == bySideType
                              select new AverageStatisticsMatchResult
                              {
                                  LeagueCountryStatistics = leagueSt,
                                  AverageStatistics = statistics,
                                  MatchIdentity = matchId,
                              }).FirstOrDefault();
            return joinedData;
        }
    }
}
