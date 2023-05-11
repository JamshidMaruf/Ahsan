using System.ComponentModel.DataAnnotations;

namespace Ahsan.Service.DTOs.Positions;

public class PositionForCreationDto
{
    [Required]
    public string Name { get; set; }
}
