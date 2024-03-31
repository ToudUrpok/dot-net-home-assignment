using BlogPlatform.Dtos;
using BlogPlatform.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BlogPlatform.Data.Entities;
using Microsoft.AspNetCore.Identity;
using BlogPlatform.Services.Exceptions;

namespace BlogPlatform.Services;

public class LoginService(BlogContext dbContext, IConfiguration configuration) : ILoginService
{
    private readonly BlogContext _dbContext = dbContext;
    private readonly IConfiguration _configuration = configuration;
    private readonly PasswordHasher<AppUser> _passwordHasher = new();

    public async Task<string> Login(LoginDto data)
    {
        var user = await _dbContext.AppUsers
        .AsNoTracking()
        .SingleOrDefaultAsync(u => u.Email == data.Email) ??
            throw new EntityNotFoundException($"The user with email={data.Email} is not found.");

        var result = _passwordHasher.VerifyHashedPassword(user, user.Password, data.Password);
        if (result == PasswordVerificationResult.Failed) throw new UserLoginException("Invalid password.");

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"] ?? "defaultkey");
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"],
            Subject = new ClaimsIdentity(new Claim[]
            {
                new(ClaimTypes.Name, user.UserName),
                new(ClaimTypes.Email, user.Email)
            }),
            Expires = DateTime.UtcNow.AddMinutes(30),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
