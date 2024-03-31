using BlogPlatform.Dtos;
using BlogPlatform.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogPlatform.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorizationController(ILogger<AuthorizationController> logger, ILoginService loginService) : ControllerBase
{
    private readonly ILogger<AuthorizationController> _logger = logger;
    private readonly ILoginService _loginService = loginService;

    /// <summary>
    /// Get auth token after successful login.
    /// </summary>
    /// <response code="401">If incorrect password is provided.</response>
    /// <response code="404">If the User which was attempted to login doesn't exist.</response>
    [HttpPost(Name = nameof(Login))]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    [AllowAnonymous]
    public async Task<ActionResult<string>> Login(LoginDto data)
    {
        var token = await _loginService.Login(data);
        return Ok(token);
    }
}
