using MediatR;
using Microservices.Demo.Policy.API.CQRS.Commands.Infrastructure.Dtos.Policy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Policy.API.CQRS.Commands.Policy.CreatePolicy
{
    public class CreatePolicyCommand: IRequest<CreatePolicyResult>
    {
        public string OfferNumber { get; set; }
        public PersonDto PolicyHolder { get; set; }
        public AddressDto PolicyHolderAddress { get; set; }
    }
}
