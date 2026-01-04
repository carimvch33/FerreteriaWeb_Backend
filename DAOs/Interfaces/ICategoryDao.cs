using FerreteríaWeb_Backend.Models.Entities;

namespace FerreteríaWeb_Backend.DAOs.Interfaces
{
    public interface ICategoryDao
    {
        bool ExistsByName(string name);
        Category Create(Category category);
        Category? GetById(int id);
        List<Category> GetActiveWithProducts();
        Category GetByIdWithProducts(int id);

    }
}
