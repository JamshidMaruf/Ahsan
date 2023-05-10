using Ahsan.Data.IRepositories;
using Ahsan.Domain.Entities;
using Ahsan.Service.DTOs.Companies;
using Ahsan.Service.DTOs.Users;
using Ahsan.Service.Exceptions;
using Ahsan.Service.Interfaces;
using Ahsan.Service.Validators.Companies;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

#pragma warning disable
namespace Ahsan.Service.Services;

public class CompanyService : ICompanyService
{
    private readonly IMapper mapper;
    private readonly IRepository<User> userRepository;
    private readonly CompanyCreateValidator validator;
    private readonly IRepository<Company> companyRepository;
    public CompanyService(
        IMapper mapper,
        IRepository<User> userRepository,
        CompanyCreateValidator validator,
        IRepository<Company> companyRepository)
    {
        this.mapper = mapper;
        this.validator = validator;
        this.userRepository = userRepository;
        this.companyRepository = companyRepository;
    }

    public async ValueTask<CompanyForResultDto> CreateAsync(CompanyForCreationDto dto)
    {
        var validatorResult = await validator.ValidateAsync(dto);
        if (validatorResult.Errors.Any())
            throw new CustomException(400, validatorResult.Errors.First().ErrorMessage);

        Company company = await this.companyRepository
            .SelectAsync(c => c.Name.ToLower() == dto.Name.ToLower() && !c.IsDeleted);
        if (company is not null)
            throw new CustomException(409, "Company already exist for given argument");

        var user = await this.userRepository
            .SelectAsync(t => t.Id.Equals(dto.OwnerId) && !t.IsDeleted);
        if (user is null)
            throw new CustomException(404, "User is not found");



        Company mappedCompany = this.mapper.Map<Company>(dto);
        Company createdCompany = await this.companyRepository.InsertAsync(mappedCompany);
        await this.companyRepository.SaveChangesAsync();

        var result = this.mapper.Map<CompanyForResultDto>(createdCompany);
        result.Owner = this.mapper.Map<UserForResultDto>(user);
        return result;
    }

    public async ValueTask<bool> DeleteAsync(long companyId)
    {
        Company company = await this.companyRepository
            .SelectAsync(company => company.Id.Equals(companyId) && !company.IsDeleted);
        if (company is null)
            throw new CustomException(404, "Company is not found for given id");

        bool result = await this.companyRepository.DeleteAsync(company);
        await this.companyRepository.SaveChangesAsync();
        return result;
    }

    public async ValueTask<List<CompanyForResultDto>> GetAllAsync(string search = null)
    {
        IQueryable<Company> companies = companyRepository
            .SelectAll(t => !t.IsDeleted, isTracking: false)
            .Include(t => t.Owner);

        var result =
            mapper.Map<List<CompanyForResultDto>>(companies);

        if (!string.IsNullOrEmpty(search))
            return result.Where(c =>
                c.Name.ToLower().Contains(search.ToLower())).ToList();
        return result;
    }

    public async ValueTask<CompanyForResultDto> GetByIdAsync(long companyId)
    {
        Company company = await companyRepository
            .SelectAsync(company => company.Id.Equals(companyId) && !company.IsDeleted);
        if (company is null)
            throw new CustomException(404, "Company not found for given id");

        var user = await this.userRepository
            .SelectAsync(t => t.Id.Equals(company.OwnerId) && !t.IsDeleted);
        if (user is null)
            throw new CustomException(404, "User is not found");

        var result = this.mapper.Map<CompanyForResultDto>(company);
        result.Owner = this.mapper.Map<UserForResultDto>(user);
        return result;
    }

    public async ValueTask<CompanyForResultDto> ModifyAsync(CompanyForUpdateDto dto)
    {
        Company company = await companyRepository.SelectAsync(u => u.Id.Equals(dto.Id) && !u.IsDeleted);
        if (company is null)
            throw new CustomException(404, "Company not found");

        var user = await this.userRepository
            .SelectAsync(t => t.Id.Equals(company.OwnerId) && !t.IsDeleted);
        if (user is null)
            throw new CustomException(404, "User is not found");

        this.mapper.Map(dto, company);
        company.UpdatedAt = DateTime.UtcNow;
        await companyRepository.SaveChangesAsync();

        var result = this.mapper.Map<CompanyForResultDto>(company);
        result.Owner = this.mapper.Map<UserForResultDto>(user);
        return result;
    }
}
