namespace Core.Entities.Dtos.ComplexDataes.MobileUI
{
    public class StatisticInfo
    {
        public int Order { get; set; }
        public string Title { get; set; }
        public string HomeValue { get; set; }
        public string AwayValue { get; set; }
        public decimal HomePercent { get; set; }
        public decimal AwayPercent { get; set; }
        public string HomeProgressColor
        {
            get
            {
                if (HomePercent > AwayPercent)
                    return "#00EE18";
                if (HomePercent < AwayPercent)
                    return "#FF0033";
                return "#F9AE00";
            }
        }
        public string AwayProgressColor
        {
            get
            {
                if (HomePercent < AwayPercent)
                    return "#00EE18";
                if (HomePercent > AwayPercent)
                    return "#FF0033";
                return "#F9AE00";
            }
        }
    }
}
