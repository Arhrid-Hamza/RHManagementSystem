using Microsoft.EntityFrameworkCore;
using RHManagementSystem.Models;

namespace RHManagementSystem.Data
{
    public class RHManagementContext : DbContext
    {
        public RHManagementContext(DbContextOptions<RHManagementContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<Department> Departments { get; set; } = null!;
        public DbSet<Project> Projects { get; set; } = null!;
        public DbSet<Report> Reports { get; set; } = null!;
    }
}
