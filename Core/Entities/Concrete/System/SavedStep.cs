using Core.Concrete.Base;

namespace Core.Entities.Concrete.System
{
    public class SavedStep : BaseEntity, IEntity
    {
        public int StartingStepId { get; set; }
        public decimal TotalBalance { get; set; }

        public virtual Step StartingStep { get; set; } = default!;
    }
}
