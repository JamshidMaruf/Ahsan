using System.ComponentModel.DataAnnotations;

namespace Ahsan.Service.DTOs.Issues;

public class IssueCategoryForCreationDto
{
    [Required]
    public string Name { get; set; }

    [Required]
    public long CompanyId { get; set; }
}
