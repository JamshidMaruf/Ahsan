using System.ComponentModel.DataAnnotations;

namespace Ahsan.Service.DTOs.Companies;

public class CompanyForUpdateDto
{
    [Required]
    public long Id { get; set; }

    [Required]
    public string Name { get; set; }
}