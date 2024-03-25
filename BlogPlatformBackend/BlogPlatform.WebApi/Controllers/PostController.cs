using BlogPlatform.Dtos;
using BlogPlatform.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlogPlatform.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PostController(ILogger<PostController> logger, IPostsService postsService) : ControllerBase
{
    private readonly ILogger<PostController> _logger = logger;
    private readonly IPostsService _postsService = postsService;

    [HttpGet(Name = nameof(GetPosts))]
    public async Task<ActionResult<List<PostDto>>> GetPosts()
    {
        var posts = await _postsService.GetPostsAsync();
        if (posts is null) return BadRequest();

        return posts.Count > 0 ? Ok(posts) : NotFound();
    }

    [HttpGet("{id}", Name = nameof(GetPost))]
    public async Task<ActionResult<PostDto>> GetPost(long id)
    {
        var post = await _postsService.GetPostAsync(id);

        return post is null ? NotFound() : Ok(post);
    }

    [HttpPost(Name = nameof(CreatePost))]
    public async Task<ActionResult<PostDto>> CreatePost(CreatePostDto data)
    {
        var post = await _postsService.CreatePostAsync(data);

        return post is null ? BadRequest() : CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);
    }

    [HttpPut("{id}", Name = nameof(UpdatePost))]
    public async Task<IActionResult> UpdatePost(long id, UpdatePostDto post)
    {
        if (id != post.Id)
        {
            return BadRequest();
        }

        var result = await _postsService.UpdatePostAsync(post);

        return result ? NoContent() : BadRequest();
    }

    [HttpDelete("{id}", Name = nameof(DeletePost))]
    public async Task<IActionResult> DeletePost(long id)
    {
        var result = await _postsService.DeletePostAsync(id);

        return result ? NoContent() : BadRequest();
    }
}
