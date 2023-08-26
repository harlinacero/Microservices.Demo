using Microservices.Demo.Policy.API.Infrastructure.Data.Context;
using Microservices.Demo.Policy.API.Infrastructure.Data.Entities;
using Microservices.Demo.Policy.API.Infrastructure.Data.Repository;
using Microservices.Demo.Policy.API.Infrastructure.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RawRabbit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Policy.API.Infrastructure.Messaging.RabbitMq.Outbox
{
    public class Outbox
    {
        private readonly IBusClient _busClient;
        private readonly OutboxLogger _outboxLogger;
        private readonly string _policyConnection;

        public Outbox(IBusClient busClient, ILogger<Outbox> outboxLogger, IConfiguration configuration)
        {
            _policyConnection = configuration.GetConnectionString("PolicyConnection");
            _busClient = busClient;           
            _outboxLogger = new OutboxLogger(outboxLogger);
        }


        public async Task PushPendingMessages()
        {
            var messagesToPush = FetchPendingMessages();
            _outboxLogger.LogPending(messagesToPush);

            foreach (var msg in messagesToPush)
            {
                if (!await TryPush(msg))
                    break;
            }
        }

        private IList<Message> FetchPendingMessages()
        {
            var optionsBuilder = new DbContextOptionsBuilder<PolicyDbContext>();
            optionsBuilder.UseSqlServer(_policyConnection);
            IList<Message> messagesToPush = new List<Message>();
            using (PolicyDbContext policyDbContext = new PolicyDbContext(optionsBuilder.Options))
            {
                IMessageRepository messageRepository = new MessageRepository(policyDbContext);
                messagesToPush = messageRepository.FetchPendingMessages(50);
            }         
            return messagesToPush;
        }

        private async Task<bool> TryPush(Message message)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PolicyDbContext>();
            optionsBuilder.UseSqlServer(_policyConnection);
            IList<Message> messagesToPush = new List<Message>();
            using (PolicyDbContext policyDbContext = new PolicyDbContext(optionsBuilder.Options))
            {
                using (var tx = policyDbContext.Database.BeginTransaction())
                {
                    try
                    {
                        await PublishMessage(message);
                        policyDbContext.Messages.Remove(message);
                        policyDbContext.SaveChanges();
                        tx.Commit();
                        _outboxLogger.LogSuccessPush();
                        return true;
                    }
                    catch (Exception e)
                    {
                        _outboxLogger.LogFailedPush(e);
                        tx?.Rollback();
                        return false;
                    }
                }
            }
        }

        private async Task PublishMessage(Message msg)
        {
            var deserializedMsg = msg.RecreateMessage();
            var messageKey = deserializedMsg.GetType().Name.ToLower();
            await _busClient.BasicPublishAsync(deserializedMsg,
                cfg =>
                {
                    cfg.OnExchange("Microservice.Demo.Exchange").WithRoutingKey(messageKey);
                });
        }

    }
}
