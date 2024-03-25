using System.ComponentModel.DataAnnotations;

namespace BlogPlatform.Dtos;

public class CreateCommentDto
{
    [Required]
    public long PostId { get; set; }

    [Required]
    public string Text { get; set; }
}
