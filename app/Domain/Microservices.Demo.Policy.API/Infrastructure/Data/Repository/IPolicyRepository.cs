
namespace Microservices.Demo.Policy.API.Infrastructure.Data.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microservices.Demo.Policy.API.Infrastructure.Data.Entities;
    public interface IPolicyRepository
    {
        void Add(Policy policy);

        Task<Policy> WithNumber(string number);

        Task<IEnumerable<Policy>> GetAll();
    }
}
