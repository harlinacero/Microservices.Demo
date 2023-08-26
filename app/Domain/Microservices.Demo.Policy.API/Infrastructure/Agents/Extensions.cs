using Microservices.Demo.Policy.API.Infrastructure.Agents.Pricing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Policy.API.Infrastructure.Agents
{
    public static class Extensions
    {
        public static IServiceCollection AddRestClients(this IServiceCollection services)
        {
            services.AddSingleton(typeof(IPricingClient), typeof(PricingClient));
            services.AddSingleton(typeof(IPricingAgent), typeof(PricingAgent));
            return services;
        }
    }
}
