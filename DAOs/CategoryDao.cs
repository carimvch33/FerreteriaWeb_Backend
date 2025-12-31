using FerreteríaWeb_Backend.DAOs.Interfaces;
using FerreteríaWeb_Backend.Data;
using FerreteríaWeb_Backend.Models.Entities;

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
    }
}
