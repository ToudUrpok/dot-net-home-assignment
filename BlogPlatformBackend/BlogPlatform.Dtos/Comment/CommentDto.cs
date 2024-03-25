using System.ComponentModel.DataAnnotations;

namespace BlogPlatform.Dtos;

public class CommentDto
{
    [Required]
    public long Id { get; set; }

    [Required]
    public string Text { get; set; }
}
