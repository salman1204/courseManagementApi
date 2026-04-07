using System.ComponentModel.DataAnnotations;

namespace CourseManagementApi.Models;

public class Course
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(50)]
    public string Title { get; set; }
}