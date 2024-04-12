using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;

namespace MvcMovie.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Person> People { get; set; } // Sửa từ "Person" thành "People" để phản ánh số nhiều

        // Nếu bạn có nhiều bảng khác, bạn cũng có thể thêm các DbSet khác ở đây
    }
}