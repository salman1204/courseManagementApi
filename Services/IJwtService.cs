using CourseManagementApi.Models;

public interface IJwtService
{
    string GenerateToken(User user);
}