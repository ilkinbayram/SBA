using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete.ExternalDbEntities;
using Core.Entities.Dtos.ComplexDataes.MobileUI;
using Core.Resources.Enums;
using SBA.ExternalDataAccess.Abstract;
using SBA.ExternalDataAccess.Concrete.EntityFramework.Contexts;
using SBA.ExternalDataAccess.Mapping;

namespace SBA.ExternalDataAccess.Concrete
{
    public class EfStatisticInfoHolderDal : EfEntityRepositoryBase<StatisticInfoHolder, ExternalAppDbContext>, IStatisticInfoHolderDal
    {
        public EfStatisticInfoHolderDal(ExternalAppDbContext applicationContext) : base(applicationContext)
        {
        }

        public StatisticInfoContainer GetAllStatisticResultById(int serial, int lang)
        {
            var result = (from matchId in this.Context.MatchIdentifiers
                              join avgStat in this.Context.AverageStatisticsHolders on matchId.Id equals avgStat.MatchIdentifierId
                              join leagueSt in this.Context.LeagueStatisticsHolders on avgStat.LeagueStaisticsHolderId equals leagueSt.Id
                              where matchId.Serial == serial && avgStat.BySideType == 1
                              select new StatisticInfoContainer
                              {
                                  Serial = serial,
                                  MatchTime = matchId.MatchDateTime.ToString("HH:mm"),
                                  HomeTeam = matchId.HomeTeam,
                                  AwayTeam = matchId.AwayTeam,
                                  LeagueStatistic = new LeagueStatisticInfo
                                  {
                                      CountFound = leagueSt.CountFound,
                                      CountryName = leagueSt.CountryName,
                                      LeagueName = leagueSt.LeagueName,
                                      FT_GoalsAverage = leagueSt.FT_GoalsAverage,
                                      FT_Over15_Percentage = leagueSt.FT_Over15_Percentage,
                                      FT_Over25_Percentage = leagueSt.FT_Over25_Percentage,
                                      FT_Over35_Percentage = leagueSt.FT_Over35_Percentage,
                                      GG_Percentage = leagueSt.GG_Percentage,
                                      HT_GoalsAverage = leagueSt.HT_GoalsAverage,
                                      HT_Over05_Percentage = leagueSt.HT_Over05_Percentage,
                                      HT_Over15_Percentage = leagueSt.HT_Over15_Percentage,
                                      SH_GoalsAverage = leagueSt.SH_GoalsAverage,
                                      SH_Over05_Percentage = leagueSt.SH_Over05_Percentage,
                                      SH_Over15_Percentage = leagueSt.SH_Over15_Percentage
                                  }
                              }).FirstOrDefault();
            result.AverageBySideStatistics = Context.StatisticInfoes.Where(x => x.Serial == serial && x.StatisticType == (int)StatisticType.Average && x.BySideType == (int)BySideType.HomeAway && (x.HomePercent > 0 || x.AwayPercent > 0) && (!(x.HomePercent < 0 || x.HomePercent < 0))).ToList().MapList(lang);
            result.AverageGeneralStatistics = Context.StatisticInfoes.Where(x => x.Serial == serial && x.StatisticType == (int)StatisticType.Average && x.BySideType == (int)BySideType.General && (x.HomePercent > 0 || x.AwayPercent > 0) && (!(x.HomePercent < 0 || x.HomePercent < 0))).ToList().MapList(lang);
            result.ComparisonBySideStatistics = Context.StatisticInfoes.Where(x => x.Serial == serial && x.StatisticType == (int)StatisticType.Comparison && x.BySideType == (int)BySideType.HomeAway && (x.HomePercent > 0 || x.AwayPercent > 0) && (!(x.HomePercent < 0 || x.HomePercent < 0))).ToList().MapList(lang);
            result.ComparisonGeneralStatistics = Context.StatisticInfoes.Where(x => x.Serial == serial && x.StatisticType == (int)StatisticType.Comparison && x.BySideType == (int)BySideType.General && (x.HomePercent > 0 || x.AwayPercent > 0) && (!(x.HomePercent < 0 || x.HomePercent < 0))).ToList().MapList(lang);
            result.PerformanceBySideStatistics = Context.StatisticInfoes.Where(x => x.Serial == serial && x.StatisticType == (int)StatisticType.Performance && x.BySideType == (int)BySideType.HomeAway && (x.HomePercent > 0 || x.AwayPercent > 0) && (!(x.HomePercent < 0 || x.HomePercent < 0))).ToList().MapList(lang);
            result.PerformanceGeneralStatistics = Context.StatisticInfoes.Where(x => x.Serial == serial && x.StatisticType == (int)StatisticType.Performance && x.BySideType == (int)BySideType.General && (x.HomePercent > 0 || x.AwayPercent > 0) && (!(x.HomePercent < 0 || x.HomePercent < 0))).ToList().MapList(lang);

            return result;
        }

