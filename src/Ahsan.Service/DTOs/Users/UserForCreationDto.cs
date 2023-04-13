using System.ComponentModel.DataAnnotations;

namespace Ahsan.Service.DTOs.Users
{
    public class UserForCreationDto
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
    }
}
