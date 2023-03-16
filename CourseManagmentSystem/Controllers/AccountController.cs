using CourseManagmentSystem.Models;
using CourseManagmentSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagmentSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager,
                                SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
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
                
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };

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
        public IActionResult RegisterAsInstructor()
        {
            return View();
        }

        //[HttpPost]
        //[AllowAnonymous]
        //public async Task<IActionResult> RegisterAsInstructor()
        //{

        //    return null;
        //}

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {     
            // Check if the model state is valid.
            if (ModelState.IsValid)
            {
                // Attempt to sign in the user using the SignInManager's PasswordSignInAsync method.
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                // If the sign in attempt was successful, redirect the user to the home page.
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        returnUrl = Url.Content("~/");
                        return LocalRedirect(returnUrl);
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

    }
}
