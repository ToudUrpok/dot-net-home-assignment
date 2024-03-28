using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BlogPlatform.Dtos;

public class LoginDto
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
