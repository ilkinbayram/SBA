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
    public class ComparisonStatisticsHolderManager : IComparisonStatisticsHolderService
    {
        private readonly IComparisonStatisticsHolderDal _statisticsHolderDal;
        private readonly IMapper _mapper;

        public ComparisonStatisticsHolderManager(IComparisonStatisticsHolderDal statisticsHolderDal,
                             IMapper mapper)
        {
            _statisticsHolderDal = statisticsHolderDal;
            _mapper = mapper;
        }

        public IDataResult<int> Add(ComparisonStatisticsHolder statisticsHolderModel)
        {
            try
            {
                int affectedRows = _statisticsHolderDal.Add(statisticsHolderModel);

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

                var deletableEntity = _statisticsHolderDal.Get(x => x.Id == Id);

                if (deletableEntity == null)
                {
                    dataResult = new SuccessDataResult<int>(-1, Messages.DeletableDataWasNotFound);
                    return dataResult;
                }

                int affectedRows = _statisticsHolderDal.Remove(deletableEntity);
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

        public IDataResult<ComparisonStatisticsHolder> Get(Expression<Func<ComparisonStatisticsHolder, bool>> filter)
        {
            try
            {
                var response = _statisticsHolderDal.Get(filter);
                var mappingResult = _mapper.Map<ComparisonStatisticsHolder>(response);
                return new SuccessDataResult<ComparisonStatisticsHolder>(mappingResult);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<ComparisonStatisticsHolder>(null, $"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}");
            }
        }

        public IDataResult<List<ComparisonStatisticsHolder>> GetList(Expression<Func<ComparisonStatisticsHolder, bool>> filter = null)
        {
            try
            {
                var response = _statisticsHolderDal.GetList(filter);
                var mappingResult = _mapper.Map<List<ComparisonStatisticsHolder>>(response);
                return new SuccessDataResult<List<ComparisonStatisticsHolder>>(mappingResult);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<List<ComparisonStatisticsHolder>>(null, $"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}");
            }
        }

        public IDataResult<int> Update(ComparisonStatisticsHolder homeMetaTagGallery)
        {
            try
            {
                int affectedRows = _statisticsHolderDal.Update(homeMetaTagGallery);
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


        public IDataResult<int> UpdateRange(List<ComparisonStatisticsHolder> homeMetaTagGalleries)
        {
            try
            {
                int affectedRows = _statisticsHolderDal.UpdateRange(homeMetaTagGalleries);
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
        public IDataResult<int> AddRange(List<ComparisonStatisticsHolder> statisticsHolders)
        {
            try
            {
                int affectedRows = _statisticsHolderDal.AddRange(statisticsHolders);

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

        public IDataResult<int> RemoveRange(List<ComparisonStatisticsHolder> statisticsHolders)
        {
            try
            {
                int affectedRows = _statisticsHolderDal.RemoveRange(statisticsHolders);
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

        public IDataResult<IQueryable<ComparisonStatisticsHolder>> Query(Expression<Func<ComparisonStatisticsHolder, bool>> filter = null)
        {
            try
            {
                var response = _statisticsHolderDal.Query(filter);
                return new SuccessDataResult<IQueryable<ComparisonStatisticsHolder>>(response);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<IQueryable<ComparisonStatisticsHolder>>(null, $"Exception Message: {$"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}"} \nInner Exception: {exception.InnerException}");
            }
        }


        #region Asynchronous

        public async Task<IDataResult<int>> AddAsync(ComparisonStatisticsHolder statisticsHolder)
        {
            try
            {
                int affectedRows = await _statisticsHolderDal.AddAsync(statisticsHolder);
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

                var deletableEntity = await _statisticsHolderDal.GetAsync(x => x.Id == Id);

                if (deletableEntity == null)
                {
                    dataResult = new SuccessDataResult<int>(-1, Messages.DeletableDataWasNotFound);
                    return dataResult;
                }

                int affectedRows = await _statisticsHolderDal.RemoveAsync(deletableEntity);
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

        public async Task<IDataResult<ComparisonStatisticsHolder>> GetAsync(Expression<Func<ComparisonStatisticsHolder, bool>> filter)
        {
            try
            {
                var response = await _statisticsHolderDal.GetAsync(filter);
                var mappingResult = _mapper.Map<ComparisonStatisticsHolder>(response);
                return new SuccessDataResult<ComparisonStatisticsHolder>(mappingResult);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<ComparisonStatisticsHolder>(null, $"Exception Message: {$"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}"} \nInner Exception: {exception.InnerException}");
            }
        }

        public async Task<IDataResult<List<ComparisonStatisticsHolder>>> GetListAsync(Expression<Func<ComparisonStatisticsHolder, bool>> filter = null)
        {
            try
            {
                var response = (await _statisticsHolderDal.GetListAsync(filter)).ToList();
                var mappingResult = _mapper.Map<List<ComparisonStatisticsHolder>>(response);
                return new SuccessDataResult<List<ComparisonStatisticsHolder>>(mappingResult);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<List<ComparisonStatisticsHolder>>(null, $"Exception Message: {$"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}"} \nInner Exception: {exception.InnerException}");
            }
        }

        public async Task<IDataResult<int>> UpdateAsync(ComparisonStatisticsHolder statisticsHolder)
        {
            try
            {
                int affectedRows = await _statisticsHolderDal.UpdateAsync(statisticsHolder);
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

        public async Task<IDataResult<int>> AddRangeAsync(List<ComparisonStatisticsHolder> statisticsHolders)
        {
            try
            {
                int affectedRows = await _statisticsHolderDal.AddRangeAsync(statisticsHolders);
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

        public async Task<IDataResult<int>> UpdateRangeAsync(List<ComparisonStatisticsHolder> statisticsHolders)
        {
            try
            {
                int affectedRows = await _statisticsHolderDal.UpdateRangeAsync(statisticsHolders);
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

        public async Task<IDataResult<int>> RemoveRangeAsync(List<ComparisonStatisticsHolder> statisticsHolders)
        {
            try
            {
                int affectedRows = await _statisticsHolderDal.RemoveRangeAsync(statisticsHolders);
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

        public async Task<IDataResult<IQueryable<ComparisonStatisticsHolder>>> QueryAsync(Expression<Func<ComparisonStatisticsHolder, bool>> filter = null)
        {
            try
            {
                var response = await _statisticsHolderDal.QueryAsync(filter);
                return new SuccessDataResult<IQueryable<ComparisonStatisticsHolder>>(response);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<IQueryable<ComparisonStatisticsHolder>>(null, $"Exception Message: {$"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}"} \nInner Exception: {exception.InnerException}");
            }
        }

        public ComparisonStatisticsMatchResult GetComparisonMatchResultById(int serial, int bySideType)
        {
            return _statisticsHolderDal.GetComparisonMatchResultById(serial, bySideType);
        }
        #endregion
    }
}
