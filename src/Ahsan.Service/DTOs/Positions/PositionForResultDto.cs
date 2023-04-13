using System.ComponentModel.DataAnnotations;

namespace Ahsan.Service.DTOs.Positions
{
    public class PositionForResultDto
    {
        [Required, MaxLength(50)]
        public string Name { get; set; }
    }
}
