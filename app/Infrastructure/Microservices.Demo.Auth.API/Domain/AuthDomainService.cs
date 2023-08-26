using Microservices.Demo.Auth.API.Infrastructure.Configuration;
using Microservices.Demo.Auth.API.Infrastructure.Data.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Demo.Auth.API.Domain
{
    public class AuthDomainService
    {        
        private readonly AuthSettings _authSettings;

        public AuthDomainService(IOptions<AuthSettings> authSettings)
        {
              this._authSettings = authSettings.Value;
        }

        public string Authenticate(User agent, string password)
        {
            if (agent == null)
                return null;

            if (!agent.PasswordMatches(password))
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_authSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("sub", agent.Username),
                    new Claim(ClaimTypes.Name, agent.Username),
                    new Claim(ClaimTypes.Role, "SALESMAN"),
                    new Claim(ClaimTypes.Role, "USER"),
                    new Claim("avatar", agent.Avatar),
                    new Claim("userType", "SALESMAN"),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
