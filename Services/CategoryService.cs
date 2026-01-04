using FerreteríaWeb_Backend.DAOs.Interfaces;
using FerreteríaWeb_Backend.Models.DTOs.Categories;
using FerreteríaWeb_Backend.Models.DTOs.Products;
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
        public List<CategoryWithProductsDto> GetActiveCategories()
        {
            var categories = _categoryDao.GetActiveWithProducts();

            return categories.Select(c => new CategoryWithProductsDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Products = c.Products.Select(p => new ProductSimpleDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Stock = p.Stock
                }).ToList()
            }).ToList();
        }
        public List<ProductByCategoryDto> GetActiveProductsByCategory(int categoryId)
        {
            var category = _categoryDao.GetByIdWithProducts(categoryId);

            if (category == null || !category.IsActive)
                throw new InvalidOperationException("Categoría no encontrada.");

            return category.Products
                .Where(p => p.IsActive)
                .Select(p => new ProductByCategoryDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Stock = p.Stock,
                    IsActive = p.IsActive
                })
                .ToList();
        }

    }
}
