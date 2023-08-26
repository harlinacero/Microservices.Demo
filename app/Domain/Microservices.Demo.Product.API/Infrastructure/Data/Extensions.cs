using Microservices.Demo.Product.API.Infrastructure.Data.Context;
using Microservices.Demo.Product.API.Infrastructure.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Product.API.Infrastructure.Data
{
    public static class Extensions
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services,IConfiguration configuration)
        {
            var productConnection = configuration.GetConnectionString("ProductConnection");
            services.AddDbContext<ProductDbContext>(options =>
            {
                options.UseSqlServer(productConnection);
            });
            services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }
    }
}
