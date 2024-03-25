namespace BlogPlatform.Data.Entities;

public class Post
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public ICollection<Comment> Comments { get; set; } = [];
}