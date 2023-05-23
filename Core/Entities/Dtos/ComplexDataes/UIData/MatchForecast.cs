using Core.Entities.Concrete.ExternalDbEntities;

namespace Core.Entities.Dtos.ComplexDataes.UIData
{
    public class MatchForecast
    {
        public MatchForecast()
        {
            if (Forecasts == null)
                Forecasts = new List<ForecastDTO>();
        }
        public int Serial { get; set; }
        public int MatchIdentityId { get; set; }
        public string CountryLeague { get; set; }
        public string FT_Result { get; set; }
        public string HT_Result { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public bool FullSucceeded => SuccessCount == ForecastCount && ForecastCount > 0;
        public int ForecastCount => Forecasts.Count;
        public int SuccessCount => Forecasts.Where(x => x.IsSuccess).Count();
        public int SuccessPercent
        {
            get
            {
                if (ForecastCount == 0) return 0;

                return Convert.ToInt32(SuccessCount * 100 / ForecastCount);
            }
        }
        public List<ForecastDTO> Forecasts { get; set; }
    }
}
