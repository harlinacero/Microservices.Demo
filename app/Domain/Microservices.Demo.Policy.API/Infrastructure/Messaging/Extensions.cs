using Microservices.Demo.Policy.API.Infrastructure.Configuration;
using Microservices.Demo.Policy.API.Infrastructure.Messaging.RabbitMq.Outbox;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit.DependencyInjection.ServiceCollection;
using RawRabbit.Instantiation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Policy.API.Infrastructure.Messaging
{
    public static class Extensions
    {
        public static IServiceCollection AddMessaging(this IServiceCollection services, IConfiguration configuration)
        {
            RabbitMQSettings rabbitMQSettings = new RabbitMQSettings();
            configuration.GetSection("RabbitMQSettings").Bind(rabbitMQSettings);

            services.AddRawRabbit(new RawRabbitOptions
            {
                ClientConfiguration = new RawRabbit.Configuration.RawRabbitConfiguration
                {
                    Username = rabbitMQSettings.Username,
                    Password = rabbitMQSettings.Password,
                    VirtualHost = rabbitMQSettings.VirtualHost,
                    Port = rabbitMQSettings.Port,
                    Hostnames = rabbitMQSettings.Hostnames,
                    RequestTimeout = TimeSpan.FromSeconds(rabbitMQSettings.RequestTimeout),
                    PublishConfirmTimeout = TimeSpan.FromSeconds(rabbitMQSettings.PublishConfirmTimeout),
                    RecoveryInterval = TimeSpan.FromSeconds(rabbitMQSettings.RecoveryInterval),
                    PersistentDeliveryMode = rabbitMQSettings.PersistentDeliveryMode,
                    AutoCloseConnection = rabbitMQSettings.AutoCloseConnection,
                    AutomaticRecovery = rabbitMQSettings.AutomaticRecovery,
                    TopologyRecovery = rabbitMQSettings.TopologyRecovery,

                    Exchange = new RawRabbit.Configuration.GeneralExchangeConfiguration
                    {
                        Durable = true,
                        AutoDelete = false,
                        Type = RawRabbit.Configuration.Exchange.ExchangeType.Topic
                    },
                    Queue = new RawRabbit.Configuration.GeneralQueueConfiguration
                    {
                        Durable = true,
                        AutoDelete = false,
                        Exclusive = false
                    }
                }
            });

            services.AddScoped<IEventPublisher, OutboxEventPublisher>();
            services.AddSingleton<Outbox>();
            services.AddHostedService<OutboxSendingService>();
            return services;
        }
    }
}
