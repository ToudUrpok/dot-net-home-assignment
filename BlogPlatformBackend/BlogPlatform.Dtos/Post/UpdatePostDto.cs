using System.ComponentModel.DataAnnotations;

namespace BlogPlatform.Dtos;

public class UpdatePostDto
{
    [Required]
    public long Id { get; set; }

    [Required]
    [MaxLength(100)]
    public required string Title { get; set; }

    [Required]
    [MaxLength(1000)]
    public required string Content { get; set; }
}
