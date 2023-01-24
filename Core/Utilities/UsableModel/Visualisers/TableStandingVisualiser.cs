namespace Core.Utilities.UsableModel.Visualisers
{
    public class TableStandingVisualiser
    {
        public TableStandingVisualiser()
        {

        }

        public TableStandingDetailVisualiser UpTeam { get; set; }
        public TableStandingDetailVisualiser DownTeam { get; set; }
    }

    public class TableStandingDetailVisualiser
    {
        public TableStandingDetailVisualiser()
        {

        }

        public string TeamName { get; set; }
        public string Order { get; set; }
        public string MatchesCount { get; set; }
        public string WinsCount { get; set; }
        public string DrawsCount { get; set; }
        public string LostsCount { get; set; }
        public string Point { get; set; }


        public string Indicator { get; set; }
    }
}
