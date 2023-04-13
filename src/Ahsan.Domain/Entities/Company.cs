using Ahsan.Domain.Commons;

namespace Ahsan.Domain.Entities;

public class Company : Auditable
{
    public string Name { get; set; }
    public long OwnerId { get; set; }
    public User Owner { get; set; }
    public ICollection<CompanyEmployee> Employees { get; set; }
    public ICollection<IssueCategory> IssueCategories { get; set; }
    public ICollection<Issue> Issues { get; set; }
}
