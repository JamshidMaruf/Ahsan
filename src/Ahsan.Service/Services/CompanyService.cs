using Ahsan.Data.IRepositories;
using Ahsan.Domain.Entities;
using Ahsan.Service.DTOs.Companies;
using Ahsan.Service.Exceptions;
using Ahsan.Service.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ahsan.Service.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IRepository<Company> companyRepository;
        private readonly IMapper mapper;
        public CompanyService(IRepository<Company> repository, IMapper mapper)
        {
            this.companyRepository = repository;
            this.mapper = mapper;
        }
        public async ValueTask<CompanyForResultDto> CreateAsync(CompanyForCreationDto dto)
        {
            var company = await this.companyRepository.GetAsync(c => c.Name.ToLower() == dto.Name.ToLower() && c.OwnerId == dto.OwnerId);

            if (company is not null)
            {
                throw new AhsanException(403, "Company already exist");
            }

            Company mappedCompany = mapper.Map<Company>(dto);

            try
            {
                var result = await this.companyRepository.InsertAsync(mappedCompany);
                await this.companyRepository.SaveChangesAsync();

                return this.mapper.Map<CompanyForResultDto>(result);
            }

            catch (Exception)
            {
                throw new AhsanException(500, "Something went wrong");
            }
        }

        public async ValueTask<bool> DeleteAsync(Expression<Func<Company, bool>> expression)
        {
            var company = await this.companyRepository.GetAsync(expression);

            if (company is null)
            {
                throw new AhsanException(404, "Company not found");
            }

            await companyRepository.DeleteAsync(company);

            await this.companyRepository.SaveChangesAsync();

            return true;
        }

        public async ValueTask<IEnumerable<CompanyForResultDto>> GetAllAsync(Expression<Func<Company, bool>> expression = null, string search = null)
        {
            var companies = companyRepository.GetAll(expression, new string[] {"User"} ,isTracking: false);

            var matchingCompanies = await companies.Where(
                c => c.Name.ToLower() == search ).ToListAsync();

            try
            {
                var result = mapper.Map<IEnumerable<CompanyForResultDto>>(matchingCompanies);
                return result;
            }

            catch
            {
                throw new AhsanException(500, "Something went wromg");
            }
        }

        public async ValueTask<CompanyForResultDto> GetAsync(Expression<Func<Company, bool>> expression)
        {
            var company = await companyRepository.GetAsync(expression);

            if (company is null)
                throw new AhsanException(404, "Company not found");

            try
            {
                var result = mapper.Map<CompanyForResultDto>(company);
                return result;
            }

            catch
            {
                throw new AhsanException(500, "Something went wrong");
            }
        }

        public async ValueTask<CompanyForResultDto> UpdateAsync(long id, CompanyForResultDto dto)
        {
            var updatingCompany = await companyRepository.GetAsync(u => u.Id == id);

            if (updatingCompany is null)
            {
                throw new AhsanException(404, "Company not found");
            }

            var company = mapper.Map<Company>(dto);

            company.UpdatedAt = DateTime.UtcNow;

            await companyRepository.UpdateAsync(company);

            await companyRepository.SaveChangesAsync();

            return mapper.Map<CompanyForResultDto>(company);
        }
    }
}
