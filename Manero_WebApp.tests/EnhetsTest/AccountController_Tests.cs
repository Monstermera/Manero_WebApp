using Manero_WebApp.Controllers;
using Manero_WebApp.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

public class AccountControllerTests
{
    [Fact]
    public async Task Index_ReturnsProfileView_WhenUserIsSignedIn()
    {
        // Arrange
        var user = new UserEntity
        {
            Id = "1",
            UserName = "testuser",
            FullName = "Test User",
            Email = "testuser@example.com"
        };

        var userManagerMock = new Mock<UserManager<UserEntity>>(
            Mock.Of<IUserStore<UserEntity>>(),
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null
        );

        userManagerMock.Setup(u => u.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user);

        var signInManagerMock = new Mock<SignInManager<UserEntity>>(
            userManagerMock.Object,
            Mock.Of<IHttpContextAccessor>(),
            Mock.Of<IUserClaimsPrincipalFactory<UserEntity>>(),
            null,
            null,
            null,
            null
        );

        signInManagerMock.Setup(s => s.IsSignedIn(It.IsAny<ClaimsPrincipal>())).Returns(true);

        var controller = new AccountController(signInManagerMock.Object, userManagerMock.Object, null);

        // Act
        var result = await controller.Index();

        // Assert
        Assert.IsType<ViewResult>(result);
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.NotNull(viewResult);

        // Check if ViewData is set
        if (viewResult.ViewData != null)
        {
            // Asserts based on the View
            Assert.Contains("Test User", viewResult.ViewData["FullName"]?.ToString());
            Assert.Contains("testuser@example.com", viewResult.ViewData["Email"]?.ToString());
        }


    }
}
