using RunGroopApp.Helpers.Response;
using RunGroopApp.ViewModels;

namespace RunGroopApp.Interfaces
{
    public interface IUserService
    {
        public Task<UserManagerResponse> Register(RegisterViewModel registerViewModel);

        public Task<UserManagerResponse> Login(LoginViewModel loginViewModel);

        public Task<UserManagerResponse> Logout();
    }
}
