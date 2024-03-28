using System.ComponentModel.DataAnnotations;

namespace BlogPlatform.Dtos;

public class UpdateUserDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    [MaxLength(64)]
    public string UserName { get; set; }
}
