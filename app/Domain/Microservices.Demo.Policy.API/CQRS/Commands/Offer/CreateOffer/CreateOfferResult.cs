using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Policy.API.CQRS.Commands.Offer.CreateOffer
{
    public class CreateOfferResult
    {
        public string OfferNumber { get; set; }
        public decimal TotalPrice { get; set; }
        public Dictionary<string, decimal> CoversPrices { get; set; }

        public static CreateOfferResult Empty() => new CreateOfferResult { CoversPrices = new Dictionary<string, decimal>() };
    }
}
