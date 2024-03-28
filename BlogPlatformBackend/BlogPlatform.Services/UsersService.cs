using BlogPlatform.Data;
using BlogPlatform.Data.Entities;
using BlogPlatform.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogPlatform.Services;

public class UsersService(BlogContext dbContext) : IUsersService
{
    private readonly BlogContext _dbContext = dbContext;

    public async Task<UserDto?> GetUserAsync(int id)
    {
        var userEntry = await _dbContext.AppUsers
            .AsNoTracking()
            .SingleOrDefaultAsync(u => u.Id == id);
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
            .SingleOrDefaultAsync(u => u.Email == email);
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
        bool isEmailAlreadyUsed = await _dbContext.AppUsers.Where(u => u.Email == data.Email).AnyAsync();
        if (isEmailAlreadyUsed) return null;

        AppUser userToAdd = new()
        {
            UserName = data.Email[..data.Email.IndexOf('@')],
            Email = data.Email
        };

        PasswordHasher<AppUser> passwordHasher = new();
        string hashedPassword = passwordHasher.HashPassword(userToAdd, data.Password);
        userToAdd.Password = hashedPassword;

        var userEntry = _dbContext.AppUsers.Add(userToAdd);
        await _dbContext.SaveChangesAsync();

        return new UserDto()
        {
            Id = userEntry.Entity.Id,
            UserName = userEntry.Entity.UserName,
            Email = userEntry.Entity.Email
        };
    }

    public async Task<bool> UpdateUserAsync(UpdateUserDto user)
    {
        var userEntry = await _dbContext.AppUsers.SingleOrDefaultAsync(u => u.Id == user.Id);
        if (userEntry is null) return false;

        if (userEntry.UserName != user.UserName)
        {
            userEntry.UserName = user.UserName;
        }
        await _dbContext.SaveChangesAsync();

        return true;
    }
}
