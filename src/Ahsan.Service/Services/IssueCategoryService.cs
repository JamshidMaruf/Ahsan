using Ahsan.Data.IRepositories;
using Ahsan.Domain.Entities;
using Ahsan.Service.DTOs.Companies;
using Ahsan.Service.DTOs.Issues;
using Ahsan.Service.DTOs.Users;
using Ahsan.Service.Exceptions;
using Ahsan.Service.Interfaces;
using AutoMapper;
using System.Linq.Expressions;

#pragma warning disable
namespace Ahsan.Service.Services;

public class IssueCategoryService : IIssueCategoryService
{
    private readonly IMapper mapper;
    private readonly IRepository<Company> companyRepository;
    private readonly IRepository<IssueCategory> IssueCategoryRepository;
    public IssueCategoryService(
        IMapper mapper,
        IRepository<IssueCategory> repository,
        IRepository<Company> companyRepository)
    {
        this.mapper = mapper;
        this.IssueCategoryRepository = repository;
        this.companyRepository = companyRepository;
    }

    public async ValueTask<IssueCategoryForResultDto> CreateAsync(IssueCategoryForCreationDto dto)
    {
        IssueCategory issueCategory = await this.IssueCategoryRepository
            .SelectAsync(u => u.Name.ToLower() == dto.Name.ToLower() && u.CompanyId == dto.CompanyId && !u.IsDeleted);
        if (issueCategory is not null)
            throw new CustomException(403, "IssueCategory already exist");

        var company = await this.companyRepository
            .SelectAsync(t => t.Id.Equals(dto.CompanyId) && !t.IsDeleted);
        if (company is null)
            throw new CustomException(404, "Company is not found");

        var mappedIssueCategory = this.mapper.Map<IssueCategory>(dto);
        var createdIssueCategory = await this.IssueCategoryRepository.InsertAsync(mappedIssueCategory);
        await this.IssueCategoryRepository.SaveChangesAsync();

        var result = this.mapper.Map<IssueCategoryForResultDto>(createdIssueCategory);
        result.Company = this.mapper.Map<CompanyForResultDto>(company);
        return result;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var issueCategory = await this.IssueCategoryRepository
            .SelectAsync(issueCategory => issueCategory.Id == id);
        if (issueCategory is null)
            throw new CustomException(404, "IssueCategory not found");

        await this.IssueCategoryRepository.DeleteAsync(issueCategory);
        await this.IssueCategoryRepository.SaveChangesAsync();
        return true;
    }

    public async ValueTask<IEnumerable<IssueCategoryForResultDto>> GetAllAsync(
        Expression<Func<IssueCategory, bool>> expression = null, string search = null)
    {
        var issueCategories = this.IssueCategoryRepository
            .SelectAll(expression, new string[] { "Company" }, isTracking: false);

        var result = this.mapper.Map<IEnumerable<IssueCategoryForResultDto>>(issueCategories);
        if (!string.IsNullOrEmpty(search))
            return result
                .Where(c => c.Name.ToLower() == search).ToList();

        return result;
    }

    public async ValueTask<IssueCategoryForResultDto> GetByIdAsync(long id)
    {
        var issueCategory = await IssueCategoryRepository
            .SelectAsync(issueCategory => issueCategory.Id.Equals(id));
        if (issueCategory is null)
            throw new CustomException(404, "IssueCategory not found");
        return mapper.Map<IssueCategoryForResultDto>(issueCategory);
    }

    public async ValueTask<IssueCategoryForResultDto> UpdateAsync(IssueCategoryForUpdateDto dto)
    {
        var updatingIssueCategory = await IssueCategoryRepository
            .SelectAsync(issueCategory => issueCategory.Id.Equals(dto.Id));
        if (updatingIssueCategory is null)
            throw new CustomException(404, "IssueCategory not found");

        this.mapper.Map(dto, updatingIssueCategory);
        updatingIssueCategory.UpdatedAt = DateTime.UtcNow;
        await IssueCategoryRepository.SaveChangesAsync();
        return mapper.Map<IssueCategoryForResultDto>(updatingIssueCategory);
    }
}
