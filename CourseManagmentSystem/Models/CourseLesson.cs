using MessagePack;
using Microsoft.Build.Framework;

namespace CourseManagmentSystem.Models
{
    public class CourseLesson
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        public string? VideoUrl { get; set; }

        public int? OrderNumber { get; set; }

        public int CourseId { get; set; }
        public virtual Course Course { get; set; }

    }
}
