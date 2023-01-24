namespace Core.Utilities.UsableModel
{
    public class StandingInfoModel
    {
        public StandingInfoModel()
        {

        }

        public StandingDetail UpTeam { get; set; }
        public StandingDetail DownTeam { get; set; }
    }

    public class StandingDetail
    {
        public StandingDetail()
        {

        }

        public string TeamName { get; set; }
        public int Order { get; set; }
        public int MatchesCount { get; set; }
        public int WinsCount { get; set; }
        public int DrawsCount { get; set; }
        public int LostsCount { get; set; }
        public int Point { get; set; }


        public decimal Indicator
        {
            get
            {
                return (decimal)this.Point * 100 / this.MatchesCount / 3;
            }
        }
    }
}
