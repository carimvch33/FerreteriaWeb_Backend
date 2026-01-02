namespace FerreteríaWeb_Backend.Models.DTOs.Products
{
    public class AddInventoryResponseDto
    {
        public int ProductId { get; set; }
        public int NewStock { get; set; }
        public string Message { get; set; } = null!;
    }
}
