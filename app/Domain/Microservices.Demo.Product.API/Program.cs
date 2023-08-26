using Elastic.Apm.Api;
using Elastic.Apm.NetCoreAll;
using Jaeger.Reporters;
using Jaeger.Samplers;
using Jaeger.Senders.Thrift;
using Jaeger;
using MediatR;
using Microservices.Demo.Product.API.Application;
using Microservices.Demo.Product.API.Domain;
using Microservices.Demo.Product.API.Infrastructure.Configuration;
using Microservices.Demo.Product.API.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Debug;
using Newtonsoft.Json.Serialization;
using Serilog;
using Steeltoe.Discovery.Client;
using Steeltoe.Extensions.Configuration;
using Steeltoe.Extensions.Configuration.ConfigServer;
using System.Reflection;
using OpenTelemetry;
using OpenTelemetry.Trace;
using OpenTelemetry.Resources;
using Microservices.Demo.Product.API.Infrastructure.Jaeger;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureLogging(loggign =>
{
    loggign.SetMinimumLevel(LogLevel.Information);
    loggign.AddConsole();
});

builder.Host.ConfigureAppConfiguration((webHostBuilderContext, configurationBuilder) =>
{
    ILoggerFactory factory = new LoggerFactory();
    var provider = new DebugLoggerProvider();
    factory.AddProvider(provider);

    var hostingEnvironment = webHostBuilderContext.HostingEnvironment;
    configurationBuilder.AddConfigServer(hostingEnvironment.EnvironmentName, factory);
});

builder.Host.UseSerilog();
Log.Logger = new LoggerConfiguration()
               .WriteTo.Http("http://microservices.demo.logstash:28080", null)
               .CreateLogger();

builder.Services.AddJaeger();
builder.Services.AddDiscoveryClient(builder.Configuration);
builder.Services.AddConfigurations(builder.Configuration);
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddApplicationServices();
builder.Services.AddDomainServices();
builder.Services.AddDataServices(builder.Configuration);

builder.Services.AddControllers()
.AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
    options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAllElasticApm(builder.Configuration);
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
