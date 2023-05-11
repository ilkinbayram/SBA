namespace Core.Entities.Dtos.ComplexDataes.MobileUI
{
    public class StatisticInfoContainer
    {
        public int Serial { get; set; }
        public string? HomeTeam { get; set; }
        public string? AwayTeam { get; set; }
        public string? MatchTime { get; set; }
        public LeagueStatisticInfo? LeagueStatistic { get; set; }
        public List<StatisticInfo>? AverageBySideStatistics { get; set; }
        public List<StatisticInfo>? AverageGeneralStatistics { get; set; }
        public List<StatisticInfo>? PerformanceBySideStatistics { get; set; }
        public List<StatisticInfo>? PerformanceGeneralStatistics { get; set; }
        public List<StatisticInfo>? ComparisonBySideStatistics { get; set; }
        public List<StatisticInfo>? ComparisonGeneralStatistics { get; set; }
        public List<StatisticInfo>? StatisticInfoes { get; set; }
    }
}
