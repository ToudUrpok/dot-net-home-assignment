using Moq;
using BlogPlatform.Services;
using BlogPlatform.Dtos;
using BlogPlatform.WebApi.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using BlogPlatform.Services.Exceptions;
using BlogPlatform.Data.Entities;

namespace BlogPlatform.Test;

public class UnitTestCommentController
{
    private readonly Mock<ICommentsService> _commentsService = new();
    private readonly Mock<ILogger<CommentController>> _logger = new();

    private readonly CommentDto TEST_COMMENT_DTO = new() { Id = 2, Text = "Test Comment" };

    [Fact]
    public async void GetCommentOk()
    {
        _commentsService.Setup(ps => ps.GetCommentAsync(TEST_COMMENT_DTO.Id)).ReturnsAsync(TEST_COMMENT_DTO);
        var commentController = new CommentController(_logger.Object, _commentsService.Object);

        var actionResult = await commentController.GetComment(2);

        Assert.NotNull(actionResult.Result);
        var okResult = Assert.IsAssignableFrom<OkObjectResult>(actionResult.Result);
        Assert.True(okResult.StatusCode == 200);
        Assert.NotNull(okResult.Value);
        var resultValue = Assert.IsAssignableFrom<CommentDto>(okResult.Value);
        Assert.Equal(TEST_COMMENT_DTO.Id, resultValue.Id);
        Assert.True(TEST_COMMENT_DTO.Equals(resultValue));
    }

    [Fact]
    public async void GetCommentEntityNotFoundException()
    {
        string errorMessage = $"The comment with id={TEST_COMMENT_DTO.Id} is not found.";
        _commentsService.Setup(ps => ps.GetCommentAsync(TEST_COMMENT_DTO.Id)).ThrowsAsync(new EntityNotFoundException(errorMessage));
        var commentController = new CommentController(_logger.Object, _commentsService.Object);

        try
        {
            await commentController.GetComment(TEST_COMMENT_DTO.Id);
        }
        catch (EntityNotFoundException ex)
        {
            var entityNotFoundException = Assert.IsAssignableFrom<EntityNotFoundException>(ex);
            Assert.True(entityNotFoundException.Message == errorMessage);
        }
    }

    [Fact]
    public async void CreateCommentCreated()
    {
        CreateCommentDto testData = new() { PostId = TEST_COMMENT_DTO.Id, Text = TEST_COMMENT_DTO.Text };
        _commentsService.Setup(ps => ps.CreateCommentAsync(testData)).ReturnsAsync(TEST_COMMENT_DTO);
        var commentController = new CommentController(_logger.Object, _commentsService.Object);

        var actionResult = await commentController.CreateComment(testData);

        Assert.NotNull(actionResult.Result);
        var createdAtActionResult = Assert.IsAssignableFrom<CreatedAtActionResult>(actionResult.Result);
        Assert.True(createdAtActionResult.StatusCode == 201);
        object? routeValue = null;
        Assert.True(createdAtActionResult.RouteValues?.TryGetValue("id", out routeValue));
        Assert.True(routeValue is not null && (long)routeValue == TEST_COMMENT_DTO.Id);
        Assert.True(createdAtActionResult.ActionName == nameof(commentController.GetComment));
        Assert.NotNull(createdAtActionResult.Value);
        var resultValue = Assert.IsAssignableFrom<CommentDto>(createdAtActionResult.Value);
        Assert.Equal(TEST_COMMENT_DTO.Id, resultValue.Id);
        Assert.True(TEST_COMMENT_DTO.Equals(resultValue));
    }

    [Fact]
    public async void CreateCommentEntityDataValidationException()
    {
        CreateCommentDto testData = new() { PostId = 1, Text = TEST_COMMENT_DTO.Text };
        string errorMessage = $"Failed to add the comment to the post with id={testData.PostId} because such post doesn't exist.";
        _commentsService.Setup(ps => ps.CreateCommentAsync(testData)).ThrowsAsync(new EntityDataValidationException(errorMessage));
        var commentController = new CommentController(_logger.Object, _commentsService.Object);
        try
        {
            await commentController.CreateComment(testData);
        }
        catch (EntityDataValidationException ex)
        {
            var entityDataValidationException = Assert.IsAssignableFrom<EntityDataValidationException>(ex);
            Assert.True(entityDataValidationException.Message == errorMessage);
        }
    }

    [Fact]
    public async void UpdateCommentNoContent()
    {
        _commentsService.Setup(ps => ps.UpdateCommentAsync(TEST_COMMENT_DTO)).ReturnsAsync(TEST_COMMENT_DTO);
        var commentController = new CommentController(_logger.Object, _commentsService.Object);

        var actionResult = await commentController.UpdateComment(TEST_COMMENT_DTO);

        Assert.NotNull(actionResult);
        var noContentResult = Assert.IsAssignableFrom<NoContentResult>(actionResult);
        Assert.True(noContentResult.StatusCode == 204);
    }

    [Fact]
    public async void UpdateCommentEntityNotFoundException()
    {
        string errorMessage = $"Failed to update the comment with id={TEST_COMMENT_DTO.Id} because such comment doesn't exist.";
        _commentsService.Setup(ps => ps.UpdateCommentAsync(TEST_COMMENT_DTO)).ThrowsAsync(new EntityNotFoundException(errorMessage));
        var commentController = new CommentController(_logger.Object, _commentsService.Object);

        try
        {
            await commentController.UpdateComment(TEST_COMMENT_DTO);
        }
        catch (EntityNotFoundException ex)
        {
            var entityNotFoundException = Assert.IsAssignableFrom<EntityNotFoundException>(ex);
            Assert.True(entityNotFoundException.Message == errorMessage);
        }
    }

    [Fact]
    public async void DeleteCommentNoContent()
    {
        _commentsService.Setup(ps => ps.DeleteCommentAsync(TEST_COMMENT_DTO.Id)).ReturnsAsync(TEST_COMMENT_DTO);
        var commentController = new CommentController(_logger.Object, _commentsService.Object);

        var actionResult = await commentController.DeleteComment(TEST_COMMENT_DTO.Id);

        Assert.NotNull(actionResult);
        var noContentResult = Assert.IsAssignableFrom<NoContentResult>(actionResult);
        Assert.True(noContentResult.StatusCode == 204);
    }

    [Fact]
    public async void DeleteCommentEntityNotFoundException()
    {
        string errorMessage = $"Failed to update the comment with id={TEST_COMMENT_DTO.Id} because such comment doesn't exist.";
        _commentsService.Setup(ps => ps.DeleteCommentAsync(TEST_COMMENT_DTO.Id)).ThrowsAsync(new EntityNotFoundException(errorMessage));
        var commentController = new CommentController(_logger.Object, _commentsService.Object);

        try
        {
            await commentController.DeleteComment(TEST_COMMENT_DTO.Id);
        }
        catch (EntityNotFoundException ex)
        {
            var entityNotFoundException = Assert.IsAssignableFrom<EntityNotFoundException>(ex);
            Assert.True(entityNotFoundException.Message == errorMessage);
        }
    }
}
