namespace Microservices.Demo.Pricing.API.Infrastructure.Data
{
    using Marten;
    using Marten.Services;
    using Microservices.Demo.Pricing.API.Infrastructure.Data.Entities;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microservices.Demo.Pricing.API.Infrastructure.Data.UnitOfWork;
    using Microservices.Demo.Pricing.API.Infrastructure.Data.Init;
    using Microsoft.AspNetCore.Builder;

    public static class Extensions
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            var pricingConnection = configuration.GetConnectionString("PricingConnection");
            
            services.AddSingleton(CreateDocumentStore(pricingConnection));

            services.AddScoped<UnitOfWork.IUnitOfWork, UnitOfWork.UnitOfWork>();

            return services;
        }

        public static IServiceCollection AddPricingDemoInitializer(this IServiceCollection services)
        {
            services.AddScoped<DataLoader>();
            return services;
        }

        public static void UseDbInitializer(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var initializer = scope.ServiceProvider.GetService<Init.DataLoader>();
                initializer.Seed().Wait();
            }
        }

        private static IDocumentStore CreateDocumentStore(string cn)
        {
            return DocumentStore.For(_ =>
            {
                _.Connection(cn);
                _.DatabaseSchemaName = "policy";
                _.Serializer(CustomizeJsonSerializer());

                _.Schema.For<Tariff>().Duplicate(t => t.Code, pgType: "varchar(50)", configure: idx => idx.IsUnique = true);
            });
        }

        private static JsonNetSerializer CustomizeJsonSerializer()
        {
            var serializer = new JsonNetSerializer();

            serializer.Customize(_ =>
            {
                _.ContractResolver = new ProtectedSettersContractResolver();
            });

            return serializer;
        }
    }
}
