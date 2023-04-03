using Core.Entities.Concrete.ExternalDbEntities;
using Core.Entities.Dtos.ComplexDataes;
using Core.Utilities.Results;
using System.Linq.Expressions;

namespace SBA.Business.Abstract
{
    public interface ITeamPerformanceStatisticsHolderService
    {
        IDataResult<List<TeamPerformanceStatisticsHolder>> GetList(Expression<Func<TeamPerformanceStatisticsHolder, bool>> filter = null);
        IDataResult<TeamPerformanceStatisticsHolder> Get(Expression<Func<TeamPerformanceStatisticsHolder, bool>> filter);

        PerformanceStatisticsMatchResult GetPerformanceMatchResultById(int serial, int bySideType, int homeOrAway);

        IDataResult<int> Add(TeamPerformanceStatisticsHolder entity);
        IDataResult<int> Update(TeamPerformanceStatisticsHolder entity);
        IDataResult<int> Remove(long Id);
        IDataResult<int> AddRange(List<TeamPerformanceStatisticsHolder> entities);
        IDataResult<int> UpdateRange(List<TeamPerformanceStatisticsHolder> entities);
        IDataResult<int> RemoveRange(List<TeamPerformanceStatisticsHolder> entities);
        IDataResult<IQueryable<TeamPerformanceStatisticsHolder>> Query(Expression<Func<TeamPerformanceStatisticsHolder, bool>> filter = null);

        Task<IDataResult<int>> RemoveRangeAsync(List<TeamPerformanceStatisticsHolder> entities);
        Task<IDataResult<int>> UpdateRangeAsync(List<TeamPerformanceStatisticsHolder> entities);
        Task<IDataResult<List<TeamPerformanceStatisticsHolder>>> GetListAsync(Expression<Func<TeamPerformanceStatisticsHolder, bool>> filter = null);
        Task<IDataResult<int>> AddRangeAsync(List<TeamPerformanceStatisticsHolder> entities);
        Task<IDataResult<int>> UpdateAsync(TeamPerformanceStatisticsHolder entity);
        Task<IDataResult<TeamPerformanceStatisticsHolder>> GetAsync(Expression<Func<TeamPerformanceStatisticsHolder, bool>> filter);
        Task<IDataResult<int>> RemoveAsync(long Id);
        Task<IDataResult<int>> AddAsync(TeamPerformanceStatisticsHolder entity);
        Task<IDataResult<IQueryable<TeamPerformanceStatisticsHolder>>> QueryAsync(Expression<Func<TeamPerformanceStatisticsHolder, bool>> filter = null);
    }
}
