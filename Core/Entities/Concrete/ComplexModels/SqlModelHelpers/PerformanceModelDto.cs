namespace Core.Entities.Concrete.ComplexModels.SqlModelHelpers
{
    public class PerformanceModelDto
    {
        public decimal Average_FT_Goals_Team { get; set; }
        public decimal Average_HT_Goals_Team { get; set; }
        public decimal Average_SH_Goals_Team { get; set; }


        public decimal Average_FT_Shut_Team { get; set; }
        public decimal Average_FT_ShutOnTarget_Team { get; set; }
        public decimal Average_FT_Corners_Team { get; set; }
        public int Is_FT_CornerWinTeam { get; set; }
        public int Is_FT_CornerX { get; set; }
        public int FT_Corner_75_Over { get; set; }
        public int FT_Corner_85_Over { get; set; }
        public int FT_Corner_95_Over { get; set; }
        public int Team_FT_Corner_35_Over { get; set; }
        public int Team_FT_Corner_45_Over { get; set; }
        public int Team_Possesion { get; set; }
        public int Team_ShutOnTarget_Percent
        {
            get => Convert.ToInt32(this.Average_FT_ShutOnTarget_Team * 100 / this.Average_FT_Shut_Team);
        }

        public int Is_FT_Win { get; set; }
        public int Is_FT_X { get; set; }

        public int Is_HT_Win { get; set; }
        public int Is_HT_X { get; set; }

        public int Is_SH_Win { get; set; }
        public int Is_SH_X { get; set; }

        public int FT_GG { get; set; }
        public int SH_GG { get; set; }
        public int HT_GG { get; set; }
        public int FT_15_Over { get; set; }
        public int FT_25_Over { get; set; }
        public int FT_35_Over { get; set; }
        public int HT_05_Over { get; set; }
        public int HT_15_Over { get; set; }
        public int SH_05_Over { get; set; }
        public int SH_15_Over { get; set; }
        public int Team_HT_05_Over { get; set; }
        public int Team_HT_15_Over { get; set; }
        public int Team_SH_05_Over { get; set; }
        public int Team_SH_15_Over { get; set; }
        public int Team_FT_05_Over { get; set; }
        public int Team_FT_15_Over { get; set; }
        public int Team_Win_Any_Half { get; set; }
    }
}
