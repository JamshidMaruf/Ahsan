using Microsoft.AspNetCore.Http;

namespace Ahsan.Service.DTOs.Users;

public class UserImageForCreationDto
{
    public IFormFile Image { get; set; }
    public long UserId { get; set; }
}
