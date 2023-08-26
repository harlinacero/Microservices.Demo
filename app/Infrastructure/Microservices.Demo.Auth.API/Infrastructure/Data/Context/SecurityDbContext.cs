using Microservices.Demo.Auth.API.Infrastructure.Configuration;
using Microservices.Demo.Auth.API.Infrastructure.Data.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Auth.API.Infrastructure.Data.Context
{
    public class SecurityDbContext
    {
        public readonly SecurityDatabaseSettings _securityDatabaseSettings;
        public readonly IMongoCollection<User> Users;
        public SecurityDbContext(IOptions<SecurityDatabaseSettings> securityDatabaseSettings)
        {
            _securityDatabaseSettings = securityDatabaseSettings.Value;
            var client = new MongoClient(_securityDatabaseSettings.ConnectionString);
            var database = client.GetDatabase(_securityDatabaseSettings.DatabaseName);

            Users = database.GetCollection<User>(_securityDatabaseSettings.UsersCollectionName);
        }
    }
}
