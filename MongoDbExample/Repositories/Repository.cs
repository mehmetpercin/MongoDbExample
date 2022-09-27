using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDbExample.Context;
using MongoDbExample.Models;
using MongoDbExample.Repositories.Interfaces;
using MongoDbExample.Settings;
using System.Linq.Expressions;

namespace MongoDbExample.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly MongoDbContext _context;
        private readonly IMongoCollection<TEntity> _collection;
        public Repository(IOptions<DatabaseSettings> settings)
        {
            _context = new MongoDbContext(settings);
            _collection = _context.GetCollection<TEntity>();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _collection.InsertOneAsync(entity);
            return entity;
        }

        public async Task AddRangeAsync(List<TEntity> entities)
        {
            await _collection.InsertManyAsync(entities);
        }

        public async Task DeleteByIdAsync(string id)
        {
            await _collection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task DeleteByFilterAsync(Expression<Func<TEntity, bool>> filter)
        {
            await _collection.DeleteManyAsync(filter);
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<List<TEntity>> GetByFilterAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _collection.Find(filter).ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(string id)
        {
            return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var exists = await _collection.Find(x => x.Id == entity.Id).FirstOrDefaultAsync();
            entity.CreatedDate = exists.CreatedDate;
            await _collection.ReplaceOneAsync(x => x.Id == exists.Id, entity);
            return entity;
        }
    }
}
