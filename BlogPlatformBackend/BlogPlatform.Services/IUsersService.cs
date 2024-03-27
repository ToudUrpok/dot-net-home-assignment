using BlogPlatform.Dtos;

namespace BlogPlatform.Services;

public interface IUsersService
{
    Task<UserDto?> GetUserAsync(int id);
    Task<UserDto?> GetUserByEmailAsync(string email);
    Task<UserDto?> CreateUserAsync(CreateUserDto data);
    Task<bool> UpdateUserAsync(UpdateUserDto comment);
}
