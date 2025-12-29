using System.ComponentModel.DataAnnotations;

namespace FerreteríaWeb_Backend.Models.Entities;

public class Provider
{
    public int Id { get; set; }

    [MaxLength(13)]
    public string Rfc { get; set; } = null!;

    [MaxLength(255)]
    public string Name { get; set; } = null!;
    
    [MaxLength(10)]
    public string Phone { get; set; } = null!;
    
    [MaxLength(255)]
    public string Email { get; set; } = null!;

    [MaxLength(255)]
    public string? Address { get; set; }
    public bool Active { get; set; }
}