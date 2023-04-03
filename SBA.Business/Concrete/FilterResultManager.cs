using AutoMapper;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Entities.Dtos.FilterResult;
using Core.Utilities.Results;
using SBA.Business.Abstract;
using SBA.DataAccess.Abstract;
using System.Linq.Expressions;

namespace Business.Concrete
{
    public class FilterResultManager : IFilterResultService
    {
        private readonly IFilterResultDal _filterResultDal;
        private readonly IMapper _mapper;

        public FilterResultManager(IFilterResultDal filterResultDal,
                             IMapper mapper)
        {
            _filterResultDal = filterResultDal;
            _mapper = mapper;
        }

        public IDataResult<int> Add(FilterResult filterResultModel)
        {
            try
            {
                int affectedRows = _filterResultDal.Add(filterResultModel);

                if (affectedRows <= 0)
                    throw new Exception(Messages.ErrorMessages.NOT_ADDED_AND_ROLLED_BACK);

                return new SuccessDataResult<int>(affectedRows, Messages.BusinessDataAdded);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<int>(-500, $"Exception Message: { $"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}"} \nInner Exception: {exception.InnerException}");
            }
        }

        public IDataResult<int> Add(CreateFilterResultDto filterResultModel)
        {
            try
            {
                var mappedModel = _mapper.Map<FilterResult>(filterResultModel);

                int affectedRows = _filterResultDal.Add(mappedModel);

                if (affectedRows <= 0)
                    throw new Exception(Messages.ErrorMessages.NOT_ADDED_AND_ROLLED_BACK);

                return new SuccessDataResult<int>(affectedRows, Messages.BusinessDataAdded);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<int>(-500, $"Exception Message: { $"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}"} \nInner Exception: {exception.InnerException}");
            }
        }

        public IDataResult<int> Remove(long Id)
        {
            try
            {
                IDataResult<int> dataResult;

                var deletableEntity = _filterResultDal.Get(x => x.Id == Id);

                if (deletableEntity == null)
                {
                    dataResult = new SuccessDataResult<int>(-1, Messages.DeletableDataWasNotFound);
                    return dataResult;
                }

                int affectedRows = _filterResultDal.Remove(deletableEntity);
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

        public IDataResult<FilterResult> Get(Expression<Func<FilterResult, bool>> filter)
        {
            try
            {
                var response = _filterResultDal.Get(filter);
                var mappingResult = _mapper.Map<FilterResult>(response);
                return new SuccessDataResult<FilterResult>(mappingResult);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<FilterResult>(null, $"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}");
            }
        }

        public IDataResult<List<FilterResult>> GetList(Expression<Func<FilterResult, bool>> filter = null)
        {
            try
            {
                var response = _filterResultDal.GetList(filter);
                var mappingResult = _mapper.Map<List<FilterResult>>(response);
                return new SuccessDataResult<List<FilterResult>>(mappingResult);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<List<FilterResult>>(null, $"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}");
            }
        }

        public IDataResult<int> Update(FilterResult homeMetaTagGallery)
        {
            try
            {
                int affectedRows = _filterResultDal.Update(homeMetaTagGallery);
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


        public IDataResult<int> UpdateRange(List<FilterResult> homeMetaTagGalleries)
        {
            try
            {
                int affectedRows = _filterResultDal.UpdateRange(homeMetaTagGalleries);
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
                return new ErrorDataResult<int>(-1, true, $"Exception Message: { $"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}"} \nInner Exception: {exception.InnerException}");
            }
        }
        public IDataResult<int> AddRange(List<FilterResult> filterResults)
        {
            try
            {
                int affectedRows = _filterResultDal.AddRange(filterResults);

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
                return new ErrorDataResult<int>(-1, true, $"Exception Message: { $"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}"} \nInner Exception: {exception.InnerException}");
            }
        }

        public IDataResult<int> RemoveRange(List<FilterResult> filterResults)
        {
            try
            {
                int affectedRows = _filterResultDal.RemoveRange(filterResults);
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
                return new ErrorDataResult<int>(-1, true, $"Exception Message: { $"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}"} \nInner Exception: {exception.InnerException}");
            }

        }

        public IDataResult<List<GetFilterResultDto>> GetDtoList(Expression<Func<FilterResult, bool>> filter = null, int takeCount = 20000000)
        {
            try
            {
                var dtoListResult = new List<GetFilterResultDto>();
                _filterResultDal.GetList(filter).Take(takeCount).ToList().ForEach(x =>
                {
                    dtoListResult.Add(_mapper.Map<GetFilterResultDto>(x));
                });

                return new SuccessDataResult<List<GetFilterResultDto>>(dtoListResult);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<List<GetFilterResultDto>>(null, $"Exception Message: { $"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}"} \nInner Exception: {exception.InnerException}");
            }
        }

        public IDataResult<GetFilterResultDto> GetDto(Expression<Func<FilterResult, bool>> filter = null)
        {
            try
            {
                var response = _filterResultDal.Get(filter);
                var mappedModel = _mapper.Map<GetFilterResultDto>(response);
                return new SuccessDataResult<GetFilterResultDto>(mappedModel);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<GetFilterResultDto>(null, $"Exception Message: { $"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}"} \nInner Exception: {exception.InnerException}");
            }
        }

        public IDataResult<IQueryable<FilterResult>> Query(Expression<Func<FilterResult, bool>> filter = null)
        {
            try
            {
                var response = _filterResultDal.Query(filter);
                return new SuccessDataResult<IQueryable<FilterResult>>(response);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<IQueryable<FilterResult>>(null, $"Exception Message: { $"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}"} \nInner Exception: {exception.InnerException}");
            }
        }


        #region Asynchronous

        public async Task<IDataResult<int>> AddAsync(FilterResult filterResult)
        {
            try
            {
                int affectedRows = await _filterResultDal.AddAsync(filterResult);
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
                return new ErrorDataResult<int>(-1, true, $"Exception Message: { $"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}"} \nInner Exception: {exception.InnerException}");
            }
        }


        public async Task<IDataResult<int>> RemoveAsync(long Id)
        {
            try
            {
                IDataResult<int> dataResult;

                var deletableEntity = await _filterResultDal.GetAsync(x => x.Id == Id);

                if (deletableEntity == null)
                {
                    dataResult = new SuccessDataResult<int>(-1, Messages.DeletableDataWasNotFound);
                    return dataResult;
                }

                int affectedRows = await _filterResultDal.RemoveAsync(deletableEntity);
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
                return new ErrorDataResult<int>(-1, true, $"Exception Message: { $"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}"} \nInner Exception: {exception.InnerException}");
            }
        }

        public async Task<IDataResult<FilterResult>> GetAsync(Expression<Func<FilterResult, bool>> filter)
        {
            try
            {
                var response = await _filterResultDal.GetAsync(filter);
                var mappingResult = _mapper.Map<FilterResult>(response);
                return new SuccessDataResult<FilterResult>(mappingResult);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<FilterResult>(null, $"Exception Message: { $"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}"} \nInner Exception: {exception.InnerException}");
            }
        }

        public async Task<IDataResult<List<FilterResult>>> GetListAsync(Expression<Func<FilterResult, bool>> filter = null)
        {
            try
            {
                var response = (await _filterResultDal.GetListAsync(filter)).ToList();
                var mappingResult = _mapper.Map<List<FilterResult>>(response);
                return new SuccessDataResult<List<FilterResult>>(mappingResult);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<List<FilterResult>>(null, $"Exception Message: { $"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}"} \nInner Exception: {exception.InnerException}");
            }
        }

        public async Task<IDataResult<int>> UpdateAsync(FilterResult filterResult)
        {
            try
            {
                int affectedRows = await _filterResultDal.UpdateAsync(filterResult);
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
                return new ErrorDataResult<int>(-1, true, $"Exception Message: { $"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}"} \nInner Exception: {exception.InnerException}");
            }
        }

        public async Task<IDataResult<int>> AddRangeAsync(List<FilterResult> filterResults)
        {
            try
            {
                int affectedRows = await _filterResultDal.AddRangeAsync(filterResults);
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
                return new ErrorDataResult<int>(-1, true, $"Exception Message: { $"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}"} \nInner Exception: {exception.InnerException}");
            }
        }

        public async Task<IDataResult<int>> UpdateRangeAsync(List<FilterResult> filterResults)
        {
            try
            {
                int affectedRows = await _filterResultDal.UpdateRangeAsync(filterResults);
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
                return new ErrorDataResult<int>(-1, true, $"Exception Message: { $"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}"} \nInner Exception: {exception.InnerException}");
            }
        }

        public async Task<IDataResult<int>> RemoveRangeAsync(List<FilterResult> filterResults)
        {
            try
            {
                int affectedRows = await _filterResultDal.RemoveRangeAsync(filterResults);
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
                return new ErrorDataResult<int>(-1, true, $"Exception Message: { $"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}"} \nInner Exception: {exception.InnerException}");
            }
        }

        public async Task<IDataResult<GetFilterResultDto>> GetDtoAsync(Expression<Func<FilterResult, bool>> filter = null)
        {
            try
            {
                var response = await _filterResultDal.GetAsync(filter);
                var mappingResult = _mapper.Map<GetFilterResultDto>(response);
                return new SuccessDataResult<GetFilterResultDto>(mappingResult);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<GetFilterResultDto>(null, $"Exception Message: { $"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}"} \nInner Exception: {exception.InnerException}");
            }
        }

        public async Task<IDataResult<List<GetFilterResultDto>>> GetDtoListAsync(Expression<Func<FilterResult, bool>> filter = null, int takeCount = 20000000)
        {
            try
            {
                var response = (await _filterResultDal.GetListAsync(filter)).ToList();
                var mappingResult = _mapper.Map<List<GetFilterResultDto>>(response).Take(takeCount).ToList();
                return new SuccessDataResult<List<GetFilterResultDto>>(mappingResult);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<List<GetFilterResultDto>>(null, $"Exception Message: { $"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}"} \nInner Exception: {exception.InnerException}");
            }
        }

        public async Task<IDataResult<IQueryable<FilterResult>>> QueryAsync(Expression<Func<FilterResult, bool>> filter = null)
        {
            try
            {
                var response = await _filterResultDal.QueryAsync(filter);
                return new SuccessDataResult<IQueryable<FilterResult>>(response);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<IQueryable<FilterResult>>(null, $"Exception Message: { $"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}"} \nInner Exception: {exception.InnerException}");
            }
        }
        #endregion
    }
}
