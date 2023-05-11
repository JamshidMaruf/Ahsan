using Ahsan.Service.DTOs.Companies;

namespace Ahsan.Service.DTOs.Issues;

public class IssueCategoryForResultDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public CompanyForResultDto Company { get; set; }
}
