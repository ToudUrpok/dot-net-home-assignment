﻿using System.ComponentModel.DataAnnotations;

namespace BlogPlatform.Dtos;

public class UserDto
{
    public int Id { get; set; }

    public string UserName { get; set; }

    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
}