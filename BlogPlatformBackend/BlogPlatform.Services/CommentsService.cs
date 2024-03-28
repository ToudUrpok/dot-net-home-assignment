using BlogPlatform.Data;
using BlogPlatform.Dtos;
using Microsoft.EntityFrameworkCore;

namespace BlogPlatform.Services;

public class CommentsService(BlogContext dbContext) : ICommentsService
{
    private readonly BlogContext _dbContext = dbContext;

    public async Task<CommentDto?> GetCommentAsync(long id)
    {
        var commentEntry = await _dbContext.Comments
            .AsNoTracking()
            .SingleOrDefaultAsync(c => c.Id == id);
        if (commentEntry is null) return null;

        return new CommentDto()
        {
            Id = commentEntry.Id,
            Text = commentEntry.Text
        };
    }

    public async Task<CommentDto?> CreateCommentAsync(CreateCommentDto data)
    {
        bool isPostExist = await _dbContext.Posts.Where(p => p.Id == data.PostId).AnyAsync();
        if (!isPostExist) return null;

        var commentEntry = _dbContext.Comments.Add(new()
        {
            Text = data.Text,
            PostId = data.PostId,
        });
        await _dbContext.SaveChangesAsync();

        return new CommentDto()
        {
            Id = commentEntry.Entity.Id,
            Text = commentEntry.Entity.Text
        };
    }

    public async Task<bool> UpdateCommentAsync(CommentDto comment)
    {
        var commentEntry = await _dbContext.Comments.SingleOrDefaultAsync(c => c.Id == comment.Id);
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
        var commentEntry = await _dbContext.Comments.SingleOrDefaultAsync(c => c.Id == id);
        if (commentEntry is null) return false;

        _dbContext.Comments.Remove(commentEntry);
        await _dbContext.SaveChangesAsync();

        return true;
    }
}
