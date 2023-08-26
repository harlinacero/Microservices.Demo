using Jaeger;
using Jaeger.Reporters;
using Jaeger.Samplers;
using Jaeger.Senders.Thrift;
using OpenTelemetry;
using OpenTelemetry.Exporter;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using OpenTracing;
using OpenTracing.Contrib.NetCore.Configuration;
using OpenTracing.Util;
using System.Reflection;
using Tracer = Jaeger.Tracer;

namespace Microservices.Demo.Auth.API.Infrastructure.Jaeger
{
    public static class JaegerServiceCollectionExtensions
    {        
        private static readonly Uri _jaegerUri = new Uri("http://microservices.demo.jaeger:4317");

        public static IServiceCollection AddJaeger(this IServiceCollection services)
        {
            string serviceName = Assembly.GetEntryAssembly().GetName().Name;

            services.AddOpenTelemetry().WithTracing(builder =>
            {
                builder.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(serviceName))
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddSqlClientInstrumentation(opts => opts.SetDbStatementForText = true)                
                .AddOtlpExporter(opts => { opts.Endpoint = _jaegerUri; });
            })
            .StartWithHost();

            return services;
        }

        public static IServiceCollection AddJaegerTracer(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddSingleton<ITracer>(serviceProvider =>
            {                
                string serviceName = Assembly.GetEntryAssembly().GetName().Name;

                ILoggerFactory loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

                ISampler sampler = new ConstSampler(sample: true);

                IReporter reporter = new RemoteReporter.Builder()
                    .WithSender(new HttpSender.Builder(_jaegerUri.ToString()).Build())
                    .Build();

                ITracer tracer = new Tracer.Builder(serviceName)
                    .WithLoggerFactory(loggerFactory)
                    .WithSampler(sampler)
                    .WithReporter(reporter)
                    .Build();

                GlobalTracer.Register(tracer);

                return tracer;
            });
            
            services.Configure<HttpHandlerDiagnosticOptions>(options =>
            {
                options.IgnorePatterns.Add(request => _jaegerUri.IsBaseOf(request.RequestUri));
            });

            return services;
        }
    }
}
