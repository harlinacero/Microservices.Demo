using Microservices.Demo.Auth.API.Infrastructure.Data.Entities;
using Microservices.Demo.Auth.API.Infrastructure.Dto.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Auth.API.Application
{
    public static class ApplicationAdapter
    {
        public static UserDto FromUserToUserDto(User user)
        {
            UserDto userDto = new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Avatar = user.Avatar,
                AvailableProducts = user.AvailableProducts
            };

            return userDto;
        }
    }
}
