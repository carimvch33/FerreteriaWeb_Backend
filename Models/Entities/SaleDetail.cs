namespace FerreteríaWeb_Backend.Models.Entities;

public class SaleDetail
{
    public int SaleId { get; set; }
    public int ProductId { get; set; }
    public int ProductQuantity { get; set; }
    
    public Product Product { get; } = null!;
    public Sale Sale { get; } = null!;
}