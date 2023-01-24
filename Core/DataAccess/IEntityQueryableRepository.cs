using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Core.Entities;


namespace Core.DataAccess
{
    public interface IEntityQueryableRepository<T> 
        where T: class, IEntity, new() 
    {
        IQueryable<T> Query(Expression<Func<T, bool>> expression = null);
        Task<IQueryable<T>> QueryAsync(Expression<Func<T, bool>> filter = null);
    }
}
