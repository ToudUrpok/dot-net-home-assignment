namespace BlogPlatform.Dtos;

public class PostDto
{
    public long Id { get; set; }
    public required string Title { get; set; }
    public required string Content { get; set; }
    public List<CommentDto> Comments { get; set; } = [];
}
