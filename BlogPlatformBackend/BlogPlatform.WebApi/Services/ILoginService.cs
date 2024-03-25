using BlogPlatform.Dtos;

namespace BlogPlatform.WebApi.Services;

public interface ILoginService
{
    Task<string?> Login(LoginDto data);
}
