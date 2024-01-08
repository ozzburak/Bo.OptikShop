using OptikShop.Business.Dtos;
using OptikShop.Business.Dtos.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptikShop.Business.Service
{
    public interface ICategoryService
    {
        bool AddCategory(AddCategoryDto addCategoryDto);

        List<ListCategoryDto> GetCategories();

        EditCategoryDto GetCategoryById(int id);

		void EditCategory(EditCategoryDto editCategoryDto);

        void DeleteCategory(int id);
    }
}
