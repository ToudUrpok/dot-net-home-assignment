using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BlogPlatform.Dtos;

public class LoginDto
{
    [Required]
    public string UserName { get; set; }

    [Required]
    [PasswordPropertyText]
    public string Password { get; set; }
}
