//using Ahsan.Data.IRepositories;
//using Ahsan.Domain.Entities;
//using Ahsan.Service.DTOs.Issues;
//using Ahsan.Service.Exceptions;
//using Ahsan.Service.Interfaces;
//using AutoMapper;
//using Microsoft.EntityFrameworkCore;
//using System.Linq.Expressions;

//namespace Ahsan.Service.Services;

//public class IssueService : IIssueService
//{
//    private readonly IRepository<Issue> _issueRepository;
//    private readonly IIssueCategoryService _issueCategoryService;
//    private readonly IUserService _userService;
//    private readonly IMapper _mapper;

//        public IssueService(
//            IRepository<Issue> repository,
//            IIssueCategoryService issueCategoryService,
//            IUserService userService,
//            IMapper mapper
//            )
//        {
//            this._issueRepository = repository;
//            this._issueCategoryService = issueCategoryService;
//            this._userService = userService;
//            this._mapper = mapper;
//        }
//        public async ValueTask<IssueForResultDto> CreateAsync(IssueForCreationDto dto)
//        {
//            var issue = await this._issueRepository.GetAsync(u => u.Title == dto.Title);

//            if (issue is not null)
//            {
//                throw new CustomException(403, "issue already exist");
//            }

//            var user = await this._userService.GetByIdAsync(dto.AssignedId);
//            var issueCategory = await this._issueCategoryService.GetByIdAsync(dto.CategoryId);
//            if (user is not null && issueCategory is not null)
//            {
//                Issue mappedUser = _mapper.Map<Issue>(dto);
//                try
//                {
//                    var result = await this._issueRepository.InsertAsync(mappedUser);
//                    await this._issueRepository.SaveChangesAsync();

//                return this._mapper.Map<IssueForResultDto>(result);
//            }

//                catch (Exception)
//                {
//                    throw new CustomException(500, "Something went wrong");
//                }
//            }
//            else
//            {
//                throw new CustomException(400, "Try again!");
//            }
//        }

//        public async ValueTask<bool> DeleteAsync(long id)
//        {
//            var issue = await this._issueRepository.GetAsync(i => i.Id == id);

//            if (issue is null)
//            {
//                throw new CustomException(404, "Issue not found");
//            }

//        await _issueRepository.DeleteAsync(issue);

//        await this._issueRepository.SaveChangesAsync();

//        return true;
//    }

//    public async ValueTask<IEnumerable<IssueForResultDto>> GetAllAsync(Expression<Func<Issue, bool>> expression = null, string search = null)
//    {
//        var issues = this._issueRepository.GetAll(expression, new string[] { "IssueCategory", "Company", "CompanyEmployee" }, isTracking: false);

//            var matchingIssues = await issues.Where(
//                c => c.Title.ToLower() == search ||
//                c.Description.Contains(search)).ToListAsync();

//        try
//        {
//            var result = _mapper.Map<IEnumerable<IssueForResultDto>>(matchingIssues);
//            return result;
//        }

//            catch
//            {
//                throw new CustomException(500, "Something went wromg");
//            }
//        }

//        public async ValueTask<IssueForResultDto> GetByIdAsync(long id)
//        {
//            var issue = await this._issueRepository.GetAsync(i => i.Id == id);

//            if (issue is null)
//                throw new CustomException(404, "Issue not found");

//        try
//        {
//            var result = _mapper.Map<IssueForResultDto>(issue);
//            return result;
//        }

//            catch
//            {
//                throw new CustomException(500, "Something went wrong");
//            }
//        }

//        public async ValueTask<IssueForResultDto> UpdateAsync(IssueForUpdateDto dto)
//        {
//            var updatingIssue = await this._issueRepository.GetAsync(u => u.Id == dto.Id);

//            if (updatingIssue is null)
//            {
//                throw new CustomException(404, "Issue not found");
//            }

//        var issue = _mapper.Map<Issue>(dto);

//        issue.UpdatedAt = DateTime.UtcNow;

//        await this._issueRepository.UpdateAsync(issue);

//        await this._issueRepository.SaveChangesAsync();

//        return _mapper.Map<IssueForResultDto>(issue);
//    }
//}
