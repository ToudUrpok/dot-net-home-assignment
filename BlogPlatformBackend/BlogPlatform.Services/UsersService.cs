using BlogPlatform.Data;
using BlogPlatform.Data.Entities;
using BlogPlatform.Dtos;
using BlogPlatform.Services.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogPlatform.Services;

public class UsersService(BlogContext dbContext) : IUsersService
{
    private readonly BlogContext _dbContext = dbContext;

    public async Task<UserDto> GetUserAsync(int id)
    {
        var userEntry = await _dbContext.AppUsers
        .AsNoTracking()
        .SingleOrDefaultAsync(u => u.Id == id) ??
            throw new EntityNotFoundException($"The user with id={id} is not found.");

        return new UserDto()
        {
            Id = userEntry.Id,
            UserName = userEntry.UserName,
            Email = userEntry.Email
        };
    }

    public async Task<UserDto> GetUserByEmailAsync(string email)
    {
        var userEntry = await _dbContext.AppUsers
        .AsNoTracking()
        .SingleOrDefaultAsync(u => u.Email == email) ??
            throw new EntityNotFoundException($"The user with email={email} is not found.");

        return new UserDto()
        {
            Id = userEntry.Id,
            UserName = userEntry.UserName,
            Email = userEntry.Email
        };
    }

    public async Task<UserDto> CreateUserAsync(CreateUserDto data)
    {
        bool isEmailAlreadyUsed = await _dbContext.AppUsers.AnyAsync(u => u.Email == data.Email);
        if (isEmailAlreadyUsed) throw new EntityAlreadyExistsException($"Failed to create the user. The user with email={data.Email} is already exist.");

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

    public async Task<UserDto> UpdateUserAsync(UpdateUserDto user)
    {
        var userEntry = await _dbContext.AppUsers.SingleOrDefaultAsync(u => u.Id == user.Id) ??
            throw new EntityNotFoundException($"Failed to update the user with id={user.Id} because such user doesn't exist.");

        if (userEntry.UserName != user.UserName)
        {
            userEntry.UserName = user.UserName;
        }
        await _dbContext.SaveChangesAsync();

        return new UserDto()
        {
            Id = userEntry.Id,
            UserName = userEntry.UserName,
            Email = userEntry.Email
        };
    }
}
