namespace Core.Entities.Concrete.ComplexModels.SqlModelHelpers
{
    public class LeagueModelDto
    {
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
