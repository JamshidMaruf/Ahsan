using Ahsan.Service.DTOs.CompanyEmployees;

namespace Ahsan.Service.DTOs.Issues;

public class IssueForResultDto
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Deadline { get; set; }
    public long Code { get; set; }
    public IssueCategoryForResultDto Category { get; set; }
    public CompanyEmployeeForResultDto AssignedUser { get; set; }
}
