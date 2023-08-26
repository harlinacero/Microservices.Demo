using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Pricing.API.Infrastructure.Data.Entities
{
    public class DiscountMarkupRuleList
    {
        public readonly List<DiscountMarkupRule> Rules;

        public DiscountMarkupRuleList(List<DiscountMarkupRule> rules)
        {
            this.Rules = rules;
        }
    }
}
