using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Pricing.API.CQRS.Commands.CalculatePrice
{
    public class CalculatePriceCommandValidator : AbstractValidator<CalculatePriceCommand>
    {
        public CalculatePriceCommandValidator()
        {
            RuleFor(m => m.ProductCode).NotEmpty();
            RuleFor(m => m.SelectedCovers).NotNull();
            RuleFor(m => m.Answers).NotNull();
        }
    }
}
