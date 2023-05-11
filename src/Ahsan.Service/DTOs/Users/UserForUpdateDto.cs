using System.ComponentModel.DataAnnotations;

namespace Ahsan.Service.DTOs.Users;

public class UserForUpdateDto
{
    [Required]
    public long Id { get; set; }

    [Required]
    public string Firstname { get; set; }

    [Required]
    public string Lastname { get; set; }

    [Required]
    public string Username { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
}
