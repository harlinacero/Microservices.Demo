using MediatR;
using Microservices.Demo.Product.API.CQRS.Commands.ActivateProduct;
using Microservices.Demo.Product.API.CQRS.Commands.CreateProduct;
using Microservices.Demo.Product.API.CQRS.Commands.DiscontinueProduct;
using Microservices.Demo.Product.API.CQRS.Queries.FindAllProducts;
using Microservices.Demo.Product.API.CQRS.Queries.FindProductByCode;
using Microservices.Demo.Product.API.CQRS.Queries.Infrastructure.Dtos.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Product.API.Application
{
    public class ProductApplicationService
    {
        private readonly IMediator _mediator;
        public ProductApplicationService(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var products = await _mediator.Send(new FindAllProductsQuery());
            return products;
        }

        public async Task<ProductDto> GetByCodeAsync(string code)
        {
            var product = await _mediator.Send(new FindProductByCodeQuery { ProductCode = code });
            return product;
        }

        public async Task<CreateProductDraftResult> CreateProductDraftAsync(CreateProductDraftCommand command)
        {
            var productDraft = await _mediator.Send(command);
            return productDraft;
        }
        public async Task<ActivateProductResult> ActivateProductAsync(ActivateProductCommand command)
        {
            var productActivated = await _mediator.Send(command);
            return productActivated;
        }
        public async Task<DiscontinueProductResult> DiscontinueProductAsync(DiscontinueProductCommand command)
        {
            var productDiscontinued = await _mediator.Send(command);
            return productDiscontinued;
        }
    }
}
