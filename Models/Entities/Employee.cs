namespace FerreteríaWeb_Backend.Models.Entities
{
    public enum EmployeeRole
    {
        Admin,
        Employee
    }

    public class Employee
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? SecondLastName { get; set; }

        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;

        public DateTime BirthDate { get; set; }
        public string Gender { get; set; } = null!;

        public string City { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string PostalCode { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;

        public EmployeeRole Role { get; set; }

        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }

        public List<Sale> Sales { get; } = [];
    }
}
