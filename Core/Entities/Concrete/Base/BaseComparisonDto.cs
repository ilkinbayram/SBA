namespace Core.Entities.Concrete.Base
{
    public class BaseComparisonDto
    {
        public decimal Average_FT_Goals_HomeTeam { get; set; }
        public decimal Average_FT_Goals_AwayTeam { get; set; }
        public decimal Average_HT_Goals_HomeTeam { get; set; }
        public decimal Average_HT_Goals_AwayTeam { get; set; }
        public decimal Average_SH_Goals_HomeTeam { get; set; }
        public decimal Average_SH_Goals_AwayTeam { get; set; }
        public decimal Average_FT_Conceded_Goals_HomeTeam { get; set; }
        public decimal Average_FT_Conceded_Goals_AwayTeam { get; set; }
        public decimal Average_HT_Conceded_Goals_HomeTeam { get; set; }
        public decimal Average_HT_Conceded_Goals_AwayTeam { get; set; }
        public decimal Average_SH_Conceded_Goals_HomeTeam { get; set; }
        public decimal Average_SH_Conceded_Goals_AwayTeam { get; set; }

        public int Is_FT_Win1 { get; set; }
        public int Is_FT_X { get; set; }
        public int Is_FT_Win2 { get; set; }

        public int Is_HT_Win1 { get; set; }
        public int Is_HT_X { get; set; }
        public int Is_HT_Win2 { get; set; }

        public int Is_SH_Win1 { get; set; }
        public int Is_SH_X { get; set; }
        public int Is_SH_Win2 { get; set; }

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
        public int Home_HT_05_Over { get; set; }
        public int Home_HT_15_Over { get; set; }
        public int Home_SH_05_Over { get; set; }
        public int Home_SH_15_Over { get; set; }
        public int Home_FT_05_Over { get; set; }
        public int Home_FT_15_Over { get; set; }
        public int Home_Win_Any_Half { get; set; }
        public int Away_HT_05_Over { get; set; }
        public int Away_HT_15_Over { get; set; }
        public int Away_SH_05_Over { get; set; }
        public int Away_SH_15_Over { get; set; }
        public int Away_FT_05_Over { get; set; }
        public int Away_FT_15_Over { get; set; }
        public int Away_Win_Any_Half { get; set; }
    }
}
