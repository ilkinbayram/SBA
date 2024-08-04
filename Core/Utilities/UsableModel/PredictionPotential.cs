namespace Core.Utilities.UsableModel
{
    public class PredictionPotential
    {
        public string Match { get; set; }
        public string Country { get; set; }
        public string League { get; set; }
        public string MatchDateTime { get; set; }
        public string PredictionDescription { get; set; }
        public int WinningPossibility { get; set; }
        public decimal IndividualOdd { get; set; }
        public int ProbableAnalysingCount { get; set; }
    }
}
