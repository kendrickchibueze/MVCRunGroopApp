using Microsoft.AspNetCore.Mvc;
using RunGroopApp.Interfaces;
using RunGroopApp.ViewModels;

namespace RunGroopApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }
            var userManagerResponse = await _userService.Login(loginViewModel);
            if (userManagerResponse.IsSuccess)
            {
                return RedirectToAction("Index", "Races");
            }
            else
            {
                TempData["Error"] = userManagerResponse.Message;
                return View(loginViewModel);
            }
        }

        public IActionResult Register()
        {
            var response = new RegisterViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(registerViewModel);
            }
            var userManagerResponse = await _userService.Register(registerViewModel);
            if (userManagerResponse.IsSuccess)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                TempData["Error"] = userManagerResponse.Message;
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            var userManagerResponse = await _userService.Logout();
            if(userManagerResponse.IsSuccess)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Races");
        }

    }
}
