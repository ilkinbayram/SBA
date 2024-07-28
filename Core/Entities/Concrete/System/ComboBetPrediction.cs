using Core.Concrete.Base;

namespace Core.Entities.Concrete.System
{
    public class ComboBetPrediction : IEntity
    {
        public int Id { get; set; }
        public int ComboBetId { get; set; }
        public int PredictionId { get; set; }

        public virtual ComboBet ComboBet { get; set; } = default!;
        public virtual Prediction Prediction { get; set; } = default!;
    }
}
