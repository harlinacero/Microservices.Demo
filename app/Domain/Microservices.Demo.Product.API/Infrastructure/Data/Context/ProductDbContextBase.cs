using Microservices.Demo.Product.API.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Product.API.Infrastructure.Data.Context
{
    public partial class ProductDbContext : DbContext
    {
        protected  void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Question>()
            .HasDiscriminator<int>("QuestionTypeId")
            .HasValue<Question>(1)
            .HasValue<NumericQuestion>(2)
            .HasValue<DateQuestion>(3)
            .HasValue<ChoiceQuestion>(4);

        }

    }
}
