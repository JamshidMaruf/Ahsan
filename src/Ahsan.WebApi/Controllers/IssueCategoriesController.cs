using Ahsan.Service.DTOs.Companies;
using Ahsan.Service.DTOs.Issues;
using Ahsan.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ahsan.WebApi.Controllers;

#pragma warning disable
public class IssueCategoriesController : BaseController
{
    private readonly IIssueCategoryService issueCategoryService;
    public IssueCategoriesController(IIssueCategoryService issueCategoryService)
    {
        this.issueCategoryService = issueCategoryService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> PostIssueCategoryAsync(IssueCategoryForCreationDto dto)
         => Ok(new
         {
             Code = 200,
             Error = "Success",
             Data = await this.issueCategoryService.CreateAsync(dto)
         });

    [HttpPut("update")]
    public async Task<IActionResult> PutIssueCategoryAsync(IssueCategoryForUpdateDto dto)
         => Ok(new
         {
             Code = 200,
             Error = "Success",
             Data = await this.issueCategoryService.UpdateAsync(dto)
         });

    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteIssueCategoryAsync(long id)
         => Ok(new
         {
             Code = 200,
             Error = "Success",
             Data = await this.issueCategoryService.DeleteAsync(id)
         });

    [HttpGet("get-by-id/{id:long}")]
    public async Task<IActionResult> GetByIdAsync(long id)
         => Ok(new
         {
             Code = 200,
             Error = "Success",
             Data = await this.issueCategoryService.GetByIdAsync(id)
         });

    [HttpGet("get-list")]
    public async Task<IActionResult> GetAllIssueCategories()
         => Ok(new
         {
             Code = 200,
             Error = "Success",
             Data = await this.issueCategoryService.GetAllAsync()
         });
}
