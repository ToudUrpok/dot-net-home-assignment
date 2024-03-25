using System.ComponentModel.DataAnnotations;

namespace BlogPlatform.Dtos;

public class UpdatePostDto
{
    [Required]
    public long Id { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public string Content { get; set; }
}
