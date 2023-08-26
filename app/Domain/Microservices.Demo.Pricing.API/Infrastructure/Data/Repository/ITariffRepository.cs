using Microservices.Demo.Pricing.API.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Pricing.API.Infrastructure.Data.Repository
{
    public interface ITariffRepository
    {
        Task<Tariff> WithCode(string code);

        Task<Tariff> this[string code] { get; }

        void Add(Tariff tariff);

        Task<bool> Exists(string code);
    }
}
