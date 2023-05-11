using AutoMapper;
using Business.Constants;
using Core.Entities.Concrete.ExternalDbEntities;
using Core.Utilities.Results;
using SBA.Business.Abstract;
using SBA.ExternalDataAccess.Abstract;
using System.Linq.Expressions;

namespace SBA.Business.Concrete
{
    public class AiDataHolderManager : IAiDataHolderService
    {
        private readonly IAiDataHolderDal _aiDataHolderDal;
        private readonly IMapper _mapper;

        public AiDataHolderManager(IAiDataHolderDal aiDataHolderDal,
                             IMapper mapper)
        {
            _aiDataHolderDal = aiDataHolderDal;
            _mapper = mapper;
        }

        public IDataResult<int> Add(AiDataHolder aiDataHolderModel)
        {
            try
            {
                int affectedRows = _aiDataHolderDal.Add(aiDataHolderModel);

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

                var deletableEntity = _aiDataHolderDal.Get(x => x.Id == Id);

                if (deletableEntity == null)
                {
                    dataResult = new SuccessDataResult<int>(-1, Messages.DeletableDataWasNotFound);
                    return dataResult;
                }

                int affectedRows = _aiDataHolderDal.Remove(deletableEntity);
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

        public IDataResult<AiDataHolder> Get(Expression<Func<AiDataHolder, bool>> filter)
        {
            try
            {
                var response = _aiDataHolderDal.Get(filter);
                var mappingResult = _mapper.Map<AiDataHolder>(response);
                return new SuccessDataResult<AiDataHolder>(mappingResult);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<AiDataHolder>(null, $"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}");
            }
        }

        public IDataResult<List<AiDataHolder>> GetList(Expression<Func<AiDataHolder, bool>> filter = null)
        {
            try
            {
                var response = _aiDataHolderDal.GetList(filter);
                var mappingResult = _mapper.Map<List<AiDataHolder>>(response);
                return new SuccessDataResult<List<AiDataHolder>>(mappingResult);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<List<AiDataHolder>>(null, $"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}");
            }
        }

        public IDataResult<int> Update(AiDataHolder homeMetaTagGallery)
        {
            try
            {
                int affectedRows = _aiDataHolderDal.Update(homeMetaTagGallery);
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


        public IDataResult<int> UpdateRange(List<AiDataHolder> homeMetaTagGalleries)
        {
            try
            {
                int affectedRows = _aiDataHolderDal.UpdateRange(homeMetaTagGalleries);
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
        public IDataResult<int> AddRange(List<AiDataHolder> aiDataHolders)
        {
            try
            {
                int affectedRows = _aiDataHolderDal.AddRange(aiDataHolders);

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

        public IDataResult<int> RemoveRange(List<AiDataHolder> aiDataHolders)
        {
            try
            {
                int affectedRows = _aiDataHolderDal.RemoveRange(aiDataHolders);
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

        public IDataResult<IQueryable<AiDataHolder>> Query(Expression<Func<AiDataHolder, bool>> filter = null)
        {
            try
            {
                var response = _aiDataHolderDal.Query(filter);
                return new SuccessDataResult<IQueryable<AiDataHolder>>(response);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<IQueryable<AiDataHolder>>(null, $"Exception Message: {$"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}"} \nInner Exception: {exception.InnerException}");
            }
        }


        #region Asynchronous

        public async Task<IDataResult<int>> AddAsync(AiDataHolder aiDataHolder)
        {
            try
            {
                int affectedRows = await _aiDataHolderDal.AddAsync(aiDataHolder);
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

                var deletableEntity = await _aiDataHolderDal.GetAsync(x => x.Id == Id);

                if (deletableEntity == null)
                {
                    dataResult = new SuccessDataResult<int>(-1, Messages.DeletableDataWasNotFound);
                    return dataResult;
                }

                int affectedRows = await _aiDataHolderDal.RemoveAsync(deletableEntity);
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

        public async Task<IDataResult<AiDataHolder>> GetAsync(Expression<Func<AiDataHolder, bool>> filter)
        {
            try
            {
                var response = await _aiDataHolderDal.GetAsync(filter);
                var mappingResult = _mapper.Map<AiDataHolder>(response);
                return new SuccessDataResult<AiDataHolder>(mappingResult);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<AiDataHolder>(null, $"Exception Message: {$"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}"} \nInner Exception: {exception.InnerException}");
            }
        }

        public async Task<IDataResult<List<AiDataHolder>>> GetListAsync(Expression<Func<AiDataHolder, bool>> filter = null)
        {
            try
            {
                var response = (await _aiDataHolderDal.GetListAsync(filter)).ToList();
                var mappingResult = _mapper.Map<List<AiDataHolder>>(response);
                return new SuccessDataResult<List<AiDataHolder>>(mappingResult);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<List<AiDataHolder>>(null, $"Exception Message: {$"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}"} \nInner Exception: {exception.InnerException}");
            }
        }

        public async Task<IDataResult<int>> UpdateAsync(AiDataHolder aiDataHolder)
        {
            try
            {
                int affectedRows = await _aiDataHolderDal.UpdateAsync(aiDataHolder);
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

        public async Task<IDataResult<int>> AddRangeAsync(List<AiDataHolder> aiDataHolders)
        {
            try
            {
                int affectedRows = await _aiDataHolderDal.AddRangeAsync(aiDataHolders);
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

        public async Task<IDataResult<int>> UpdateRangeAsync(List<AiDataHolder> aiDataHolders)
        {
            try
            {
                int affectedRows = await _aiDataHolderDal.UpdateRangeAsync(aiDataHolders);
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

        public async Task<IDataResult<int>> RemoveRangeAsync(List<AiDataHolder> aiDataHolders)
        {
            try
            {
                int affectedRows = await _aiDataHolderDal.RemoveRangeAsync(aiDataHolders);
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

        public async Task<IDataResult<IQueryable<AiDataHolder>>> QueryAsync(Expression<Func<AiDataHolder, bool>> filter = null)
        {
            try
            {
                var response = await _aiDataHolderDal.QueryAsync(filter);
                return new SuccessDataResult<IQueryable<AiDataHolder>>(response);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<IQueryable<AiDataHolder>>(null, $"Exception Message: {$"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}"} \nInner Exception: {exception.InnerException}");
            }
        }
        #endregion
    }
}
