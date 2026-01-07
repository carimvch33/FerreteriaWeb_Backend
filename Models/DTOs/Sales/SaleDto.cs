namespace FerreteríaWeb_Backend.Models.DTOs;

public class SaleDto
{
    public int Id { get; set; }
    public DateTime? CreatedAt { get; set; }
    public String PaymentMethod { get; set; } = "";
    public int EmployeeId { get; set; }
    public List<SaleDetailDto> SaleDetails { get; set; } = [];

    public bool IsValid()
    {
        return Enum.TryParse(PaymentMethod, true, out PaymentMethod _);
    }
}