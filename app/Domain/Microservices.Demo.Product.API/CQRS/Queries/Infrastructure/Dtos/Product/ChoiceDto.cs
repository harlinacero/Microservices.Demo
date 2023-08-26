using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Product.API.CQRS.Queries.Infrastructure.Dtos.Product
{
    public class ChoiceDto
    {
        public string Code { get; set; }
        public string Label { get; set; }
    }
}
