using AutoMapper;
using Business.Constants;
using Core.Entities.Concrete.ExternalDbEntities;
using Core.Entities.Dtos.ComplexDataes.MobileUI;
using Core.Resources.Enums;
using Core.Utilities.Results;
using SBA.Business.Abstract;
using SBA.ExternalDataAccess.Abstract;
using System.Linq.Expressions;

namespace SBA.Business.Concrete
{
    public class StatisticInfoHolderManager : IStatisticInfoHolderService
    {
        private readonly IStatisticInfoHolderDal _statisticInfoHolderDal;
        private readonly IMapper _mapper;

        public StatisticInfoHolderManager(IStatisticInfoHolderDal statisticInfoHolderDal,
                             IMapper mapper)
        {
            _statisticInfoHolderDal = statisticInfoHolderDal;
            _mapper = mapper;
        }

        public IDataResult<int> Add(StatisticInfoHolder statisticInfoHolderModel)
        {
            try
            {
                int affectedRows = _statisticInfoHolderDal.Add(statisticInfoHolderModel);

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

                var deletableEntity = _statisticInfoHolderDal.Get(x => x.Id == Id);

                if (deletableEntity == null)
                {
                    dataResult = new SuccessDataResult<int>(-1, Messages.DeletableDataWasNotFound);
                    return dataResult;
                }

                int affectedRows = _statisticInfoHolderDal.Remove(deletableEntity);
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

        public IDataResult<StatisticInfoHolder> Get(Expression<Func<StatisticInfoHolder, bool>> filter)
        {
            try
            {
                var response = _statisticInfoHolderDal.Get(filter);
                var mappingResult = _mapper.Map<StatisticInfoHolder>(response);
                return new SuccessDataResult<StatisticInfoHolder>(mappingResult);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<StatisticInfoHolder>(null, $"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}");
            }
        }

        public IDataResult<List<StatisticInfoHolder>> GetList(Expression<Func<StatisticInfoHolder, bool>> filter = null)
        {
            try
            {
                var response = _statisticInfoHolderDal.GetList(filter);
                var mappingResult = _mapper.Map<List<StatisticInfoHolder>>(response);
                return new SuccessDataResult<List<StatisticInfoHolder>>(mappingResult);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<List<StatisticInfoHolder>>(null, $"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}");
            }
        }

        public IDataResult<int> Update(StatisticInfoHolder homeMetaTagGallery)
        {
            try
            {
                int affectedRows = _statisticInfoHolderDal.Update(homeMetaTagGallery);
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


        public IDataResult<int> UpdateRange(List<StatisticInfoHolder> homeMetaTagGalleries)
        {
            try
            {
                int affectedRows = _statisticInfoHolderDal.UpdateRange(homeMetaTagGalleries);
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
        public IDataResult<int> AddRange(List<StatisticInfoHolder> statisticInfoHolders)
        {
            try
            {
                int affectedRows = _statisticInfoHolderDal.AddRange(statisticInfoHolders);

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

        public IDataResult<int> RemoveRange(List<StatisticInfoHolder> statisticInfoHolders)
        {
            try
            {
                int affectedRows = _statisticInfoHolderDal.RemoveRange(statisticInfoHolders);
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

        public IDataResult<IQueryable<StatisticInfoHolder>> Query(Expression<Func<StatisticInfoHolder, bool>> filter = null)
        {
            try
            {
                var response = _statisticInfoHolderDal.Query(filter);
                return new SuccessDataResult<IQueryable<StatisticInfoHolder>>(response);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<IQueryable<StatisticInfoHolder>>(null, $"Exception Message: {$"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}"} \nInner Exception: {exception.InnerException}");
            }
        }


        #region Asynchronous

        public async Task<IDataResult<int>> AddAsync(StatisticInfoHolder statisticInfoHolder)
        {
            try
            {
                int affectedRows = await _statisticInfoHolderDal.AddAsync(statisticInfoHolder);
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

                var deletableEntity = await _statisticInfoHolderDal.GetAsync(x => x.Id == Id);

                if (deletableEntity == null)
                {
                    dataResult = new SuccessDataResult<int>(-1, Messages.DeletableDataWasNotFound);
                    return dataResult;
                }

                int affectedRows = await _statisticInfoHolderDal.RemoveAsync(deletableEntity);
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

        public async Task<IDataResult<StatisticInfoHolder>> GetAsync(Expression<Func<StatisticInfoHolder, bool>> filter)
        {
            try
            {
                var response = await _statisticInfoHolderDal.GetAsync(filter);
                var mappingResult = _mapper.Map<StatisticInfoHolder>(response);
                return new SuccessDataResult<StatisticInfoHolder>(mappingResult);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<StatisticInfoHolder>(null, $"Exception Message: {$"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}"} \nInner Exception: {exception.InnerException}");
            }
        }

        public async Task<IDataResult<List<StatisticInfoHolder>>> GetListAsync(Expression<Func<StatisticInfoHolder, bool>> filter = null)
        {
            try
            {
                var response = (await _statisticInfoHolderDal.GetListAsync(filter)).ToList();
                var mappingResult = _mapper.Map<List<StatisticInfoHolder>>(response);
                return new SuccessDataResult<List<StatisticInfoHolder>>(mappingResult);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<List<StatisticInfoHolder>>(null, $"Exception Message: {$"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}"} \nInner Exception: {exception.InnerException}");
            }
        }

        public async Task<IDataResult<int>> UpdateAsync(StatisticInfoHolder statisticInfoHolder)
        {
            try
            {
                int affectedRows = await _statisticInfoHolderDal.UpdateAsync(statisticInfoHolder);
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

        public async Task<IDataResult<int>> AddRangeAsync(List<StatisticInfoHolder> statisticInfoHolders)
        {
            try
            {
                int affectedRows = await _statisticInfoHolderDal.AddRangeAsync(statisticInfoHolders);
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

        public async Task<IDataResult<int>> UpdateRangeAsync(List<StatisticInfoHolder> statisticInfoHolders)
        {
            try
            {
                int affectedRows = await _statisticInfoHolderDal.UpdateRangeAsync(statisticInfoHolders);
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

        public async Task<IDataResult<int>> RemoveRangeAsync(List<StatisticInfoHolder> statisticInfoHolders)
        {
            try
            {
                int affectedRows = await _statisticInfoHolderDal.RemoveRangeAsync(statisticInfoHolders);
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

        public async Task<IDataResult<IQueryable<StatisticInfoHolder>>> QueryAsync(Expression<Func<StatisticInfoHolder, bool>> filter = null)
        {
            try
            {
                var response = await _statisticInfoHolderDal.QueryAsync(filter);
                return new SuccessDataResult<IQueryable<StatisticInfoHolder>>(response);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<IQueryable<StatisticInfoHolder>>(null, $"Exception Message: {$"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}"} \nInner Exception: {exception.InnerException}");
            }
        }


        public StatisticInfoContainer GetAverageStatisticResultById(int serial, int bySideType, int lang)
        {
            return _statisticInfoHolderDal.GetAverageStatisticResultById(serial, bySideType, lang);
        }

        public StatisticInfoContainer GetComparisonStatisticResultById(int serial, int bySideType, int lang)
        {
            return _statisticInfoHolderDal.GetComparisonStatisticResultById(serial, bySideType, lang);
        }

        public StatisticInfoContainer GetPerformanceStatisticResultById(int serial, int bySideType, int lang)
        {
            return _statisticInfoHolderDal.GetPerformanceStatisticResultById(serial, bySideType, lang);
        }

        public StatisticInfoContainer GetAllStatisticResultById(int serial, int lang)
        {
            return _statisticInfoHolderDal.GetAllStatisticResultById(serial, lang);
        }
        #endregion
    }
}
