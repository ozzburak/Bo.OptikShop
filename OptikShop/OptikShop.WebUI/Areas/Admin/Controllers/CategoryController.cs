using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OptikShop.Business.Dtos;
using OptikShop.Business.Dtos.Category;
using OptikShop.Business.Service;
using OptikShop.Data.Entities;
using OptikShop.WebUI.Areas.Admin.Models;

namespace OptikShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            var ListCategoryDto = _categoryService.GetCategories();

            var viewModel = ListCategoryDto.Select(x => new CategoryListViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Summary = x.Description 
            }).ToList();

            return View(viewModel);
        }
        public IActionResult Form()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult Form(AddCategoryViewModel viewModel)
        {
            var dto = new Business.Dtos.Category.AddCategoryDto()
            {
                Name = viewModel.Name,
                Description = viewModel.Description,
            };
           var control = _categoryService.AddCategory(dto);
            if (control)
            {
                return RedirectToAction("Index");
            }
                return View(viewModel);
           
        }
		[HttpGet]
        public IActionResult Edit(int id)
        {
			var editCategoryDto = _categoryService.GetCategoryById(id);

			var viewModel = new CategoryFormViewModel()
			{
				Id = editCategoryDto.Id,
				Name = editCategoryDto.Name,
				Description = editCategoryDto.Description
			};

			return View(viewModel);
		}
		[HttpPost]
		public IActionResult Save(CategoryFormViewModel formData)
		{
			// TODO: GÜNCELLEME İÇİN DEBUG YAP

			if (!ModelState.IsValid)
			{
				return View("Form", formData);
			}


			if (formData.Id == 0) // yeni kayıt
			{

				var addCategoryDto = new AddCategoryDto()
				{
					Name = formData.Name.Trim()
				};

				// Description null olursa trim işlemi sırasında uygulama exception verir. O nedenle trim yapmak istiyorsak aşağıdaki kontrolü yapmalıyız.
				if (formData.Description is not null)
				{
					addCategoryDto.Description = formData.Description.Trim();
				}

				var result = _categoryService.AddCategory(addCategoryDto);


				if (result)
				{
					RedirectToAction("Index","Product");

				}
				else
				{
					ViewBag.ErrorMessage = "Bu isimde bir ürün zaten mevcut.";
					return View("Form", formData);
				}

			}
			else // kayıt güncelleme
			{
				var editCategoryDto = new EditCategoryDto()
				{
					Id = formData.Id,
					Name = formData.Name,
					Description = formData.Description
				};

				_categoryService.EditCategory(editCategoryDto);

				return RedirectToAction("Index", "Category");
			}


			return RedirectToAction("Index", "Category");
		}

		public IActionResult Delete(int id)
        {
            _categoryService.DeleteCategory(id);
            
            return RedirectToAction("Index");
        }
       
    }
}
