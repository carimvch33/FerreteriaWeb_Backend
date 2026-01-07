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
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleDetail> SaleDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sale>()
                .HasMany(e => e.Products)
                .WithMany(e => e.Sales)
                .UsingEntity<SaleDetail>();

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Sales)
                .WithOne(e => e.Employee)
                .HasForeignKey(e => e.EmployeeId)
                .IsRequired();
        }
    }
}