        public StatisticInfoContainer GetAverageStatisticResultById(int serial, int bySideType, int lang)
        {
            var joinedData = (from matchId in this.Context.MatchIdentifiers
                              join avgStat in this.Context.AverageStatisticsHolders on matchId.Id equals avgStat.MatchIdentifierId
                              join leagueSt in this.Context.LeagueStatisticsHolders on avgStat.LeagueStaisticsHolderId equals leagueSt.Id
                              where matchId.Serial == serial && avgStat.BySideType == bySideType
                              select new StatisticInfoContainer
                              {
                                  Serial = serial,
                                  MatchTime = matchId.MatchDateTime.ToString("HH:mm"),
                                  HomeTeam = matchId.HomeTeam,
                                  AwayTeam = matchId.AwayTeam,
                                  LeagueStatistic = new LeagueStatisticInfo
                                  {
                                      CountFound = leagueSt.CountFound,
                                      CountryName = leagueSt.CountryName,
                                      LeagueName = leagueSt.LeagueName,
                                      FT_GoalsAverage = leagueSt.FT_GoalsAverage,
                                      FT_Over15_Percentage = leagueSt.FT_Over15_Percentage,
                                      FT_Over25_Percentage = leagueSt.FT_Over25_Percentage,
                                      FT_Over35_Percentage = leagueSt.FT_Over35_Percentage,
                                      GG_Percentage = leagueSt.GG_Percentage,
                                      HT_GoalsAverage = leagueSt.HT_GoalsAverage,
                                      HT_Over05_Percentage = leagueSt.HT_Over05_Percentage,
                                      HT_Over15_Percentage = leagueSt.HT_Over15_Percentage,
                                      SH_GoalsAverage = leagueSt.SH_GoalsAverage,
                                      SH_Over05_Percentage = leagueSt.SH_Over05_Percentage,
                                      SH_Over15_Percentage = leagueSt.SH_Over15_Percentage
                                  },
                                  StatisticInfoes = Context.StatisticInfoes.Where(x => x.ParentId == avgStat.UniqueIdentity).ToList().MapList(lang)
                              }).FirstOrDefault();

            return joinedData;
        }

        public StatisticInfoContainer GetComparisonStatisticResultById(int serial, int bySideType, int lang)
        {
            var result = (from matchId in Context.MatchIdentifiers
                          join compStat in Context.ComparisonStatisticsHolders on matchId.Id equals compStat.MatchIdentifierId
                          join leagueSt in Context.LeagueStatisticsHolders on compStat.LeagueStaisticsHolderId equals leagueSt.Id
                          where matchId.Serial == serial && compStat.BySideType == bySideType
                          select new StatisticInfoContainer
                          {
                              Serial = serial,
                              MatchTime = matchId.MatchDateTime.ToString("HH:mm"),
                              HomeTeam = matchId.HomeTeam,
                              AwayTeam = matchId.AwayTeam,
                              LeagueStatistic = new LeagueStatisticInfo
                              {
                                  CountFound = leagueSt.CountFound,
                                  CountryName = leagueSt.CountryName,
                                  LeagueName = leagueSt.LeagueName,
                                  FT_GoalsAverage = leagueSt.FT_GoalsAverage,
                                  FT_Over15_Percentage = leagueSt.FT_Over15_Percentage,
                                  FT_Over25_Percentage = leagueSt.FT_Over25_Percentage,
                                  FT_Over35_Percentage = leagueSt.FT_Over35_Percentage,
                                  GG_Percentage = leagueSt.GG_Percentage,
                                  HT_GoalsAverage = leagueSt.HT_GoalsAverage,
                                  HT_Over05_Percentage = leagueSt.HT_Over05_Percentage,
                                  HT_Over15_Percentage = leagueSt.HT_Over15_Percentage,
                                  SH_GoalsAverage = leagueSt.SH_GoalsAverage,
                                  SH_Over05_Percentage = leagueSt.SH_Over05_Percentage,
                                  SH_Over15_Percentage = leagueSt.SH_Over15_Percentage
                              },
                              StatisticInfoes = Context.StatisticInfoes.Where(x => x.ParentId == compStat.UniqueIdentity).ToList().MapList(lang)
                          }).FirstOrDefault();
            return result;
        }

        public StatisticInfoContainer GetPerformanceStatisticResultById(int serial, int bySideType, int lang)
        {
            var joinedData = (from matchId in this.Context.MatchIdentifiers
                              join perfStat in this.Context.ComparisonStatisticsHolders on matchId.Id equals perfStat.MatchIdentifierId
                              join leagueSt in this.Context.LeagueStatisticsHolders on perfStat.LeagueStaisticsHolderId equals leagueSt.Id
                              where matchId.Serial == serial && perfStat.BySideType == bySideType
                              select new StatisticInfoContainer
                              {
                                  Serial = serial,
                                  MatchTime = matchId.MatchDateTime.ToString("HH:mm"),
                                  HomeTeam = matchId.HomeTeam,
                                  AwayTeam = matchId.AwayTeam,
                                  LeagueStatistic = new LeagueStatisticInfo
                                  {
                                      CountFound = leagueSt.CountFound,
                                      CountryName = leagueSt.CountryName,
                                      LeagueName = leagueSt.LeagueName,
                                      FT_GoalsAverage = leagueSt.FT_GoalsAverage,
                                      FT_Over15_Percentage = leagueSt.FT_Over15_Percentage,
                                      FT_Over25_Percentage = leagueSt.FT_Over25_Percentage,
                                      FT_Over35_Percentage = leagueSt.FT_Over35_Percentage,
                                      GG_Percentage = leagueSt.GG_Percentage,
                                      HT_GoalsAverage = leagueSt.HT_GoalsAverage,
                                      HT_Over05_Percentage = leagueSt.HT_Over05_Percentage,
                                      HT_Over15_Percentage = leagueSt.HT_Over15_Percentage,
                                      SH_GoalsAverage = leagueSt.SH_GoalsAverage,
                                      SH_Over05_Percentage = leagueSt.SH_Over05_Percentage,
                                      SH_Over15_Percentage = leagueSt.SH_Over15_Percentage
                                  },
                                  StatisticInfoes = Context.StatisticInfoes.Where(x => x.ParentId == perfStat.UniqueIdentity).ToList().MapList(lang)
                              }).FirstOrDefault();

            return joinedData;
        }
    }
}
