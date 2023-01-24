namespace Core.Utilities.UsableModel
{
    public class VirtualOddAnalyseInTimeModel
    {
        public VirtualOddAnalyseInTimeModel()
        {
            Is_GG = new InTimeModelResultContainer();
            Is_25_Over = new InTimeModelResultContainer();
            Is_HT_15_Over = new InTimeModelResultContainer();
            Is_35_Over = new InTimeModelResultContainer();
            Is_35_Under = new InTimeModelResultContainer();
            Is_Goals_23 = new InTimeModelResultContainer();
            Is_Score_1_1 = new InTimeModelResultContainer();
        }

        public string Serial { get; set; }
        public string TargetUrl { get; set; }
        public string TeamVsTeam { get; set; }
        public InTimeModelResultContainer Is_GG { get; set; }
        public InTimeModelResultContainer Is_25_Over { get; set; }
        public InTimeModelResultContainer Is_HT_15_Over { get; set; }
        public InTimeModelResultContainer Is_35_Over { get; set; }
        public InTimeModelResultContainer Is_35_Under { get; set; }
        public InTimeModelResultContainer Is_Goals_23 { get; set; }
        public InTimeModelResultContainer Is_Score_1_1 { get; set; }

    }
}
