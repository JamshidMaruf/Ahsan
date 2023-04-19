using Ahsan.Domain.Commons;
using Ahsan.Domain.Enums;

namespace Ahsan.Domain.Entities;

public class User : Auditable
{
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Phone { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public UserRole Role { get; set; } = UserRole.User;
    public ICollection<Company> Companies { get; set; }
}
