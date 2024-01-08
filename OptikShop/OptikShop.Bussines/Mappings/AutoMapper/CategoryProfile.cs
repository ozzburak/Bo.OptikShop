using AutoMapper;
using OptikShop.Business.Dtos;
using OptikShop.Business.Dtos.Category;
using OptikShop.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptikShop.Business.Mappings.AutoMapper
{
    public class CategoryProfile :Profile
    {
        public CategoryProfile() 
        { 
            CreateMap<CategoryEntity,AddCategoryDto>().ReverseMap();
            CreateMap<CategoryEntity, EditCategoryDto>().ReverseMap();
            CreateMap<CategoryEntity,ListCategoryDto>().ReverseMap();
        }

    }
}
