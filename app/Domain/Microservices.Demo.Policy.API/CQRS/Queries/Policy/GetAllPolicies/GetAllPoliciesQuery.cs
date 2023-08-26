using MediatR;
using Microservices.Demo.Policy.API.CQRS.Queries.Infrastructure.Dtos.Policy;

namespace Microservices.Demo.Policy.API.CQRS.Queries.Policy.GetAllPolicies
{
    public record GetAllPoliciesQuery() : IRequest<IEnumerable<PolicyDto>>;
}
