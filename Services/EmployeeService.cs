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
                throw new InvalidOperationException("Ya existe una cuenta para ese correo electrónico. Por favor inicie sesión o use un correo diferente.");
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

        public List<EmployeeListItemDto> GetActiveEmployees()
        {
            return _employeeDao.GetActiveEmployees().Select(e => new EmployeeListItemDto
            {
                Id = e.Id,
                Name = e.Name,
                LastName = $"{e.LastName} {e.SecondLastName}",
                Email = e.Email,
                Phone = e.Phone
            }).ToList();
        }

        public UpdateEmployeeResponseDto GetEmployeeById(int id)
        {
            var employee = _employeeDao.GetById(id)
                ?? throw new InvalidOperationException("Empleado no encontrado");

            return new UpdateEmployeeResponseDto
            {
                Id = employee.Id,
                Name = employee.Name,
                LastName = employee.LastName,
                SecondLastName = employee.SecondLastName,
                Phone = employee.Phone,
                BirthDate = employee.BirthDate,
                Gender = employee.Gender,
                City = employee.City,
                Address = employee.Address,
                PostalCode = employee.PostalCode
            };
        }

        public UpdateEmployeeResponseDto UpdateEmployee(int id, UpdateEmployeeRequestDto dto)
        {
            var employee = _employeeDao.GetById(id);

            if (employee == null || !employee.IsActive)
            {
                throw new InvalidOperationException("Empleado no encontrado.");
            }

            employee.Name = dto.Name;
            employee.LastName = dto.LastName;
            employee.SecondLastName = dto.SecondLastName;
            employee.Phone = dto.Phone;
            employee.BirthDate = dto.BirthDate;
            employee.Gender = dto.Gender;
            employee.City = dto.City;
            employee.Address = dto.Address;
            employee.PostalCode = dto.PostalCode;

            _employeeDao.Update(employee);

            return new UpdateEmployeeResponseDto
            {
                Id = employee.Id,
                Name = employee.Name,
                LastName = employee.LastName,
                SecondLastName = employee.SecondLastName,
                Phone = employee.Phone,
                BirthDate = employee.BirthDate,
                Gender = employee.Gender,
                City = employee.City,
                Address = employee.Address,
                PostalCode = employee.PostalCode,
            };
        }

        public UpdateEmployeeResponseDto DeleteEmployee(int id)
        {
            var employee = _employeeDao.GetById(id);

            if (employee == null || !employee.IsActive)
            {
                throw new InvalidOperationException("Empleado no encontrado.");
            }

            _employeeDao.Deactivate(employee);

            return new UpdateEmployeeResponseDto
            {
                Id = employee.Id,
            };
        }
    }
}
