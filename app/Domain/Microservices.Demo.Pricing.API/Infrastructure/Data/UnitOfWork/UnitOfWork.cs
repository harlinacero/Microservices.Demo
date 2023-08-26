using Marten;
using Microservices.Demo.Pricing.API.Infrastructure.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Pricing.API.Infrastructure.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDocumentSession session;
        private readonly ITariffRepository tariffs;

        public UnitOfWork(IDocumentStore documentStore)
        {
            session = documentStore.LightweightSession();
            tariffs = new TariffRepository(session);
        }

        public ITariffRepository TariffsRepository => tariffs;

        public async Task CommitChanges()
        {
            await session.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                session.Dispose();
            }

        }
    }
}
