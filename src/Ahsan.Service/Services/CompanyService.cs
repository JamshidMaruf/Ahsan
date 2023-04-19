using Ahsan.Data.IRepositories;
using Ahsan.Domain.Entities;
using Ahsan.Service.DTOs.Companies;
using Ahsan.Service.Exceptions;
using Ahsan.Service.Interfaces;
using AutoMapper;
using System.Linq.Expressions;

#pragma warning disable
namespace Ahsan.Service.Services;

public class CompanyService : ICompanyService
{
    private readonly IMapper mapper;
    private readonly IRepository<Company> companyRepository;
    public CompanyService(IRepository<Company> repository, IMapper mapper)
    {
        this.mapper = mapper;
        this.companyRepository = repository;
    }

    public async ValueTask<CompanyForResultDto> CreateAsync(CompanyForCreationDto dto)
    {
        Company company = await this.companyRepository
            .SelectAsync(c => c.Name.ToLower() == dto.Name.ToLower());
        if (company is not null)
            throw new CustomException(409, "Company already exist for given argument");

        Company mappedCompany = this.mapper.Map<Company>(dto);
        Company result = await this.companyRepository.InsertAsync(mappedCompany);
        await this.companyRepository.SaveChangesAsync();
        return this.mapper.Map<CompanyForResultDto>(result);
    }

    public async ValueTask<bool> DeleteAsync(long companyId)
    {
        Company company = await this.companyRepository
            .SelectAsync(company => company.Id.Equals(companyId));
        if (company is null)
            throw new CustomException(404, "Company is not found for given id");

        bool result = await this.companyRepository.DeleteAsync(company);
        await this.companyRepository.SaveChangesAsync();
        return result;
    }

    public async ValueTask<IEnumerable<CompanyForResultDto>> GetAllAsync(
        Expression<Func<Company, bool>> expression = null, string search = null)
    {
        IQueryable<Company> companies = companyRepository
            .SelectAll(expression, new string[] { "User" }, isTracking: false);

        IEnumerable<CompanyForResultDto> result = 
            mapper.Map<IEnumerable<CompanyForResultDto>>(companies);

        if (!string.IsNullOrEmpty(search))
            return result.Where(c => 
                c.Name.ToLower().Contains(search.ToLower())).ToList();
        return result;
    }

    public async ValueTask<CompanyForResultDto> GetByIdAsync(long companyId)
    {
        Company company = await companyRepository
            .SelectAsync(company => company.Id.Equals(companyId));
        if (company is null)
            throw new CustomException(404, "Company not found for given id");
        return mapper.Map<CompanyForResultDto>(company);
    }

    public async ValueTask<CompanyForResultDto> ModifyAsync(CompanyForUpdateDto dto)
    {
        Company company = await companyRepository.SelectAsync(u => u.Id.Equals(dto.Id));
        if (company is null)
            throw new CustomException(404, "Company not found");

        this.mapper.Map(dto, company);
        company.UpdatedAt = DateTime.UtcNow;
        await companyRepository.SaveChangesAsync();
        return mapper.Map<CompanyForResultDto>(company);
    }
}
