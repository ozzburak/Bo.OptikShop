using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OptikShop.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptikShop.Data.Configuration
{
    public class ProductConfiguration : BaseConfiguration<ProductEntity>
    {
        public void Configure(EntityTypeBuilder<ProductEntity> builder)
        {

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);

            // description null olabilir
            builder.Property(x => x.Description)
                .IsRequired(false);

            // unitprice null olabilir
            builder.Property(x => x.UnitPrice)
                .IsRequired(false);

            // ImagePath null olabilir.
            builder.Property(x => x.ImagePath)
                .IsRequired(false);

            // categoryId zorunlu
            builder.Property(x => x.CategoryId)
                .IsRequired();

            base.Configure(builder);
        }
    }
}
