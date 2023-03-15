using Microsoft.Build.Framework;

namespace CourseManagmentSystem.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int? ParentId { get; set; }
        public Category? Parent { get; set; }
        public virtual ICollection<Category>? Children { get; set; }
        public virtual ICollection<Course>? Courses{ get; set; }

    }
}
