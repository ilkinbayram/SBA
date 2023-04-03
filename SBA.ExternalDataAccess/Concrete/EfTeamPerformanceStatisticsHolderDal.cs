using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete.ExternalDbEntities;
using Core.Entities.Dtos.ComplexDataes;
using SBA.ExternalDataAccess.Abstract;
using SBA.ExternalDataAccess.Concrete.EntityFramework.Contexts;

namespace SBA.ExternalDataAccess.Concrete
{
    public class EfTeamPerformanceStatisticsHolderDal : EfEntityRepositoryBase<TeamPerformanceStatisticsHolder, ExternalAppDbContext>, ITeamPerformanceStatisticsHolderDal
    {
        public EfTeamPerformanceStatisticsHolderDal(ExternalAppDbContext applicationContext) : base(applicationContext)
        {
        }

        public PerformanceStatisticsMatchResult GetPerformanceMatchResultById(int serial, int bySideType, int homeOrAway)
        {
            var joinedData = (from matchId in this.Context.MatchIdentifiers
                              join statistics in this.Context.TeamPerformanceStatisticsHolders on matchId.Id equals statistics.MatchIdentifierId
                              join leagueSt in this.Context.LeagueStatisticsHolders on statistics.LeagueStaisticsHolderId equals leagueSt.Id
                              where matchId.Serial == serial &&
                                    statistics.BySideType == bySideType &&
                                    statistics.HomeOrAway == homeOrAway
                              select new PerformanceStatisticsMatchResult
                              {
                                  LeagueCountryStatistics = leagueSt,
                                  TeamPerformanceStatistics = statistics,
                                  MatchIdentity = matchId,
                              }).FirstOrDefault();
            return joinedData;
        }
    }
}
