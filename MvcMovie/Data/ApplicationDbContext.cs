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
        public DbSet<Employee> Employees { get;set;} = default!;
        public DbSet<Student> Student { get;set;} = default!;

         public DbSet<DaiLy> DaiLy { get; set; }
    }
}