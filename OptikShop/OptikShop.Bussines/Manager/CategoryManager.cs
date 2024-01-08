using OptikShop.Business.Dtos;
using OptikShop.Business.Dtos.Category;
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
    public class CategoryManager : ICategoryService
    {
        private readonly IRepository<CategoryEntity> _categoryRepository;

        public CategoryManager(IRepository<CategoryEntity> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }


        public bool AddCategory(AddCategoryDto addCategoryDto)
        {

            var hasCategory = _categoryRepository.GetAll(x => x.Name.ToLower() == addCategoryDto.Name.ToLower()).ToList();

            var entity = new CategoryEntity()
            {
                
                Name = addCategoryDto.Name,
                Description = addCategoryDto.Description,
            };
            try
            {
                _categoryRepository.Add(entity);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
            
        }

        public void DeleteCategory(int id)
        {            
            _categoryRepository.Delete(id);
        }

		public void EditCategory(EditCategoryDto editCategoryDto)
		{
			var categoryEntity = _categoryRepository.GetById(editCategoryDto.Id);

			categoryEntity.Name = editCategoryDto.Name;
			categoryEntity.Description = editCategoryDto.Description;

			_categoryRepository.Update(categoryEntity);
		}

		public List<ListCategoryDto> GetCategories()
        {
            var categoryEntities = _categoryRepository.GetAll();

            var listDto = categoryEntities.Select(x => new ListCategoryDto 
            { 
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
            }).ToList();

            return listDto;
        }

		public EditCategoryDto GetCategoryById(int id)
		{
			var categoryEntity = _categoryRepository.GetById(id);

			var editCategoryDto = new EditCategoryDto()
			{
				Id = categoryEntity.Id,
				Name = categoryEntity.Name,
				Description = categoryEntity.Description
			};

			return editCategoryDto;
		}
	}
}
