using Marten;
using Microservices.Demo.Pricing.API.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Pricing.API.Infrastructure.Data.Repository
{
    public class TariffRepository : ITariffRepository
    {
        private readonly IDocumentSession session;
        public TariffRepository(IDocumentSession session)
        {
            this.session = session;
        }

        public void Add(Tariff tariff)
        {
            session.Insert(tariff);
        }

        public async Task<bool> Exists(string code)
        {
            return await session.Query<Tariff>().AnyAsync(t => t.Code == code);
        }

        public async Task<Tariff> WithCode(string code)
        {
            return await session.Query<Tariff>().FirstOrDefaultAsync(t => t.Code == code);
        }

        public Task<Tariff> this[string code] => WithCode(code);
    }
}
