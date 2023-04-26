using Ahsan.Domain.Configurations;
using Ahsan.Domain.Entities;
using Ahsan.Service.DTOs.Issues;
using System.Linq.Expressions;

namespace Ahsan.Service.Interfaces;

public interface IIssueService
{
    ValueTask<IssueForResultDto> CreateAsync(IssueForCreationDto dto);
    ValueTask<IssueForResultDto> UpdateAsync(IssueForUpdateDto dto);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<IssueForResultDto> GetByIdAsync(long id);
    ValueTask<IEnumerable<IssueForResultDto>> GetAllAsync(PaginationParams @params, string search = null);
}
