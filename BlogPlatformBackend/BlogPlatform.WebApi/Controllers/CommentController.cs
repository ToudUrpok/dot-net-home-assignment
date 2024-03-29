using BlogPlatform.Dtos;
using BlogPlatform.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogPlatform.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class CommentController(ILogger<CommentController> logger, ICommentsService commentsService) : ControllerBase
{
    private readonly ILogger<CommentController> _logger = logger;
    private readonly ICommentsService _commentsService = commentsService;

    [HttpGet("{id}", Name = nameof(GetComment))]
    [AllowAnonymous]
    public async Task<ActionResult<CommentDto>> GetComment(long id)
    {
        var comment = await _commentsService.GetCommentAsync(id);

        return comment is null ? NotFound() : Ok(comment);
    }

    [HttpPost(Name = nameof(CreateComment))]
    public async Task<ActionResult<CommentDto>> CreateComment(CreateCommentDto data)
    {
        var comment = await _commentsService.CreateCommentAsync(data);

        return comment is null ? BadRequest() : CreatedAtAction(nameof(GetComment), new { id = comment.Id }, comment);
    }

    [HttpPut(Name = nameof(UpdateComment))]
    public async Task<IActionResult> UpdateComment(CommentDto data)
    {
        var result = await _commentsService.UpdateCommentAsync(data);

        return result ? NoContent() : BadRequest();
    }

    [HttpDelete("{id}", Name = nameof(DeleteComment))]
    public async Task<IActionResult> DeleteComment(long id)
    {
        var result = await _commentsService.DeleteCommentAsync(id);

        return result ? NoContent() : BadRequest();
    }
}
