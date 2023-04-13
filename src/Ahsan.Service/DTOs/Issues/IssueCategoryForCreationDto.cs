using Ahsan.Domain.Entities;

namespace Ahsan.Service.DTOs.Issues
{
    public class IssueCategoryForCreationDto
    {
        public string Name { get; set; }
        public long CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
