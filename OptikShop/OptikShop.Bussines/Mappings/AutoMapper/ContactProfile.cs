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
    public class ContactProfile : Profile
    {
        public ContactProfile() 
        { 
            CreateMap<ContactEntity,AddContactDto>().ReverseMap();
            CreateMap<ContactEntity,ListContactDto>().ReverseMap();
        }
    }
}
