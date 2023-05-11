namespace Core.Entities.Concrete.ComplexModels.ML
{
    public class StatisticalPercentAiModel
    {
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public ComparisonAiStatisticsHolder? General_H2H_Comparison_Statistics_ByLast_10_Matches { get; set; }
        public ComparisonAiStatisticsHolder? HomeAtHome_AwayAtAway_H2H_Comparison_Statistics_ByLast_6_Matches { get; set; }
        public PerformanceAiStatisticsHolder? General_Form_Performance_Statistics_By_Last_10_Matches_Of_HomeTeam { get; set; }
        public PerformanceAiStatisticsHolder? General_Form_Performance_Statistics_By_Last_10_Matches_Of_AwayTeam { get; set; }
        public PerformanceAiStatisticsHolder? HomeAtHome_Form_Performance_Statistics_ByLast_6_Matches_Of_HomeTeam { get; set; }
        public PerformanceAiStatisticsHolder? AwayAtAway_Form_Performance_Statistics_ByLast_6_Matches_Of_AwayTeam { get; set; }
    }
}
