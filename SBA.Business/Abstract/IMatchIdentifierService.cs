using Core.Entities.Concrete.ComplexModels.Program;
using Core.Entities.Concrete.ExternalDbEntities;
using Core.Utilities.Results;
using System.Linq.Expressions;
using sqlParamModel = Core.Utilities.UsableModel;
using TLMixedCore = Core.Entities.Concrete.ComplexModels.Sql;

namespace SBA.Business.Abstract
{
    public interface IMatchIdentifierService
    {
        IDataResult<List<MatchIdentifier>> GetList(Expression<Func<MatchIdentifier, bool>> filter = null);
        IDataResult<MatchIdentifier> Get(Expression<Func<MatchIdentifier, bool>> filter);

        MatchProgramList GetGroupedMatchsProgram(int month, int day);
        MatchDetailProgram GetAllMatchsProgram(int month, int day);
        Task<MatchProgramList> GetGroupedMatchsProgramAsync(int month, int day);
        Task<MatchDetailProgram> GetAllMatchsProgramAsync(int month, int day);
        Task<MatchDetailProgram> GetPossibleForecastMatchsProgramAsync();
        Task<MatchProgramList> GetGroupedFilteredForecastMatchsProgramAsync();
        Task<List<int>> GetAllProgramSerialsAsync();

        sqlParamModel.MatchPerformanceOverallParameterModel SpGetMatchInformation(int serial);
        TLMixedCore.TeamLeagueMixedStat SP_GetTeamLeagueMixedStatResult(int serial);

        IDataResult<int> Add(MatchIdentifier entity);
        IDataResult<int> Update(MatchIdentifier entity);
        IDataResult<int> Remove(long Id);
        IDataResult<int> AddRange(List<MatchIdentifier> entities);
        IDataResult<int> UpdateRange(List<MatchIdentifier> entities);
        IDataResult<int> RemoveRange(List<MatchIdentifier> entities);
        IDataResult<IQueryable<MatchIdentifier>> Query(Expression<Func<MatchIdentifier, bool>> filter = null);

        Task<IDataResult<int>> RemoveRangeAsync(List<MatchIdentifier> entities);
        Task<IDataResult<int>> UpdateRangeAsync(List<MatchIdentifier> entities);
        Task<IDataResult<List<MatchIdentifier>>> GetListAsync(Expression<Func<MatchIdentifier, bool>> filter = null);
        Task<IDataResult<int>> AddRangeAsync(List<MatchIdentifier> entities);
        Task<IDataResult<int>> UpdateAsync(MatchIdentifier entity);
        Task<IDataResult<MatchIdentifier>> GetAsync(Expression<Func<MatchIdentifier, bool>> filter);
        Task<IDataResult<int>> RemoveAsync(long Id);
        Task<IDataResult<int>> AddAsync(MatchIdentifier entity);
        Task<IDataResult<IQueryable<MatchIdentifier>>> QueryAsync(Expression<Func<MatchIdentifier, bool>> filter = null);
    }
}
