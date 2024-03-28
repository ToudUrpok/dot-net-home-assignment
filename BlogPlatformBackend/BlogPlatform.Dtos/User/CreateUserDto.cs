using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BlogPlatform.Dtos;

public class CreateUserDto
{
    [Required]
    [MinLength(6)]
    [MaxLength(320)]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required]
    [MinLength(6)]
    [PasswordPropertyText]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
