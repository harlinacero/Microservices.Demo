using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Pricing.API.Infrastructure.Data.Entities
{
    public class PercentMarkupRule: DiscountMarkupRule 
    {
        public PercentMarkupRule(string applyIfFormula, decimal paramValue)
        {
            ApplyIfFormula = applyIfFormula;
            ParamValue = paramValue;
        }
    }
}
