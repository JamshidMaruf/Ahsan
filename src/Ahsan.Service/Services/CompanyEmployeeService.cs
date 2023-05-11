using Ahsan.Data.IRepositories;
using Ahsan.Domain.Configurations;
using Ahsan.Domain.Entities;
using Ahsan.Service.DTOs.CompanyEmployees;
using Ahsan.Service.DTOs.Issues;
using Ahsan.Service.Exceptions;
using Ahsan.Service.Interfaces;
using AutoMapper;

namespace Ahsan.Service.Services
{
    public class CompanyEmployeeService : ICompanyEmployeeService
    {
        private readonly IMapper mapper;
        private readonly IUserService userService;
        private readonly ICompanyService companyService;
        private readonly IPositionService positionService;
        private readonly IRepository<Issue> issueRepository;
        private readonly IIssueCategoryService issueCategoryService;
        private readonly IRepository<CompanyEmployee> companyEmployeeRepository;
        public CompanyEmployeeService(
            IMapper mapper,
            IUserService userService,
            ICompanyService companyService,
            IPositionService positionService,
            IRepository<Issue> issueRepository,
            IIssueCategoryService issueCategoryService,
            IRepository<CompanyEmployee> companyEmployeeRepository)
        {
            this.mapper = mapper;
            this.userService = userService;
            this.companyService = companyService;
            this.issueRepository = issueRepository;
            this.positionService = positionService;
            this.issueCategoryService = issueCategoryService;
            this.companyEmployeeRepository = companyEmployeeRepository;
        }

        public async ValueTask<CompanyEmployeeForResultDto> CreateAsync(CompanyEmployeeForCreationDto dto)
        {
            var emloyee = await this.companyEmployeeRepository.SelectAsync(t => t.EmployeeId == dto.EmployeeId && !t.IsDeleted);
            if (emloyee is not null)
                throw new CustomException(403, "Employee alread exist");

            var mappedEmployee = this.mapper.Map<CompanyEmployee>(dto);
            var createdCompany = await this.companyEmployeeRepository.InsertAsync(mappedEmployee);
            await this.companyEmployeeRepository.SaveChangesAsync();

            var result = this.mapper.Map<CompanyEmployeeForResultDto>(createdCompany);

            result.Employee = await this.userService.GetByIdAsync(dto.EmployeeId);
            result.Company = await this.companyService.GetByIdAsync(dto.CompanyId);
            result.Position = await this.positionService.GetByIdAsync(dto.PositionId);
            var assignments = this.issueRepository.SelectAll(t => t.AssignedId == result.Id);
            result.Assignments = this.mapper.Map<List<IssueForEmployeeDto>>(assignments);

            return result;
        }

        public async ValueTask<bool> DeleteAsync(long id)
        {
            var emloyee = await this.companyEmployeeRepository.SelectAsync(t => t.Id == id && !t.IsDeleted);
            if (emloyee is null)
                throw new CustomException(404, "Employee not found");

            bool result = await this.companyEmployeeRepository.DeleteAsync(emloyee);
            await this.companyEmployeeRepository.SaveChangesAsync();
            return result;
        }

        public async ValueTask<IEnumerable<CompanyEmployeeForResultDto>> GetAllAsync(PaginationParams @params = null, string search = null)
        {
            if(@params is null)
            {
                var pagedEmployee = this.companyEmployeeRepository
               .SelectAll(t => !t.IsDeleted, /*includes: new string[] { "User", "Company", "Position", "Issue" },*/ isTracking: false);
                var pagedResult = this.mapper.Map<IEnumerable<CompanyEmployeeForResultDto>>(pagedEmployee);


                if (!string.IsNullOrEmpty(search))
                {
                    var matchingEmployee = pagedResult.Where(c => c.Company.Name.ToLower().Contains(search.ToLower()));
                    return matchingEmployee;
                }

                return pagedResult;
            }
            var employee = this.companyEmployeeRepository
                .SelectAll(t => !t.IsDeleted, /*includes: new string[] { "User", "Company", "Position", "Issue" },*/ isTracking: false);
            var result = this.mapper.Map<IEnumerable<CompanyEmployeeForResultDto>>(employee);


            if (!string.IsNullOrEmpty(search))
            {
                var matchingEmployee = result.Where(c => c.Company.Name.ToLower().Contains(search.ToLower()));
                return matchingEmployee;
            }

            return result;
        }

        public async ValueTask<CompanyEmployeeForResultDto> GetByIdAsync(long id)
        {
            var employee = await this.companyEmployeeRepository.SelectAsync(t => t.Id == id && !t.IsDeleted);
            if (employee is null)
                throw new CustomException(404, "Employee not found");

            var result = this.mapper.Map<CompanyEmployeeForResultDto>(employee);

            result.Employee = await this.userService.GetByIdAsync(employee.EmployeeId);
            result.Company = await this.companyService.GetByIdAsync(employee.CompanyId);
            result.Position = await this.positionService.GetByIdAsync(employee.PositionId);
            var assignments = this.issueRepository.SelectAll(t => t.AssignedId == employee.Id);
            result.Assignments = this.mapper.Map<List<IssueForEmployeeDto>>(assignments);

            return result;
        }

        public async ValueTask<CompanyEmployeeForResultDto> ModifyAsync(CompanyEmployeeForUpdateDto dto)
        {
            var employee = await this.companyEmployeeRepository.SelectAsync(t => t.Id == dto.Id && !t.IsDeleted);
            if (employee is null)
                throw new CustomException(404, "Employee not found");

            this.mapper.Map(dto, employee);
            employee.UpdatedAt = DateTime.UtcNow;
            await this.companyEmployeeRepository.SaveChangesAsync();

            var result = this.mapper.Map<CompanyEmployeeForResultDto>(employee);

            result.Employee = await this.userService.GetByIdAsync(employee.EmployeeId);
            result.Company = await this.companyService.GetByIdAsync(employee.CompanyId);
            result.Position = await this.positionService.GetByIdAsync(employee.PositionId);
            var assignments = this.issueRepository.SelectAll(t => t.AssignedId == employee.Id);
            result.Assignments = this.mapper.Map<List<IssueForEmployeeDto>>(assignments);

            return result;
        }
    }
}
