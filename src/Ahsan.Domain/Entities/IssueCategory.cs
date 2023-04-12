using Ahsan.Domain.Commons;

namespace Ahsan.Domain.Entities;

public class IssueCategory : Auditable
{
    public string Name { get; set; }
    public long CompanyId { get; set; }
    public Company Company { get; set; }
}
