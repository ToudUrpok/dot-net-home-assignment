namespace BlogPlatform.Services.Exceptions;

public class EntityAlreadyExistsException(string message) : Exception(message)
{
}
