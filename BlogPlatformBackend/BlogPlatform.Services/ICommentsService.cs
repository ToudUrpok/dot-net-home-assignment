using BlogPlatform.Dtos;

namespace BlogPlatform.Services;

public interface ICommentsService
{
    Task<CommentDto> GetCommentAsync(long id);
    Task<CommentDto> CreateCommentAsync(CreateCommentDto data);
    Task<CommentDto> UpdateCommentAsync(CommentDto comment);
    Task<CommentDto> DeleteCommentAsync(long id);
}
