using Ahsan.Domain.Entities;
using Ahsan.Service.DTOs.Companies;
using System.Linq.Expressions;

namespace Ahsan.Service.Interfaces
{
    public interface ICompanyService
    {
        ValueTask<CompanyForResultDto> CreateAsync(CompanyForCreationDto dto);
        ValueTask<CompanyForResultDto> UpdateAsync(long id, CompanyForResultDto dto);
        ValueTask<CompanyForResultDto> GetAsync(Expression<Func<Company, bool>> expression);
        ValueTask<IEnumerable<CompanyForResultDto>> GetAllAsync(Expression<Func<Company, bool>> expression = null, string search = null);
        ValueTask<bool> DeleteAsync(Expression<Func<Company, bool>> expression);
    }
}
