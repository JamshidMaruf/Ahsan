using Ahsan.Domain.Commons;
using Ahsan.Domain.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ahsan.Domain.Entities;

public class User : Auditable
{
    [MaxLength(50)]
    public string Firstname { get; set; }
    [MaxLength(50)]
    public string Lastname { get; set; }
    [Required, MaxLength(50)]
    public string Phone { get; set; }
    [MinLength(5), MaxLength(50)]
    public string Username { get; set; }
    [Required, MinLength(5), MaxLength(50)]
    public string Password { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    public UserRole Role { get; set; } = UserRole.User;
}
