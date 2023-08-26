using MediatR;
using Microservices.Demo.Product.API.CQRS.Commands.Infrastructure.Dtos.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Product.API.CQRS.Commands.CreateProduct
{
    public class CreateProductDraftCommand : IRequest<CreateProductDraftResult>
    {
        public ProductDraftDto ProductDraft { get; set; }
    }
}
