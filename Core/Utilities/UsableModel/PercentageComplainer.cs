namespace Core.Utilities.UsableModel
{
    public class PercentageComplainer
    {
        public PercentageComplainer()
        {
            Percentage = -1;
            CountFound = -1;
            CountAll = -1;

            FeatureName = string.Empty;
            PropertyName = string.Empty;
        }
        public int Percentage { get; set; }
        public int CountFound { get; set; }
        public int CountAll { get; set; }
        public string FeatureName { get; set; }
        public string PropertyName { get; set; }
    }
}
