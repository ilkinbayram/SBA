using AutoMapper;
using Business.Constants;
using Core.Entities.Concrete.System;
using Core.Entities.Dtos.SystemModels;
using Core.Utilities.Results;
using SBA.Business.Abstract;
using SBA.Business.Mapping;
using SBA.DataAccess.Abstract;
using System.Linq.Expressions;

namespace SBA.Business.Concrete
{
    public class BetSystemManager : IBetSystemService
    {
        private readonly IBetSystemDal _betSystemDal;
        private readonly IStepDal _stepDal;
        private readonly IBundleDal _bundleDal;
        private readonly IMapper _mapper;

        public BetSystemManager(IBetSystemDal betSystemDal,
                                IMapper mapper,
                                IStepDal stepDal,
                                IBundleDal bundleDal)
        {
            _betSystemDal = betSystemDal;
            _mapper = mapper;
            _stepDal = stepDal;
            _bundleDal = bundleDal;
        }

        public void FinaliseStep(int id)
        {

        }

        public void Process(int id)
        {
            var processSystem = _betSystemDal.Get(x=>x.Id == id && x.IsActive);
            if (processSystem == null) return;

            var processSteps = _stepDal.GetList(x => x.IsActive && x.BetSystemId == processSystem.Id);

            var existingStep = processSteps.OrderBy(p=>p.Number).FirstOrDefault(x=>x.Status == Core.Resources.Enums.StepStatus.New);
            existingStep.Status = Core.Resources.Enums.StepStatus.Active;

            var bundlePart = Convert.ToInt32(Math.Floor(processSystem.TotalBalance / existingStep.InsuredBetAmount));

            if (bundlePart / 2 > processSystem.MaxBundleCount)
            {
                // Should add saved step...
            }

            var bundles = _bundleDal.GetList(x=>x.SystemId ==  processSystem.Id && x.IsActive);

            for (int i = 0; i < processSystem.MaxBundleCount * 2; i++)
            {
                
            }


            // Update New -> Active
            _stepDal.Update

        }

        

        public IDataResult<int> Add(CreateSystemDto createDto)
        {
            try
            {
                var entity = createDto.MapToSystemEntityForCreation();
                int affectedRows = _betSystemDal.Add(entity);

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

                var deletableEntity = _betSystemDal.Get(x => x.Id == Id);

                if (deletableEntity == null)
                {
                    dataResult = new SuccessDataResult<int>(-1, Messages.DeletableDataWasNotFound);
                    return dataResult;
                }

                int affectedRows = _betSystemDal.Remove(deletableEntity);
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

        public IDataResult<BetSystem> Get(Expression<Func<BetSystem, bool>> filter)
        {
            try
            {
                var response = _betSystemDal.Get(filter);
                var mappingResult = _mapper.Map<BetSystem>(response);
                return new SuccessDataResult<BetSystem>(mappingResult);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<BetSystem>(null, $"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}");
            }
        }

        public IDataResult<List<BetSystem>> GetList(Expression<Func<BetSystem, bool>> filter = null)
        {
            try
            {
                var response = _betSystemDal.GetList(filter);
                var mappingResult = _mapper.Map<List<BetSystem>>(response);
                return new SuccessDataResult<List<BetSystem>>(mappingResult);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<List<BetSystem>>(null, $"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}");
            }
        }

        public IDataResult<int> Update(BetSystem homeMetaTagGallery)
        {
            try
            {
                int affectedRows = _betSystemDal.Update(homeMetaTagGallery);
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


        public IDataResult<int> UpdateRange(List<BetSystem> homeMetaTagGalleries)
        {
            try
            {
                int affectedRows = _betSystemDal.UpdateRange(homeMetaTagGalleries);
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
        public IDataResult<int> AddRange(List<BetSystem> betSystems)
        {
            try
            {
                int affectedRows = _betSystemDal.AddRange(betSystems);

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

        public IDataResult<int> RemoveRange(List<BetSystem> betSystems)
        {
            try
            {
                int affectedRows = _betSystemDal.RemoveRange(betSystems);
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

        public IDataResult<IQueryable<BetSystem>> Query(Expression<Func<BetSystem, bool>> filter = null)
        {
            try
            {
                var response = _betSystemDal.Query(filter);
                return new SuccessDataResult<IQueryable<BetSystem>>(response);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<IQueryable<BetSystem>>(null, $"Exception Message: {$"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}"} \nInner Exception: {exception.InnerException}");
            }
        }


        #region Asynchronous

        public async Task<IDataResult<int>> AddAsync(BetSystem betSystem)
        {
            try
            {
                int affectedRows = await _betSystemDal.AddAsync(betSystem);
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

                var deletableEntity = await _betSystemDal.GetAsync(x => x.Id == Id);

                if (deletableEntity == null)
                {
                    dataResult = new SuccessDataResult<int>(-1, Messages.DeletableDataWasNotFound);
                    return dataResult;
                }

                int affectedRows = await _betSystemDal.RemoveAsync(deletableEntity);
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

        public async Task<IDataResult<BetSystem>> GetAsync(Expression<Func<BetSystem, bool>> filter)
        {
            try
            {
                var response = await _betSystemDal.GetAsync(filter);
                var mappingResult = _mapper.Map<BetSystem>(response);
                return new SuccessDataResult<BetSystem>(mappingResult);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<BetSystem>(null, $"Exception Message: {$"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}"} \nInner Exception: {exception.InnerException}");
            }
        }

        public async Task<IDataResult<List<BetSystem>>> GetListAsync(Expression<Func<BetSystem, bool>> filter = null)
        {
            try
            {
                var response = (await _betSystemDal.GetListAsync(filter)).ToList();
                var mappingResult = _mapper.Map<List<BetSystem>>(response);
                return new SuccessDataResult<List<BetSystem>>(mappingResult);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<List<BetSystem>>(null, $"Exception Message: {$"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}"} \nInner Exception: {exception.InnerException}");
            }
        }

        public async Task<IDataResult<int>> UpdateAsync(BetSystem betSystem)
        {
            try
            {
                int affectedRows = await _betSystemDal.UpdateAsync(betSystem);
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

        public async Task<IDataResult<int>> AddRangeAsync(List<BetSystem> betSystems)
        {
            try
            {
                int affectedRows = await _betSystemDal.AddRangeAsync(betSystems);
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

        public async Task<IDataResult<int>> UpdateRangeAsync(List<BetSystem> betSystems)
        {
            try
            {
                int affectedRows = await _betSystemDal.UpdateRangeAsync(betSystems);
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

        public async Task<IDataResult<int>> RemoveRangeAsync(List<BetSystem> betSystems)
        {
            try
            {
                int affectedRows = await _betSystemDal.RemoveRangeAsync(betSystems);
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

        public async Task<IDataResult<IQueryable<BetSystem>>> QueryAsync(Expression<Func<BetSystem, bool>> filter = null)
        {
            try
            {
                var response = await _betSystemDal.QueryAsync(filter);
                return new SuccessDataResult<IQueryable<BetSystem>>(response);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<IQueryable<BetSystem>>(null, $"Exception Message: {$"Exception Message: {exception.Message} \nInner Exception: {exception.InnerException}"} \nInner Exception: {exception.InnerException}");
            }
        }
        #endregion
    }
}
