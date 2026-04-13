using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CourseManagementApi.Models;

namespace CourseManagementApi.Controllers;

[ApiController]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    public AuthController(AppDbContext context)
    {
        _context = context;
    }
    [HttpPost("api/auth/register")]
    
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        var userExists = await _context.Users.AnyAsync(u => u.Username == registerDto.Username);
        if (userExists)
            return BadRequest("Username already exists");
        
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);
        var user = new User
        {
            Username = registerDto.Username,
            PasswordHash = passwordHash,
            Role = registerDto.Role.ToLower() == "staff" ? UserRole.Staff : UserRole.Student
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        
        return Ok("User created");
    }

}