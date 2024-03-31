using BlogPlatform.Dtos;

namespace BlogPlatform.Services;

public interface IPostsService
{
    Task<IEnumerable<PostDto>> GetPostsAsync();
    Task<PostDto> GetPostAsync(long id);
    Task<PostDto> CreatePostAsync(CreatePostDto data);
    Task<PostDto> UpdatePostAsync(UpdatePostDto post);
    Task<PostDto> DeletePostAsync(long id);
}
