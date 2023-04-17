using Ahsan.Service.DTOs.Companies;
using Ahsan.Service.DTOs.Users;
using Ahsan.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ahsan.WebApi.Controllers;

public class UsersOrderController : BaseController
{
    private readonly IUserService userService; 
    public UsersOrderController(IUserService userService)
    {
        this.userService = userService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> PostCompanyAsync(UserForCreationDto dto)
       => Ok(await this.userService.CreateAsync(dto));

    [HttpPut("update")]
    public async Task<IActionResult> PutCompanyAsync(UserForUpdateDto dto)
        => Ok(await this.userService.UpdateAsync(dto));

    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteCompany(long id)
        => Ok(await this.userService.DeleteAsync(id));

    [HttpGet("get-by-id/{id:long}")]
    public async Task<IActionResult> GetByIdAsync(long id)
        => Ok(await this.userService.GetByIdAsync(id));

    [HttpGet("get-list")]
    public async Task<IActionResult> GetAllCompany()
        => Ok(await this.userService.GetAllAsync());
}
