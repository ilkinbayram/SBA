namespace Core.Entities.Dtos.ComplexDataes.MobileUI
{
    public class LeagueStatisticInfo
    {
        public int CountFound { get; set; }

        public string CountryName { get; set; }
        public string LeagueName { get; set; }
        public decimal FT_GoalsAverage { get; set; }
        public decimal HT_GoalsAverage { get; set; }
        public decimal SH_GoalsAverage { get; set; }
        public int GG_Percentage { get; set; }
        public int FT_Over15_Percentage { get; set; }
        public int FT_Over25_Percentage { get; set; }
        public int FT_Over35_Percentage { get; set; }
        public int HT_Over05_Percentage { get; set; }
        public int HT_Over15_Percentage { get; set; }
        public int SH_Over05_Percentage { get; set; }
        public int SH_Over15_Percentage { get; set; }
    }
}
