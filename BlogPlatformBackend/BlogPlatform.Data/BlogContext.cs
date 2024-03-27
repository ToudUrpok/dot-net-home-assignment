using Microsoft.EntityFrameworkCore;
using BlogPlatform.Data.Entities;
using System.Reflection.Metadata;

namespace BlogPlatform.Data;

public class BlogContext(DbContextOptions<BlogContext> options)
    : DbContext(options)
{
    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AppUser>()
            .HasData(new AppUser()
            {
                Id = 1,
                UserName = "TestUser",
                Email = "test@gmail.com",
                Password = "123456"
            });
    }
}
