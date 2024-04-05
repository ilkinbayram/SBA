using Core.DataAccess;
using Core.Entities.Concrete.ComplexModels.Program;
using Core.Entities.Concrete.ExternalDbEntities;
using sqlParamModel = Core.Utilities.UsableModel;

namespace SBA.ExternalDataAccess.Abstract
{
    public interface IMatchIdentifierDal : IEntityRepository<MatchIdentifier>, IEntityQueryableRepository<MatchIdentifier>
    {
        MatchProgramList GetGroupedMatchsProgram(int month, int day);
        MatchDetailProgram GetAllMatchsProgram(int month, int day);
        Task<MatchProgramList> GetGroupedMatchsProgramAsync(int month, int day);
        Task<MatchDetailProgram> GetAllMatchsProgramAsync(int month, int day);
        Task<MatchDetailProgram> GetPossibleForecastMatchsProgramAsync();
        Task<MatchProgramList> GetGroupedFilteredForecastMatchsProgramAsync();
        sqlParamModel.MatchPerformanceOverallParameterModel SpGetMatchInformation(int serial);
    }
}
