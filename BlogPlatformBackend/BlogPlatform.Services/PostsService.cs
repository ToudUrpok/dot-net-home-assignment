using Microsoft.EntityFrameworkCore;
using BlogPlatform.Data;
using BlogPlatform.Data.Entities;
using BlogPlatform.Dtos;

namespace BlogPlatform.Services;

public class PostsService(BlogContext dbContext) : IPostsService
{
    private readonly BlogContext _dbContext = dbContext;

    public async Task<IEnumerable<PostDto>?> GetPostsAsync()
    {
        var postsEntries = await _dbContext.Posts
            .AsNoTracking()
            .ToListAsync();
        if (postsEntries is null) return null;

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

    public async Task<PostDto?> GetPostAsync(long id)
    {
        var postEntry = await _dbContext.Posts
            .Include(p => p.Comments)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);

        return postEntry is null ? null : CreatePostDto(postEntry);
    }

    public async Task<PostDto?> CreatePostAsync(CreatePostDto data)
    {
        var postEntry = _dbContext.Posts.Add(new()
        {
            Title = data.Title,
            Content = data.Content
        });
        await _dbContext.SaveChangesAsync();

        return CreatePostDto(postEntry.Entity);
    }

    public async Task<bool> UpdatePostAsync(UpdatePostDto post)
    {
        var postEntry = await _dbContext.Posts.FindAsync(post.Id);
        if (postEntry is null) return false;

        if (postEntry.Title != post.Title)
        {
            postEntry.Title = post.Title;
        }
        if (postEntry.Content != post.Content)
        {
            postEntry.Content = post.Content;
        }
        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeletePostAsync(long id)
    {
        var postEntry = await _dbContext.Posts.FindAsync(id);
        if (postEntry is null) return false;

        _dbContext.Posts.Remove(postEntry);
        await _dbContext.SaveChangesAsync();

        return true;
    }

    private static PostDto CreatePostDto(Post entry)
    {
        var postDto = new PostDto()
        {
            Id = entry.Id,
            Title = entry.Title,
            Content = entry.Content,
        };

        if (entry.Comments.Count > 0)
        {
            var comments = new List<CommentDto>();
            foreach (var comment in entry.Comments)
            {
                comments.Add(new CommentDto()
                {
                    Id = comment.Id,
                    Text = comment.Text
                });
            }
            postDto.Comments = comments;
        }

        return postDto;
    }
}
