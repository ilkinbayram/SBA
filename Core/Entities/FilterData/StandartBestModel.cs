namespace Core.Entities.FilterData
{
    public class StandartBestModel
    {
        public decimal FTWin1_Odd { get; set; }
        public decimal FTDraw_Odd { get; set; }
        public decimal FTWin2_Odd { get; set; }
        public decimal HTWin1_Odd { get; set; }
        public decimal HTDraw_Odd { get; set; }
        public decimal HTWin2_Odd { get; set; }
        public decimal HT_Under_1_5_Odd { get; set; }
        public decimal HT_Over_1_5_Odd { get; set; }
        public decimal FT_Under_2_5_Odd { get; set; }
        public decimal FT_Over_2_5_Odd { get; set; }
        public decimal FT_GG_Odd { get; set; }
        public decimal FT_NG_Odd { get; set; }
        public decimal FT_01_Odd { get; set; }
        public decimal FT_23_Odd { get; set; }
        public decimal FT_45_Odd { get; set; }
        public decimal FT_6_Odd { get; set; }
        public string HT_Result { get; set; }
        public string FT_Result { get; set; }
        public string HT_FT_Result { get; set; }
        public string HT_1_5_Over { get; set; }
        public string FT_2_5_Over { get; set; }
        public string FT_3_5_Over { get; set; }
        public string FT_GG { get; set; }
        public string FT_TotalBetween { get; set; }
        public int CountFound { get; set; }
        public string HT_2_5_Over { get; set; }
        public string SH_2_5_Over { get; set; }
        public string FT_4_5_Over { get; set; }
    }
}
