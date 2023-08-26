using MediatR;
using Microservices.Demo.Policy.API.CQRS.Queries.Infrastructure.Dtos.Policy;
using Microservices.Demo.Policy.API.Domain;
using Microservices.Demo.Policy.API.Infrastructure.Data.Repository;
using Microservices.Demo.Policy.API.Infrastructure.Data.UnitOfWork;

namespace Microservices.Demo.Policy.API.CQRS.Queries.Policy.GetAllPolicies
{
    public class GetAllPoliciesQueryHandler : IRequestHandler<GetAllPoliciesQuery, IEnumerable<PolicyDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllPoliciesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<IEnumerable<PolicyDto>> Handle(GetAllPoliciesQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<API.Infrastructure.Data.Entities.Policy> policies = await _unitOfWork.Policies.GetAll();
            List<PolicyDto> getAllPoliciesQueryResult = new();

            foreach (var policy in policies)
            {
                getAllPoliciesQueryResult.Add(new PolicyDto()
                {
                    AgentLogin = policy.AgentLogin,
                    CreationDate = policy.CreationDate,
                    Number = policy.Number,
                    PolicyId = policy.PolicyId,
                    PolicyStatusId = policy.PolicyStatusId,
                    ProductCode = policy.ProductCode                    
                });
            }

            return getAllPoliciesQueryResult;
        }
    }
}
