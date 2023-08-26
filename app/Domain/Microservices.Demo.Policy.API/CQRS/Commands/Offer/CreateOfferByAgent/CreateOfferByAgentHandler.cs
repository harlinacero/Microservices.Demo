namespace Microservices.Demo.Policy.API.CQRS.Commands.Offer.CreateOfferByAgent
{
    using MediatR;
    using Microservices.Demo.Policy.API.CQRS.Commands.Offer.CreateOffer;
    using Microservices.Demo.Policy.API.Domain;
    using Microservices.Demo.Policy.API.Infrastructure.Agents.Pricing;
    using Microservices.Demo.Policy.API.Infrastructure.Data.Entities;
    using Microservices.Demo.Policy.API.Infrastructure.Data.UnitOfWork;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    public class CreateOfferByAgentHandler : IRequestHandler<CreateOfferByAgentCommand, CreateOfferResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPricingAgent _pricingAgent;
        private readonly OfferDomainService _offerDomainService;

        public CreateOfferByAgentHandler(IUnitOfWork unitOfWork, IPricingAgent pricingAgent,OfferDomainService offerDomainService)
        {
            _unitOfWork = unitOfWork;
            _pricingAgent = pricingAgent;
            _offerDomainService = offerDomainService;
        }

        public async Task<CreateOfferResult> Handle(CreateOfferByAgentCommand request, CancellationToken cancellationToken)
        {
            //calculate price
            var priceParams = ConstructPriceParams(request);
            var price = await _pricingAgent.CalculatePrice(priceParams);


            var offer = _offerDomainService.CreateOfferForPriceAndAgent(
                priceParams.ProductCode,
                priceParams.PolicyFrom,
                priceParams.PolicyTo,
                null,
                price,
                request.AgentLogin
            );

            //create and save offer
            await _unitOfWork.Offers.Add(offer);
            await _unitOfWork.CommitChanges();

            //return result
            return ConstructResult(offer);
        }

        private CreateOfferResult ConstructResult(Offer offer)
        {
            return new CreateOfferResult
            {
                OfferNumber = offer.Number,
                TotalPrice = offer.TotalPrice,
                CoversPrices = offer.OfferCovers.ToDictionary(c => c.Code, c => c.Price)
            };
        }

        private PricingParams ConstructPriceParams(CreateOfferCommand request)
        {
            return new PricingParams
            {
                ProductCode = request.ProductCode,
                PolicyFrom = request.PolicyFrom,
                PolicyTo = request.PolicyTo,
                SelectedCovers = request.SelectedCovers,
                Answers = request.Answers.Select(a => Answer.Create(a.QuestionType, a.QuestionCode, a.GetAnswer())).ToList()
            };
        }

    }
}
