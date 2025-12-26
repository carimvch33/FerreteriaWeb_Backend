using FerreteríaWeb_Backend.DAOs.Interfaces;
using FerreteríaWeb_Backend.Data;
using FerreteríaWeb_Backend.Models.Entities;

namespace FerreteríaWeb_Backend.DAOs
{
    public class EmployeeDao : IEmployeeDao
    {
        private readonly FerreteriaDbContext _context;

        public EmployeeDao(FerreteriaDbContext context)
        {
            _context = context;
        }

        public bool ExistsByEmail(string email)
        {
            return _context.Employees.Any(e => e.Email == email);
        }

        public Employee Create(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return employee;
        }
    }
}
