using System.ComponentModel.DataAnnotations;

namespace BlogPlatform.Data.Entities;

public class Post
{
    public long Id { get; set; }

    [MaxLength(100)]
    public required string Title { get; set; }

    [MaxLength(1000)]
    public required string Content { get; set; }

    public ICollection<Comment> Comments { get; set; } = [];
}