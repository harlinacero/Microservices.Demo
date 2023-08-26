using Microservices.Demo.Policy.API.Infrastructure.Data.Context;
using Microservices.Demo.Policy.API.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Policy.API.Infrastructure.Data.Repository
{
    public class OfferRepository: IOfferRepository
    {
        private readonly PolicyDbContext _policyDbContext;

        public OfferRepository(PolicyDbContext policyDbContext)
        {
            _policyDbContext = policyDbContext;
        }

        public async Task Add(Offer offer)
        {
            await _policyDbContext.Offers.AddAsync(offer);
        }

        public async Task<Offer> WithNumber(string number)
        {
            return await _policyDbContext.Offers
                .Include(i=>i.PolicyValidityPeriod)
                .Include(i=>i.OfferCovers)
                .Include(i => i.OfferStatus)
                .Include(i => i.PolicyHolder)
                .Where(o => o.Number == number).FirstOrDefaultAsync();
        }
    }
}
