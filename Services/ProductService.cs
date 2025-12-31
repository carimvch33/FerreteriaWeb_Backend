using FerreteríaWeb_Backend.DAOs.Interfaces;
using FerreteríaWeb_Backend.Models.DTOs.Products;
using FerreteríaWeb_Backend.Models.Entities;
using FerreteríaWeb_Backend.Services.Interfaces;

namespace FerreteríaWeb_Backend.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductDao _productDao;
        private readonly ICategoryDao _categoryDao;

        public ProductService(IProductDao productDao, ICategoryDao categoryDao)
        {
            _productDao = productDao;
            _categoryDao = categoryDao;
        }

        public Product RegisterProduct(RegisterProductDto dto)
        {
            if (_productDao.ExistsByName(dto.Name))
                throw new InvalidOperationException("El producto ya existe.");

            var category = _categoryDao.GetById(dto.CategoryId);
            if (category == null)
                throw new InvalidOperationException("La categoría no existe.");

            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Stock = dto.InitialStock,
                CategoryId = dto.CategoryId,
                IsActive = true,
                CreatedAt = DateTime.Now
            };

            return _productDao.Create(product);
        }
    }
}
