using BlogPlatform.Data;
using BlogPlatform.Dtos;
using BlogPlatform.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BlogPlatform.Services;

public class CommentsService(BlogContext dbContext) : ICommentsService
{
    private readonly BlogContext _dbContext = dbContext;

    public async Task<CommentDto> GetCommentAsync(long id)
    {
        var commentEntry = await _dbContext.Comments
        .AsNoTracking()
        .SingleOrDefaultAsync(c => c.Id == id) ??
            throw new EntityNotFoundException($"The comment with id={id} is not found.");

        return new CommentDto()
        {
            Id = commentEntry.Id,
            Text = commentEntry.Text
        };
    }

    public async Task<CommentDto> CreateCommentAsync(CreateCommentDto data)
    {
        bool isPostExist = await _dbContext.Posts.AnyAsync(p => p.Id == data.PostId);
        if (!isPostExist) throw new EntityDataValidationException($"Failed to add the comment to the post with id={data.PostId} because such post doesn't exist.");

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

    public async Task<CommentDto> UpdateCommentAsync(CommentDto comment)
    {
        var commentEntry = await _dbContext.Comments.SingleOrDefaultAsync(c => c.Id == comment.Id) ??
            throw new EntityNotFoundException($"Failed to update the comment with id={comment.Id} because such comment doesn't exist.");

        if (commentEntry.Text != comment.Text)
        {
            commentEntry.Text = comment.Text;
        }
        await _dbContext.SaveChangesAsync();

        return new CommentDto()
        {
            Id = commentEntry.Id,
            Text = commentEntry.Text
        };
    }

    public async Task<CommentDto> DeleteCommentAsync(long id)
    {
        var commentEntry = await _dbContext.Comments.SingleOrDefaultAsync(c => c.Id == id) ??
            throw new EntityNotFoundException($"Failed to delete the comment with id={id} because such comment doesn't exist."); ;

        _dbContext.Comments.Remove(commentEntry);
        await _dbContext.SaveChangesAsync();

        return new CommentDto()
        {
            Id = commentEntry.Id,
            Text = commentEntry.Text
        };
    }
}
