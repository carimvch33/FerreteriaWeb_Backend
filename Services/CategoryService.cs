using FerreteríaWeb_Backend.DAOs.Interfaces;
using FerreteríaWeb_Backend.Models.DTOs.Categories;
using FerreteríaWeb_Backend.Models.Entities;
using FerreteríaWeb_Backend.Services.Interfaces;

namespace FerreteríaWeb_Backend.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryDao _categoryDao;

        public CategoryService(ICategoryDao categoryDao)
        {
            _categoryDao = categoryDao;
        }

        public Category RegisterCategory(RegisterCategoryDto dto)
        {
            if (_categoryDao.ExistsByName(dto.Name))
            {
                throw new InvalidOperationException("La categoría ya existe.");
            }

            var category = new Category
            {
                Name = dto.Name,
                Description = dto.Description,
                CreatedAt = DateTime.Now,
                IsActive = true
            };

            return _categoryDao.Create(category);
        }
    }
}
