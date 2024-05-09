using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;

namespace MvcMovie.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Person> Persons { get; set; } = default!;
        public DbSet<Employee> Employees { get; set; } = default!;
        public DbSet<Student> Student { get; set; } = default!;
        public DbSet<Hethongphanphoi> Hethongphanphois { get; set; } = default!;
        public DbSet<DaiLy>DaiLy { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the Hethongphanphoi table
            modelBuilder.Entity<Hethongphanphoi>()
                .ToTable("Hethongphanphois")
                .HasKey(h => h.MaHTPP);
        }
    }
}
