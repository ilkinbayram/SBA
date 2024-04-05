using AutoMapper;
using Business.Constants;
using Core.Entities.Concrete.ComplexModels.Program;
using Core.Entities.Concrete.ExternalDbEntities;
using Core.Utilities.Results;
using SBA.Business.Abstract;
using SBA.ExternalDataAccess.Abstract;
using System.Linq.Expressions;

namespace SBA.Business.Concrete
{
    public class MatchIdentifierManager : IMatchIdentifierService
    {
        private readonly IMatchIdentifierDal _matchIdentifierDal;
        private readonly IMapper _mapper;

        public MatchIdentifierManager(IMatchIdentifierDal matchIdentifierDal,
                             IMapper mapper)
        {
            _matchIdentifierDal = matchIdentifierDal;
            _mapper = mapper;
        }

        public IDataResult<int> Add(MatchIdentifier matchIdentifierModel)
        {
            try
            {
                int affectedRows = _matchIdentifierDal.Add(matchIdentifierModel);

                if (affectedRows <= 0)
                    throw new Exception(Messages.ErrorMessages.NOT_ADDED_AND_ROLLED_BACK);

                return new SuccessDataResult<int>(affectedRows, Messages.BusinessDataAdded);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<int>(-500, $"Exception Message: {$"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}"} \nInner Exception: {exception.InnerException}");
            }
        }

