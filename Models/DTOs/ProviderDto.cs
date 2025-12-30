namespace FerreteríaWeb_Backend.Models.DTOs;

public class ProviderDto
{   
    public int Id { get; set; }
    public string Rfc { get; set; } = "";
    public string Name { get; set; } = "";
    public string Phone { get; set; } = "";
    public string Email { get; set; } = "";
    public string? Address { get; set; }
    public bool Active { get; set; } = true;

    public bool IsValid()
    {
        return !string.IsNullOrWhiteSpace(Rfc)
            && !string.IsNullOrWhiteSpace(Name)
            && !string.IsNullOrWhiteSpace(Phone)
            && !string.IsNullOrWhiteSpace(Email);
    }
}