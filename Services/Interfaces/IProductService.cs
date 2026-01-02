using FerreteríaWeb_Backend.Models.DTOs.Products;
using FerreteríaWeb_Backend.Models.Entities;

namespace FerreteríaWeb_Backend.Services.Interfaces
{
    public interface IProductService
    {
        Product RegisterProduct(RegisterProductDto dto);
        UpdateProductResponseDto UpdateProduct(int productId, UpdateProductDto dto);
        AddInventoryResponseDto AddInventory(int productId, AddInventoryDto dto);
    }
}
