using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OptikShop.Business.Dtos;

using OptikShop.Business.Service;
using OptikShop.Data.Entities;
using OptikShop.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptikShop.Business.Manager
{

    public class ContactManager : IContactService
    {
        private readonly IRepository<ContactEntity> _contactRepository;
        private readonly IMapper _mapper;

        public ContactManager(IRepository<ContactEntity> contactRepository,IMapper mapper)
        {
                _contactRepository = contactRepository;
            _mapper = mapper;
        }
        public bool AddContact(AddContactDto contactDto)
        {
            var hasContact = _contactRepository.GetAll();

            var entity = _mapper.Map<ContactEntity>(contactDto);
            
            try
            {
                _contactRepository.Add(entity);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        

        public void DeleteContact(int id)
        {
            
            _contactRepository.Delete(id);
            
            
        }

        public List<ListContactDto> GetContacts()
        {
            var contactEntites = _contactRepository.GetAll();

            

            var contactDtos = contactEntites.Select(x => new ListContactDto{

                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                Message= x.Message,

            }).ToList();
           
            return contactDtos;
        }
    }
}
