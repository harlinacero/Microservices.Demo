using MediatR;
using Microservices.Demo.Policy.API.CQRS.Commands.Infrastructure.Dtos.Policy;
using Microservices.Demo.Policy.API.Domain;
using Microservices.Demo.Policy.API.Domain.Entities;
using Microservices.Demo.Policy.API.Infrastructure.Data.UnitOfWork;
using Microservices.Demo.Policy.API.Infrastructure.Messaging;
using Microservices.Demo.Policy.API.Infrastructure.Messaging.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Microservices.Demo.Policy.API.CQRS.Commands.Policy.TerminatePolicy
{
    public class TerminatePolicyHandler : IRequestHandler<TerminatePolicyCommand, TerminatePolicyResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventPublisher _eventPublisher;
        private readonly OfferDomainService _offerDomainService;
        private readonly PolicyDomainService _policyDomainService;

        public TerminatePolicyHandler(
            IUnitOfWork uow, 
            IEventPublisher eventPublisher,
            OfferDomainService offerDomainService,
            PolicyDomainService policyDomainService)
        {
            this._unitOfWork = uow;
            this._eventPublisher = eventPublisher;
            _offerDomainService = offerDomainService;
            _policyDomainService = policyDomainService;
        }

        public async Task<TerminatePolicyResult> Handle(TerminatePolicyCommand request, CancellationToken cancellationToken)
        {
            var policy = await _unitOfWork.Policies.WithNumber(request.PolicyNumber);

            var terminationResult = _policyDomainService.Terminate(policy,request.TerminationDate);

            await _eventPublisher.PublishMessage(PolicyTerminated(terminationResult));

            await _unitOfWork.CommitChanges();

            return new TerminatePolicyResult
            {
                PolicyNumber = policy.Number,
                MoneyToReturn = terminationResult.AmountToReturn
            };
        }

        private PolicyTerminated PolicyTerminated(PolicyTerminationResult terminationResult)
        {
            return new PolicyTerminated
            {
                PolicyNumber = terminationResult.TerminalVersion.Policy.Number,
                PolicyFrom = terminationResult.TerminalVersion.CoverPeriodPolicyValidityPeriod.PolicyFrom,
                PolicyTo = terminationResult.TerminalVersion.CoverPeriodPolicyValidityPeriod.PolicyTo,
                ProductCode = terminationResult.TerminalVersion.Policy.ProductCode,
                TotalPremium = terminationResult.TerminalVersion.TotalPremiumAmount,
                AmountToReturn = terminationResult.AmountToReturn,
                PolicyHolder = new PersonDto
                {
                    FirstName = terminationResult.TerminalVersion.PolicyHolder.FirstName,
                    LastName = terminationResult.TerminalVersion.PolicyHolder.LastName,
                    TaxId = terminationResult.TerminalVersion.PolicyHolder.Pesel
                }
            };
        }
    }
}
