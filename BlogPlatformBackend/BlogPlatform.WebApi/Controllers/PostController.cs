using BlogPlatform.Dtos;
using BlogPlatform.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogPlatform.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class PostController(ILogger<PostController> logger, IPostsService postsService) : ControllerBase
{
    private readonly ILogger<PostController> _logger = logger;
    private readonly IPostsService _postsService = postsService;

    /// <summary>
    /// Gets all Posts.
    /// </summary>
    [HttpGet(Name = nameof(GetPosts))]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<PostDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<PostDto>>> GetPosts()
    {
        var posts = await _postsService.GetPostsAsync();
        return Ok(posts);
    }

    /// <summary>
    /// Gets the Post with specified id.
    /// </summary>
    [HttpGet("{id}", Name = nameof(GetPost))]
    [AllowAnonymous]
    [ProducesResponseType(typeof(PostDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PostDto>> GetPost(long id)
    {
        var post = await _postsService.GetPostAsync(id);
        return Ok(post);
    }

    /// <summary>
    /// Creates the Post.
    /// </summary>
    [HttpPost(Name = nameof(CreatePost))]
    [ProducesResponseType(typeof(PostDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<PostDto>> CreatePost(CreatePostDto data)
    {
        var post = await _postsService.CreatePostAsync(data);
        return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);
    }

    /// <summary>
    /// Updates the Post.
    /// </summary>
    [HttpPut(Name = nameof(UpdatePost))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdatePost(UpdatePostDto post)
    {
        await _postsService.UpdatePostAsync(post);
        return NoContent();
    }

    /// <summary>
    /// Deletes the Post by specified id.
    /// </summary>
    [HttpDelete("{id}", Name = nameof(DeletePost))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletePost(long id)
    {
        await _postsService.DeletePostAsync(id);
        return NoContent();
    }
}
