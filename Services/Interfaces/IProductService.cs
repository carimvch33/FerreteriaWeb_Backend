using FerreteríaWeb_Backend.Models.DTOs;
using FerreteríaWeb_Backend.Models.DTOs.Products;
using FerreteríaWeb_Backend.Models.Entities;

namespace FerreteríaWeb_Backend.Services.Interfaces
{
    public interface IProductService
    {
        Product RegisterProduct(RegisterProductDto dto);
        UpdateProductResponseDto UpdateProduct(int productId, UpdateProductDto dto);
        AddInventoryResponseDto AddInventory(int productId, AddInventoryDto dto);
        List<ProductListItemDto> GetActiveProducts();
        Result<List<ProductListItemDto>> GetProductsBySearchString(string searchString);
        Result<ProductListItemDto?> GetProductById(int id);
    }
}
