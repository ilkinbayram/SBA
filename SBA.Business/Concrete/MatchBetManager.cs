using AutoMapper;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Entities.Concrete.ComplexModels.Sql;
using Core.Entities.Concrete.ExternalDbEntities;
using Core.Entities.Concrete.SqlEntities.QueryModels;
using Core.Entities.Dtos.MatchBet;
using Core.Utilities.Results;
using Core.Utilities.UsableModel;
using SBA.Business.Abstract;
using SBA.Business.BusinessHelper;
using SBA.Business.FunctionalServices.Concrete;
using SBA.Business.Mapping;
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

        public IDataResult<int> Add(CreateMatchBetDto matchBetModel)
        {
            try
            {
                var mappedModel = _mapper.Map<MatchBet>(matchBetModel);

                int affectedRows = _matchBetDal.Add(mappedModel);

                if (affectedRows <= 0)
                    throw new Exception(Messages.ErrorMessages.NOT_ADDED_AND_ROLLED_BACK);

                return new SuccessDataResult<int>(affectedRows, Messages.BusinessDataAdded);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<int>(-500, $"Exception Message: {$"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}"} \nInner Exception: {exception.InnerException}");
            }
        }

        public IDataResult<int> Remove(int Id)
        {
            try
            {
                IDataResult<int> dataResult;

                var deletableEntity = _matchBetDal.Get(x => x.SerialUniqueID == Id);

                if (deletableEntity == null)
                {
                    dataResult = new SuccessDataResult<int>(-1, Messages.DeletableDataWasNotFound);
                    return dataResult;
                }

                int affectedRows = _matchBetDal.Remove(deletableEntity);
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
                return new ErrorDataResult<int>(-1, true, $"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}");
            }
        }

        public IDataResult<MatchBet> Get(Expression<Func<MatchBet, bool>> filter)
        {
            try
            {
                var response = _matchBetDal.Get(filter);
                var mappingResult = _mapper.Map<MatchBet>(response);
                return new SuccessDataResult<MatchBet>(mappingResult);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<MatchBet>(null, $"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}");
            }
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

        public IDataResult<List<MatchBetQM>> GetMatchBetQueryModels(string countryName, string teamName, int takeCount, Expression<Func<MatchBetQM, bool>> filter = null)
        {
            try
            {
                var response = filter == null
                    ? _matchBetDal.GetMatchBetQueryModelsForPerformanceResult(countryName, teamName, takeCount).ToList()
                    : _matchBetDal.GetMatchBetQueryModelsForPerformanceResult(countryName, teamName, takeCount, filter).ToList();

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


        public IDataResult<int> Update(MatchBet entity)
        {
            try
            {
                int affectedRows = _matchBetDal.Update(entity);
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
                return new ErrorDataResult<int>(-1, true, $"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}");
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

        public IDataResult<int> SyncRange(List<MatchBet> entities)
        {
            try
            {
                int affectedRows = 0;
                var addingEntities = entities.Where(e => !_matchBetDal.Query().Select(x => x.SerialUniqueID).Contains(e.SerialUniqueID)).ToList();

                if (addingEntities.Count > 0)
                    affectedRows = _matchBetDal.AddRange(addingEntities);

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

        public IDataResult<int> AddRangeFromMatchInfoList(List<MatchInfoContainer> matchInfoContainers)
        {
            try
            {
                int affectedRows = 0;
                var matchBetList = new List<MatchBet>();
                foreach (var matchInfo in matchInfoContainers)
                {
                    var mappedModel = matchInfo.MapToMatchBetFromMatchInfo();
                    matchBetList.Add(mappedModel);
                }

                affectedRows = _matchBetDal.AddRange(matchBetList);

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

        public IDataResult<int> AddFromMatchInfo(MatchInfoContainer matchInfoContainer)
        {
            try
            {
                int affectedRows = 0;
                var mappedModel = matchInfoContainer.MapToMatchBetFromMatchInfo();
                affectedRows += _matchBetDal.Add(mappedModel);

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

        public IDataResult<List<GetMatchBetDto>> GetDtoList(Expression<Func<MatchBet, bool>> filter = null, int takeCount = 20000000)
        {
            try
            {
                var dtoListResult = new List<GetMatchBetDto>();
                _matchBetDal.GetList(filter).Take(takeCount).ToList().ForEach(x =>
                {
                    dtoListResult.Add(_mapper.Map<GetMatchBetDto>(x));
                });

                return new SuccessDataResult<List<GetMatchBetDto>>(dtoListResult);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<List<GetMatchBetDto>>(null, $"Exception Message: {$"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}"} \nInner Exception: {exception.InnerException}");
            }
        }

        public IDataResult<GetMatchBetDto> GetDto(Expression<Func<MatchBet, bool>> filter = null)
        {
            try
            {
                var response = _matchBetDal.Get(filter);
                var mappedModel = _mapper.Map<GetMatchBetDto>(response);
                return new SuccessDataResult<GetMatchBetDto>(mappedModel);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<GetMatchBetDto>(null, $"Exception Message: {$"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}"} \nInner Exception: {exception.InnerException}");
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

        public async Task<IDataResult<int>> AddAsync(MatchBet matchBet)
        {
            try
            {
                int affectedRows = await _matchBetDal.AddAsync(matchBet);
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

        public async Task<IDataResult<int>> RemoveAsync(long Id)
        {
            try
            {
                IDataResult<int> dataResult;

                var deletableEntity = await _matchBetDal.GetAsync(x => x.Id == Id);

                if (deletableEntity == null)
                {
                    dataResult = new SuccessDataResult<int>(-1, Messages.DeletableDataWasNotFound);
                    return dataResult;
                }

                int affectedRows = await _matchBetDal.RemoveAsync(deletableEntity);
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

        public async Task<IDataResult<MatchBet>> GetAsync(Expression<Func<MatchBet, bool>> filter)
        {
            try
            {
                var response = await _matchBetDal.GetAsync(filter);
                var mappingResult = _mapper.Map<MatchBet>(response);
                return new SuccessDataResult<MatchBet>(mappingResult);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<MatchBet>(null, $"Exception Message: {$"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}"} \nInner Exception: {exception.InnerException}");
            }
        }

        public async Task<IDataResult<List<MatchBet>>> GetListAsync(Expression<Func<MatchBet, bool>> filter = null)
        {
            try
            {
                var response = (await _matchBetDal.GetListAsync(filter)).ToList();
                var mappingResult = _mapper.Map<List<MatchBet>>(response);
                return new SuccessDataResult<List<MatchBet>>(mappingResult);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<List<MatchBet>>(null, $"Exception Message: {$"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}"} \nInner Exception: {exception.InnerException}");
            }
        }

        public async Task<IDataResult<int>> UpdateAsync(MatchBet matchBet)
        {
            try
            {
                int affectedRows = await _matchBetDal.UpdateAsync(matchBet);
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

        public async Task<IDataResult<int>> AddRangeAsync(List<MatchBet> matchBets)
        {
            try
            {
                int affectedRows = await _matchBetDal.AddRangeAsync(matchBets);
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

        public async Task<IDataResult<int>> UpdateRangeAsync(List<MatchBet> matchBets)
        {
            try
            {
                int affectedRows = await _matchBetDal.UpdateRangeAsync(matchBets);
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

        public async Task<IDataResult<int>> RemoveRangeAsync(List<MatchBet> matchBets)
        {
            try
            {
                int affectedRows = await _matchBetDal.RemoveRangeAsync(matchBets);
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

        public async Task<IDataResult<GetMatchBetDto>> GetDtoAsync(Expression<Func<MatchBet, bool>> filter = null)
        {
            try
            {
                var response = await _matchBetDal.GetAsync(filter);
                var mappingResult = _mapper.Map<GetMatchBetDto>(response);
                return new SuccessDataResult<GetMatchBetDto>(mappingResult);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<GetMatchBetDto>(null, $"Exception Message: {$"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}"} \nInner Exception: {exception.InnerException}");
            }
        }

        public async Task<IDataResult<List<GetMatchBetDto>>> GetDtoListAsync(Expression<Func<MatchBet, bool>> filter = null, int takeCount = 20000000)
        {
            try
            {
                var response = (await _matchBetDal.GetListAsync(filter)).ToList();
                var mappingResult = _mapper.Map<List<GetMatchBetDto>>(response).Take(takeCount).ToList();
                return new SuccessDataResult<List<GetMatchBetDto>>(mappingResult);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<List<GetMatchBetDto>>(null, $"Exception Message: {$"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}"} \nInner Exception: {exception.InnerException}");
            }
        }


        public async Task<IDataResult<IQueryable<MatchBet>>> QueryAsync(Expression<Func<MatchBet, bool>> filter = null)
        {
            try
            {
                var response = await _matchBetDal.QueryAsync(filter);
                return new SuccessDataResult<IQueryable<MatchBet>>(response);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<IQueryable<MatchBet>>(null, $"Exception Message: {$"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}"} \nInner Exception: {exception.InnerException}");
            }
        }

        public List<StatisticInfoHolder> GetOddFilteredResult(int serial, decimal range)
        {
            var proceeder = new MatchInfoProceeder();
            var inTimeModel = proceeder.GenerateUnstartedShortMatchInfoByRegex(serial.ToString());

            List<FilterResultMutateModel> mutatedFilterResult = _matchBetDal.GetOddFilteredResult(inTimeModel, range);

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
