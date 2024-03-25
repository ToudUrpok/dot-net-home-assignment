using BlogPlatform.WebApi.Model;
using BlogPlatform.WebApi.Model.Entities;
using BlogPlatform.Dtos;

namespace BlogPlatform.WebApi.Services;

public class CommentsService(BlogContext dbContext) : ICommentsService
{
    private readonly BlogContext _dbContext = dbContext;

    public async Task<CommentDto?> GetCommentAsync(long id)
    {
        var commentEntry = await _dbContext.Comments.FindAsync(id);
        if (commentEntry is null) return null;

        return new CommentDto()
        {
            Id = commentEntry.Id,
            Text = commentEntry.Text
        };
    }

    public async Task<CommentDto?> CreateCommentAsync(CreateCommentDto data)
    {
        var commentEntry = _dbContext.Comments.Add(new()
        {
            Text = data.Text,
            PostId = data.PostId,
        });

        try
        {
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            return null;
        }

        return new CommentDto()
        {
            Id = commentEntry.Entity.Id,
            Text = commentEntry.Entity.Text
        };
    }

    public async Task<bool> UpdateCommentAsync(CommentDto comment)
    {
        var commentEntry = await _dbContext.Comments.FindAsync(comment.Id);
        if (commentEntry is null) return false;

        if (commentEntry.Text != comment.Text)
        {
            commentEntry.Text = comment.Text;
        }
        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteCommentAsync(long id)
    {
        var commentEntry = await _dbContext.Comments.FindAsync(id);
        if (commentEntry is null) return false;

        _dbContext.Comments.Remove(commentEntry);
        await _dbContext.SaveChangesAsync();

        return true;
    }
}
