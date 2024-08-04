namespace Core.Utilities.UsableModel
{
    public class ComboPotential
    {
        public ComboPotential()
        {
            if (Predictions == null)
                Predictions = new List<PredictionPotential>();
        }

        public int TotalWinningPossibility { get; set; }
        public decimal Odd { get; set; }
        public int TotalSelectedMatch => Predictions.Count;
        public List<PredictionPotential> Predictions { get; set; }
    }
}
