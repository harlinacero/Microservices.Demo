namespace Microservices.Demo.Report.API.Aplication
{
    public static class Extensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IReportApplicationService, ReportApplicationService>();

            return services;
        }
    }
}
