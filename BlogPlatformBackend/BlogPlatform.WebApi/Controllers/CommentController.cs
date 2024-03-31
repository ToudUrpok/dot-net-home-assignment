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

    /// <summary>
    /// Gets the Comment with specified id.
    /// </summary>
    [HttpGet("{id}", Name = nameof(GetComment))]
    [AllowAnonymous]
    [ProducesResponseType(typeof(CommentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CommentDto>> GetComment(long id)
    {
        var comment = await _commentsService.GetCommentAsync(id);
        return Ok(comment);
    }

    /// <summary>
    /// Creates the Comment.
    /// </summary>
    /// <response code="400">If attempt to add the Comment to not existing Post.</response>
    [HttpPost(Name = nameof(CreateComment))]
    [ProducesResponseType(typeof(CommentDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CommentDto>> CreateComment(CreateCommentDto data)
    {
        var comment = await _commentsService.CreateCommentAsync(data);
        return CreatedAtAction(nameof(GetComment), new { id = comment.Id }, comment);
    }

    /// <summary>
    /// Updates the Comment.
    /// </summary>
    [HttpPut(Name = nameof(UpdateComment))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateComment(CommentDto data)
    {
        await _commentsService.UpdateCommentAsync(data);
        return NoContent();
    }

    /// <summary>
    /// Deletes the Comment by specified id.
    /// </summary>
    [HttpDelete("{id}", Name = nameof(DeleteComment))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteComment(long id)
    {
        await _commentsService.DeleteCommentAsync(id);
        return NoContent();
    }
}
