using Microservices.Demo.Report.API.Domain.Entities;
using RestEase;

namespace Microservices.Demo.Report.API.Infraestucture.Agents.Policies
{
    public interface IPolicyClient
    {
        [Get]
        Task<IEnumerable<Policy>> GetAsync();
    }
}
