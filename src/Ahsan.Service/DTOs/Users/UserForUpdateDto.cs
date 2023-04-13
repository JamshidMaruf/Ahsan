using System.ComponentModel.DataAnnotations;

namespace Ahsan.Service.DTOs.Users
{
    public class UserForUpdateDto
    {
        [Required,MaxLength(50)]
        public string Firstname { get; set; }

        [Required, MaxLength(50)]
        public string Lastname { get; set; }

        [Required, MaxLength(50)]
        public string Phone { get; set; }

        [Required, MinLength(5), MaxLength(50)]
        public string Username { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }
    }
}
