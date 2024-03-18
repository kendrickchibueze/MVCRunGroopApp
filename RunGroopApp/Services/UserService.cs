using Microsoft.AspNetCore.Identity;
using RunGroopApp.Data;
using RunGroopApp.Helpers.Response;
using RunGroopApp.Interfaces;
using RunGroopApp.Models;
using RunGroopApp.ViewModels;

namespace RunGroopApp.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(UserManager<AppUser> userManager,
             RoleManager<IdentityRole> roleManager,
            SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;

        }

        public async Task<UserManagerResponse> Login(LoginViewModel loginViewModel)
        {
            var user = await _userManager.FindByEmailAsync(loginViewModel.EmailAddress);
            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                    if (result.Succeeded)
                    {
                        return new UserManagerResponse
                        {
                            IsSuccess = true,
                            Message = "User successfully loggedIn"
                        };
                    }
                    return new UserManagerResponse
                    {
                        IsSuccess = false,
                        Message = "Wrong credentials.Please try again"
                    };
                }
            }
            return new UserManagerResponse
            {
                IsSuccess = false,
                Message = "User not found"
            };
        }

        public async Task<UserManagerResponse> Register(RegisterViewModel registerViewModel)
        {
            var user = await _userManager.FindByEmailAsync(registerViewModel.EmailAddress);
            if(user !=null)
            {
                return new UserManagerResponse
                {
                    IsSuccess = false,
                    Message = "This Email Address is already in use"
                };
            }
            var newUser = new AppUser()
            {
                Email = registerViewModel.EmailAddress,
                UserName = registerViewModel.EmailAddress
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, registerViewModel.Password);
            if(newUserResponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
            }
            return new UserManagerResponse
            {
                IsSuccess = true,
                Message = "Registeration Completed!"
            };
        }
        public async Task<UserManagerResponse> Logout()
        {
            await _signInManager.SignOutAsync();
            return new UserManagerResponse
            {
                IsSuccess = true,
                Message = "User Logged Out Successfully!"
            };
        }

    }
}
