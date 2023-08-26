using MediatR;
using Microservices.Demo.Report.API.CQRS.Queries.GetAllPolicies;
using Microservices.Demo.Report.API.CQRS.Queries.Infraestucture.Dtos;

namespace Microservices.Demo.Report.API.Aplication
{
    public class ReportApplicationService : IReportApplicationService
    {
        private readonly IMediator _mediator;

        public ReportApplicationService(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public Task<IEnumerable<PolicyDto>> GetPolicies(string AgentLogin)
        {
            return _mediator.Send(new GetAllPoliciesQuery { AgentLogin = AgentLogin });
        }
    }
}
