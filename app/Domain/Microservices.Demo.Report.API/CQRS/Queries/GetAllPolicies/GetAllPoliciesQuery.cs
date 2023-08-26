using MediatR;
using Microservices.Demo.Report.API.CQRS.Queries.Infraestucture.Dtos;

namespace Microservices.Demo.Report.API.CQRS.Queries.GetAllPolicies
{
    public class GetAllPoliciesQuery : IRequest<IEnumerable<PolicyDto>>
    {
        public string AgentLogin { get; set; }
    }
}
