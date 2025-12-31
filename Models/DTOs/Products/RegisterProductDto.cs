using System.ComponentModel.DataAnnotations;

namespace FerreteríaWeb_Backend.Models.DTOs.Products
{
    public class RegisterProductDto
    {
        [Required]
        [MaxLength(150)]
        public string Name { get; set; } = null!;

        [MaxLength(255)]
        public string? Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int InitialStock { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}
