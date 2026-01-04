using FerreteríaWeb_Backend.Models.DTOs.Categories;
using FerreteríaWeb_Backend.Models.DTOs.Products;
using FerreteríaWeb_Backend.Models.Entities;

namespace FerreteríaWeb_Backend.Services.Interfaces
{
    public interface ICategoryService
    {
        Category RegisterCategory(RegisterCategoryDto dto);
        List<CategoryWithProductsDto> GetActiveCategories();
        List<ProductByCategoryDto> GetActiveProductsByCategory(int categoryId);
    }
}
