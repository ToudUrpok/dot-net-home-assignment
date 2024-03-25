using BlogPlatform.Dtos;

namespace BlogPlatform.WebApi.Services;

public interface ICommentsService
{
    Task<CommentDto?> GetCommentAsync(long id);
    Task<CommentDto?> CreateCommentAsync(CreateCommentDto data);
    Task<bool> UpdateCommentAsync(CommentDto comment);
    Task<bool> DeleteCommentAsync(long id);
}
