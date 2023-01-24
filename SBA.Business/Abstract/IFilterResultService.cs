using Core.Entities.Concrete;
using Core.Entities.Dtos.FilterResult;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SBA.Business.Abstract
{
    public interface IFilterResultService
    {
        IDataResult<List<FilterResult>> GetList(Expression<Func<FilterResult, bool>> filter = null);
        IDataResult<FilterResult> Get(Expression<Func<FilterResult, bool>> filter);
        IDataResult<List<GetFilterResultDto>> GetDtoList(Expression<Func<FilterResult, bool>> filter = null, int takeCount = 20000000);
        IDataResult<GetFilterResultDto> GetDto(Expression<Func<FilterResult, bool>> filter = null);
        IDataResult<int> Add(CreateFilterResultDto entity);
        IDataResult<int> Add(FilterResult entity);
        IDataResult<int> Update(FilterResult entity);
        IDataResult<int> Remove(long Id);
        IDataResult<int> AddRange(List<FilterResult> entities);
        IDataResult<int> UpdateRange(List<FilterResult> entities);
        IDataResult<int> RemoveRange(List<FilterResult> entities);
        IDataResult<IQueryable<FilterResult>> Query(Expression<Func<FilterResult, bool>> filter = null);

        Task<IDataResult<List<GetFilterResultDto>>> GetDtoListAsync(Expression<Func<FilterResult, bool>> filter = null, int takeCount = 20000000);
        Task<IDataResult<GetFilterResultDto>> GetDtoAsync(Expression<Func<FilterResult, bool>> filter = null);
        Task<IDataResult<int>> RemoveRangeAsync(List<FilterResult> entities);
        Task<IDataResult<int>> UpdateRangeAsync(List<FilterResult> entities);
        Task<IDataResult<List<FilterResult>>> GetListAsync(Expression<Func<FilterResult, bool>> filter = null);
        Task<IDataResult<int>> AddRangeAsync(List<FilterResult> entities);
        Task<IDataResult<int>> UpdateAsync(FilterResult entity);
        Task<IDataResult<FilterResult>> GetAsync(Expression<Func<FilterResult, bool>> filter);
        Task<IDataResult<int>> RemoveAsync(long Id);
        Task<IDataResult<int>> AddAsync(FilterResult entity);
        Task<IDataResult<IQueryable<FilterResult>>> QueryAsync(Expression<Func<FilterResult, bool>> filter = null);
    }
}