        public IDataResult<int> Remove(long Id)
        {
            try
            {
                IDataResult<int> dataResult;

                var deletableEntity = _matchIdentifierDal.Get(x => x.Id == Id);

                if (deletableEntity == null)
                {
                    dataResult = new SuccessDataResult<int>(-1, Messages.DeletableDataWasNotFound);
                    return dataResult;
                }

                int affectedRows = _matchIdentifierDal.Remove(deletableEntity);
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

        public IDataResult<MatchIdentifier> Get(Expression<Func<MatchIdentifier, bool>> filter)
        {
            try
            {
                var response = _matchIdentifierDal.Get(filter);
                var mappingResult = _mapper.Map<MatchIdentifier>(response);
                return new SuccessDataResult<MatchIdentifier>(mappingResult);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<MatchIdentifier>(null, $"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}");
            }
        }

        public IDataResult<List<MatchIdentifier>> GetList(Expression<Func<MatchIdentifier, bool>> filter = null)
        {
            try
            {
                var response = _matchIdentifierDal.GetList(filter);
                var mappingResult = _mapper.Map<List<MatchIdentifier>>(response);
                return new SuccessDataResult<List<MatchIdentifier>>(mappingResult);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<List<MatchIdentifier>>(null, $"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}");
            }
        }

        public IDataResult<int> Update(MatchIdentifier homeMetaTagGallery)
        {
            try
            {
                int affectedRows = _matchIdentifierDal.Update(homeMetaTagGallery);
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


        public IDataResult<int> UpdateRange(List<MatchIdentifier> homeMetaTagGalleries)
        {
            try
            {
                int affectedRows = _matchIdentifierDal.UpdateRange(homeMetaTagGalleries);
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
        public IDataResult<int> AddRange(List<MatchIdentifier> matchIdentifiers)
        {
            try
            {
                int affectedRows = _matchIdentifierDal.AddRange(matchIdentifiers);

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

        public IDataResult<int> RemoveRange(List<MatchIdentifier> matchIdentifiers)
        {
            try
            {
                int affectedRows = _matchIdentifierDal.RemoveRange(matchIdentifiers);
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

        public IDataResult<IQueryable<MatchIdentifier>> Query(Expression<Func<MatchIdentifier, bool>> filter = null)
        {
            try
            {
                var response = _matchIdentifierDal.Query(filter);
                return new SuccessDataResult<IQueryable<MatchIdentifier>>(response);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<IQueryable<MatchIdentifier>>(null, $"Exception Message: {$"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}"} \nInner Exception: {exception.InnerException}");
            }
        }


        #region Asynchronous

        public async Task<IDataResult<int>> AddAsync(MatchIdentifier matchIdentifier)
        {
            try
            {
                int affectedRows = await _matchIdentifierDal.AddAsync(matchIdentifier);
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

                var deletableEntity = await _matchIdentifierDal.GetAsync(x => x.Id == Id);

                if (deletableEntity == null)
                {
                    dataResult = new SuccessDataResult<int>(-1, Messages.DeletableDataWasNotFound);
                    return dataResult;
                }

                int affectedRows = await _matchIdentifierDal.RemoveAsync(deletableEntity);
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

        public async Task<IDataResult<MatchIdentifier>> GetAsync(Expression<Func<MatchIdentifier, bool>> filter)
        {
            try
            {
                var response = await _matchIdentifierDal.GetAsync(filter);
                var mappingResult = _mapper.Map<MatchIdentifier>(response);
                return new SuccessDataResult<MatchIdentifier>(mappingResult);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<MatchIdentifier>(null, $"Exception Message: {$"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}"} \nInner Exception: {exception.InnerException}");
            }
        }

        public async Task<IDataResult<List<MatchIdentifier>>> GetListAsync(Expression<Func<MatchIdentifier, bool>> filter = null)
        {
            try
            {
                var response = (await _matchIdentifierDal.GetListAsync(filter)).ToList();
                var mappingResult = _mapper.Map<List<MatchIdentifier>>(response);
                return new SuccessDataResult<List<MatchIdentifier>>(mappingResult);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<List<MatchIdentifier>>(null, $"Exception Message: {$"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}"} \nInner Exception: {exception.InnerException}");
            }
        }

        public async Task<IDataResult<int>> UpdateAsync(MatchIdentifier matchIdentifier)
        {
            try
            {
                int affectedRows = await _matchIdentifierDal.UpdateAsync(matchIdentifier);
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

        public async Task<IDataResult<int>> AddRangeAsync(List<MatchIdentifier> matchIdentifiers)
        {
            try
            {
                int affectedRows = await _matchIdentifierDal.AddRangeAsync(matchIdentifiers);
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

        public async Task<IDataResult<int>> UpdateRangeAsync(List<MatchIdentifier> matchIdentifiers)
        {
            try
            {
                int affectedRows = await _matchIdentifierDal.UpdateRangeAsync(matchIdentifiers);
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

        public async Task<IDataResult<int>> RemoveRangeAsync(List<MatchIdentifier> matchIdentifiers)
        {
            try
            {
                int affectedRows = await _matchIdentifierDal.RemoveRangeAsync(matchIdentifiers);
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

        public async Task<IDataResult<IQueryable<MatchIdentifier>>> QueryAsync(Expression<Func<MatchIdentifier, bool>> filter = null)
        {
            try
            {
                var response = await _matchIdentifierDal.QueryAsync(filter);
                return new SuccessDataResult<IQueryable<MatchIdentifier>>(response);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<IQueryable<MatchIdentifier>>(null, $"Exception Message: {$"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}"} \nInner Exception: {exception.InnerException}");
            }
        }

        public MatchProgramList GetGroupedMatchsProgram(int month, int day)
        {
            return _matchIdentifierDal.GetGroupedMatchsProgram(month, day);
        }

        public MatchDetailProgram GetAllMatchsProgram(int month, int day)
        {
            return _matchIdentifierDal.GetAllMatchsProgram(month, day);
        }

        public async Task<MatchProgramList> GetGroupedMatchsProgramAsync(int month, int day)
        {
            return await _matchIdentifierDal.GetGroupedMatchsProgramAsync(month, day);
        }

        public async Task<MatchDetailProgram> GetAllMatchsProgramAsync(int month, int day)
        {
            return await _matchIdentifierDal.GetAllMatchsProgramAsync(month, day);
        }

        public async Task<MatchDetailProgram> GetPossibleForecastMatchsProgramAsync()
        {
            return await _matchIdentifierDal.GetPossibleForecastMatchsProgramAsync();
        }

        public async Task<MatchProgramList> GetGroupedFilteredForecastMatchsProgramAsync()
        {
            return await _matchIdentifierDal.GetGroupedFilteredForecastMatchsProgramAsync();
        }

        #endregion
    }
}
