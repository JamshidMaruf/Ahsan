using Ahsan.Domain.Entities;

namespace Ahsan.Service.DTOs.Issues
{
    public class IssueCategoryForResultDto
    {
        public string Name { get; set; }
        public long CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
