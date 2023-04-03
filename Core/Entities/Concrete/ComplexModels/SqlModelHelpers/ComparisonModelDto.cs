using Core.Entities.Concrete.Base;

namespace Core.Entities.Concrete.ComplexModels.SqlModelHelpers
{
    public class ComparisonModelDto : BaseComparisonDto
    {
        public decimal Average_FT_Shut_HomeTeam { get; set; }
        public decimal Average_FT_Shut_AwayTeam { get; set; }
        public decimal Average_FT_ShutOnTarget_HomeTeam { get; set; }
        public decimal Average_FT_ShutOnTarget_AwayTeam { get; set; }
        public decimal Average_FT_Corners_HomeTeam { get; set; }
        public decimal Average_FT_Corners_AwayTeam { get; set; }
        public int Is_FT_CornerWin1 { get; set; }
        public int Is_FT_CornerX { get; set; }
        public int Is_FT_CornerWin2 { get; set; }
        public int FT_Corner_75_Over { get; set; }
        public int FT_Corner_85_Over { get; set; }
        public int FT_Corner_95_Over { get; set; }
        public int Home_FT_Corner_35_Over { get; set; }
        public int Home_FT_Corner_45_Over { get; set; }
        public int Away_FT_Corner_35_Over { get; set; }
        public int Away_FT_Corner_45_Over { get; set; }
        public int Home_Possesion { get; set; }
        public int Away_Possesion { get; set; }
        public int Home_ShutOnTarget_Percent
        {
            get => Convert.ToInt32(this.Average_FT_ShutOnTarget_HomeTeam * 100 / this.Average_FT_Shut_HomeTeam);
        }
        public int Away_ShutOnTarget_Percent
        {
            get => Convert.ToInt32(this.Average_FT_ShutOnTarget_AwayTeam * 100 / this.Average_FT_Shut_AwayTeam);
        }
    }
}
