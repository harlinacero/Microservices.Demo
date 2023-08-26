using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Pricing.API.Infrastructure.Data.Entities
{
    public class DiscountMarkupRule
    {
        public string ApplyIfFormula { get; protected set; }
        public decimal ParamValue { get; protected set; }
    }
}
