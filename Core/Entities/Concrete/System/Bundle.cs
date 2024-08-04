using Core.Concrete.Base;

namespace Core.Entities.Concrete.System
{
    public class Bundle : BaseEntity, IEntity
    {
        public int SystemId { get; set; }
        public int BundlePriority { get; set; }
        public bool IsManualBundle { get; set; }

        public virtual BetSystem System { get; set; } = default!;
        public virtual List<ComboBet>? ComboBets { get; set; }
    }
}
