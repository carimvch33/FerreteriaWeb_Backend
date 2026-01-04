using FerreteríaWeb_Backend.DAOs.Interfaces;
using FerreteríaWeb_Backend.Data;
using FerreteríaWeb_Backend.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FerreteríaWeb_Backend.DAOs
{
    public class CategoryDao : ICategoryDao
    {
        private readonly FerreteriaDbContext _context;

        public CategoryDao(FerreteriaDbContext context)
        {
            _context = context;
        }

        public bool ExistsByName(string name)
        {
            return _context.Categories.Any(c => c.Name == name && c.IsActive);
        }

        public Category Create(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return category;
        }

        public Category? GetById(int id)
        {
            return _context.Categories.FirstOrDefault(c => c.Id == id && c.IsActive);
        }
        public List<Category> GetActiveWithProducts()
        {
            return _context.Categories
                .Include(c => c.Products.Where(p => p.IsActive))
                .Where(c => c.IsActive)
                .ToList();
        }
        public Category GetByIdWithProducts(int id)
        {
            return _context.Categories
                .Include(c => c.Products.Where(p => p.IsActive))
                .FirstOrDefault(c => c.Id == id);
        }
    }
}
