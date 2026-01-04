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

        public UpdateProductResponseDto UpdateProduct(int id, UpdateProductDto dto)
        {
            var product = _productDao.GetById(id);
            var category = _categoryDao.GetById(dto.CategoryId);
            if (category == null)
                throw new InvalidOperationException("La categoría no existe.");


            if (product == null)
                throw new InvalidOperationException("Producto no encontrado.");

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.Stock = dto.Stock;
            product.CategoryId = dto.CategoryId;
            product.IsActive = dto.IsActive;

            _productDao.Update(product);
            var message = dto.IsActive
                ? "Producto actualizado correctamente."
                : "Producto desactivado correctamente.";


            return new UpdateProductResponseDto
            {
                Id = product.Id,
                Message = message
            };
        }
        public AddInventoryResponseDto AddInventory(int productId, AddInventoryDto dto)
        {
            if (dto.Quantity <= 0)
                throw new InvalidOperationException("La cantidad debe ser mayor a cero.");

            var product = _productDao.GetById(productId);

            if (product == null || !product.IsActive)
                throw new InvalidOperationException("Producto no encontrado o inactivo.");

            product.Stock += dto.Quantity;

            _productDao.Update(product);

            return new AddInventoryResponseDto
            {
                ProductId = product.Id,
                NewStock = product.Stock,
                Message = "El inventario se ha actualizado correctamente."
            };
        }
        public List<ProductListItemDto> GetActiveProducts()
        {
            return _productDao.GetActiveProducts()
                .Select(p => new ProductListItemDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Stock = p.Stock,
                    CategoryId = p.CategoryId
                })
                .ToList();
        }

    }
}
