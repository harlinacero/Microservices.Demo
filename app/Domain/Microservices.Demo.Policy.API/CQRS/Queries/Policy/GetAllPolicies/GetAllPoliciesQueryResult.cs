using Microservices.Demo.Policy.API.CQRS.Queries.Infrastructure.Dtos.Policy;

namespace Microservices.Demo.Policy.API.CQRS.Queries.Policy.GetAllPolicies
{
    public class GetAllPoliciesQueryResult
    {
        public IEnumerable<PolicyDetailsDto> Policies { get; set; }
    }
}
