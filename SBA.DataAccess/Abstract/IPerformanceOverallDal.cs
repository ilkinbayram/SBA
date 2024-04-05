using Core.DataAccess;
using Core.Entities.Concrete;
using Core.Entities.Concrete.ComplexModels.Sql;
using Core.Utilities.UsableModel;

namespace SBA.DataAccess.Abstract
{
    public interface IPerformanceOverallDal : IEntityRepository<PerformanceOverall>, IEntityQueryableRepository<PerformanceOverall>
    {
        MatchStatisticOverallResultModel GetSpMatchAnalyzeResult(MatchPerformanceOverallParameterModel parameters);
    }
}
