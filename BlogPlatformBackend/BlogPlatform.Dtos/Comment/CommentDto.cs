using System.ComponentModel.DataAnnotations;

namespace BlogPlatform.Dtos;

public class CommentDto
{
    [Required]
    public long Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Text { get; set; }
}
