namespace Microservices.Demo.Report.API.Infraestucture.Configuration
{
    public static partial class Extension
    {
        public static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            

            var servicesUrl = configuration.GetSection("ServicesUrl");
            services.Configure<ServicesUrl>(servicesUrl);

            return services;
        }
    }
}
