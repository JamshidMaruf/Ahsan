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
        var company = await this.companyRepository
            .GetAsync(c => c.Name.ToLower() == dto.Name.ToLower());
        if (company is not null)
            throw new CustomException(403, "Company already exist");

        Company mappedCompany = this.mapper.Map<Company>(dto);
        var result = await this.companyRepository.InsertAsync(mappedCompany);
        await this.companyRepository.SaveChangesAsync();
        return this.mapper.Map<CompanyForResultDto>(result);
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var company = await this.companyRepository
            .GetAsync(company => company.Id.Equals(id));
        if (company is null)
            throw new CustomException(404, "Company not found");

        await companyRepository.DeleteAsync(company);
        await this.companyRepository.SaveChangesAsync();
        return true;
    }

    public async ValueTask<IEnumerable<CompanyForResultDto>> GetAllAsync(
        Expression<Func<Company, bool>> expression = null, string search = null)
    {
        var companies = companyRepository
            .GetAll(expression, new string[] { "User" }, isTracking: false);

        var result = mapper.Map<IEnumerable<CompanyForResultDto>>(companies);
        if (string.IsNullOrEmpty(search))
            return result
                .Where(c => c.Name.ToLower().Contains(search.ToLower())).ToList();

        return result;
    }

    public async ValueTask<CompanyForResultDto> GetByIdAsync(long id)
    {
        var company = await companyRepository.GetAsync(company => company.Id.Equals(id));
        if (company is null)
            throw new CustomException(404, "Company not found");
        return mapper.Map<CompanyForResultDto>(company);
    }

    public async ValueTask<CompanyForResultDto> UpdateAsync(CompanyForUpdateDto dto)
    {
        var company = await companyRepository.GetAsync(u => u.Id.Equals(dto.Id));
        if (company is null)
            throw new CustomException(404, "Company not found");

        this.mapper.Map(dto, company);
        company.UpdatedAt = DateTime.UtcNow;
        await companyRepository.SaveChangesAsync();
        return mapper.Map<CompanyForResultDto>(company);
    }
}
