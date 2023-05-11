using Ahsan.Domain.Configurations;
using Ahsan.Service.DTOs.CompanyEmployees;
using Ahsan.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ahsan.WebApi.Controllers
{
    public class CompanyEmployeesController : BaseController
    {
        private readonly ICompanyEmployeeService companyEmployeeService;
        public CompanyEmployeesController(ICompanyEmployeeService companyEmployeeService)
        {
            this.companyEmployeeService = companyEmployeeService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> PostEmployeeAsync(CompanyEmployeeForCreationDto dto)
         => Ok(new
         {
             Code = 200,
             Error = "Success",
             Data = await this.companyEmployeeService.CreateAsync(dto)
         });

        [HttpPut("update")]
        public async Task<IActionResult> PutEmployeeAsync(CompanyEmployeeForUpdateDto dto)
             => Ok(new
             {
                 Code = 200,
                 Error = "Success",
                 Data = await this.companyEmployeeService.ModifyAsync(dto)
             });

        [HttpDelete("delete/{id:long}")]
        public async Task<IActionResult> DeleteEmployeeAsync(long id)
             => Ok(new
             {
                 Code = 200,
                 Error = "Success",
                 Data = await this.companyEmployeeService.DeleteAsync(id)
             });

        [HttpGet("get-by-id/{id:long}")]
        public async Task<IActionResult> GetByIdAsync(long id)
             => Ok(new
             {
                 Code = 200,
                 Error = "Success",
                 Data = await this.companyEmployeeService.GetByIdAsync(id)
             });

        [HttpGet("get-list")]
        public async Task<IActionResult> GetAllEmployees(string search = null, PaginationParams @params = null)
             => Ok(new
             {
                 Code = 200,
                 Error = "Success",
                 Data = await this.companyEmployeeService.GetAllAsync(@params,search)
             });
    }
}
