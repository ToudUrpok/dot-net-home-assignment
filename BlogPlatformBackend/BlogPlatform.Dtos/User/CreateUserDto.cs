using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BlogPlatform.Dtos;

public class CreateUserDto
{
    [Required]
    [MinLength(6)]
    [MaxLength(320)]
    [EmailAddress]
    [DataType(DataType.EmailAddress)]
    public required string Email { get; set; }

    [Required]
    [MinLength(6)]
    [PasswordPropertyText]
    [DataType(DataType.Password)]
    public required string Password { get; set; }
}
