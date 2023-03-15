using Microsoft.AspNetCore.Identity;

namespace CourseManagmentSystem.Models
{
    public class ApplicationUser:IdentityUser
    {
        public int? InstructorId { get; set; }
        public int? StudentId { get; set; }
    }
}
