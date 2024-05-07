﻿using Core.Entities.Concrete;
using Core.Entities.Concrete.ExternalDbEntities;
using Core.Entities.Concrete.SqlEntities.QueryModels;
using Core.Utilities.Results;
using System.Linq.Expressions;

namespace SBA.Business.Abstract
{
    public interface IMatchBetService
    {
        IDataResult<List<MatchBet>> GetList(Expression<Func<MatchBet, bool>> filter = null);
        IDataResult<int> AddRange(List<MatchBet> entities);
        IDataResult<int> UpdateRange(List<MatchBet> entities);
        IDataResult<int> RemoveRange(List<MatchBet> entities);
        IDataResult<IQueryable<MatchBet>> Query(Expression<Func<MatchBet, bool>> filter = null);
        IDataResult<List<MatchBetQM>> GetMatchBetQueryModels(List<int> leagueIds, string countryName, string teamName, int takeCount, Expression<Func<MatchBetQM, bool>> filter = null);
        IDataResult<List<MatchBetQM>> GetMatchBetFilterResultQueryModels(Expression<Func<MatchBetQM, bool>> filter = null);
        List<StatisticInfoHolder> GetOddFilteredResult(int serial, decimal range);
        List<StatisticInfoHolder> GetPerformanceOverallResult(int serial);
    }
}
