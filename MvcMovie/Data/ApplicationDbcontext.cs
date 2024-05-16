using Microsoft.EntityFrameworkCore;

using MvcMovie.Models;
namespace DemoMVC.Data
{
    public class ApplicationDbContext: DbContext 
    {
         public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options) : base(options)
        {}
        public DbSet<Person>Persons {get;set;}
        public DbSet<Student>Student {get;set;}

        public DbSet<DaiLy>DaiLy {get;set;}
        public DbSet<Employee>Employees {get;set;}
        public DbSet<Hethongphanphoi>Hethongphanphois {get;set;}
         protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .HasIndex(p => p.PersonId)
                .IsUnique();
        }
        }
        }
        