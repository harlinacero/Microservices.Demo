namespace Microservices.Demo.Pricing.API.Domain
{
    using DynamicExpresso;
    using Microservices.Demo.Pricing.API.Infrastructure.Data.Entities;
    using Microservices.Demo.Pricing.API.Infrastructure.Data.Entities.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using static System.String;
    using static System.Decimal;
    public class PricingDomainService
    {
        public Calculation CalculatePrice(Tariff tariff,Calculation calculation)
        {
            calculation = CalcBasePrices(tariff,calculation);
            tariff = ApplyDiscounts(tariff,calculation);
            calculation = UpdateTotals(calculation);
            return calculation;
        }

        private Calculation CalcBasePrices(Tariff tariff, Calculation calculation)
        {
            calculation.Covers.ToList().ForEach(keyValuePair =>
            {
                calculation.Covers[keyValuePair.Key] = SetPrice(keyValuePair.Value, CalculateBasePriceFor(tariff.BasePremiumRules, keyValuePair.Value, calculation));
            });

            return calculation;
        }

        private Tariff ApplyDiscounts(Tariff tariff, Calculation calculation)
        {
            tariff.DiscountMarkupRules = ApplyDiscountMarkupRuleCalculation(tariff.DiscountMarkupRules,calculation);
            return tariff;
        }

        private Calculation UpdateTotals(Calculation calculation)
        {
            UpdateTotal(calculation);
            return calculation;
        }

        public Calculation UpdateTotal(Calculation calculation)
        {
            calculation.TotalPremium = calculation.Covers.Values.Sum(c => c.Price);
            return calculation;
        }

        private Cover ZeroPrice(string coverCode)
        {            
            Cover cover = new Cover(coverCode, 0M);
            return cover;
        }

        public Cover SetPrice(Cover cover,decimal price)
        {
            cover.Price = price;
            return cover;
        }
        public BasePremiumRuleList AddBasePriceRule(BasePremiumRuleList basePremiumRules, string coverCode, string applyIfFormula, string basePriceFormula)
        {
            basePremiumRules.Rules.Add(new BasePremiumRule(coverCode, applyIfFormula, basePriceFormula));
            return basePremiumRules;
        }

        public decimal CalculateBasePriceFor(BasePremiumRuleList basePremiumRules, Cover cover, Calculation calculation)
        {
            return basePremiumRules.Rules
                .Where(r => AppliesBasePremiumRuleCalculationParams(r,cover, calculation))
                .Select(r => CalculateBasePrice(r,calculation))
                .FirstOrDefault();
        }

        public bool AppliesBasePremiumRuleCalculationParams(BasePremiumRule basePremiumRule, Cover cover, Calculation calculation)
        {
            if (cover.Code != basePremiumRule.CoverCode)
                return false;

            if (IsNullOrEmpty(basePremiumRule.ApplyIfFormula))
                return true;

            var (paramDefinitions, values) = DomainAdapter.CalculationToCalculationParams(calculation);
            return (bool)new Interpreter()
                .Parse(basePremiumRule.ApplyIfFormula, paramDefinitions.ToArray())
                .Invoke(values.ToArray());
        }

        public decimal CalculateBasePrice(BasePremiumRule basePremiumRule,Calculation calculation)
        {
            var (paramDefinitions, values) = DomainAdapter.CalculationToCalculationParams(calculation);

            return (decimal)new Interpreter()
                .Parse(basePremiumRule.BasePriceFormula, paramDefinitions.ToArray())
                .Invoke(values.ToArray());
        }
        public DiscountMarkupRuleList AddPercentMarkup(DiscountMarkupRuleList discountMarkupRuleList,string applyIfFormula, decimal markup)
        {
            discountMarkupRuleList.Rules.Add(new PercentMarkupRule(applyIfFormula, markup));
            return discountMarkupRuleList;
        }

        public DiscountMarkupRuleList ApplyDiscountMarkupRuleCalculation(DiscountMarkupRuleList discountMarkupRuleList, Calculation calculation)
        {
            discountMarkupRuleList.Rules
                .Where(r => AppliesDiscountMarkupRuleCalculationParams(r,calculation))
                .ForEach(r => ApplyDiscountMarkupRuleCalculation(r,calculation));

            return discountMarkupRuleList;
        }

        public bool AppliesDiscountMarkupRuleCalculationParams(DiscountMarkupRule discountMarkupRule,Calculation calculation)
        {
            if (IsNullOrEmpty(discountMarkupRule.ApplyIfFormula))
                return true;

            var (paramDefinitions, values) = DomainAdapter.CalculationToCalculationParams(calculation);
            return (bool)new Interpreter()
                .Parse(discountMarkupRule.ApplyIfFormula, paramDefinitions.ToArray())
                .Invoke(values.ToArray());
        }
        public Calculation ApplyDiscountMarkupRuleCalculation(DiscountMarkupRule discountMarkupRule, Calculation calculation)
        {
            calculation.Covers.ToList().ForEach(keyValuePair =>
            {
                var priceAfterMarkup = Round(keyValuePair.Value.Price * discountMarkupRule.ParamValue, 2);
                calculation.Covers[keyValuePair.Key] = SetPrice(keyValuePair.Value, priceAfterMarkup);
            });

            return calculation;
        }

        public Calculation CreateCalculation(
            string productCode,
            DateTimeOffset policyFrom,
            DateTimeOffset policyTo,
            IEnumerable<string> selectedCovers,
            Dictionary<string, object> subject
            )
        {
            Calculation calculation = new Calculation();

            calculation.ProductCode = productCode;
            calculation.PolicyFrom = policyFrom;
            calculation.PolicyTo = policyTo;
            calculation.TotalPremium = 0M;
            selectedCovers.ForEach(coverCode =>
            {
                calculation.Covers.Add(coverCode,ZeroPrice(coverCode));
            });
            calculation.Subject = subject;

            return calculation;
        }
    }
}
