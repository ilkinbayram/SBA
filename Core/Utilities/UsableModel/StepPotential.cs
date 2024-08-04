namespace Core.Utilities.UsableModel
{
    public class StepPotential
    {
        public StepPotential()
        {
            if (PotentialCombos == null)
                PotentialCombos = new List<ComboPotential>();
        }

        public decimal RequiredOdd { get; set; }
        public int TotalCreatedCombos => PotentialCombos.Count;
        public List<ComboPotential> PotentialCombos { get; set; }
    }
}
