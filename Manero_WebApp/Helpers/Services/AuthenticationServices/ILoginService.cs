using Manero_WebApp.ViewModels.AccountViewModels;

namespace Manero_WebApp.Helpers.Services.AuthenticationServices;

public interface ILoginService
{
    Task<bool> LoginAsync(SignInViewModel model);
}
