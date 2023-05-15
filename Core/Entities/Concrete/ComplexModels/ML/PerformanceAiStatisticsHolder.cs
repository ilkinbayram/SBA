namespace Core.Entities.Concrete.ComplexModels.ML
{
    public class PerformanceAiStatisticsHolder
    {
        public decimal Team_Average_FT_Goals { get; set; }
        public decimal Team_Average_HT_Goals { get; set; }
        public decimal Team_Average_SH_Goals { get; set; }
        public decimal Team_Average_FT_Conceded_Goals { get; set; }
        public decimal Team_Average_HT_Conceded_Goals { get; set; }
        public decimal Team_Average_SH_Conceded_Goals { get; set; }

        public int FT_Win_Percent { get; set; }
        public int FT_Draw_Percent { get; set; }

        public int HT_Win_Percent { get; set; }
        public int HT_Draw_Percent { get; set; }

        public int SH_Win_Percent { get; set; }
        public int SH_Draw_Percent { get; set; }

        public int FT_BothTeamToScore_Percent { get; set; }
        public int SH_BothTeamToScore_Percent { get; set; }
        public int HT_BothTeamToScore_Percent { get; set; }
        public int FT_15_Over_Percent { get; set; }
        public int FT_25_Over_Percent { get; set; }
        public int FT_35_Over_Percent { get; set; }
        public int HT_05_Over_Percent { get; set; }
        public int HT_15_Over_Percent { get; set; }
        public int SH_05_Over_Percent { get; set; }
        public int SH_15_Over_Percent { get; set; }
        public int Team_HT_05_Over_Percent { get; set; }
        public int Team_HT_15_Over_Percent { get; set; }
        public int Team_SH_05_Over_Percent { get; set; }
        public int Team_SH_15_Over_Percent { get; set; }
        public int Team_FT_05_Over_Percent { get; set; }
        public int Team_FT_15_Over_Percent { get; set; }
        public int Team_Win_Any_Half_Percent { get; set; }

        public PerformanceAiMoreDetailsHolder? MoreMatchInfoDetails { get; set; }
    }

    public class PerformanceAiMoreDetailsHolder
    {
        public decimal Team_Average_FT_GK_Saves { get; set; }
        public decimal Team_Average_FT_Shot { get; set; }
        public decimal Team_Average_FT_ShotOnTarget { get; set; }
        public decimal Team_Average_FT_Corners { get; set; }
        public int FT_Corner_Win_Percent { get; set; }
        public int FT_Corner_Draw_Percent { get; set; }
        public int FT_Corner_75_Over_Percent { get; set; }
        public int FT_Corner_85_Over_Percent { get; set; }
        public int FT_Corner_95_Over_Percent { get; set; }
        public int Team_FT_Corner_35_Over_Percent { get; set; }
        public int Team_FT_Corner_45_Over_Percent { get; set; }
        public int Team_Average_BallPossesion_Percent { get; set; }
        public int Team_ShutOnTarget_Percent
        {
            get => Convert.ToInt32(this.Team_Average_FT_ShotOnTarget * 100 / this.Team_Average_FT_Shot);
        }
    }
}
