using Moq;
using BlogPlatform.Services;
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
        var postController = new PostController(_logger.Object , _postService.Object);

        var actionResult = await postController.GetPosts();
        var okResult = (OkObjectResult)actionResult.Result!;
        var resultValue = (IEnumerable<PostDto>)okResult.Value!;

        Assert.NotNull(okResult);
        Assert.True(okResult.StatusCode == 200);
        Assert.NotNull(resultValue);
        Assert.Equal(POSTS.Count(), resultValue.Count());
        Assert.True(POSTS.Equals(resultValue));
    }

    [Fact]
    public async void GetPostsNotFound()
    {
        _postService.Setup(ps => ps.GetPostsAsync()).ReturnsAsync([]);
        var postController = new PostController(_logger.Object, _postService.Object);

        var actionResult = await postController.GetPosts();
        var notFoundResult = (NotFoundResult)actionResult.Result!;

        Assert.NotNull(notFoundResult);
        Assert.True(notFoundResult.StatusCode == 404);
    }

    [Fact]
    public async void GetPostsBadRequest()
    {
        IEnumerable<PostDto>? testResult = null;
        _postService.Setup(ps => ps.GetPostsAsync()).ReturnsAsync(testResult);
        var postController = new PostController(_logger.Object, _postService.Object);

        var actionResult = await postController.GetPosts();
        var badRequestResult = (BadRequestResult)actionResult.Result!;

        Assert.NotNull(badRequestResult);
        Assert.True(badRequestResult.StatusCode == 400);
    }

    [Fact]
    public async void GetPostOk()
    {
        var testPost = POSTS.ToList()[1];
        _postService.Setup(ps => ps.GetPostAsync(2)).ReturnsAsync(testPost);
        var postController = new PostController(_logger.Object, _postService.Object);

        var actionResult = await postController.GetPost(2);
        var okResult = (OkObjectResult)actionResult.Result!;
        var resultValue = (PostDto)okResult.Value!;

        Assert.NotNull(okResult);
        Assert.True(okResult.StatusCode == 200);
        Assert.NotNull(resultValue);
        Assert.Equal(testPost.Id, resultValue.Id);
        Assert.True(testPost.Equals(resultValue));
    }

    [Fact]
    public async void GetPostNotFound()
    {
        PostDto? testResult = null;
        _postService.Setup(ps => ps.GetPostAsync(10)).ReturnsAsync(testResult);
        var postController = new PostController(_logger.Object, _postService.Object);

        var actionResult = await postController.GetPost(10);
        var notFoundResult = (NotFoundResult)actionResult.Result!;

        Assert.NotNull(notFoundResult);
        Assert.True(notFoundResult.StatusCode == 404);
    }

    [Fact]
    public async void CreatePostCreated()
    {
        CreatePostDto testData = new() { Title = "Created Title", Content = "Created Content" };
        PostDto testResult = new() { Id = 5, Title = testData.Title, Content = testData.Content };
        _postService.Setup(ps => ps.CreatePostAsync(testData)).ReturnsAsync(testResult);
        var postController = new PostController(_logger.Object, _postService.Object);

        var actionResult = await postController.CreatePost(testData);
        var createdAtActionResult = (CreatedAtActionResult)actionResult.Result!;
        var resultValue = (PostDto)createdAtActionResult.Value!;

        Assert.NotNull(createdAtActionResult);
        Assert.True(createdAtActionResult.StatusCode == 201);
        object? routeValue = null;
        Assert.True(createdAtActionResult.RouteValues?.TryGetValue("id", out routeValue));
        Assert.True(routeValue is not null && (long)routeValue == testResult.Id);
        Assert.True(createdAtActionResult.ActionName == nameof(postController.GetPost));
        Assert.NotNull(resultValue);
        Assert.Equal(testResult.Id, resultValue.Id);
        Assert.True(testResult.Equals(resultValue));
    }

    [Fact]
    public async void CreatePostBadRequest()
    {
        CreatePostDto testData = new() { Title = "", Content = "" };
        PostDto? testResult = null;
        _postService.Setup(ps => ps.CreatePostAsync(testData)).ReturnsAsync(testResult);
        var postController = new PostController(_logger.Object, _postService.Object);

        var actionResult = await postController.CreatePost(testData);
        var badRequestResult = (BadRequestResult)actionResult.Result!;

        Assert.NotNull(badRequestResult);
        Assert.True(badRequestResult.StatusCode == 400);
    }
}