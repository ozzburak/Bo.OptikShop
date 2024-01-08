using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OptikShop.Business.Dtos;
using OptikShop.Business.Service;
using OptikShop.Data.Entities;
using OptikShop.WebUI.Areas.Admin.Models;

namespace OptikShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class ContactController : Controller
    {
        private readonly IContactService _contact;
        public ContactController(IContactService contact)
        {
                _contact = contact;
        }

        public IActionResult Index()
        {
            var listDto = _contact.GetContacts();

            var viewModel = listDto.Select(x => new ContactListViewModel
            {
                
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                Message = x.Message,
                
            }).ToList();

           
            return View(viewModel);
            
        }
        public IActionResult Delete(int id)
        {           

            _contact.DeleteContact(id);
                      
            return RedirectToAction("Index");
        }
    }
}
