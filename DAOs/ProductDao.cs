using FerreteríaWeb_Backend.DAOs.Interfaces;
using FerreteríaWeb_Backend.Data;
using FerreteríaWeb_Backend.Models.Entities;

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
    }
}
