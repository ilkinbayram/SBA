namespace Core.Entities.Dtos.SystemModels
{
    public class CreateSystemDto
    {
        public string? Name { get; set; }
        public decimal AcceptedOdd { get; set; }
        public int AcceptedDivider { get; set; }
        public decimal StartingAmount { get; set; }
        public int StepsGoalCount { get; set; }
        public int MaxBundleCount { get; set; }
    }
}
