using MediatR;
using Microservices.Demo.Product.API.CQRS.Queries.Infrastructure.Adapters;
using Microservices.Demo.Product.API.CQRS.Queries.Infrastructure.Dtos.Product;
using Microservices.Demo.Product.API.Infrastructure.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Microservices.Demo.Product.API.CQRS.Queries.FindProductByCode
{
    public class FindProductByCodeHandler: IRequestHandler<FindProductByCodeQuery, ProductDto>
    {
        private readonly IProductRepository _productRepository;

        public FindProductByCodeHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public async Task<ProductDto> Handle(FindProductByCodeQuery request, CancellationToken cancellationToken)
        {
            var result = await _productRepository.FindOne(request.ProductCode);

            return result != null ? new ProductDto
            {
                Code = result.Code,
                Name = result.Name,
                Description = result.Description,
                Image = result.Image,
                MaxNumberOfInsured = result.MaxNumberOfInsured,
                Questions = result.Questions != null ? ProductAdapter.FromQuestionsToQuestionDtos(result.Questions.ToList()) : null,
                Covers = result.Covers != null ? ProductAdapter.FromCoversToCoverDtos(result.Covers.ToList()) : null
            } : null;
        }
    }
}
