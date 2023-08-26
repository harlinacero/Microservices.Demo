using MediatR;
using Microservices.Demo.Product.API.CQRS.Queries.Infrastructure.Dtos.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Product.API.CQRS.Queries.FindAllProducts
{
    public class FindAllProductsQuery : IRequest<IEnumerable<ProductDto>>
    {

    }
}
