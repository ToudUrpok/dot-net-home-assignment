using BlogPlatform.Dtos;

namespace BlogPlatform.Services;

public interface ILoginService
{
    Task<string> Login(LoginDto data);
}
