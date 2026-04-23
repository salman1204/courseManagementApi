using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CourseManagementApi.Enums;
namespace CourseManagementApi.Models;

public class User
{
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Username { get; set; }

    [Required]
    [PasswordPropertyText]
    public string PasswordHash { get; set; }

    [Required]
    public UserRole Role { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

