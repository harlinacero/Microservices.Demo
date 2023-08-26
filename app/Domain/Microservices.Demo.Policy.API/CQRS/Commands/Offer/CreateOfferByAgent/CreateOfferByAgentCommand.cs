using MediatR;
using Microservices.Demo.Policy.API.CQRS.Commands.Offer.CreateOffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Policy.API.CQRS.Commands.Offer.CreateOfferByAgent
{
    public class CreateOfferByAgentCommand : CreateOfferCommand, IRequest<CreateOfferResult>
    {
        public string AgentLogin { get; set; }

        public CreateOfferByAgentCommand(string agentLogin, CreateOfferCommand command)
        {
            AgentLogin = agentLogin;
            ProductCode = command.ProductCode;
            PolicyFrom = command.PolicyFrom;
            PolicyTo = command.PolicyTo;
            SelectedCovers = command.SelectedCovers;
            Answers = command.Answers;
        }
    }
}
