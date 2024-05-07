using Core.DataAccess;
using Core.Entities.Concrete.ComplexModels.Program;
using Core.Entities.Concrete.ExternalDbEntities;
using sqlParamModel = Core.Utilities.UsableModel;
using TLMixedCore = Core.Entities.Concrete.ComplexModels.Sql;

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
        Task<List<int>> GetAllProgramSerialsAsync();
        sqlParamModel.MatchPerformanceOverallParameterModel SpGetMatchInformation(int serial);
        TLMixedCore.TeamLeagueMixedStat SP_GetTeamLeagueMixedStatResult(int serial);
    }
}
