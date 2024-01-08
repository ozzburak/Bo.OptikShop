using OptikShop.Business.Dtos;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptikShop.Business.Service
{
	public interface IContactService
	{
		bool AddContact(AddContactDto addContact);

		List<ListContactDto> GetContacts();
		void DeleteContact(int id);
	}
}
