using Microsoft.AspNetCore.Mvc;
using OptikShop.Business.Dtos;
using OptikShop.Business.Service;
using OptikShop.WebUI.Models;

namespace OptikShop.WebUI.Controllers
{
    public class ContactController : Controller
    {
       
        private readonly IContactService _contact;
        public ContactController(IContactService contact)
        {
                _contact = contact;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Form(AddContactViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            var dto = new AddContactDto()
            {
               Id = viewModel.Id,
               Email = viewModel.Email,
               FirstName = viewModel.FirstName,
               LastName = viewModel.LastName,
               Message = viewModel.Message,
            };

           var result = _contact.AddContact(dto);
            if (result)
            {
                return RedirectToAction("Index");
            }
            return View("Index", dto);
        }

        
         
    }
}
