using MediatR;
using Microservices.Demo.Policy.API.CQRS.Commands.Policy.CreatePolicy;
using Microservices.Demo.Policy.API.CQRS.Commands.Policy.TerminatePolicy;
using Microservices.Demo.Policy.API.CQRS.Queries.Infrastructure.Dtos.Policy;
using Microservices.Demo.Policy.API.CQRS.Queries.Policy.GetAllPolicies;
using Microservices.Demo.Policy.API.CQRS.Queries.Policy.GetPolicyDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Policy.API.Application
{
    public class PolicyApplicationService
    {
        private readonly IMediator _mediator;
        public PolicyApplicationService(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<CreatePolicyResult> CreatePolicyAsync(CreatePolicyCommand command)
        {
            var policyResult = await _mediator.Send(command);
            return policyResult;
        }
        public async Task<GetPolicyDetailsQueryResult> GetPolicyDetails(string policyNumber)
        {
            var result = await _mediator.Send(new GetPolicyDetailsQuery { PolicyNumber = policyNumber });
            return result;
        }

        public async Task<IEnumerable<PolicyDto>> GetAllPolicies()
        {
            var result = await _mediator.Send(new GetAllPoliciesQuery());
            return result;
        }
        public async Task<TerminatePolicyResult> TerminatePolicy(TerminatePolicyCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }
    }
}
