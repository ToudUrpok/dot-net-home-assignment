using Moq;
using BlogPlatform.Services;
using BlogPlatform.Dtos;
using BlogPlatform.WebApi.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

namespace BlogPlatform.Test;

public class UnitTestCommentController
{
    private readonly Mock<ICommentsService> _commentsService = new();
    private readonly Mock<ILogger<CommentController>> _logger = new();

    [Fact]
    public async void GetCommentOk()
    {
        CommentDto? testResult = new() { Id = 2, Text = "Test Comment" };
        _commentsService.Setup(ps => ps.GetCommentAsync(2)).ReturnsAsync(testResult);
        var commentController = new CommentController(_logger.Object, _commentsService.Object);

        var actionResult = await commentController.GetComment(2);

        Assert.NotNull(actionResult.Result);
        var okResult = Assert.IsAssignableFrom<OkObjectResult>(actionResult.Result);
        Assert.True(okResult.StatusCode == 200);
        Assert.NotNull(okResult.Value);
        var resultValue = Assert.IsAssignableFrom<CommentDto>(okResult.Value);
        Assert.Equal(testResult.Id, resultValue.Id);
        Assert.True(testResult.Equals(resultValue));
    }

    [Fact]
    public async void GetCommentNotFound()
    {
        CommentDto? testResult = null;
        _commentsService.Setup(ps => ps.GetCommentAsync(10)).ReturnsAsync(testResult);
        var commentController = new CommentController(_logger.Object, _commentsService.Object);

        var actionResult = await commentController.GetComment(10);

        Assert.NotNull(actionResult.Result);
        var notFoundResult = Assert.IsAssignableFrom<NotFoundResult>(actionResult.Result);
        Assert.True(notFoundResult.StatusCode == 404);
    }

    [Fact]
    public async void CreateCommentCreated()
    {
        CreateCommentDto testData = new() { PostId = 1, Text = "Test Comment" };
        CommentDto testResult = new() { Id = 1, Text = testData.Text };
        _commentsService.Setup(ps => ps.CreateCommentAsync(testData)).ReturnsAsync(testResult);
        var commentController = new CommentController(_logger.Object, _commentsService.Object);

        var actionResult = await commentController.CreateComment(testData);

        Assert.NotNull(actionResult.Result);
        var createdAtActionResult = Assert.IsAssignableFrom<CreatedAtActionResult>(actionResult.Result);
        Assert.True(createdAtActionResult.StatusCode == 201);
        object? routeValue = null;
        Assert.True(createdAtActionResult.RouteValues?.TryGetValue("id", out routeValue));
        Assert.True(routeValue is not null && (long)routeValue == testResult.Id);
        Assert.True(createdAtActionResult.ActionName == nameof(commentController.GetComment));
        Assert.NotNull(createdAtActionResult.Value);
        var resultValue = Assert.IsAssignableFrom<CommentDto>(createdAtActionResult.Value);
        Assert.Equal(testResult.Id, resultValue.Id);
        Assert.True(testResult.Equals(resultValue));
    }

    [Fact]
    public async void CreateCommentBadRequest()
    {
        CreateCommentDto testData = new();
        CommentDto? testResult = null;
        _commentsService.Setup(ps => ps.CreateCommentAsync(testData)).ReturnsAsync(testResult);
        var commentController = new CommentController(_logger.Object, _commentsService.Object);

        var actionResult = await commentController.CreateComment(testData);

        Assert.NotNull(actionResult.Result);
        var badRequestResult = Assert.IsAssignableFrom<BadRequestResult>(actionResult.Result);
        Assert.True(badRequestResult.StatusCode == 400);
    }

    [Fact]
    public async void UpdateCommentOk()
    {
        CommentDto testData = new() { Id = 1, Text = "Updated Text" };
        _commentsService.Setup(ps => ps.UpdateCommentAsync(testData)).ReturnsAsync(true);
        var commentController = new CommentController(_logger.Object, _commentsService.Object);

        var actionResult = await commentController.UpdateComment(testData.Id, testData);

        Assert.NotNull(actionResult);
        var noContentResult = Assert.IsAssignableFrom<NoContentResult>(actionResult);
        Assert.True(noContentResult.StatusCode == 204);
    }

    [Fact]
    public async void UpdateCommentBadRequest()
    {
        CommentDto testData = new() { Id = 1 };
        _commentsService.Setup(ps => ps.UpdateCommentAsync(testData)).ReturnsAsync(false);
        var commentController = new CommentController(_logger.Object, _commentsService.Object);

        var actionResult = await commentController.UpdateComment(testData.Id, testData);

        Assert.NotNull(actionResult);
        var badRequestResult = Assert.IsAssignableFrom<BadRequestResult>(actionResult);
        Assert.True(badRequestResult.StatusCode == 400);
    }

    [Fact]
    public async void DeleteCommentOk()
    {
        _commentsService.Setup(ps => ps.DeleteCommentAsync(10)).ReturnsAsync(true);
        var commentController = new CommentController(_logger.Object, _commentsService.Object);

        var actionResult = await commentController.DeleteComment(10);

        Assert.NotNull(actionResult);
        var noContentResult = Assert.IsAssignableFrom<NoContentResult>(actionResult);
        Assert.True(noContentResult.StatusCode == 204);
    }

    [Fact]
    public async void DeleteCommentBadRequest()
    {
        _commentsService.Setup(ps => ps.DeleteCommentAsync(10)).ReturnsAsync(false);
        var commentController = new CommentController(_logger.Object, _commentsService.Object);

        var actionResult = await commentController.DeleteComment(10);

        Assert.NotNull(actionResult);
        var badRequestResult = Assert.IsAssignableFrom<BadRequestResult>(actionResult);
        Assert.True(badRequestResult.StatusCode == 400);
    }
}
