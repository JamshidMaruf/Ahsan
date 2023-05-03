using Ahsan.Domain.Enums;
using Ahsan.Service.DTOs.Companies;
using Ahsan.Service.DTOs.Issues;
using Ahsan.Service.DTOs.Positions;
using Ahsan.Service.DTOs.Users;

namespace Ahsan.Service.DTOs.CompanyEmployees
{
    public class CompanyEmployeeForResultDto
    {
        public long Id { get; set; }
        public UserForResultDto Employee { get; set; }

        public CompanyForResultDto Company { get; set; }

        public long PositionId { get; set; }
        public PositionForResultDto Position { get; set; }

        public UserPermission Permission { get; set; }
        public List<IssueForResultDto> Assignments { get; set; }
    }
}
