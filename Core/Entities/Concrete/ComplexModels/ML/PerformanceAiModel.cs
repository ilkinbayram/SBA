namespace Core.Entities.Concrete.ComplexModels.ML
{
    public class PerformanceAiModel
    {
        public HomeTeamAiPerformanceHolder HomeTeam { get; set; }
        public AwayTeamAiPerformanceHolder AwayTeam { get; set; }
    }
}
