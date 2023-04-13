using Ahsan.Data.IRepositories;
using Ahsan.Domain.Entities;
using Ahsan.Service.DTOs.Issues;
using Ahsan.Service.Exceptions;
using Ahsan.Service.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ahsan.Service.Services
{
    public class IssueService : IIssueService
    {
        private readonly IRepository<Issue> _issueRepository;
        private readonly IIssueCategoryService _issueCategoryService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public IssueService(
            IRepository<Issue> repository,
            IIssueCategoryService issueCategoryService,
            IUserService userService,
            IMapper mapper
            )
        {
            this._issueRepository = repository;
            this._issueCategoryService = issueCategoryService;
            this._userService = userService;
            this._mapper = mapper;
        }
        public async ValueTask<IssueForResultDto> CreateAsync(IssueForCreationDto dto)
        {
            var issue = await this._issueRepository.GetAsync(u => u.Number == dto.Number);

            if (issue is not null)
            {
                throw new AhsanException(403, "issue already exist");
            }

            var user = await this._userService.GetAsync(u => u.Id == dto.AssignedId);
            var issueCategory = await this._issueCategoryService.GetAsync(u => u.Id == dto.CategoryId);
            if (user is not null && issueCategory is not null)
            {
                Issue mappedUser = _mapper.Map<Issue>(dto);
                try
                {
                    var result = await this._issueRepository.InsertAsync(mappedUser);
                    await this._issueRepository.SaveChangesAsync();

                    return this._mapper.Map<IssueForResultDto>(result);
                }

                catch (Exception)
                {
                    throw new AhsanException(500, "Something went wrong");
                }
            }
            else
            {
                throw new AhsanException(400, "Try again!");
            }
        }

        public async ValueTask<bool> DeleteAsync(Expression<Func<Issue, bool>> expression)
        {
            var issue = await this._issueRepository.GetAsync(expression);

            if (issue is null)
            {
                throw new AhsanException(404, "Issue not found");
            }

            await _issueRepository.DeleteAsync(issue);

            await this._issueRepository.SaveChangesAsync();

            return true;
        }

        public async ValueTask<IEnumerable<IssueForResultDto>> GetAllAsync(Expression<Func<Issue, bool>> expression = null, string search = null)
        {
            var issues = this._issueRepository.GetAll(expression, new string[] { "IssueCategory", "Company", "CompanyEmployee" }, isTracking: false);

            var matchingIssues = await issues.Where(
                c => c.Title.ToLower() == search ||
                c .Description.Contains(search) ||
                c.Number.ToString() == search).ToListAsync();

            try
            {
                var result = _mapper.Map<IEnumerable<IssueForResultDto>>(matchingIssues);
                return result;
            }

            catch
            {
                throw new AhsanException(500, "Something went wromg");
            }
        }

        public async ValueTask<IssueForResultDto> GetAsync(Expression<Func<Issue, bool>> expression)
        {
            var issue = await this._issueRepository.GetAsync(expression);

            if (issue is null)
                throw new AhsanException(404, "Issue not found");

            try
            {
                var result = _mapper.Map<IssueForResultDto>(issue);
                return result;
            }

            catch
            {
                throw new AhsanException(500, "Something went wrong");
            }
        }

        public async ValueTask<IssueForResultDto> UpdateAsync(long id, IssueForCreationDto dto)
        {
            var updatingIssue = await this._issueRepository.GetAsync(u => u.Id == id);

            if (updatingIssue is null)
            {
                throw new AhsanException(404, "Issue not found");
            }

            var issue = _mapper.Map<Issue>(dto);

            issue.UpdatedAt = DateTime.UtcNow;

            await this._issueRepository.UpdateAsync(issue);

            await this._issueRepository.SaveChangesAsync();

            return _mapper.Map<IssueForResultDto>(issue);
        }
    }
}
