using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Policy.API.Infrastructure.Data.Entities
{
    public partial class OfferCover
    {
        public OfferCover(string code, decimal price)
        {
            Code = code;
            Price = price;
        }
    }
}
