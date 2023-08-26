using Elastic.Apm.NetCoreAll;
using Microservices.Demo.Client.Web.ApiGateway;
using Microservices.Demo.Client.Web.ApiGateway.Infrastructure.Jeager;
using Microservices.Demo.Client.Web.ApiGateway.Infrastructure.Security;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Debug;
using Ocelot.Middleware;
using Serilog;
using Steeltoe.Extensions.Configuration;
using Steeltoe.Extensions.Configuration.ConfigServer;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureLogging(loggign =>
{
    loggign.SetMinimumLevel(LogLevel.Information);
    loggign.AddConsole();
});

builder.Host.ConfigureAppConfiguration((webHostBuilderContext, configurationBuilder) =>
{
    var config = new ColoredConsoleLoggerConfiguration
    {
        LogLevel = LogLevel.Information,
        Color = ConsoleColor.Red
    };
    ILoggerFactory factory = new LoggerFactory();
    var provider = new DebugLoggerProvider();
    var providerConsole = new ColoredConsoleLoggerProvider(config);

    //new ConsoleLoggerProvider((category, logLevel) => logLevel >= LogLevel.Debug, false)
    factory.AddProvider(provider);

    var hostingEnvironment = webHostBuilderContext.HostingEnvironment;
    configurationBuilder.AddConfigServer(hostingEnvironment.EnvironmentName, factory);
});

builder.Host.UseSerilog();
Log.Logger = new LoggerConfiguration()
               .WriteTo.Http("http://microservices.demo.logstash:28080", null)
               .CreateLogger();

builder.Services.AddJaeger();
builder.Services.AddSecurity(builder.Configuration);

var app = builder.Build();

app.UseAllElasticApm(builder.Configuration);
app.UseCors("CorsPolicy");
app.UseOcelot().Wait();

app.Run();
