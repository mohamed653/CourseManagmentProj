using CourseManagmentSystem.Data;
using CourseManagmentSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace CourseManagmentSystem.Controllers
{
    public class CourseController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> userManager;
        public CourseController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            this.userManager = userManager;

        }
        public IActionResult Index()
        {
            IEnumerable<Models.Course> courses = _context.Courses.ToList();
            return View(courses);
        }
        [HttpGet]
        [Authorize(Roles = "Instructor,Admin")]
        public IActionResult Create()
        {
            ViewBag.CategoryNames = _context.Categories.Select(
                c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }).ToList();

            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Instructor,Admin")]
        public async Task<IActionResult> Create(Course course)
        {
            if (ModelState.IsValid)
            {
                course.CreationDate = DateTime.Now;
                course.Instructor = await GetInstructorByUser();

                await _context.Courses.AddAsync(course);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(course);
        }

        public async Task<Instructor> GetInstructorByUser()
        {
            var currentUser = await userManager.GetUserAsync(User);
            var instrucor = _context.Instructors?.FirstOrDefault(i => i.Id == currentUser.InstructorId);
            return instrucor;
        }
    }
}
