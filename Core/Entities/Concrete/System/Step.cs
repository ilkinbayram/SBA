using Core.Concrete.Base;
using Core.Resources.Enums;

namespace Core.Entities.Concrete.System
{
    public class Step : BaseEntity, IEntity
    {
        public bool IsSuccess { get; set; }
        public int? ParentId { get; set; }
        public int? ChildId { get; set; }
        public int BetSystemId { get; set; }
        public StepStatus Status { get; set; }
        public int Number { get; set; }
        public decimal InsuredBetAmount { get; set; }
        public decimal TotalBalance { get; set; }

        public virtual BetSystem System { get; set; } = default!;
        public virtual List<Bundle>? Bundles { get; set; }
    }
}
