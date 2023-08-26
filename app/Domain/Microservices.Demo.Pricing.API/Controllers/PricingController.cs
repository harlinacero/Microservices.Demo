using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservices.Demo.Pricing.API.Application;
using Microservices.Demo.Pricing.API.CQRS.Commands.CalculatePrice;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Demo.Pricing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PricingController : ControllerBase
    {
        private readonly PricingApplicationService _pricingApplicationService;
        public PricingController(PricingApplicationService pricingApplicationService)
        {
            _pricingApplicationService = pricingApplicationService;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CalculatePriceCommand command)
        {
            return new JsonResult(await _pricingApplicationService.CalculatePriceAsync(command));
        }
    }
}