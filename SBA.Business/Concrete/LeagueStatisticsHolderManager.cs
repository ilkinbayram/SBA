using AutoMapper;
using Business.Constants;
using Core.Entities.Concrete.ComplexModels.RequestModelHelpers;
using Core.Entities.Concrete.ComplexModels.Sql;
using Core.Entities.Concrete.ExternalDbEntities;
using Core.Resources.Enums;
using Core.Utilities.Results;
using SBA.Business.Abstract;
using SBA.ExternalDataAccess.Abstract;
using System.Linq.Expressions;

namespace SBA.Business.Concrete
{
    public class LeagueStatisticsHolderManager : ILeagueStatisticsHolderService
    {
        private readonly ILeagueStatisticsHolderDal _leagueStatisticsHolderDal;
        private readonly IMapper _mapper;

        public LeagueStatisticsHolderManager(ILeagueStatisticsHolderDal leagueStatisticsHolderDal,
                             IMapper mapper)
        {
            _leagueStatisticsHolderDal = leagueStatisticsHolderDal;
            _mapper = mapper;
        }

        public IDataResult<int> Add(LeagueStatisticsHolder leagueStatisticsHolderModel)
        {
            try
            {
                int affectedRows = _leagueStatisticsHolderDal.Add(leagueStatisticsHolderModel);

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

                var deletableEntity = _leagueStatisticsHolderDal.Get(x => x.Id == Id);

                if (deletableEntity == null)
                {
                    dataResult = new SuccessDataResult<int>(-1, Messages.DeletableDataWasNotFound);
                    return dataResult;
                }

                int affectedRows = _leagueStatisticsHolderDal.Remove(deletableEntity);
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

        public IDataResult<LeagueStatisticsHolder> Get(Expression<Func<LeagueStatisticsHolder, bool>> filter)
        {
            try
            {
                var response = _leagueStatisticsHolderDal.Get(filter);
                var mappingResult = _mapper.Map<LeagueStatisticsHolder>(response);
                return new SuccessDataResult<LeagueStatisticsHolder>(mappingResult);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<LeagueStatisticsHolder>(null, $"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}");
            }
        }

        public IDataResult<List<LeagueStatisticsHolder>> GetList(Expression<Func<LeagueStatisticsHolder, bool>> filter = null)
        {
            try
            {
                var response = _leagueStatisticsHolderDal.GetList(filter);
                var mappingResult = _mapper.Map<List<LeagueStatisticsHolder>>(response);
                return new SuccessDataResult<List<LeagueStatisticsHolder>>(mappingResult);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<List<LeagueStatisticsHolder>>(null, $"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}");
            }
        }

        public IDataResult<int> Update(LeagueStatisticsHolder homeMetaTagGallery)
        {
            try
            {
                int affectedRows = _leagueStatisticsHolderDal.Update(homeMetaTagGallery);
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


        public IDataResult<int> UpdateRange(List<LeagueStatisticsHolder> homeMetaTagGalleries)
        {
            try
            {
                int affectedRows = _leagueStatisticsHolderDal.UpdateRange(homeMetaTagGalleries);
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
        public IDataResult<int> AddRange(List<LeagueStatisticsHolder> leagueStatisticsHolders)
        {
            try
            {
                int affectedRows = _leagueStatisticsHolderDal.AddRange(leagueStatisticsHolders);

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

        public IDataResult<int> RemoveRange(List<LeagueStatisticsHolder> leagueStatisticsHolders)
        {
            try
            {
                int affectedRows = _leagueStatisticsHolderDal.RemoveRange(leagueStatisticsHolders);
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

        public IDataResult<IQueryable<LeagueStatisticsHolder>> Query(Expression<Func<LeagueStatisticsHolder, bool>> filter = null)
        {
            try
            {
                var response = _leagueStatisticsHolderDal.Query(filter);
                return new SuccessDataResult<IQueryable<LeagueStatisticsHolder>>(response);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<IQueryable<LeagueStatisticsHolder>>(null, $"Exception Message: {$"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}"} \nInner Exception: {exception.InnerException}");
            }
        }


        #region Asynchronous

        public async Task<IDataResult<int>> AddAsync(LeagueStatisticsHolder leagueStatisticsHolder)
        {
            try
            {
                int affectedRows = await _leagueStatisticsHolderDal.AddAsync(leagueStatisticsHolder);
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

                var deletableEntity = await _leagueStatisticsHolderDal.GetAsync(x => x.Id == Id);

                if (deletableEntity == null)
                {
                    dataResult = new SuccessDataResult<int>(-1, Messages.DeletableDataWasNotFound);
                    return dataResult;
                }

                int affectedRows = await _leagueStatisticsHolderDal.RemoveAsync(deletableEntity);
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

        public async Task<IDataResult<LeagueStatisticsHolder>> GetAsync(Expression<Func<LeagueStatisticsHolder, bool>> filter)
        {
            try
            {
                var response = await _leagueStatisticsHolderDal.GetAsync(filter);
                var mappingResult = _mapper.Map<LeagueStatisticsHolder>(response);
                return new SuccessDataResult<LeagueStatisticsHolder>(mappingResult);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<LeagueStatisticsHolder>(null, $"Exception Message: {$"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}"} \nInner Exception: {exception.InnerException}");
            }
        }

        public async Task<IDataResult<List<LeagueStatisticsHolder>>> GetListAsync(Expression<Func<LeagueStatisticsHolder, bool>> filter = null)
        {
            try
            {
                var response = (await _leagueStatisticsHolderDal.GetListAsync(filter)).ToList();
                var mappingResult = _mapper.Map<List<LeagueStatisticsHolder>>(response);
                return new SuccessDataResult<List<LeagueStatisticsHolder>>(mappingResult);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<List<LeagueStatisticsHolder>>(null, $"Exception Message: {$"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}"} \nInner Exception: {exception.InnerException}");
            }
        }

        public async Task<IDataResult<int>> UpdateAsync(LeagueStatisticsHolder leagueStatisticsHolder)
        {
            try
            {
                int affectedRows = await _leagueStatisticsHolderDal.UpdateAsync(leagueStatisticsHolder);
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

        public async Task<IDataResult<int>> AddRangeAsync(List<LeagueStatisticsHolder> leagueStatisticsHolders)
        {
            try
            {
                int affectedRows = await _leagueStatisticsHolderDal.AddRangeAsync(leagueStatisticsHolders);
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

        public async Task<IDataResult<int>> UpdateRangeAsync(List<LeagueStatisticsHolder> leagueStatisticsHolders)
        {
            try
            {
                int affectedRows = await _leagueStatisticsHolderDal.UpdateRangeAsync(leagueStatisticsHolders);
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

        public async Task<IDataResult<int>> RemoveRangeAsync(List<LeagueStatisticsHolder> leagueStatisticsHolders)
        {
            try
            {
                int affectedRows = await _leagueStatisticsHolderDal.RemoveRangeAsync(leagueStatisticsHolders);
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

        public async Task<IDataResult<IQueryable<LeagueStatisticsHolder>>> QueryAsync(Expression<Func<LeagueStatisticsHolder, bool>> filter = null)
        {
            try
            {
                var response = await _leagueStatisticsHolderDal.QueryAsync(filter);
                return new SuccessDataResult<IQueryable<LeagueStatisticsHolder>>(response);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<IQueryable<LeagueStatisticsHolder>>(null, $"Exception Message: {$"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}"} \nInner Exception: {exception.InnerException}");
            }
        }

        public MatchLeagueComplexDto GetAiComplexStatistics(int serial)
        {
            return _leagueStatisticsHolderDal.GetAiComplexStatistics(serial);
        }

        public ComparisonResponseModel GetComparisonStatistics(int serial, int bySide)
        {
            return _leagueStatisticsHolderDal.GetComparisonStatistics(serial, bySide);
        }

        public PerformanceResponseModel GetPerformanceStatistics(int serial, int bySide, int homeOrAway)
        {
            return _leagueStatisticsHolderDal.GetPerformanceStatistics(serial, bySide, homeOrAway);
        }

        public LeagueStatisticsResponseModel GetLeagueStatistics(int serial)
        {
            return _leagueStatisticsHolderDal.GetLeagueStatistics(serial);
        }
        #endregion
    }
}
