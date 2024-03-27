using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BlogPlatform.Dtos;

public class CreateUserDto
{
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required]
    [PasswordPropertyText]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
