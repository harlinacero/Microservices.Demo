using MediatR;
using Microservices.Demo.Pricing.API.CQRS.Commands.Infrastructure.Dtos.Pricing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Pricing.API.CQRS.Commands.CalculatePrice
{
    public class CalculatePriceCommand : IRequest<CalculatePriceResult>
    {
        public string ProductCode { get; set; }
        public DateTimeOffset PolicyFrom { get; set; }
        public DateTimeOffset PolicyTo { get; set; }
        public List<string> SelectedCovers { get; set; }
        public List<QuestionAnswerDto> Answers { get; set; }
    }
}
