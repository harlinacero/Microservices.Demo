using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Pricing.API.Infrastructure.Data.Entities
{
    public partial class Tariff
    {
        public Guid Id { get; private set; }
        public string Code { get; private set; }
        [JsonProperty]
        public List<BasePremiumRule> BasePremiumRuleList;
        [JsonProperty]
        public List<DiscountMarkupRule> DiscountMarkupRuleList;
        [JsonIgnore]
        public BasePremiumRuleList BasePremiumRules;
        [JsonIgnore]
        public DiscountMarkupRuleList DiscountMarkupRules;

        public Tariff()
        {
            Id = Guid.NewGuid();
            BasePremiumRuleList = new List<BasePremiumRule>();
            DiscountMarkupRuleList = new List<DiscountMarkupRule>();
            BasePremiumRules = new BasePremiumRuleList(BasePremiumRuleList);
            DiscountMarkupRules = new DiscountMarkupRuleList(DiscountMarkupRuleList);
        }

        public Tariff(string code)
        {
            Id = Guid.NewGuid();
            Code = code;
            BasePremiumRuleList = new List<BasePremiumRule>();
            DiscountMarkupRuleList = new List<DiscountMarkupRule>();
            BasePremiumRules = new BasePremiumRuleList(BasePremiumRuleList);
            DiscountMarkupRules = new DiscountMarkupRuleList(DiscountMarkupRuleList);
        }
    }
}
