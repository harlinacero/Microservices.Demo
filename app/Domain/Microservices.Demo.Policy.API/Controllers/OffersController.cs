using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microservices.Demo.Policy.API.Application;
using Microservices.Demo.Policy.API.CQRS.Commands.Offer.CreateOffer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Demo.Policy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OffersController : ControllerBase
    {
        private readonly OfferApplicationService _offerApplicationService;
        public OffersController(OfferApplicationService offerApplicationService)
        {
            _offerApplicationService = offerApplicationService;
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateOfferCommand command, [FromHeader] string AgentLogin)
        {
            return new JsonResult(await _offerApplicationService.CreateOfferAsync(command, AgentLogin));
        }

    }
}