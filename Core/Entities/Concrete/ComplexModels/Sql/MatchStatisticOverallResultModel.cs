namespace Core.Entities.Concrete.ComplexModels.Sql
{
    public class MatchStatisticOverallResultModel
    {
        public decimal Average_Home_FT_Goals { get; set; }
        public decimal Average_Home_HT_Goals { get; set; }
        public decimal Average_Home_SH_Goals { get; set; }
        public decimal Average_Home_FT_Conceded_Goals { get; set; }
        public decimal Average_Home_HT_Conceded_Goals { get; set; }
        public decimal Average_Home_SH_Conceded_Goals { get; set; }

        public decimal Average_Away_FT_Goals { get; set; }
        public decimal Average_Away_HT_Goals { get; set; }
        public decimal Average_Away_SH_Goals { get; set; }
        public decimal Average_Away_FT_Conceded_Goals { get; set; }
        public decimal Average_Away_HT_Conceded_Goals { get; set; }
        public decimal Average_Away_SH_Conceded_Goals { get; set; }

        public decimal Average_Home_FT_GK_Saves { get; set; }
        public decimal Average_Home_FT_Shuts { get; set; }
        public decimal Average_Home_FT_Shuts_ON_Target { get; set; }
        public int Average_Home_TeamPossesionPercent { get; set; }

        public decimal Average_Away_FT_GK_Saves { get; set; }
        public decimal Average_Away_FT_Shuts { get; set; }
        public decimal Average_Away_FT_Shuts_ON_Target { get; set; }
        public int Average_Away_TeamPossesionPercent { get; set; }

        public decimal Average_Home_FT_WinPercent { get; set; }
        public decimal Average_Home_HT_WinPercent { get; set; }
        public decimal Average_Home_SH_WinPercent { get; set; }
        public decimal Average_FT_DrawPercent { get; set; }
        public decimal Average_HT_DrawPercent { get; set; }
        public decimal Average_SH_DrawPercent { get; set; }

        public decimal Average_Away_FT_WinPercent { get; set; }
        public decimal Average_Away_HT_WinPercent { get; set; }
        public decimal Average_Away_SH_WinPercent { get; set; }

        public decimal Average_GG_Percent { get; set; }
        public decimal Average_FT_1_5_Over_Percent { get; set; }
        public decimal Average_FT_2_5_Over_Percent { get; set; }
        public decimal Average_FT_3_5_Over_Percent { get; set; }
        public decimal Average_HT_0_5_Over_Percent { get; set; }
        public decimal Average_SH_0_5_Over_Percent { get; set; }

        public decimal Average_Home_IND_FT_0_5_Over_Percent { get; set; }
        public decimal Average_Home_IND_FT_1_5_Over_Percent { get; set; }
        public decimal Average_Home_IND_HT_0_5_Over_Percent { get; set; }
        public decimal Average_Home_IND_SH_0_5_Over_Percent { get; set; }
        public decimal Average_Home_WinAnyHalf_Percent { get; set; }

        public decimal Average_Away_IND_FT_0_5_Over_Percent { get; set; }
        public decimal Average_Away_IND_FT_1_5_Over_Percent { get; set; }
        public decimal Average_Away_IND_HT_0_5_Over_Percent { get; set; }
        public decimal Average_Away_IND_SH_0_5_Over_Percent { get; set; }
        public decimal Average_Away_WinAnyHalf_Percent { get; set; }

        public int FoundMatchCount { get; set; }
    }
}
