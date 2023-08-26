using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservices.Demo.Policy.API.Application;
using Microservices.Demo.Policy.API.CQRS.Commands.Policy.CreatePolicy;
using Microservices.Demo.Policy.API.CQRS.Commands.Policy.TerminatePolicy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Demo.Policy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PoliciesController : ControllerBase
    {
        private readonly PolicyApplicationService _policyApplicationService;
        public PoliciesController(PolicyApplicationService policyApplicationService)
        {
            _policyApplicationService = policyApplicationService;
        }

        // POST 
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreatePolicyCommand command)
        {
            return new JsonResult(await _policyApplicationService.CreatePolicyAsync(command));
        }

        // GET 
        [HttpGet("{policyNumber}")]
        public async Task<ActionResult> Get(string policyNumber)
        {            
            return new JsonResult(await _policyApplicationService.GetPolicyDetails(policyNumber));
        }

        // GET 
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return new JsonResult(await _policyApplicationService.GetAllPolicies());
        }

        // DELETE
        [HttpDelete("/terminate")]
        public async Task<ActionResult> Post([FromBody] TerminatePolicyCommand command)
        {
            return new JsonResult(await _policyApplicationService.TerminatePolicy(command));
        }
    }
}