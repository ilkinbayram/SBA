using Core.Concrete.Base;

namespace Core.Entities.Concrete
{
    public class PerformanceOverall : BaseEntity, IEntity
    {
        public decimal Average_FT_Goals_Home_Team { get; set; }
        public decimal Average_FT_Goals_Away_Team { get; set; }
        public decimal Average_HT_Goals_Home_Team { get; set; }
        public decimal Average_HT_Goals_Away_Team { get; set; }
        public decimal Average_SH_Goals_Home_Team { get; set; }
        public decimal Average_SH_Goals_Away_Team { get; set; }
        public decimal Average_FT_Conceded_Goals_Home_Team { get; set; }
        public decimal Average_FT_Conceded_Goals_Away_Team { get; set; }
        public decimal Average_HT_Conceded_Goals_Home_Team { get; set; }
        public decimal Average_HT_Conceded_Goals_Away_Team { get; set; }
        public decimal Average_SH_Conceded_Goals_Home_Team { get; set; }
        public decimal Average_SH_Conceded_Goals_Away_Team { get; set; }
        public decimal Average_FT_GK_Saves_Home_Team { get; set; }
        public decimal Average_FT_GK_Saves_Away_Team { get; set; }
        public decimal Average_FT_Shut_Home_Team { get; set; }
        public decimal Average_FT_Shut_Away_Team { get; set; }
        public decimal Average_FT_ShutOnTarget_Home_Team { get; set; }
        public decimal Average_FT_ShutOnTarget_Away_Team { get; set; }
        public int Home_Team_Possesion { get; set; }
        public int Away_Team_Possesion { get; set; }
        public int Is_FT_Win1 { get; set; }
        public int Is_FT_Win2 { get; set; }
        public int Is_FT_X1 { get; set; }
        public int Is_FT_X2 { get; set; }
        public int Is_HT_Win1 { get; set; }
        public int Is_HT_Win2 { get; set; }
        public int Is_HT_X1 { get; set; }
        public int Is_HT_X2 { get; set; }
        public int Is_SH_Win1 { get; set; }
        public int Is_SH_Win2 { get; set; }
        public int Is_SH_X1 { get; set; }
        public int Is_SH_X2 { get; set; }
        public int FT_GG_Home { get; set; }
        public int FT_GG_Away { get; set; }
        public int FT_15_Over_Home { get; set; }
        public int FT_25_Over_Home { get; set; }
        public int FT_35_Over_Home { get; set; }
        public int HT_05_Over_Home { get; set; }
        public int SH_05_Over_Home { get; set; }
        public int FT_15_Over_Away { get; set; }
        public int FT_25_Over_Away { get; set; }
        public int FT_35_Over_Away { get; set; }
        public int HT_05_Over_Away { get; set; }
        public int SH_05_Over_Away { get; set; }
        public int Home_Team_HT_05_Over { get; set; }
        public int Home_Team_SH_05_Over { get; set; }
        public int Home_Team_FT_05_Over { get; set; }
        public int Home_Team_FT_15_Over { get; set; }
        public int Away_Team_HT_05_Over { get; set; }
        public int Away_Team_SH_05_Over { get; set; }
        public int Away_Team_FT_05_Over { get; set; }
        public int Away_Team_FT_15_Over { get; set; }
        public int Home_Team_Win_Any_Half { get; set; }
        public int Away_Team_Win_Any_Half { get; set; }
        public int SerialUniqueID { get; set; }
        public DateTime MatchDate { get; set; }
        public string Hashed_Full_Detailed { get; set; }
        public string Hashed_Detailed { get; set; }
        public string Hashed_Compact { get; set; }
        public string Hashed_Simple { get; set; }
        public string Hashed_Less_Content { get; set; }
    }
}
