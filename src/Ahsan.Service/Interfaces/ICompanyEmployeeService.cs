using Ahsan.Domain.Configurations;
using Ahsan.Domain.Entities;
using Ahsan.Service.DTOs.CompanyEmployees;
using System.Linq.Expressions;

namespace Ahsan.Service.Interfaces;

public interface ICompanyEmployeeService
{
    /// <summary>
    /// Register company employee
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    ValueTask<CompanyEmployeeForResultDto> CreateAsync(CompanyEmployeeForCreationDto dto);
    /// <summary>
    /// Update copmany employee
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    ValueTask<CompanyEmployeeForResultDto> ModifyAsync(CompanyEmployeeForUpdateDto dto);
    /// <summary>
    /// Remove from company employee
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    ValueTask<bool> DeleteAsync(long id);
    /// <summary>
    /// Get employee information
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    ValueTask<CompanyEmployeeForResultDto> GetByIdAsync(long id);
    /// <summary>
    /// Get all employees
    /// </summary>
    /// <param name="expression"></param>
    /// <param name="search"></param>
    /// <returns></returns>
    ValueTask<IEnumerable<CompanyEmployeeForResultDto>> GetAllAsync(PaginationParams @params = null, string search = null);
}
