using Ahsan.Domain.Entities;
using Ahsan.Domain.Enums;

namespace Ahsan.Service.DTOs.CompanyEmployees
{
    public class CompanyEmployeeForUpdateDto
    {
        public long Id { get; set; }
        public long EmployeeId { get; set; }
        public long CompanyId { get; set; }
        public long PositionId { get; set; }
        public UserPermission Permission { get; set; }
    }
}
