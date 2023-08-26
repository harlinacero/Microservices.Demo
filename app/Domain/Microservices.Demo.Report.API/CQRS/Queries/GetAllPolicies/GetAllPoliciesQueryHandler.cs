using MediatR;
using Microservices.Demo.Report.API.CQRS.Queries.Infraestucture.Dtos;
using Microservices.Demo.Report.API.Infraestucture.Agents.Policies;
using Microservices.Demo.Report.API.Infraestucture.Agents.Products;

namespace Microservices.Demo.Report.API.CQRS.Queries.GetAllPolicies
{
    public class GetAllPoliciesQueryHandler : IRequestHandler<GetAllPoliciesQuery, IEnumerable<PolicyDto>>
    {
        private readonly IProductClient _productClient;
        private readonly IPolicyClient _policyClient;

        public GetAllPoliciesQueryHandler(IProductClient productClient, IPolicyClient policyClient)
        {
            _productClient = productClient;
            _policyClient = policyClient;
        }

        public async Task<IEnumerable<PolicyDto>> Handle(GetAllPoliciesQuery request, CancellationToken cancellationToken)
        {
            var products = await _productClient.GetAsync();
            var policies = await _policyClient.GetAsync();
            
            List<PolicyDto> policyDtos = new List<PolicyDto>();
            foreach (var policy in policies)
            {
                var product = products.FirstOrDefault(p => p.Code == policy.ProductCode);
                policyDtos.Add(new PolicyDto(
                    policy.PolicyId,
                    policy.Number,
                    policy.ProductCode,
                    policy.AgentLogin,
                    policy.PolicyStatusId,
                    policy.CreationDate,
                    product.Name,
                    product.Image,
                    product.Description
                    ));
            }

            return policyDtos;
        }
    }
}
