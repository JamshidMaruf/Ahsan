using Ahsan.Service.DTOs.Issues;

namespace Ahsan.Service.Interfaces;

public interface IIssueService
{
    ValueTask<IssueForResultDto> CreateAsync(IssueForCreationDto dto);
    ValueTask<IssueForResultDto> UpdateAsync(IssueForUpdateDto dto);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<IssueForResultDto> GetByIdAsync(long id);
    ValueTask<IEnumerable<IssueForResultDto>> GetAllAsync(string search = null);
}
