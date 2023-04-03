using Core.Entities.Concrete.ExternalDbEntities;

namespace Core.Entities.Dtos.ComplexDataes
{
    public class AverageStatisticsMatchResult
    {
        public LeagueStatisticsHolder LeagueCountryStatistics { get; set; }
        public AverageStatisticsHolder AverageStatistics { get; set; }
        public MatchIdentifier MatchIdentity { get; set; }
    }
}
