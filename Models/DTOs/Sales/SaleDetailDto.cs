namespace FerreteríaWeb_Backend.Models.DTOs.Sales;

public class SaleDetailDto
{
    public int SaleId { get; set; }
    public int ProductId { get; set; }
    public int ProductQuantity { get; set; }

    public bool IsValid()
    {
        return ProductQuantity > 0;
    }
}