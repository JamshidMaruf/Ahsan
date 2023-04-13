using Ahsan.Domain.Entities;
using Ahsan.Service.DTOs.Issues;
using System.Linq.Expressions;

namespace Ahsan.Service.Interfaces
{
    public interface IIssueCategoryService
    {
        ValueTask<IssueCategoryForResultDto> CreateAsync(IssueCategoryForCreationDto dto);
        ValueTask<IssueCategoryForResultDto> UpdateAsync(long id, IssueCategoryForCreationDto dto);
        ValueTask<IssueCategoryForResultDto> GetAsync(Expression<Func<IssueCategory, bool>> expression);
        ValueTask<IEnumerable<IssueCategoryForResultDto>> GetAllAsync(Expression<Func<IssueCategory, bool>> expression = null, string search = null);
        ValueTask<bool> DeleteAsync(Expression<Func<IssueCategory, bool>> expression);
    }
}
