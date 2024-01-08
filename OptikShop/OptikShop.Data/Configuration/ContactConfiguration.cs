using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OptikShop.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptikShop.Data.Configuration
{
    public class ContactConfiguration : BaseConfiguration<ContactEntity>
    {
        public void Configure(EntityTypeBuilder<ContactEntity> builder)
        {

            builder.Property(x => x.FirstName).IsRequired();
            builder.Property(x => x.FirstName).HasMaxLength(50);
            builder.Property(x=>x.LastName).IsRequired();
            builder.Property(x=>x.LastName).HasMaxLength(50);
            builder.Property(x=> x.Email).IsRequired();
            builder.Property(x=>x.Email).HasMaxLength(60);
            builder.Property(x=>x.Message).IsRequired();
            builder.Property(x=>x.Message).HasMaxLength(2000);
            
            


            base.Configure(builder);
        }
    }
}
