using System.ComponentModel.DataAnnotations;

namespace Ahsan.Service.DTOs.Issues;

public class IssueCategoryForUpdateDto
{
    [Required]
    public long Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public long CompanyId { get; set; }
}
