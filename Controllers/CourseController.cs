using Microsoft.AspNetCore.Mvc;

namespace CourseManagementApi.Controllers;

public class CourseController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}