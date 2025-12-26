namespace FerreteríaWeb_Backend.Models.DTOs.Employees
{
    public class EmployeeResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Role { get; set; } = null!;
    }
}
