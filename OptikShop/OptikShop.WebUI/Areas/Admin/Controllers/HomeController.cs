using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OptikShop.Business.Service;
using OptikShop.Data.Context;
using OptikShop.Data.Entities;
using OptikShop.Data.Repository;
using OptikShop.WebUI.Areas.Admin.Models;



namespace OptikShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="admin")]
    public class HomeController : Controller
    {
        private readonly IRepository<ContactEntity> _contactrepository;
        private readonly IRepository<CategoryEntity> _categoryRepository;
       private readonly IRepository<ProductEntity> _productRepository;
        public HomeController(IRepository<ContactEntity> contactrepository, IRepository<CategoryEntity> categoryRepository, IRepository<ProductEntity> productRepository)
        {
            _contactrepository = contactrepository;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }
        public IActionResult Index()
        {


            var statusmodel = new StatusViewModel()
            {
               CategoryCount = _categoryRepository.GetAll().ToList().Count,
               ProductCount = _productRepository.GetAll().ToList().Count,
               ContactCount = _contactrepository.GetAll().ToList().Count,
            };

            return View(statusmodel);
        }
       
       
    }
}
