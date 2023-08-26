using Microservices.Demo.Policy.API.Infrastructure.Data.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Policy.API.Infrastructure.Messaging.RabbitMq.Outbox
{
    public class OutboxLogger
    {
        private readonly ILogger<Outbox> logger;

        public OutboxLogger(ILogger<Outbox> logger) => this.logger = logger;

        public void LogPending(IEnumerable<Message> messages)
        {
            logger.LogInformation($"{messages.Count()} messages about to be pushed.");
        }

        public void LogSuccessPush()
        {
            logger.LogInformation("Successfully pushed message");
        }

        public void LogFailedPush(Exception e)
        {
            logger.LogError(e, "Failed to push message from outbox", null);
        }
    }
}
