using FerreteríaWeb_Backend.Models.DTOs;

namespace FerreteríaWeb_Backend.Models.Entities;

public class Sale
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public int EmployeeId { get; set; }
    
    public List<Product> Products { get; } = [];
    public List<SaleDetail> SaleDetails { get; } = [];
    public Employee Employee { get; } = null!;
}