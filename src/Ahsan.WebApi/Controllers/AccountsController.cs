using Ahsan.Service.DTOs.Users;
using Ahsan.Service.Interfaces;
using Ahsan.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ahsan.WebApi.Controllers;

public class AccountsController : BaseController
{
    private readonly IAuthService authService;
    private readonly IUserService userService;
    public AccountsController(IUserService userService, IAuthService authService)
    {
        this.userService = userService;
        this.authService = authService;
    }


    [HttpPost("sign-up")]
    public async Task<IActionResult> PostCompanyAsync(UserForCreationDto dto)
       => Ok(await this.userService.CreateAsync(dto));


    [HttpPost("token")]
    public async Task<IActionResult> GenerateToken(string username, string password = null)
    {
        var token = await this.authService.GenerateTokenAsync(username, password);
        return Ok(new Response
        {
            Code = 200,
            Error = "Success",
            Data = token
        });
    }
}
