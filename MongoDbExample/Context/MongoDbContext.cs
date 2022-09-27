using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDbExample.Settings;

namespace MongoDbExample.Context
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;
        public MongoDbContext(IOptions<DatabaseSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<TEntity> GetCollection<TEntity>()
        {
            return _database.GetCollection<TEntity>(typeof(TEntity).Name.Trim()+"Collection");
        }

        public IMongoDatabase GetDatabase()
        {
            return _database;
        }
    }
}
