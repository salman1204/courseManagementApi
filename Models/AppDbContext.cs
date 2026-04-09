using CourseManagementApi.Models;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Course> Courses { get; set; } 
    public DbSet<Class> Classes { get; set; }
    public DbSet<User> Users { get; set; }
}