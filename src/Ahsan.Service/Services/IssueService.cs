using AutoMapper;
using Ahsan.Domain.Entities;
using Ahsan.Data.IRepositories;
using Ahsan.Service.Exceptions;
using Ahsan.Service.Interfaces;
using Ahsan.Service.DTOs.Issues;

namespace Ahsan.Service.Services;

public class IssueService : IIssueService
{
    private readonly IMapper mapper;
    private readonly IRepository<Issue> issueRepository;
    private readonly ICompanyEmployeeService employeeService;
    private readonly IIssueCategoryService issueCategoryService;

    public IssueService(
        IMapper mapper,
        IRepository<Issue> repository,
        ICompanyEmployeeService employeeService,
        IIssueCategoryService issueCategoryService,
        IRepository<User> userRepository)
    {
        this.mapper = mapper;
        this.issueRepository = repository;
        this.employeeService = employeeService;
        this.issueCategoryService = issueCategoryService;
    }

    public async ValueTask<IssueForResultDto> CreateAsync(IssueForCreationDto dto)
    {
        var CountOfCompanyIssues = this.issueRepository.SelectAll(t => t.CompanyId == dto.CompanyId).Count();
        var issue = this.mapper.Map<Issue>(dto);
        issue.Code = ++CountOfCompanyIssues;
        issue.CreatedAt = DateTime.UtcNow;
        var createdIssue = await this.issueRepository.InsertAsync(issue);
        await this.issueRepository.SaveChangesAsync();

        var result = this.mapper.Map<IssueForResultDto>(issue);
        result.Category = await this.issueCategoryService.GetByIdAsync(issue.CategoryId);
        result.AssignedUser = await this.employeeService.GetByIdAsync(issue.AssignedId);

        return result;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var issue = await this.issueRepository.SelectAsync(i => i.Id == id && !i.IsDeleted);
        if (issue is null)
            throw new CustomException(404, "Issue is not found");

        await issueRepository.DeleteAsync(issue);
        await this.issueRepository.SaveChangesAsync();
        return true;
    }

    public async ValueTask<IEnumerable<IssueForResultDto>> GetAllAsync(string search = null)
    {
        var issues = this.issueRepository.SelectAll(t => !t.IsDeleted, isTracking: false);
        var results = this.mapper.Map<IEnumerable<IssueForResultDto>>(issues);

        if (!string.IsNullOrEmpty(search))
        {
            var matchingIssues = results
                    .Where(t => t.Title.ToLower() == search || t.Description.Contains(search));
            return matchingIssues;
        }

        foreach (var item in results)
        {
            item.AssignedUser = await employeeService.GetByIdAsync(item.AssignedId);
            item.Category = await issueCategoryService.GetByIdAsync(item.CategoryId);
        }
        return results;
    }

    public async ValueTask<IssueForResultDto> GetByIdAsync(long id)
    {
        var issue = await this.issueRepository.SelectAsync(i => i.Id == id && !i.IsDeleted);
        if (issue is null)
            throw new CustomException(404, "Issue is not found");

        var result = this.mapper.Map<IssueForResultDto>(issue);
        result.Category = await this.issueCategoryService.GetByIdAsync(issue.CategoryId);
        result.AssignedUser = await this.employeeService.GetByIdAsync(issue.AssignedId);

        return result;
    }

    public async ValueTask<IssueForResultDto> UpdateAsync(IssueForUpdateDto dto)
    {
        var updatingIssue = await this.issueRepository.SelectAsync(u => u.Id == dto.Id && !u.IsDeleted);
        if (updatingIssue is null)
            throw new CustomException(404, "Issue is not found");

        var issue = mapper.Map(dto, updatingIssue);
        issue.UpdatedAt = DateTime.UtcNow;
        await this.issueRepository.UpdateAsync(issue);
        await this.issueRepository.SaveChangesAsync();

        var result = this.mapper.Map<IssueForResultDto>(issue);
        result.Category = await this.issueCategoryService.GetByIdAsync(issue.CategoryId);
        result.AssignedUser = await this.employeeService.GetByIdAsync(issue.AssignedId);

        return result;
    }
}