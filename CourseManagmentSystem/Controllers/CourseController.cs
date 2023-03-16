using CourseManagmentSystem.Data;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagmentSystem.Controllers
{
    public class CourseController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CourseController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            IEnumerable<Models.Course> courses = _context.Courses.ToList();
            return View(courses);
        }
    }
}
