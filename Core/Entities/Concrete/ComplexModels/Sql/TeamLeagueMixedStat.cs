namespace Core.Entities.Concrete.ComplexModels.Sql
{
    public class TeamLeagueMixedStat
    {
        public decimal Average_FT_Goals_HomeTeam { get; set; }
        public decimal Average_FT_Goals_AwayTeam { get; set; }
        public decimal Average_FT_Conceeded_Goals_HomeTeam { get; set; }
        public decimal Average_FT_Conceeded_Goals_AwayTeam { get; set; }
        public decimal Average_HT_Goals_HomeTeam { get; set; }
        public decimal Average_HT_Goals_AwayTeam { get; set; }
        public decimal Average_HT_Conceeded_Goals_HomeTeam { get; set; }
        public decimal Average_HT_Conceeded_Goals_AwayTeam { get; set; }
        public decimal Average_SH_Goals_HomeTeam { get; set; }
        public decimal Average_SH_Goals_AwayTeam { get; set; }
        public decimal Average_SH_Conceeded_Goals_HomeTeam { get; set; }
        public decimal Average_SH_Conceeded_Goals_AwayTeam { get; set; }
        public decimal ShutSaveHomeTeam { get; set; }
        public decimal ShutSaveAwayTeam { get; set; }
        public int HomeInd_FT_05_Over { get; set; }
        public int AwayInd_FT_05_Over { get; set; }
        public int HomeInd_FT_15_Over { get; set; }
        public int AwayInd_FT_15_Over { get; set; }
        public int HomeInd_HT_05_Over { get; set; }
        public int AwayInd_HT_05_Over { get; set; }
        public int HomeInd_SH_05_Over { get; set; }
        public int AwayInd_SH_05_Over { get; set; }
        public int FT_GG_Home { get; set; }
        public int FT_GG_Away { get; set; }
        public int FT_15_Over_Home { get; set; }
        public int FT_15_Over_Away { get; set; }
        public int FT_25_Over_Home { get; set; }
        public int FT_25_Over_Away { get; set; }
        public int FT_35_Over_Home { get; set; }
        public int FT_35_Over_Away { get; set; }
        public int HT_05_Over_Home { get; set; }
        public int HT_05_Over_Away { get; set; }
        public int HT_15_Over_Home { get; set; }
        public int HT_15_Over_Away { get; set; }
        public int SH_05_Over_Home { get; set; }
        public int SH_05_Over_Away { get; set; }
        public int SH_15_Over_Home { get; set; }
        public int SH_15_Over_Away { get; set; }
        public decimal League_FT_GoalsAverage { get; set; }
        public decimal League_HT_GoalsAverage { get; set; }
        public decimal League_SH_GoalsAverage { get; set; }
        public int League_GG_Percentage { get; set; }
        public int League_FT_Over15_Percentage { get; set; }
        public int League_FT_Over25_Percentage { get; set; }
        public int League_FT_Over35_Percentage { get; set; }
        public int League_HT_Over05_Percentage { get; set; }
        public int League_HT_Over15_Percentage { get; set; }
        public int League_SH_Over05_Percentage { get; set; }
        public int League_SH_Over15_Percentage { get; set; }
    }
}
