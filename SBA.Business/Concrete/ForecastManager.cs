using AutoMapper;
using Business.Constants;
using Core.Entities.Concrete.ExternalDbEntities;
using Core.Entities.Concrete.SqlEntities.FunctionViewProcModels;
using Core.Entities.Dtos.ComplexDataes.UIData;
using Core.Utilities.Results;
using SBA.Business.Abstract;
using SBA.ExternalDataAccess.Abstract;
using System.Linq.Expressions;

namespace SBA.Business.Concrete
{
    public class ForecastManager : IForecastService
    {
        private readonly IForecastDal _forecastDal;
        private readonly IMapper _mapper;

        public ForecastManager(IForecastDal forecastDal,
                             IMapper mapper)
        {
            _forecastDal = forecastDal;
            _mapper = mapper;
        }

        public IDataResult<int> Add(Forecast forecastModel)
        {
            try
            {
                int affectedRows = _forecastDal.Add(forecastModel);

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

                var deletableEntity = _forecastDal.Get(x => x.Id == Id);

                if (deletableEntity == null)
                {
                    dataResult = new SuccessDataResult<int>(-1, Messages.DeletableDataWasNotFound);
                    return dataResult;
                }

                int affectedRows = _forecastDal.Remove(deletableEntity);
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

        public IDataResult<Forecast> Get(Expression<Func<Forecast, bool>> filter)
        {
            try
            {
                var response = _forecastDal.Get(filter);
                var mappingResult = _mapper.Map<Forecast>(response);
                return new SuccessDataResult<Forecast>(mappingResult);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<Forecast>(null, $"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}");
            }
        }

        public IDataResult<List<Forecast>> GetList(Expression<Func<Forecast, bool>> filter = null)
        {
            try
            {
                var response = _forecastDal.GetList(filter);
                var mappingResult = _mapper.Map<List<Forecast>>(response);
                return new SuccessDataResult<List<Forecast>>(mappingResult);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<List<Forecast>>(null, $"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}");
            }
        }

        public IDataResult<int> Update(Forecast homeMetaTagGallery)
        {
            try
            {
                int affectedRows = _forecastDal.Update(homeMetaTagGallery);
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


        public IDataResult<int> UpdateRange(List<Forecast> homeMetaTagGalleries)
        {
            try
            {
                int affectedRows = _forecastDal.UpdateRange(homeMetaTagGalleries);
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
        public IDataResult<int> AddRange(List<Forecast> forecasts)
        {
            try
            {
                int affectedRows = _forecastDal.AddRange(forecasts);

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

        public IDataResult<int> RemoveRange(List<Forecast> forecasts)
        {
            try
            {
                int affectedRows = _forecastDal.RemoveRange(forecasts);
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

        public IDataResult<IQueryable<Forecast>> Query(Expression<Func<Forecast, bool>> filter = null)
        {
            try
            {
                var response = _forecastDal.Query(filter);
                return new SuccessDataResult<IQueryable<Forecast>>(response);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<IQueryable<Forecast>>(null, $"Exception Message: {$"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}"} \nInner Exception: {exception.InnerException}");
            }
        }


        #region Asynchronous

        public async Task<IDataResult<int>> AddAsync(Forecast forecast)
        {
            try
            {
                int affectedRows = await _forecastDal.AddAsync(forecast);
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

                var deletableEntity = await _forecastDal.GetAsync(x => x.Id == Id);

                if (deletableEntity == null)
                {
                    dataResult = new SuccessDataResult<int>(-1, Messages.DeletableDataWasNotFound);
                    return dataResult;
                }

                int affectedRows = await _forecastDal.RemoveAsync(deletableEntity);
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

        public async Task<IDataResult<Forecast>> GetAsync(Expression<Func<Forecast, bool>> filter)
        {
            try
            {
                var response = await _forecastDal.GetAsync(filter);
                var mappingResult = _mapper.Map<Forecast>(response);
                return new SuccessDataResult<Forecast>(mappingResult);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<Forecast>(null, $"Exception Message: {$"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}"} \nInner Exception: {exception.InnerException}");
            }
        }

        public async Task<IDataResult<List<Forecast>>> GetListAsync(Expression<Func<Forecast, bool>> filter = null)
        {
            try
            {
                var response = (await _forecastDal.GetListAsync(filter)).ToList();
                return new SuccessDataResult<List<Forecast>>(response);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<List<Forecast>>(null, $"Exception Message: {$"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}"} \nInner Exception: {exception.InnerException}");
            }
        }

        public async Task<IDataResult<int>> UpdateAsync(Forecast forecast)
        {
            try
            {
                int affectedRows = await _forecastDal.UpdateAsync(forecast);
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

        public async Task<IDataResult<int>> AddRangeAsync(List<Forecast> forecasts)
        {
            try
            {
                int affectedRows = await _forecastDal.AddRangeAsync(forecasts);
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

        public async Task<IDataResult<int>> UpdateRangeAsync(List<Forecast> forecasts)
        {
            try
            {
                int affectedRows = await _forecastDal.UpdateRangeAsync(forecasts);
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

        public async Task<IDataResult<int>> RemoveRangeAsync(List<Forecast> forecasts)
        {
            try
            {
                int affectedRows = await _forecastDal.RemoveRangeAsync(forecasts);
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

        public async Task<IDataResult<IQueryable<Forecast>>> QueryAsync(Expression<Func<Forecast, bool>> filter = null)
        {
            try
            {
                var response = await _forecastDal.QueryAsync(filter);
                return new SuccessDataResult<IQueryable<Forecast>>(response);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<IQueryable<Forecast>>(null, $"Exception Message: {$"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}"} \nInner Exception: {exception.InnerException}");
            }
        }

        public async Task<int> AddPossibleForecastsAsync(List<PossibleForecast> possibleForecasts)
        {
            return await _forecastDal.AddPossibleForecastsAsync(possibleForecasts);
        }

        public async Task<ForecastDataContainer> SelectForecastContainerInfoAsync(bool isCheckedItems, Func<MatchForecastFM, bool> filter = null)
        {
            return await _forecastDal.SelectForecastContainerInfoAsync(isCheckedItems, filter);
        }

        public async Task<List<string>> SelectForecastsBySerialAsync(int serial)
        {
            return await _forecastDal.SelectForecastsBySerialAsync(serial);
        }
        #endregion
    }
}
