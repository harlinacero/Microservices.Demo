using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Microservices.Demo.Product.API.Domain
{
    using Microservices.Demo.Product.API.Infrastructure.Data.Entities;
    public class ProductDomainService
    {
        public Product CreateDraft(string code, string name, string image, string description, int maxNumberOfInsured)
        {
            return new Product(code, name, image, description, maxNumberOfInsured);
        }

        public Product Activate(Product product)
        {
            EnsureIsDraft(product);
            product.ProductStatusId = (int)EnumProductStatus.Active;
            return product;
        }

        public Product Discontinue(Product product)
        {
            product.ProductStatusId = (int)EnumProductStatus.Discontinued;
            return product;
        }
        public Product AddCover(Product product,string code, string name, string description, bool optional, decimal? sumInsured)
        {
            EnsureIsDraft(product);
            product.Covers.Add(new Cover(code, name, description, optional, sumInsured));
            return product;
        }

        public Product AddQuestions(Product product, IEnumerable<Question> questions)
        {
            EnsureIsDraft(product);
            foreach (var q in questions)
                product.Questions.Add(q);

            return product;
        }
        private void EnsureIsDraft(Product product)
        {
            if (product.ProductStatusId != (int)EnumProductStatus.Draft)
            {
                throw new ApplicationException("Only draft version can be modified and activated");
            }
        }
    }
}
