using System.ComponentModel.DataAnnotations;

namespace Ahsan.Service.DTOs.Positions
{
    public class PositionForCreationDto
    {
        [Required, MaxLength(50)]
        public string Name { get; set; }
    }
}
