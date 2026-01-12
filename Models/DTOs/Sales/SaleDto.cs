namespace FerreteríaWeb_Backend.Models.DTOs.Sales;

public class SaleDto
{
    public int Id { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string PaymentMethod { get; set; } = "";
    public int EmployeeId { get; set; }
    public List<SaleDetailDto> SaleDetails { get; set; } = [];

    public bool IsValid()
    {
        return Enum.TryParse(PaymentMethod, true, out PaymentMethod _);
    }
}