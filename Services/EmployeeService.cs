using FerreteríaWeb_Backend.DAOs.Interfaces;
using FerreteríaWeb_Backend.Models.DTOs.Employees;
using FerreteríaWeb_Backend.Models.Entities;
using FerreteríaWeb_Backend.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace FerreteríaWeb_Backend.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeDao _employeeDao;

        public EmployeeService(IEmployeeDao employeeDao)
        {
            _employeeDao = employeeDao;
        }

        public Employee RegisterEmployee(RegisterEmployeeDto dto)
        {
            if (dto.Password != dto.ConfirmPassword)
            {
                throw new InvalidOperationException("Las contraseñas no coinciden.");
            }

            if (_employeeDao.ExistsByEmail(dto.Email))
            {
                throw new InvalidOperationException(
                    "Ya existe una cuenta para ese correo electrónico."
                );
            }

            var employee = new Employee
            {
                Name = dto.Name,
                LastName = dto.LastName,
                SecondLastName = dto.SecondLastName,
                Email = dto.Email,
                Phone = dto.Phone,
                BirthDate = dto.BirthDate,
                Gender = dto.Gender,
                City = dto.City,
                Address = dto.Address,
                PostalCode = dto.PostalCode,
                PasswordHash = HashPassword(dto.Password),
                Role = EmployeeRole.Employee,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            return _employeeDao.Create(employee);
        }

        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}
