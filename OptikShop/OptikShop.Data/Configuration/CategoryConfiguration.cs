using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OptikShop.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptikShop.Data.Configuration
{
    public class CategoryConfiguration : BaseConfiguration<CategoryEntity>
    {
        public void Configure(EntityTypeBuilder<CategoryEntity> builder)
        {
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x=>x.Name).HasMaxLength(50);
            builder.Property(x=> x.Description).IsRequired().HasMaxLength(1000);

            base.Configure(builder);
        }
    }
}
