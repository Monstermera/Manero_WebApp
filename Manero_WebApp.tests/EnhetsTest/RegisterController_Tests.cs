using Manero_WebApp.Contexts;
using Manero_WebApp.Controllers;
using Manero_WebApp.Helpers.Services.AuthenticationServices;
using Manero_WebApp.Helpers.Services.UserServices;
using Manero_WebApp.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
namespace Manero_WebApp.Tests.EnhetsTest;

public class RegisterControllerTests
{
    private readonly Mock<IRegisterService> _mockRegisterService;
    private readonly Mock<ICheckIfUserExistsService> _mockCheckIfUserExistsService;
    private readonly Mock<ILoginService> _mockLoginService;
    private readonly Mock<HttpContext> _mockHttpContext;
    private readonly RegisterController _controller;

    public RegisterControllerTests()
    {
        _mockHttpContext = new Mock<HttpContext>();
        _mockRegisterService = new Mock<IRegisterService>();
        _mockCheckIfUserExistsService = new Mock<ICheckIfUserExistsService>();
        _mockLoginService = new Mock<ILoginService>();

        _controller = new RegisterController(_mockRegisterService.Object, _mockCheckIfUserExistsService.Object, _mockLoginService.Object)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = _mockHttpContext.Object
            }
        };
    }

    [Fact]
    public async Task Index_Post_IfUserAlreadyExists_ReturnErrorMessage()
    {
        // Arrange
        var existingUserEmail = "existing@example.com";
        var model = new RegistrationViewModel { Email = existingUserEmail };

        _mockCheckIfUserExistsService
            .Setup(x => x.UserExistsAsync(It.IsAny<Expression<Func<UserEntity, bool>>>()))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Index(model) as ViewResult;

        // Assert
        Assert.NotNull(result);
        Assert.False(_controller.ModelState.IsValid);
        Assert.True(_controller.ModelState.ContainsKey(string.Empty));
        var errorMessage = _controller.ModelState[string.Empty]?.Errors[0].ErrorMessage;
        Assert.Equal("A user with the same email already exists", errorMessage);
        Assert.Equal(model, result.Model);
    }

    [Fact]
    public async Task Index_Post_ShouldReturnWithSuccessViewIfRegisterAsyncSucceeds()
    {
        // Arrange
        var model = new RegistrationViewModel { FullName = "name", Email = "test@test.se", Password = "asdf", ConfirmPassword = "asdf" };

        _mockCheckIfUserExistsService
            .Setup(x => x.UserExistsAsync(It.IsAny<Expression<Func<UserEntity, bool>>>()))
            .ReturnsAsync(false);

        _mockRegisterService
            .Setup(x => x.RegisterAsync(model))
            .ReturnsAsync(true);

        _mockLoginService
            .Setup(x => x.LoginAsync(It.IsAny<SignInViewModel>()))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Index(model) as RedirectToActionResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Success", result.ActionName);
    }
}
