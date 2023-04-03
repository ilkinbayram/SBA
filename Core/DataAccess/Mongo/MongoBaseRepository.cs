using Core.DataAccess.Configuration.Base;
using Core.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.DataAccess.Mongo
{
    public class MongoBaseRepository<TEntity> : IEntityRepository<TEntity>, IEntityQueryableRepository<TEntity>
        where TEntity : class, IEntity, new()
    {
        protected readonly IMongoDatabase Database;
        private readonly string _collectionName;
        private const string _connectionString = "mongodb+srv://sbaadminuser:System.23444327777@sbacluster.cy4y4fk.mongodb.net/?retryWrites=true&w=majority";
        private const string _databaseName = "sbacluster";

        public MongoBaseRepository(string collectionName)
        {
            var mongoClient = new MongoClient(_connectionString);
            Database = mongoClient.GetDatabase(_databaseName);
            _collectionName = collectionName;
        }

        public MongoBaseRepository(IOptions<IDbConfig> dbConfig, string collectionName)
        {
            var mongoClient = new MongoClient(dbConfig.Value.NOSQL_CONNECTION_STRING);
            Database = mongoClient.GetDatabase(dbConfig.Value.DATABASE_NAME);
            _collectionName = collectionName;
        }

        public TEntity GetById(int id) => Database.GetCollection<TEntity>(_collectionName).Find(x => x.Id == id).FirstOrDefault();

        public TEntity Get(Expression<Func<TEntity, bool>> filter) => Database.GetCollection<TEntity>(_collectionName).Find(filter).FirstOrDefault();

        public List<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            return filter == null
                ? Database.GetCollection<TEntity>(_collectionName).Find(entity => true).ToList()
                : Database.GetCollection<TEntity>(_collectionName).Find(filter).ToList();
        }

        int IEntityRepository<TEntity>.Add(TEntity entity)
        {
            try
            {
                Database.GetCollection<TEntity>(_collectionName).InsertOne(entity);
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        int IEntityRepository<TEntity>.Update(TEntity entity)
        {
            try
            {
                Database.GetCollection<TEntity>(_collectionName).ReplaceOne(x => x.Id == entity.Id, entity);
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int AddRange(List<TEntity> entities)
        {
            try
            {
                //entities.ForEach(x => x.Id = x.SerialUniqueID);
                Database.GetCollection<TEntity>(_collectionName).InsertMany(entities);
                return entities.Count;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int UpdateRange(List<TEntity> entities)
        {
            int result = 0;
            try
            {
                foreach (var entity in entities)
                {
                    Database.GetCollection<TEntity>(_collectionName).ReplaceOne(x => x.Id == entity.Id, entity);
                    result++;
                }
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public int Remove(TEntity entity)
        {
            try
            {
                Database.GetCollection<TEntity>(_collectionName).DeleteOne(x => x.Id == entity.Id);
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int RemoveRange(List<TEntity> entities)
        {
            int result = 0;
            try
            {
                foreach (var entity in entities)
                {
                    Database.GetCollection<TEntity>(_collectionName).DeleteOne(x => x.Id == entity.Id);
                    result++;
                }
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public async Task<int> AddAsync(TEntity entity)
        {
            try
            {
                await Database.GetCollection<TEntity>(_collectionName).InsertOneAsync(entity);
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<int> RemoveAsync(TEntity entity)
        {
            try
            {
                await Database.GetCollection<TEntity>(_collectionName).DeleteOneAsync(x => x.Id == entity.Id);
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter)
        {
            var result = await Database.GetCollection<TEntity>(_collectionName).FindAsync(filter);
            return await result.FirstOrDefaultAsync();
        }

        public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            var result = await Database.GetCollection<TEntity>(_collectionName).FindAsync(entity => true);
            return await result.ToListAsync();
        }

        public async Task<int> UpdateAsync(TEntity entity)
        {
            try
            {
                await Database.GetCollection<TEntity>(_collectionName).ReplaceOneAsync(x => x.Id == entity.Id, entity);
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<int> AddRangeAsync(List<TEntity> entities)
        {
            try
            {
                await Database.GetCollection<TEntity>(_collectionName).InsertManyAsync(entities);
                return entities.Count;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<int> UpdateRangeAsync(List<TEntity> entities)
        {
            int result = 0;
            try
            {
                foreach (var entity in entities)
                {
                    await Database.GetCollection<TEntity>(_collectionName).ReplaceOneAsync(x => x.Id == entity.Id, entity);
                    result++;
                }
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public async Task<int> RemoveRangeAsync(List<TEntity> entities)
        {
            int result = 0;
            try
            {
                foreach (var entity in entities)
                {
                    await Database.GetCollection<TEntity>(_collectionName).DeleteOneAsync(x => x.Id == entity.Id);
                    result++;
                }
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            var result = await Database.GetCollection<TEntity>(_collectionName).FindAsync(x => x.Id == id);
            return await result.FirstOrDefaultAsync();
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> expression = null)
        {
            return expression == null
                ? Database.GetCollection<TEntity>(_collectionName).AsQueryable()
                : Database.GetCollection<TEntity>(_collectionName).AsQueryable().Where(expression);
        }

        public async Task<IQueryable<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            var result = filter == null
                ? Database.GetCollection<TEntity>(_collectionName).AsQueryable()
                : Database.GetCollection<TEntity>(_collectionName).AsQueryable().Where(filter);

            return await Task.FromResult(result);
        }
    }
}
