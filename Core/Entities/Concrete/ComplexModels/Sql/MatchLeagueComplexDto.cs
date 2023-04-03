using Core.Entities.Concrete.ComplexModels.SqlModelHelpers;

namespace Core.Entities.Concrete.ComplexModels.Sql
{
    public class MatchLeagueComplexDto
    {
        public MatchModelDto Match { get; set; }
        public LeagueModelDto LeagueStat { get; set; }
        public ComparisonModelDto ComparisonHomeAway { get; set; }
        public ComparisonModelDto ComparisonGeneral { get; set; }
        public PerformanceModelDto HomeTeamPerformanceAtHome { get; set; }
        public PerformanceModelDto AwayTeamPerformanceAtAway { get; set; }
        public PerformanceModelDto HomeTeamPerformanceGeneral { get; set; }
        public PerformanceModelDto AwayTeamPerformanceGeneral { get; set; }
    }
}
