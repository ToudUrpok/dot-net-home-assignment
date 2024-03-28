using BlogPlatform.Dtos;
using BlogPlatform.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogPlatform.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class UserController(ILogger<UserController> logger, IUsersService usersService) : ControllerBase
{
    private readonly ILogger<UserController> _logger = logger;
    private readonly IUsersService _usersService = usersService;

    [HttpGet(Name = nameof(GetUser))]
    public async Task<ActionResult<UserDto>> GetUser()
    {
        if (HttpContext.User.Identity is not ClaimsIdentity identity) return Unauthorized();

        var email = identity.FindFirst(ClaimTypes.Email)?.Value;
        if (email is null) return Unauthorized();

        var user = await _usersService.GetUserByEmailAsync(email);

        return user is null ? NotFound() : Ok(user);
    }

    [HttpGet("{id}", Name = nameof(GetUserById))]
    public async Task<ActionResult<UserDto>> GetUserById(int id)
    {
        var user = await _usersService.GetUserAsync(id);

        return user is null ? NotFound() : Ok(user);
    }

    [HttpPost(Name = nameof(CreateUser))]
    [AllowAnonymous]
    public async Task<ActionResult<UserDto>> CreateUser(CreateUserDto data)
    {
        var user = await _usersService.CreateUserAsync(data);

        return user is null ? BadRequest() : CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
    }

    [HttpPut("{id}", Name = nameof(UpdateUser))]
    public async Task<IActionResult> UpdateUser(long id, UpdateUserDto data)
    {
        var result = await _usersService.UpdateUserAsync(data);

        return result ? NoContent() : BadRequest();
    }
}
