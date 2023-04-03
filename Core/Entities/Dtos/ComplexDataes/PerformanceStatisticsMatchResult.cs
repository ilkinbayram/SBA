using Core.Entities.Concrete.ExternalDbEntities;

namespace Core.Entities.Dtos.ComplexDataes
{
    public class PerformanceStatisticsMatchResult
    {
        public LeagueStatisticsHolder LeagueCountryStatistics { get; set; }
        public TeamPerformanceStatisticsHolder TeamPerformanceStatistics { get; set; }
        public MatchIdentifier MatchIdentity { get; set; }
    }
}
