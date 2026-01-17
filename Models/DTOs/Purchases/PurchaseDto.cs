namespace FerreteríaWeb_Backend.Models.DTOs.Purchases;

public class PurchaseDto
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public int EmployeeId { get; set; }
    public int ProviderId { get; set; }
    public List<PurchasedProductDto> Products { get; set; } = [];
}