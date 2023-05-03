using Ahsan.Domain.Configurations;
using Ahsan.Service.DTOs.Issues;
using Ahsan.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ahsan.WebApi.Controllers
{
    public class IssuesController : BaseController
    {
        private readonly IIssueService issueService;
        public IssuesController(IIssueService issueService)
        {
            this.issueService = issueService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> PostIssueAsync(IssueForCreationDto dto)
         => Ok(new
         {
             Code = 200,
             Error = "Success",
             Data = await this.issueService.CreateAsync(dto)
         });

        [HttpPut("update")]
        public async Task<IActionResult> PutIssueAsync(IssueForUpdateDto dto)
             => Ok(new
             {
                 Code = 200,
                 Error = "Success",
                 Data = await this.issueService.UpdateAsync(dto)
             });

        [HttpDelete("delete/{id:long}")]
        public async Task<IActionResult> DeleteIssueAsync(long id)
             => Ok(new
             {
                 Code = 200,
                 Error = "Success",
                 Data = await this.issueService.DeleteAsync(id)
             });

        [HttpGet("get-by-id/{id:long}")]
        public async Task<IActionResult> GetByIdAsync(long id)
             => Ok(new
             {
                 Code = 200,
                 Error = "Success",
                 Data = await this.issueService.GetByIdAsync(id)
             });

        [HttpGet("get-list")]
        public async Task<IActionResult> GetAllIssues(PaginationParams @params, string search = null)
             => Ok(new
             {
                 Code = 200,
                 Error = "Success",
                 Data = await this.issueService.GetAllAsync(@params, search)
             });
    }
}
