using CourseManagmentSystem.Models;
using CourseManagmentSystem.ViewModels;
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
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
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
        [HttpGet]
        public IActionResult ListRoles()
        {
            var Roles = roleManager.Roles.ToList();
            return View(Roles);
        }

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
        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel>model,string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with id ={id} cannot be found";
                return View("NotFound");
            }
            for (int i = 0; i < model.Count; i++)
            {
                var user = await userManager.FindByIdAsync(model[i].UserId);
                IdentityResult result;
                if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user,role.Name)))
                {
                   result = await  userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected &&(await userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }
                if (result.Succeeded)
                {
                    if (i < model.Count - 1)
                        continue;
                    else
                        return RedirectToAction("EditRole", new { Id = id });
                }
            }

            return RedirectToAction("EditRole", new { Id = id });
        }

    }
}
