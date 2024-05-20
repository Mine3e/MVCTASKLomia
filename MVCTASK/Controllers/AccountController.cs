using Business.Helpers.Account;
using Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCTASK.DTOs.AccountDto;

namespace MVCTASK.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _usermanager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _rolemanager;
        public AccountController(UserManager<User> usermanager, SignInManager<User> signInManager, RoleManager<IdentityRole> rolemanager)
        {
            _usermanager = usermanager;
            _signInManager = signInManager;
            _rolemanager = rolemanager;
        }

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult LogOut()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await _usermanager.FindByNameAsync(loginDto.UserNameOrEmail);
            if (user == null)
            {
                user = await _usermanager.FindByEmailAsync(loginDto.UserNameOrEmail);
                if (user == null)
                {
                    ModelState.AddModelError("", "UsernameOrEmail ve ya password dogru deyil");
                    return View();
                }
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, true);
            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "Birazdan yeniden cehd edin");
                return View();
            }
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "UsernameOrEmail ve ya password duzdun deyil");
                return View();
            }
            await _signInManager.SignInAsync(user, loginDto.IsRemembered);
            return RedirectToAction("Index", "Team", new { area = "Adminn" });
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            User user = new User()
            {
                Name = registerDto.Name,
                Surname = registerDto.Surname,
                Email = registerDto.Email,
                UserName = registerDto.Username,
            };
            var result = await _usermanager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View();
            }
            await _usermanager.AddToRoleAsync(user, UserRole.Admin.ToString());
            return RedirectToAction(nameof(Login));
        }
        public async Task<IActionResult>CreateRole()
        {
            foreach(var item in Enum.GetValues(typeof(UserRole)))
            {
                await _rolemanager.CreateAsync(new IdentityRole()
                {
                    Name = item.ToString(),
                });
              
            }
            return Ok();
        }


    }
}
