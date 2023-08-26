using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Pricing.API.Infrastructure.Data.Entities
{
    public class BasePremiumRuleList
    {
        public readonly List<BasePremiumRule> Rules;

        public BasePremiumRuleList(List<BasePremiumRule> rules)
        {
            this.Rules = rules;
        }
    }
}
