using Core.Concrete.Base;
using Core.Entities.Concrete.ExternalDbEntities;

namespace Core.Entities.Concrete.System
{
    public class Prediction : BaseEntity, IEntity
    {
        public int ForecastId { get; set; }

        public decimal Odd { get; set; }
        public int ProbableWinPercent { get; set; }
        public int AnalysingProbableMatchCount { get; set; }

        public virtual List<ComboBetPrediction>? ComboBetPredictions { get; set; }

    }
}
