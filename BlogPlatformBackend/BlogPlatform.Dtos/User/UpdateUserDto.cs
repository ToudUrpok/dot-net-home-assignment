using System.ComponentModel.DataAnnotations;

namespace BlogPlatform.Dtos;

public class UpdateUserDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string UserName { get; set; }
}
