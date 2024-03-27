using BlogPlatform.Data;
using BlogPlatform.Dtos;
using Microsoft.EntityFrameworkCore;

namespace BlogPlatform.Services;

public class UsersService(BlogContext dbContext) : IUsersService
{
    private readonly BlogContext _dbContext = dbContext;

    public async Task<UserDto?> GetUserAsync(int id)
    {
        var userEntry = await _dbContext.AppUsers.FindAsync(id);
        if (userEntry is null) return null;

        return new UserDto()
        {
            Id = userEntry.Id,
            UserName = userEntry.UserName,
            Email = userEntry.Email
        };
    }

    public async Task<UserDto?> GetUserByEmailAsync(string email)
    {
        var userEntry = await _dbContext.AppUsers
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email);
        if (userEntry is null) return null;

        return new UserDto()
        {
            Id = userEntry.Id,
            UserName = userEntry.UserName,
            Email = userEntry.Email
        };
    }

    public async Task<UserDto?> CreateUserAsync(CreateUserDto data)
    {
        var userEntry = _dbContext.AppUsers.Add(new()
        {
            UserName = data.Email[..data.Email.IndexOf('@')],
            Email = data.Email,
            Password = data.Password
        });

        try
        {
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            return null;
        }

        return new UserDto()
        {
            Id = userEntry.Entity.Id,
            UserName = userEntry.Entity.UserName,
            Email = userEntry.Entity.Email
        };
    }

    public async Task<bool> UpdateUserAsync(UpdateUserDto user)
    {
        var userEntry = await _dbContext.AppUsers.FindAsync(user.Id);
        if (userEntry is null) return false;

        if (userEntry.UserName != user.UserName)
        {
            userEntry.UserName = user.UserName;
        }
        await _dbContext.SaveChangesAsync();

        return true;
    }
}
