using System.ComponentModel.DataAnnotations;

namespace Ahsan.Service.DTOs.Companies;

public class CompanyForCreationDto
{
    public string Name { get; set; }
    public long OwnerId { get; set; }
}
