﻿using BlogPlatform.Dtos;

namespace BlogPlatform.Services;

public interface IUsersService
{
    Task<UserDto> GetUserAsync(int id);
    Task<UserDto> CreateUserAsync(CreateUserDto data);
    Task<UserDto> UpdateUserAsync(UpdateUserDto comment);
}
