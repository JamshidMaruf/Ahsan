using Ahsan.Service.DTOs.Users;
using Ahsan.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ahsan.WebApi.Controllers;

public class AccountsController : BaseController
{
    private readonly IUserService userService;
    public AccountsController(IUserService userService)
    {
        this.userService = userService;
    }


    [HttpPost("sign-up")]
    public async Task<IActionResult> PostCompanyAsync(UserForCreationDto dto)
       => Ok(await this.userService.CreateAsync(dto));


}
