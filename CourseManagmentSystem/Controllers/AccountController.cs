﻿using CourseManagmentSystem.Data;
using CourseManagmentSystem.Models;
using CourseManagmentSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Xml.Linq;

namespace CourseManagmentSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ApplicationDbContext context;

        public AccountController(UserManager<ApplicationUser> userManager,
                                SignInManager<ApplicationUser> signInManager,
                                ApplicationDbContext context,
                                RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.context = context;
            this.roleManager = roleManager;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {

            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Creating new Student

                var user = new ApplicationUser { UserName = model.UserName, Email = model.Email };

                // Create new User with secured password
                var result = await userManager.CreateAsync(user, model.Password);

                // Auto sign-in after registeration
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                // View list of errors
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            // If the model not valid return the same view with the same model with its values
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> RegisterAsInstructor()
        {
            // get the current user signed in.
            var currentUser = await userManager.GetUserAsync(User);
            var currentInstructor = GetInstructorByUser();
            // create new instance of RegisterAsInstructorViewModel 
            RegisterAsInstructorViewModel model = new RegisterAsInstructorViewModel
            {
                Id = currentUser.Id,
                Email = currentUser.Email,
                
            };
            if (currentInstructor != null)
            {
                model.Description = currentUser.Instructor?.Description;
                model.Website = currentUser.Instructor?.Website;
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterAsInstructor(RegisterAsInstructorViewModel model, IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var profilePic = new byte[file.Length];
                using (var reader = new BinaryReader(file.OpenReadStream()))
                {
                    profilePic = reader.ReadBytes((int)file.Length);
                }
                model.ProfilePic = profilePic;
            }
            if (ModelState.IsValid)
            {
                var currentUser = await userManager.GetUserAsync(User);
                // Save the model to the database using Entity Framework or any other ORM of your choice
                var instructor = new Instructor
                {
                    Description = model.Description,
                    Website = model.Website,
                    ProfilePic = model.ProfilePic,
                    IsRequested = true,
                    User = currentUser
                };
                //currentUser.Instructor= instructor;
                await context.Instructors.AddAsync(instructor);
                await context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? id)
        {


            // Check if the model state is valid.
            if (ModelState.IsValid)
            {
                // Attempt to sign in the user using the SignInManager's PasswordSignInAsync method.
                var result = await signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);

                // If the sign in attempt was successful, redirect the user to the home page.
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(id))
                    {
                        string cleanUrl = id.Replace("%2F", "/");

                        return LocalRedirect(cleanUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }

                }

                // If the sign in attempt failed, add an error to the model state indicating an invalid login attempt.
                ModelState.AddModelError("", "Invalid Login Attempt");
            }

            // Return the view for the login page, passing in the current model.

            return View(model);
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }


        public async Task<Instructor> GetInstructorByUser()
        {
            var currentUser = await userManager.GetUserAsync(User);
            var instrucor = context.Instructors?.FirstOrDefault(i => i.Id == currentUser.InstructorId);
            return instrucor;
        }

    }
}
