using Ahsan.Domain.Entities;
using Ahsan.Service.DTOs.Issues;
using System.Linq.Expressions;

namespace Ahsan.Service.Interfaces;

public interface IIssueCategoryService
{
    ValueTask<IssueCategoryForResultDto> CreateAsync(IssueCategoryForCreationDto dto);
    ValueTask<IssueCategoryForResultDto> UpdateAsync(IssueCategoryForUpdateDto dto);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<IssueCategoryForResultDto> GetByIdAsync(long id);
    ValueTask<IEnumerable<IssueCategoryForResultDto>> GetAllAsync(
        Expression<Func<IssueCategory, bool>> expression = null, string search = null);
}
