using System.ComponentModel.DataAnnotations;

namespace Ahsan.Service.DTOs.Positions;

public class PositionForUpdateDto
{
    [Required]
    public long Id { get; set; }

    [Required]
    public string Name { get; set; }
}
