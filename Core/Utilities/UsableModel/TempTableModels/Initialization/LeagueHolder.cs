namespace Core.Utilities.UsableModel.TempTableModels.Initialization
{
    public class LeagueHolder
    {
        public int CountFound { get; set; }
        public string Country { get; set; }
        public string League { get; set; }
        

        public decimal GoalsAverage { get; set; }
        public decimal HT_GoalsAverage { get; set; }
        public decimal SH_GoalsAverage { get; set; }
        public int GG_Percentage { get; set; }
        public int Over_2_5_Percentage { get; set; }
        public int Over_1_5_Percentage { get; set; }
        public int Over_3_5_Percentage { get; set; }
        
        public int HT_Over_1_5_Percentage { get; set; }
        public int HT_Over_0_5_Percentage { get; set; }
        public int SH_Over_1_5_Percentage { get; set; }
        public int SH_Over_0_5_Percentage { get; set; }

        public int HomeFT_05_Over_Percentage { get; set; }
        public int HomeHT_05_Over_Percentage { get; set; }
        public decimal HomeFT_GoalsAverage { get; set; }
        public decimal HomeHT_GoalsAverage { get; set; }

        public int AwayFT_05_Over_Percentage { get; set; }
        public int AwayHT_05_Over_Percentage { get; set; }
        public decimal AwayFT_GoalsAverage { get; set; }
        public decimal AwayHT_GoalsAverage { get; set; }

        public int Corner_Over_7_5_Percentage { get; set; }
        public int Corner_Over_8_5_Percentage { get; set; }
        public int Corner_Over_9_5_Percentage { get; set; }
        public decimal CornerAverage { get; set; }
        public decimal HomeCornerAverage { get; set; }
        public decimal AwayCornerAverage { get; set; }
        public int Home_Corner_35_Over_Percentage { get; set; }
        public int Home_Corner_45_Over_Percentage { get; set; }
        public int Home_Corner_55_Over_Percentage { get; set; }
        public int Away_Corner_35_Over_Percentage { get; set; }
        public int Away_Corner_45_Over_Percentage { get; set; }
        public int Away_Corner_55_Over_Percentage { get; set; }
        public int Corner_FT_Win1_Percentage { get; set; }
        public int Corner_FT_X_Percentage { get; set; }
        public int Corner_FT_Win2_Percentage { get; set; }
    }
}
