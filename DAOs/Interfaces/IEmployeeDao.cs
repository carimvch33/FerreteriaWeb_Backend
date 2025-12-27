using FerreteríaWeb_Backend.Models.Entities;

namespace FerreteríaWeb_Backend.DAOs.Interfaces
{
    public interface IEmployeeDao
    {
        bool ExistsByEmail(string email);
        Employee Create(Employee employee);
        Employee? GetByEmail(string email);
    }
}
