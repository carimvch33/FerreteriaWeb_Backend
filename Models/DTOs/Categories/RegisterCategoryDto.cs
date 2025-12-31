using System.ComponentModel.DataAnnotations;

namespace FerreteríaWeb_Backend.Models.DTOs.Categories
{
    public class RegisterCategoryDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [MaxLength(255)]
        public string? Description { get; set; }
    }
}
