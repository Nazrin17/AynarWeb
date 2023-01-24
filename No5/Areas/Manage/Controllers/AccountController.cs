using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using No5.Dtos.User;
using No5.Models;

namespace No5.Areas.Manage.Controllers
{
    [Area("manage")]
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        //public async Task<IActionResult> Index()
        //{
        //    AppUser user = new AppUser
        //    {
        //        UserName = "Admin",
        //    };
        //   await _userManager.CreateAsync(user, "Admin123@");
        //    await _userManager.AddToRoleAsync(user, "Admin");
        //    return Json("ok");
        //}
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto loginDto)
        {
            AppUser user = await _userManager.FindByNameAsync(loginDto.UserName);
            if (user == null)
            {
                return NotFound();
            }
            var result = await _signInManager.PasswordSignInAsync(user, loginDto.Password, true, true);
            if (!result.Succeeded) return Json("error");
            return RedirectToAction("Index", "Employee");
        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
