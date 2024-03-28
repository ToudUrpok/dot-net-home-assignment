using BlogPlatform.Dtos;

namespace BlogPlatform.Services;

public interface IPostsService
{
    Task<IEnumerable<PostDto>> GetPostsAsync();
    Task<PostDto?> GetPostAsync(long id);
    Task<PostDto?> CreatePostAsync(CreatePostDto data);
    Task<bool> UpdatePostAsync(UpdatePostDto post);
    Task<bool> DeletePostAsync(long id);
}
