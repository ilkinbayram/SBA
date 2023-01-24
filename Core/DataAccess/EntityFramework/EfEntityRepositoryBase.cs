using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Core.Entities;


namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>, IEntityQueryableRepository<TEntity>
    where TEntity : class, IEntity, new()
    where TContext : DbContext, new()
    {
        protected readonly TContext Context;

        public EfEntityRepositoryBase(TContext applicationContext)
        {
            Context = applicationContext;
        }

        public virtual int Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
            return Context.SaveChanges();
        }

        public virtual int Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
            return Context.SaveChanges();
        }

        public virtual TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            return Context.Set<TEntity>().FirstOrDefault(filter);
        }

        public virtual List<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            return filter == null
                ? Context.Set<TEntity>().ToList()
                : Context.Set<TEntity>().Where(filter).ToList();
        }

        public virtual int Update(TEntity entity)
        {
            Context.Set<TEntity>().Update(entity);
            return Context.SaveChanges();
        }

        public virtual int AddRange(List<TEntity> entities)
        {
            Context.Set<TEntity>().AddRange(entities);
            return Context.SaveChanges();
        }


        public virtual int UpdateRange(List<TEntity> entities)
        {
            Context.Set<TEntity>().UpdateRange(entities);
            return Context.SaveChanges();
        }

        public virtual int RemoveRange(List<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
            return Context.SaveChanges();
        }
        public virtual TEntity GetById(int id)
        {
            var result = Context.Set<TEntity>().Find(id);
            return result;
        }

        public async virtual Task<IQueryable<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            Context.ChangeTracker.LazyLoadingEnabled = true;
            var result = expression != null
                ? Task.FromResult(Context.Set<TEntity>().Where(expression))
                : Task.FromResult(Context.Set<TEntity>().AsQueryable());
            return await result;
        }

        public async virtual Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            Context.ChangeTracker.LazyLoadingEnabled = true;
            var result = expression != null
                ? Context.Set<TEntity>().Where(expression).ToListAsync()
                : Context.Set<TEntity>().ToListAsync();
            return await result;
        }

        public virtual IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> expression = null)
        {
            Context.ChangeTracker.LazyLoadingEnabled = true;
            var result = expression == null ?
                Context.Set<TEntity>() :
                Context.Set<TEntity>().Where(expression);
            return result;
        }

        public async virtual Task<int> AddAsync(TEntity entity)
        {
            await Context.Set<TEntity>().AddAsync(entity);

            return await Context.SaveChangesAsync();
        }

        public async virtual Task<int> RemoveAsync(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
            return await Context.SaveChangesAsync();
        }

        public async virtual Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await Context.Set<TEntity>().FirstOrDefaultAsync(filter);
        }

        public async virtual Task<int> UpdateAsync(TEntity entity)
        {
            Context.Set<TEntity>().Update(entity);
            return await Context.SaveChangesAsync();
        }

        public async virtual Task<int> AddRangeAsync(List<TEntity> entities)
        {
            await Context.Set<TEntity>().AddRangeAsync(entities);
            return await Context.SaveChangesAsync();
        }


        public async virtual Task<int> UpdateRangeAsync(List<TEntity> entities)
        {
            Context.Set<TEntity>().UpdateRange(entities);
            return await Context.SaveChangesAsync();
        }

        public async virtual Task<int> RemoveRangeAsync(List<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
            return await Context.SaveChangesAsync();
        }

        public async virtual Task<TEntity> GetByIdAsync(int id)
        {
            var result = Context.Set<TEntity>().FindAsync(id);
            return await result;
        }

    }
}
