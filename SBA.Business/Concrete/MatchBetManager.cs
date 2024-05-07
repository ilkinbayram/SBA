using AutoMapper;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Entities.Concrete.ComplexModels.Sql;
using Core.Entities.Concrete.ExternalDbEntities;
using Core.Entities.Concrete.SqlEntities.QueryModels;
using Core.Utilities.Results;
using SBA.Business.Abstract;
using SBA.Business.BusinessHelper;
using SBA.Business.FunctionalServices.Concrete;
using SBA.DataAccess.Abstract;
using SBA.ExternalDataAccess.Abstract;
using System.Linq.Expressions;

namespace Business.Concrete
{
    public class MatchBetManager : IMatchBetService
    {
        private readonly IMatchBetDal _matchBetDal;
        private readonly IPerformanceOverallDal _performanceOverallDal;
        private readonly IMatchIdentifierDal _matchIdentifierDal;
        private readonly IMapper _mapper;

        public MatchBetManager(IMatchBetDal matchBetDal,
                             IMapper mapper,
                             IPerformanceOverallDal performanceOverallDal,
                             IMatchIdentifierDal matchIdentifierDal)
        {
            _matchBetDal = matchBetDal;
            _mapper = mapper;
            _performanceOverallDal = performanceOverallDal;
            _matchIdentifierDal = matchIdentifierDal;
        }

        public IDataResult<List<MatchBet>> GetList(Expression<Func<MatchBet, bool>> filter = null)
        {
            try
            {
                var response = _matchBetDal.GetList(filter);
                var mappingResult = _mapper.Map<List<MatchBet>>(response);
                return new SuccessDataResult<List<MatchBet>>(mappingResult);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<List<MatchBet>>(null, $"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}");
            }
        }

        public IDataResult<List<MatchBetQM>> GetMatchBetQueryModels(List<int> leagueIds, string countryName, string teamName, int takeCount, Expression<Func<MatchBetQM, bool>> filter = null)
        {
            try
            {
                var response = filter == null
                    ? _matchBetDal.GetMatchBetQueryModelsForPerformanceResult(leagueIds, countryName, teamName, takeCount).ToList()
                    : _matchBetDal.GetMatchBetQueryModelsForPerformanceResult(leagueIds, countryName, teamName, takeCount, filter).ToList();

                return new SuccessDataResult<List<MatchBetQM>>(response);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<List<MatchBetQM>>(null, $"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}");
            }
        }

        public IDataResult<List<MatchBetQM>> GetMatchBetFilterResultQueryModels(Expression<Func<MatchBetQM, bool>> filter = null)
        {
            try
            {
                var response = filter == null
                    ? _matchBetDal.GetMatchBetFilterResultQueryModels().ToList()
                    : _matchBetDal.GetMatchBetFilterResultQueryModels(filter).ToList();

                return new SuccessDataResult<List<MatchBetQM>>(response);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<List<MatchBetQM>>(null, $"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}");
            }
        }

        public IDataResult<int> UpdateRange(List<MatchBet> entities)
        {
            try
            {
                int affectedRows = _matchBetDal.UpdateRange(entities);
                IDataResult<int> dataResult;
                if (affectedRows > 0)
                {
                    dataResult = new SuccessDataResult<int>(affectedRows, Messages.BusinessDataUpdated);
                }
                else
                {
                    dataResult = new ErrorDataResult<int>(-1, false, Messages.BusinessDataWasNotUpdated);
                }

                return dataResult;
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<int>(-1, true, $"Exception Message: {$"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}"} \nInner Exception: {exception.InnerException}");
            }
        }

        public IDataResult<int> AddRange(List<MatchBet> entities)
        {
            try
            {
                int affectedRows = _matchBetDal.AddRange(entities);
                IDataResult<int> dataResult;
                if (affectedRows > 0)
                {
                    dataResult = new SuccessDataResult<int>(affectedRows, Messages.BusinessDataAdded);
                }
                else
                {
                    dataResult = new ErrorDataResult<int>(-1, false, Messages.BusinessDataWasNotAdded);
                }

                return dataResult;
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<int>(-1, true, $"Exception Message: {$"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}"} \nInner Exception: {exception.InnerException}");
            }
        }

        public IDataResult<int> RemoveRange(List<MatchBet> matchBets)
        {
            try
            {
                int affectedRows = _matchBetDal.RemoveRange(matchBets);
                IDataResult<int> dataResult;
                if (affectedRows > 0)
                {
                    dataResult = new SuccessDataResult<int>(affectedRows, Messages.BusinessDataDeleted);
                }
                else
                {
                    dataResult = new ErrorDataResult<int>(-1, false, Messages.BusinessDataWasNotDeleted);
                }

                return dataResult;
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<int>(-1, true, $"Exception Message: {$"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}"} \nInner Exception: {exception.InnerException}");
            }

        }

        public IDataResult<IQueryable<MatchBet>> Query(Expression<Func<MatchBet, bool>> filter = null)
        {
            try
            {
                var response = _matchBetDal.Query(filter);
                return new SuccessDataResult<IQueryable<MatchBet>>(response);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<IQueryable<MatchBet>>(null, $"Exception Message: {$"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}"} \nInner Exception: {exception.InnerException}");
            }
        }


        #region Asynchronous

        public List<StatisticInfoHolder> GetOddFilteredResult(int serial, decimal range)
        {
            var proceeder = new MatchInfoProceeder();
            var inTimeModel = proceeder.GenerateUnstartedShortMatchInfoByRegex(serial.ToString());

            List<FilterResultMutateModel> mutatedFilterResult = _matchBetDal.GetOddFilteredResult(inTimeModel, range, inTimeModel.MatchDate);

            var result = OperationalProcessor.GenerateOddPercentageStatInfoes(serial, mutatedFilterResult, range);

            return result;
        }

        public List<StatisticInfoHolder> GetPerformanceOverallResult(int serial)
        {
            var parameterModel = _matchIdentifierDal.SpGetMatchInformation(serial);
            var parameters = _performanceOverallDal.GetSpMatchAnalyzeResult(parameterModel);
            var result = OperationalProcessor.GenerateOvereallResultStatInfoes(serial, parameters);

            return result;
        }
        #endregion
    }
}
