namespace BlogPlatform.Services.Exceptions;

public sealed class UserLoginException(string message) : Exception(message)
{
}
