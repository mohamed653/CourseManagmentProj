using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseManagmentSystem.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        [Display(Name="Course Name")]
        public string Name { get; set; } = null!;
        public DateTime CreationDate { get; set; }
        public string? Description { get; set; }

        [Required]
        public int CategoryId { get;set; } 
        public virtual Category Category { get; set; }
        public int? InstructorId { get; set; }
        public virtual Instructor? Instructor { get; set; }

        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }
        public virtual ICollection<CourseLesson> CourseLessons{ get; set; }
    }
}
