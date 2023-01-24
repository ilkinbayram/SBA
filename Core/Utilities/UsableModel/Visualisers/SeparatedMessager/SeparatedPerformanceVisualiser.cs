namespace Core.Utilities.UsableModel.Visualisers.SeparatedMessager
{
    public class SeparatedPerformanceVisualiser
    {
        public FormHomePerformanceVisualiser PERFORMANCE_HOME_Visualiser { get; set; }
        public FormAwayPerformanceVisualiser PERFORMANCE_AWAY_Visualiser { get; set; }
        public TableStandingVisualiser TABLE_Visualiser { get; set; }

        public string ZZZ_FinishDivider
        {
            get => "===============================";
        }
    }
}
