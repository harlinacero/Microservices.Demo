using Microservices.Demo.Policy.API.Infrastructure.Data.Context;
using Microservices.Demo.Policy.API.Infrastructure.Data.Entities;
using Microservices.Demo.Policy.API.Infrastructure.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Policy.API.Infrastructure.Messaging.RabbitMq.Outbox
{
    public class OutboxEventPublisher : IEventPublisher
    {
        private readonly IUnitOfWork _unitOfWork;

        public OutboxEventPublisher(IUnitOfWork unitOfWork,IConfiguration configuration)
        {
            //var policyConnection = configuration.GetConnectionString("PolicyConnection");
            //var optionsBuilder = new DbContextOptionsBuilder<PolicyDbContext>();
            //optionsBuilder.UseSqlServer(policyConnection);
            //PolicyDbContext policyDbContext = new PolicyDbContext(optionsBuilder.Options);

            //_unitOfWork = new UnitOfWork(policyDbContext);
            _unitOfWork = unitOfWork;
        }

        public async Task PublishMessage<T>(T msg)
        {
            await _unitOfWork.SaveAsync(new Message(msg));
        }
    }
}
