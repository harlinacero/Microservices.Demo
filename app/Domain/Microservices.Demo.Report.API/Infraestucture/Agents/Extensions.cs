using Microservices.Demo.Report.API.Infraestucture.Agents.Policies;
using Microservices.Demo.Report.API.Infraestucture.Agents.Products;

namespace Microservices.Demo.Report.API.Infraestucture.Agents
{
    public static class Extensions
    {
        public static IServiceCollection AddRestClients(this IServiceCollection services)
        {
            services.AddScoped<IProductClient, ProductClient>();
            services.AddScoped<IPolicyClient, PolicyClient>();
            //services.AddHttpClient<IProductClient, ProductClient>();
            //services.AddHttpClient<IPolicyClient, PolicyClient>();
            return services;
        }
    }
}
