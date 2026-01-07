using FerreteríaWeb_Backend.Models.DTOs;
using FerreteríaWeb_Backend.Models.DTOs.Products;
using FerreteríaWeb_Backend.Models.Entities;

namespace FerreteríaWeb_Backend.DAOs.Interfaces
{
    public interface IProductDao
    {
        bool ExistsByName(string name);
        Product Create(Product product);
        Product? GetById(int id);
        void Update(Product product);
        List<Product> GetActiveProducts();
        Result<List<ProductListItemDto>> GetProductsBySearchString(string searchString);
    }
}
