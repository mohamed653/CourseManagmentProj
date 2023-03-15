using System.ComponentModel.DataAnnotations;

namespace CourseManagmentSystem.Models
{
    public class Instructor
    {
        [Key]
        public int Id { get; set; }
        public string? Description { get; set; }
        public string? Website { get; set; }
        public string? ProfilePicUrl { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual ICollection< Course>? Courses{ get; set; }

    }
}
