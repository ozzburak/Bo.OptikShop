using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient.Server;
using OptikShop.Business.Dtos;
using OptikShop.Business.Service;
using OptikShop.WebUI.Areas.Admin.Models;

namespace OptikShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _environment;

        public ProductController(IProductService productService, ICategoryService categoryService, IWebHostEnvironment environment)
        {
            _productService = productService;
            _categoryService = categoryService;
            _environment = environment;
        }

        public IActionResult Index()
        {

            var productDtoList = _productService.GetProducts();

            // Select ile bir tür listeden diğer tür listeye çeviriyorum.
            // Her bir elemanı için yeni bir ListProductViewModel açılıp veriler aktarılıyor.
            var viewModel = productDtoList.Select(x => new ProductListViewModel
            {
                Id = x.Id,
                Name = x.Name,
                CategoryId = x.CategoryId,
                CategoryName = x.CategoryName,
                UnitInStock = x.UnitInStock,
                UnitPrice = x.UnitPrice,
                ImagePath = x.ImagePath
            }).ToList();

            return View(viewModel);


        }
        public IActionResult Form()
        {
            var c = _categoryService.GetCategories();

            List<SelectListItem> values = (from x in c.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.Name,
                                               Value = x.Id.ToString(),

                                           }).ToList();

            ViewBag.V1 = values;

            return View();
        }

        [HttpPost]
        public IActionResult Form(AddProductViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var c = _categoryService.GetCategories();

                List<SelectListItem> values = (from x in c.ToList()
                                               select new SelectListItem
                                               {
                                                   Text = x.Name,
                                                   Value = x.Id.ToString(),

                                               }).ToList();

                ViewBag.V1 = values;
            }

            var newFileName = "";

            if (viewModel.File is not null) // bir görsel gönderildiyse
            {

                var allowedFileTypes = new string[] { "image/jpeg", "image/jpg", "image/png", "image/jfif" };
                // izin vereceğim dosya türleri.

                var allowedFileExtensions = new string[] { ".jpg", ".jpeg", ".png", ".jfif" };
                // izin vereceğim dosya uzantıları.

                var fileContentType = viewModel.File.ContentType; //dosyanın içerik tipi.

                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(viewModel.File.FileName); // uzantısız dosya ismi.

                var fileExtension = Path.GetExtension(viewModel.File.FileName); // uzantı.

                if (!allowedFileTypes.Contains(fileContentType) ||
                    !allowedFileExtensions.Contains(fileExtension))
                {
                    ViewBag.FileError = "Dosya formatı veya içeriği hatalı";


                    var c = _categoryService.GetCategories();

                    List<SelectListItem> values = (from x in c.ToList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.Name,
                                                       Value = x.Id.ToString(),

                                                   }).ToList();

                    ViewBag.V1 = values;
                    return View("Form", viewModel);

                }

                newFileName = fileNameWithoutExtension + "-" + Guid.NewGuid() + fileExtension;
                // Aynı isimde iki dosya yüklenildiğinde hata vermesin, birbiriyle asla eşleşmeyecek şekilde her dosya adına unique(eşsiz) bir metin ilavesi yapıyorum.

                var folderPath = Path.Combine("images", "products");
                // images/products

                var wwwrootFolderPath = Path.Combine(_environment.WebRootPath, folderPath);
                // ...wwwroot/images/products

                var wwwrootFilePath = Path.Combine(wwwrootFolderPath, newFileName);
                // ...wwwroot/images/products/urunGorseli-12312312.jpg

                Directory.CreateDirectory(wwwrootFolderPath); // Eğer images/products yoksa, oluştur.

                using (var fileStream = new FileStream(
                    wwwrootFilePath, FileMode.Create))
                {
                    viewModel.File.CopyTo(fileStream);
                }
                // asıl dosyayı kopyaladığım kısım.

                // using içerisinde new'lenen filestream nesnesi scope boyunca yaşar, scope bitimi silinir.
            }

            var dto = new AddProductDto()
            {

                Name = viewModel.Name,
                Description = viewModel.Description,
                UnitPrice = viewModel.UnitPrice,
                UnitInStock = viewModel.UnitInStock,
                CategoryId = viewModel.CategoryId,
                ImagePath = newFileName

            };
            var control = _productService.AddProduct(dto);
            if (control)
            {
                return RedirectToAction("Index");
            }
            return View(viewModel);


        }
        public IActionResult Edit(int id)
        {

            var c = _categoryService.GetCategories();

            List<SelectListItem> values = (from x in c.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.Name,
                                               Value = x.Id.ToString(),

                                           }).ToList();

            ViewBag.V1 = values;

            var editProductDto = _productService.GetProductById(id);

            var viewModel = new ProductFormViewModel()
            {
                Id = editProductDto.Id,
                Name = editProductDto.Name,
                Description = editProductDto.Description,
                UnitInStock = editProductDto.UnitInStock,
                UnitPrice = editProductDto.UnitPrice,
                CategoryId = editProductDto.CategoryId
            };
            ViewBag.ImagePath = editProductDto.ImagePath;
            ViewBag.Categories = _categoryService.GetCategories();
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Save(ProductFormViewModel formData)
        {
        
            var newFileName = "";

            if (formData.File is not null) // bir görsel gönderildiyse
            {

                var allowedFileTypes = new string[] { "image/jpeg", "image/jpg", "image/png", "image/jfif" };
                // izin vereceğim dosya türleri.

                var allowedFileExtensions = new string[] { ".jpg", ".jpeg", ".png", ".jfif" };
                // izin vereceğim dosya uzantıları.

                var fileContentType = formData.File.ContentType; //dosyanın içerik tipi.

                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(formData.File.FileName); // uzantısız dosya ismi.

                var fileExtension = Path.GetExtension(formData.File.FileName); // uzantı.

                if (!allowedFileTypes.Contains(fileContentType) ||
                    !allowedFileExtensions.Contains(fileExtension))
                {
                    ViewBag.FileError = "Dosya formatı veya içeriği hatalı";


                    ViewBag.Categories = _categoryService.GetCategories();
                    return View("Form", formData);

                }

                newFileName = fileNameWithoutExtension + "-" + Guid.NewGuid() + fileExtension;
                // Aynı isimde iki dosya yüklenildiğinde hata vermesin, birbiriyle asla eşleşmeyecek şekilde her dosya adına unique(eşsiz) bir metin ilavesi yapıyorum.

                var folderPath = Path.Combine("images", "products");
                // images/products

                var wwwrootFolderPath = Path.Combine(_environment.WebRootPath, folderPath);
                // ...wwwroot/images/products

                var wwwrootFilePath = Path.Combine(wwwrootFolderPath, newFileName);
                // ...wwwroot/images/products/urunGorseli-12312312.jpg

                Directory.CreateDirectory(wwwrootFolderPath); // Eğer images/products yoksa, oluştur.

                using (var fileStream = new FileStream(
                    wwwrootFilePath, FileMode.Create))
                {
                    formData.File.CopyTo(fileStream);
                }
                // asıl dosyayı kopyaladığım kısım.

                // using içerisinde new'lenen filestream nesnesi scope boyunca yaşar, scope bitimi silinir.
            }

            var editProductDto = new EditProductDto()
            {
                Id = formData.Id,
                Name = formData.Name,
                Description = formData.Description,
                UnitInStock = formData.UnitInStock,
                UnitPrice = formData.UnitPrice,
                CategoryId = formData.CategoryId
            };

            if (formData.File is not null)
            {
                editProductDto.ImagePath = newFileName;
            }
            // Bu kontrolü hem controller hem business katmanında yapacağım. Yeni bir dosya seçilmediyse yani null gönderildiyse Db'de olan görselin üzerine yazılmasını istemiyorum.

            _productService.EditProduct(editProductDto);

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            _productService.DeleteProduct(id);
            return RedirectToAction("Index");
        }

    }
}
