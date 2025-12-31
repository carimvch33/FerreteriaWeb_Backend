using FerreteríaWeb_Backend.Models.Entities;

namespace FerreteríaWeb_Backend.DAOs.Interfaces
{
    public interface IProductDao
    {
        bool ExistsByName(string name);
        Product Create(Product product);
    }
}
