using Microsoft.AspNetCore.Mvc;
using OptikShop.Business.Service;
using OptikShop.WebUI.Models;

namespace OptikShop.WebUI.ViewComponents
{
    public class ProductViewComponent : ViewComponent
    {
        private readonly IProductService _productService;

        public ProductViewComponent(IProductService productService)
        {
            _productService = productService;
        }

        public IViewComponentResult Invoke(int? categoryId = null) 
        {
            var productDtos = _productService.GetProductsByCategoryId(categoryId);

            var viewModel = productDtos.Select(x => new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name,
                UnitInStock = x.UnitInStock,
                UnitPrice = x.UnitPrice,
                CategoryName = x.CategoryName,
                CategoryId = x.CategoryId,
                ImagePath = x.ImagePath
            }).ToList();


            return View(viewModel);
        }
    }
}
