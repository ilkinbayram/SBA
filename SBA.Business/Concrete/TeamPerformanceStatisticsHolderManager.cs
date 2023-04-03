using AutoMapper;
using Business.Constants;
using Core.Entities.Concrete.ExternalDbEntities;
using Core.Entities.Dtos.ComplexDataes;
using Core.Utilities.Results;
using SBA.Business.Abstract;
using SBA.ExternalDataAccess.Abstract;
using System.Linq.Expressions;

namespace SBA.Business.Concrete
{
    public class TeamPerformanceStatisticsHolderManager : ITeamPerformanceStatisticsHolderService
    {
        private readonly ITeamPerformanceStatisticsHolderDal _teamPerformanceStatisticsHolderDal;
        private readonly IMapper _mapper;

        public TeamPerformanceStatisticsHolderManager(ITeamPerformanceStatisticsHolderDal teamPerformanceStatisticsHolderDal,
                             IMapper mapper)
        {
            _teamPerformanceStatisticsHolderDal = teamPerformanceStatisticsHolderDal;
            _mapper = mapper;
        }

        public IDataResult<int> Add(TeamPerformanceStatisticsHolder teamPerformanceStatisticsHolderModel)
        {
            try
            {
                int affectedRows = _teamPerformanceStatisticsHolderDal.Add(teamPerformanceStatisticsHolderModel);

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

                var deletableEntity = _teamPerformanceStatisticsHolderDal.Get(x => x.Id == Id);

                if (deletableEntity == null)
                {
                    dataResult = new SuccessDataResult<int>(-1, Messages.DeletableDataWasNotFound);
                    return dataResult;
                }

                int affectedRows = _teamPerformanceStatisticsHolderDal.Remove(deletableEntity);
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

        public IDataResult<TeamPerformanceStatisticsHolder> Get(Expression<Func<TeamPerformanceStatisticsHolder, bool>> filter)
        {
            try
            {
                var response = _teamPerformanceStatisticsHolderDal.Get(filter);
                var mappingResult = _mapper.Map<TeamPerformanceStatisticsHolder>(response);
                return new SuccessDataResult<TeamPerformanceStatisticsHolder>(mappingResult);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<TeamPerformanceStatisticsHolder>(null, $"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}");
            }
        }

        public IDataResult<List<TeamPerformanceStatisticsHolder>> GetList(Expression<Func<TeamPerformanceStatisticsHolder, bool>> filter = null)
        {
            try
            {
                var response = _teamPerformanceStatisticsHolderDal.GetList(filter);
                var mappingResult = _mapper.Map<List<TeamPerformanceStatisticsHolder>>(response);
                return new SuccessDataResult<List<TeamPerformanceStatisticsHolder>>(mappingResult);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<List<TeamPerformanceStatisticsHolder>>(null, $"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}");
            }
        }

        public IDataResult<int> Update(TeamPerformanceStatisticsHolder homeMetaTagGallery)
        {
            try
            {
                int affectedRows = _teamPerformanceStatisticsHolderDal.Update(homeMetaTagGallery);
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


        public IDataResult<int> UpdateRange(List<TeamPerformanceStatisticsHolder> homeMetaTagGalleries)
        {
            try
            {
                int affectedRows = _teamPerformanceStatisticsHolderDal.UpdateRange(homeMetaTagGalleries);
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
        public IDataResult<int> AddRange(List<TeamPerformanceStatisticsHolder> teamPerformanceStatisticsHolders)
        {
            try
            {
                int affectedRows = _teamPerformanceStatisticsHolderDal.AddRange(teamPerformanceStatisticsHolders);

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

        public IDataResult<int> RemoveRange(List<TeamPerformanceStatisticsHolder> teamPerformanceStatisticsHolders)
        {
            try
            {
                int affectedRows = _teamPerformanceStatisticsHolderDal.RemoveRange(teamPerformanceStatisticsHolders);
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

        public IDataResult<IQueryable<TeamPerformanceStatisticsHolder>> Query(Expression<Func<TeamPerformanceStatisticsHolder, bool>> filter = null)
        {
            try
            {
                var response = _teamPerformanceStatisticsHolderDal.Query(filter);
                return new SuccessDataResult<IQueryable<TeamPerformanceStatisticsHolder>>(response);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<IQueryable<TeamPerformanceStatisticsHolder>>(null, $"Exception Message: {$"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}"} \nInner Exception: {exception.InnerException}");
            }
        }


        #region Asynchronous

        public async Task<IDataResult<int>> AddAsync(TeamPerformanceStatisticsHolder teamPerformanceStatisticsHolder)
        {
            try
            {
                int affectedRows = await _teamPerformanceStatisticsHolderDal.AddAsync(teamPerformanceStatisticsHolder);
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

                var deletableEntity = await _teamPerformanceStatisticsHolderDal.GetAsync(x => x.Id == Id);

                if (deletableEntity == null)
                {
                    dataResult = new SuccessDataResult<int>(-1, Messages.DeletableDataWasNotFound);
                    return dataResult;
                }

                int affectedRows = await _teamPerformanceStatisticsHolderDal.RemoveAsync(deletableEntity);
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

        public async Task<IDataResult<TeamPerformanceStatisticsHolder>> GetAsync(Expression<Func<TeamPerformanceStatisticsHolder, bool>> filter)
        {
            try
            {
                var response = await _teamPerformanceStatisticsHolderDal.GetAsync(filter);
                var mappingResult = _mapper.Map<TeamPerformanceStatisticsHolder>(response);
                return new SuccessDataResult<TeamPerformanceStatisticsHolder>(mappingResult);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<TeamPerformanceStatisticsHolder>(null, $"Exception Message: {$"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}"} \nInner Exception: {exception.InnerException}");
            }
        }

        public async Task<IDataResult<List<TeamPerformanceStatisticsHolder>>> GetListAsync(Expression<Func<TeamPerformanceStatisticsHolder, bool>> filter = null)
        {
            try
            {
                var response = (await _teamPerformanceStatisticsHolderDal.GetListAsync(filter)).ToList();
                var mappingResult = _mapper.Map<List<TeamPerformanceStatisticsHolder>>(response);
                return new SuccessDataResult<List<TeamPerformanceStatisticsHolder>>(mappingResult);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<List<TeamPerformanceStatisticsHolder>>(null, $"Exception Message: {$"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}"} \nInner Exception: {exception.InnerException}");
            }
        }

        public async Task<IDataResult<int>> UpdateAsync(TeamPerformanceStatisticsHolder teamPerformanceStatisticsHolder)
        {
            try
            {
                int affectedRows = await _teamPerformanceStatisticsHolderDal.UpdateAsync(teamPerformanceStatisticsHolder);
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

        public async Task<IDataResult<int>> AddRangeAsync(List<TeamPerformanceStatisticsHolder> teamPerformanceStatisticsHolders)
        {
            try
            {
                int affectedRows = await _teamPerformanceStatisticsHolderDal.AddRangeAsync(teamPerformanceStatisticsHolders);
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

        public async Task<IDataResult<int>> UpdateRangeAsync(List<TeamPerformanceStatisticsHolder> teamPerformanceStatisticsHolders)
        {
            try
            {
                int affectedRows = await _teamPerformanceStatisticsHolderDal.UpdateRangeAsync(teamPerformanceStatisticsHolders);
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

        public async Task<IDataResult<int>> RemoveRangeAsync(List<TeamPerformanceStatisticsHolder> teamPerformanceStatisticsHolders)
        {
            try
            {
                int affectedRows = await _teamPerformanceStatisticsHolderDal.RemoveRangeAsync(teamPerformanceStatisticsHolders);
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

        public async Task<IDataResult<IQueryable<TeamPerformanceStatisticsHolder>>> QueryAsync(Expression<Func<TeamPerformanceStatisticsHolder, bool>> filter = null)
        {
            try
            {
                var response = await _teamPerformanceStatisticsHolderDal.QueryAsync(filter);
                return new SuccessDataResult<IQueryable<TeamPerformanceStatisticsHolder>>(response);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<IQueryable<TeamPerformanceStatisticsHolder>>(null, $"Exception Message: {$"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}"} \nInner Exception: {exception.InnerException}");
            }
        }

        public PerformanceStatisticsMatchResult GetPerformanceMatchResultById(int serial, int bySideType, int homeOrAway)
        {
            return _teamPerformanceStatisticsHolderDal.GetPerformanceMatchResultById(serial, bySideType, homeOrAway);
        }
        #endregion
    }
}
