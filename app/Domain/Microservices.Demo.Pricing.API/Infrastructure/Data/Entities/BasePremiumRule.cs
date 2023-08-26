using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Pricing.API.Infrastructure.Data.Entities
{
    public class BasePremiumRule
    {
        public string CoverCode { get; private set; }
        public string ApplyIfFormula { get; private set; }
        public string BasePriceFormula { get; private set; }

        public BasePremiumRule(string coverCode, string applyIfFormula, string basePriceFormula)
        {
            CoverCode = coverCode;
            ApplyIfFormula = applyIfFormula;
            BasePriceFormula = basePriceFormula;
        }
    }
}
