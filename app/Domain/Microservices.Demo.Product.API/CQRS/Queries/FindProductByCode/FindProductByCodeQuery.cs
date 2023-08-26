using MediatR;
using Microservices.Demo.Product.API.CQRS.Queries.Infrastructure.Dtos.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Product.API.CQRS.Queries.FindProductByCode
{
    public class FindProductByCodeQuery : IRequest<ProductDto>
    {
        public string ProductCode { get; set; }
    }
}
