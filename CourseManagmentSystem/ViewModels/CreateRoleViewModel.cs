using System.ComponentModel.DataAnnotations;

namespace CourseManagmentSystem.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}
