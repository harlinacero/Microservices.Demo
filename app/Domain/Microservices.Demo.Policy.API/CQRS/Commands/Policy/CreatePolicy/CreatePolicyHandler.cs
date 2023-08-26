namespace Microservices.Demo.Policy.API.CQRS.Commands.Policy.CreatePolicy
{
    using MediatR;
    using Microservices.Demo.Policy.API.CQRS.Commands.Infrastructure.Dtos.Policy;
    using Microservices.Demo.Policy.API.Domain;
    using Microservices.Demo.Policy.API.Infrastructure.Data.Entities;
    using Microservices.Demo.Policy.API.Infrastructure.Data.UnitOfWork;
    using Microservices.Demo.Policy.API.Infrastructure.Messaging;
    using Microservices.Demo.Policy.API.Infrastructure.Messaging.Events;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    public class CreatePolicyHandler: IRequestHandler<CreatePolicyCommand, CreatePolicyResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventPublisher _eventPublisher;
        private readonly PolicyDomainService _policyDomainService;
        private readonly OfferDomainService _offerDomainService;

        public CreatePolicyHandler(
            IUnitOfWork unitOfWork,
            IEventPublisher eventPublisher,
            OfferDomainService offerDomainService,
            PolicyDomainService policyDomainService)
        {
            this._unitOfWork = unitOfWork;
            this._eventPublisher = eventPublisher;
            _offerDomainService = offerDomainService;
            _policyDomainService = policyDomainService; 
        }

        public async Task<CreatePolicyResult> Handle(CreatePolicyCommand request, CancellationToken cancellationToken)
        {

            var offer = await _unitOfWork.Offers.WithNumber(request.OfferNumber);
            var customer = new PolicyHolder
            (
                request.PolicyHolder.FirstName,
                request.PolicyHolder.LastName,
                request.PolicyHolder.TaxId,
                _policyDomainService.CreateAddress(
                    request.PolicyHolderAddress.Country,
                    request.PolicyHolderAddress.ZipCode,
                    request.PolicyHolderAddress.City,
                    request.PolicyHolderAddress.Street
                )
            );
            offer = _offerDomainService.Buy(offer, customer);
            var policy = _policyDomainService.Buy(offer,customer);

            _unitOfWork.Policies.Add(policy);

            await _eventPublisher.PublishMessage(PolicyCreated(policy));

            await _unitOfWork.CommitChanges();

            return new CreatePolicyResult
            {
                PolicyNumber = policy.Number
            };

        }

        private static PolicyCreated PolicyCreated(Policy policy)
        {
            var version = policy.PolicyVersions.First(v => v.VersionNumber == 1);

            return new PolicyCreated
            {
                PolicyNumber = policy.Number,
                PolicyFrom = version.CoverPeriodPolicyValidityPeriod.PolicyFrom,
                PolicyTo = version.CoverPeriodPolicyValidityPeriod.PolicyTo,
                ProductCode = policy.ProductCode,
                TotalPremium = version.TotalPremiumAmount,
                PolicyHolder = new PersonDto
                {
                    FirstName = version.PolicyHolder.FirstName,
                    LastName = version.PolicyHolder.LastName,
                    TaxId = version.PolicyHolder.Pesel
                },
                AgentLogin = policy.AgentLogin
            };
        }
    }
}
