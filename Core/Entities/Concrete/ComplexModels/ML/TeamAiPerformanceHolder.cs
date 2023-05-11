namespace Core.Entities.Concrete.ComplexModels.ML
{
    public class TeamAiPerformanceHolder
    {
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
        public string FullTime_Result { get; set; }
        public string SecondHalf_Result { get; set; }
        public PerformanceMoreDetails? MoreDetails { get; set; }
    }

    public class PerformanceMoreDetails
    {
        public int Home_Ball_Possesion { get; set; }
        public int Away_Ball_Possesion { get; set; }
        public int HomeShotsCount { get; set; }
        public int AwayShotsCount { get; set; }
        public int HomeShotsOnTargetCount { get; set; }
        public int AwayShotsOnTargetCount { get; set; }
        public int HomeGK_SavesCount { get; set; }
        public int AwayGK_SavesCount { get; set; }
    }
}
