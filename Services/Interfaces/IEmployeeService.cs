using FerreteríaWeb_Backend.Models.DTOs.Employees;
using FerreteríaWeb_Backend.Models.Entities;

namespace FerreteríaWeb_Backend.Services.Interfaces
{
    public interface IEmployeeService
    {
        Employee RegisterEmployee(RegisterEmployeeDto dto);
    }
}
