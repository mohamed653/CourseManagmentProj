using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseManagmentSystem.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        public string? Language { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
