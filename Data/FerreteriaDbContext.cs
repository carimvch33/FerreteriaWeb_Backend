using FerreteríaWeb_Backend.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FerreteríaWeb_Backend.Data
{
    public class FerreteriaDbContext : DbContext
    {
        public FerreteriaDbContext(DbContextOptions<FerreteriaDbContext> options)
            : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Provider> Providers { get; set; }
    }
}
