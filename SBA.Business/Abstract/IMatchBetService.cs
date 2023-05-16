using Core.Entities.Concrete;
using Core.Entities.Concrete.ComplexModels.Sql;
using Core.Entities.Concrete.SqlEntities.QueryModels;
using Core.Entities.Dtos.MatchBet;
using Core.Utilities.Results;
using Core.Utilities.UsableModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SBA.Business.Abstract
{
    public interface IMatchBetService
    {
        IDataResult<List<MatchBet>> GetList(Expression<Func<MatchBet, bool>> filter = null);
        IDataResult<MatchBet> Get(Expression<Func<MatchBet, bool>> filter);
        IDataResult<List<GetMatchBetDto>> GetDtoList(Expression<Func<MatchBet, bool>> filter = null, int takeCount = 20000000);
        IDataResult<GetMatchBetDto> GetDto(Expression<Func<MatchBet, bool>> filter = null);
        IDataResult<int> Add(CreateMatchBetDto entity);
        IDataResult<int> Update(MatchBet entity);
        IDataResult<int> Remove(int Id);
        IDataResult<int> AddRange(List<MatchBet> entities);
        IDataResult<int> AddRangeFromMatchInfoList(List<MatchInfoContainer> matchInfoContainers);
        IDataResult<int> AddFromMatchInfo(MatchInfoContainer matchInfoContainer);
        IDataResult<int> UpdateRange(List<MatchBet> entities);
        IDataResult<int> RemoveRange(List<MatchBet> entities);
        IDataResult<IQueryable<MatchBet>> Query(Expression<Func<MatchBet, bool>> filter = null);
        IDataResult<List<MatchBetQM>> GetMatchBetQueryModels(string countryName, string teamName, int takeCount, Expression<Func<MatchBetQM, bool>> filter = null);

        IDataResult<int> SyncRange(List<MatchBet> entities);

        IDataResult<List<MatchBetQM>> GetMatchBetFilterResultQueryModels(Expression<Func<MatchBetQM, bool>> filter = null);

        Task<List<FilterResultMutateModel>> GetOddFilteredResultAsync(InTimeShortOddModel inTimeOdds, decimal range);

        Task<IDataResult<List<GetMatchBetDto>>> GetDtoListAsync(Expression<Func<MatchBet, bool>> filter = null, int takeCount = 20000000);
        Task<IDataResult<GetMatchBetDto>> GetDtoAsync(Expression<Func<MatchBet, bool>> filter = null);
        Task<IDataResult<int>> RemoveRangeAsync(List<MatchBet> entities);
        Task<IDataResult<int>> UpdateRangeAsync(List<MatchBet> entities);
        Task<IDataResult<List<MatchBet>>> GetListAsync(Expression<Func<MatchBet, bool>> filter = null);
        Task<IDataResult<int>> AddRangeAsync(List<MatchBet> entities);
        Task<IDataResult<int>> UpdateAsync(MatchBet entity);
        Task<IDataResult<MatchBet>> GetAsync(Expression<Func<MatchBet, bool>> filter);
        Task<IDataResult<int>> RemoveAsync(long Id);
        Task<IDataResult<int>> AddAsync(MatchBet entity);
        Task<IDataResult<IQueryable<MatchBet>>> QueryAsync(Expression<Func<MatchBet, bool>> filter = null);
    }
}
