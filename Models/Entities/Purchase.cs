namespace FerreteríaWeb_Backend.Models.Entities;

public class Purchase
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public int EmployeeId { get; set; }
    public int ProviderId { get; set; }
}