using AutoMapper;
using Business.Constants;
using Core.Entities.Concrete.ExternalDbEntities;
using Core.Entities.Dtos.ComplexDataes;
using Core.Entities.Dtos.ComplexDataes.MobileUI;
using Core.Utilities.Results;
using SBA.Business.Abstract;
using SBA.ExternalDataAccess.Abstract;
using System.Linq.Expressions;

namespace SBA.Business.Concrete
{
    public class AverageStatisticsHolderManager : IAverageStatisticsHolderService
    {
        private readonly IAverageStatisticsHolderDal _averageStatisticsHolderDal;
        private readonly IMapper _mapper;

        public AverageStatisticsHolderManager(IAverageStatisticsHolderDal averageStatisticsHolderDal,
                             IMapper mapper)
        {
            _averageStatisticsHolderDal = averageStatisticsHolderDal;
            _mapper = mapper;
        }

        public IDataResult<int> Add(AverageStatisticsHolder averageStatisticsHolderModel)
        {
            try
            {
                int affectedRows = _averageStatisticsHolderDal.Add(averageStatisticsHolderModel);

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

                var deletableEntity = _averageStatisticsHolderDal.Get(x => x.Id == Id);

                if (deletableEntity == null)
                {
                    dataResult = new SuccessDataResult<int>(-1, Messages.DeletableDataWasNotFound);
                    return dataResult;
                }

                int affectedRows = _averageStatisticsHolderDal.Remove(deletableEntity);
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

        public IDataResult<AverageStatisticsHolder> Get(Expression<Func<AverageStatisticsHolder, bool>> filter)
        {
            try
            {
                var response = _averageStatisticsHolderDal.Get(filter);
                var mappingResult = _mapper.Map<AverageStatisticsHolder>(response);
                return new SuccessDataResult<AverageStatisticsHolder>(mappingResult);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<AverageStatisticsHolder>(null, $"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}");
            }
        }

        public IDataResult<List<AverageStatisticsHolder>> GetList(Expression<Func<AverageStatisticsHolder, bool>> filter = null)
        {
            try
            {
                var response = _averageStatisticsHolderDal.GetList(filter);
                var mappingResult = _mapper.Map<List<AverageStatisticsHolder>>(response);
                return new SuccessDataResult<List<AverageStatisticsHolder>>(mappingResult);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<List<AverageStatisticsHolder>>(null, $"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}");
            }
        }

        public IDataResult<int> Update(AverageStatisticsHolder homeMetaTagGallery)
        {
            try
            {
                int affectedRows = _averageStatisticsHolderDal.Update(homeMetaTagGallery);
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


        public IDataResult<int> UpdateRange(List<AverageStatisticsHolder> homeMetaTagGalleries)
        {
            try
            {
                int affectedRows = _averageStatisticsHolderDal.UpdateRange(homeMetaTagGalleries);
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
        public IDataResult<int> AddRange(List<AverageStatisticsHolder> averageStatisticsHolders)
        {
            try
            {
                int affectedRows = _averageStatisticsHolderDal.AddRange(averageStatisticsHolders);

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

        public IDataResult<int> RemoveRange(List<AverageStatisticsHolder> averageStatisticsHolders)
        {
            try
            {
                int affectedRows = _averageStatisticsHolderDal.RemoveRange(averageStatisticsHolders);
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

        public IDataResult<IQueryable<AverageStatisticsHolder>> Query(Expression<Func<AverageStatisticsHolder, bool>> filter = null)
        {
            try
            {
                var response = _averageStatisticsHolderDal.Query(filter);
                return new SuccessDataResult<IQueryable<AverageStatisticsHolder>>(response);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<IQueryable<AverageStatisticsHolder>>(null, $"Exception Message: {$"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}"} \nInner Exception: {exception.InnerException}");
            }
        }


        #region Asynchronous

        public async Task<IDataResult<int>> AddAsync(AverageStatisticsHolder averageStatisticsHolder)
        {
            try
            {
                int affectedRows = await _averageStatisticsHolderDal.AddAsync(averageStatisticsHolder);
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

                var deletableEntity = await _averageStatisticsHolderDal.GetAsync(x => x.Id == Id);

                if (deletableEntity == null)
                {
                    dataResult = new SuccessDataResult<int>(-1, Messages.DeletableDataWasNotFound);
                    return dataResult;
                }

                int affectedRows = await _averageStatisticsHolderDal.RemoveAsync(deletableEntity);
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

        public async Task<IDataResult<AverageStatisticsHolder>> GetAsync(Expression<Func<AverageStatisticsHolder, bool>> filter)
        {
            try
            {
                var response = await _averageStatisticsHolderDal.GetAsync(filter);
                var mappingResult = _mapper.Map<AverageStatisticsHolder>(response);
                return new SuccessDataResult<AverageStatisticsHolder>(mappingResult);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<AverageStatisticsHolder>(null, $"Exception Message: {$"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}"} \nInner Exception: {exception.InnerException}");
            }
        }

        public async Task<IDataResult<List<AverageStatisticsHolder>>> GetListAsync(Expression<Func<AverageStatisticsHolder, bool>> filter = null)
        {
            try
            {
                var response = (await _averageStatisticsHolderDal.GetListAsync(filter)).ToList();
                var mappingResult = _mapper.Map<List<AverageStatisticsHolder>>(response);
                return new SuccessDataResult<List<AverageStatisticsHolder>>(mappingResult);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<List<AverageStatisticsHolder>>(null, $"Exception Message: {$"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}"} \nInner Exception: {exception.InnerException}");
            }
        }

        public async Task<IDataResult<int>> UpdateAsync(AverageStatisticsHolder averageStatisticsHolder)
        {
            try
            {
                int affectedRows = await _averageStatisticsHolderDal.UpdateAsync(averageStatisticsHolder);
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

        public async Task<IDataResult<int>> AddRangeAsync(List<AverageStatisticsHolder> averageStatisticsHolders)
        {
            try
            {
                int affectedRows = await _averageStatisticsHolderDal.AddRangeAsync(averageStatisticsHolders);
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

        public async Task<IDataResult<int>> UpdateRangeAsync(List<AverageStatisticsHolder> averageStatisticsHolders)
        {
            try
            {
                int affectedRows = await _averageStatisticsHolderDal.UpdateRangeAsync(averageStatisticsHolders);
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

        public async Task<IDataResult<int>> RemoveRangeAsync(List<AverageStatisticsHolder> averageStatisticsHolders)
        {
            try
            {
                int affectedRows = await _averageStatisticsHolderDal.RemoveRangeAsync(averageStatisticsHolders);
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

        public async Task<IDataResult<IQueryable<AverageStatisticsHolder>>> QueryAsync(Expression<Func<AverageStatisticsHolder, bool>> filter = null)
        {
            try
            {
                var response = await _averageStatisticsHolderDal.QueryAsync(filter);
                return new SuccessDataResult<IQueryable<AverageStatisticsHolder>>(response);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<IQueryable<AverageStatisticsHolder>>(null, $"Exception Message: {$"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}"} \nInner Exception: {exception.InnerException}");
            }
        }

        public AverageStatisticsMatchResult GetAverageMatchResultById(int serial, int bySideType)
        {
            return _averageStatisticsHolderDal.GetAverageMatchResultById(serial, bySideType);
        }
        #endregion
    }
}
