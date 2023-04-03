using Core.Entities.Concrete.ExternalDbEntities;

namespace Core.Entities.Concrete.ComplexModels.ML
{
    public class AiAnalyseModel
    {
        public string Sport { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public DateTime MatchDateTime { get; set; }

        public LeagueStatisticsAiModel League_Statistics_For_Current_Month { get; set; }
        public ComparisonAiModel H2H_Comparison { get; set; }
        public PerformanceAiModel Form_Performance_Teams { get; set; }
    }
}
