using Moq;
using BlogPlatform.Services;
using BlogPlatform.Dtos;
using BlogPlatform.WebApi.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

namespace BlogPlatform.Test;

public class UnitTestAuthorizationController
{
    private readonly Mock<ILoginService> _loginService = new();
    private readonly Mock<ILogger<AuthorizationController>> _logger = new();

    [Fact]
    public async void LoginOk()
    {
        LoginDto testData = new() { UserName = "test", Password = "test" };
        string testToken = "test_token_123456";
        _loginService.Setup(ls => ls.Login(testData)).ReturnsAsync(testToken);
        var authorizationController = new AuthorizationController(_logger.Object, _loginService.Object);

        var actionResult = await authorizationController.Login(testData);

        Assert.NotNull(actionResult.Result);
        var okResult = Assert.IsAssignableFrom<OkObjectResult>(actionResult.Result);
        Assert.True(okResult.StatusCode == 200);
        Assert.NotNull(okResult.Value);
        var resultValue = Assert.IsAssignableFrom<string>(okResult.Value);
        Assert.Equal(resultValue, testToken);
    }

    [Fact]
    public async void LoginBadRequest()
    {
        LoginDto testData = new();
        string? testToken = null;
        _loginService.Setup(ls => ls.Login(testData)).ReturnsAsync(testToken);
        var authorizationController = new AuthorizationController(_logger.Object, _loginService.Object);

        var actionResult = await authorizationController.Login(testData);

        Assert.NotNull(actionResult.Result);
        var badRequestResult = Assert.IsAssignableFrom<BadRequestResult>(actionResult.Result);
        Assert.True(badRequestResult.StatusCode == 400);
    }
}
