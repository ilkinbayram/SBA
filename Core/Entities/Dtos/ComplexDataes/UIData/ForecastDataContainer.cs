namespace Core.Entities.Dtos.ComplexDataes.UIData
{
    public class ForecastDataContainer
    {
        public ForecastDataContainer()
        {
            if (MatchForecasts == null)
                MatchForecasts = new List<MatchForecast>();
        }

        public int AllSuccessCount => MatchForecasts.Select(x => x.SuccessCount).Sum();
        public int AllForecastCount => MatchForecasts.Select(x => x.ForecastCount).Sum();
        public int AllSuccessPercent
        {
            get
            {
                if (AllForecastCount == 0) return 0;

                return Convert.ToInt32(AllSuccessCount * 100 / AllForecastCount);
            }
        }

        public List<MatchForecast> MatchForecasts { get; set; }
    }
}
