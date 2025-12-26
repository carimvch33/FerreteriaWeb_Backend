using FerreteríaWeb_Backend.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FerreteríaWeb_Backend.Data
{
    public class FerreteriaDbContext : DbContext
    {
        public FerreteriaDbContext(DbContextOptions<FerreteriaDbContext> options)
            : base(options) { }

        public DbSet<Employee> Employees { get; set; }
    }
}
