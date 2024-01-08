using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OptikShop.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptikShop.Data.Configuration
{
    public class UserConfiguration : BaseConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {

            builder.Property(x => x.FirstName)
               .IsRequired()
               .HasMaxLength(30);

            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Password)
                .IsRequired();

           // builder.HasMany(x => x.Contacts).WithOne(x => x.User).HasForeignKey(x => x.UserId);


            base.Configure(builder);
        }

    }
}
