namespace Core.Entities.Concrete.ComplexModels.ML
{
    public class MatchDataAiModel
    {
        public MatchDataAiModel()
        {
        }
        public MatchDataAiModel(string countryName, string leagueName, string homeTeam, string awayTeam, DateTime matchDateTime, string sport = "FootBall")
        {
            CountryName = countryName;
            LeagueName = leagueName;
            HomeTeam = homeTeam;
            AwayTeam = awayTeam;
            MatchDateTime = matchDateTime;
            Sport = sport;
        }

        public string CountryName { get; set; }
        public string LeagueName { get; set; }
        public string Sport { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public DateTime MatchDateTime { get; set; }
    }
}
