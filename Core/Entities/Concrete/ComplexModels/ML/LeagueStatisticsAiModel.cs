namespace Core.Entities.Concrete.ComplexModels.ML
{
    public class LeagueStatisticsAiModel
    {
        public string CountryName { get; set; }
        public string LeagueName { get; set; }
        public decimal FullTime_Goals_Average { get; set; }
        public decimal HalfTime_Goals_Average { get; set; }
        public decimal SecondHald_Goals_Average { get; set; }

        public int BothTeamsToScore_Percentage { get; set; }
        public int FullTime_Over15_Percentage { get; set; }
        public int FullTime_Over25_Percentage { get; set; }
        public int FullTime_Over35_Percentage { get; set; }
        public int HalfTime_Over05_Percentage { get; set; }
        public int HalfTime_Over15_Percentage { get; set; }
        public int SecondHalf_Over05_Percentage { get; set; }
        public int SecondHalf_Over15_Percentage { get; set; }
    }
}
