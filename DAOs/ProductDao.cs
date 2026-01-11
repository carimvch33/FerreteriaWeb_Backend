using FerreteríaWeb_Backend.DAOs.Interfaces;
using FerreteríaWeb_Backend.Data;
using FerreteríaWeb_Backend.Models.DTOs;
using FerreteríaWeb_Backend.Models.DTOs.Products;
using FerreteríaWeb_Backend.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FerreteríaWeb_Backend.DAOs
{
    public class ProductDao : IProductDao
    {
        private readonly FerreteriaDbContext _context;

        public ProductDao(FerreteriaDbContext context)
        {
            _context = context;
        }

        public bool ExistsByName(string name)
        {
            return _context.Products.Any(p => p.Name == name && p.IsActive);
        }

        public Product Create(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return product;
        }
        public Product? GetById(int id)
        {
            return _context.Products.FirstOrDefault(p => p.Id == id);
        }
        public void Update(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }
        public List<Product> GetActiveProducts()
        {
            return _context.Products
                .Where(p => p.IsActive)
                .ToList();
        }

        public Result<List<ProductListItemDto>> GetProductsBySearchString(string searchString)
        {
            Result<List<ProductListItemDto>> result = new();
            List<ProductListItemDto> products = [];

            try
            {
                var queryResult = _context.Products.Where((p) => EF.Functions.Like(p.Name.ToLower(), $"%{searchString}%") && p.IsActive);
                foreach (Product product in queryResult)
                {
                    products.Add(new()
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Description = product.Description ?? "",
                        Price = product.Price,
                        Stock = product.Stock,
                        CategoryId = product.CategoryId
                    });
                }
            }
            catch (DbUpdateException error)
            {
                result.InnerException = error;
                result.IsAccomplished = false;
            }

            if (result.InnerException is null)
            {
                result.Data = products;
                result.IsAccomplished = true;
            }

            return result;
        }
    }
}
