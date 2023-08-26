namespace Microservices.Demo.Policy.API.CQRS.Queries.Policy.GetPolicyDetails
{
    using MediatR;
    using Microservices.Demo.Policy.API.Infrastructure.Data.UnitOfWork;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microservices.Demo.Policy.API.Infrastructure.Data.Entities;
    using Microservices.Demo.Policy.API.Domain;
    using Microservices.Demo.Policy.API.CQRS.Queries.Infrastructure.Dtos.Policy;

    public class GetPolicyDetailsHandler : IRequestHandler<GetPolicyDetailsQuery, GetPolicyDetailsQueryResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PolicyDomainService _policyDomainService;

        public GetPolicyDetailsHandler(IUnitOfWork unitOfWork,PolicyDomainService policyDomainService)
        {
            _unitOfWork = unitOfWork;
            _policyDomainService = policyDomainService;
        }

        public async Task<GetPolicyDetailsQueryResult> Handle(GetPolicyDetailsQuery request, CancellationToken cancellationToken)
        {
            var policy = await _unitOfWork.Policies.WithNumber(request.PolicyNumber);
            if (policy == null)
            {
                throw new ApplicationException($"Policy {request.PolicyNumber} not found!");
            }

            return ConstructResult(policy);
        }

        private GetPolicyDetailsQueryResult ConstructResult(Policy policy)
        {
            var effectiveVersion = _policyDomainService.FirstVersion(policy.PolicyVersions);

            return new GetPolicyDetailsQueryResult
            {
                Policy = new PolicyDetailsDto
                {
                    Number = policy.Number,
                    ProductCode = policy.ProductCode,
                    DateFrom = effectiveVersion.CoverPeriodPolicyValidityPeriod.PolicyFrom,
                    DateTo = effectiveVersion.CoverPeriodPolicyValidityPeriod.PolicyTo,
                    PolicyHolder = $"{effectiveVersion.PolicyHolder.FirstName} {effectiveVersion.PolicyHolder.LastName}",
                    TotalPremium = effectiveVersion.TotalPremiumAmount,

                    AccountNumber = null,
                    Covers = effectiveVersion.PolicyCovers.Select(c => c.Code).ToList()
                }
            };
        }
    }
}
