namespace BlogPlatform.Dtos;

public class PostDto
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public List<CommentDto>? Comments { get; set; }
}
