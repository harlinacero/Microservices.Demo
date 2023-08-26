using Microservices.Demo.Auth.API.Infrastructure.Data.Context;
using Microservices.Demo.Auth.API.Infrastructure.Data.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Auth.API.Infrastructure.Data
{
    public static class Extensions
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services)
        {
            services.AddScoped<SecurityDbContext>();
            services.AddScoped<UserRepository>();

            return services;
        }
    }
}
