using Ahsan.Domain.Configurations;
using Ahsan.Domain.Entities;
using Ahsan.Service.DTOs.Positions;
using System.Linq.Expressions;

namespace Ahsan.Service.Interfaces;

public interface IPositionService
{
    ValueTask<PositionForResultDto> CreateAsync(PositionForCreationDto dto);
    ValueTask<PositionForResultDto> UpdateAsync(PositionForUpdateDto dto);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<PositionForResultDto> GetByIdAsync(long id);
    ValueTask<IEnumerable<PositionForResultDto>> GetAllAsync(
          Expression<Func<Position, bool>> expression = null, string search = null, PaginationParams @params = null);
}
