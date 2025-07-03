
using Microsoft.EntityFrameworkCore;
using SimpleTodoApp.Models;

namespace SimpleTodoApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Mỗi DbSet đại diện cho một bảng trong CSDL
        public DbSet<TaskEntity> Tasks { get; set; }
    }
}
