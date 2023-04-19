//using Ahsan.Data.IRepositories;
//using Ahsan.Domain.Entities;
//using Ahsan.Service.DTOs.Issues;
//using Ahsan.Service.Exceptions;
//using Ahsan.Service.Interfaces;
//using AutoMapper;
//using System.Linq.Expressions;

//#pragma warning disable
//namespace Ahsan.Service.Services;

//public class IssueCategoryService : IIssueCategoryService
//{
//    private readonly IRepository<IssueCategory> IssueCategoryRepository;
//    private readonly IMapper mapper;
//    public IssueCategoryService(IRepository<IssueCategory> repository, IMapper mapper)
//    {
//        this.IssueCategoryRepository = repository;
//        this.mapper = mapper;
//    }

//    public async ValueTask<IssueCategoryForResultDto> CreateAsync(IssueCategoryForCreationDto dto)
//    {
//        IssueCategory issueCategory = await this.IssueCategoryRepository
//            .GetAsync(u => u.Name.ToLower() == dto.Name.ToLower() && u.CompanyId == dto.CompanyId);
//        if (issueCategory is not null)
//            throw new CustomException(403, "IssueCategory already exist");

//        var mappedIssueCategory = this.mapper.Map<IssueCategory>(dto);
//        var result = await this.IssueCategoryRepository.InsertAsync(mappedIssueCategory);
//        await this.IssueCategoryRepository.SaveChangesAsync();
//        return this.mapper.Map<IssueCategoryForResultDto>(result);
//    }

//    public async ValueTask<bool> DeleteAsync(long id)
//    {
//        var issueCategory = await this.IssueCategoryRepository
//            .GetAsync(issueCategory => issueCategory.Id == id);
//        if (issueCategory is null)
//            throw new CustomException(404, "IssueCategory not found");

//        await this.IssueCategoryRepository.DeleteAsync(issueCategory);
//        await this.IssueCategoryRepository.SaveChangesAsync();
//        return true;
//    }

//    public async ValueTask<IEnumerable<IssueCategoryForResultDto>> GetAllAsync(
//        Expression<Func<IssueCategory, bool>> expression = null, string search = null)
//    {
//        var issueCategories = IssueCategoryRepository
//            .GetAll(expression, new string[] { "Company" }, isTracking: false);

//        var result = this.mapper.Map<IEnumerable<IssueCategoryForResultDto>>(issueCategories);
//        if (string.IsNullOrEmpty(search))
//            return result
//                .Where(c => c.Name.ToLower() == search).ToList();

//        return result;
//    }

//    public async ValueTask<IssueCategoryForResultDto> GetByIdAsync(long id)
//    {
//        var issueCategory = await IssueCategoryRepository
//            .GetAsync(issueCategory => issueCategory.Id.Equals(id));
//        if (issueCategory is null)
//            throw new CustomException(404, "IssueCategory not found");
//        return mapper.Map<IssueCategoryForResultDto>(issueCategory);
//    }

//    public async ValueTask<IssueCategoryForResultDto> UpdateAsync(IssueCategoryForUpdateDto dto)
//    {
//        var updatingIssueCategory = await IssueCategoryRepository
//            .GetAsync(issueCategory => issueCategory.Id.Equals(dto.Id));
//        if (updatingIssueCategory is null)
//            throw new CustomException(404, "IssueCategory not found");

//        this.mapper.Map(dto, updatingIssueCategory);
//        updatingIssueCategory.UpdatedAt = DateTime.UtcNow;
//        await IssueCategoryRepository.SaveChangesAsync();
//        return mapper.Map<IssueCategoryForResultDto>(updatingIssueCategory);
//    }
//}
