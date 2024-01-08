using Microsoft.AspNetCore.Mvc;

namespace OptikShop.WebUI.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
