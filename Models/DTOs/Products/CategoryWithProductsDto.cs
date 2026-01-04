using FerreteríaWeb_Backend.Models.DTOs.Products;

namespace FerreteríaWeb_Backend.Models.DTOs.Categories
{
    public class CategoryWithProductsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public List<ProductSimpleDto> Products { get; set; } = new();
    }
}
