namespace Core.Entities.Concrete.ComplexModels.ML
{
    public class ComparisonAiStatisticsHolder
    {
        public decimal Average_FullTime_Goals_HomeTeam { get; set; }
        public decimal Average_FullTime_Goals_AwayTeam { get; set; }
        public decimal Average_HalfTime_Goals_HomeTeam { get; set; }
        public decimal Average_HalfTime_Goals_AwayTeam { get; set; }
        public decimal Average_SecondHalf_Goals_HomeTeam { get; set; }
        public decimal Average_SecondHalf_Goals_AwayTeam { get; set; }

        public int FullTime_Home_Win_Percentage { get; set; }
        public int FullTime_Draw_Percentage { get; set; }
        public int FullTime_Away_Win_Percentage { get; set; }

        public int HalfTime_Home_Win_Percentage { get; set; }
        public int HalfTime_Draw_Percentage { get; set; }
        public int HalfTime_Away_Win_Percentage { get; set; }

        public int SecondHalf_Home_Win_Percentage { get; set; }
        public int SecondHalf_Draw_Percentage { get; set; }
        public int SecondHalf_Away_Win_Percentage { get; set; }

        public int FullTime_BothTeamToScore_Percentage { get; set; }
        public int SecondHalf_BothTeamToScore_Percentage { get; set; }
        public int HalfTime_BothTeamToScore_Percentage { get; set; }
        public int FullTime_15_Over_Percentage { get; set; }
        public int FullTime_25_Over_Percentage { get; set; }
        public int FullTime_35_Over_Percentage { get; set; }
        public int HalfTime_05_Over_Percentage { get; set; }
        public int HalfTime_15_Over_Percentage { get; set; }
        public int SecondHalf_05_Over_Percentage { get; set; }
        public int SecondHalf_15_Over_Percentage { get; set; }
        public int Home_HalfTime_05_Over_Percentage { get; set; }
        public int Home_HalfTime_15_Over_Percentage { get; set; }
        public int Home_SecondHalf_05_Over_Percentage { get; set; }
        public int Home_SecondHalf_15_Over_Percentage { get; set; }
        public int Home_FullTime_05_Over_Percentage { get; set; }
        public int Home_FullTime_15_Over_Percentage { get; set; }
        public int Home_Win_Any_Half_Percentage { get; set; }
        public int Away_HalfTime_05_Over_Percentage { get; set; }
        public int Away_HalfTime_15_Over_Percentage { get; set; }
        public int Away_SecondHalf_05_Over_Percentage { get; set; }
        public int Away_SecondHalf_15_Over_Percentage { get; set; }
        public int Away_FullTime_05_Over_Percentage { get; set; }
        public int Away_FullTime_15_Over_Percentage { get; set; }
        public int Away_Win_Any_Half_Percentage { get; set; }
    }
}
