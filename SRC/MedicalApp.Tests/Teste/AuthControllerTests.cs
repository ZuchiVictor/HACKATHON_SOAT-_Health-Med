using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Threading.Tasks;
using Xunit;
using MedicalApp.Application;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;
public class AuthControllerTests
{
    private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
    private readonly Mock<SignInManager<ApplicationUser>> _signInManagerMock;
    private readonly Mock<IConfiguration> _configurationMock;
    private readonly AuthController _authController;

    public AuthControllerTests()
    {
        var store = new Mock<IUserStore<ApplicationUser>>();
        _userManagerMock = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);
        var contextAccessor = new Mock<IHttpContextAccessor>();
        var claimsFactory = new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>();
        _signInManagerMock = new Mock<SignInManager<ApplicationUser>>(_userManagerMock.Object, contextAccessor.Object, claimsFactory.Object, null, null, null, null);
        _configurationMock = new Mock<IConfiguration>();

        _authController = new AuthController(_userManagerMock.Object, _signInManagerMock.Object, _configurationMock.Object);
    }

    [Fact]
    public async Task Register_ReturnsOkResult_WhenRegistrationIsSuccessful()
    {
        // Arrange
        var registerRequest = new RegisterRequest { Username = "testuser", Email = "test@example.com", Password = "Password123!", FullName = "Test User" };
        _userManagerMock.Setup(um => um.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);

        // Act
        var result = await _authController.Register(registerRequest);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);
    }

    [Fact]
    public async Task Login_ReturnsOkResult_WhenLoginIsSuccessful()
    {
        // Arrange
        var loginRequest = new LoginRequest { Username = "testuser", Password = "Password123!" };
        _signInManagerMock.Setup(sm => sm.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>())).ReturnsAsync(SignInResult.Success);
        _userManagerMock.Setup(um => um.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(new ApplicationUser { UserName = "testuser" });

        // Act
        var result = await _authController.Login(loginRequest);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);
    }
}
