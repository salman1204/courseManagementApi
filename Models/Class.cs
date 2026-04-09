using System.ComponentModel.DataAnnotations;

namespace CourseManagementApi.Models;

public class Class
{
    public int Id { get; set; }
        
    [Required]
    [StringLength(50)]
    public string title { get; set; }
}