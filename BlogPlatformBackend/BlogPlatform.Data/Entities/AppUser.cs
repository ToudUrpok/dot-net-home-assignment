using System.ComponentModel;

namespace BlogPlatform.Data.Entities;

public class AppUser
{
    public int Id { get; set; }
    public string UserName { get; set; }

    [PasswordPropertyText]
    public string Password { get; set; }
}