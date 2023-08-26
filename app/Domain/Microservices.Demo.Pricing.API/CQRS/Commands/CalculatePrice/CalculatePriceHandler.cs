using FluentValidation;
using MediatR;
using Microservices.Demo.Pricing.API.Domain;
using Microservices.Demo.Pricing.API.Infrastructure.Data.Entities;
using Microservices.Demo.Pricing.API.Infrastructure.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Microservices.Demo.Pricing.API.CQRS.Commands.CalculatePrice
{
    public class CalculatePriceHandler: IRequestHandler<CalculatePriceCommand, CalculatePriceResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PricingDomainService _pricingDomainService;
        private readonly CalculatePriceCommandValidator commandValidator = new CalculatePriceCommandValidator();

        public CalculatePriceHandler(IUnitOfWork unitOfWork,PricingDomainService pricingDomainService)
        {
            _unitOfWork = unitOfWork;
            _pricingDomainService = pricingDomainService;
        }

        public async Task<CalculatePriceResult> Handle(CalculatePriceCommand command, CancellationToken cancellationToken)
        {
            commandValidator.ValidateAndThrow(command);

            Tariff tariff = await _unitOfWork.TariffsRepository[command.ProductCode];

            var calculation = _pricingDomainService.CalculatePrice(tariff, ToCalculation(command));

            return ToResult(calculation);
        }

        private Calculation ToCalculation(CalculatePriceCommand cmd)
        {
            return _pricingDomainService.CreateCalculation(
                cmd.ProductCode,
                cmd.PolicyFrom,
                cmd.PolicyTo,
                cmd.SelectedCovers,
                cmd.Answers.ToDictionary(a => a.QuestionCode, a => a.GetAnswer()));
        }

        private static CalculatePriceResult ToResult(Calculation calculation)
        {
            return new CalculatePriceResult
            {
                TotalPrice = calculation.TotalPremium,
                CoverPrices = calculation.Covers.ToDictionary(c => c.Key, c => c.Value.Price)
            };
        }
    }
}
