using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BlogPlatform.Data.Entities;

[Index(nameof(Email), IsUnique = true)]
public class AppUser
{
    public int Id { get; set; }

    [MaxLength(64)]
    public string UserName { get; set; }

    [MaxLength(320)]
    public string Email {  get; set; }
    public string Password { get; set; }
}