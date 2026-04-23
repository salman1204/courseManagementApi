using CourseManagementApi.Models;
using CourseManagementApi.Enums;
using Microsoft.EntityFrameworkCore;

public static class DbSeeder
{
    public static async Task SeedAdminAsync(AppDbContext context)
    {
        var adminExists = await context.Users
            .AnyAsync(u => u.Username == "admin");

        if (adminExists) return;

        var adminUser = new User
        {
            Username = "admin",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin"),
            Role = UserRole.Staff
        };

        context.Users.Add(adminUser);
        await context.SaveChangesAsync();
    }
}