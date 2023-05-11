namespace Core.Entities.Concrete.ComplexModels.ML
{
    public class ComparisonAiModel
    {
        public string Season { get; set; }
        public DateTime MatchDate { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public int HT_Goals_HomeTeam { get; set; }
        public int HT_Goals_AwayTeam { get; set; }
        public int SH_Goals_HomeTeam { get; set; }
        public int SH_Goals_AwayTeam { get; set; }
        public int FT_Goals_HomeTeam { get; set; }
        public int FT_Goals_AwayTeam { get; set; }
        public string HalfTime_Result { get; set; }
        public string SecondHalf_Result { get; set; }
        public string FullTime_Result { get; set; }
    }
}
