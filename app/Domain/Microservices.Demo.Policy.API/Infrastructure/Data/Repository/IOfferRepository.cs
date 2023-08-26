using Microservices.Demo.Policy.API.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Policy.API.Infrastructure.Data.Repository
{
    public interface IOfferRepository
    {
        Task Add(Offer offer);
        Task<Offer> WithNumber(string number);
    }
}
