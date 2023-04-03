using Core.Entities.Concrete.ComplexModels.RequestModelHelpers;
using Core.Entities.Concrete.ComplexModels.Sql;
using Core.Entities.Concrete.ExternalDbEntities;
using Core.Resources.Enums;
using Core.Utilities.Results;
using System.Linq.Expressions;

namespace SBA.Business.Abstract
{
    public interface ILeagueStatisticsHolderService
    {
        IDataResult<List<LeagueStatisticsHolder>> GetList(Expression<Func<LeagueStatisticsHolder, bool>> filter = null);
        IDataResult<LeagueStatisticsHolder> Get(Expression<Func<LeagueStatisticsHolder, bool>> filter);

        MatchLeagueComplexDto GetAiComplexStatistics(int serial);
        ComparisonResponseModel GetComparisonStatistics(int serial, int bySide);
        PerformanceResponseModel GetPerformanceStatistics(int serial, int bySide, int homeOrAway);
        LeagueStatisticsResponseModel GetLeagueStatistics(int serial);

        IDataResult<int> Add(LeagueStatisticsHolder entity);
        IDataResult<int> Update(LeagueStatisticsHolder entity);
        IDataResult<int> Remove(long Id);
        IDataResult<int> AddRange(List<LeagueStatisticsHolder> entities);
        IDataResult<int> UpdateRange(List<LeagueStatisticsHolder> entities);
        IDataResult<int> RemoveRange(List<LeagueStatisticsHolder> entities);
        IDataResult<IQueryable<LeagueStatisticsHolder>> Query(Expression<Func<LeagueStatisticsHolder, bool>> filter = null);

        Task<IDataResult<int>> RemoveRangeAsync(List<LeagueStatisticsHolder> entities);
        Task<IDataResult<int>> UpdateRangeAsync(List<LeagueStatisticsHolder> entities);
        Task<IDataResult<List<LeagueStatisticsHolder>>> GetListAsync(Expression<Func<LeagueStatisticsHolder, bool>> filter = null);
        Task<IDataResult<int>> AddRangeAsync(List<LeagueStatisticsHolder> entities);
        Task<IDataResult<int>> UpdateAsync(LeagueStatisticsHolder entity);
        Task<IDataResult<LeagueStatisticsHolder>> GetAsync(Expression<Func<LeagueStatisticsHolder, bool>> filter);
        Task<IDataResult<int>> RemoveAsync(long Id);
        Task<IDataResult<int>> AddAsync(LeagueStatisticsHolder entity);
        Task<IDataResult<IQueryable<LeagueStatisticsHolder>>> QueryAsync(Expression<Func<LeagueStatisticsHolder, bool>> filter = null);
    }
}
