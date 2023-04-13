using Ahsan.Domain.Entities;
using Ahsan.Service.DTOs.Positions;
using System.Linq.Expressions;

namespace Ahsan.Service.Interfaces
{
    public interface IPositionService
    {
        ValueTask<PositionForResultDto> CreateAsync(PositionForCreationDto dto);
        ValueTask<PositionForResultDto> UpdateAsync(long id, PositionForCreationDto dto);
        ValueTask<PositionForResultDto> GetAsync(Expression<Func<Position, bool>> expression);
        ValueTask<IEnumerable<PositionForResultDto>> GetAllAsync(Expression<Func<Position, bool>> expression = null, string search = null);
        ValueTask<bool> DeleteAsync(Expression<Func<Position, bool>> expression);
    }
}
