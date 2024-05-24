using ExamCode.Core.Models;
using ExamCode.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExamCode.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        private readonly SignInManager<AppUser> _signInManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login(AdminLoginVm vm)
        {
            if (!ModelState.IsValid)
                return View();

            AppUser admin = await _userManager.FindByNameAsync(vm.UserName);

            if (admin == null)
            {
                ModelState.AddModelError("", "UserName or Password is not valid!");
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(admin, vm.Password, vm.IsPersistent, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "UserName or Password is not valid!");
                return View();
            }


            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login","User");
        }
    }
}
