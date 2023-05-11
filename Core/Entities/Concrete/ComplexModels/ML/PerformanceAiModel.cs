namespace Core.Entities.Concrete.ComplexModels.ML
{
    public class PerformanceAiModel
    {
        public List<TeamAiPerformanceHolder> HomePerformance { get; set; }
        public List<TeamAiPerformanceHolder> AwayPerformance { get; set; }
    }
}
