using Microsoft.EntityFrameworkCore;
using BlogPlatform.Data;
using BlogPlatform.Data.Entities;
using BlogPlatform.Dtos;
using BlogPlatform.Services.Exceptions;

namespace BlogPlatform.Services;

public class PostsService(BlogContext dbContext) : IPostsService
{
    private readonly BlogContext _dbContext = dbContext;

    public async Task<IEnumerable<PostDto>> GetPostsAsync()
    {
        var postsEntries = await _dbContext.Posts
            .AsNoTracking()
            .ToListAsync();

        List<PostDto> postsDtos = [];
        foreach (var postEntry in postsEntries)
        {
            postsDtos.Add(new PostDto()
            {
                Id = postEntry.Id,
                Title = postEntry.Title,
                Content = postEntry.Content,
            });
        }

        return postsDtos;
    }

    public async Task<PostDto> GetPostAsync(long id)
    {
        var postEntry = await _dbContext.Posts
        .Include(p => p.Comments)
        .AsNoTracking()
        .SingleOrDefaultAsync(p => p.Id == id) ??
            throw new EntityNotFoundException($"Post with id={id} is not found.");

        return MapPostToDto(postEntry);
    }

    public async Task<PostDto> CreatePostAsync(CreatePostDto data)
    {
        var postEntry = _dbContext.Posts.Add(new()
        {
            Title = data.Title,
            Content = data.Content
        });
        await _dbContext.SaveChangesAsync();

        return MapPostToDto(postEntry.Entity);
    }

    public async Task<PostDto> UpdatePostAsync(UpdatePostDto post)
    {
        var postEntry = await _dbContext.Posts.SingleOrDefaultAsync(p => p.Id == post.Id) ??
            throw new EntityNotFoundException($"Failed to update the post with id={post.Id} because such post doesn't exist.");

        if (postEntry.Title != post.Title)
        {
            postEntry.Title = post.Title;
        }
        if (postEntry.Content != post.Content)
        {
            postEntry.Content = post.Content;
        }
        await _dbContext.SaveChangesAsync();

        return new PostDto()
        {
            Id = postEntry.Id,
            Title = postEntry.Title,
            Content = postEntry.Content,
        };
    }

    public async Task<PostDto> DeletePostAsync(long id)
    {
        var postEntry = await _dbContext.Posts.SingleOrDefaultAsync(p => p.Id == id) ??
            throw new EntityNotFoundException($"Failed to delete the post with id={id} because such post doesn't exist.");

        _dbContext.Posts.Remove(postEntry);
        await _dbContext.SaveChangesAsync();

        return new PostDto()
        {
            Id = postEntry.Id,
            Title = postEntry.Title,
            Content = postEntry.Content,
        };
    }

    private static PostDto MapPostToDto(Post entry)
    {
        var postDto = new PostDto()
        {
            Id = entry.Id,
            Title = entry.Title,
            Content = entry.Content,
            Comments = []
        };

        foreach (var comment in entry.Comments)
        {
            postDto.Comments.Add(new CommentDto()
            {
                Id = comment.Id,
                Text = comment.Text
            });
        }

        return postDto;
    }
}
