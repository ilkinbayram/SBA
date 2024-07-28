using Core.Concrete.Base;

namespace Core.Entities.Concrete.System
{
    public class Bundle : BaseEntity, IEntity
    {
        public int StepId { get; set; }

        public virtual Step Step { get; set; } = default!;
        public virtual List<ComboBet>? ComboBets { get; set; }
    }
}
