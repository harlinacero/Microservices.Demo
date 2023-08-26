using MediatR;
using Microservices.Demo.Product.API.CQRS.Queries.Infrastructure.Adapters;
using Microservices.Demo.Product.API.CQRS.Queries.Infrastructure.Dtos.Product;
using Microservices.Demo.Product.API.Infrastructure.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Microservices.Demo.Product.API.CQRS.Queries.FindAllProducts
{
    public class FindAllProductsHandler : IRequestHandler<FindAllProductsQuery, IEnumerable<ProductDto>>
    {
        private readonly IProductRepository _productRepository;

        public FindAllProductsHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public async Task<IEnumerable<ProductDto>> Handle(FindAllProductsQuery request, CancellationToken cancellationToken)
        {
            var result = await _productRepository.FindAllActive();

            return result.Select(p => new ProductDto
            {
                Code = p.Code,
                Name = p.Name,
                Description = p.Description,
                Image = p.Image,
                MaxNumberOfInsured = p.MaxNumberOfInsured,
                Questions = p.Questions != null ? ProductAdapter.FromQuestionsToQuestionDtos(p.Questions.ToList()) : null,
                Covers = p.Covers.Any() ? ProductAdapter.FromCoversToCoverDtos(p.Covers.ToList()) : null
            }).ToList();
        }
    }
}
