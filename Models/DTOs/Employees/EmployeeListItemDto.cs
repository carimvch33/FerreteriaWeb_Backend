namespace FerreteríaWeb_Backend.Models.DTOs.Employees
{
    public class EmployeeListItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
    }
}
