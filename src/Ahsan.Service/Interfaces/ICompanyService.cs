using Ahsan.Domain.Entities;
using Ahsan.Service.DTOs.Companies;
using System.Linq.Expressions;

namespace Ahsan.Service.Interfaces;

public interface ICompanyService
{
    /// <summary>
    /// Create company
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    ValueTask<CompanyForResultDto> CreateAsync(CompanyForCreationDto dto);

    /// <summary>
    /// Update company
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    ValueTask<CompanyForResultDto> ModifyAsync(CompanyForUpdateDto dto);

    /// <summary>
    /// Delete company with given id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    ValueTask<bool> DeleteAsync(long companyId);

    /// <summary>
    /// Get company via given id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    ValueTask<CompanyForResultDto> GetByIdAsync(long companyId);

    /// <summary>
    /// Get company list
    /// </summary>
    /// <param name="expression"></param>
    /// <param name="search"></param>
    /// <returns></returns>
    ValueTask<List<CompanyForResultDto>> GetAllAsync(string search = null);
}
