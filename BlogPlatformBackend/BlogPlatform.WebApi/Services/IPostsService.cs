using BlogPlatform.Dtos;

namespace BlogPlatform.WebApi.Services;

public interface IPostsService
{
    Task<List<PostDto>?> GetPostsAsync();
    Task<PostDto?> GetPostAsync(long id);
    Task<PostDto?> CreatePostAsync(CreatePostDto data);
    Task<bool> UpdatePostAsync(UpdatePostDto post);
    Task<bool> DeletePostAsync(long id);
}
