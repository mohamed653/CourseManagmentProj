﻿using System.ComponentModel.DataAnnotations;

namespace CourseManagmentSystem.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name ="User Name")]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Your password and confirmation password do not match")]
        public string ConfirmPassword { get; set; }
    }
}
