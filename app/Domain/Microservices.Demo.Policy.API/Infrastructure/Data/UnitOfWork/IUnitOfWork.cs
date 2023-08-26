using Microservices.Demo.Policy.API.Infrastructure.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Policy.API.Infrastructure.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IOfferRepository Offers { get; }

        IPolicyRepository Policies { get; }

        IMessageRepository Messages { get; }

        Task CommitChanges();
        Task SaveAsync(object obj);
    }
}
