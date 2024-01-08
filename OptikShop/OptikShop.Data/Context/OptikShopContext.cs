using Microsoft.EntityFrameworkCore;
using OptikShop.Data.Configuration;
using OptikShop.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptikShop.Data.Context
{
    public class OptikShopContext : DbContext
    {

        public OptikShopContext(DbContextOptions<OptikShopContext> options): base (options) 
        { 

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ContactConfiguration());


            modelBuilder.Entity<UserEntity>().HasData(new List<UserEntity>()
            {
                new UserEntity() 
                { 
                    Id = 1,
                    FirstName = "Burak",
                    LastName = "Öz",
                    Email = "mefurkanoz@gmail.com",
                    Password = "123",
                    UserType = Enums.UserTypeEnum.admin
                }
            });


            base.OnModelCreating(modelBuilder);
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<ContactEntity> Contacts { get; set; }
        public DbSet<ProductEntity> Products { get; set; }

    }
}
