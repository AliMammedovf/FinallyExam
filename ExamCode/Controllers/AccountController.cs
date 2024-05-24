using ExamCode.Core.Models;
using ExamCode.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExamCode.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterVm vm)
        {

            if (!ModelState.IsValid)
                return View();

            AppUser user = null;

            user= await _userManager.FindByNameAsync(vm.UserName);

            if (user is not null)
            {
                ModelState.AddModelError("UserName", "Username alraedy exsist!");
                return View();
            }

            user= await _userManager.FindByEmailAsync(vm.Email);

            if (user is not null)
            {
                ModelState.AddModelError("Email", "Email alraedy exsist!");
                return View();
            }

            user = new AppUser()
            {
                UserName = vm.UserName,
                Email = vm.Email
            };

            var result= await _userManager.CreateAsync(user, vm.Password);

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                    return View();
                }
            }

            await _userManager.AddToRoleAsync(user, "Member");

            return RedirectToAction("Login","User");
        }
    }
}
