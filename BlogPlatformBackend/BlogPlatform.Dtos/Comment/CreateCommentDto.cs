using System.ComponentModel.DataAnnotations;

namespace BlogPlatform.Dtos;

public class CreateCommentDto
{
    [Required]
    public long PostId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Text { get; set; }
}
