namespace BlogPlatform.Dtos;

public class ErrorDto
{
    public int StatusCode { get; set; }
    public string? Message { get; set; }
    public string? Details { get; set; }
}
