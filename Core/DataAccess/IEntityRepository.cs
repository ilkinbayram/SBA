using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Core.Entities;



namespace Core.DataAccess
{
    public interface IEntityRepository<T> where T:class,IEntity,new()
    {
        T GetById(int id);
        T Get(Expression<Func<T, bool>> filter);
        List<T> GetList(Expression<Func<T, bool>> filter=null);
        int Add(T entity);
        int Update(T entity);
        int AddRange(List<T> entities);
        int UpdateRange(List<T> entities);
        int Remove(T entity);
        int RemoveRange(List<T> entities);


        Task<int> AddAsync(T entity);
        Task<int> RemoveAsync(T entity);
        Task<T> GetAsync(Expression<Func<T, bool>> filter);
        Task<List<T>> GetListAsync(Expression<Func<T, bool>> expression = null);
        Task<int> UpdateAsync(T entity);
        Task<int> AddRangeAsync(List<T> entities);
        Task<int> UpdateRangeAsync(List<T> entities);
        Task<int> RemoveRangeAsync(List<T> entities);
        Task<T> GetByIdAsync(int id);
    }
}
