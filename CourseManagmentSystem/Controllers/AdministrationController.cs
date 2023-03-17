using CourseManagmentSystem.Models;
using CourseManagmentSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.AccessControl;

namespace CourseManagmentSystem.Controllers
{
    
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public AdministrationController(RoleManager<IdentityRole> roleManager,
                                        UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole()
                {
                    Name = model.RoleName
                };

                var identityResult = await roleManager.CreateAsync(identityRole);
                if (identityResult.Succeeded)
                {
                    return RedirectToAction("ListRole", "Administration");
                }
                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult ListRoles()
        {
            var Roles = roleManager.Roles.ToList();
            return View(Roles);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var Role = await roleManager.FindByIdAsync(id);
            if (Role == null)
            {
                ViewBag.ErrorMessage = $"Role with id ={id} cannot be found";
                return View("NotFound");
            }
            else
            {
                // New instance of EditRoleView Model
                var model = new EditRoleViewModel
                {
                    Id = Role.Id,
                    RoleName = Role.Name,
                };
                var usersInRole = await userManager.GetUsersInRoleAsync(Role.Name);
                foreach (var user in usersInRole)
                {
                    model.Users.Add(user.UserName);
                }
                return View(model);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var Role = await roleManager.FindByIdAsync(model.Id);
            if (Role == null)
            {
                ViewBag.ErrorMessage = $"Role with id ={model.Id} cannot be found";
                return View("NotFound");
            }
            else
            {
                Role.Name = model.RoleName;
                var result =await roleManager.UpdateAsync(Role);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string id)
        {
            ViewBag.RoleId = id;
            var Role = await roleManager.FindByIdAsync(id);
            if (Role == null)
            {
                ViewBag.ErrorMessage = $"Role with id ={id} cannot be found";
                return View("NotFound");
            }
            var model = new List<UserRoleViewModel>();
            foreach (var user in userManager.Users)
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };

                if (await userManager.IsInRoleAsync(user, Role.Name))
                {
                    userRoleViewModel.IsSelected= true; 
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }
                model.Add(userRoleViewModel);
            }
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost] 
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string id) 
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role == null) 
            {
                ViewBag.ErrorMessage = $"Role with id ={id} cannot be found"; 
                return View("NotFound");
            }
            for (int i = 0; i < model.Count; i++) 
            {
                var user = await userManager.FindByIdAsync(model[i].UserId); // retrieves the user with the specified id using the userManager object
                IdentityResult result; 
                if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name))) // checks if the user is selected and not already in the role
                {
                    result = await userManager.AddToRoleAsync(user, role.Name); // adds the user to the role 
                }
                else if (!model[i].IsSelected && (await userManager.IsInRoleAsync(user, role.Name))) // checks if the user is not selected and already in the role
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name); // removes the user from the role 
                }
                else
                {
                    continue; // skip
                }
                if (result.Succeeded) // checks if the operation was successful
                {
                    if (i < model.Count - 1)
                        continue; // skip
                    else
                        return RedirectToAction("EditRole", new { Id = id }); // redirects to the EditRole action with the specified id
                }
            }
            return RedirectToAction("EditRole", new { Id = id }); // redirects to the EditRole action with the specified id
        }

    }
}
