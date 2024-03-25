using BlogPlatform.Dtos;
using BlogPlatform.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogPlatform.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorizationController(ILogger<CommentController> logger, ILoginService loginService) : ControllerBase
{
    private readonly ILogger<CommentController> _logger = logger;
    private readonly ILoginService _loginService = loginService;

    [HttpPost(Name = nameof(Login))]
    [AllowAnonymous]
    public async Task<ActionResult<string>> Login(LoginDto data)
    {
        var token = await _loginService.Login(data);

        return string.IsNullOrEmpty(token) ? BadRequest() : Ok(token);
    }
}
