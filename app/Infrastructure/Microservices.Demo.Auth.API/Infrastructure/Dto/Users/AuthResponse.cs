using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Auth.API.Infrastructure.Dto.Users
{
    public class AuthResponse
    {
        public string BearerToken { get; set; }
        public bool IsAuthenticated { get; set; }
        public UserDto User { get; set; }
    }
}