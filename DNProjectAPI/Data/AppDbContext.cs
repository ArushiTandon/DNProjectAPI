using DNProjectAPI.Entity;
using Microsoft.EntityFrameworkCore;

namespace DNProjectAPI.Data
{
    public class AppDbContext : DbContext
    {
       
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { 
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;

    }
}
