using Manero_WebApp.ViewModels.AccountViewModels;

namespace Manero_WebApp.Helpers.Services.AuthenticationServices;

public interface IRegisterService
{
    Task<bool> RegisterAsync(RegistrationViewModel model);
}
