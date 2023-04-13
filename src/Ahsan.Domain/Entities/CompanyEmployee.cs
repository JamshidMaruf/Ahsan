using Ahsan.Domain.Commons;
using Ahsan.Domain.Enums;

namespace Ahsan.Domain.Entities;

public class CompanyEmployee : Auditable
{
    public long EmployeeId { get; set; }
    public User Employee { get; set; }

    public long CompanyId { get; set; }
    public Company Company { get; set; }

    public long PositionId { get; set; }
    public Position Position { get; set; }
    public UserPermission Permission { get; set; }

    public ICollection<Issue> Assignments { get; set; }
}
