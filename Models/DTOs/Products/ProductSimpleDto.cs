namespace FerreteríaWeb_Backend.Models.DTOs.Products
{
    public class ProductSimpleDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
