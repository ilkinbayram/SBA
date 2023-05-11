namespace Core.Entities.Concrete.ComplexModels.ML
{
    public class StandingAiModel
    {
        public StandingTeamAiDetailsModel HomeTeam_StandingDetails { get; set; }
        public StandingTeamAiDetailsModel AwayTeam_StandingDetails { get; set; }
    }

    public class StandingTeamAiDetailsModel
    {
        public string TeamName { get; set; }
        public int Order { get; set; }
        public int MatchesCount { get; set; }
        public int WinsCount { get; set; }
        public int DrawsCount { get; set; }
        public int LostsCount { get; set; }
        public int Point { get; set; }
    }
}
