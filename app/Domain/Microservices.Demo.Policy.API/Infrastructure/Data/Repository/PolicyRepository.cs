namespace Microservices.Demo.Policy.API.Infrastructure.Data.Repository
{
    using Microservices.Demo.Policy.API.Infrastructure.Data.Context;
    using Microservices.Demo.Policy.API.Infrastructure.Data.Entities;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public class PolicyRepository : IPolicyRepository
    {
        private readonly PolicyDbContext _policyDbContext;
        public PolicyRepository(PolicyDbContext policyDbContext)
        {
            _policyDbContext = policyDbContext;
        }

        public void Add(Policy policy)
        {
            _policyDbContext.Policies.Add(policy);
        }

        public async Task<Policy> WithNumber(string number)
        {
            return await _policyDbContext.Policies.Where(p => p.Number == number).FirstOrDefaultAsync();

        }

        public async Task<IEnumerable<Policy>> GetAll()
        {
            return await _policyDbContext.Policies.ToListAsync();
        }
    }
}
