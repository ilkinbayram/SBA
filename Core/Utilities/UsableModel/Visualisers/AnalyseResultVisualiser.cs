namespace Core.Utilities.UsableModel.Visualisers
{
    public class AnalyseResultVisualiser
    {
        public string ExtraDetail { get; set; }
        public string HomeTeamVsAwayTeam { get; set; }
        public string TargetURL { get; set; }
        public bool Is_GG { get; set; }
        public bool Is_FT_25_Over { get; set; }
        public bool Is_FT_25_Under { get; set; }
        public bool Is_HT_15_Over{ get; set; }
        public OddAnalyseVisualiser ODD_PERCENTAGE_Visualiser { get; set; }
        public AverageVisualiser AVERAGE_Visualiser { get; set; }
        public ComparisonVisualiser COMPARISON_Visualiser { get; set; }
        public FormHomePerformanceVisualiser PERFORMANCE_HOME_Visualiser { get; set; }
        public FormAwayPerformanceVisualiser PERFORMANCE_AWAY_Visualiser { get; set; }
        public TableStandingVisualiser TABLE_Visualiser { get; set; }

        public string ZZZ_FinishDivider
        {
            get => "===============================";
        }
    }
}
