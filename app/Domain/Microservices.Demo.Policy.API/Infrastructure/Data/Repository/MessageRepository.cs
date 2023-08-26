namespace Microservices.Demo.Policy.API.Infrastructure.Data.Repository
{
    using Microservices.Demo.Policy.API.Infrastructure.Data.Context;
    using Microservices.Demo.Policy.API.Infrastructure.Data.Entities;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public class MessageRepository: IMessageRepository
    {
        private readonly PolicyDbContext _policyDbContext;
        public MessageRepository(PolicyDbContext policyDbContext)
        {
            _policyDbContext = policyDbContext;
        }

        public void Add(Message message)
        {
            _policyDbContext.Messages.Add(message);
        }

        public IList<Message> FetchPendingMessages(int count)
        {
            var query = _policyDbContext.Messages.OrderBy(o => o.MessageId).Take(count);
            return query.ToList();
        }
    }
}
    