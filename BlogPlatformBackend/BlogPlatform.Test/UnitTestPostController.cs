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

        Assert.NotNull(actionResult.Result);
        var okResult = Assert.IsAssignableFrom<OkObjectResult>(actionResult.Result);
        Assert.True(okResult.StatusCode == 200);
        Assert.NotNull(okResult.Value);
        var resultValue = Assert.IsAssignableFrom<IEnumerable<PostDto>>(okResult.Value);
        Assert.Equal(POSTS.Count(), resultValue.Count());
        Assert.True(POSTS.Equals(resultValue));
    }

    [Fact]
    public async void GetPostsNotFound()
    {
        _postService.Setup(ps => ps.GetPostsAsync()).ReturnsAsync([]);
        var postController = new PostController(_logger.Object, _postService.Object);

        var actionResult = await postController.GetPosts();

        Assert.NotNull(actionResult.Result);
        var notFoundResult = Assert.IsAssignableFrom<NotFoundResult>(actionResult.Result);
        Assert.True(notFoundResult.StatusCode == 404);
    }

    [Fact]
    public async void GetPostsBadRequest()
    {
        IEnumerable<PostDto>? testResult = null;
        _postService.Setup(ps => ps.GetPostsAsync()).ReturnsAsync(testResult);
        var postController = new PostController(_logger.Object, _postService.Object);

        var actionResult = await postController.GetPosts();

        Assert.NotNull(actionResult.Result);
        var badRequestResult = Assert.IsAssignableFrom<BadRequestResult>(actionResult.Result);
        Assert.True(badRequestResult.StatusCode == 400);
    }

    [Fact]
    public async void GetPostOk()
    {
        var testPost = POSTS.ToList()[1];
        _postService.Setup(ps => ps.GetPostAsync(2)).ReturnsAsync(testPost);
        var postController = new PostController(_logger.Object, _postService.Object);

        var actionResult = await postController.GetPost(2);

        Assert.NotNull(actionResult.Result);
        var okResult = Assert.IsAssignableFrom<OkObjectResult>(actionResult.Result);
        Assert.True(okResult.StatusCode == 200);
        Assert.NotNull(okResult.Value);
        var resultValue = Assert.IsAssignableFrom<PostDto>(okResult.Value);
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

        Assert.NotNull(actionResult.Result);
        var notFoundResult = Assert.IsAssignableFrom<NotFoundResult>(actionResult.Result);
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
    public async void CreatePostBadRequest()
    {
        CreatePostDto testData = new() { Title = "", Content = "" };
        PostDto? testResult = null;
        _postService.Setup(ps => ps.CreatePostAsync(testData)).ReturnsAsync(testResult);
        var postController = new PostController(_logger.Object, _postService.Object);

        var actionResult = await postController.CreatePost(testData);

        Assert.NotNull(actionResult.Result);
        var badRequestResult = Assert.IsAssignableFrom<BadRequestResult>(actionResult.Result);
        Assert.True(badRequestResult.StatusCode == 400);
    }

    [Fact]
    public async void UpdatePostOk()
    {
        UpdatePostDto testData = new() { Id = 10, Title = "Updated Title", Content = "Updated Content" };
        _postService.Setup(ps => ps.UpdatePostAsync(testData)).ReturnsAsync(true);
        var postController = new PostController(_logger.Object, _postService.Object);

        var actionResult = await postController.UpdatePost(testData.Id, testData);

        Assert.NotNull(actionResult);
        var noContentResult = Assert.IsAssignableFrom<NoContentResult>(actionResult);
        Assert.True(noContentResult.StatusCode == 204);
    }

    [Fact]
    public async void UpdatePostIdMismatch()
    {
        UpdatePostDto testData = new() { Id = 10 };
        var postController = new PostController(_logger.Object, _postService.Object);

        var actionResult = await postController.UpdatePost(testData.Id + 1, testData);

        Assert.NotNull(actionResult);
        var badRequestResult = Assert.IsAssignableFrom<BadRequestObjectResult>(actionResult);
        Assert.True(badRequestResult.StatusCode == 400);
        Assert.NotNull(badRequestResult.Value);
        Assert.True(badRequestResult.Value.ToString() == "Invalid update data. Id values from URL mismatches Id value from data object.");
    }
}