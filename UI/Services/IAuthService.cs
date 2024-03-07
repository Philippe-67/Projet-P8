using UI.Models;

namespace UI.Services
{
    public interface IAuthService
    {
        Task<AuthStatus> RegisterAsync(Register register);
        Task<AuthStatus> LoginAsync(Login loging);
        Task LogoutAsync();
    }
}
