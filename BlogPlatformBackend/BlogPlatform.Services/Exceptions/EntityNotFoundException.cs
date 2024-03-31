namespace BlogPlatform.Services.Exceptions;

public class EntityNotFoundException(string message) : Exception(message)
{
}
