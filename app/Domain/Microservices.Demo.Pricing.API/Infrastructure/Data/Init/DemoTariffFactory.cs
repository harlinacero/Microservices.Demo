using Microservices.Demo.Pricing.API.Domain;
using Microservices.Demo.Pricing.API.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Pricing.API.Infrastructure.Data.Init
{    
    internal static class DemoTariffFactory
    {
        internal static PricingDomainService _pricingDomainService;
        internal static Tariff Travel()
        {
            _pricingDomainService = new PricingDomainService();

            var travel = new Tariff("TRI");

            travel.BasePremiumRules = _pricingDomainService.AddBasePriceRule(travel.BasePremiumRules,"C1", null, "(NUM_OF_ADULTS) * (DESTINATION == \"EUR\" ? 26.00M : 34.00M)");
            travel.BasePremiumRules = _pricingDomainService.AddBasePriceRule(travel.BasePremiumRules, "C2", null, "(NUM_OF_ADULTS + NUM_OF_CHILDREN) * 26.00M");
            travel.BasePremiumRules = _pricingDomainService.AddBasePriceRule(travel.BasePremiumRules, "C3", null, "(NUM_OF_ADULTS + NUM_OF_CHILDREN) * 10.00M");

            travel.DiscountMarkupRules = _pricingDomainService.AddPercentMarkup(travel.DiscountMarkupRules, "DESTINATION == \"WORLD\"", 1.5M);

            return travel;
        }

        internal static Tariff House()
        {
            Tariff house = new Tariff("HSI");

            house.BasePremiumRules = _pricingDomainService.AddBasePriceRule(house.BasePremiumRules, "C1", "TYP == \"APT\"", "AREA * 1.25M");
            house.BasePremiumRules = _pricingDomainService.AddBasePriceRule(house.BasePremiumRules, "C1", "TYP == \"HOUSE\"", "AREA * 1.50M");

            house.BasePremiumRules = _pricingDomainService.AddBasePriceRule(house.BasePremiumRules, "C2", "TYP == \"APT\"", "AREA * 0.25M");
            house.BasePremiumRules = _pricingDomainService.AddBasePriceRule(house.BasePremiumRules, "C2", "TYP == \"HOUSE\"", "AREA * 0.45M");

            house.BasePremiumRules = _pricingDomainService.AddBasePriceRule(house.BasePremiumRules, "C3", null, "30M");
            house.BasePremiumRules = _pricingDomainService.AddBasePriceRule(house.BasePremiumRules, "C4", null, "50M");

            house.DiscountMarkupRules = _pricingDomainService.AddPercentMarkup(house.DiscountMarkupRules, "FLOOD == \"YES\"", 1.50M);
            house.DiscountMarkupRules = _pricingDomainService.AddPercentMarkup(house.DiscountMarkupRules, "NUM_OF_CLAIM > 1 ", 1.25M);

            return house;
        }

        internal static Tariff Farm()
        {
            Tariff farm = new Tariff("FAI");

            farm.BasePremiumRules = _pricingDomainService.AddBasePriceRule(farm.BasePremiumRules,"C1", null, "10M");
            farm.BasePremiumRules = _pricingDomainService.AddBasePriceRule(farm.BasePremiumRules,"C2", null, "20M");
            farm.BasePremiumRules = _pricingDomainService.AddBasePriceRule(farm.BasePremiumRules,"C3", null, "30M");
            farm.BasePremiumRules = _pricingDomainService.AddBasePriceRule(farm.BasePremiumRules,"C4", null, "40M");

            //farm.DiscountMarkupRules.AddPercentMarkup("FLOOD == \"YES\"", 1.50M);
            farm.DiscountMarkupRules = _pricingDomainService.AddPercentMarkup(farm.DiscountMarkupRules,"NUM_OF_CLAIM > 2", 2.00M);

            return farm;
        }

        internal static Tariff Car()
        {
            Tariff car = new Tariff("CAR");

            car.BasePremiumRules = _pricingDomainService.AddBasePriceRule(car.BasePremiumRules,"C1", null, "100M");
            car.DiscountMarkupRules = _pricingDomainService.AddPercentMarkup(car.DiscountMarkupRules, "NUM_OF_CLAIM > 2", 1.50M);

            return car;
        }
    }
}
