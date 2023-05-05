
using Ahsan.Data.IRepositories;
using Ahsan.Domain.Configurations;
using Ahsan.Domain.Entities;
using Ahsan.Service.DTOs.Issues;
using Ahsan.Service.Exceptions;
using Ahsan.Service.Extensions;
using Ahsan.Service.Interfaces;
using AutoMapper;
using System.Runtime.InteropServices;

namespace Ahsan.Service.Services;

public class IssueService : IIssueService
{
    private readonly IMapper mapper;
    private readonly IUserService userService;
    private readonly IRepository<Issue> issueRepository;
    private readonly IIssueCategoryService issueCategoryService;

    public IssueService(
        IMapper mapper,
        IUserService userService,
        IRepository<Issue> repository,
        IIssueCategoryService issueCategoryService
        )
    {
        this.mapper = mapper;
        this.userService = userService;
        this.issueRepository = repository;
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
        return this.mapper.Map<IssueForResultDto>(createdIssue);
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

    public async ValueTask<IEnumerable<IssueForResultDto>> GetAllAsync(PaginationParams @params, string search = null)
    {
        var issues = this.issueRepository
            .SelectAll(t => !t.IsDeleted, includes: new string[] { "IssueCategory", "Company", "CompanyEmployee" }, isTracking: false);
        if (!string.IsNullOrEmpty(search))
        {
            var matchingIssues = issues
                    .Where(t => !t.IsDeleted)
                    .Where(t => t.Title.ToLower() == search || t.Description.Contains(search));
            return (IEnumerable<IssueForResultDto>)matchingIssues.ToPagedList(@params);
        }
        return (IEnumerable<IssueForResultDto>)issues.ToPagedList(@params);
    }

    public async ValueTask<IssueForResultDto> GetByIdAsync(long id)
    {
        var issue = await this.issueRepository.SelectAsync(i => i.Id == id && !i.IsDeleted);
        if (issue is null)
            throw new CustomException(404, "Issue is not found");

            return this.mapper.Map<IssueForResultDto>(issue);
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
        return mapper.Map<IssueForResultDto>(issue);
    }
}
