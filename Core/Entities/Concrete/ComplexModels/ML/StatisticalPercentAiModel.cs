namespace Core.Entities.Concrete.ComplexModels.ML
{
    public class StatisticalPercentAiModel
    {
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public ComparisonAiStatisticsHolder? General_H2H { get; set; }
        public ComparisonAiStatisticsHolder? HomeAtHome_AwayAtAway_H2H { get; set; }
        public PerformanceAiStatisticsHolder? General_Form_HomeTeam { get; set; }
        public PerformanceAiStatisticsHolder? General_Form_AwayTeam { get; set; }
        public PerformanceAiStatisticsHolder? HomeAtHome_Form_HomeTeam { get; set; }
        public PerformanceAiStatisticsHolder? AwayAtAway_Form_AwayTeam { get; set; }
    }
}
