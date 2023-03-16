using CourseManagmentSystem.Models;

namespace CourseManagmentSystem.ViewModels
{
    public class CoursesViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreationDate { get; set; }
        public string? Description { get; set; }
        public virtual Category Category { get; set; }
        public virtual Instructor? Instructor { get; set; }
    }
}
