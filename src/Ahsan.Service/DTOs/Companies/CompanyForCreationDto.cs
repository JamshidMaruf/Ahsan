using Ahsan.Domain.Entities;

namespace Ahsan.Service.DTOs.Companies
{
    public class CompanyForCreationDto
    {
        public string Name { get; set; }
        public long OwnerId { get; set; }
        public User Owner { get; set; }
    }
}
