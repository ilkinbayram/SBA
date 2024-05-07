using Core.Entities.Concrete.Base;

namespace Core.Entities.Concrete.ExternalDbEntities
{
    public class LeagueStatisticsHolder : Identifier, IEntity
    {
        public LeagueStatisticsHolder()
        {
            if (ComparisonStatisticsHolders == null)
                ComparisonStatisticsHolders = new List<ComparisonStatisticsHolder>();
            if (TeamPerformanceStatisticsHolders == null)
                TeamPerformanceStatisticsHolders = new List<TeamPerformanceStatisticsHolder>();
            if (AverageStatisticsHolders == null)
                AverageStatisticsHolders = new List<AverageStatisticsHolder>();
        }


        public int CountFound { get; set; }

        public string CountryName { get; set; }
        public string LeagueName { get; set; }
        public string LeagueIdsConcat { get; set; }
        public DateTime DateOfAnalyse { get; set; }
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

        public DateTime CreatedDateTime { get; set; }

        public DateTime ModifiedDateTime { get; set; }

        public string? CreatedBy { get; set; }

        public string? ModifiedBy { get; set; }

        public virtual List<ComparisonStatisticsHolder> ComparisonStatisticsHolders { get; set; }
        public virtual List<TeamPerformanceStatisticsHolder> TeamPerformanceStatisticsHolders { get; set; }
        public virtual List<AverageStatisticsHolder> AverageStatisticsHolders { get; set; }
    }
}
