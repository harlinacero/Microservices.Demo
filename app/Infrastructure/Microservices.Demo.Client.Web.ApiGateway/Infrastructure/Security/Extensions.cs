using Microservices.Demo.Client.Web.ApiGateway.Infrastructure.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Provider.Eureka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Microservices.Demo.Client.Web.ApiGateway.Infrastructure.Security
{
    public static class Extensions
    {
        public static IServiceCollection AddSecurity(this IServiceCollection services, IConfiguration configuration)
        {
            var webApiGatewaySettingsSection = configuration.GetSection("WebApiGatewaySettings");
            var webApiGatewaySettings = webApiGatewaySettingsSection.Get<WebApiGatewaySettings>();
            var key = Encoding.ASCII.GetBytes(webApiGatewaySettings.Secret);

            services.AddCors(opt => opt.AddPolicy("CorsPolicy",
            builder =>
            {
                builder
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    //.AllowCredentials()
                    .AllowAnyOrigin();
                    //.WithOrigins(webApiGatewaySettingsSection.Get<WebApiGatewaySettings>().AllowedChatOrigins);
            }));

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer("ApiSecurityAuthenticationScheme", x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            //services.AddOcelot().AddEureka().AddCacheManager(x => x.WithDictionaryHandle());
            services.AddOcelot().AddEureka();
            return services;
        }
    }
}
