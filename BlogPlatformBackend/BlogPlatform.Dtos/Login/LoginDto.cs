﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BlogPlatform.Dtos;

public class LoginDto
{
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required]
    [PasswordPropertyText]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
