namespace Core.Entities.Concrete.SqlEntities.FunctionViewProcModels
{
    public class MatchForecastFM
    {
        public int Serial { get; set; }
        public int MatchIdentityId { get; set; }
        public DateTime MatchDateTime { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public bool IsChecked { get; set; }
        public bool IsSuccess { get; set; }
        public string Description { get; set; }
        public string CountryLeague { get; set; }
        public string HT_Result { get; set; }
        public string FT_Result { get; set; }
    }
}
