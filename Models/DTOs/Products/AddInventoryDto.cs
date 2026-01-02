using System.ComponentModel.DataAnnotations;

namespace FerreteríaWeb_Backend.Models.DTOs.Products
{
    public class AddInventoryDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a cero.")]
        public int Quantity { get; set; }
    }
}
