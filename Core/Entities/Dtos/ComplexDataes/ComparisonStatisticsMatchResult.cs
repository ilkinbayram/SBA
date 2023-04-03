using Core.Entities.Concrete.ExternalDbEntities;

namespace Core.Entities.Dtos.ComplexDataes
{
    public class ComparisonStatisticsMatchResult
    {
        public LeagueStatisticsHolder LeagueCountryStatistics { get; set; }
        public ComparisonStatisticsHolder ComparisonStatistics { get; set; }
        public MatchIdentifier MatchIdentity { get; set; }
    }
}
