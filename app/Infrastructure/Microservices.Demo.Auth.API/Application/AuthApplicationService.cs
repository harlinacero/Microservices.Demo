using Microservices.Demo.Auth.API.Domain;
using Microservices.Demo.Auth.API.Infrastructure.Data.Entities;
using Microservices.Demo.Auth.API.Infrastructure.Data.Repository;
using Microservices.Demo.Auth.API.Infrastructure.Dto.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Auth.API.Application
{
    public class AuthApplicationService
    {
        private AuthDomainService _authDomainService;
        private UserRepository _userRepository;
        public AuthApplicationService(
            AuthDomainService authDomainService, 
            UserRepository userRepository)
        {
            _authDomainService = authDomainService;
            _userRepository = userRepository;
        }

        public AuthResponse Authenticate(string username, string password)
        {
            AuthResponse authResponse = null;
            User agent = _userRepository.FindByUsername(username);
            string token = _authDomainService.Authenticate(agent, password);
            
            if (token != null)
            {
                authResponse = new AuthResponse
                {
                    BearerToken = token,
                    IsAuthenticated = true,
                    User = ApplicationAdapter.FromUserToUserDto(agent)
                };
            }
            

            return authResponse;
        }

        public UserDto AgentFromUsername(string username)
        {
            User user = _userRepository.FindByUsername(username);
            UserDto userDto = ApplicationAdapter.FromUserToUserDto(user);

            return userDto;
        }
    }
}
