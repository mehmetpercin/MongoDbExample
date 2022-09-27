using Microsoft.Extensions.Options;
using MongoDbExample.Models;
using MongoDbExample.Repositories.Interfaces;
using MongoDbExample.Settings;

namespace MongoDbExample.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(IOptions<DatabaseSettings> settings) : base(settings)
        {
        }
    }
}
