using System.ComponentModel.DataAnnotations;

namespace FerreteríaWeb_Backend.Models.DTOs.Employees
{
    public class UpdateEmployeeRequestDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = null!;

        [MaxLength(50)]
        public string? SecondLastName { get; set; }

        [Required]
        [Phone]
        [MaxLength(10)]
        public string Phone { get; set; } = null!;

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public string Gender { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string City { get; set; } = null!;

        [Required]
        [MaxLength(255)]
        public string Address { get; set; } = null!;

        [Required]
        [MaxLength(5)]
        public string PostalCode { get; set; } = null!;
    }
}
