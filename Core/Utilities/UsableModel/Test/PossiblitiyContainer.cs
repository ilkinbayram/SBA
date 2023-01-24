namespace Core.Utilities.UsableModel.Test
{
    public class PossiblitiyContainer
    {
        public PossiblitiyTestingModel FT_25_Over_Testing { get; set; }
        public PossiblitiyTestingModel FT_35_Over_Testing { get; set; }
        public PossiblitiyTestingModel FT_35_Under_Testing { get; set; }
        public PossiblitiyTestingModel HT_15_Over_Testing { get; set; }
        public PossiblitiyTestingModel FT_Goals_23_Testing { get; set; }
        public PossiblitiyTestingModel FT_Score_1_1_Testing { get; set; }
        public PossiblitiyTestingModel FT_GG_Testing { get; set; }
    }

    public class PossiblitiyTestingModel
    {
        public int CountFound { get; set; }
        public int ResultPercentage { get; set; }
    }
}
