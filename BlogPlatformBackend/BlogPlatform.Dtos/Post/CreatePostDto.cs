using System.ComponentModel.DataAnnotations;

namespace BlogPlatform.Dtos;

public class CreatePostDto
{
    [Required]
    [MaxLength(100)]
    public string Title { get; set; }

    [Required]
    [MaxLength(1000)]
    public string Content { get; set; }
}
