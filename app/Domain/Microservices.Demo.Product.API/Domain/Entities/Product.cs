using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Product.API.Infrastructure.Data.Entities
{
    public partial class Product
    {
        public Product(string code, string name, string image, string description, int maxNumberOfInsured)
        {            
            Code = code;
            Name = name;
            ProductStatusId = (int)EnumProductStatus.Draft;
            Image = image;
            Description = description;
            Covers = new HashSet<Cover>();
            Questions = new HashSet<Question>();
            MaxNumberOfInsured = maxNumberOfInsured;
        }   
    }

    public enum EnumProductStatus
    {
        Draft=1,
        Active =2,
        Discontinued =3
    }
}
