namespace Core.Entities.Concrete.ComplexModels.ML
{
    public class LeagueStatisticsAiModel
    {
        public string CountryName { get; set; }
        public string LeagueName { get; set; }
        public decimal Average_FullTime_Goals { get; set; }
        public decimal Average_HalfTime_Goals { get; set; }
        public decimal Average_SecondHalf_Goals { get; set; }

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
