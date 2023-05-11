using Core.Entities.Concrete.ExternalDbEntities;
using Core.Utilities.UsableModel;

namespace Core.Entities.Concrete.ComplexModels.ML
{
    public class AiAnalyseModel
    {
        public MatchDataAiModel MatchDataes { get; set; }
        public LeagueStatisticsAiModel? LeagueStatistics { get; set; }
        public StandingAiModel? StandingInfoes { get; set; }
        public List<ComparisonAiModel> ComparisonDataes { get; set; }
        public List<TeamAiPerformanceHolder> HomeTeamPerformanceMatches { get; set; }
        public List<TeamAiPerformanceHolder> AwayTeamPerformanceMatches { get; set; }
        public StatisticalPercentAiModel StatisticPercentageModel { get; set; }
    }
}
