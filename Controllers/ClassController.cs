using CourseManagementApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourseManagementApi.Controllers;

[Route("api/class")]
[ApiController]

public class ClassController : ControllerBase
{
    private readonly AppDbContext _context;
    
    public ClassController(AppDbContext context)
    {
        _context = context;
    }
    
   [HttpGet]
   public async Task<IActionResult> GetClasses()
   {
       var classes = await _context.Classes.ToListAsync();
       return Ok(classes);
   }
   
   [HttpGet("{id}")]
   public async Task<IActionResult> GetClass(int id)
   {
       var classItem = await _context.Classes.FindAsync(id);
       if (classItem == null)
           return NotFound();
       
       return Ok(classItem);
   }
   
   [HttpPost]
   public async Task<IActionResult> CreateClass([FromBody] Class classItem)
   {
       _context.Classes.Add(classItem);
       await _context.SaveChangesAsync();
       return CreatedAtAction(nameof(GetClass), new { id = classItem.Id }, classItem);
   }
   
   [HttpPut("{id}")]
   public async Task<IActionResult> UpdateClass(int id, [FromBody] Class classItem)
   {
       if (id != classItem.Id)
           return BadRequest();
       
       _context.Entry(classItem).State = EntityState.Modified;
       await _context.SaveChangesAsync();
       return NoContent();
   }

   [HttpDelete("{id}")]
   public IActionResult DeleteClass(int id)
   {
       var classes = _context.Classes.Find(id);
       if (classes == null)
           return NotFound();
       _context.Classes.Remove(classes);
       _context.SaveChanges();
       return NoContent();
   }
}