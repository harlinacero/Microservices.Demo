using Microservices.Demo.Pricing.API.Infrastructure.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Pricing.API.Infrastructure.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        ITariffRepository TariffsRepository { get; }

        Task CommitChanges();
    }
}
