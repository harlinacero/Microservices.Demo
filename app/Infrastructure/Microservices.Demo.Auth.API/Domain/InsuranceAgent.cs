using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Auth.API.Infrastructure.Data.Entities
{
    public partial class User
    {
        public User(string username, string password, string firstName, string lastName, string avatar, List<string> availableProducts)
        {
            Username = username;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Avatar = avatar;
            AvailableProducts = availableProducts;
        }
        public bool PasswordMatches(string passwordToTest)
        {
            return Password == passwordToTest;
        }
    }
}
