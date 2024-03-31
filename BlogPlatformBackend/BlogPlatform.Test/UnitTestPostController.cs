using Moq;
using BlogPlatform.Services;
using BlogPlatform.Services.Exceptions;
using BlogPlatform.Dtos;
using BlogPlatform.WebApi.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

namespace BlogPlatform.Test;

public class UnitTestPostController()
{
    private readonly Mock<IPostsService> _postService = new();
    private readonly Mock<ILogger<PostController>> _logger = new();

    private static readonly IEnumerable<PostDto> POSTS =
        [
            new PostDto
            {
                Id = 1,
                Title = "C#",
                Content = "C# is a programming language.",
                Comments =
                [
                    new CommentDto
                    {
                        Id= 1,
                        Text= "I like C# !",
                    },
                    new CommentDto
                    {
                        Id= 2,
                        Text= "I like C# very much !",
                    }
                ]
            },
            new PostDto
            {
                Id = 2,
                Title = "JavaScript",
                Content = "JavaScript is a programming language.",
                Comments =
                [
                    new CommentDto
                    {
                        Id= 3,
                        Text= "I like JS !",
                    }
                ]
            },
            new PostDto
            {
                Id = 3,
                Title = "TypeScript",
                Content = "TypeScript is a programming language."
            },
        ];

    [Fact]
    public async void GetPostsOK()
    {
        _postService.Setup(ps => ps.GetPostsAsync()).ReturnsAsync(POSTS);
        var postController = new PostController(_logger.Object, _postService.Object);

        var actionResult = await postController.GetPosts();

        Assert.NotNull(actionResult.Result);
        var okResult = Assert.IsAssignableFrom<OkObjectResult>(actionResult.Result);
        Assert.True(okResult.StatusCode == 200);
        Assert.NotNull(okResult.Value);
        var resultValue = Assert.IsAssignableFrom<IEnumerable<PostDto>>(okResult.Value);
        Assert.Equal(POSTS.Count(), resultValue.Count());
        Assert.True(POSTS.Equals(resultValue));
    }

    [Fact]
    public async void GetPostOk()
    {
        var testResult = POSTS.ToList()[1];
        _postService.Setup(ps => ps.GetPostAsync(testResult.Id)).ReturnsAsync(testResult);
        var postController = new PostController(_logger.Object, _postService.Object);

        var actionResult = await postController.GetPost(testResult.Id);

        Assert.NotNull(actionResult.Result);
        var okResult = Assert.IsAssignableFrom<OkObjectResult>(actionResult.Result);
        Assert.True(okResult.StatusCode == 200);
        Assert.NotNull(okResult.Value);
        var resultValue = Assert.IsAssignableFrom<PostDto>(okResult.Value);
        Assert.Equal(testResult.Id, resultValue.Id);
        Assert.True(testResult.Equals(resultValue));
    }

    [Fact]
    public async void GetPostEntityNotFoundException()
    {
        string errorMessage = $"Post with id={10} is not found.";
        _postService.Setup(ps => ps.GetPostAsync(10)).ThrowsAsync(new EntityNotFoundException(errorMessage));
        var postController = new PostController(_logger.Object, _postService.Object);

        try
        {
            await postController.GetPost(10);
        }
        catch (EntityNotFoundException ex)
        {
            var entityNotFoundException = Assert.IsAssignableFrom<EntityNotFoundException>(ex);
            Assert.True(entityNotFoundException.Message == errorMessage);
        }
    }

    [Fact]
    public async void CreatePostCreated()
    {
        CreatePostDto testData = new() { Title = "Test", Content = "Test" };
        PostDto testResult = new() { Id = 5, Title = testData.Title, Content = testData.Content };
        _postService.Setup(ps => ps.CreatePostAsync(testData)).ReturnsAsync(testResult);
        var postController = new PostController(_logger.Object, _postService.Object);

        var actionResult = await postController.CreatePost(testData);

        Assert.NotNull(actionResult.Result);
        var createdAtActionResult = Assert.IsAssignableFrom<CreatedAtActionResult>(actionResult.Result);
        Assert.True(createdAtActionResult.StatusCode == 201);
        object? routeValue = null;
        Assert.True(createdAtActionResult.RouteValues?.TryGetValue("id", out routeValue));
        Assert.True(routeValue is not null && (long)routeValue == testResult.Id);
        Assert.True(createdAtActionResult.ActionName == nameof(postController.GetPost));
        Assert.NotNull(createdAtActionResult.Value);
        var resultValue = Assert.IsAssignableFrom<PostDto>(createdAtActionResult.Value);
        Assert.Equal(testResult.Id, resultValue.Id);
        Assert.True(testResult.Equals(resultValue));
    }

    [Fact]
    public async void UpdatePostNoContent()
    {
        UpdatePostDto testData = new() { Id = 1, Content = "Test", Title = "Test" };
        PostDto testResult = new() { Id = 1, Content = "Test", Title = "Test" };
        _postService.Setup(ps => ps.UpdatePostAsync(testData)).ReturnsAsync(testResult);
        var postController = new PostController(_logger.Object, _postService.Object);

        var actionResult = await postController.UpdatePost(testData);

        Assert.NotNull(actionResult);
        var noContentResult = Assert.IsAssignableFrom<NoContentResult>(actionResult);
        Assert.True(noContentResult.StatusCode == 204);
    }

    [Fact]
    public async void UpdatePostEntityNotFoundException()
    {
        UpdatePostDto testData = new() { Id = 10, Title = string.Empty, Content = string.Empty };
        string errorMessage = $"Failed to update the post with id={testData.Id} because such post doesn't exist.";
        _postService.Setup(ps => ps.UpdatePostAsync(testData)).ThrowsAsync(new EntityNotFoundException(errorMessage));
        var postController = new PostController(_logger.Object, _postService.Object);
        try
        {
            await postController.UpdatePost(testData);
        }
        catch (EntityNotFoundException ex)
        {
            var entityNotFoundException = Assert.IsAssignableFrom<EntityNotFoundException>(ex);
            Assert.True(entityNotFoundException.Message == errorMessage);
        }
    }

    [Fact]
    public async void DeletePostNoContent()
    {
        PostDto testResult = new() { Id = 1, Content = "Test", Title = "Test" };
        _postService.Setup(ps => ps.DeletePostAsync(1)).ReturnsAsync(testResult);
        var postController = new PostController(_logger.Object, _postService.Object);

        var actionResult = await postController.DeletePost(1);

        Assert.NotNull(actionResult);
        var noContentResult = Assert.IsAssignableFrom<NoContentResult>(actionResult);
        Assert.True(noContentResult.StatusCode == 204);
    }

    [Fact]
    public async void DeletePostEntityNotFoundException()
    {
        string errorMessage = $"Failed to update the post with id={10} because such post doesn't exist.";
        _postService.Setup(ps => ps.DeletePostAsync(10)).ThrowsAsync(new EntityNotFoundException(errorMessage));
        var postController = new PostController(_logger.Object, _postService.Object);

        try
        {
            await postController.DeletePost(10);
        }
        catch (EntityNotFoundException ex)
        {
            var entityNotFoundException = Assert.IsAssignableFrom<EntityNotFoundException>(ex);
            Assert.True(entityNotFoundException.Message == errorMessage);
        }
    }
}