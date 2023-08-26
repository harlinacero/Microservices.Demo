using Microservices.Demo.Auth.API.Infrastructure.Data.Context;
using Microservices.Demo.Auth.API.Infrastructure.Data.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Auth.API.Infrastructure.Data.Repository
{
    public class UserRepository
    {
        public SecurityDbContext _securityDbContext { get;}
        public IMongoCollection<User> _users { get;}
        public UserRepository(SecurityDbContext securityDbContext)
        {
            _securityDbContext = securityDbContext;
            _users = _securityDbContext.Users;
        }

        public List<User> Get() =>
           _users.Find(_users => true).ToList();

        public User Get(string id) =>
            _users.Find<User>(user => user.Id == id).FirstOrDefault();

        public User Create(User user)
        {
            _users.InsertOne(user);
            return user;
        }

        public void Update(string id, User userIn) =>
            _users.ReplaceOne(user => user.Id == id, userIn);

        public void Remove(User userIn) =>
            _users.DeleteOne(user => user.Id == userIn.Id);

        public void Remove(string id) =>
            _users.DeleteOne(user => user.Id == id);

        public User FindByUsername(string username) =>
            _users.Find(user => user.Username == username).FirstOrDefault();
    }
}
