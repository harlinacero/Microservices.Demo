using Microservices.Demo.Policy.API.Infrastructure.Agents.Pricing.Commands;
using RestEase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Policy.API.Infrastructure.Agents.Pricing
{
    public interface IPricingClient
    {

        [Post]
        Task<CalculatePriceResult> CalculatePrice([Body] CalculatePriceCommand cmd);
    }
}
