using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Product.API.CQRS.Commands.Infrastructure.Dtos.Product
{
    public class ProductDraftDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public IList<CoverDto> Covers { get; set; }
        public IList<QuestionDto> Questions { get; set; }
        public int MaxNumberOfInsured { get; set; }
    }
}
