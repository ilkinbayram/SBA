using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete.ComplexModels.Program;
using Core.Entities.Concrete.ExternalDbEntities;
using SBA.ExternalDataAccess.Abstract;
using SBA.ExternalDataAccess.Concrete.EntityFramework.Contexts;

namespace SBA.ExternalDataAccess.Concrete
{
    public class EfMatchIdentifierDal : EfEntityRepositoryBase<MatchIdentifier, ExternalAppDbContext>, IMatchIdentifierDal
    {
        public EfMatchIdentifierDal(ExternalAppDbContext applicationContext) : base(applicationContext)
        {
        }

        public MatchProgramList GetMatchsProgram()
        {
            var result = (from mid in Context.MatchIdentifiers
                          join cmpHA in Context.ComparisonStatisticsHolders on mid.Id equals cmpHA.MatchIdentifierId
                          where cmpHA.BySideType == 1 && mid.MatchDateTime.Date == DateTime.Now.Date
                          join lg in Context.LeagueStatisticsHolders on cmpHA.LeagueStaisticsHolderId equals lg.Id
                          group mid by new { lg.CountryName, lg.LeagueName } into matchGroup
                          select new MatchProgram
                          {
                              Country = matchGroup.Key.CountryName,
                              League = matchGroup.Key.LeagueName,
                              Matches = matchGroup.Select(m => new Match
                              {
                                  Serial = m.Serial,
                                  HomeTeam = m.HomeTeam,
                                  AwayTeam = m.AwayTeam,
                                  MatchTime = m.MatchDateTime.ToString("HH:mm")
                              }).ToList()
                          }).ToList();

            return new MatchProgramList { Matches = result };
        }
    }
}
