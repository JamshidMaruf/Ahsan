using System.ComponentModel.DataAnnotations;

namespace Ahsan.Service.DTOs.Issues;

public class IssueForUpdateDto
{
    [Required]
    public long Id { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public long CategoryId { get; set; }

    [Required]
    public long CompanyId { get; set; }
    public string Description { get; set; }
    public long AssignedId { get; set; }
    public DateTime Deadline { get; set; }
}
