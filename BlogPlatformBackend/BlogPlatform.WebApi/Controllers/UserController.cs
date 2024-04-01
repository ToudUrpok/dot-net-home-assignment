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
