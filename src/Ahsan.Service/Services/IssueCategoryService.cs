//using Ahsan.Data.IRepositories;
//using Ahsan.Domain.Entities;
//using Ahsan.Service.DTOs.Issues;
//using Ahsan.Service.Exceptions;
//using Ahsan.Service.Interfaces;
//using AutoMapper;
//using Microsoft.EntityFrameworkCore;
//using System.Linq.Expressions;

//namespace Ahsan.Service.Services
//{
//    public class IssueCategoryService : IIssueCategoryService
//    {
//        private readonly IRepository<IssueCategory> IssueCategoryRepository;
//        private readonly IMapper mapper;
//        public IssueCategoryService(IRepository<IssueCategory> repository, IMapper mapper)
//        {
//            this.IssueCategoryRepository = repository;
//            this.mapper = mapper;
//        }
//        public async ValueTask<IssueCategoryForResultDto> CreateAsync(IssueCategoryForCreationDto dto)
//        {
//            IssueCategory issueCategory = await this.IssueCategoryRepository.GetAsync(u => u.Name.ToLower() == dto.Name.ToLower() && u.CompanyId == dto.CompanyId);

//            if (issueCategory is not null)
//            {
//                throw new CustomException(403, "IssueCategory already exist");
//            }

//            var mappedIssueCategory = mapper.Map<IssueCategory>(dto);

//            try
//            {
//                var result = await this.IssueCategoryRepository.InsertAsync(mappedIssueCategory);
//                await this.IssueCategoryRepository.SaveChangesAsync();

//                return this.mapper.Map<IssueCategoryForResultDto>(result);
//            }

//            catch (Exception)
//            {
//                throw new CustomException(500, "Something went wrong");
//            }
//        }
//        public async ValueTask<bool> DeleteAsync(Expression<Func<IssueCategory, bool>> expression)
//        {
//            var issueCategory = await this.IssueCategoryRepository.GetAsync(expression);

//            if (issueCategory is null)
//            {
//                throw new CustomException(404, "IssueCategory not found");
//            }

//            await IssueCategoryRepository.DeleteAsync(issueCategory);

//            await this.IssueCategoryRepository.SaveChangesAsync();

//            return true;
//        }

//        public async ValueTask<IEnumerable<IssueCategoryForResultDto>> GetAllAsync(Expression<Func<IssueCategory, bool>> expression = null, string search = null)
//        {
//            var issueCategories = IssueCategoryRepository.GetAll(expression, new string[] { "Company" }, isTracking: false);

//            var matchingIssueCategories = await issueCategories.Where(
//                c => c.Name.ToLower() == search).ToListAsync();

//            try
//            {
//                var result = mapper.Map<IEnumerable<IssueCategoryForResultDto>>(matchingIssueCategories);
//                return result;
//            }

//            catch
//            {
//                throw new CustomException(500, "Something went wromg");
//            }
//        }

//        public async ValueTask<IssueCategoryForResultDto> GetAsync(Expression<Func<IssueCategory, bool>> expression)
//        {
//            var issueCategory = await IssueCategoryRepository.GetAsync(expression);

//            if (issueCategory is null)
//                throw new CustomException(404, "IssueCategory not found");

//            try
//            {
//                var result = mapper.Map<IssueCategoryForResultDto>(issueCategory);
//                return result;
//            }

//            catch
//            {
//                throw new CustomException(500, "Something went wrong");
//            }
//        }

//        public async ValueTask<IssueCategoryForResultDto> UpdateAsync(long id, IssueCategoryForCreationDto dto)
//        {
//            var updatingIssueCategory = await IssueCategoryRepository.GetAsync(u => u.Id == id);

//            if (updatingIssueCategory is null)
//            {
//                throw new CustomException(404, "IssueCategory not found");
//            }

//            var issueCategory = mapper.Map<IssueCategory>(dto);

//            issueCategory.UpdatedAt = DateTime.UtcNow;

//            await IssueCategoryRepository.UpdateAsync(issueCategory);

//            await IssueCategoryRepository.SaveChangesAsync();

//            return mapper.Map<IssueCategoryForResultDto>(issueCategory);
//        }
//    }
//}
