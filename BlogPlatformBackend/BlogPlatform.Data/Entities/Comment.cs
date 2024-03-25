namespace BlogPlatform.Data.Entities;

public class Comment
{
    public long Id { get; set; }
    public string Text { get; set; }
    public long PostId { get; set; }

    public Post Post { get; set; }
}
