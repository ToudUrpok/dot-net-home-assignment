using Microsoft.EntityFrameworkCore;
using BlogPlatform.WebApi.Model.Entities;

namespace BlogPlatform.WebApi.Model;

public class BlogContext(DbContextOptions<BlogContext> options)
    : DbContext(options)
{
    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
}
