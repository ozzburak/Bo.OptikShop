using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OptikShop.Data.Entities.BaseEntity;

namespace OptikShop.Data.Entities
{
    public class CategoryEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        // Relational Property
        public List<ProductEntity> Products { get; set; }
    }



}
