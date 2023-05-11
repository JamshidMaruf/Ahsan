using Ahsan.Domain.Commons;

namespace Ahsan.Domain.Entities;

public class UserImage : Auditable
{
    public string Name { get; set; }
    public string Path { get; set; }
    public long UserId { get; set; }
    public User User { get; set; }
}
