using Ahsan.Domain.Commons;
using System.ComponentModel.DataAnnotations;

namespace Ahsan.Domain.Entities;

public class Position : Auditable
{
    [Required, MaxLength(50)]
    public string Name { get; set; }
}
