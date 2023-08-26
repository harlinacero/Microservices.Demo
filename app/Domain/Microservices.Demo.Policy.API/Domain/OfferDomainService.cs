namespace Microservices.Demo.Policy.API.Domain
{
    using Microservices.Demo.Policy.API.Domain.Entities;
    using Microservices.Demo.Policy.API.Infrastructure.Data.Entities;
    using System;
    using System.Linq;

    public class OfferDomainService
    {
        public Offer CreateOfferForPrice(String productCode,
            DateTime policyFrom,
            DateTime policyTo,
            PolicyHolder policyHolder,
            Price price)
        {
            return CreateOffer
            (
                productCode,
                policyFrom,
                policyTo,   
                policyHolder,
                price,
                null
            );
        }

        public Offer CreateOffer(
           string productCode,
           DateTime policyFrom,
           DateTime policyTo,
           PolicyHolder policyHolder,
           Price price,
           string agentLogin)
        {
            return new Offer
            {
                Number = Guid.NewGuid().ToString(),
                ProductCode = productCode,
                PolicyValidityPeriod = PolicyValidityPeriodBetween(policyFrom, policyTo),
                PolicyHolder = policyHolder,
                OfferCovers = price.CoverPrices.Select(c => new OfferCover(c.Key, c.Value)).ToList(),
                OfferStatusId = (int)Enum.OfferStatus.New,
                CreationDate = SysTime.CurrentTime,
                TotalPrice = price.CoverPrices.Sum(c => c.Value),
                AgentLogin = agentLogin
            };

        }

        public Offer CreateOfferForPriceAndAgent(
           string productCode,
           DateTime policyFrom,
           DateTime policyTo,
           PolicyHolder policyHolder,
           Price price,
           string agent)
        {
            return CreateOffer
            (
                productCode,
                policyFrom,
                policyTo,
                policyHolder,
                price,
                agent
            );
        }
        public Offer Buy(Offer offer, PolicyHolder customer)
        {
            if (IsExpired(offer, SysTime.CurrentTime))
                throw new ApplicationException($"Offer {offer.Number} has expired");

            if (offer.OfferStatusId != (int)Enum.OfferStatus.New)
                throw new ApplicationException($"Offer {offer.Number} is not in new status and cannot be bought");

            offer.OfferStatusId = (int)Enum.OfferStatus.Converted;

            return offer;
        }
        
        public virtual bool IsExpired(Offer offer, DateTime theDate)
        {
            return offer.CreationDate.AddDays(30) < theDate;
        }
        public PolicyValidityPeriod PolicyValidityPeriodBetween(DateTime validFrom, DateTime validTo)
        {
            return new PolicyValidityPeriod(validFrom, validTo);
        }
    }
}
