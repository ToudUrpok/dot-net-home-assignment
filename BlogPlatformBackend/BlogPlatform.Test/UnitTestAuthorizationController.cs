using Moq;
using BlogPlatform.Services;
using BlogPlatform.Services.Exceptions;
using BlogPlatform.Dtos;
using BlogPlatform.WebApi.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

namespace BlogPlatform.Test;

public class UnitTestAuthorizationController
{
    private readonly Mock<ILoginService> _loginService = new();
    private readonly Mock<ILogger<AuthorizationController>> _logger = new();

    private static readonly LoginDto TEST_LOGIN_DTO = new() { Email = "test@email.com", Password = "123456" };

    [Fact]
    public async void LoginOk()
    {
        string testToken = "test_token_123456";
        _loginService.Setup(ls => ls.Login(TEST_LOGIN_DTO)).ReturnsAsync(testToken);
        var authorizationController = new AuthorizationController(_logger.Object, _loginService.Object);

        var actionResult = await authorizationController.Login(TEST_LOGIN_DTO);

        Assert.NotNull(actionResult.Result);
        var okResult = Assert.IsAssignableFrom<OkObjectResult>(actionResult.Result);
        Assert.True(okResult.StatusCode == 200);
        Assert.NotNull(okResult.Value);
        var resultValue = Assert.IsAssignableFrom<string>(okResult.Value);
        Assert.Equal(resultValue, testToken);
    }

    [Fact]
    public async void LoginEntityNotFoundException()
    {
        string errorMessage = $"The user with email={TEST_LOGIN_DTO.Email} is not found.";
        _loginService.Setup(ls => ls.Login(TEST_LOGIN_DTO)).ThrowsAsync(new EntityNotFoundException(errorMessage));
        var authorizationController = new AuthorizationController(_logger.Object, _loginService.Object);

        try
        {
            await authorizationController.Login(TEST_LOGIN_DTO);
        }
        catch (EntityNotFoundException ex)
        {
            var entityNotFoundException = Assert.IsAssignableFrom<EntityNotFoundException>(ex);
            Assert.True(entityNotFoundException.Message == errorMessage);
        }
    }

    [Fact]
    public async void LoginUserLoginException()
    {
        string errorMessage = "Invalid password.";
        _loginService.Setup(ls => ls.Login(TEST_LOGIN_DTO)).ThrowsAsync(new UserLoginException(errorMessage));
        var authorizationController = new AuthorizationController(_logger.Object, _loginService.Object);

        try
        {
            await authorizationController.Login(TEST_LOGIN_DTO);
        }
        catch (UserLoginException ex)
        {
            var userLoginException = Assert.IsAssignableFrom<UserLoginException>(ex);
            Assert.True(userLoginException.Message == errorMessage);
        }
    }
}
