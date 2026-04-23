using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CourseManagementApi.Models;
using CourseManagementApi.Enums;

namespace CourseManagementApi.Controllers;

[ApiController]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IJwtService _jwtService;

    public AuthController(AppDbContext context, IJwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }
    [HttpPost("api/auth/register")]

    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        var userExists = await _context.Users.AnyAsync(u => u.Username == registerDto.Username);
        if (userExists)
            return BadRequest("Username already exists");

        if (!Enum.TryParse<UserRole>(registerDto.Role, true, out var parsedRole))
        {
            return BadRequest("Invalid role. Allowed values: Staff, Student");
        }

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);
        var user = new User
        {
            Username = registerDto.Username,
            PasswordHash = passwordHash,
            Role = parsedRole
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok("User created");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.Username == dto.Username);

        if (user == null) return Unauthorized();

        var isValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);

        if (!isValid) return Unauthorized();

        var token = _jwtService.GenerateToken(user);

        return Ok(new { token });
    }

}