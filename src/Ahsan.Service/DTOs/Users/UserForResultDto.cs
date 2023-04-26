using Ahsan.Domain.Enums;

namespace Ahsan.Service.DTOs.Users;

public class UserForResultDto
{
    public long Id { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Phone { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public UserRole Role { get; set; }
    public UserImageForResultDto Image { get; set; }
}
