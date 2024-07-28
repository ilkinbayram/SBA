using Core.Concrete.Base;
using Core.Resources.Enums;

namespace Core.Entities.Concrete.System
{
    public class ComboBet : BaseEntity, IEntity
    {
        public decimal TotalOdd { get; set; }
        public int PotentialWinningPercent { get; set; }
        public bool IsInsuredBet { get; set; }
        public BetType BetType { get; set; }
        public int BundleId { get; set; }

        public virtual Bundle Bundle { get; set; } = default!;
        public virtual List<ComboBetPrediction>? ComboBetPredictions { get; set; }
    }
}
