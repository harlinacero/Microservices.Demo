using MediatR;
using Microservices.Demo.Pricing.API.CQRS.Commands.CalculatePrice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Pricing.API.Application
{
    public class PricingApplicationService
    {
        private readonly IMediator _mediator;
        public PricingApplicationService(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        public async Task<CalculatePriceResult> CalculatePriceAsync(CalculatePriceCommand command)
        {
            var productDraft = await _mediator.Send(command);
            return productDraft;
        }
        
    }
}
