using Ahsan.Domain.Entities;
using Ahsan.Service.DTOs.Issues;
using System.Linq.Expressions;

namespace Ahsan.Service.Interfaces
{
    public interface IIssueService
    {
        ValueTask<IssueForResultDto> CreateAsync(IssueForCreationDto dto);
        ValueTask<IssueForResultDto> UpdateAsync(long id, IssueForCreationDto dto);
        ValueTask<IssueForResultDto> GetAsync(Expression<Func<Issue, bool>> expression);
        ValueTask<IEnumerable<IssueForResultDto>> GetAllAsync(Expression<Func<Issue, bool>> expression = null, string search = null);
        ValueTask<bool> DeleteAsync(Expression<Func<Issue, bool>> expression);
    }
}
