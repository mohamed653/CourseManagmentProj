using Microsoft.AspNetCore.Identity;

namespace CourseManagmentSystem.Models
{
    public class ApplicationUser:IdentityUser
    {
        public int? InstructorId { get; set; }
        public virtual ICollection<Course>? Courses { get; set; }
    }
}
