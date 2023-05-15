namespace Core.Entities.Concrete.ComplexModels.ML
{
    public class ComparisonAiStatisticsHolder
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

        public int FT_Home_Win_Percent { get; set; }
        public int FT_Draw_Percent { get; set; }
        public int FT_Away_Win_Percent { get; set; }

        public int HT_Home_Win_Percent { get; set; }
        public int HT_Draw_Percent { get; set; }
        public int HT_Away_Win_Percent { get; set; }

        public int SH_Home_Win_Percent { get; set; }
        public int SH_Draw_Percent { get; set; }
        public int SH_Away_Win_Percent { get; set; }

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
        public int Home_HT_05_Over_Percent { get; set; }
        public int Home_HT_15_Over_Percent { get; set; }
        public int Home_SH_05_Over_Percent { get; set; }
        public int Home_SH_15_Over_Percent { get; set; }
        public int Home_FT_05_Over_Percent { get; set; }
        public int Home_FT_15_Over_Percent { get; set; }
        public int Home_Win_Any_Half_Percent { get; set; }
        public int Away_HT_05_Over_Percent { get; set; }
        public int Away_HT_15_Over_Percent { get; set; }
        public int Away_SH_05_Over_Percent { get; set; }
        public int Away_SH_15_Over_Percent { get; set; }
        public int Away_FT_05_Over_Percent { get; set; }
        public int Away_FT_15_Over_Percent { get; set; }
        public int Away_Win_Any_Half_Percent { get; set; }
    }
}
