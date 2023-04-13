using Ahsan.Domain.Entities;
using Ahsan.Service.DTOs.Companies;
using System.Linq.Expressions;

namespace Ahsan.Service.Interfaces;

public interface ICompanyService
{
    ValueTask<CompanyForResultDto> CreateAsync(CompanyForCreationDto dto);
    ValueTask<CompanyForResultDto> UpdateAsync(CompanyForUpdateDto dto);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<CompanyForResultDto> GetByIdAsync(long id);
    ValueTask<IEnumerable<CompanyForResultDto>> GetAllAsync(
        Expression<Func<Company, bool>> expression = null, string search = null);
}
