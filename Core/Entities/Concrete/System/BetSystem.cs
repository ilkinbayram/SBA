using Core.Concrete.Base;

namespace Core.Entities.Concrete.System
{
    public class BetSystem : BaseEntity, IEntity
    {
        public string? Name { get; set; }
        public decimal AcceptedOdd { get; set; }
        public int AcceptedDivider { get; set; }
        public decimal StartingAmount { get; set; }
        public decimal TotalBalance { get; set; }
        public int StepsGoalCount { get; set; }
        public int MaxBundleCount { get; set; }

        public virtual List<Step>? Steps { get; set; }
        public virtual List<SavedStep>? SavedSteps { get; set; }
        public virtual List<Bundle>? Bundles { get; set; }
    }
}
