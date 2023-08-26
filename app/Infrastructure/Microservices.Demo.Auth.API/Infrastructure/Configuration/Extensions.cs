using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Auth.API.Infrastructure.Configuration
{
    public static class Extensions
    {
        public static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            var appSettingsSection = configuration.GetSection("AuthSettings");
            services.Configure<AuthSettings>(appSettingsSection);

            var securityDatabaseSettings = configuration.GetSection("SecurityDatabaseSettings");
            services.Configure<SecurityDatabaseSettings>(securityDatabaseSettings);
            
            return services;
        }
    }
}
