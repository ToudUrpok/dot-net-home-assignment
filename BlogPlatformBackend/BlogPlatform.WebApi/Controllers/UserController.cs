using BlogPlatform.Dtos;
using BlogPlatform.Services;
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

    /// <summary>
    /// Gets the data for authorized User (by email from claim data of auth token).
    /// </summary>
    /// <response code="401">If unauthorized attempt to get User data.</response>
    /// <response code="404">If failed to find User by auth data (email).</response>
    [HttpGet(Name = nameof(GetUser))]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserDto>> GetUser()
    {
        if (HttpContext.User.Identity is not ClaimsIdentity identity) return Unauthorized();

        var email = identity.FindFirst(ClaimTypes.Email)?.Value;
        if (email is null) return Unauthorized();

        var user = await _usersService.GetUserByEmailAsync(email);
        return Ok(user);
    }

    /// <summary>
    /// Gets the User by specified id.
    /// </summary>
    [HttpGet("{id}", Name = nameof(GetUserById))]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserDto>> GetUserById(int id)
    {
        var user = await _usersService.GetUserAsync(id);
        return Ok(user);
    }

    /// <summary>
    /// Creates the User.
    /// </summary>
    /// <response code="409">If attempt to add the User with the email which is already in use by another user.</response>
    [HttpPost(Name = nameof(CreateUser))]
    [AllowAnonymous]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<UserDto>> CreateUser(CreateUserDto data)
    {
        var user = await _usersService.CreateUserAsync(data);
        return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
    }

    /// <summary>
    /// Updates the User.
    /// </summary>
    [HttpPut(Name = nameof(UpdateUser))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateUser(UpdateUserDto data)
    {
        await _usersService.UpdateUserAsync(data);
        return NoContent();
    }
}
