using AutoMapper;
using OptikShop.Business.Dtos;
using OptikShop.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptikShop.Business.Mappings.AutoMapper
{
    public class ProductProfile : Profile
    {
        public ProductProfile() 
        { 
            CreateMap<ProductEntity,AddProductDto>().ReverseMap();
            CreateMap<ProductEntity,EditProductDto>().ReverseMap();
            CreateMap<ProductEntity, ListProductDto>().ReverseMap();
        }
    }
}
