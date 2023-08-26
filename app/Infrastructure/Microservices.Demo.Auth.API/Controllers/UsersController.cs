using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservices.Demo.Auth.API.Application;
using Microservices.Demo.Auth.API.Infrastructure.Dto.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Demo.Auth.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private AuthApplicationService _authApplicationService;
        public UsersController(AuthApplicationService authApplicationService)
        {
            _authApplicationService = authApplicationService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post([FromBody] AuthRequest user)
        {
            var authResponse = _authApplicationService.Authenticate(user.Username, user.Password);

            if (authResponse == null)
                return BadRequest(new { message = "Username of password incorrect" });

            return Ok(authResponse);
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_authApplicationService.AgentFromUsername(HttpContext.User.Identity.Name));
        }

    }
}
