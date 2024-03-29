using System.ComponentModel.DataAnnotations;

namespace BlogPlatform.Dtos;

public class UserDto
{
    public int Id { get; set; }

    public required string UserName { get; set; }

    [DataType(DataType.EmailAddress)]
    public required string Email { get; set; }
}
