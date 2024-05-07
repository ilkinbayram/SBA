namespace Core.Utilities.UsableModel
{
    public class CalculationReportingModel
    {
        public CalculationReportingModel(string title, int minimumPercent, int allMatchSize, int minCount = 10)
        {
            Title = title;
            MinimumPercent = minimumPercent;
            MinimumCount = minCount;
            AllMatchSize = allMatchSize;
        }

        public string Title { get; set; }
        public int MinimumPercent { get; set; }
        public int MinimumCount { get; set; }
        public int SuccessPossiblity
        {
            get
            {
                if (FoundCount == 0) return 0;

                return CorrectCount * 100 / FoundCount;
            }
        }
        public int CorrectCount { get; set; }
        public int FoundCount { get; set; }
        public decimal PerFoundAverage
        {
            get
            {
                if (AllMatchSize == 0) return 0;

                return FoundCount * 100 / AllMatchSize;
            }
        }
        public int AllMatchSize { get; set; }
    }
}
