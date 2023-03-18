using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CourseManagmentSystem.ViewModels
{
    public class RegisterAsInstructorViewModel
    {
        public  string Id { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name ="Confirmed Email")]
        public string? Email { get; set; }
        [Required]
        public string? Name { get; set; }
       
        [Display(Name = "Give us description about yourself, skills and experience")]
        public string? Description { get; set; }
        [Display(Name = "Give us website/ social media link.")]
        public string? Website { get; set; }

        [Display(Name = "Select file:")]
        public byte[]? ProfilePic { get; set; }


    }
}
