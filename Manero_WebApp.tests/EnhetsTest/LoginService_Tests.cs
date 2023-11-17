using Manero_WebApp.Contexts;
using Manero_WebApp.Helpers.Services.AuthenticationServices;
using Manero_WebApp.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public class LoginServiceTests
{
    public static DataContext Context { get; set; }
    public static async Task<LoginService> Setup(UserEntity user)
    {
        // Arrange
        var serviceProvider = new ServiceCollection()
            .AddEntityFrameworkInMemoryDatabase()
            .BuildServiceProvider();

        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "in_memory_db")
            .UseInternalServiceProvider(serviceProvider)
            .Options;
        var userInDb = new UserEntity { Id = "1", UserName = "testuser", FullName = "Test User", Email = "testuser@example.com", PasswordHash = "123" };

        Context = new DataContext(options);
        Context.Users.Add(userInDb);
        await Context.SaveChangesAsync();
        var userManager = new UserManager<UserEntity>(new UserStore<UserEntity>(Context), null, null, null, null, null, null, null, null);
      

        var signInManagerMock = new Mock<SignInManager<UserEntity>>(
            userManager,
            Mock.Of<IHttpContextAccessor>(),
            Mock.Of<IUserClaimsPrincipalFactory<UserEntity>>(),
            null,
            null,
            null,
            null
        );
        signInManagerMock.Setup(s => s
        .PasswordSignInAsync(
            userInDb,
            It.IsAny<string>(),
            true,
            false))
        .ReturnsAsync((userInDb.Email == user.Email && userInDb.PasswordHash == user.PasswordHash) ? SignInResult.Success : SignInResult.Failed);
        var signInService = new LoginService(userManager, signInManagerMock.Object);

        return signInService;
    }
    [Fact]
    public async Task Login_MatchingCredentials_LoginSucess()
    {
        var logInUserCredentials = new UserEntity
        {
            Id = "1",
            UserName = "testuser",
            FullName = "Test User",
            Email = "testuser@example.com",
            PasswordHash = "123"
        };
        var service = Setup(logInUserCredentials);

        var model = new SignInViewModel() { Email = logInUserCredentials.Email, KeepMeSignedIn = true, Password = logInUserCredentials.PasswordHash };
        // Act

        var result = await (await service).LoginAsync(model);

        // Assert
        Assert.True(result);
    }
    [Fact]
    public async Task Login_NotMatchingCredentials_LoginFailed()
    {
        var logInUserCredentials = new UserEntity
        {
            Id = "2",
            UserName = "asd",
            FullName = "asd",
            Email = "asd@example.com",
            PasswordHash = "wrongpw"
        };

        var service = Setup(logInUserCredentials);
        var model = new SignInViewModel() { Email = logInUserCredentials.Email, KeepMeSignedIn = true, Password = logInUserCredentials.PasswordHash };

        // Act
        var result = await (await service).LoginAsync(model);

        // Assert
        Assert.False(result);
    }
}