namespace Core.Entities.Concrete.ComplexModels.ML
{
    public class PerformanceAiStatisticsHolder
    {
        public decimal Individual_Average_FullTime_Goals { get; set; }
        public decimal Individual_Average_HalfTime_Goals { get; set; }
        public decimal Individual_Average_SecondHalf_Goals { get; set; }


        public decimal Individual_Average_FullTime_Shot { get; set; }
        public decimal Individual_Average_FullTime_ShotOnTarget { get; set; }
        public decimal Individual_Average_FullTime_Corners { get; set; }
        public int FullTime_Corner_Win_Percentage { get; set; }
        public int FullTime_Corner_Draw_Percentage { get; set; }
        public int FullTime_Corner_75_Over_Percentage { get; set; }
        public int FullTime_Corner_85_Over_Percentage { get; set; }
        public int FullTime_Corner_95_Over_Percentage { get; set; }
        public int Individual_FullTime_Corner_35_Over_Percentage { get; set; }
        public int Individual_FullTime_Corner_45_Over_Percentage { get; set; }
        public int Individual_Average_Possesion_Of_Ball { get; set; }
        public int Individual_ShutOnTarget_Percentage
        {
            get => Convert.ToInt32(this.Individual_Average_FullTime_ShotOnTarget * 100 / this.Individual_Average_FullTime_Shot);
        }

        public int FullTime_Win_Percentage { get; set; }
        public int FullTime_Draw_Percentage { get; set; }

        public int HalfTime_Win_Percentage { get; set; }
        public int HalfTime_Draw_Percentage { get; set; }

        public int SecondHalf_Win_Percentage { get; set; }
        public int SecondHalf_Draw_Percentage { get; set; }

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
        public int Individual_HalfTime_05_Over_Percentage { get; set; }
        public int Individual_HalfTime_15_Over_Percentage { get; set; }
        public int Individual_SecondHalf_05_Over_Percentage { get; set; }
        public int Individual_SecondHalf_15_Over_Percentage { get; set; }
        public int Individual_FullTime_05_Over_Percentage { get; set; }
        public int Individual_FullTime_15_Over_Percentage { get; set; }
        public int Individual_Win_Any_Half_Percentage { get; set; }
    }
}
